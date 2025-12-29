using CRUD_Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_DataAccess.Context
{
    public class CrudDbContext : DbContext
    {
        public CrudDbContext(DbContextOptions<CrudDbContext> options)
         : base(options)
        {
        }
        public DbSet<User> Users { get; set; } = null!;
    }
}
