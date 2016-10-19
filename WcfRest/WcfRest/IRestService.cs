using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WcfRest
{
    [ServiceContract]
    public interface IRestService
    {
        [OperationContract]
        [WebGet(UriTemplate = "")]
        string GetMainPage();

        [OperationContract]
        [WebGet(UriTemplate = "username")]
        string GetUserName();

        [OperationContract]
        [WebInvoke(UriTemplate = "username", Method = "PUT")]
        void PutUserName(string username);

        [OperationContract]
        [WebGet(UriTemplate = "accounts")]
        List<Account> GetAccounts();

        [OperationContract]
        [WebGet(UriTemplate = "accounts/{stringGuid}")]
        Account GetAccount(string stringGuid);

        [OperationContract]
        [WebInvoke(UriTemplate = "accounts", Method = "POST")]
        void PostAccount(Account account);

        [OperationContract]
        [WebInvoke(UriTemplate = "accounts/{stringGuid}", Method = "PUT")]
        void PutAccount(string stringGuid, Account account);
    }
}