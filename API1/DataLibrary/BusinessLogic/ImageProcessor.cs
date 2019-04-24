using DataLibrary.BusinessLogic;
using LitJson;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary1.BusinessLogic
{
    public static class ImageProcessor
    {
        public static async Task<string> GetImage(string image)
        {
            string myJson = "{'image': '" + image + "'}";
            JsonData json = new JsonData();
            string imageData = image;

            try
            {
                using (var client = new HttpClient())
                {
                    var post = await client.PostAsync(
                                ReferenceList.ImgProcServer1,
                                new StringContent(myJson, Encoding.UTF8, "application/json"));

                    json = await post.Content.ReadAsStringAsync();
                }

                json = JsonMapper.ToObject(json.ToString());
                imageData = json["image"].ToString();

                return imageData;
            }
            catch (Exception)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        var post = await client.PostAsync(
                                    ReferenceList.ImgProcServer2,
                                    new StringContent(myJson, Encoding.UTF8, "application/json"));

                        json = await post.Content.ReadAsStringAsync();
                    }

                    json = JsonMapper.ToObject(json.ToString());
                    imageData = json["image"].ToString();

                    return imageData;
                }
                catch (Exception)
                {

                    return imageData;
                }
            }
        }
    }
}
