using CRUD_Business.Interfaces;
using CRUD_Contracts.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;

namespace CRUD_Islemler.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        // Service DI ile alınır
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            // Business Create çağrısı
            var createdUserId = await _userService.CreateAsync(dto);

            // HTTP 201 Created
            return CreatedAtAction(
                nameof(Create),
                new { id = createdUserId },
                new { Id = createdUserId }
            );
            //1.parametre → URL / route için
            //2.parametre → JSON response body için

        }

        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }


        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto dto)
        {
            await _userService.UpdateAsync(id, dto);
            return Ok(new { message = "Kullanıcı başarıyla güncellendi." });
        }
        [HttpPut("SoftDeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.SoftDeleteAsync(id);
            return Ok(new { message = "Kullanıcı başarıyla silindi." });
        }

        [HttpPut("ActiveUser/{id}")]
        public async Task<IActionResult> ActivateUser(int id)
        {
            await _userService.ActivateAsync(id);
            return Ok(new { message = "Kullanıcı başarıyla aktif edildi." });
        }

        [HttpDelete("HardDeleteUser/{id}")]
        public async Task<IActionResult> HardDeleteUser(int id)
        {
            await _userService.HardDeleteAsync(id);
            return Ok(new { message = "Kullanıcı kalıcı olarak silindi." });
        }

    }
}
