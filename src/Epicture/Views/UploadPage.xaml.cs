using Epicture.ViewModels;
using Epicture.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.FilePicker.Abstractions;
using Plugin.FilePicker;
using System.IO;

namespace Epicture.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UploadPage : ContentPage
    {
        public FileData file;
        public string URL = "https://maxcdn.icons8.com/Share/icon/Photo_Video/add_image1600.png";
        public string test = "Uri: ";
        public Exeption exeption = new Exeption();
   
        public UploadPage()
        {
            InitializeComponent();
            BindingContext = new UploadViewModel();
            imagePreview.Source = URL;
            test += URL;
        }

  
        private async void Choose_Image_Click(object sender, System.EventArgs e)
        {
            try
            {
                file = await CrossFilePicker.Current.PickFile();
                Stream data = new MemoryStream(file.DataArray);
                imagePreview.Source = ImageSource.FromStream(() => { return data; });
            }
            catch (Exception)
            {
                exeption.Error(ref error_text, "No image selected !");
                imagePreview.Source = URL;
            }
        }

        public void Save_Image_Click(object sender, System.EventArgs e)
        {
            saveImage.IsEnabled = false;
            saveImage.IsVisible = false;
            bool uploadSucceful;
            if (imagePreview.Source.ToString() == test)
            {
                exeption.Error(ref error_text, "You must select an image!");
                saveImage.IsVisible = true;
                return;
            }
            if (imageTitles.Text == "" || imageTitles.Text == null)
            {
                exeption.Error(ref error_text, "Fill the \"Title\" field.");
                saveImage.IsVisible = true;
                return;
            }
            if (file.DataArray == null)
            {
                exeption.Error(ref error_text, "File not found.");
                saveImage.IsVisible = true;
                return;
            }
            uploadSucceful = ResquestAPI.uploadImage(file.DataArray, imageDescription.Text, imageTitles.Text);
            if (uploadSucceful)
            {
                imageTitles.Text = "";
                imageDescription.Text = "";
                imagePreview.Source = URL;
                saveImage.IsEnabled = true;
                saveImage.IsVisible = true;
                return;
            }
            exeption.Error(ref error_text, "Impossible de publier le fichier");
            saveImage.IsVisible = true;
        }
    }
}