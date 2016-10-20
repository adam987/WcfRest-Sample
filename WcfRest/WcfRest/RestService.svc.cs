using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public string GetMainPage()
        {
            return "This is the main page";
        }

        public string GetUserName()
        {
            return _username;
        }

        public void PutUserName(string username)
        {
            _username = username;
        }

        public List<Account> GetAccounts()
        {
            return _accounts.ToList();
        }

        public Account GetAccount(string stringGuid)
        {
            var guid = Guid.Parse(stringGuid);
            var account = _accounts.SingleOrDefault(acc => acc.Guid == guid);
            if (account != null)
                return account;

            throw new WebFaultException(HttpStatusCode.NotFound);
        }

        public void PostAccount(Account account)
        {
            account.Guid = Guid.NewGuid();
            account.CreationDate = DateTime.Now;
            CreateAccount(account);
        }

        public void PutAccount(string stringGuid, Account account)
        {
            var guid = Guid.Parse(stringGuid);
            account.Guid = guid;

            if (account.Balance < 0)
                throw new WebFaultException<CustomError>(
                    new CustomError { Account = account, Message = "Invalid balance" }, HttpStatusCode.BadRequest);

            var existingAccount = _accounts.SingleOrDefault(acc => acc.Guid == guid);
            if (existingAccount == null)
            {
                CreateAccount(account);
                return;
            }

            _accounts.Remove(existingAccount);
            _accounts.Add(account);
        }

        private void CreateAccount(Account account)
        {
            _accounts.Add(account);
            Response.Location = $"accounts/{account.Guid}";
            Response.StatusCode = HttpStatusCode.Created;
        }
    }
}