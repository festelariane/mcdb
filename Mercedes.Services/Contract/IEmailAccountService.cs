using Mercedes.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Services.Contract
{
    public partial interface IEmailAccountService
    {
        void InsertEmailAccount(EmailAccount emailAccount);
        void UpdateEmailAccount(EmailAccount emailAccount);
        void DeleteEmailAccount(EmailAccount emailAccount);
        EmailAccount GetEmailAccountById(int emailAccountId);
        IList<EmailAccount> GetAllEmailAccounts();
    }
}
