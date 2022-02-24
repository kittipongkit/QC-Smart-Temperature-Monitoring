//using Smart_Temperature_Monitoring.Models;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using GemBox.Spreadsheet;
using System.Data.SqlClient;
using static Smart_Temperature_Monitoring.InterfaceDB;

using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;


namespace Smart_Temperature_Monitoring
{
    public partial class sfrmReport1 : Form
    {
        static string _reportDate = "";

        //  Declare Logging
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        

        //private Spreadsheet spreadsheet;
        public sfrmReport1()
        {
            InitializeComponent();
            calenReport.SelectionRange.Start = calenReport.TodayDate;
            txtDateSelected.Text = calenReport.SelectionRange.Start.ToString("dd/MM/yyyy");
        }

        private static DataTable pGet_Temp_Range(DateTime selected_date, int sampling_min)
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_actual_value 2022-01-15 00:00:00.000 pGet_Temp_data
                SqlParameterCollection param = new SqlCommand().Parameters;
                param.AddWithValue("@selected_date", SqlDbType.DateTime).Value = selected_date;
                param.AddWithValue("@sampling_min", SqlDbType.DateTime).Value = sampling_min;
                ds = new DBClass().SqlExcSto("pGet_data_report_app", "DbSet", param);
                dataTable = ds.Tables[0];
            }
            catch (SqlException e)
            {
                dataTable = null;
                log.Error("Report pGet_Temp_Range SqlException : " + e.Message);
            }
            catch (Exception ex)
            {
                dataTable = null;
                log.Error("Report pGet_Temp_Range Exception : " + ex.Message);
            }
            return dataTable;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                //  ติดต่อ Database อ่านค่าจาก pGet_data_report
                SqlParameterCollection param = new SqlCommand().Parameters;
                param.AddWithValue("@selected_date", SqlDbType.DateTime).Value = calenReport.SelectionRange.Start;
                DataSet dataSet = new DBClass().SqlExcSto("pGet_data_report_app", "dbResult", param);


                if (dataSet == null || dataSet.Tables.Count <= 0)
                {
                    MessageBox.Show("Report no data");
                    log.Info("Report no data");
                    return;
                }

                //  Create report
                using (var package = new ExcelPackage())
                {
                    DataTable dataTable = dataSet.Tables[0];
                    //  Add a new worksheet to the empty workbook
                    var worksheet = package.Workbook.Worksheets.Add("Daily EX-1 Data");
                    //  Add the headers
                    int iColumn = 1;
                    string dateReport = "" + Convert.ToDateTime(dataTable.Rows[0][1]).ToString("dd-MM-yyyy");
                    worksheet.Cells[1, 1].Value = "EX-1 Daily Temperature Report";
                    worksheet.Cells[2, 1].Value = "Date :";
                    worksheet.Cells[2, 2].Value = dateReport;
                    _reportDate = dateReport;
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        if (iColumn <= 2)
                            worksheet.Cells[3, iColumn].Value = column.ColumnName;
                        else if (iColumn == 3 || iColumn == 6 || iColumn == 9)
                            worksheet.Cells[4, iColumn].Value = "MIN";
                        else if (iColumn == 4 || iColumn == 7 || iColumn == 10)
                            worksheet.Cells[4, iColumn].Value = "MAX";
                        else if (iColumn == 5 || iColumn == 8 || iColumn == 11)
                            worksheet.Cells[4, iColumn].Value = "AVG";
                        worksheet.Column(iColumn).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Column(iColumn).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        iColumn++;
                    }
                    //  Add data
                    int startRow = 5;
                    string twoDecimal = "_( #,##0.00_);_( (#,##0.00);_( \"-\"??_);_(@_)";

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        worksheet.Cells["A" + (i + startRow).ToString()].Value = dataTable.Rows[i][dataTable.Columns[0].ColumnName];
                        worksheet.Cells["B" + (i + startRow).ToString()].Value = dataTable.Rows[i][dataTable.Columns[1].ColumnName];
                        worksheet.Cells["C" + (i + startRow).ToString()].Value = dataTable.Rows[i][dataTable.Columns[2].ColumnName];
                        worksheet.Cells["D" + (i + startRow).ToString()].Value = dataTable.Rows[i][dataTable.Columns[3].ColumnName];
                        worksheet.Cells["E" + (i + startRow).ToString()].Value = dataTable.Rows[i][dataTable.Columns[4].ColumnName];
                        worksheet.Cells["F" + (i + startRow).ToString()].Value = dataTable.Rows[i][dataTable.Columns[5].ColumnName];
                        worksheet.Cells["G" + (i + startRow).ToString()].Value = dataTable.Rows[i][dataTable.Columns[6].ColumnName];
                        worksheet.Cells["H" + (i + startRow).ToString()].Value = dataTable.Rows[i][dataTable.Columns[7].ColumnName];
                        worksheet.Cells["I" + (i + startRow).ToString()].Value = dataTable.Rows[i][dataTable.Columns[8].ColumnName];
                        worksheet.Cells["J" + (i + startRow).ToString()].Value = dataTable.Rows[i][dataTable.Columns[9].ColumnName];
                        worksheet.Cells["K" + (i + startRow).ToString()].Value = dataTable.Rows[i][dataTable.Columns[10].ColumnName];

                        worksheet.Cells["A" + (i + startRow).ToString()].Style.Numberformat.Format = "HH:mm";
                        worksheet.Cells["B" + (i + startRow).ToString()].Style.Numberformat.Format = "HH:mm";
                        worksheet.Cells["C" + (i + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                        worksheet.Cells["H" + (i + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                        worksheet.Cells["I" + (i + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                        worksheet.Cells["J" + (i + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                        worksheet.Cells["K" + (i + startRow).ToString()].Style.Numberformat.Format = twoDecimal;

                        double avg1, min1, max1, avg2, min2, max2, avg3, min3, max3;
                        avg1 = Convert.ToDouble(dataTable.Rows[i][4]);
                        min1 = Convert.ToDouble(dataTable.Rows[i][11]);
                        max1 = Convert.ToDouble(dataTable.Rows[i][12]);
                        avg2 = Convert.ToDouble(dataTable.Rows[i][7]);
                        min2 = Convert.ToDouble(dataTable.Rows[i][13]);
                        max2 = Convert.ToDouble(dataTable.Rows[i][14]);
                        avg3 = Convert.ToDouble(dataTable.Rows[i][10]);
                        min3 = Convert.ToDouble(dataTable.Rows[i][15]);
                        max3 = Convert.ToDouble(dataTable.Rows[i][16]);
                        if (avg1 < min1 || avg1 > max1)
                            worksheet.Cells["E" + (i + startRow).ToString()].Style.Font.Color.SetColor(Color.Red);
                        if (avg2 < min2 || avg2 > max2)
                            worksheet.Cells["H" + (i + startRow).ToString()].Style.Font.Color.SetColor(Color.Red);
                        if (avg3 < min3 || avg3 > max3)
                            worksheet.Cells["K" + (i + startRow).ToString()].Style.Font.Color.SetColor(Color.Red);
                    }

                    //  Average row
                    int totalRow = dataTable.Rows.Count;
                    worksheet.Cells["A" + (totalRow + startRow).ToString()].Value = "Daily Average";
                    worksheet.Cells["C" + (totalRow + startRow).ToString()].Formula = "AVERAGE(C" + (startRow).ToString() + ":C" + (totalRow + startRow - 1).ToString() + ")";
                    worksheet.Cells["D" + (totalRow + startRow).ToString()].Formula = "AVERAGE(D" + (startRow).ToString() + ":D" + (totalRow + startRow - 1).ToString() + ")";
                    worksheet.Cells["E" + (totalRow + startRow).ToString()].Formula = "AVERAGE(E" + (startRow).ToString() + ":E" + (totalRow + startRow - 1).ToString() + ")";
                    worksheet.Cells["F" + (totalRow + startRow).ToString()].Formula = "AVERAGE(F" + (startRow).ToString() + ":F" + (totalRow + startRow - 1).ToString() + ")";
                    worksheet.Cells["G" + (totalRow + startRow).ToString()].Formula = "AVERAGE(G" + (startRow).ToString() + ":G" + (totalRow + startRow - 1).ToString() + ")";
                    worksheet.Cells["H" + (totalRow + startRow).ToString()].Formula = "AVERAGE(H" + (startRow).ToString() + ":H" + (totalRow + startRow - 1).ToString() + ")";
                    worksheet.Cells["I" + (totalRow + startRow).ToString()].Formula = "AVERAGE(I" + (startRow).ToString() + ":I" + (totalRow + startRow - 1).ToString() + ")";
                    worksheet.Cells["J" + (totalRow + startRow).ToString()].Formula = "AVERAGE(J" + (startRow).ToString() + ":J" + (totalRow + startRow - 1).ToString() + ")";
                    worksheet.Cells["K" + (totalRow + startRow).ToString()].Formula = "AVERAGE(K" + (startRow).ToString() + ":K" + (totalRow + startRow - 1).ToString() + ")";

                    //  Set header color
                    worksheet.Cells["A3:K4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells["A3:K4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(221, 235, 247));
                    worksheet.Cells["A3:K4"].Style.WrapText = true;

                    //  Set average style
                    worksheet.Cells["C" + (totalRow + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                    worksheet.Cells["D" + (totalRow + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                    worksheet.Cells["E" + (totalRow + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                    worksheet.Cells["F" + (totalRow + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                    worksheet.Cells["G" + (totalRow + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                    worksheet.Cells["H" + (totalRow + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                    worksheet.Cells["I" + (totalRow + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                    worksheet.Cells["J" + (totalRow + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                    worksheet.Cells["K" + (totalRow + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                    string lastRow = "A" + (totalRow + startRow).ToString() + ":K" + (totalRow + startRow).ToString();
                    worksheet.Cells[lastRow].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[lastRow].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(221, 235, 247));
                    worksheet.Cells[lastRow].Style.WrapText = true;

                    //  Make all text fit the cells
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                    worksheet.Cells[worksheet.Dimension.Address].AutoFilter = false;

                    //  Set default column width
                    int colWidth = 19;
                    for (int i = 1; i < 12; i++)
                        worksheet.Column(i).Width = colWidth;

                    //  Freeze
                    worksheet.View.FreezePanes(3, 1);

                    //  Merge
                    worksheet.Cells[3, 1, 4, 1].Merge = true;
                    worksheet.Cells[3, 2, 4, 2].Merge = true;
                    worksheet.Cells[3, 3, 3, 5].Merge = true;
                    worksheet.Cells[3, 6, 3, 8].Merge = true;
                    worksheet.Cells[3, 9, 3, 11].Merge = true;
                    worksheet.Cells[totalRow + startRow, 1, totalRow + startRow, 2].Merge = true;

                    //  Assing room headwe
                    worksheet.Cells[3, 3].Value = "Chilled room (4°C)";
                    worksheet.Cells[3, 6].Value = "Preparation room (15°C)";
                    worksheet.Cells[3, 9].Value = "Feeding room (15°C)";

                    //  Assign borders
                    var FirstTableRange = worksheet.Cells[3, 1, totalRow + startRow, 11];
                    FirstTableRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    FirstTableRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    FirstTableRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    FirstTableRange.Style.Border.BorderAround(ExcelBorderStyle.Thick);

                    //  Create chartChilledRoom
                    ExcelChart chartChilledRoom = worksheet.Drawings.AddChart("chart1", eChartType.Line);
                    chartChilledRoom.Title.Text = "Chilled room (4°C)";
                    chartChilledRoom.Title.Font.Size = 12;
                    chartChilledRoom.Title.Font.Bold = true;
                    chartChilledRoom.YAxis.Title.Text = "Temp °C";
                    chartChilledRoom.YAxis.Title.Font.Size = 10;
                    chartChilledRoom.YAxis.MajorTickMark = eAxisTickMark.None;
                    chartChilledRoom.YAxis.MinValue = Convert.ToDouble(dataTable.Compute("min([ห้องเย็น MIN])", string.Empty)) - 2;
                    chartChilledRoom.SetSize(480, 300);
                    chartChilledRoom.SetPosition(totalRow + startRow, 5, 0, 0);
                    chartChilledRoom.YAxis.Orientation = eAxisOrientation.MinMax;
                    chartChilledRoom.Legend.Position = eLegendPosition.Bottom;
                    var minSeries1 = chartChilledRoom.Series.Add(("C" + (startRow) + ":" + "C" + (totalRow + startRow - 1)), ("A" + (startRow) + ":" + "A" + (totalRow + startRow - 1)));
                    minSeries1.Header = "MIN";
                    var maxSeries1 = chartChilledRoom.Series.Add(("D" + (startRow) + ":" + "D" + (totalRow + startRow - 1)), ("A" + (startRow) + ":" + "A" + (totalRow + startRow - 1)));
                    maxSeries1.Header = "MAX";
                    var avgSeries1 = chartChilledRoom.Series.Add(("E" + (startRow) + ":" + "E" + (totalRow + startRow - 1)), ("A" + (startRow) + ":" + "A" + (totalRow + startRow - 1)));
                    avgSeries1.Header = "AVG";

                    //  Create chartPreparationRoom
                    ExcelChart chartPreparationRoom = worksheet.Drawings.AddChart("chart2", eChartType.Line);
                    chartPreparationRoom.Title.Text = "Preparation room (15°C)";
                    chartPreparationRoom.Title.Font.Size = 12;
                    chartPreparationRoom.Title.Font.Bold = true;
                    chartPreparationRoom.YAxis.Title.Text = "Temp °C";
                    chartPreparationRoom.YAxis.Title.Font.Size = 10;
                    chartPreparationRoom.YAxis.MajorTickMark = eAxisTickMark.None;
                    chartPreparationRoom.YAxis.MinValue = Convert.ToDouble(dataTable.Compute("min([ห้องเตรียม MIN])", string.Empty)) - 2;
                    chartPreparationRoom.SetSize(480, 300);
                    chartPreparationRoom.SetPosition(totalRow + startRow, 5, 3, 91);
                    chartPreparationRoom.YAxis.Orientation = eAxisOrientation.MinMax;
                    chartPreparationRoom.Legend.Position = eLegendPosition.Bottom;
                    var minSeries2 = chartPreparationRoom.Series.Add(("F" + (startRow) + ":" + "F" + (totalRow + startRow - 1)), ("A" + (startRow) + ":" + "A" + (totalRow + startRow - 1)));
                    minSeries2.Header = "MIN";
                    var maxSeries2 = chartPreparationRoom.Series.Add(("G" + (startRow) + ":" + "G" + (totalRow + startRow - 1)), ("A" + (startRow) + ":" + "A" + (totalRow + startRow - 1)));
                    maxSeries2.Header = "MAX";
                    var avgSeries2 = chartPreparationRoom.Series.Add(("H" + (startRow) + ":" + "H" + (totalRow + startRow - 1)), ("A" + (startRow) + ":" + "A" + (totalRow + startRow - 1)));
                    avgSeries2.Header = "AVG";

                    //  Create chartFeedingRoom
                    ExcelChart chartFeedingRoom = worksheet.Drawings.AddChart("chart3", eChartType.Line);
                    chartFeedingRoom.Title.Text = "Feeding room (15°C)";
                    chartFeedingRoom.Title.Font.Size = 12;
                    chartFeedingRoom.Title.Font.Bold = true;
                    chartFeedingRoom.YAxis.Title.Text = "Temp °C";
                    chartFeedingRoom.YAxis.Title.Font.Size = 10;
                    chartFeedingRoom.YAxis.MajorTickMark = eAxisTickMark.None;
                    chartFeedingRoom.YAxis.MinValue = Convert.ToDouble(dataTable.Compute("min([ห้องฟีด MIN])", string.Empty)) - 2;
                    chartFeedingRoom.SetSize(480, 300);
                    chartFeedingRoom.SetPosition(totalRow + startRow, 5, 7, 50);
                    chartFeedingRoom.YAxis.Orientation = eAxisOrientation.MinMax;
                    chartFeedingRoom.Legend.Position = eLegendPosition.Bottom;
                    var minSeries3 = chartFeedingRoom.Series.Add(("I" + (startRow) + ":" + "I" + (totalRow + startRow - 1)), ("A" + (startRow) + ":" + "A" + (totalRow + startRow - 1)));
                    minSeries3.Header = "MIN";
                    var maxSeries3 = chartFeedingRoom.Series.Add(("J" + (startRow) + ":" + "J" + (totalRow + startRow - 1)), ("A" + (startRow) + ":" + "A" + (totalRow + startRow - 1)));
                    maxSeries3.Header = "MAX";
                    var avgSeries3 = chartFeedingRoom.Series.Add(("K" + (startRow) + ":" + "K" + (totalRow + startRow - 1)), ("A" + (startRow) + ":" + "A" + (totalRow + startRow - 1)));
                    avgSeries3.Header = "AVG";

                    // Set some document properties
                    package.Workbook.Properties.Title = "AJI-NK_REPORT_EX-1_TEMP";
                    package.Workbook.Properties.Author = "AJI-NK Smart System";
                    package.Workbook.Properties.Comments = "N/A";

                    // Set some extended property values
                    package.Workbook.Properties.Company = "Ajinomoto Nong Khae";

                    // Set some custom property values
                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", "AJI-NK Smart System");
                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "AJI-NK Smart System");

                    //var xlFile = FileOutputUtil.GetFileInfo("DAILY_REPORT_EX-1_TEMP_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
                    // Check folder path 
                    string DestinationPath = ConfigurationManager.AppSettings["ReportDestination"];
                    if (!Directory.Exists(DestinationPath))
                    {
                        // Create folder
                        Directory.CreateDirectory(DestinationPath);
                    }
                    string dt = "DAILY_REPORT_EX-1_TEMP_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";
                    string filePath = DestinationPath + dt;

                    FileInfo xlFile = new FileInfo(filePath);

                    // Save our new workbook in the output directory and we are done!
                    package.SaveAs(xlFile);
                    MessageBox.Show("Report was generated by user");
                    log.Info("Report was generated by user");

                    // Return file
                    //if (xlFile.FullName == null)
                    //{
                    //    MessageBox.Show("Filename not present.");
                    //    log.Info("Filename not present.");
                    //}

                    //var memory = new MemoryStream();
                    //using (var stream = new FileStream(xlFile.FullName, FileMode.Open))
                    //{
                    //    await stream.CopyToAsync(memory);
                    //}
                    //memory.Position = 0;
                    //return File(memory, GetContentType(xlFile.FullName), Path.GetFileName(xlFile.FullName));
                    //return xlFile.FullName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Save report Exception \n" + ex.Message);
                log.Error("Save report Exception : " + ex.Message);
                return;
            }


        }

        private void calenReport_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtDateSelected.Text = calenReport.SelectionRange.Start.ToString("dd/MM/yyyy");
        }
    }
   
    
}
