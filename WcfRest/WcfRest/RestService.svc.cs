using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WcfRest
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class RestService : IRestService
    {
        private readonly List<Account> _accounts = new List<Account>();
        private string _username = "";
        private static OutgoingWebResponseContext Response => WebOperationContext.Current?.OutgoingResponse;
    }
}