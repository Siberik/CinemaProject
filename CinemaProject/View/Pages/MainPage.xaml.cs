using CinemaProject.Model;
using CinemaProjectLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CinemaProject.View.Pages
{
    public partial class MainPage : Page
    {
        private const int MaxButtonsPerPage = 5;
        private List<Button> filmButtons = new List<Button>();
        private int currentPageIndex = 0;
        private Users user;
        private Administrators admin;
        Core db = new Core();

        public MainPage(Users user=null,Administrators admin=null)
        {
            this.user = user;
            this.admin = admin;
            InitializeComponent();
            PopulateFilmButtons();
            if (admin != null) { 
            AddButton.Visibility = Visibility.Visible;
            }
            else
            {
                AddButton.Visibility= Visibility.Collapsed;
            }
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
                // Получение ссылки на стиль из словаря
                Style buttonStyle = Application.Current.Resources["FilmButtonStyle"] as Style;

                foreach (var film in filmList)
                {
                    Button button = new Button();
                    button.Margin = new Thickness(0, 0, 10, 0);
                    button.HorizontalAlignment = HorizontalAlignment.Stretch;
                    button.Width = 240;
                    button.Height = 390;
                    button.Style = buttonStyle; // Применение стиля к кнопке
                    button.Content = CreateFilmButtonContent(film);
                    button.Click += (sender, e) => FilmButton_Click(film);

                    filmButtons.Add(button);
                }

                ShowCurrentPage();
            }
        }



        private void ShowCurrentPage()
        {
            FilmsStackPanel.Children.Clear();

            int startIndex = currentPageIndex * MaxButtonsPerPage;
            int endIndex = startIndex + MaxButtonsPerPage;
            List<Button> currentPageButtons = filmButtons.Skip(startIndex).Take(MaxButtonsPerPage).ToList();

            foreach (Button button in currentPageButtons)
            {
                FilmsStackPanel.Children.Add(button);
            }

            UpdatePaginationButtons();
        }

        private void UpdatePaginationButtons()
        {
            PrevButton.IsEnabled = currentPageIndex > 0;
            NextButton.IsEnabled = (currentPageIndex + 1) * MaxButtonsPerPage < filmButtons.Count;
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPageIndex > 0)
            {
                currentPageIndex--;
                ShowCurrentPage();
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)filmButtons.Count / MaxButtonsPerPage);

            if (currentPageIndex < totalPages - 1)
            {
                currentPageIndex++;
                ShowCurrentPage();
            }
        }

        private Grid CreateFilmButtonContent(Films film)
        {
            Grid grid = new Grid();

            Image filmImage = new Image();
            filmImage.Source = LoadFilmImage(film); // Загрузка изображения фильма
            filmImage.Stretch = Stretch.Fill; // Растягивание изображения на всю кнопку
            filmImage.Width = 230; // Установка ширины картинки
            filmImage.Height = 350; // Установка высоты картинки

            grid.Children.Add(filmImage);

            TextBlock filmNameTextBlock = new TextBlock();
            filmNameTextBlock.Text = film.Name;
            filmNameTextBlock.TextAlignment = TextAlignment.Center; // Выравнивание текста по центру
            filmNameTextBlock.Foreground = Brushes.White; // Цвет текста

            // Расположение названия фильма внизу кнопки
            Grid.SetRow(filmNameTextBlock, 1);

            grid.Children.Add(filmNameTextBlock);

            // Определение строк Grid
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            return grid;
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
            NavigationService.Navigate(new FilmInfoPage(film,user,admin));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            AddFilm addFilm = new AddFilm();
            addFilm.Show();
        }
    }
}
