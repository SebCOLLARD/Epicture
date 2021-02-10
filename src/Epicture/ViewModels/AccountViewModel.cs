using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Epicture.ViewModels
{
    class AccountViewModel : BaseViewModel
    {
        public AccountViewModel()
        {
            Title = "Post";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamain-quickstart"));
        }
        public ICommand OpenWebCommand { get; }
    }
}