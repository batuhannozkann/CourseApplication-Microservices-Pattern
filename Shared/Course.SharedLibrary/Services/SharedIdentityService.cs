using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course.SharedLibrary.Services.Abstract;
using Microsoft.AspNetCore.Http;

namespace Course.SharedLibrary.Services
{
    public class SharedIdentityService:ISharedIdentityService
    {
        private IHttpContextAccessor _contextAccessor;

        public SharedIdentityService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public string GetUserId
        {
            get
            {
                return _contextAccessor.HttpContext.User.FindFirst( "sub").Value;
            }
        }
    }
}
