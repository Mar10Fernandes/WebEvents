using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Model;

namespace Web.Model
{
    public interface IContactRepository
    {
        void Add(Contact item);
        IEnumerable<Contact> GetAll();
        Contact Find(int ID);
        Contact FindByName(string name);
        Contact FindByEmail(string email);
        Contact FindByPhone(int phone);
        void Remove(int ID);
        void Update(Contact contact);
    }
}
