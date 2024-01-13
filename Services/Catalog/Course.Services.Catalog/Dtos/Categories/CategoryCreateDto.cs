using FluentValidation;

namespace Course.Services.Catalog.Dtos;

public class CategoryCreateDto
{
    public string Name { get; set; }
}
public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}