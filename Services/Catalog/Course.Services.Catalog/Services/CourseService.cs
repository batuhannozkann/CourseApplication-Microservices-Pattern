using AutoMapper;
using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Dtos.Common;
using Course.Services.Catalog.Models;
using Course.Services.Catalog.Settings;
using Course.SharedLibrary.Dtos;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Course.Services.Catalog.Services;

public class CourseService : ICourseService
{
    private IMongoCollection<Models.Course> _courseCollection;
    private IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;
    private readonly IMongoCollection<PurchasedCoursesOfUser> _purchasedCourseCollection;

    public CourseService(IMapper mapper, IOptions<DatabaseSetting> databaseSetting)
    {
        _mapper = mapper;
        var client = new MongoClient(databaseSetting.Value.ConnectionString);
        var database = client.GetDatabase(databaseSetting.Value.DatabaseName);
        _courseCollection = database.GetCollection<Models.Course>(databaseSetting.Value.CourseCollectionName);
        _categoryCollection = database.GetCollection<Category>(databaseSetting.Value.CategoryCollectionName);
        _purchasedCourseCollection = database.GetCollection<PurchasedCoursesOfUser>(databaseSetting.Value.PurchasedCoursesOfUserCollectionName);
    }

    public async Task<ResponseDto<List<CourseDto>>> GetAllAsync()
    {
        List<Models.Course> courses = await _courseCollection.Find(x => true).ToListAsync();
        if (courses.Any())
        {
            foreach (Models.Course course in courses)
            {
                course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstOrDefaultAsync();
            }
        }
        else
        {
            courses = new List<Models.Course>() { };
        }
        return ResponseDto<List<CourseDto>>.Success(data: _mapper.Map<List<Models.Course>, List<CourseDto>>(courses),
            statusCode: 200);
    }

    public async Task<ResponseDto<CourseDto>> CreateAsync(CourseCreateDto createDto)
    {
        Models.Course course = _mapper.Map<Models.Course>(createDto);
        await _courseCollection.InsertOneAsync(course);
        course.Category = await _categoryCollection.Find(x => x.Id == createDto.CategoryId).FirstOrDefaultAsync();
        return ResponseDto<CourseDto>.Success(data: _mapper.Map<CourseDto>(course), statusCode: 200);
    }

    public async Task<ResponseDto<CourseDto>> GetByIdAsync(string id, string? userId)
    {
        Models.Course course = await _courseCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        if (course == null) return ResponseDto<CourseDto>.Fail("Course has not found", 400);
        course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstOrDefaultAsync();
        CourseDto courseDto = _mapper.Map<CourseDto>(course);
        if (userId != null) {
            if (_purchasedCourseCollection.Find(x => x.UserId == userId && x.CourseId == id).FirstOrDefault() != null) courseDto.userOwned = true;
        }
        return ResponseDto<CourseDto>.Success(data: courseDto, statusCode: 200);
    }

    public async Task<ResponseDto<List<CourseDto>>> GetByUserId(string userId)
    {
        List<Models.Course> courses = await _courseCollection.Find(x => x.UserId == userId).ToListAsync();
        if (courses.Any())
        {
            foreach (Models.Course course in courses)
            {
                course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstOrDefaultAsync();
            }
        }
        else
        {
            courses = new List<Models.Course>() { };
        }

        return ResponseDto<List<CourseDto>>.Success(data: _mapper.Map<List<Models.Course>, List<CourseDto>>(courses),
            statusCode: 200);
    }

    public async Task<ResponseDto<CourseDto>> UpdateAsync(CourseUpdateDto updateDto)
    {
        Models.Course course = _mapper.Map<Models.Course>(updateDto);
        Models.Course updatedCourse = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == course.Id, course);
        if (updatedCourse == null)
        {
            return ResponseDto<CourseDto>.Fail("Course not found", 400);
        }

        return ResponseDto<CourseDto>.Success(data: _mapper.Map<CourseDto>(updatedCourse), statusCode: 200);
    }

    public async Task<ResponseDto<CourseDto>> RemoveAsync(string id)
    {
        CourseDto courseDto = _mapper.Map<CourseDto>(await _courseCollection.Find(x => x.Id == id).FirstOrDefaultAsync());
        var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);
        if (result.DeletedCount > 0) return ResponseDto<CourseDto>.Success(data: courseDto, statusCode: 200);
        return ResponseDto<CourseDto>.Fail("Course did not remove", 400);

    }
    public async Task<ResponseDto<List<CourseDto>>> GetAllCourseAccordingToUser(string userId)
    {
        List<PurchasedCoursesOfUser> purchasedCoursesOfUsers = _purchasedCourseCollection.Find(x => x.UserId == userId).ToList();
        List<Models.Course> courses = _courseCollection.Find(x => true).ToList();
        if (!courses.Any()) return ResponseDto<List<CourseDto>>.Fail("There is no course on system", 400);
        foreach (Models.Course course in courses)
        {
            course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstOrDefaultAsync();
        }
        var coursesDto = _mapper.Map<List<CourseDto>>(courses);
        if (purchasedCoursesOfUsers.Count == 0) return ResponseDto<List<CourseDto>>.Success(coursesDto, statusCode: 200);
        foreach (var course in purchasedCoursesOfUsers)
        {
            var ownedCourse = coursesDto.Where(x => x.Id == course.CourseId).FirstOrDefault();
            ownedCourse.userOwned = true;
        }
        return ResponseDto<List<CourseDto>>.Success(coursesDto, statusCode: 200);
    }
    public async Task<ResponseDto<List<CourseDto>>> GetFilteredCourses(FilterParameters filterParameters)
    {

        try
        {
            var filterBuilder = Builders<Models.Course>.Filter;
            var filter = filterBuilder.Empty;

            if (filterParameters?.CategoryIds?.Count > 0)
            {
                filter &= filterBuilder.In(x => x.CategoryId, filterParameters.CategoryIds);
            }

            if (filterParameters?.MinPrice > 0)
            {
                filter &= filterBuilder.Gte(x => x.Price, filterParameters.MinPrice);
            }

            if (filterParameters?.MaxPrice > 0)
            {
                filter &= filterBuilder.Lte(x => x.Price, filterParameters.MaxPrice);
            }


            var filteredCourses = await _courseCollection.Find(filter).ToListAsync();


            var responseDto = ResponseDto<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(filteredCourses), 200);
            return responseDto;
        }
        catch (Exception ex)
        {

            var errorMessage = "An error occurred while filtering courses: " + ex.Message;
            return ResponseDto<List<CourseDto>>.Fail(errorMessage, 500); 
    }

    }