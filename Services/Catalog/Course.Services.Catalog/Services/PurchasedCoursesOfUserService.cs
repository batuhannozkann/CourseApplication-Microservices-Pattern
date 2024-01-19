using AutoMapper;
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
        private IMongoCollection<Models.Course> _courseCollection;
        private IMapper _mapper;

        public PurchasedCoursesOfUserService(IMapper mapper , IOptions<DatabaseSetting> databaseSetting)
        {
            var client = new MongoClient(databaseSetting.Value.ConnectionString);
            var database = client.GetDatabase(databaseSetting.Value.DatabaseName);
            _purchasedCoursesOfUserCollection = database.GetCollection<PurchasedCoursesOfUser>(databaseSetting.Value.PurchasedCoursesOfUserCollectionName);
            _courseCollection=database.GetCollection<Models.Course>(databaseSetting.Value.CourseCollectionName);
            _mapper = mapper;


        }

        public async Task<ResponseDto<PurchasedCoursesOfUserDto>> CreateAsync(CreatePurchasedCoursesOfUserDto createPurchasedCoursesOfUserDto)
        {
            PurchasedCoursesOfUser purchasedCoursesOfUser = _mapper.Map<PurchasedCoursesOfUser>(createPurchasedCoursesOfUserDto);
            if (await _courseCollection.FindAsync(x => x.Id == createPurchasedCoursesOfUserDto.CourseId) == null) ResponseDto<PurchasedCoursesOfUserDto>.Fail($"Course with Id {createPurchasedCoursesOfUserDto.CourseId} not found", 404);
            purchasedCoursesOfUser.Course = _mapper.Map<Models.Course>(await _courseCollection.FindAsync(x=>x.Id==createPurchasedCoursesOfUserDto.CourseId));
            await _purchasedCoursesOfUserCollection.InsertOneAsync(purchasedCoursesOfUser);
            PurchasedCoursesOfUserDto createdPurchasedCoursesOfUser = _mapper.Map<PurchasedCoursesOfUserDto>(purchasedCoursesOfUser);
            return ResponseDto<PurchasedCoursesOfUserDto>.Success(createdPurchasedCoursesOfUser, 200);
        }

        public Task<ResponseDto<bool>> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<List<PurchasedCoursesOfUserDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
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
