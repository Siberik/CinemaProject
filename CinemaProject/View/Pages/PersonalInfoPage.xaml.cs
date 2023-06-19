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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CinemaProject.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для PersonalInfoPage.xaml
    /// </summary>
    public partial class PersonalInfoPage : Page
    {
        Core db=new Core();
        private Administrators admin;
        public PersonalInfoPage(Administrators admin)
        {
            this.admin = admin;
         var adminRole=   db.context.AdministratoRole.FirstOrDefault(x => x.Id == admin.AdministratoRole.Id);
            InitializeComponent();
            txtLastName.Text += admin.Lastname;
            txtFirstName.Text += admin.Fitstname;
            txtPatronymic.Text += admin.Patronymic;
            txtPassport.Text += admin.Passport;
            txtPhone.Text += admin.TelephonNum;
            txtRole.Text += adminRole.Post;
            txtRequirements.Text += adminRole.Requirements;
            txtResponsibilities.Text += adminRole.Responsibilities;
            dateBirthDate.Text += admin.Date;
            txtAddress.Text += admin.Address;
            txtSalary.Text=admin.Oklad.ToString();
        }

        private void RedactButtonClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PersonalRedactPage(admin));
        }

        private void DaeleteButtonClick(object sender, RoutedEventArgs e)
        {
           
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этого сотрудника?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {

                // Находим фильм в базе данных
                Administrators administrators = db.context.Administrators.FirstOrDefault(f => f.Id == admin.Id);

                if (administrators != null)
                {

                    // Удаление фильма
                    db.context.Administrators.Remove(administrators);
                    db.context.SaveChanges();

                    MessageBox.Show("Сотрудник успешно удален.");

                    // Перенаправление на предыдущую страницу
                    NavigationService?.GoBack();
                }
                else
                {
                    MessageBox.Show("Сотрудник не найден.");
                }
            }
        }


    }
}
