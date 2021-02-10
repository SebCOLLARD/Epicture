using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Epicture.Services;
using Xamarin.Forms.Markup;
using System.Windows.Input;

namespace Epicture.Views
{
    public partial class FavoritesPage : ContentPage
    {
        private FavoriteClass _favorite;
        private StackLayout _mainStackLayout;
        public FavoritesPage()
        {
            Title = "Favorites";
            _favorite = null;
            _mainStackLayout = new StackLayout 
            {
                BackgroundColor = Color.FromHex("101010"),
                Padding = new Thickness(5),
            };
            relaodFavorites();
            ScrollView scrollView = new ScrollView { Content = _mainStackLayout };
            RefreshView refreshView = new RefreshView();
            ICommand refreshCommand = new Command(() =>
            {
                relaodFavorites();
                refreshView.IsRefreshing = false;
            });
            refreshView.Command = refreshCommand;
            refreshView.Content = scrollView;
            Content = refreshView;
        }

        public void loadData()
        {
            _favorite = null;
            _favorite = ResquestAPI.getFavoritesImageaccount();
        }

        public Frame buildCard(DataImg element)
        {
            string imge_almbum = null;

            if (element == null)
                return null;
            if (element.is_album == true) {
                imge_almbum = "album.png";
                if (element.type == "image/jpeg")
                    element.link = "https://i.imgur.com/" + element.cover + ".jpg";
                else
                    element.link = "https://i.imgur.com/" + element.cover + ".png";
            } else {
                imge_almbum = "picture.png";
                if (element.type != "image/png" && element.type != "image/jpeg")
                    element.link = Path.ChangeExtension(element.link, ".png");
            }
            Label label = new Label
            {
                Text = element.title,
                Margin = new Thickness(0, 5, 0, 0),
                FontSize = 18
            };
            ImageButton mainImg = new ImageButton
            {
                BackgroundColor = Color.FromHex("000000"),
                Source = element.link,
                HeightRequest = 300
            };
            ImageButton Del = new ImageButton
            {
                BackgroundColor = Color.FromHex("000000"),
                Source = "clear.png",
                HeightRequest = 30,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.Center
            };
            Image album = new Image
            {
                BackgroundColor = Color.FromHex("000000"),
                Source = imge_almbum,
                HeightRequest = 30,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Center
            };
            BoxView linetop = new BoxView()
            {
                Color = Color.FromHex("101010"),
                WidthRequest = 100,
                HeightRequest = 2,
                Margin = new Thickness(0, 0, 0, 5)
            };
            Grid stackLayout_bt = new Grid
            {
                Children =
                {
                    album,
                    Del
                }
            };
            Frame tmp = new Frame
            {
                BackgroundColor = Color.FromHex("000000"),
                Content = new StackLayout
                {
                    Children =
                    {
                        stackLayout_bt,
                        label,
                        linetop,
                        mainImg
                    }
                },
            };
            Del.Clicked += delegate
            {
                OnAlertYesNoClicked(element, tmp);
            };
            return tmp;
        }

        async void OnAlertYesNoClicked(DataImg element, Frame tmp)
        {
            bool answer = false;
            string str = "Do you really want to remove this image from your favorites?";
            string str1 = "Do you really want to remove this album from your favorites?";


            answer = await DisplayAlert("Question?", (element.is_album == true) ? str1 : str, "Yes", "No");
            if (answer == true)
            {
                if (element.is_album == true)
                    GalleryPage.test2(element.id);
                else
                    GalleryPage.test(element.id);
                _mainStackLayout.Children.Remove(tmp);
            }
        }

        public void loadViewFavorite()
        {
            IList<DataImg> data = null;
            Frame tmp = null;

            if (_favorite == null)
                return;
            data = _favorite.data;
            foreach (DataImg element in data) {
                if (element == null)
                    continue;
                tmp = buildCard(element);
                if (tmp != null)
                    _mainStackLayout.Children.Add(tmp);
                tmp = null;
            }
        }

        public void clearSatklayout()
        {
            if (_mainStackLayout.Children.Count == 0)
                return;
            for (int i = _mainStackLayout.Children.Count - 1; i >= 0; i--)
                _mainStackLayout.Children.RemoveAt(i);
        }
        
        public void relaodFavorites()
        {
            clearSatklayout();
            loadData();
            loadViewFavorite();
        }
    }
}