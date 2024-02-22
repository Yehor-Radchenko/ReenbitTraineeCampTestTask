using Microsoft.AspNetCore.Mvc;
using ReenbitTraineeCampTestTask.Services;

namespace ReenbitTraineeCampTestTask.Controlers
{
    public class MainController : Controller
    {
        public AzureBlobService _service;
        public MainController(AzureBlobService service)
        {
            _service = service; 
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetBlobList()
        {
            var blobs = await _service.GetUploadedBlobs();
            ViewData["Blobs"] = blobs;
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var response = await _service.UploadFile(file);
            return Ok(response);
        }
    }
}
