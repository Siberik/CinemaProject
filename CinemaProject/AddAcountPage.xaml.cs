//Theme Code ==================>>
using CinemaProject.Model;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using PopupApp;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
//=============================>>
using System.Windows;
using System.Windows.Input;

namespace CinemaProject
{
    /// <summary>
    /// Логика взаимодействия для AddAcountPage.xaml
    /// </summary>
    public partial class AddAcountPage : Window
    {
        Core db = new Core();
        public AddAcountPage()
        {
            ITheme theme = paletteHelper.GetTheme();
            InitializeComponent();
            theme.SetBaseTheme(MaterialDesignThemes.Wpf.Theme.Dark);
            paletteHelper.SetTheme(theme);
        }
        //Theme Code ========================>
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();
        //===================================>

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            //Theme Code ========================>
            ITheme theme = paletteHelper.GetTheme();
            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(MaterialDesignThemes.Wpf.Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(MaterialDesignThemes.Wpf.Theme.Dark);
            }

            paletteHelper.SetTheme(theme);
            //===================================>
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }



        private void loginBtnClick(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password != null && txtUsername.Text != null) {
                Users users = new Users()
                {
                    login=txtUsername.Text,
                    password = txtPassword.Password,
                };
                db.context.Users.Add(users);
                db.context.SaveChanges();
                MessageBox.Show("Вы успешно зарегистрированы!");
                MainWindow mainWindow = new MainWindow(users);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Заполните все данные");
            }
        }

    }
}
