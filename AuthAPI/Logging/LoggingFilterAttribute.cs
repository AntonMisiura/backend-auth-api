using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Web.Http.Tracing;
using System.Web.Http;
using AuthAPI.Helpers;
using NLog;
using System.Text;

namespace AuthAPI.Helpers
{
    public class LoggingFilterAttribute: ActionFilterAttribute  
    {
     
        private static Logger _logger = LogManager.GetLogger("DeviceAuth.Auth");
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            
        }

        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            var log = new StringBuilder();
            foreach (var item in context.ActionContext.ActionArguments)
            {
                log.Append(item);
            }
            if (context.Response != null) {
                if (context.Response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    log.Append(".(Response Code)." + context.Response.StatusCode.ToString("D"));
                    _logger.Info(log);
                    //_logger.Info("Response: " + context.Response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    log.Append(".(Response Code)." + context.Response.StatusCode.ToString("D"));
                    _logger.Error(log);

                }
            }
            
            
        }
    }  
}