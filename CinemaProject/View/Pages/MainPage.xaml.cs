using CinemaProject.Model;
using CinemaProjectLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CinemaProject.View.Pages
{
    public partial class MainPage : Page
    {
       
        Core db = new Core();

        public MainPage()
        {
            InitializeComponent();
            PopulateFilmButtons();
        }

        private void PopulateFilmButtons()
        {
            var filmList = db.context.Films.ToList();

            if (filmList.Count == 0)
            {
                FilmsStackPanel.Children.Add(new TextBlock { Text = "Фильмы ещё не добавлены" });
            }
            else
            {
                foreach (var film in filmList)
                {
                    Button button = new Button();
                    button.Content = CreateFilmButtonContent(film);
                    button.Click += (sender, e) => FilmButton_Click(film);

                    FilmsStackPanel.Children.Add(button);
                }
            }
        }

        private StackPanel CreateFilmButtonContent(Films film)
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;

            Image filmImage = new Image();
            filmImage.Source = LoadFilmImage(film); // Загрузка изображения фильма
            filmImage.Width = 100;
            filmImage.Height = 150;
            stackPanel.Children.Add(filmImage);

            TextBlock filmNameTextBlock = new TextBlock();
            filmNameTextBlock.Text = film.Name;
            stackPanel.Children.Add(filmNameTextBlock);

            return stackPanel;
        }

        private BitmapImage LoadFilmImage(Films film)
        {
            try
            {
                // Загрузка изображения фильма из базы данных или файла
                byte[] imageBytes = film.Image;
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = new System.IO.MemoryStream(imageBytes);
                    image.EndInit();
                    return image;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading film image: " + ex.Message);
            }

            // Возвращаем пустое изображение по умолчанию, если загрузка не удалась
            return new BitmapImage();
        }

        private void FilmButton_Click(Films film)
        {
            // Здесь вы можете выполнить необходимые действия при нажатии на кнопку фильма
            // например, перейти на другую страницу и передать экземпляр объекта фильма
            // NavigationService.Navigate(new FilmPage(film));
        }
    }
}
