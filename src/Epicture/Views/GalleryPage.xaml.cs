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
using Xamarin.Forms.Markup;
using System.Windows.Input;

namespace Epicture.Views
{
    public partial class GalleryPage : ContentPage
    {
        private StackLayout _stackLayout;
        private DataJsonApiAllGallery _dataImage;
        private int _index;
        private Editor _textSearch;
        private Button _buttonSearch;

        public GalleryPage()
        {
            Title = "Gallery";
            _index = 0;
            _dataImage = ResquestAPI.getAllGallery(_index.ToString());
            
            _stackLayout = new StackLayout
            {
                BackgroundColor =  Color.FromHex("101010"),
                Padding = new Thickness(5),
            };
          
            reloadview();
            ScrollView scrollView = new ScrollView { Content = _stackLayout };
            RefreshView refreshView = new RefreshView();
            ICommand refreshCommand = new Command(() =>
            {
                reloadview();
                refreshView.IsRefreshing = false;
            });
            refreshView.Command = refreshCommand;
            refreshView.Content = scrollView;
            Content = refreshView;
        }
        public void morePageGallery()
        {
            ++_index;
            _dataImage = null;
            _dataImage = ResquestAPI.getAllGallery("1");
            reloadview();
        }
        public void Searchview()
        {
            _textSearch = new Editor { };
            _buttonSearch = new Button
            {
                BackgroundColor = Color.FromHex("101010"),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Text = "Search"
            };
            Frame search = new Frame
            {
                BackgroundColor = Color.FromHex("000000"),
                Content = new StackLayout
                {
                    Children =
                    {
                        _textSearch,
                        _buttonSearch
                    }
                },

            };
            _buttonSearch.Clicked += delegate {
                if (_textSearch.Text == null || _textSearch.Text == "") {
                    _dataImage = ResquestAPI.getAllGallery("0");
                    reloadview();
                    return;
                }
                Console.WriteLine("--------------------\n" + _textSearch.Text + "\n--------------------");
                _dataImage = ResquestAPI.getSearchImages(_textSearch.Text);
                reloadview();
            };
            _stackLayout.Children.Add(search);
        }
        public static void test(string id)
        {
            Console.WriteLine(id);
            ResquestAPI.AddFavoriteImgAccount(id);
        }
        public static void test2(string id)
        {
            Console.WriteLine(id);
            ResquestAPI.AddFavoriteAlbumAccount(id);
        }
        public void AddImageInGallery(string url, string titre, IList<ImageApi> image, CompomentImage elm)
        {
            string tmp = null;
            string imge_almbum = null;

            if (elm.images_count > 1)
                imge_almbum = "album.png";
            else
                imge_almbum = "picture.png";
            Console.WriteLine("----------------" + _index++ + elm.favorite);
            if (elm.favorite == true)
                tmp = "heart.png";
            else
                tmp = "like.png";
            ImageButton favori = new ImageButton
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
                Text = elm.views != null ? elm.views.ToString() : "0",
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
            BoxView linetop = new BoxView()
            {
                Color = Color.FromHex("101010"),
                WidthRequest = 100,
                HeightRequest = 2,
                Margin = new Thickness(0, 0, 0, 5)
            };
            Image album = new Image
            {
                BackgroundColor = Color.FromHex("000000"),
                Source = imge_almbum,
                HeightRequest = 30,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Center
            };
            BoxView line = new BoxView()
            {
                Color = Color.FromHex("101010"),
                WidthRequest = 100,
                HeightRequest = 2,
                Margin = new Thickness(0, 5, 0, 0)
            };
            Grid grid_buttom = new Grid
            {
                Children =
                {
                    favori,
                    vue,
                    imgvue
                }
            };
            favori.Clicked += delegate {
                elm.favorite = !elm.favorite;
                if (elm.favorite == true)
                    favori.Source = "heart.png";
                else
                    favori.Source = "like.png"; 
                test2(elm.id);
            };
            ImageButton img = new ImageButton
            {
                BackgroundColor = Color.FromHex("000000"),
                Source = url,
                HeightRequest = 300
            };
            img.Clicked += delegate
            {
                if (elm.images_count > 1)
                    Navigation.PushAsync(new AlbumPage(image, titre));
            };
            Label dess = new Label
            {
                Text = (elm.images[0].description != null) ? elm.images[0].description.ToString() : "",
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
                        album,
                        new Label
                        {
                            Text=titre,
                            FontSize = 18
                        },
                        linetop,
                        img,
                        grid_buttom,
                        line,
                        dess
                    }
                },
            });
        }

        public void build()
        {
            IList<CompomentImage> data = _dataImage.data;
            IList<ImageApi> tmp = null;
            string titre = null;
 
            if (_dataImage.status == -1)
                return;
            foreach (CompomentImage element in data) {
                Console.WriteLine(element);
                if (element.images == null)
                    continue;
                tmp = element.images;
                titre = element.title.ToString();
                if (Path.GetExtension(tmp[0].link) != ".png" && Path.GetExtension(tmp[0].link) != ".jpg")
                    tmp[0].link = Path.ChangeExtension(tmp[0].link, ".png");
                AddImageInGallery(tmp[0].link, titre, tmp, element);
                tmp = null;
            }
        }
        public void clearSatklayout()
        {
            if (_stackLayout.Children.Count == 0)
                return;
            for (int i = _stackLayout.Children.Count - 1; i >= 0; i--)
                _stackLayout.Children.RemoveAt(i);
        }
        public void reloadview()
        {
            clearSatklayout();
            Searchview();
            build();
            /*Button more = new Button
            {
                Text = "More",
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };
            more.Clicked += delegate {
                morePageGallery();
            };
            _stackLayout.Children.Add(more);*/
        }
    }
}