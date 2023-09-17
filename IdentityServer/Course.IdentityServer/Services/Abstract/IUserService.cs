using System.Collections.Generic;
using System.Threading.Tasks;
using Course.IdentityServer.Dtos;
using Course.IdentityServer.Models;
using Course.SharedLibrary.Dtos;

namespace Course.IdentityServer.Services.Abstract
{
    public interface IUserService
    {
        public Task<ResponseDto<ApplicationUser>> RegisterUser(UserRegisterDto userRegisterDto);
        public ResponseDto<List<ApplicationUser>> GetAllUser();

    }
}
