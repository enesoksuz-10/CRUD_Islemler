using CRUD_DataAccess.Context;
using CRUD_DataAccess.Repositories.Implementations;
using CRUD_Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_DataAccess.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly CrudDbContext _context; //Veri tabanı bağlantısını temsil eden DbContext tutar. Repository'nin DB ile haberleşmesini sağlar. Private olma sebebi dışarıdan müdahale edilmemesi gerektiği içindir. (Encapsulation). Readonly Constructor’da bir kere atanır. Sonradan değiştirilmesi engellenir.

        public UserRepository(CrudDbContext context) //Constructor (Dependency Injection)
        {
            _context = context;
        }

        public async Task AddAsync(User user) //Metodun içinde asenkron işlemler var. await kullanabilmek için şart.
        {
            await _context.Users.AddAsync(user); //await olmasaydı işlemin bitip bitmediği anlaşılmazdı.
            await _context.SaveChangesAsync();
        }
    }
}
//API’den gelen User Business → Repository’ye gönderir Repository:EF Core ile Users tablosuna ekler Değişiklikleri DB’ye yazar İşlem async tamamlanır.
