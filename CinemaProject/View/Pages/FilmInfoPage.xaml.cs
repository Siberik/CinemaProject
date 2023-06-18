using CinemaProject.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace CinemaProject.View.Pages
{
    public partial class FilmInfoPage : Page
    {
        private Core db = new Core();
        private Films film;
        private Users user;
        public FilmInfoPage(Films film, Users user = null, Administrators admin = null)
        {
            InitializeComponent();
            this.user = user;
            this.film = film;
            if (admin == null) {
                AddSeansButton.Visibility = System.Windows.Visibility.Collapsed;
                RedactStackPanel.Visibility = System.Windows.Visibility.Collapsed;
            }
            // Set film image
            if (film.Image != null && film.Image.Length > 0)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = new System.IO.MemoryStream(film.Image);
                image.EndInit();
                FilmImage.Source = image;
            }

            // Set text values from Films instance
            IdTextBlock.Text = $"Id фильма: {film.Id.ToString()}";
            NameTextBlock.Text = $"Название фильма: {film.Name}";
            DurationTextBlock.Text = $"Длительность: {film.Duration.ToString()}";
            GenreTextBlock.Text = $"Жанр: {film.Genre}";
            GenreDescriptionTextBlock.Text = $"Описание жанра: {film.GenreDescription}";
            FirmTextBlock.Text = $"Фирма: {film.Firm}";
            CountryTextBlock.Text = $"Страна: {film.Country}";
            ActorsTextBlock.Text = $"Актеры: {film.Actors}";
            VozrastTextBlock.Text = $"Возрастное ограничение: {film.Vozrast}";
            DescriptionTextBlock.Text = $"Описание фильма: {film.Description}";

            var seansesList = db.context.Seanses.Where(x => x.FilmId == film.Id).ToList();

            if (seansesList.Count == 0)
            {
                TextBlock noSessionsTextBlock = new TextBlock();
                noSessionsTextBlock.Text = "Сеансов ещё нет";
                SeansesStackPanel.Children.Add(noSessionsTextBlock);
            }
            else
            {
                foreach (var seans in seansesList)
                {
                    Button button = new Button();
                    button.Content = seans.SeansTime.ToString();
                    button.Click += (sender, e) => NavigateToSeansesPage(seans);
                    button.Margin = new System.Windows.Thickness(5, 0, 0, 0);
                    SeansesStackPanel.Children.Add(button);
                }
            }
        }

        private void NavigateToSeansesPage(Seanses seans)
        {
            SeansInfoPage seansesPage = new SeansInfoPage(seans, user);
            NavigationService.Navigate(seansesPage);
        }



        private void AddSeansButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            AddSeans addSeans = new AddSeans(film);
            addSeans.ShowDialog();
        }

        private void RedactFilmButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            RedactPage redactPage = new RedactPage(film);
            redactPage.Show();
        }

     
         
            private void DeleteFilmClick(object sender, RoutedEventArgs e)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этот фильм?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                  
                        // Находим фильм в базе данных
                        Films filmToDelete = db.context.Films.FirstOrDefault(f => f.Id == film.Id);

                        if (filmToDelete != null)
                        {
                            // Удаление билетов, связанных с фильмом
                            var ticketsToDelete = db.context.Tickets.Where(t => t.Seanses.FilmId == filmToDelete.Id);
                            db.context.Tickets.RemoveRange(ticketsToDelete);

                            // Удаление сеансов, связанных с фильмом
                            var seansesToDelete = db.context.Seanses.Where(s => s.FilmId == filmToDelete.Id);
                            db.context.Seanses.RemoveRange(seansesToDelete);

                            // Удаление фильма
                            db.context.Films.Remove(filmToDelete);
                            db.context.SaveChanges();

                            MessageBox.Show("Фильм успешно удален.");

                            // Перенаправление на предыдущую страницу
                            NavigationService?.GoBack();
                        }
                        else
                        {
                            MessageBox.Show("Фильм не найден.");
                        }
                    }
                }
            



        }
    }

