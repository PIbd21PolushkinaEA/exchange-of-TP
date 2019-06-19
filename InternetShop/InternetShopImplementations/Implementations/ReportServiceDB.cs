using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Data.Entity;
using System.IO;
using InternetShopServiceDAL.BindingModels;
using Microsoft.Office.Interop.Excel;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace InternetShopImplementations.Implementations
{
    public class ReportServiceDB : IReportService
    {
        private AbstractWebDbContext context;

        public ReportServiceDB(AbstractWebDbContext context)
        {
            this.context = context;
        }

        public ReportServiceDB()
        {
            this.context = new AbstractWebDbContext();
        }

        public List<ClientBasketViewModel> GetClientBaskets(ReportBindingModel model, int ClientId)
        {
            return context.Baskets
                            .Include(rec => rec.Client)
                            .Where(rec => rec.Client.Id == ClientId)
                            .Select(rec => new ClientBasketViewModel
                            {
                                Name = rec.Client.Name,
                                isReserved = rec.IsReserved,
                                productList = rec.ProductsBasket.Select(recp => new ProductViewModel
                                {
                                    ComponentsProduct = recp.Product.ComponentsProduct.Select(recm => new ComponentProductViewModel
                                    {
                                        ComponentName = recm.Component.Name,
                                        Count = recm.Count * recp.Count
                                    }).ToList()
                                }).ToList()
                            }).ToList();
        }
        public void SaveClientBaskets(ReportBindingModel model, int ClientId)
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

                excelcells = excelcells.get_Offset(1, 1);
                excelcells.Interior.Color = Color.Yellow;
                excelcells.Font.Bold = true;
                excelcells.Value2 = "Клиент";
                excelcells = excelcells.get_Offset(0, 1);
                excelcells.Interior.Color = Color.Yellow;
                excelcells.Font.Bold = true;
                excelcells.Value2 = "Компнент";
                excelcells = excelcells.get_Offset(0, 1);
                excelcells.Interior.Color = Color.Yellow;
                excelcells.Font.Bold = true;
                excelcells.Value2 = "Количество";
                excelcells = excelcells.get_Offset(0, -1);

                var dict = GetClientBaskets(model, ClientId);
                int i = 0;
                if (dict != null)
                {
                    foreach (var pt in dict)
                    {
                        if (i == dict.Count - 1)
                        {
                            excelcells = excelcells.get_Offset(2, 0);
                            excelcells.Value2 = pt.Name;
                            excelcells = excelcells.get_Offset(0, 1);
                            if (pt.productList.Count() > 0)
                            {
                                foreach (var pr in pt.productList)
                                {
                                    var excelBorder =
                                    excelworksheet.get_Range(excelcells,
                                                excelcells.get_Offset(pr.ComponentsProduct.Count() - 1, 1));
                                    excelBorder.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                                    excelBorder.Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                                    excelBorder.HorizontalAlignment = Constants.xlCenter;
                                    excelBorder.VerticalAlignment = Constants.xlCenter;
                                    excelBorder.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                                                            Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium,
                                                            Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                                                            1);
                                    foreach (var pm in pr.ComponentsProduct)
                                    {
                                        excelcells.Value2 = pm.ComponentName;
                                        excelcells = excelcells.get_Offset(0, 1);
                                        excelcells.Value2 = pm.Count;
                                        excelcells = excelcells.get_Offset(1, -1);
                                    }
                                }
                            }
                            excelcells = excelcells.get_Offset(0, -1);
                        }
                        i++;
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
        public void SaveClientAllBaskets(ReportBindingModel model, int ClientId)
        {
            //открываем файл для работы
            FileStream fs = new FileStream(model.FileName, FileMode.OpenOrCreate, FileAccess.Write);
            //создаем документ, задаем границы, связываем документ и поток
            iTextSharp.text.Document doc = new iTextSharp.text.Document();
            doc.SetMargins(0.5f, 0.5f, 0.5f, 0.5f);
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            doc.Open();
            BaseFont baseFont = BaseFont.CreateFont("TIMCYR.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            //вставляем заголовок
            var phraseTitle = new Phrase("Покупки клиента",
            new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD));
            iTextSharp.text.Paragraph paragraph = new
            iTextSharp.text.Paragraph(phraseTitle)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);

            var phrasePeriod = new Phrase("c " + model.DateFrom.Value.ToShortDateString()
                + " по " + model.DateTo.Value.ToShortDateString(), new iTextSharp.text.Font(baseFont, 14,
                iTextSharp.text.Font.BOLD));
            paragraph = new iTextSharp.text.Paragraph(phrasePeriod)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);

            //вставляем таблицу, задаем количество столбцов, и ширину колонок
            PdfPTable table = new PdfPTable(3)
            {
                TotalWidth = 800F
            };
            table.SetTotalWidth(new float[] { 160, 140, 160 });

            //вставляем шапку
            PdfPCell cell = new PdfPCell();
            var fontForCellBold = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.BOLD);
            table.AddCell(new PdfPCell(new Phrase("Клиент", fontForCellBold))
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
            var list = GetClientBaskets(model, ClientId);
            var fontForCells = new iTextSharp.text.Font(baseFont, 10);
            foreach (var pt in list)
            {
                cell = new PdfPCell(new Phrase(pt.Name, fontForCells));
                table.AddCell(cell);
                int i = 0;
                foreach (var pr in pt.productList)
                {
                    foreach (var pm in pr.ComponentsProduct)
                    {
                        cell = new PdfPCell(new Phrase(pm.ComponentName, fontForCells));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase(pm.Count.ToString(), fontForCells));
                        table.AddCell(cell);
                    }
                    i++;
                    if ((pt.productList.Count() > 1) && (i != pt.productList.Count()))
                    {
                        cell = new PdfPCell(new Phrase(" ", fontForCells));
                        table.AddCell(cell);
                    }

                }
                cell = new PdfPCell(new Phrase("--", fontForCells));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("--", fontForCells));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("--", fontForCells));
                table.AddCell(cell);
            }
            doc.Add(table);
            doc.Close();
        }
    }
}
