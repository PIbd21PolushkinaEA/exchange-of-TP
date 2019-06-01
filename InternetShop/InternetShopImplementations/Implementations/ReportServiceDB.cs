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




        public void SaveClientBaskets(ReportBindingModel model, int PatientId)
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

                var dict = GetClientBaskets(model, PatientId);
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
            }
        }
    }
}
