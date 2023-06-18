using CinemaProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CinemaProject.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddSeans.xaml
    /// </summary>
    public partial class AddSeans : Window
    {
        Core db= new Core();
        private Films film;
        public AddSeans(Films film)
        {
            this.film = film;
            InitializeComponent();
            var hallList= db.context.Hall.Select(x=>x.Id ).ToList();
            HallComboBox.ItemsSource = hallList;
        }

      

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
          

            if (DateCalendar.SelectedDate != null&& HallComboBox.SelectedValue!=null&& TimeClock.Time!=null)
            {
                var time = TimeClock.Time;
                var date = DateCalendar.SelectedDate;
                DateTime combinedDateTime = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, time.Hour, time.Minute, time.Second);

                // Используйте combinedDateTime для нужных дальнейших действий


                Seanses seanses = new Seanses()
                {
                    SeansTime = combinedDateTime,
                    FilmId = film.Id,
                    Hall_Id_FK = HallComboBox.SelectedIndex + 1,
                };
                db.context.Seanses.Add(seanses);
                if(db.context.SaveChanges()!=0)
                {
                    MessageBox.Show("Добавление успешно сделано!");
                    Close();
                }

            }
            else
            {
                MessageBox.Show("Проверьте, все ли значения введены и попробуйте снова");
            }
        }
    }
}
