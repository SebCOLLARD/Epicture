using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Epicture.Services;

namespace Epicture.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        private StackLayout _stackLayout;
        private DataJsonApiAllGallery _dataImage;

        public SearchPage(string textSearch)
        {
            Title = "Gallery";
            _dataImage = ResquestAPI.getSearchImages(textSearch);
            _stackLayout = new StackLayout
            {
                BackgroundColor = Color.FromHex("101010"),
                Spacing = 20,
                Padding = new Thickness(20),
            };
            build(_dataImage);
            ScrollView scrollView = new ScrollView { Content = _stackLayout };
            Content = scrollView;
        }
        public static void test(string id)
        {
            Console.WriteLine(id);
            ResquestAPI.AddFavoriteImgAccount(id);
        }
        public void AddImageInGallery(string url, string titre, IList<ImageApi> image)
        {
            ImageButton butonn = new ImageButton
            {
                Margin = 10,
                Source = "heart.png",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromHex("2E1212"),
                HeightRequest = 40
            };
            butonn.Clicked += delegate {
                test(image[0].id);
            };
            ImageButton img = new ImageButton
            {
                BackgroundColor = Color.FromHex("000000"),
                Source = url,
                HeightRequest = 300
            };
            img.Clicked += delegate {
                Navigation.PushAsync(new AlbumPage(image, titre));
            };
            _stackLayout.Children.Add(new Frame
            {
                BackgroundColor = Color.FromHex("000000"),
                Content = new StackLayout
                {
                    Children =
                    {
                        new Label
                        {
                            Text=titre
                        },
                        img,
                        butonn
                    }
                },
            });
        }

        public void build(DataJsonApiAllGallery Inputdata)
        {
            IList<CompomentImage> data = Inputdata.data;
            IList<ImageApi> tmp = null;
            string titre = null;

            if (Inputdata.status == -1)
                return;
            foreach (CompomentImage element in data)
            {
                Console.WriteLine(element);
                if (element.images == null)
                    continue;
                tmp = element.images;
                titre = element.title.ToString();
                if (Path.GetExtension(tmp[0].link) != ".png" && Path.GetExtension(tmp[0].link) != ".jpg")
                    tmp[0].link = Path.ChangeExtension(tmp[0].link, ".png");
                AddImageInGallery(tmp[0].link, titre, tmp);
                tmp = null;
            }
        }
    }
}