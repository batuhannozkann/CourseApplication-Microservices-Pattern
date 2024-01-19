using FluentValidation;

namespace Course.Services.Catalog.Dtos;

public class CourseCreateDto
{
    public string Name { get; set; }
    
    public decimal Price { get; set; }

    public string Picture { get; set; }
    
    public string Description { get; set; }
    public string CourseContent { get; set; }
    public string Requirement { get; set; }

    public string CategoryId { get; set; }

    public string? UserId { get; set; }
    
    public FeatureDto Feature { get; set; }
}
public class CourseCreateDtoValidator : AbstractValidator<CourseCreateDto>
{
    public CourseCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x=>x.Price).NotEmpty();
        RuleFor(x=>x.Description).NotEmpty();
        RuleFor(x=>x.CategoryId).NotEmpty();
    }
}