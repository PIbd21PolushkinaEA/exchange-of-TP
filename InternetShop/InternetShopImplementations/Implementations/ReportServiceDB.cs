using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;
using InternetShopServiceDAL.BindingModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;


namespace InternetShopImplementations.Implementations
{
    public class ReportServiceDB : IReportService
    {
        private AbstractDbContext context;

        public ReportServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }

        public ReportServiceDB()
        {
            this.context = new AbstractDbContext();
        }

        public List<ReportViewModel> GetRequests(ReportBindingModel model)
        {
            return context.RequestComponents
                .Include(rec => rec.Request)
                .Include(rec => rec.Component)
                .Where(rec => rec.Request.Date >= model.DateFrom && rec.Request.Date <= model.DateTo)
                .Select(rec => new ReportViewModel
                {
                    NameBuy = rec.Request.RequestName,
                    Date = SqlFunctions.DateName("dd", rec.Request.Date) + " " +
                           SqlFunctions.DateName("mm", rec.Request.Date) + " " +
                           SqlFunctions.DateName("yyyy", rec.Request.Date),
                    Name = "Admin",
                    ComponentName = rec.ComponentName,
                    ComponentCount = rec.CountComponents
                })
                .ToList();
        }
        public List<ReportViewModel> GetBaskets(ReportBindingModel model, int ClientId)
        {
            //return context.TreatmentPrescriptions
            //    .Include(rec => rec.Treatment)
            //    .Include(rec => rec.Treatment.Patient)
            //    .Include(rec => rec.Prescription)
            //    .Include(rec => rec.Prescription.PrescriptionMedications)
            //    .Where(rec => rec.Treatment.Date >= model.DateFrom && rec.Treatment.Date <= model.DateTo)
            //    .Select(rec => new ReportViewModel
            //    {
            //        Title = rec.Treatment.Title,
            //        Date = SqlFunctions.DateName("dd", rec.Treatment.Date) + " " +
            //               SqlFunctions.DateName("mm", rec.Treatment.Date) + " " +
            //               SqlFunctions.DateName("yyyy", rec.Treatment.Date),
            //        FIO = rec.Treatment.Patient.FIO,
            //        MedicationName = rec.Prescription.PrescriptionMedications.FirstOrDefault(recP => recP.PrescriptionId == rec.Prescription.Id).MedicationName,
            //        MedicationCount = rec.Count * rec.Prescription.PrescriptionMedications.FirstOrDefault(recP => recP.PrescriptionId == rec.Prescription.Id).CountMedications
            //    })
            //    .ToList();

            List<ReportViewModel> list = new List<ReportViewModel>();

            if (ClientId != -1)
            {
                foreach (var o in context.Baskets.Where(rec => rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo && rec.ClientId == ClientId))
                {
                    foreach (var p in context.ProductsBasket.Where(rec => rec.BasketId == o.Id))
                    {
                        int i = 0;
                        foreach (var med in context.ComponentsProduct.Where(rec => rec.ProductId == p.ProductId))
                        {
                            ReportViewModel rep = new ReportViewModel();
                            if (i < 1)
                            {
                                rep.Name = context.Clients.FirstOrDefault(rec => rec.Id == o.ClientId).Name;
                                rep.NameBuy = o.NameBuy;
                                rep.Date = o.DateCreate.ToShortDateString();
                            }
                            else
                            {
                                rep.Name = " ";
                                rep.NameBuy = " ";
                                rep.Date = " ";
                            }
                            rep.ComponentName = med.ComponentName;
                            rep.ComponentCount = med.Count * p.Count;
                            list.Add(rep);
                            i++;
                        }
                    }
                }
            }
            else
            {
                foreach (var o in context.Baskets.Where(rec => rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo))
                {
                    foreach (var p in context.ProductsBasket.Where(rec => rec.BasketId == o.Id))
                    {
                        int i = 0;
                        foreach (var med in context.ComponentsProduct.Where(rec => rec.ProductId == p.ProductId))
                        {
                            ReportViewModel rep = new ReportViewModel();
                            if (i < 1)
                            {
                                rep.Name = context.Clients.FirstOrDefault(rec => rec.Id == o.ClientId).Name;
                                rep.NameBuy = o.NameBuy;
                                rep.Date = o.DateCreate.ToShortDateString();
                            }
                            else
                            {
                                rep.Name = " ";
                                rep.NameBuy = " ";
                                rep.Date = " ";
                            }
                            rep.ComponentName = med.ComponentName;
                            rep.ComponentCount = med.Count * p.Count;
                            list.Add(rep);
                            i++;
                        }
                    }
                }
            }
            return list;
        }

        public void SaveBaskets(ReportBindingModel model, int ClientId)
        {
            FileStream fs = new FileStream(model.FileName, FileMode.OpenOrCreate, FileAccess.Write);

            //создаем документ, задаем границы, связываем документ и поток
            iTextSharp.text.Document doc = new iTextSharp.text.Document();

            try
            {
                //открываем файл для работы                
                doc.SetMargins(0.5f, 0.5f, 0.5f, 0.5f);
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);

                doc.Open();
                BaseFont baseFont = BaseFont.CreateFont("C:\\Users\\Евгения\\Desktop\\TIMCYR.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                //вставляем заголовок
                var phraseTitle = new Phrase("Отчет",
                new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD));
                iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph(phraseTitle)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 12
                };
                doc.Add(paragraph);

                var phrasePeriod = new Phrase("c " + model.DateFrom.Value.ToShortDateString() +
                                              " по " + model.DateTo.Value.ToShortDateString(),
                                              new iTextSharp.text.Font(baseFont, 14, iTextSharp.text.Font.BOLD));

                paragraph = new iTextSharp.text.Paragraph(phrasePeriod)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 12
                };
                doc.Add(paragraph);

                //вставляем таблицу, задаем количество столбцов, и ширину колонок
                PdfPTable table = new PdfPTable(5)
                {
                    TotalWidth = 800F
                };
                table.SetTotalWidth(new float[] { 160, 100, 120, 180, 120 });

                //вставляем шапку
                PdfPCell cell = new PdfPCell();
                var fontForCellBold = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.BOLD);
                table.AddCell(new PdfPCell(new Phrase("Название", fontForCellBold))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                table.AddCell(new PdfPCell(new Phrase("Дата", fontForCellBold))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                table.AddCell(new PdfPCell(new Phrase("Имя", fontForCellBold))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                table.AddCell(new PdfPCell(new Phrase("Компонент", fontForCellBold))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                table.AddCell(new PdfPCell(new Phrase("Количество", fontForCellBold))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });

                //заполняем таблицу
                var list = GetBaskets(model, ClientId);
                var fontForCells = new iTextSharp.text.Font(baseFont, 10);

                foreach (var el in list)
                {
                    cell = new PdfPCell(new Phrase(el.NameBuy, fontForCells));
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(el.Date, fontForCells));
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(el.Name, fontForCells));
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(el.ComponentName, fontForCells));
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(el.ComponentCount.ToString(), fontForCells));
                    table.AddCell(cell);
                }

                doc.Add(table);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                doc.Close();
                Thread.Sleep(5);
            }
        }
        public void SaveLoad(ReportBindingModel model, int ClientId)
        {
            var excel = new Microsoft.Office.Interop.Excel.Application();
            try
            {
                if (File.Exists(model.FileName))
                {
                    excel.Workbooks.Open(model.FileName, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing);
                }
                else
                {
                    excel.SheetsInNewWorkbook = 1;
                    excel.Workbooks.Add(Type.Missing);
                    excel.Workbooks[1].SaveAs(model.FileName, XlFileFormat.xlExcel8, Type.Missing,
                        Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                Sheets excelsheets = excel.Workbooks[1].Worksheets;
                var excelworksheet = (Worksheet)excelsheets.get_Item(1);
                excelworksheet.Cells.Clear();
                excelworksheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                excelworksheet.PageSetup.CenterHorizontally = true;
                excelworksheet.PageSetup.CenterVertically = true;
                Microsoft.Office.Interop.Excel.Range excelcells = excelworksheet.get_Range("A1", "E1");
                excelcells.Merge(Type.Missing);
                excelcells.Font.Bold = true;
                excelcells.Value2 = "Отчет";
                excelcells.RowHeight = 25;
                excelcells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                excelcells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                excelcells.Font.Name = "Times New Roman";
                excelcells.Font.Size = 14;

                excelcells = excelworksheet.get_Range("A2", "E2");
                excelcells.Merge(Type.Missing);
                excelcells.Value2 = DateTime.Now.ToShortDateString();
                excelcells.RowHeight = 20;
                excelcells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                excelcells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                excelcells.Font.Name = "Times New Roman";
                excelcells.Font.Size = 12;

                excelcells = excelworksheet.get_Range("A3", "A3");
                excelcells.Interior.Color = Color.Yellow;
                excelcells.Font.Bold = true;
                excelcells.ColumnWidth = 15;
                excelcells.Value2 = "Название";
                excelcells = excelcells.get_Offset(0, 1);
                excelcells.Interior.Color = Color.Yellow;
                excelcells.Font.Bold = true;
                excelcells.ColumnWidth = 15;
                excelcells.Value2 = "Дата";
                excelcells = excelcells.get_Offset(0, 1);
                excelcells.Interior.Color = Color.Yellow;
                excelcells.Font.Bold = true;
                excelcells.ColumnWidth = 15;
                excelcells.Value2 = "Имя";
                excelcells = excelcells.get_Offset(0, 1);
                excelcells.Interior.Color = Color.Yellow;
                excelcells.Font.Bold = true;
                excelcells.ColumnWidth = 15;
                excelcells.Value2 = "Компонент";
                excelcells = excelcells.get_Offset(0, 1);
                excelcells.Interior.Color = Color.Yellow;
                excelcells.Font.Bold = true;
                excelcells.ColumnWidth = 15;
                excelcells.Value2 = "Количество";
                excelcells = excelworksheet.get_Range("A4", "A4");

                List<ReportViewModel> list = new List<ReportViewModel>();

                if (model.FileName.Contains("Client"))
                {
                    list = GetBaskets(model, ClientId);
                }
                else
                {
                    list = GetRequests(model);
                }

                if (list != null)
                {
                    foreach (var el in list)
                    {
                        excelcells.Value2 = el.NameBuy;
                        excelcells = excelcells.get_Offset(0, 1);

                        excelcells.Value2 = el.Date;
                        excelcells = excelcells.get_Offset(0, 1);

                        excelcells.Value2 = el.Name;
                        excelcells = excelcells.get_Offset(0, 1);

                        excelcells.Value2 = el.ComponentName;
                        excelcells = excelcells.get_Offset(0, 1);

                        excelcells.Value2 = el.ComponentCount;
                        excelcells = excelcells.get_Offset(1, -4);
                    }
                }
                excel.Workbooks[1].Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                excel.Quit();
                Thread.Sleep(5);
            }

        }
    }
}
