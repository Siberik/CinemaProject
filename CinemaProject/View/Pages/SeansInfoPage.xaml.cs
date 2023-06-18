using CinemaProject.Model;
using FirstFloor.ModernUI.Presentation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CinemaProject.View.Pages;
using System.Collections.Generic;

namespace CinemaProject.View.Pages
{
    public partial class SeansInfoPage : Page, INotifyPropertyChanged
    {
        private ObservableCollection<SeatItem> seatItems;
        private ObservableCollection<string> selectedSeats;

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
        Core db = new Core();
        private Seanses seans;
        private Users user;
        public SeansInfoPage(Seanses seans, Users user)
        {
            InitializeComponent();
            this.seans = seans;
            this.user = user;

            int rows = 6;
            int columns = 10;

            SeatItems = new ObservableCollection<SeatItem>();
            selectedSeats = new ObservableCollection<string>();

            // Получение информации о купленных билетах для данного сеанса
            var boughtTickets = db.context.Tickets.Where(t => t.SeansId == seans.SeansId).ToList();

            for (int row = 1; row <= rows; row++)
            {
                for (int column = 1; column <= columns; column++)
                {
                    SeatItem seatItem = new SeatItem(row, column);

                    // Проверка, является ли место уже купленным
                    var boughtTicket = boughtTickets.FirstOrDefault(t => t.Row == row && t.Columns == column);
                    if (boughtTicket != null)
                    {
                        seatItem.SeatColor = Brushes.Red;
                        seatItem.IsSelectable = false; // Устанавливаем флаг, что место нельзя выбрать
                    }

                    SeatItems.Add(seatItem);
                }
            }

            ToggleSeatCommand = new RelayCommand(ExecuteToggleSeatCommand);
            DataContext = this;
        }

        private void ExecuteToggleSeatCommand(object parameter)
        {
            if (parameter is SeatItem seatItem)
            {
                if (!seatItem.IsSelectable)
                {
                    MessageBox.Show("Это место уже куплено.");
                    return;
                }

                if (seatItem.IsSelected)
                {
                    seatItem.IsSelected = false;
                    selectedSeats.Remove(seatItem.SeatName);
                }
                else
                {
                    if (selectedSeats.Count < 5)
                    {
                        seatItem.IsSelected = true;
                        selectedSeats.Add(seatItem.SeatName);
                    }
                    else
                    {
                        MessageBox.Show("Вы можете выбрать только до 5 мест.");
                    }
                }
            }
        }

        private void BuyTicketButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSeats.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите место для покупки билета.");
                return;
            }

            int totalTicketsCount = db.context.Tickets.Count(t => t.SeansId == seans.SeansId && t.Users_Id_FK == user.Id);

            if (selectedSeats.Count + totalTicketsCount > 5)
            {
                MessageBox.Show("Вы не можете купить больше 5 билетов на данный сеанс.");
                return;
            }

            List<Tickets> boughtTickets = new List<Tickets>();

            foreach (string seat in selectedSeats)
            {
                string[] seatParts = seat.Split('_');
                int row = int.Parse(seatParts[0].Substring(6));
                int column = int.Parse(seatParts[1]);
                SeatItem seatItem = SeatItems.FirstOrDefault(s => s.SeatName == seat);
                if (seatItem != null)
                {
                    seatItem.SeatColor = Brushes.Red; // Изменение цвета места после покупки
                    seatItem.IsSelectable = false; // Место больше нельзя выбрать
                }

                // Здесь можно добавить логику для создания билета и его сохранения в базе данных
                Tickets ticket = new Tickets()
                {
                    Users_Id_FK = user.Id,
                    SeansId = seans.SeansId,
                    Row = row,
                    Columns = column,
                };
                db.context.Tickets.Add(ticket);

                // Добавляем купленный билет в список
                boughtTickets.Add(ticket);
            }

            db.context.SaveChanges();

            MessageBox.Show("Билеты куплены.");

            //// Открываем новую страницу с передачей списка только что купленных билетов
            FinalTicketPage finalTicketPage = new FinalTicketPage(boughtTickets);
            NavigationService.Navigate(finalTicketPage);

            selectedSeats.Clear();
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
        private bool isSelectable;

        public int Row { get; set; }
        public int Column { get; set; }
        public int SeatNumber { get; set; }
        public string SeatName => $"Button{Row}_{Column}";

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

        public bool IsSelectable
        {
            get { return isSelectable; }
            set
            {
                if (isSelectable != value)
                {
                    isSelectable = value;
                    OnPropertyChanged(nameof(IsSelectable));
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
            IsSelectable = true;
            UpdateSeatColor();
        }

        private void UpdateSeatColor()
        {
            if (!IsSelectable)
            {
                SeatColor = Brushes.Red;
            }
            else if (IsSelected)
            {
                SeatColor = Brushes.Green;
            }
            else
            {
                SeatColor = Brushes.White;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
