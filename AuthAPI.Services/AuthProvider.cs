using System;
using AuthAPI.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthAPI.Data.Repositories;
using AuthAPI.Data.Models;
using AuthAPI.Services.Models;
using System.Configuration;
using NLog;

namespace AuthAPI.Services
{

    public class AuthProvider
    {
        private string _serverPrivateKey;
        const int RANDOM_PAYLOAD_LENGTH = 32;
        private PostgresRepository _deviceKeyRepo;
        private static Logger _logger;
        public AuthProvider()
        {
            _serverPrivateKey = ConfigurationManager.AppSettings["serverKey"];
            _deviceKeyRepo = new PostgresRepository();
            _logger = LogManager.GetCurrentClassLogger();
        }
        
        string GetRandomPayload()
        {
            return CryptoHelpers.GetSHA(CryptoHelpers.GetRandomSalt(RANDOM_PAYLOAD_LENGTH));
        }
        string GetServerPayloadSignature(string randomPayload)
        {
            return CryptoHelpers.GetSHA(CryptoHelpers.GetXOR(randomPayload, _serverPrivateKey));
        }
        public RandomPayloadAndServerSignature GetRandomPayloadAndServerSignature(DeviceKey device) {
            var randomPayload = GetRandomPayload();
            var s = GetDevicePayloadSignatureTest(randomPayload, device);
            _logger.Info("Device signature payload "+s);
            return new RandomPayloadAndServerSignature(randomPayload, GetServerPayloadSignature(randomPayload));
        }
        public bool CheckServerAndDevicePayload(PacketWithId packet, DeviceKey device)
        {
            if (GetServerPayloadSignature(packet.randomPayload) == packet.serverSignature && 
                GetDevicePayloadSignature(packet.randomPayload, device) == packet.deviceSignature) return true;
            return false;
        }
        
        public string GetJWTToken(DeviceKey device)
        {
            if (JWTService.TokenKeysHolder.keys.Count == 0) return null;
            string sec = JWTService.TokenKeysHolder.keys[0].k;

            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Convert.FromBase64String(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);
            header.Add("kid", JWTService.TokenKeysHolder.keys[0].kid);
            var claims = new[]
            {
               new Claim("device", device.OriginalID.ToString()),
               new Claim(JwtRegisteredClaimNames.Exp, ((Int32)(DateTime.UtcNow.AddDays(1).Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString())
               
            };
            var payload = new JwtPayload(claims);
            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();
            var tokenString = handler.WriteToken(secToken);
            try
            {
                RedisRepository rp = new RedisRepository();
                rp.SaveToken(device.ID.ToString(), tokenString);
                rp.SaveCar(device.ID.ToString(), device.CarID.ToString());
                var x = rp.GetByKey("token:" + tokenString + ":deviceid");

                var y = rp.GetByKey("deviceid:" + device.ID.ToString() + ":carid");
            }
            catch (Exception ex) {
                _logger.Error(ex);
            }
            
            //_logger.Info(tokenString);
            return tokenString;
        }
        string GetDevicePayloadSignature(string randomPayload, DeviceKey device)
        {
            return CryptoHelpers.GetSHA(CryptoHelpers.GetXOR(randomPayload, device.SecretKey));
        }

        public string GetDevicePayloadSignatureTest(string randomPayload, DeviceKey device)
        {
            return CryptoHelpers.GetSHA(CryptoHelpers.GetXOR(randomPayload, device.SecretKey));
        }

        public DeviceKey GetDevice(string deviceID) {
            try
            {
                return _deviceKeyRepo.GetById(deviceID);
            }
            catch (Exception ex) { return null; }
        }


    }
}
