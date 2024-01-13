using FluentValidation;

namespace Course.Services.Basket.Dtos
{
    public class BasketItemDto
    {
        public string CourseId { get; set; }
        public string CourseName  { get; set; }
        public decimal Price { get; set; }
        
    }
    public class BasketItemDtoValidator : AbstractValidator<BasketItemDto>
    {
        public BasketItemDtoValidator()
        {
            RuleFor(x=>x.CourseId).NotEmpty();
            RuleFor(x=>x.CourseName).NotEmpty();
            RuleFor(x=>x.Price).NotEmpty();
        }
    }
}
