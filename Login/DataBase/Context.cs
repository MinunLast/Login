using Login.Models;
using Microsoft.EntityFrameworkCore;

namespace Login.DataBase
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        { }
        public DbSet<User> Users { get; set; }
    }
}
