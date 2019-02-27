using System.Configuration;
using System.Web;
using System.Web.Mvc;

namespace BootCampProject.Controllers
{
    public class FileController : Controller
    {
        [Route("file/download/{filename}")]
        public FileResult GetFile(string fileName)
        {
            var filePath = ConfigurationManager.AppSettings.Get("ThumbnailPath") + fileName + ".png";
            var bytes = System.IO.File.ReadAllBytes(filePath);
    
            return File(bytes, MimeMapping.GetMimeMapping(filePath), fileName);
        }
    }
}