using FluentValidation;

namespace Course.IdentityServer.Dtos
{
    public class UserRegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email  { get; set; }
        public string Password { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }

    public class UserValidator : AbstractValidator<UserRegisterDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(3);
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MaximumLength(20).MinimumLength(7);
        }
    }
}
