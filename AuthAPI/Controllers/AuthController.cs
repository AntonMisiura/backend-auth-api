using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AuthAPI.Services;
using AuthAPI.Services.Models;
using AuthAPI.Data;
using AuthAPI.Models;
using AuthAPI.Data.Models;
using NLog;

namespace AuthAPI.Controllers
{
    [RoutePrefix("auth")]
    public class AuthController : ApiController
    {
        private IDataPersistance<DeviceKey> _deviceStorage;
        private static Logger _logger;
        AuthProvider _authProvider;
        public AuthController()
            : this(new SessionDataPersistance<DeviceKey>())
        { }
        public AuthController(IDataPersistance<DeviceKey> deviceStorage) {
            _deviceStorage = deviceStorage;
            _authProvider = new AuthProvider();
            _logger = LogManager.GetCurrentClassLogger();
        }
        
        [HttpGet]
        [Route("{Device_ID}")]
        public IHttpActionResult Get(string Device_ID)
        {
            if (!string.IsNullOrEmpty(Device_ID))
            {
                var device = _authProvider.GetDevice(Device_ID);
                if (device == null) return InternalServerError();
                if (device.ID != 0) {
                    _deviceStorage.ObjectValue = device;
                    
                    return Ok(_authProvider.GetRandomPayloadAndServerSignature(device));

                }
                else return NotFound();
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("{Device_ID}")]
        public IHttpActionResult Post(string Device_ID, [FromBody]PacketFromClient packet)
        {
            if (packet != null)
            {
                var device = _deviceStorage.ObjectValue;
                if (Device_ID == device.OriginalID.ToString())
                {
                    PacketWithId pack = new PacketWithId(packet.randomPayload, packet.serverSignature, packet.deviceSignature, Device_ID);
                    if (_authProvider.CheckServerAndDevicePayload(pack, device))
                    {
                        var token = _authProvider.GetJWTToken(device);
                        if (token != null)
                        {
                            return Ok(token);
                        }
                        return InternalServerError();
                    }
                }
                else return NotFound();
                
                
            }
            return BadRequest();
        }

    
    }
}
