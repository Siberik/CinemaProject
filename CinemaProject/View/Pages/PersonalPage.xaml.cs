using CinemaProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CinemaProject.View.Pages
{
    public partial class PersonalPage : Page
    {
        private Core db = new Core();
        private List<Administrators> administrators;
        private const int ItemsPerPage = 10;
        private int currentPage = 1;
        private int totalPages;

        public PersonalPage()
        {
            InitializeComponent();

            administrators = db.context.Administrators.ToList();
            totalPages = (int)Math.Ceiling(administrators.Count / (double)ItemsPerPage);

            DisplayAdministrators(currentPage);
        }

        private void DisplayAdministrators(int page)
        {
            PersonalStackPanel.Children.Clear();

            var administratorsPage = administrators
                .Skip((page - 1) * ItemsPerPage)
                .Take(ItemsPerPage);

            foreach (var administrator in administratorsPage)
            {
                var button = new Button
                {
                    Content = $"{administrator.Lastname} {administrator.Lastname} {administrator.Patronymic}",
                    Tag = administrator
                };

                button.Click += AdministratorButton_Click;
                button.Margin= new Thickness(0,10,0,0);
                button.Width =400;
                PersonalStackPanel.Children.Add(button);
            }
        }

        private void AdministratorButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var administrator = (Administrators)button.Tag;

            // Переход на другую страницу, например, Page2 с передачей данных
            PersonalInfoPage page2 = new PersonalInfoPage(administrator);
            NavigationService?.Navigate(page2);
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                DisplayAdministrators(currentPage);
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                DisplayAdministrators(currentPage);
            }
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            AddPersonalWindow add= new AddPersonalWindow();
            add.ShowDialog();
        }
    }
}
