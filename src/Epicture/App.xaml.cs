using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Epicture.Services;
using Epicture.Views;

namespace Epicture
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
