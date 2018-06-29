using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BrAInsaveWebMain.Models
{
    public class CognitiveService
    {
        public static async Task<string> FaceDetctionAsync(string imgPath)
        {
            //string imageWithFaces = "{\"url\":\"" + imgURL + "\"}";
            byte[] imgBytes;
            using (FileStream file = new FileStream(imgPath, FileMode.Open, FileAccess.Read))
            {
                imgBytes = new byte[file.Length];
                file.Read(imgBytes, 0, (int)file.Length);
            }

            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            var faceAttributes = "age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigService.CognitiveServiceConfig.subscriptionKey);

            HttpResponseMessage response;

            // Request parameters
            queryString["returnFaceId"] = "true";
            queryString["returnFaceLandmarks"] = "false";
            queryString["returnFaceAttributes"] = faceAttributes;
            var uri = ConfigService.CognitiveServiceConfig.baseURI + "detect?" + queryString;

            // Request body
            // byte[] byteData = Encoding.UTF8.GetBytes(imageWithFaces);

            using (var content = new ByteArrayContent(imgBytes))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);
            }
  
            return await response.Content.ReadAsStringAsync(); 
        }
    }
}
