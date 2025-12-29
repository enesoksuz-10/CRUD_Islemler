using CRUD_DataAccess.Context;
using CRUD_DataAccess.Repositories.Interfaces;
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

        // Aktif kullanıcıları getirir
        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users
                .Where(x => x.RecordStatus == 10)
                .ToListAsync();
        }

        // Id’ye göre tek kullanıcı
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id && x.RecordStatus == 10);
        }


        // Update için kullanıcıyı getirir (aktif mi kontrol edilir)
        public async Task<User?> GetByIdForUpdateAsync(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id && x.RecordStatus == 10);
        }

        // Güncelleme işlemi
        public async Task UpdateAsync(User user)
        { 
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        // Soft delete için aktif kullanıcıyı getirir
        public async Task<User?> GetByIdForDeleteAsync(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id && x.RecordStatus == 10);
        }

        // Fiziksel silme yok, sadece update
        public async Task SoftDeleteAsync(User user)
        {
            user.RecordStatus = 40;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        // Aktif etmek için pasif kullanıcıyı getirir
        public async Task<User?> GetByIdForActivateAsync(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id && x.RecordStatus == 40);
        }

        // Kullanıcıyı aktif et
        public async Task ActivateAsync(User user)
        {
            user.RecordStatus = 10;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        // Hard delete edilecek kullanıcıyı getirir (sadece silinmiş)
        public async Task<User?> GetByIdForHardDeleteAsync(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id && x.RecordStatus == 40);
        }

        // Fiziksel silme
        public async Task HardDeleteAsync(User user)
        {
            _context.Users.Remove(user); // DELETE FROM Users
            await _context.SaveChangesAsync();
        }

    }
}
//API’den gelen User Business → Repository’ye gönderir Repository:EF Core ile Users tablosuna ekler Değişiklikleri DB’ye yazar İşlem async tamamlanır.
