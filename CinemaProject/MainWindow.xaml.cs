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
using System.Windows.Controls.Primitives;
using CinemaProject.View.Pages;
using MaterialDesignThemes.Wpf;
using CinemaProject.Model;

namespace PopupApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Core db = new Core();
        private Users user;
        private Administrators admin;
        private readonly PaletteHelper paletteHelper = new PaletteHelper();
        public MainWindow(Users user=null,Administrators admin=null)
        {
            this.user = user;
            this.admin = admin;
            InitializeComponent();
            if(user != null)
            {
                Profile.Visibility = Visibility.Collapsed;
            }
            MainFrame.NavigationService.Navigate(new MainPage(user,admin));
            ITheme theme = paletteHelper.GetTheme();
            theme.SetBaseTheme(Theme.Light);
            paletteHelper.SetTheme(theme);
        }

        private void Home_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = Home;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Главная страница";
        }

        private void Home_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void Profile_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = Profile;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Сотрудники";
        }

        private void Profile_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void Settings_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = Settings;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Управление";
        }

        private void Settings_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HomeClick(object sender, RoutedEventArgs e)
        {
           
            MainFrame.NavigationService.Navigate(new MainPage(user,admin));
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            DragMove();
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PersonalPage());
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SettingsPage());
        }
    }
}
