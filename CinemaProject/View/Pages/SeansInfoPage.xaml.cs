using CinemaProject.Model;
using FirstFloor.ModernUI.Presentation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CinemaProject.View.Pages
{
    public partial class SeansInfoPage : Page, INotifyPropertyChanged
    {
        private Core db = new Core();
        private Seanses seanses;
        private Users user;
        private ObservableCollection<SeatItem> seatItems;

        public ObservableCollection<SeatItem> SeatItems
        {
            get { return seatItems; }
            set
            {
                seatItems = value;
                OnPropertyChanged(nameof(SeatItems));
            }
        }

        public ICommand ToggleSeatCommand { get; }

        public SeansInfoPage(Seanses seanses, Users user = null)
        {
            this.user = user;
            this.seanses = seanses;
            InitializeComponent();
            Hall hall = db.context.Hall.FirstOrDefault(x => x.Id == seanses.Hall_Id_FK);
            int rows = 6;
            int columns = 10;

            SeatItems = new ObservableCollection<SeatItem>();
            for (int row = 1; row <= rows; row++)
            {
                for (int column = 1; column <= columns; column++)
                {
                    SeatItems.Add(new SeatItem(row, column));
                }
            }

            ToggleSeatCommand = new RelayCommand(ExecuteToggleSeatCommand);
            DataContext = this;
        }

        private void ExecuteToggleSeatCommand(object parameter)
        {
            if (parameter is SeatItem seatItem)
            {
                seatItem.IsSelected = !seatItem.IsSelected;
            }
        }

        private void BuyTicketsButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedSeats = SeatItems.Where(s => s.IsSelected).Take(5).ToList();
            if (selectedSeats.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите места для покупки билетов.");
                return;
            }

            if (selectedSeats.Count > 5)
            {
                MessageBox.Show("Вы можете купить не более 5 билетов за один раз.");
                return;
            }

            foreach (var seat in selectedSeats)
            {
                Tickets ticket = new Tickets
                {
                    SeansId = seanses.SeansId,
                    Row = seat.Row,
                    Columns = seat.Column,
                    Users_Id_FK = user.Id,
                    Tariff_Id_FK = 1 // Здесь нужно указать соответствующий идентификатор тарифа
                };

                db.context.Tickets.Add(ticket);
            }

            db.context.SaveChanges();

            MessageBox.Show("Билеты куплены.");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class SeatItem : INotifyPropertyChanged
    {
        private bool isSelected;
        private Brush seatColor;

        public int Row { get; set; }
        public int Column { get; set; }
        public int SeatNumber { get; set; }

        public Brush SeatColor
        {
            get { return seatColor; }
            set
            {
                if (seatColor != value)
                {
                    seatColor = value;
                    OnPropertyChanged(nameof(SeatColor));
                }
            }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                    UpdateSeatColor();
                }
            }
        }

        public SeatItem(int row, int column)
        {
            Row = row;
            Column = column;
            SeatNumber = (row - 1) * 10 + column;
            IsSelected = false;
            UpdateSeatColor();
        }

        private void UpdateSeatColor()
        {
            SeatColor = IsSelected ? Brushes.Green : Brushes.White;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
