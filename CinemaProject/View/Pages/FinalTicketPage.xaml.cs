using CinemaProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using OfficeWord = Microsoft.Office.Interop.Word;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace CinemaProject.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для FinalTicketPage.xaml
    /// </summary>
    public partial class FinalTicketPage : Page
    {
        private Core db = new Core();
        private List<Tickets> tickets;

        public FinalTicketPage(List<Tickets> tickets)
        {
            InitializeComponent();
            this.tickets = tickets;
        }

        private void WordClick(object sender, RoutedEventArgs e)
        {
            string fileNameBase = "Билет на KinoHub";
            string fileExtension = ".docx";
            string documentsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Assets\\Documents\\");

            string downloadsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            downloadsDirectory = Path.Combine(downloadsDirectory, "Downloads");

            foreach (Tickets ticket in tickets)
            {
                string fileName = $"{fileNameBase}_{ticket.TicketId}{fileExtension}";
                string docxFilePath = Path.Combine(downloadsDirectory, fileName);
                string pdfFilePath = Path.ChangeExtension(docxFilePath, ".pdf");

                OfficeWord.Application wordApp = new OfficeWord.Application();
                wordApp.Visible = false;

                string templateFilePath = Path.Combine(documentsDirectory, fileNameBase + fileExtension);
                OfficeWord.Document doc = wordApp.Documents.Open(templateFilePath);

                var date = db.context.Seanses.Where(x => x.SeansId == ticket.SeansId).Select(x => x.SeansTime).FirstOrDefault();
                var seanses = db.context.Seanses.FirstOrDefault(x => x.SeansId == ticket.SeansId);
                var hall = db.context.Hall.Where(x => x.Id == seanses.Hall_Id_FK).Select(x => x.Id).FirstOrDefault();
                var mesta = $"Ряд {ticket.Row} Место {ticket.Columns}";
                doc.Bookmarks["Дата"].Range.Text = $"{date}";
                doc.Bookmarks["Зал"].Range.Text = $"{hall}";
                doc.Bookmarks["Места"].Range.Text = $"{mesta}";

                doc.SaveAs2(docxFilePath, OfficeWord.WdSaveFormat.wdFormatDocumentDefault);
                doc.Close();
                wordApp.Quit();

                
            }

            MessageBox.Show("Билеты успешно загружены. Проверьте папку 'Загрузки'.");
        }

        private void PDFClick(object sender, RoutedEventArgs e)
        {
            string fileNameBase = "Билет на KinoHub";
            string fileExtension = ".docx";
            string documentsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Assets\\Documents\\");

            string downloadsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            downloadsDirectory = Path.Combine(downloadsDirectory, "Downloads");

            foreach (Tickets ticket in tickets)
            {
                string fileName = $"{fileNameBase}_{ticket.TicketId}{fileExtension}";
                string docxFilePath = Path.Combine(downloadsDirectory, fileName);
                string pdfFilePath = Path.ChangeExtension(docxFilePath, ".pdf");

                OfficeWord.Application wordApp = new OfficeWord.Application();
                wordApp.Visible = false;

                string templateFilePath = Path.Combine(documentsDirectory, fileNameBase + fileExtension);
                OfficeWord.Document doc = wordApp.Documents.Open(templateFilePath);

                var date = db.context.Seanses.Where(x => x.SeansId == ticket.SeansId).Select(x => x.SeansTime).FirstOrDefault();
                var seanses = db.context.Seanses.FirstOrDefault(x => x.SeansId == ticket.SeansId);
                var hall = db.context.Hall.Where(x => x.Id == seanses.Hall_Id_FK).Select(x => x.Id).FirstOrDefault();
                var mesta = $"Ряд {ticket.Row} Место {ticket.Columns}";
                doc.Bookmarks["Дата"].Range.Text = $"{date}";
                doc.Bookmarks["Зал"].Range.Text = $"{hall}";
                doc.Bookmarks["Места"].Range.Text = $"{mesta}";

                doc.SaveAs2(docxFilePath, OfficeWord.WdSaveFormat.wdFormatDocumentDefault);
                doc.Close();
                wordApp.Quit();

                ConvertToPdf(docxFilePath, pdfFilePath);
            }

            MessageBox.Show("Билеты успешно загружены. Проверьте папку 'Загрузки'.");
        }


        private void ConvertToPdf(string docxFilePath, string pdfFilePath)
        {
            OfficeWord.Application wordApp = new OfficeWord.Application();
            wordApp.Visible = false;

            OfficeWord.Document doc = wordApp.Documents.Open(docxFilePath);
            doc.SaveAs2(pdfFilePath, OfficeWord.WdSaveFormat.wdFormatPDF);
            doc.Close();
            wordApp.Quit();
        }




    }

}

