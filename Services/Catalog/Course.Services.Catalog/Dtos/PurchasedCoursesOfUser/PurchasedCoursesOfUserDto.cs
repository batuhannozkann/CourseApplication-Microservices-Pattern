using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Course.Services.Catalog.Dtos.PurchasedCoursesOfUser
{
    public class PurchasedCoursesOfUserDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CourseId { get; set; }
        public CourseDto Course { get; set; }
    }
}
