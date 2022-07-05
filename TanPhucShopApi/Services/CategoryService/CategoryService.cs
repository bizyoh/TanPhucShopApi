﻿using AutoMapper;
using TanPhucShopApi.Data;
using TanPhucShopApi.Models;
using TanPhucShopApi.Models.DTO.Category;

namespace TanPhucShopApi.Services.CategoryService
{
    public class InvoiceService : IInvoiceService
    {
        private AppDBContext db;
        private IMapper mapper;
        public InvoiceService(AppDBContext _db,IMapper _mapper)
        {
            db=_db;
            mapper = _mapper;
        }

        public CreatedCategoryDto Create(CreateCategoryDto createCategoryDto)
        {
            var category = mapper.Map<Category>(createCategoryDto);
            db.Categories.Add(category);
            if(db.SaveChanges() > 0)
            {
                var createdCategoryDto = mapper.Map<CreatedCategoryDto>(category);
                return createdCategoryDto;
            }
            return null;
        }

        public bool Delete(int id)
        {
            var category = db.Categories.FirstOrDefault(c => c.Id == id);
            if(category != null)
            {
                db.Categories.Remove(category);
                return db.SaveChanges() > 0;
            }
            return false;
        }

        public List<GetAllCategoryDto> GetAllCategoryByStatus(bool status)
        {
            var categories = db.Categories.Where(x => x.Status == status).Select(x=> new GetAllCategoryDto
            {
                Name = x.Name,
                Id = x.Id,
                Status = x.Status
            }).ToList();
            return categories;
        }

        public List<GetAllCategoryDto> GetAllCategory()
        {
            var categories = db.Categories.Select(x => new GetAllCategoryDto
            {
                Name = x.Name,
                Id = x.Id,
                Status = x.Status
            }).ToList();
            return categories;
        }

        public Category GetById(int id)
        {
            return db.Categories.Find(id);
        }

        public bool Update(int id, UpdateCategoryDto updateCategoryDto)
        {
            var category = db.Categories.Find(id);
            if(category != null)
            {
                mapper.Map(updateCategoryDto,category);
                db.Update(category);
                return db.SaveChanges() > 0;
            }
            return false;
        }
    }
}
