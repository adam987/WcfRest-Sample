using System;
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
            return true;
        }

        private static string ConvertFromBase64(string header)
            => Encoding.ASCII.GetString(Convert.FromBase64String(header));
    }
}