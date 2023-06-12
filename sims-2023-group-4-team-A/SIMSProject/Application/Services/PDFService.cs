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

                PdfPTable table = statistics.First().Type == Domain.Models.AccommodationStatisticType.Monthly ? new(7) : new(6);
                table.AddCell("Godina");
                if (statistics.First().Type == Domain.Models.AccommodationStatisticType.Monthly) table.AddCell("Mesec");
                table.AddCell("Broj rezervacija");
                table.AddCell("Otkazane rezervacije");
                table.AddCell("Pomerene rezervacije");
                table.AddCell("Preporuke za renoviranje");
                table.AddCell("Najveća zauzetost");

                foreach (var statistic in statistics)
                {
                    table.AddCell(statistic.Year.ToString());
                    if (statistics.First().Type == Domain.Models.AccommodationStatisticType.Monthly) table.AddCell(statistic.ShortMonth);
                    table.AddCell(statistic.TotalReservations.ToString());
                    table.AddCell(statistic.CancelledReservations.ToString());
                    table.AddCell(statistic.RescheduledReservations.ToString());
                    table.AddCell(statistic.RenovationRecommendations.ToString());
                    table.AddCell(statistic.Best ? "DA." : string.Empty);
                }

                document.Add(table);
                document.Close();

                //MessageBox.Show("PDF file generated successfully.");

                if (File.Exists(filePath))
                {
                    ProcessStartInfo startInfo = new(filePath);
                    startInfo.UseShellExecute = true;
                    Process.Start(startInfo);
                }


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
                if (File.Exists(filePath))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo(filePath);
                    startInfo.UseShellExecute = true;
                    Process.Start(startInfo);
                }
                //MessageBox.Show("PDF file generated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating PDF file: " + ex.Message);
            }
        }

        public static bool GenerateAccommodationReservationDetailsPDF(AccommodationReservation reservation)
        {
            try
            {
                string filePath = OpenFilePicker();

                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                writer.SetPdfVersion(PdfWriter.PDF_VERSION_1_7);
                writer.SetFullCompression();

                document.Open();

                string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                string imagePath = $"{assemblyName}.Resources.Images.guest1logo.png"; // Putanja do slike unutar projekta

                // Učitajte sliku kao stream koristeći GetManifestResourceStream metodu
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(imagePath))
                {
                    if (stream != null)
                    {
                        iTextSharp.text.Image logoImage = iTextSharp.text.Image.GetInstance(stream);

                        float desiredHeight = 120f;
                        float aspectRatio = logoImage.Width / logoImage.Height;
                        float desiredWidth = desiredHeight * aspectRatio;

                        // Centrirajte sliku
                        logoImage.Alignment = iTextSharp.text.Image.ALIGN_CENTER;

                        // Postavite željene dimenzije slike
                        logoImage.ScaleAbsolute(desiredWidth, desiredHeight);

                        // Dodajte sliku na dokument
                        document.Add(logoImage);
                    }
                    else
                    {
                        MessageBox.Show("Error loading image.");
                    }
                }

                Paragraph heading = new Paragraph("Izveštaj o rezervaciji smeštaja", new Font(Font.FontFamily.HELVETICA, 20, Font.BOLDITALIC));
                heading.Alignment = Element.ALIGN_CENTER;
                heading.SpacingAfter = 15f;
                document.Add(heading);

                // Kreiranje tabele sa dve kolone
                PdfPTable table = new PdfPTable(2);
                table.SpacingBefore = 20f;
                table.SpacingAfter = 40;
                table.WidthPercentage = 100f;

                // Prva kolona - Detalji rezervacije
                PdfPCell reservationCell = new PdfPCell();
                reservationCell.Border = Rectangle.NO_BORDER;

                Paragraph reservationTitle = new Paragraph("Detalji rezervacije:", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD));
                reservationTitle.SpacingAfter = 9f;
                reservationCell.AddElement(reservationTitle);

                // Dodavanje detalja o rezervaciji u prvu kolonu
                Paragraph guestName = new Paragraph();
                guestName.Add(new Chunk("Ime gosta: ", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD)));
                guestName.Add(new Chunk(reservation.Guest.Username, new Font(Font.FontFamily.HELVETICA, 14, Font.ITALIC)));
                reservationCell.AddElement(guestName);
                guestName.SpacingAfter = 8f;

                Paragraph arrivalDate = new Paragraph();
                arrivalDate.Add(new Chunk("Datum dolaska: ", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD)));
                arrivalDate.Add(new Chunk(reservation.StartDate.ToString("dd.MM.yyyy."), new Font(Font.FontFamily.HELVETICA, 14, Font.ITALIC)));
                reservationCell.AddElement(arrivalDate);
                arrivalDate.SpacingAfter = 8f;

                Paragraph departureDate = new Paragraph();
                departureDate.Add(new Chunk("Datum odlaska: ", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD)));
                departureDate.Add(new Chunk(reservation.EndDate.ToString("dd.MM.yyyy."), new Font(Font.FontFamily.HELVETICA, 14, Font.ITALIC)));
                reservationCell.AddElement(departureDate);
                departureDate.SpacingAfter = 8f;

                Paragraph numberOfNights = new Paragraph();
                numberOfNights.Add(new Chunk("Broj dana: ", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD)));
                numberOfNights.Add(new Chunk(reservation.NumberOfDays.ToString(), new Font(Font.FontFamily.HELVETICA, 14, Font.ITALIC)));
                reservationCell.AddElement(numberOfNights);
                numberOfNights.SpacingAfter = 8f;

                Paragraph numberOfGuests = new Paragraph();
                numberOfGuests.Add(new Chunk("Broj gostiju: ", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD)));
                numberOfGuests.Add(new Chunk(reservation.GuestNumber.ToString(), new Font(Font.FontFamily.HELVETICA, 14, Font.ITALIC)));
                reservationCell.AddElement(numberOfGuests);
                numberOfGuests.SpacingAfter = 8f;

                // Druga kolona - Detalji smeštaja
                PdfPCell accommodationCell = new PdfPCell();
                accommodationCell.Border = Rectangle.NO_BORDER;

                Paragraph accommodationTitle = new Paragraph("Detalji smeštaja:", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD));
                accommodationTitle.SpacingAfter = 8f;
                accommodationCell.AddElement(accommodationTitle);

                // Dodavanje detalja o smeštaju u drugu kolonu
                Paragraph accommodationName = new Paragraph();
                accommodationName.Add(new Chunk("Naziv: ", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD)));
                accommodationName.Add(new Chunk(reservation.Accommodation.Name, new Font(Font.FontFamily.HELVETICA, 14, Font.ITALIC)));
                accommodationCell.AddElement(accommodationName);
                accommodationName.SpacingAfter = 8f;

                Paragraph accommodationLocation = new Paragraph();
                accommodationLocation.Add(new Chunk("Lokacija: ", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD)));
                accommodationLocation.Add(new Chunk(reservation.Accommodation.Location.ToString(), new Font(Font.FontFamily.HELVETICA, 14, Font.ITALIC)));
                accommodationCell.AddElement(accommodationLocation);
                accommodationLocation.SpacingAfter = 8f;

                // Dodavanje ćelija u tabelu
                table.AddCell(reservationCell);
                table.AddCell(accommodationCell);

                // Dodavanje tabele u dokument
                document.Add(table);

                // Dodavanje datuma u donji desni ugao
                DateTime currentDate = DateTime.Now;
                Paragraph dateParagraph = new Paragraph("Datum kreiranja izveštaja: ", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD));
                Chunk currentDateChunk = new Chunk(currentDate.ToString("dd.MM.yyyy."), new Font(Font.FontFamily.HELVETICA, 14, Font.ITALIC));
                dateParagraph.Add(currentDateChunk);
                dateParagraph.Alignment = Element.ALIGN_RIGHT;
                document.Add(dateParagraph);
                dateParagraph.SpacingBefore = 20;
                document.Close();
                if(File.Exists(filePath))
                {
                    ProcessStartInfo startinfo = new ProcessStartInfo(filePath);
                    startinfo.UseShellExecute = true;
                    Process.Start(startinfo);
                    
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating PDF file: " + ex.Message);
                return false;
            }



        }
    }
}
