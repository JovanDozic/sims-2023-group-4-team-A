using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.TourModels;

namespace SIMSProject.Application.Services
{
    public static class PDFService
    {
        private static string OpenFilePicker()
        {
            SaveFileDialog saveFileDialog = new();
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
                    new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD))
                {
                    SpacingAfter = 15f
                };
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
                MessageBox.Show("Error generating PDF file: " + ex.Message);
            }
        }

        public static void GenerateTourReservationDetailsPDF(TourReservation tourReservation)
        {
            try
            {
                string filePath = OpenFilePicker();

                Document document = new();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                writer.SetPdfVersion(PdfWriter.PDF_VERSION_1_7);
                writer.SetFullCompression();

                document.Open();

                string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                string imagePath = $"{assemblyName}.Resources.Images.tripbooklogoPDF.png"; // Putanja do slike unutar projekta

                // Učitajte sliku kao stream koristeći GetManifestResourceStream metodu
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(imagePath))
                {
                    if (stream != null)
                    {
                        iTextSharp.text.Image logoImage = iTextSharp.text.Image.GetInstance(stream);

                        float desiredHeight = 150f;
                        float aspectRatio = logoImage.Width / logoImage.Height;
                        float desiredWidth = desiredHeight * aspectRatio;
                       
                        PdfPTable table = new PdfPTable(2);
                        table.DefaultCell.Border = Rectangle.NO_BORDER;

                        PdfPCell imageCell = new PdfPCell(logoImage);
                        imageCell.FixedHeight = desiredHeight;
                        imageCell.Border = Rectangle.NO_BORDER;
                        table.AddCell(imageCell);

                        PdfPCell textCell = new PdfPCell(new Phrase("TripBook"));
                        textCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        textCell.Border = Rectangle.NO_BORDER;

                        BaseFont baseFont = BaseFont.CreateFont("Helvetica-Bold", "Cp1252", false);
                        Font font = new Font(baseFont, 30f);
                        //font.Color = BaseColor.ORANGE; // Narandžasta boja

                        Chunk chunk = new Chunk("TripBook", font);
                        Phrase phrase = new Phrase(chunk);
                        textCell.AddElement(phrase);

                        table.AddCell(textCell);

                        document.Add(table);
                    }
                    else
                    {
                        MessageBox.Show("Error loading image.");
                    }
                }

                Paragraph heading = new(
                    $"Izveštaj o rezervaciji ture: {tourReservation.TourAppointment.Tour.Name} \n\n",
                    new Font(Font.FontFamily.HELVETICA, 20, Font.BOLDITALIC))
                {
                    SpacingAfter = 15f
                };
                document.Add(heading);

                Paragraph details = new(
                    $"Vaš termin pocinje {tourReservation.TourAppointment.Date:dd.MM.yyyy.} u {tourReservation.TourAppointment.Date:HH.mm}h i traje {tourReservation.TourAppointment.Tour.Duration}h.\n\n" +
                    $"Vaša rezervacija važi za sledeci broj gostiju: {tourReservation.GuestNumber} \n\n",
                    new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD))
                {
                    SpacingAfter = 15f
                };
                document.Add(details);

                Paragraph footer = new(
                    $"Hvala na ukazanom poverenju,\n" +
                    $"Vaš TripBook.",
                    new Font(Font.FontFamily.HELVETICA, 20, Font.BOLDITALIC))
                {
                    SpacingAfter = 15f
                };
                footer.Alignment = Element.ALIGN_RIGHT;
                document.Add(footer);

                //document.Add(table);
                document.Close();
                //MessageBox.Show("PDF file generated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating PDF file: " + ex.Message);
            }
        }
    }
}
