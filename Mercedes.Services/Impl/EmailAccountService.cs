using Mercedes.Core.Domain;
using Mercedes.Data.Repositories.Contract;
using Mercedes.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Services.Impl
{
    public partial class EmailAccountService : IEmailAccountService
    {
        private readonly IEmailAccountRepository _emailAccountRepository;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="emailAccountRepository">Email account repository</param>
        /// <param name="eventPublisher">Event published</param>
        public EmailAccountService(IEmailAccountRepository emailAccountRepository)
        {
            this._emailAccountRepository = emailAccountRepository;
        }

        /// <summary>
        /// Inserts an email account
        /// </summary>
        /// <param name="emailAccount">Email account</param>
        public virtual void InsertEmailAccount(EmailAccount emailAccount)
        {
            if (emailAccount == null)
                throw new ArgumentNullException("emailAccount");

            emailAccount.Email = CodeHelper.EnsureNotNull(emailAccount.Email);
            emailAccount.DisplayName = CodeHelper.EnsureNotNull(emailAccount.DisplayName);
            emailAccount.Host = CodeHelper.EnsureNotNull(emailAccount.Host);
            emailAccount.Username = CodeHelper.EnsureNotNull(emailAccount.Username);
            emailAccount.Password = CodeHelper.EnsureNotNull(emailAccount.Password);

            emailAccount.Email = emailAccount.Email.Trim();
            emailAccount.DisplayName = emailAccount.DisplayName.Trim();
            emailAccount.Host = emailAccount.Host.Trim();
            emailAccount.Username = emailAccount.Username.Trim();
            emailAccount.Password = emailAccount.Password.Trim();

            emailAccount.Email = CodeHelper.EnsureMaximumLength(emailAccount.Email, 255);
            emailAccount.DisplayName = CodeHelper.EnsureMaximumLength(emailAccount.DisplayName, 255);
            emailAccount.Host = CodeHelper.EnsureMaximumLength(emailAccount.Host, 255);
            emailAccount.Username = CodeHelper.EnsureMaximumLength(emailAccount.Username, 255);
            emailAccount.Password = CodeHelper.EnsureMaximumLength(emailAccount.Password, 255);
            emailAccount.CreatedOn = DateTime.UtcNow;
            try
            {
                _emailAccountRepository.Add(emailAccount);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Updates an email account
        /// </summary>
        /// <param name="emailAccount">Email account</param>
        public virtual void UpdateEmailAccount(EmailAccount emailAccount)
        {
            if (emailAccount == null)
                throw new ArgumentNullException("emailAccount");

            emailAccount.Email = CodeHelper.EnsureNotNull(emailAccount.Email);
            emailAccount.DisplayName = CodeHelper.EnsureNotNull(emailAccount.DisplayName);
            emailAccount.Host = CodeHelper.EnsureNotNull(emailAccount.Host);
            emailAccount.Username = CodeHelper.EnsureNotNull(emailAccount.Username);
            emailAccount.Password = CodeHelper.EnsureNotNull(emailAccount.Password);

            emailAccount.Email = emailAccount.Email.Trim();
            emailAccount.DisplayName = emailAccount.DisplayName.Trim();
            emailAccount.Host = emailAccount.Host.Trim();
            emailAccount.Username = emailAccount.Username.Trim();
            emailAccount.Password = emailAccount.Password.Trim();

            emailAccount.Email = CodeHelper.EnsureMaximumLength(emailAccount.Email, 255);
            emailAccount.DisplayName = CodeHelper.EnsureMaximumLength(emailAccount.DisplayName, 255);
            emailAccount.Host = CodeHelper.EnsureMaximumLength(emailAccount.Host, 255);
            emailAccount.Username = CodeHelper.EnsureMaximumLength(emailAccount.Username, 255);
            emailAccount.Password = CodeHelper.EnsureMaximumLength(emailAccount.Password, 255);

            _emailAccountRepository.Update(emailAccount);
        }

        /// <summary>
        /// Deletes an email account
        /// </summary>
        /// <param name="emailAccount">Email account</param>
        public virtual void DeleteEmailAccount(EmailAccount emailAccount)
        {
            if (emailAccount == null)
                throw new ArgumentNullException("emailAccount");

            if (GetAllEmailAccounts().Count == 1)
                throw new Exception("You cannot delete this email account. At least one account is required.");

            _emailAccountRepository.Delete(emailAccount);
        }

        /// <summary>
        /// Gets an email account by identifier
        /// </summary>
        /// <param name="emailAccountId">The email account identifier</param>
        /// <returns>Email account</returns>
        public virtual EmailAccount GetEmailAccountById(int emailAccountId)
        {
            if (emailAccountId == 0)
                return null;

            return _emailAccountRepository.Get(emailAccountId);
        }

        /// <summary>
        /// Gets all email accounts
        /// </summary>
        /// <returns>Email accounts list</returns>
        public virtual IList<EmailAccount> GetAllEmailAccounts()
        {
            return _emailAccountRepository.GetAll().ToList();
        }
    }
}
