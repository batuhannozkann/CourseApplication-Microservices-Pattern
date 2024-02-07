using FluentValidation;
using System.Reflection.Metadata;

namespace Course.Services.Basket.Dtos
{
    public class BasketDto
    {
        public string? UserId { get; set; }
        public string? DiscountCode { get; set; }
        public List<BasketItemDto> BasketItems { get; set; } = new List<BasketItemDto>();
        public decimal TotalPrice
        {
            get => BasketItems.Sum(x => x.Price);
        }
    }
    public class BasketDtoValidator : AbstractValidator<BasketDto>
    {
        public BasketDtoValidator()
        {
            
        }
    }
}
