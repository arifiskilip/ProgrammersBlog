using Microsoft.AspNetCore.Http;
using ProgrammersBlog.Shared.Entities.Abstract;
using ProgrammersBlog.Shared.Results;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProgrammersBlog.Shared.Helpers.Image
{
    public class ImageHelper : IImageHelper
    {
        private string _fileDirectory = Environment.CurrentDirectory + "\\wwwroot\\img";

        public async Task<IDataResult<UploadedImageDto>> UploadImage(string userName, IFormFile pictureFile, string folderName="userImages")
        {
            if (!Directory.Exists($"{_fileDirectory}/{folderName}"))
            {
                Directory.CreateDirectory($"{_fileDirectory}/{folderName}");
            }
            string fileExtension = Path.GetExtension(pictureFile.FileName);
            string fileName = $"{userName}_{Guid.NewGuid().ToString()}{fileExtension}";
            var path = Path.Combine($"{_fileDirectory}/{folderName}", fileName);
            await using(var strea = new FileStream(path, FileMode.Create))
            {
                await pictureFile.CopyToAsync(strea);
            }
            return new DataResult<UploadedImageDto>(ResultStatus.Success,"Ekleme işlemi başarılı!", new()
            {
                Extension = fileExtension,
                FolderName = folderName,
                FullName = $"{folderName}/{fileName}",
                Path = path,
                Size = pictureFile.Length
            });
        }

        public IResult DeleteImage(string pictureName)
        {
            var fileToDelete = Path.Combine($"{_fileDirectory}", pictureName);
            if (System.IO.File.Exists(fileToDelete))
            {
                if (fileToDelete.Contains("default.png"))
                {
                    return new Result(ResultStatus.Success,"Silme işlemi başarılı!");
                }
                System.IO.File.Delete(fileToDelete);
                return new Result(ResultStatus.Success, "Silme işlemi başarılı!");
            }
            else
            {
                return new Result(ResultStatus.Error, "Silme işlemi başarısız!");
            }
        }
    }
}
