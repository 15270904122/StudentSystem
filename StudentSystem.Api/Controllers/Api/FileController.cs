using StudentSystem.Api.Models.File;
using StudentSystem.Infrastructure.Result;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace StudentSystem.Api.Controllers.Api
{
    /// <summary>
    /// 文件上传
    /// </summary>
    [RoutePrefix("Api/File")]
    public class FileController : ApiController
    {

        public FileController()
        {

        }
        [Route("Upload"), HttpPost]
        public async Task<Result<FileResult>> Upload()
        {
            var request = HttpContext.Current.Request;
            var fileCollection = request.Files;
            if (fileCollection.Count > 0)
            {
                var fileResult = await UploadFile(fileCollection[0]);
                return fileResult;
            }
            else
                return Result.FromError<FileResult>("系统获取不到上传文件");
        }

        private async Task<Result<FileResult>> UploadFile(HttpPostedFile file)
        {
            var originalFileName = file.FileName;
            var fileExtensionName = Path.GetExtension(originalFileName);
            var newFileName = Guid.NewGuid().ToString("N") + fileExtensionName;
            var date = DateTime.Now.ToString("yyyyMMdd");
            var relativePath = $"/Upload/{date}/{newFileName}";
            var guidIndexDirectory = AppDomain.CurrentDomain.BaseDirectory + $"/Upload/{date}";
            if (!Directory.Exists(guidIndexDirectory))
            {
                Directory.CreateDirectory(guidIndexDirectory);
            }
            var fullPath = guidIndexDirectory + $"/{newFileName}";
            file.SaveAs(fullPath);

            return Result.FromData(new FileResult { Src = relativePath });
        }


    }
}
