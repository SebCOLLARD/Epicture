using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Epicture.Services;

namespace Epicture.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private WebView imgurView = new WebView();
        string url = "https://api.imgur.com/oauth2/authorize?client_id=" + ResquestAPI.clientID + "&response_type=token";
        public MainPage()
        {
            this.BindingContext = this;
            InitializeComponent();
            imgurView.Source = url;
            imgurView.GoForward();
            Content = imgurView;
            imgurView.Navigated += GetURL;
        }

        public void Button_Clicked(object sender, System.EventArgs e)
        {
            /*imgurView.GoForward();
            Content = imgurView;
            imgurView.Navigated += GetURL;*/
        }

        public void GetURL(object sender, WebNavigatedEventArgs e)
        {
            if (e.Url.ToString().Split('/').ToArray()[2] != "www.blank.org")
            {
                return;
            }
            string Token = e.Url.ToString();
            string[] res = Token.Split('&').ToArray();
            string RefreshToken = res[3];
            string[] finalParse = RefreshToken.Split('=').ToArray();
            RefreshToken = finalParse[1];

            bool resPage = ResquestAPI.GetToken(RefreshToken);
            string test = ResquestAPI.TokenClass.access_token;
            Console.WriteLine("this is the token = " + test);
            if (resPage == true)
            {
                Application.Current.MainPage = new AppShell();
                Shell.Current.GoToAsync("//APP");
            }
        }

    }
}