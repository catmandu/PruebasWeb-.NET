using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Threading;
using System.Threading.Tasks;

namespace RegistroIdentity.Facebook
{
    public class FacebookChannelHandler:HttpClientHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            if(!request.RequestUri.AbsolutePath.Contains("/oath"))
            {
                request.RequestUri = new Uri(request.RequestUri.AbsoluteUri.Replace("?access_token","&access_token"));
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}