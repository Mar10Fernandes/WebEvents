
using Microsoft.EntityFrameworkCore;
using ClassLibrary;

namespace WebApi.Model
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options): base(options)
        {
        }

        public DbSet<Contact> Contact { get; set; }
    }
}
