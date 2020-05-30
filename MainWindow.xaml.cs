using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using OMDbAPI.Components;

namespace OMDbAPI
{
    public partial class MainWindow : Window
    {
        private string filmName;
        private string filmType;
        private FilmList filmList;
        private string apiKey = "15bc6b97";
        public MainWindow()
        {
            InitializeComponent();
            filmList = new FilmList();
        }
        private string GetResponseOMDb()
        {
            string baseUri = $"http://www.omdbapi.com/?apikey={apiKey}";

            string trimStart = "{\"Search\":";
            string trimEnd = ",\"totalResults\":\"5\",\"Response\":\"True\"}\"";

            var sb = new StringBuilder(baseUri);
            sb.Append($"&s={filmName}");
            sb.Append($"&type={filmType}");

            var request = WebRequest.Create(sb.ToString());
            request.Timeout = 1000;
            request.Method = "GET";
            request.ContentType = "application/json";

            string result = string.Empty;

            try
            {
                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            result = reader.ReadToEnd();
                            result = result.Substring(trimStart.Length);
                            result = result.Substring(0, result.Length - trimEnd.Length + 1);
                        }
                    }
                }
            }
            catch (WebException e)
            {

            }
            catch (Exception e)
            {

            }
            
            return result;
        }

        private void PushToResultPanel()
        {
            SearchResult.Children.Clear();
            if (filmList.List == null)
            {
                Label lbl = CreateLabel("No film results ;(");
                SearchResult.Children.Add(lbl);
                return;
            }
            foreach (Film film in filmList.List)
            {
                try
                {
                    Image img = new Image();
                    img.Height = 320;
                    img.Width = 370;
                    BitmapImage logo = new BitmapImage();
                    logo.BeginInit();
                    logo.UriSource = new Uri(film.Poster);
                    logo.EndInit();
                    img.Source = logo;
                    SearchResult.Children.Add(img);
                }
                catch (System.UriFormatException)
                {
                    Label lbl = CreateLabel(film.Title);
                    SearchResult.Children.Add(lbl);
                } 
            }
        }

        private Label CreateLabel(string content)
        {
            Label lbl = new Label();
            lbl.Content = content;
            lbl.Margin = new Thickness(10.0, 10.0, 10.0, 10.0);
            lbl.Background = new SolidColorBrush(Colors.Blue) { Opacity = 0.6 };
            return lbl;
        }

        #region BaseEvents
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            JsonBehaviour.SaveFilmsToJson(GetResponseOMDb());
            filmList.List = JsonBehaviour.LoadFilmsFromJson();
            PushToResultPanel();
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            filmType = ((sender as ComboBox).SelectedItem as TextBlock).Text.ToLower();
        }
        
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            filmName = (sender as TextBox).Text.ToLower();
        }
        
        #endregion



    }
}

