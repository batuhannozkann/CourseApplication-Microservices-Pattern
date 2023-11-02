using System;
using System.Collections.Generic;
using System.Linq;
using Course.IdentityServer.Dtos;
using Course.IdentityServer.Models;
using Course.SharedLibrary.Dtos;
using System.Threading.Tasks;
using Course.IdentityServer.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace Course.IdentityServer.Services.Concrete
{
    public class UserService:IUserService
    {
        private UserManager<ApplicationUser> _userManager;
        private IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ResponseDto<ApplicationUser>> RegisterUser(UserRegisterDto userRegisterDto)
        {
            var user = new ApplicationUser()
            {
                City = userRegisterDto.City,
                Email = userRegisterDto.Email,
                UserName = userRegisterDto.Email,
                FullName = userRegisterDto.FullName
            };
            var result = await _userManager.CreateAsync(user, userRegisterDto.Password);
            if (!result.Succeeded)
            {
                return ResponseDto<ApplicationUser>.Fail(new List<string>(result.Errors.Select(x=>x.Description).ToList()),400);
            }

            var createdUser = await _userManager.FindByEmailAsync(userRegisterDto.Email);
            return ResponseDto<ApplicationUser>.Success(createdUser,200);
        }

        public ResponseDto<List<ApplicationUser>> GetAllUser()
        {
            List<ApplicationUser> users = _userManager.Users.ToList();
            return ResponseDto<List<ApplicationUser>>.Success(users,200);
        }
        public async Task<ResponseDto<UserRegisterDto>> GetUser()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null) return ResponseDto<UserRegisterDto>.Fail("User is not found",400);
            var user = await _userManager.FindByIdAsync(userIdClaim.Value);
            if (user == null) return ResponseDto<UserRegisterDto>.Fail("User is not found", 400);
            return ResponseDto<UserRegisterDto>.Success(new UserRegisterDto(){Email = user.Email,FullName = user.FullName},200);

        }
        public async Task<ResponseDto<string>> GenerateEmailConfirmationTokenAsync(string email)
        {
            ApplicationUser appUser = await _userManager.FindByEmailAsync(email);
            if (appUser == null) return  ResponseDto<string>.Fail("User is not found", 400);
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            MailMessage mailMessage = new MailMessage();
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(appUser.Email);
            mailMessage.From = new MailAddress("ekutuphanemvc@gmail.com", "Email Validation");
            mailMessage.Subject = "Email Validation";
            mailMessage.Body = $"<a target=\"_blank\" href=\"http://localhost:5001/api/User/EmailConfirmationByToken?token={Uri.EscapeDataString(token)}&email={appUser.Email}\">Emailinizi onaylamak için tıklayınız.</a>";
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential("ekutuphanemvc@gmail.com", "wakqorhijngdxddx");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Send(mailMessage);
            return ResponseDto<string>.Success(HttpUtility.UrlEncode(token), 200);
            
        }
        public async Task<ResponseDto<string>> EmailConfirmationByTokenAsync(string email,string token)
        {
            ApplicationUser appUser = await _userManager.FindByEmailAsync(email);
            if (appUser == null) return ResponseDto<string>.Fail("User has not found", 400);
            if (appUser.EmailConfirmed == true) return ResponseDto<string>.Fail("Email has already confirmed", 400);
            var result = await _userManager.ConfirmEmailAsync(appUser, Uri.UnescapeDataString(token));
            if (result.Succeeded)
            {
                return ResponseDto<string>.Success("Email has confirmed", 200);
            }
            return ResponseDto<string>.Fail(result.Errors.FirstOrDefault().Description + result.Errors.FirstOrDefault().Code, 400);

        }
        public async Task<ResponseDto<string>> GeneratePasswordResetTokenAsync(string email)
        {
            ApplicationUser appUser = await _userManager.FindByEmailAsync(email);
            if (appUser == null) return ResponseDto<string>.Fail("User is not found", 400);
            string token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
            MailMessage mailMessage = new MailMessage();
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(appUser.Email);
            mailMessage.From = new MailAddress("ekutuphanemvc@gmail.com", "Email Validation");
            mailMessage.Subject = "Reset Password";
            mailMessage.Body = $"<a target=\"_blank\" href=\"http://localhost:5173/ResetPassword?token={Uri.EscapeDataString(token)}&email={appUser.Email}\">Şifrenizi sıfırlamak için tıklayınız.</a>";
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential("ekutuphanemvc@gmail.com", "wakqorhijngdxddx");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Send(mailMessage);
            return ResponseDto<string>.Success(HttpUtility.UrlEncode(token), 200);

        }

        public async Task<ResponseDto<bool>> ResetPasswordAsync(string token,string email,string password)
        {
            ApplicationUser applicationUser =await _userManager.FindByEmailAsync(email);
            var result = await _userManager.ResetPasswordAsync(applicationUser, token, password);
            await _userManager.UpdateSecurityStampAsync(applicationUser);
            if (!result.Succeeded)
            {
                return ResponseDto<bool>.Fail(result.Errors.SingleOrDefault().Description,400);
            }
            return ResponseDto<bool>.Success(true,200);
        }

    }
}
