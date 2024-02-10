using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Results;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<CategoryDto>> Get(int categoryId);
        Task<IDataResult<CategoryListDto>> GetAll();
        Task<IDataResult<CategoryListDto>> GetAllByNonDeleted();
        Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActive();
        Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto, string createdByName);
        Task<IDataResult<Category>> Addv2(CategoryAddDto categoryAddDto, string createdByName);
        Task<IDataResult<CategoryUpdateDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName);
        Task<IDataResult<Category>> Delete(int categoryId, string modifiedByName);
        Task<IDataResult<CategoryDto>> HardDelete(int categoryId);
        Task<IDataResult<CategoryUpdateDto>> GetCategoryDtoAsync(int categoryId);
    }
}
