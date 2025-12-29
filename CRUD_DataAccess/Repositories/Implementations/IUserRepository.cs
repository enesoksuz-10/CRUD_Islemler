using CRUD_Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_DataAccess.Repositories.Implementations
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
    }
}
// Task kullanmamızın sebebi:
// Veritabanı işlemleri I/O-bound olduğu için async çalıştırılır.
// Böylece thread bloklanmaz, uygulama daha performanslı ve ölçeklenebilir olur.
//
// Interface seviyesinde Task kullanılır çünkü async yapı
// Controller -> Service -> Repository zinciri boyunca korunmalıdır.
//
// Bunun yerine ne kullanılabilirdi?
// - void / T dönüşlü senkron metotlar (ÖNERİLMEZ, thread bloklar)
// - ValueTask (çok spesifik performans senaryolarında)
// - Fire-and-forget (kritik işlemler için uygun değil)
