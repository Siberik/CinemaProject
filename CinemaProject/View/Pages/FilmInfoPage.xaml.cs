using CinemaProject.Model;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace CinemaProject.View.Pages
{
    public partial class FilmInfoPage : Page
    {
        private Core db = new Core();
        private Films film;

        public FilmInfoPage(Films film)
        {
            InitializeComponent();

            this.film = film;

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
                    SeansesStackPanel.Children.Add(button);
                }
            }
        }

        private void NavigateToSeansesPage(Seanses seans)
        {
            //SeansesPage seansesPage = new SeansesPage(seans);
            //NavigationService.Navigate(seansesPage);
        }

     

        private void AddSeansButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            AddSeans addSeans = new AddSeans(this.film);
            addSeans.ShowDialog();
        }
    }
}
