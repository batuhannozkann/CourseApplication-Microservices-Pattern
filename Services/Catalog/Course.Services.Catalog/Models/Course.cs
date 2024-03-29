﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Course.Services.Catalog.Models;

public class Course
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Name { get; set; }
    
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }

    public string Picture { get; set; }
    public string? CourseContent { get; set; }
    public string? Requirement { get; set; }

    public string Description { get; set; }
    
    [BsonRepresentation(BsonType.ObjectId)]
    public string CategoryId { get; set; }
    public string UserId { get; set; }
    public string UserFullName { get; set; }
    public DateTime CreatedDate => DateTime.Now;
    public Feature Feature { get; set; }
    
    [BsonIgnore]
    public Category Category { get; set; }
    
    
}