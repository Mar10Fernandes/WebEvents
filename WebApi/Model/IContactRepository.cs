using System;
using System.Collections.Generic;
using ClassLibrary;

namespace WebApi.Model
{
    public interface IContactRepository
    {
        void Add(Contact item);
        IEnumerable<Contact> GetAll();
        IEnumerable<Contact> Find(int ID);
        IEnumerable<Contact> FindByName(string name);
        IEnumerable<Contact> FindByEmail(string email);
        IEnumerable<Contact> FindByPhone(int phone);
        void Remove(int ID);
        void Update(Contact contact);
    }
}
