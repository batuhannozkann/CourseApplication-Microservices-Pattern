using System.Collections.Generic;
using System.Threading.Tasks;
using Course.IdentityServer.Dtos;
using Course.IdentityServer.Models;
using Course.SharedLibrary.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Course.IdentityServer.Services.Abstract
{
    public interface IUserService
    {
        public Task<ResponseDto<ApplicationUser>> RegisterUser(UserRegisterDto userRegisterDto);
        public ResponseDto<List<ApplicationUser>> GetAllUser();
        Task<ResponseDto<UserRegisterDto>> GetUser();
        Task<ResponseDto<string>> GenerateEmailConfirmationTokenAsync(string email);
        Task<ResponseDto<string>> EmailConfirmationByTokenAsync(string username, string token);
        Task<ResponseDto<bool>> ResetPasswordAsync(string token, string email, string password);
        Task<ResponseDto<string>> GeneratePasswordResetTokenAsync(string email);

    }
}
