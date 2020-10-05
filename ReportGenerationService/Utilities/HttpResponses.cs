using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerationService.Utilities
{
    public class HttpResponses : ActionResult
    {
        public byte[] Content { get; set; }       
        public int StatusCode { get; set; }

        public HttpResponses()
        {
        }

        public static HttpResponses TeapotResult(ApiOffence offense, string propertName)
        {
            var res = new HttpResponses();
            res.Content = getContent(offense, propertName);
            res.StatusCode = 418;
            return res;
        }

        private static byte[] getContent(ApiOffence offense, string propertName)
        {
            var obj = new Dictionary<string, object>()
            {
                [propertName] = new { ErrorCode = offense.ErrorCode }
            };

            return UTF8Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            return ExecuteResultAsync(context.HttpContext);
        }
        public Task ExecuteResultAsync(HttpContext context)
        {
            var resp = context.Response;
            resp.StatusCode = StatusCode;

            if (Content != null)
            {
                resp.ContentType = "application/json";
                return resp.Body.WriteAsync(Content, 0, Content.Length);
            }

            return Task.CompletedTask;
        }
    }
}
