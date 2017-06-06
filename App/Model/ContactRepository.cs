using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Model;

namespace Web.Model
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactContext _context;

        public ContactRepository(ContactContext context)
        {
            _context = context;
            //Add(new Contact { Name = "Item1" });
        }

        public IEnumerable<Contact> GetAll()
        {
            return _context.Contact.ToList();
        }

        public void Add(Contact Contact)
        {
            _context.Contact.Add(Contact);
            _context.SaveChanges();
        }

        public Contact Find(int ID)
        {
            return _context.Contact.FirstOrDefault(t => t.ID == ID);
        }

        public Contact FindByName(string name)
        {
            return _context.Contact.FirstOrDefault(t => t.Name.Contains(name));
        }

        public Contact FindByEmail(string email)
        {
            return _context.Contact.FirstOrDefault(t => t.Email == email);
        }

        public Contact FindByPhone(int phoneNumber)
        {
            return _context.Contact.FirstOrDefault(t => t.PhoneNumber == phoneNumber);
        }

        public void Remove(int ID)
        {
            var entity = _context.Contact.First(t => t.ID == ID);
            _context.Contact.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Contact Contact)
        {
            _context.Contact.Update(Contact);
            _context.SaveChanges();
        }
    }
}
