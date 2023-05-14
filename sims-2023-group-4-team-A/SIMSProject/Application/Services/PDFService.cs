using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using SIMSProject.Domain.Models.AccommodationModels;

namespace SIMSProject.Application.Services
{
    public static class PDFService
    {

        private static string OpenFilePicker()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            saveFileDialog.DefaultExt = "pdf";
            if (saveFileDialog.ShowDialog() == true)
                return saveFileDialog.FileName;
            throw new Exception("Save file dialog returned error!");
        }

        public static void GenerateAccommodationStatsPDF(List<AccommodationStatistic> statistics)
        {
            try
            {
                string filePath = OpenFilePicker();

                Document document = new();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                writer.SetPdfVersion(PdfWriter.PDF_VERSION_1_7);
                writer.SetFullCompression();

                document.Open();

                Paragraph heading = new(
                    $"Statistika za smeštaj ({statistics.First().Accommodation.Id}) {statistics.First().Accommodation.Name}:", 
                    new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD));
                heading.SpacingAfter = 15f;
                document.Add(heading);

                PdfPTable table = statistics.First().Type == Domain.Models.AccommodationStatisticType.MONTHLY ? new(7) : new(6);
                table.AddCell("Godina");
                if (statistics.First().Type == Domain.Models.AccommodationStatisticType.MONTHLY) table.AddCell("Mesec");
                table.AddCell("Broj rezervacija");
                table.AddCell("Otkazane rezervacije");
                table.AddCell("Pomerene rezervacije");
                table.AddCell("Preporuke za renoviranje");
                table.AddCell("Najveća zauzetost");

                foreach (var statistic in statistics)
                {
                    table.AddCell(statistic.Year.ToString());
                    if (statistics.First().Type == Domain.Models.AccommodationStatisticType.MONTHLY) table.AddCell(statistic.ShortMonth);
                    table.AddCell(statistic.TotalReservations.ToString());
                    table.AddCell(statistic.CancelledReservations.ToString());
                    table.AddCell(statistic.RescheduledReservations.ToString());
                    table.AddCell(statistic.RenovationRecommendations.ToString());
                    table.AddCell(statistic.Best ? "DA." : string.Empty);
                }

                document.Add(table);
                document.Close();

                MessageBox.Show("PDF file generated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("PDF generation cancelled! " + ex.Message);
            }
        }
    }
}
