using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Epicture.ViewModels
{
    public class UploadViewModel : BaseViewModel
    {
        public UploadViewModel()
        {
            Title = "Upload";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamain-quickstart"));
        }

        public ICommand OpenWebCommand { get; }
    }
}