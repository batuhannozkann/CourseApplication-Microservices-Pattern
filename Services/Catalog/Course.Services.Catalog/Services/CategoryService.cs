﻿using System.Globalization;
using AutoMapper;
using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Models;
using Course.Services.Catalog.Settings;
using Course.SharedLibrary.Dtos;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Course.Services.Catalog.Services;

public class CategoryService:ICategoryService
{
    
    private IMapper _mapper;
    private IMongoCollection<Category> _categoryCollection;

    public CategoryService(IMapper mapper,IOptions<DatabaseSetting> databaseSetting)
    {
        _mapper = mapper;
        var client = new MongoClient(databaseSetting.Value.ConnectionString);
        var database = client.GetDatabase(databaseSetting.Value.DatabaseName);
        _categoryCollection = database.GetCollection<Category>(databaseSetting.Value.CategoryCollectionName);
    }

    public async Task<ResponseDto<List<CategoryDto>>> GetAllAsync()
    {
        List<Category> categories = await _categoryCollection.Find(category => true).ToListAsync();
        return ResponseDto<List<CategoryDto>>.Success(data:_mapper.Map<List<Category>,List<CategoryDto>>(categories),statusCode:200);
    }
    
    public async Task<ResponseDto<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto)
    {
        Category category = _mapper.Map<Category>(categoryCreateDto);
        await _categoryCollection.InsertOneAsync(category);
        return ResponseDto<CategoryDto>.Success(data: _mapper.Map<CategoryDto>(category), statusCode: 200);
    }
    public async Task<ResponseDto<bool>> DeleteAsync(string id)
    {
        var filter = Builders<Category>.Filter.Eq(x => x.Id,id);
        var deleteResult = await _categoryCollection.DeleteOneAsync(filter);

        if (deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0)
        {
            // Güncelleme işlemi başarılı oldu.
            return ResponseDto<bool>.Success(true,200);
        }
        else
        {
            // Güncelleme işlemi başarısız oldu veya hiçbir belge güncellenmedi.
            return ResponseDto<bool>.Fail("Category has not deleted", 400);
        }
    }
    public async Task<ResponseDto<CategoryDto>> UpdateAsync(CategoryDto categoryDto)
    {
        Category category = _mapper.Map<Category>(categoryDto);
        var updatedCategory = await _categoryCollection.FindOneAndReplaceAsync(x=>x.Id==categoryDto.Id, category);

        if (updatedCategory != null)
        {
            // Güncelleme işlemi başarılı oldu.
            return ResponseDto<CategoryDto>.Success(_mapper.Map<CategoryDto>(updatedCategory),200);
        }
        else
        {
            // İstemci tarafından hatalı bir istek olduğu için 400 Bad Request hatası döndürülebilir.
            return ResponseDto<CategoryDto>.Fail("Category has not updated", 400);
        }
    }
    public async Task<ResponseDto<CategoryDto>> GetByIdAsync(string id)
    {
        if (id.Length != 24) return ResponseDto<CategoryDto>.Fail("Id should be 24 digit", 400);
        Category category = await _categoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        if (category == null)
        {
            return ResponseDto<CategoryDto>.Fail(error:"Category has not found", statusCode:400);
        }
        return ResponseDto<CategoryDto>.Success(data: _mapper.Map<CategoryDto>(category), statusCode: 200);
    }
}   
