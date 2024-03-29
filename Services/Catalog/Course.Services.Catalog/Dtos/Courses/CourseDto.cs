﻿namespace Course.Services.Catalog.Dtos;

public class CourseDto
{
    public string Id { get; set; }

    public string Name { get; set; }
   
    public decimal Price { get; set; }

    public string Picture { get; set; }
    public string? CourseContent { get; set; }
    public string? Requirement { get; set; }
    public bool? userOwned { get; set; }

    public string Description { get; set; }
 
    public string CategoryId { get; set; }

    public string UserId { get; set; }
    
    public CategoryDto Category { get; set; }
    public FeatureDto Feature { get; set; }

}