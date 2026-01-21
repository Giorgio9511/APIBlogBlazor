using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;

namespace ApiBlog.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files.FirstOrDefault();

                if (file == null || file.Length == 0)
                    return BadRequest("No se envió ningun archivo");

                var folderName = "ImagenesPosts";
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetFileName(file.FileName);
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName + "/", fileName);
                    using(var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(dbPath);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
