using System.ComponentModel;
using Xamarin.Forms;
using Epicture.ViewModels;

namespace Epicture.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}