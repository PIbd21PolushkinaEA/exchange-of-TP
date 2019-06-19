using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.IO;
using System.Linq;
using System.Reflection;
using System.util;
using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Office.Interop.Word;
using Document = Microsoft.Office.Interop.Word.Document;

namespace InternetShopImplementations.Implementations {
    public class ReportServiceDB : IReportService {
        private AbstractWebDbContext context;

        public ReportServiceDB(AbstractWebDbContext context) {
            this.context = context;
        }

        public void SaveProductPrice(ReportBindingModel model) {
            if ( File.Exists(model.FileName) ) {
                File.Delete(model.FileName);
            }

            var winword = new Application();
            try {
                object missing = Missing.Value;
                //создаем документ
                Document document =
                    winword.Documents.Add(ref missing, ref missing, ref missing, ref
                    missing);
                //получаем ссылку на параграф
                var paragraph = document.Paragraphs.Add(missing);
                var range = paragraph.Range;
                //задаем текст
                range.Text = "Прайс изделий";
                //задаем настройки шрифта
                var font = range.Font;
                font.Size = 16;
                font.Name = "Times New Roman";
                font.Bold = 1;
                //задаем настройки абзаца
                var paragraphFormat = range.ParagraphFormat;
                paragraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                paragraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphFormat.SpaceAfter = 10;
                paragraphFormat.SpaceBefore = 0;
                //добавляем абзац в документ
                range.InsertParagraphAfter();
                var products = context.Products.ToList();
                //создаем таблицу
                var paragraphTable = document.Paragraphs.Add(Type.Missing);
                var rangeTable = paragraphTable.Range;
                var table = document.Tables.Add(rangeTable, products.Count, 2, ref
                missing, ref missing);
                font = table.Range.Font;
                font.Size = 14;
                font.Name = "Times New Roman";
                var paragraphTableFormat = table.Range.ParagraphFormat;
                paragraphTableFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphTableFormat.SpaceAfter = 0;
                paragraphTableFormat.SpaceBefore = 0;
                for ( int i = 0; i < products.Count; ++i ) {
                    table.Cell(i + 1, 1).Range.Text = products[i].ProductName;
                    table.Cell(i + 1, 2).Range.Text = products[i].Price.ToString();
                }

                //задаем границы таблицы
                table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleInset;
                table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
                paragraph = document.Paragraphs.Add(missing);
                range = paragraph.Range;
                range.Text = "Дата: " + DateTime.Now.ToLongDateString();
                font = range.Font;
                font.Size = 12;
                font.Name = "Times New Roman";
                paragraphFormat = range.ParagraphFormat;
                paragraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                paragraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphFormat.SpaceAfter = 10;
                paragraphFormat.SpaceBefore = 10;
                range.InsertParagraphAfter();
                //сохраняем
                object fileFormat = WdSaveFormat.wdFormatXMLDocument;
                document.SaveAs(model.FileName, ref fileFormat, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing);
                document.Close(ref missing, ref missing, ref missing);
            }
            finally {
                winword.Quit();
            }
        }

        public List<ClientOrdersModel> GetClientOrders(ReportBindingModel model) {
            return context.Baskets
                .Include(rec => rec.Client)
                .Include(rec => rec.ProductsBasket)
                .Where(rec => rec.DateCreate >= model.DateFrom &&
                              rec.DateCreate <= model.DateTo)
                .Select(rec => new ClientOrdersModel {
                    ClientName = rec.Client.Name,
                    DateCreate = SqlFunctions.DateName("dd", rec.DateCreate)
                                 + " " +
                                 SqlFunctions.DateName("mm", rec.DateCreate) +
                                 " " +
                                 SqlFunctions.DateName("yyyy", rec.DateCreate),
                    ProductName = rec.NameBuy,
                    Count = rec.CountOfChoosedProducts,
                    Sum = rec.SumOfChoosedProducts
                })
                .ToList();
        }

        public void SaveClientOrders(ReportBindingModel model) {
            if (!File.Exists("TIMCYR.TTF"))
            {
                File.WriteAllBytes("TIMCYR.TTF", Properties.Resources.TIMCYR);
            }
            //открываем файл для работы
            FileStream fs = new FileStream(model.FileName, FileMode.OpenOrCreate,
            FileAccess.Write);
            //создаем документ, задаем границы, связываем документ и поток
            iTextSharp.text.Document doc = new iTextSharp.text.Document();
            doc.SetMargins(0.5f, 0.5f, 0.5f, 0.5f);
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();
            BaseFont baseFont = BaseFont.CreateFont("TIMCYR.TTF", BaseFont.IDENTITY_H,
            BaseFont.NOT_EMBEDDED);
            //вставляем заголовок
            var phraseTitle = new Phrase("Заказы клиентов",
            new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD));
            iTextSharp.text.Paragraph paragraph = new
                iTextSharp.text.Paragraph(phraseTitle) {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 12
                };
            doc.Add(paragraph);
            var phrasePeriod = new Phrase("c " + model.DateFrom.Value.ToShortDateString()
                                               +
                                               " по " + model.DateTo.Value.ToShortDateString(),
            new iTextSharp.text.Font(baseFont, 14,
            iTextSharp.text.Font.BOLD));
            paragraph = new iTextSharp.text.Paragraph(phrasePeriod) {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);
            //вставляем таблицу, задаем количество столбцов, и ширину колонок
            PdfPTable table = new PdfPTable(6) {
                TotalWidth = 800F
            };
            table.SetTotalWidth(new float[] {160, 140, 160, 100, 100, 140});
            //вставляем шапку
            PdfPCell cell = new PdfPCell();
            var fontForCellBold = new iTextSharp.text.Font(baseFont, 10,
            iTextSharp.text.Font.BOLD);
            table.AddCell(new PdfPCell(new Phrase("ФИО клиента", fontForCellBold)) {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Дата создания", fontForCellBold)) {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Изделие", fontForCellBold)) {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Количество", fontForCellBold)) {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Сумма", fontForCellBold)) {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Статус", fontForCellBold)) {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            //заполняем таблицу
            var list = GetClientOrders(model);
            var fontForCells = new iTextSharp.text.Font(baseFont, 10);
            for ( int i = 0; i < list.Count; i++ ) {
                cell = new PdfPCell(new Phrase(list[i].ClientName, fontForCells));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].DateCreate, fontForCells));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].ProductName, fontForCells));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].Count.ToString(), fontForCells));
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].Sum.ToString(), fontForCells));
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(cell);
                table.AddCell(cell);
            }

            //вставляем итого
            cell = new PdfPCell(new Phrase("Итого:", fontForCellBold)) {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Colspan = 4,
                Border = 0
            };
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(list.Sum(rec => rec.Sum).ToString(),
            fontForCellBold)) {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Border = 0
            };
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("", fontForCellBold)) {
                Border = 0
            };
            table.AddCell(cell);
            //вставляем таблицу
            doc.Add(table);
            doc.Close();
        }
    }
}