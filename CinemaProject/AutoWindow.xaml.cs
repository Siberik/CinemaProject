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

namespace WPF_login
{
  
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AutoWindow : Window
    {
        Core db = new Core();
        public AutoWindow()
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
            Users user;
        if(db.context.Users.Where(x=>x.login== txtUsername.Text) != null)
            {
               user= db.context.Users.FirstOrDefault(x=>x.login == txtUsername.Text);
                if (db.context.Users.Where(x => x.password == user.password) != null) {
                    MessageBox.Show("Вы авторизовались как пользователь");
                    MainWindow mainWindow = new MainWindow(user);
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Проверьте правильность введённого пароля");
                }
            }
        else if(db.context.Administrators.Where(x=>x.login==txtUsername.Text) != null)
            {
                Administrators admin= db.context.Administrators.FirstOrDefault(x=>x.login==txtUsername.Text);
                if(db.context.Administrators.Where(x=>x.password==admin.password) != null)
                {
                    MessageBox.Show("Вы авторизовались как администратор");
                    MainWindow mainWindow = new MainWindow(null,admin);
                    mainWindow.Show();
                    this.Close();
                }

            }
        }
        

       
    }
}
