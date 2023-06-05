using DeviceDetectorNET;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Threading.Tasks;

namespace ps09.Utlis
{
    public class PageRedirection
    {
        private readonly RequestDelegate _next;

        public PageRedirection(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string userAgent = context.Request.Headers["User-Agent"].ToString();
            var userAgentParser = new DeviceDetector(userAgent);
            userAgentParser.Parse();

            string browserName = userAgentParser.GetClient().Match.Name;
            if (browserName.Contains("Edge") || browserName.Contains("Edg") || browserName.Contains("IE"))
            {
                
                    context.Response.Redirect("https://www.mozilla.org/pl/firefox/new/");
                    return;
                
                
            }

            await _next(context);
        }
    }
}