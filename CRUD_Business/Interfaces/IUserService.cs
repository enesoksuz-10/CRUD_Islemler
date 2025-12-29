using CRUD_Contracts.Users;
using CRUD_Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Business.Interfaces
{
    public interface IUserService
    {
        Task<int> CreateAsync(CreateUserDto dto);
        Task<List<ReadUserDto>> GetAllAsync();
        Task<ReadUserDto> GetByIdAsync(int id);
        Task UpdateAsync(int id, UpdateUserDto dto);
        Task SoftDeleteAsync(int id);
        Task ActivateAsync(int id);
        Task HardDeleteAsync(int id);

    }
}
