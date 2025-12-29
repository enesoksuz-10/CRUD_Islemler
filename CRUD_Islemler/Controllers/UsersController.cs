using CRUD_Business.Interfaces;
using CRUD_Contracts.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;

namespace CRUD_Islemler.Controllers
{
    [ApiController]
    [Route("api/CreatUsers")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        // Service DI ile alınır
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
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
    }
}
