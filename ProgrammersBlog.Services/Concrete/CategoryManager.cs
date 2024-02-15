using AutoMapper;
using ProgrammersBlog.Data.UnitOfWork;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Services.Utilities.StaticMessagess;
using ProgrammersBlog.Shared.Results;
using System;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Concrete
{
    public class CategoryManager :BaseManager, ICategoryService
    {
        public CategoryManager(IUow uow, IMapper mapper) : base(uow, mapper)
        {
        }



        public async Task<IDataResult<CategoryListDto>> GetAllByDeletedAsync()
        {
            var categories = await Uow.Categories.GetAllAsync(c => c.IsDeleted);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<CategoryDto>> UndoDeleteAsync(int categoryId, string modifiedByName)
        {
            var category = await Uow.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = false;
                category.IsActive = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                var deletedCategory = await Uow.Categories.UpdateAsync(category);
                await Uow.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Success, Messages.Category.UndoDelete(deletedCategory.Name), new CategoryDto
                {
                    Category = deletedCategory,
                    ResultStatus = ResultStatus.Success,
                    Message = Messages.Category.UndoDelete(deletedCategory.Name)
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: false), new CategoryDto
            {
                Category = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Category.NotFound(isPlural: false)
            });
        }
        public async Task<IDataResult<CategoryDto>> GetAsync(int categoryId)
        {
            var category = await Uow.Categories.GetAsync(c => c.Id == categoryId, c => c.Articles);
            if (category != null)
            {
                return new DataResult<CategoryDto>(ResultStatus.Success, new CategoryDto
                {
                    Category = category,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı.", null);
        }

        public async Task<IDataResult<CategoryListDto>> GetAllAsync()
        {
            var categories = await Uow.Categories.GetAllAsync(null, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hiç bir kategori bulunamadı.", null);
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAsync()
        {
            var categories = await Uow.Categories.GetAllAsync(c => !c.IsDeleted, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hiç bir kategori bulunamadı.", null);
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActiveAsync()
        {
            var categories = await Uow.Categories.GetAllAsync(c => !c.IsDeleted && c.IsActive, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hiç bir kategori bulunamadı.", null);
        }

        public async Task<IDataResult<CategoryDto>> AddAsync(CategoryAddDto categoryAddDto, string createdByName)
        {
            var category = Mapper.Map<Category>(categoryAddDto);
            category.CreatedByName = createdByName;
            category.ModifiedByName = createdByName;
            var addedCategory = await Uow.Categories.AddAsync(category);
            await Uow.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success,$"{categoryAddDto.Name} adlı kategori başarıyla eklenmiştir.", new() { Category = addedCategory,ResultStatus=ResultStatus.Success,Message = "Eklendi!"});
        }

        public async Task<IDataResult<CategoryUpdateDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var oldCategory = await Uow.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
            var category = Mapper.Map<CategoryUpdateDto, Category>(categoryUpdateDto, oldCategory);
            category.ModifiedByName = modifiedByName;
            var updatedCategory = await Uow.Categories.UpdateAsync(category);
            await Uow.SaveAsync();
            return new DataResult<CategoryUpdateDto>(ResultStatus.Success, $"{categoryUpdateDto.Name} adlı kategori başarıyla güncellenmiştir.",categoryUpdateDto);
        }

        public async Task<IDataResult<Category>> DeleteAsync(int categoryId, string modifiedByName)
        {
            var category = await Uow.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = true;
                category.IsActive = false;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                await Uow.Categories.UpdateAsync(category);
                await Uow.SaveAsync();
                return new DataResult<Category>(ResultStatus.Success, $"{category.Name} adlı kategori başarıyla silinmiştir.",category);
            }
            return new DataResult<Category>(ResultStatus.Error, "Böyle bir kategori bulunamadı.",null);
        }

        public async Task<IDataResult<CategoryDto>> HardDeleteAsync(int categoryId)
        {
            var category = await Uow.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                await Uow.Categories.DeleteAsync(category);
                await Uow.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Success, $"{category.Name} adlı kategori başarıyla veritabanından silinmiştir.", Mapper.Map<CategoryDto>(category));
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı.", null);
        }

        public async Task<IDataResult<Category>> Addv2Async(CategoryAddDto categoryAddDto, string createdByName)
        {
            var category = Mapper.Map<Category>(categoryAddDto);
            category.CreatedByName = createdByName;
            category.ModifiedByName = createdByName;
            var addedCategory = await Uow.Categories.AddAsync(category);
            await Uow.SaveAsync();
            return new DataResult<Category>(ResultStatus.Success, $"{categoryAddDto.Name} adlı kategori başarıyla eklenmiştir.", addedCategory);
        }

        public async Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDtoAsync(int categoryId)
        {
            var result = await Uow.Categories.AnyAsync(c => c.Id == categoryId);
            if (result)
            {
                var category = await Uow.Categories.GetAsync(c => c.Id == categoryId);
                var categoryUpdateDto = Mapper.Map<CategoryUpdateDto>(category);
                return new DataResult<CategoryUpdateDto>(ResultStatus.Success, categoryUpdateDto);
            }
            else
            {
                return new DataResult<CategoryUpdateDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı.", null);
            }
        }
        public async Task<IDataResult<int>> CountAsync()
        {
            var categoriesCount = await Uow.Categories.CountAsync();
            if (categoriesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, categoriesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }

        public async Task<IDataResult<int>> CountByNonDeletedAsync()
        {
            var categoriesCount = await Uow.Categories.CountAsync(c => !c.IsDeleted);
            if (categoriesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, categoriesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }
    }
}
