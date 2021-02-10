using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using Epicture.ViewModels;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Epicture.Services;

namespace Epicture.Views
{
    public partial class AlbumPage : ContentPage
    {
        private StackLayout _stackLayout;
        public AlbumPage(IList<ImageApi> image, string titre)
        {
            Title = "Album";
            _stackLayout = new StackLayout
            {
                BackgroundColor = Color.FromHex("101010"),
                Padding = new Thickness(5),
            };
            Label titreview = new Label
            {
                Text = titre,
                Margin = 20
            };
            StackLayout stackLayout = new StackLayout
            {
                BackgroundColor = Color.FromHex("000000"),
                Children =
                {
                    titreview
                }
            };
            _stackLayout.Children.Add(stackLayout);
            foreach (ImageApi img in image)
            {
                if (Path.GetExtension(img.link) != ".png" && Path.GetExtension(img.link) != ".jpg")
                    img.link = Path.ChangeExtension(img.link, ".png");
                AddImageInGallery(img.link, img.id, img);
            }
            ScrollView scrollView = new ScrollView { Content = _stackLayout };
            Content = scrollView;
        }

        public void AddImageInGallery(string url, string id, ImageApi img)
        {
            string tmp = null;
            if (img.favorite == true)
                tmp = "heart.png";
            else
                tmp = "like.png";
            ImageButton butonn = new ImageButton
            {
                Margin = 10,
                Source = tmp,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromHex("000000"),
                HeightRequest = 40
            };
            Label vue = new Label
            {
                Text = img.views != null ? img.views.ToString() : "0",
                Margin = new Thickness(0, 0, 60, 0),
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            Image imgvue = new Image
            {
                Margin = 10,
                Source = "eyes.png",
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromHex("000000"),
                HeightRequest = 40
            };
            Grid grid_buttom = new Grid
            {
                Children =
                {
                    butonn,
                    vue,
                    imgvue
                }
            };
            butonn.Clicked += delegate {
                img.favorite = !img.favorite;
                if (img.favorite == true)
                    butonn.Source = "heart.png";
                else
                    butonn.Source = "like.png";
                GalleryPage.test(id);
            };
            BoxView line = new BoxView()
            {
                Color = Color.FromHex("101010"),
                WidthRequest = 100,
                HeightRequest = 2,
                Margin = new Thickness(0, 5, 0, 0)
            };
            Label dess = new Label
            {
                Text = (img.description != null) ? img.description.ToString() : "",
                FontSize = 18,
                Margin = new Thickness(5, 0, 0, 0)
            };
            _stackLayout.Children.Add(new Frame
            {
                BackgroundColor = Color.FromHex("000000"),
                Content = new StackLayout
                {
                    Children =
                    {
                        new Image {
                            Source = url,
                            HeightRequest = 300
                        },
                        grid_buttom,
                        line,
                        dess
                    }
                },
            });
        }
    }
}