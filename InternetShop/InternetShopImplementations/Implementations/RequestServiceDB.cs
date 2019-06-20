using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using InternetShopModel;
using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using Application = Microsoft.Office.Interop.Excel.Application;
using Range = Microsoft.Office.Interop.Excel.Range;
using XlBorderWeight = Microsoft.Office.Interop.Excel.XlBorderWeight;
using XlColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex;
using XlHAlign = Microsoft.Office.Interop.Excel.XlHAlign;
using XlLineStyle = Microsoft.Office.Interop.Excel.XlLineStyle;
using XlVAlign = Microsoft.Office.Interop.Excel.XlVAlign;

namespace InternetShopImplementations.Implementations {
    public class RequestServiceDB : IRequestService {
        private AbstractWebDbContext context;

        public RequestServiceDB(AbstractWebDbContext context) {
            this.context = context;
        }

        public void AddElement(RequestBindingModel model) {
            Request request = context.Requests.FirstOrDefault(rec => rec.Date ==
                                                                     model.Date);
            if ( request != null ) {
                throw new Exception("Уже есть заявка с такой датой");
            }

            context.Requests.Add(new Request {
                Date = model.Date
            });
            context.SaveChanges();
        }

        public void CreateRequest(RequestBindingModel model, bool type) {
            using ( var transaction = context.Database.BeginTransaction() ) {
                try {
                    Request element = new Request {
                        Date = model.Date
                    };
                    context.Requests.Add(element);
                    context.SaveChanges();

                    var groupComponents = model.RequestComponents
                        .GroupBy(rec => rec.ComponentID)
                        .Select(rec => new {
                            ComponentId = rec.Key,
                            Count = rec.Sum(r => r.CountComponents)
                        });
                    foreach ( var groupComponent in groupComponents ) {
                        context.RequestComponents.Add(new RequestComponent {
                            RequestId = element.Id,
                            ComponentId = groupComponent.ComponentId,
                            CountComponents = groupComponent.Count
                        });
                        context.SaveChanges();

                        var reservation =
                            context.Components.FirstOrDefault(rec => rec.Id == groupComponent.ComponentId);
                        reservation.CountOfAvailable += groupComponent.Count;
                        context.SaveChanges();
                    }

                    string typeMessage = "";
                    string fName = "";
                    if ( type ) {
                        RequestWord(model);
                        typeMessage = "word";
                        fName = "C:\\Users\\User\\Documents\\file.doc";
                    }
                    else {
                        RequestExcel(model);
                        typeMessage = "xls";
                        fName = "C:\\Users\\User\\Documents\\file.xls";
                    }

                    //Mail.SendEmail(null, "Заявка на брони", "Заявка на брони в формате " + typeMessage, fName);
                    transaction.Commit();
                }
                catch ( Exception ) {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private void RequestExcel(RequestBindingModel model) {
            var excel = new Application();
            try {
                //или создаем excel-файл, или открываем существующий
                if ( File.Exists("C:\\Users\\User\\Documents\\file.xls") ) {
                    excel.Workbooks.Open("C:\\Users\\User\\Documents\\file.xls", Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                else {
                    excel.SheetsInNewWorkbook = 1;
                    excel.Workbooks.Add(Type.Missing);
                    excel.Workbooks[1].SaveAs("C:\\Users\\User\\Documents\\file.xls", XlFileFormat.xlExcel8,
                    Type.Missing, Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }

                Sheets excelsheets = excel.Workbooks[1].Worksheets;
                //Получаем ссылку на лист
                var excelworksheet = (Worksheet) excelsheets.get_Item(1);
                //очищаем ячейки
                excelworksheet.Cells.Clear();
                //настройки страницы
                excelworksheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                excelworksheet.PageSetup.CenterHorizontally = true;
                excelworksheet.PageSetup.CenterVertically = true;
                //получаем ссылку на первые 3 ячейки
                Range excelcells = excelworksheet.get_Range("A1", "B1");
                //объединяем их
                excelcells.Merge(Type.Missing);
                //задаем текст, настройки шрифта и ячейки
                excelcells.Font.Bold = true;
                excelcells.Value2 = "Заявка на комплектующие";
                excelcells.RowHeight = 25;
                excelcells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                excelcells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                excelcells.Font.Name = "Times New Roman";
                excelcells.Font.Size = 14;
                excelcells = excelworksheet.get_Range("A2", "B2");
                excelcells.Merge(Type.Missing);
                excelcells.Value2 = "Дата:" + model.Date.ToShortDateString();
                excelcells.RowHeight = 20;
                excelcells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                excelcells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                excelcells.Font.Name = "Times New Roman";
                excelcells.Font.Size = 12;

                var components = context.Components.Select(rec => new ComponentViewModel {
                    Id = rec.Id,
                    Name = rec.Name,
                    Manufacturer = rec.Manufacturer,
                    Brand = rec.Brand
                }).ToList();
                var requestComponents = model.RequestComponents;

                excelcells = excelworksheet.get_Range("A3", "A3");
                //обводим границы
                //получаем ячейки для выбеления рамки под таблицу
                var excelBorder = excelworksheet.get_Range(excelcells,
                excelcells.get_Offset(requestComponents.Count() - 1, 4));
                excelBorder.Borders.LineStyle = XlLineStyle.xlContinuous;
                excelBorder.Borders.Weight = XlBorderWeight.xlThin;
                excelBorder.HorizontalAlignment = Constants.xlCenter;
                excelBorder.VerticalAlignment = Constants.xlCenter;
                excelBorder.BorderAround(XlLineStyle.xlContinuous,
                XlBorderWeight.xlMedium,
                XlColorIndex.xlColorIndexAutomatic, 1);
                foreach ( var requestComponent in requestComponents ) {
                    excelcells.Value2 = components.FirstOrDefault(rec => rec.Id == requestComponent.ComponentID)
                        .Name;
                    excelcells.ColumnWidth = 20;
                    excelcells.get_Offset(0, 1).Value2 = components.FirstOrDefault(rec => rec.Id == requestComponent.ComponentID)
                        ?.Manufacturer;
                    excelcells.get_Offset(0, 2).Value2 = components.FirstOrDefault(rec => rec.Id == requestComponent.ComponentID)
                        ?.Brand;
                    excelcells.get_Offset(0, 3).Value2 = requestComponent.CountComponents;
                    excelcells = excelcells.get_Offset(1, 0);
                }

                //сохраняем
                excel.Workbooks[1].Save();
                excel.Workbooks[1].Close();
            }
            catch ( Exception ) {
                throw;
            }
            finally {
                //закрываем
                excel.Quit();
            }
        }

        private void RequestWord(RequestBindingModel model) {
            Console.WriteLine();
            if ( File.Exists("C:\\Users\\User\\Documents\\file.doc") ) {
                File.Delete("C:\\Users\\User\\Documents\\file.doc");
            }

            var winword = new Microsoft.Office.Interop.Word.Application();
            try {
                object missing = Missing.Value;
                //создаем документ
                Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                //получаем ссылку на параграф
                var paragraph = document.Paragraphs.Add(missing);
                var range = paragraph.Range;
                //задаем текст
                range.Text = "Заявка на брони";
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

                var requestComponents = model.RequestComponents;
                var reservations = context.Components.Select(rec => new ComponentViewModel {
                    Id = rec.Id,
                    Name = rec.Name,
                    Manufacturer = rec.Manufacturer,
                    Brand = rec.Brand,
                }).ToList();
                //создаем таблицу
                var paragraphTable = document.Paragraphs.Add(Type.Missing);
                var rangeTable = paragraphTable.Range;
                var table = document.Tables.Add(rangeTable, requestComponents.Count, 4, ref missing, ref missing);
                font = table.Range.Font;
                font.Size = 14;
                font.Name = "Times New Roman";
                var paragraphTableFormat = table.Range.ParagraphFormat;
                paragraphTableFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphTableFormat.SpaceAfter = 0;
                paragraphTableFormat.SpaceBefore = 0;
                for ( int i = 0; i < requestComponents.Count; ++i ) {
                    table.Cell(i + 1, 1).Range.Text = reservations
                        .FirstOrDefault(rec => rec.Id == requestComponents[i].ComponentID)
                        ?.Name;
                    table.Cell(i + 1, 2).Range.Text = reservations
                        .FirstOrDefault(rec => rec.Id == requestComponents[i].ComponentID)
                        ?.Manufacturer;
                    table.Cell(i + 1, 3).Range.Text = reservations
                        .FirstOrDefault(rec => rec.Id == requestComponents[i].ComponentID)
                        ?.Brand;
                        table.Cell(i + 1, 4).Range.Text = requestComponents[i].CountComponents.ToString();
                }
            
                //задаем границы таблицы
                table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleInset;
                table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
                paragraph = document.Paragraphs.Add(missing);
                range = paragraph.Range;
                range.Text = "Дата: " + model.Date.ToLongDateString();
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
                document.SaveAs("C:\\Users\\User\\Documents\\file.doc", ref fileFormat, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing);
                document.Close(ref missing, ref missing, ref missing);
            }
            catch ( Exception ) {
                throw;
            }
            finally {
                winword.Quit();
            }
        }

        public void DelElement(int id) {
            Request request = context.Requests.FirstOrDefault(rec => rec.Id == id);
            if ( request != null ) {
                context.Requests.Remove(request);
                context.SaveChanges();
            }
            else {
                throw new Exception("Элемент не найден");
            }
        }

        public RequestViewModel GetElement(int id) {
            Request request = context.Requests.FirstOrDefault(rec => rec.Id == id);
            if ( request != null ) {
                return new RequestViewModel {
                    Id = request.Id,
                    DateCreate = request.Date
                };
            }

            throw new Exception("Элемент не найден");
        }

        public List<RequestViewModel> GetList() {
            List<RequestViewModel> result = context.Requests
                .Select(rec => new RequestViewModel {
                    Id = rec.Id,
                    DateCreate = rec.Date
                })
                .ToList();
            return result;
        }

        public void UpdElement(RequestBindingModel model) {
            Request request = context.Requests.FirstOrDefault(rec => rec.Date != model.Date);
            if ( request != null ) {
                throw new Exception("Уже есть заявка с такой датой");
            }

            request = context.Requests.FirstOrDefault(rec => rec.Id == model.Id);
            if ( request == null ) {
                throw new Exception("Элемент не найден");
            }

            request.Date = model.Date;
            context.SaveChanges();
        }
    }
}