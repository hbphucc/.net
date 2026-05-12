using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FashionShopAPI.Models.DTOs;
using FashionShopAPI.Repositories.Interfaces;

namespace FashionShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userRepo.GetByIdAsync(userId!);

            if (user == null) return NotFound();

            return Ok(new
            {
                user.UserId,
                user.FullName,
                user.Email,
                user.Phone,
                user.Address,
                user.Role
            });
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userRepo.GetByIdAsync(userId!);
            if (user == null) return NotFound();

            user.FullName = request.FullName;
            user.Phone = request.Phone;
            user.Address = request.Address;

            await _userRepo.UpdateProfileAsync(user);
            return Ok(new { message = "Profile updated successfully!" });
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var success = await _userRepo.ChangePasswordAsync(userId!, request.OldPassword, request.NewPassword);
            if (!success) return BadRequest(new { message = "Old password is incorrect!" });

            return Ok(new { message = "Password changed successfully!" });
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepo.GetAllUsersAsync();

            return Ok(users);
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateUserStatus(string id, bool status)
        {
            var result = await _userRepo.UpdateUserStatusAsync(id, status);

            if (!result)
                return NotFound();

            return Ok(new { message = "User status updated successfully!" });
        }
    }
}