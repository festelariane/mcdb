using Mercedes.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Services.Contract
{
    public interface IContactService
    {      
        Contact GetContactById(int Id);
        IList<Contact> GetAllContacts();
        bool AddContact(Contact contact);
        bool DeleteContact(Contact contact);
        bool UpdateContact(Contact contact);        
    }
}
