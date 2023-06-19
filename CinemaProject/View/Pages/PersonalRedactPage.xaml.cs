using CinemaProject.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для PersonaRedactPage.xaml
    /// </summary>
    public partial class PersonalRedactPage : Page
    {
        Core db = new Core();
        private Administrators admin;
        public PersonalRedactPage(Administrators admin)
        {
            this.admin = admin;
            InitializeComponent();
            var admRoleList = db.context.AdministratoRole.Select(x => x.Post).ToList();
            cmbRoles.ItemsSource = admRoleList;
            cmbRoles.SelectedIndex = admin.AdministratorRole_FK-1;

            txtLastName.Text = admin.Lastname;
            txtAddress.Text = admin.Address;
            txtFirstName.Text = admin.Fitstname;
            txtPassport.Text = admin.Passport;
            txtPatronymic.Text = admin.Patronymic;
            txtPhone.Text = admin.TelephonNum;
            txtSalary.Text = admin.Oklad.ToString();
            datePickerBirthDate.SelectedDate=admin.Date;
            
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
                string.IsNullOrWhiteSpace(txtSalary.Text) || cmbRoles.SelectedItem == null)
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            admin.Lastname= txtLastName.Text;
            admin.AdministratorRole_FK = cmbRoles.SelectedIndex + 1;
            admin.Fitstname=txtFirstName.Text;
            admin.Address=txtAddress.Text;
            admin.Date =(DateTime) datePickerBirthDate.SelectedDate;
            admin.TelephonNum=txtPhone.Text;
            admin.Patronymic = txtPatronymic.Text;
            admin.Oklad = Int32.Parse(txtSalary.Text);
            admin.Passport = txtPassport.Text;
            db.context.Administrators.AddOrUpdate(admin);
            db.context.SaveChanges();
            MessageBox.Show("Сохранено.");
        }
    }
}
