using FluentValidation;

namespace Course.Services.Catalog.Dtos;

public class CategoryDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    
}
public class CategoryDtoValidator : AbstractValidator<CategoryDto>
{
    public CategoryDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Id).NotEmpty();
    }
}