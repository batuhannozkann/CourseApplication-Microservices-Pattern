using FluentValidation;

namespace Course.IdentityServer.Dtos
{
    public class UserRegisterDto
    {
        public string FullName { get; set; }
        public string Email  { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
    }

    public class UserValidator : AbstractValidator<UserRegisterDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MaximumLength(20).MinimumLength(7);
        }
    }
}
