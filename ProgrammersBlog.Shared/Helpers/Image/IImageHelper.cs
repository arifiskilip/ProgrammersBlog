using Microsoft.AspNetCore.Http;
using ProgrammersBlog.Shared.Entities.Abstract;
using ProgrammersBlog.Shared.Results;
using System.Threading.Tasks;

namespace ProgrammersBlog.Shared.Helpers.Image
{
    public interface IImageHelper
    {
        Task<IDataResult<UploadedImageDto>> UploadImage(string userName, IFormFile pictureFile, string folderName = "userImages");
        IResult DeleteImage(string pictureName);
    }
}
