using CinemaProject.Model;
using CinemaProjectLibrary;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using static System.Net.WebRequestMethods;

namespace CinemaProject.View.Pages
{
    public partial class AddFilm : Window
    {
        private Core db = new Core();
        private byte[] imageData;
        HourAndDateChekingClass check = new HourAndDateChekingClass();
        public AddFilm()
        {
            InitializeComponent();
        }

        private void LoadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.png, *.jpeg, *.gif)|*.jpg;*.png;*.jpeg;*.gif";

            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                ImageNameTextBlock.Text = Path.GetFileName(imagePath);

                // Чтение содержимого файла в виде массива байтов
                imageData = System.IO.File.ReadAllBytes(imagePath);
            }
        }

        private void AddFilm_Click(object sender, RoutedEventArgs e)
        {
            // Получение значений полей ввода из интерфейса
            string filmName = FilmNameTextBox.Text;
            string duration = DurationTextBox.Text;
            string genre = GenreTextBox.Text;
            string genreDescription = GenreDescriptionTextBox.Text;
            string firm = FirmTextBox.Text;
            string country = CountryTextBox.Text;
            string actors = ActorsTextBox.Text;
            string vozrast = VozrastTextBox.Text;
            if (check.IsValidTimeFormat(duration))
            {
                // Создание нового экземпляра фильма
                Films film = new Films
            {
                Name = filmName,
                Duration = TimeSpan.Parse(duration),
                Genre = genre,
                GenreDescription = genreDescription,
                Firm = firm,
                Country = country,
                Actors = actors,
                Vozrast = vozrast,
                Image = imageData
            };

            // Добавление фильма в базу данных
            db.context.Films.Add(film);
            db.context.SaveChanges();

            MessageBox.Show("Фильм успешно добавлен!");

            // Закрытие окна
            this.Close();
            }
            else
            {
                MessageBox.Show("Неправильно введена продолжительность");
            }
          
         
        }
    }
}
