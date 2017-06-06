
using System.Collections.Generic;
using System.Linq;
using ClassLibrary;

namespace WebApi.Model
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactContext _context;

        public ContactRepository(ContactContext context)
        {
            _context = context;
            
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

        public IEnumerable<Contact> Find(int ID)
        {
            return _context.Contact.Where(t => t.ID == ID);
        }

        public IEnumerable<Contact> FindByName(string name)
        {
            return _context.Contact.Where(t => t.Name.Contains(name));
        }

        public IEnumerable<Contact> FindByEmail(string email)
        {
            return _context.Contact.Where(t => t.Email == email);
        }

        public IEnumerable<Contact> FindByPhone(int phoneNumber)
        {
            return _context.Contact.Where(t => t.PhoneNumber == phoneNumber);
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
