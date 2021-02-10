using Epicture.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Epicture.Services;
using System.IO;
using System.Windows.Input;
using Xamarin.Essentials;



namespace Epicture.Views
{
    public partial class AccountPage : ContentPage
    {
        private UploadImage2 _favorite;
        private StackLayout _mainStackLayout;
        public AccountPage()
        {
            Title = "Post";
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
            ToolbarItem ToolBarUpload = new ToolbarItem
            {
                Text = "Upload Image"
            };
            ToolBarUpload.Clicked += async delegate 
            {
                await Navigation.PushAsync(new UploadPage());
            };
            this.ToolbarItems.Add(ToolBarUpload);
            refreshView.Command = refreshCommand;
            refreshView.Content = scrollView;
            Content = refreshView;
        }

        public void loadData()
        {
            _favorite = null;
            _favorite = ResquestAPI.getPost();
        }

        public Frame buildCard(Data element)
        {
            if (element == null)
                return null;
            Label titre = new Label
            {
                Text = (element.title != null) ? element.title.ToString() : "",
                FontSize = 18,
                Margin = new Thickness(0, 0, 0, 5)
            };
            Label dess = new Label
            {
                Text = (element.description != null) ? element.description.ToString() : "",
                FontSize = 18,
                Margin = new Thickness(5, 0, 0, 0)
            };
            BoxView linetop = new BoxView()
            {
                Color = Color.FromHex("101010"),
                WidthRequest = 100,
                HeightRequest = 2,
                Margin = new Thickness(0, 0, 0, 5)
            };
            BoxView line = new BoxView()
            {
                Color = Color.FromHex("101010"),
                WidthRequest = 100,
                HeightRequest = 2,
                Margin = new Thickness(0, 5, 0, 0)
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
            Frame tmp = new Frame
            {
                BackgroundColor = Color.FromHex("000000"),
                Content = new StackLayout
                {
                    Children =
                    {
                        Del,
                        titre,
                        linetop,
                        mainImg,
                        line,
                        dess
                    }
                },
            };
            Del.Clicked += delegate
            {
                OnAlertYesNoClicked(element, tmp);
            };
            return tmp;
        }
        public void test(string id)
        {
            ResquestAPI.delPost(id);
        }
        async void OnAlertYesNoClicked(Data element, Frame tmp)
        {
            bool answer = false;
            string str = "Do you really want to remove this image from your favorites?";

            answer = await DisplayAlert("Question?", str, "Yes", "No");
            if (answer == true) {
                test(element.id);
                _mainStackLayout.Children.Remove(tmp);
            }
        }

        public void loadViewFavorite()
        {
            IList<Data> data = null;
            Frame tmp = null;

            if (_favorite == null)
                return;
            data = _favorite.data;
            foreach (Data element in data)
            {
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