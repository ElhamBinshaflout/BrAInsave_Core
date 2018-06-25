using System;
using System.Collections.Generic;
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
        public static async Task<string> FaceDetctionAsync(string imgURL)
        {
            string imageWithFaces = "{\"url\":\"" + imgURL + "\"}";
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            var faceAttributes = "age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigService.getConfig().cognitiveServiceConfig.subscriptionKey);

            HttpResponseMessage response;

            // Request parameters
            queryString["returnFaceId"] = "true";
            queryString["returnFaceLandmarks"] = "false";
            queryString["returnFaceAttributes"] = faceAttributes;
            var uri = ConfigService.getConfig().cognitiveServiceConfig.baseURI + "detect?" + queryString;

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes(imageWithFaces);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
            }
  
            return await response.Content.ReadAsStringAsync(); ;
        }
    }
}
