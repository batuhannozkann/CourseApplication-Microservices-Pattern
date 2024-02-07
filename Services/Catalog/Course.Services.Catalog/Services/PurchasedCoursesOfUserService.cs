using AutoMapper;
using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Dtos.PurchasedCoursesOfUser;
using Course.Services.Catalog.Models;
using Course.Services.Catalog.Settings;
using Course.SharedLibrary.Dtos;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Course.Services.Catalog.Services
{
    public class PurchasedCoursesOfUserService:IPurchasedCoursesOfUserService
    {
        private IMongoCollection<PurchasedCoursesOfUser> _purchasedCoursesOfUserCollection;
        private IMongoCollection<Category> _categoryCollection;
        private IMongoCollection<Models.Course> _courseCollection;

        private IMapper _mapper;

        public PurchasedCoursesOfUserService(IMapper mapper , IOptions<DatabaseSetting> databaseSetting)
        {
            var client = new MongoClient(databaseSetting.Value.ConnectionString);
            var database = client.GetDatabase(databaseSetting.Value.DatabaseName);
            _purchasedCoursesOfUserCollection = database.GetCollection<PurchasedCoursesOfUser>(databaseSetting.Value.PurchasedCoursesOfUserCollectionName);
            _courseCollection=database.GetCollection<Models.Course>(databaseSetting.Value.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSetting.Value.CategoryCollectionName);
            _mapper = mapper;


        }

        public async Task<ResponseDto<PurchasedCoursesOfUserDto>> CreateAsync(CreatePurchasedCoursesOfUserDto createPurchasedCoursesOfUserDto)
        {
            PurchasedCoursesOfUser purchasedCoursesOfUser = _mapper.Map<PurchasedCoursesOfUser>(createPurchasedCoursesOfUserDto);
            if (await _courseCollection.FindAsync(x => x.Id == createPurchasedCoursesOfUserDto.CourseId) == null) return ResponseDto<PurchasedCoursesOfUserDto>.Fail($"Course with Id {createPurchasedCoursesOfUserDto.CourseId} not found", 404);
            purchasedCoursesOfUser.Course = _courseCollection.Find(x=>x.Id==createPurchasedCoursesOfUserDto.CourseId).FirstOrDefault();
            purchasedCoursesOfUser.Course.Category = _categoryCollection.Find(x => x.Id == purchasedCoursesOfUser.Course.CategoryId).FirstOrDefault();
            await _purchasedCoursesOfUserCollection.InsertOneAsync(purchasedCoursesOfUser);
            PurchasedCoursesOfUserDto createdPurchasedCoursesOfUser = _mapper.Map<PurchasedCoursesOfUserDto>(purchasedCoursesOfUser);
            return ResponseDto<PurchasedCoursesOfUserDto>.Success(createdPurchasedCoursesOfUser, 200);
        }
        public async Task<ResponseDto<List<PurchasedCoursesOfUserDto>>> CreateRangeAsync(ICollection<CreatePurchasedCoursesOfUserDto> createPurchasedCoursesOfUserDtos)
        {
            List<PurchasedCoursesOfUser> purchasedCoursesOfUser = _mapper.Map<List<PurchasedCoursesOfUser>>(createPurchasedCoursesOfUserDtos);
            foreach(var dto in purchasedCoursesOfUser)
            {
                if (await _courseCollection.FindAsync(x => x.Id == dto.CourseId) == null) return ResponseDto<List<PurchasedCoursesOfUserDto>>.Fail($"Course with Id {dto.CourseId} not found", 404);
                dto.Course = _courseCollection.Find(x => x.Id == dto.CourseId).FirstOrDefault();
                dto.Course.Category = _categoryCollection.Find(x => x.Id == dto.Course.CategoryId).FirstOrDefault();
            }
            await _purchasedCoursesOfUserCollection.InsertManyAsync(purchasedCoursesOfUser);
            List<PurchasedCoursesOfUserDto> purchasedCoursesOfUserDtos = _mapper.Map<List<PurchasedCoursesOfUserDto>>(purchasedCoursesOfUser);
            if (purchasedCoursesOfUserDtos.Count > 0) return ResponseDto<List<PurchasedCoursesOfUserDto>>.Success(purchasedCoursesOfUserDtos, 200);
            return ResponseDto<List<PurchasedCoursesOfUserDto>>.Fail("Courses have not added",400);
        }
        public Task<ResponseDto<bool>> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto<List<PurchasedCoursesOfUserDto>>> GetAllAsync()
        {
            ICollection<Models.PurchasedCoursesOfUser> purschasedCoursesOfUser = _purchasedCoursesOfUserCollection.Find(x=>true).ToList();
            if (purschasedCoursesOfUser.Count > 0)
            {
                var purchasedCoursesOfUserDtos = _mapper.Map<List<PurchasedCoursesOfUserDto>>(purschasedCoursesOfUser);
                return ResponseDto<List<PurchasedCoursesOfUserDto>>.Success(purchasedCoursesOfUserDtos, 200);
            }
            return ResponseDto<List<PurchasedCoursesOfUserDto>>.Fail("Purchased course is not found", 400);
        }

        public Task<ResponseDto<PurchasedCoursesOfUserDto>> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public ResponseDto<List<PurchasedCoursesOfUserDto>> GetByUserId(string userId)
        {
            List<PurchasedCoursesOfUserDto> purchasedCoursesOfUserDtos = _mapper.Map<List<PurchasedCoursesOfUserDto>>(_purchasedCoursesOfUserCollection.Find(x=>x.UserId == userId).ToList());
            if(purchasedCoursesOfUserDtos.Count > 0)
            {
                foreach(var p in purchasedCoursesOfUserDtos)
                {
                    p.Course=_mapper.Map<CourseDto>(_courseCollection.Find(x=>x.Id == p.CourseId).FirstOrDefault());
                    p.Course.Category = _mapper.Map<CategoryDto>(_categoryCollection.Find(x=>x.Id==p.Course.CategoryId).FirstOrDefault());
                }
                return ResponseDto<List<PurchasedCoursesOfUserDto>>.Success(purchasedCoursesOfUserDtos, 200);
            }
            return ResponseDto<List<PurchasedCoursesOfUserDto>>.Fail("User doesn't have any course",400);
        }

        public Task<ResponseDto<PurchasedCoursesOfUserDto>> UpdateAsync(CreatePurchasedCoursesOfUserDto createPurchasedCoursesOfUserDto)
        {
            throw new NotImplementedException();
        }
    }
}
