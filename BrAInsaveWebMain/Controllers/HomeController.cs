using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BrAInsaveWebMain.Models;

namespace BrAInsaveWebMain.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            string imgURL = "https://jiafengtrystorage.blob.core.windows.net/samples-workitems/patient1";
            ViewData["imgURL"] = imgURL;
            ViewData["faceDetectionResult"] = await CognitiveService.FaceDetctionAsync(imgURL);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public Task<string> BlobUpdate(string img)
        {
            //this function will be called when receiving this http Post request: {URLBase}/Home/BlobUpdate
            return null;
        }
    }
}
