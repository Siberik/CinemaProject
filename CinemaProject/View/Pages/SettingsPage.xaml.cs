using CinemaProject.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CinemaProject.View.Pages
{
    public partial class SettingsPage : Page
    {
        private Core db = new Core();

        public SettingsPage()
        {
            InitializeComponent();
        }

        private void StatisticExcelClick(object sender, RoutedEventArgs e)
        {
            List<Tickets> tickets = db.context.Tickets.ToList();
            List<Seanses> seanses = db.context.Seanses.ToList();
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            // Создание нового Excel-пакета
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                // Создание нового листа
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Статистика");

                // Заголовки столбцов
                worksheet.Cells[1, 1].Value = "Фильм";
                worksheet.Cells[1, 2].Value = "Продано билетов";
                worksheet.Cells[1, 3].Value = "Продано билетов (всего)";

                // Заполнение данных
                int row = 2;
                foreach (Seanses seans in seanses)
                {
                    Films film = seans.Films; // Получаем фильм для текущего сеанса
                    int soldTickets = tickets.Count(t => t.SeansId == seans.SeansId && t.Seanses.SeansTime.Date == DateTime.Today);
                    worksheet.Cells[row, 1].Value = film.Name;
                    worksheet.Cells[row, 2].Value = soldTickets;
                    row++;
                }

                // Добавление общего количества проданных билетов
                int totalSoldTickets = tickets.Count(t => t.Seanses.SeansTime.Date == DateTime.Today);
                worksheet.Cells[row, 3].Value = totalSoldTickets;

                // Сохранение Excel-файла в "Загрузки" с использованием сегодняшней даты в названии файла
                string downloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
                string fileName = $"Статистика_{DateTime.Now.ToString("yyyy_MM_dd")}.xlsx";
                string filePath = Path.Combine(downloadsFolder, fileName);
                excelPackage.SaveAs(new FileInfo(filePath));
            }

            MessageBox.Show("Статистика успешно сохранена в Excel-файле.");
        }
    }
}
