using CinemaProject.Model;
using CinemaProjectLibrary;
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
    /// Логика взаимодействия для AddPersonalWindow.xaml
    /// </summary>
    public partial class AddPersonalWindow : Window
    {
        Core db=new Core();
        public AddPersonalWindow()
        {
            InitializeComponent();
            var admRoleList = db.context.AdministratoRole.Select(x => x.Post).ToList();   
            cmbRoles.ItemsSource=admRoleList;
        }

        private void AddPersonalButtonClick(object sender, RoutedEventArgs e)
        {
            // Проверяем, что все поля заполнены
            if (string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtPatronymic.Text) ||
                datePickerBirthDate.SelectedDate == null ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtPassport.Text) ||
                string.IsNullOrWhiteSpace(txtSalary.Text)||cmbRoles.SelectedItem==null)
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            string login = LoginAndPasswordGeneratorClass.GenerateRandomLogin(4);
            string password = LoginAndPasswordGeneratorClass.GenerateRandomPassword(4);
            Administrators admin = new Administrators{
                Lastname = txtLastName.Text,
                Fitstname = txtFirstName.Text,
                Patronymic = txtPatronymic.Text,
                Passport = txtPassport.Text,
                Date=(DateTime)datePickerBirthDate.SelectedDate,
                Address= txtAddress.Text,
                AdministratorRole_FK=cmbRoles.SelectedIndex+1,
                login= login,
                password=password,
                Oklad= Int32.Parse(txtSalary.Text),
                TelephonNum= txtPhone.Text,
                
            };
            db.context.Administrators.Add(admin);
            db.context.SaveChanges();
            MessageBox.Show($"Новый сотрудник успешно создан. Сохраните следующие данные от аккаунта:\nЛогин: {login}\nПароль: {password}");
         Close();
        }

    }
}
