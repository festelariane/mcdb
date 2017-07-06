using Mercedes.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mercedes.Core.Domain;
using Mercedes.Data.Repositories.Contract;

namespace Mercedes.Services.Impl
{
    public class ContactService : IContactService
    {       
        private readonly IContactRepository _contactRepository;
        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;            
        }

        public Contact GetContactById(int Id)
        {
            return _contactRepository.Get(Id);
        }

        public IList<Contact> GetAllContacts()
        {
            return _contactRepository.GetAll().ToList();
        }

        public bool AddContact(Contact contact)
        {
            try
            {
                _contactRepository.Add(contact);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }      

        public bool DeleteContact(Contact contact)
        {
            try
            {
                _contactRepository.Delete(contact);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }        
        
        public bool UpdateContact(Contact contact)
        {
            try
            {
                _contactRepository.Update(contact);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }                
    }
}
