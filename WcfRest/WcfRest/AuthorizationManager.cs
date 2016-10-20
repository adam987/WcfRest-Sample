using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfRest
{
    public class AuthorizationManager : ServiceAuthorizationManager
    {
        private static IncomingWebRequestContext IncomingContext => WebOperationContext.Current?.IncomingRequest;
        private static OutgoingWebResponseContext OutgoingContext => WebOperationContext.Current?.OutgoingResponse;

        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            var authHeader = IncomingContext.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authHeader))
            {
                var credentials = ConvertFromBase64(authHeader.Substring(6)).Split(':');
                if ((credentials.Length == 2) && (credentials[0] == "user") && (credentials[1] == "pass"))
                    return true;
            }

            OutgoingContext.Headers.Add("WWW-Authenticate", "Basic realm=\"WcfRest\"");
            OutgoingContext.StatusCode = HttpStatusCode.Unauthorized;
            return false;
        }

        private static string ConvertFromBase64(string header)
            => Encoding.ASCII.GetString(Convert.FromBase64String(header));
    }
}