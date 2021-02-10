using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Newtonsoft.Json;
using RestSharp;

namespace Epicture.Services
{
    class ResquestAPI : ContentPage
    {
        public static string clientID = "2516362069d9e15";
        public static string clientSecret = "110b0e90198f49997a9657b8219b86b55a29ffc4";
        public static DataJsonApiToken TokenClass;
        public static DataJsonApiGallery GalleryClass;
       
        public static bool GetToken(string token)
        {
            RestClient client = new RestClient("https://api.imgur.com/oauth2/token")
            {
                Timeout = -1
            };
            RestRequest request = new RestRequest(Method.POST)
            {
                AlwaysMultipartFormData = true
            };
            request.AddParameter("refresh_token", token);
            request.AddParameter("client_id", clientID);
            request.AddParameter("client_secret", clientSecret);
            request.AddParameter("grant_type", "refresh_token");
            IRestResponse response = client.Execute(request);

            TokenClass = JsonConvert.DeserializeObject<DataJsonApiToken>(response.Content);
            return true;
        }

        public static void RequestGallery(string search)
        {
            RestClient client = new RestClient($"https://api.imgur.com/3/gallery/search/top/0/?q_exactly={search}")
            {
                Timeout = -1
            };
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Client-ID {clientID}");
            request.AlwaysMultipartFormData = true;
            IRestResponse response = client.Execute(request);
            GalleryClass = JsonConvert.DeserializeObject<DataJsonApiGallery>(response.Content);
        }

        public static DataJsonApiAllGallery getAllGallery(string page)
        {
            var client = new RestClient($"https://api.imgur.com/3/gallery/hot/top/day/{page}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Client-ID {clientID}");
            request.AlwaysMultipartFormData = true;
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<DataJsonApiAllGallery>(response.Content);
        }

        public static bool uploadImage(byte[] path, string name, string title = null)
        {
            UploadImage checkSuccess;
            RestClient client = new RestClient("https://api.imgur.com/3/image")
            {
                Timeout = -1
            };
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {TokenClass.access_token}");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("image", Convert.ToBase64String(path));
            if (title != null)
            {
                request.AddParameter("title", title);
            }
            request.AddParameter("description", name);
            IRestResponse response = client.Execute(request);
            checkSuccess = JsonConvert.DeserializeObject<UploadImage>(response.Content);
            return checkSuccess.success;
        }

        public static FavoriteClass getFavoritesImageaccount()
        {
            RestClient client = new RestClient($"https://api.imgur.com/3/account/{TokenClass.account_username}/favorites/");
            client.Timeout = -1;
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {TokenClass.access_token}");
            request.AlwaysMultipartFormData = true;
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<FavoriteClass>(response.Content);
        }

        public static void AddFavoriteImgAccount(string img_ID)
        {
            RestClient client = new RestClient($"https://api.imgur.com/3/image/{img_ID}/favorite");
            client.Timeout = -1;
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {TokenClass.access_token}");
            request.AlwaysMultipartFormData = true;
            client.Execute(request);
        }

        public static void AddFavoriteAlbumAccount(string img_ID)
        {
            RestClient client = new RestClient($"https://api.imgur.com/3/album/{img_ID}/favorite");
            client.Timeout = -1;
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {TokenClass.access_token}");
            request.AlwaysMultipartFormData = true;
            client.Execute(request);
        }

        public static UploadImage2 getPost()
        {
            RestClient client = new RestClient("https://api.imgur.com/3/account/me/images");
            client.Timeout = -1;
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {TokenClass.access_token}");
            request.AlwaysMultipartFormData = true;
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<UploadImage2>(response.Content);
        }

        public static void delPost(string img_ID)
        {
            RestClient client = new RestClient($"https://api.imgur.com/3/image/{img_ID}");
            client.Timeout = -1;
            RestRequest request = new RestRequest(Method.DELETE);
            request.AddHeader("Authorization", $"Bearer {TokenClass.access_token}");
            request.AlwaysMultipartFormData = true;
            IRestResponse response = client.Execute(request);
        }

        public static DataJsonApiAllGallery getSearchImages(string textSearch)
        {
            RestClient client = new RestClient($"https://api.imgur.com/3/gallery/search/?q={textSearch}");
            client.Timeout = -1;
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Client-ID {clientID}");
            request.AlwaysMultipartFormData = true;
            IRestResponse response = client.Execute(request);
            /*Console.WriteLine(response.Content);*/
            return JsonConvert.DeserializeObject<DataJsonApiAllGallery>(response.Content);
        }
    }
}
