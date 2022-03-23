//using Smart_Temperature_Monitoring.Models;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

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

        private static DataTable _pGet_tool_name = new DataTable();
        private static DataTable _pGet_location_name = new DataTable();
        private static DataTable _pGet_foor_name = new DataTable();
        private static DataTable _pGet_temp_name = new DataTable();



        //private Spreadsheet spreadsheet;
        public sfrmReport1()
        {
            InitializeComponent();
            get_tool_name();
            get_location_name();
            get_foor_name();
            get_temp_name();

            List<report_type> list = new List<report_type>();
            list.Add(new report_type() { No = "1", Name = "Daily report" });
            list.Add(new report_type() { No = "2", Name = "Monthly report" });

            //Display member and value for combobox Sampling Time
            cbbReportType.DataSource = list;
            cbbReportType.ValueMember = "No";
            cbbReportType.DisplayMember = "Name";

            calenReport.SelectionRange.Start = calenReport.TodayDate;
            txtDateSelected.Text = calenReport.SelectionRange.Start.ToString("dd/MM/yyyy");
        }
        private void get_tool_name()
        {
            _pGet_tool_name = new DataTable();
            _pGet_tool_name = pGet_tool_name();
            if (_pGet_tool_name != null)
            {
                //Insert the Default Item to DataTable.
                DataRow row = _pGet_tool_name.NewRow();
                row[0] = 0;
                row[1] = "Please select";
                _pGet_tool_name.Rows.InsertAt(row, 0);

                //Assign DataTable as DataSource.
                cbbSelectedTool.DataSource = _pGet_tool_name;
                cbbSelectedTool.DisplayMember = "tool_name"; ;
                cbbSelectedTool.ValueMember = "tool_id";                
            }
        }


        private void get_location_name()
        {
            _pGet_location_name = new DataTable();
            _pGet_location_name = pGet_location_name();
            if (_pGet_location_name != null)
            {
                //Insert the Default Item to DataTable.
                DataRow row = _pGet_location_name.NewRow();
                row[0] = 0;
                row[1] = "Please select";
                _pGet_location_name.Rows.InsertAt(row, 0);

                //Assign DataTable as DataSource.
                cbbSelectedLocation.DataSource = _pGet_location_name;
                cbbSelectedLocation.DisplayMember = "location_name";
                cbbSelectedLocation.ValueMember = "location_id";
            }
        }

        private void get_foor_name()
        {
            _pGet_foor_name = new DataTable();
            _pGet_foor_name = pGet_foor_name();
            if (_pGet_foor_name != null)
            {
                //Insert the Default Item to DataTable.
                DataRow row = _pGet_foor_name.NewRow();
                row[0] = 0;
                row[1] = "Please select";
                _pGet_foor_name.Rows.InsertAt(row, 0);

                //Assign DataTable as DataSource.
                cbbSelectedFoor.DataSource = _pGet_foor_name;
                cbbSelectedFoor.DisplayMember = "foor_name";
                cbbSelectedFoor.ValueMember = "foor_id";
            }
        }

        private void get_temp_name()
        {

            _pGet_temp_name = new DataTable();
            _pGet_temp_name = pGet_temp_name(Convert.ToInt32(cbbSelectedTool.SelectedValue), Convert.ToInt32(cbbSelectedLocation.SelectedValue), Convert.ToInt32(cbbSelectedFoor.SelectedValue));
            if (_pGet_temp_name != null)
            {
                cbbSelectedName.DataSource = _pGet_temp_name;
                cbbSelectedName.DisplayMember = "temp_name";
                cbbSelectedName.ValueMember = "temp_number";
            }
        }

        private static DataTable pGet_data_report_app(DateTime selected_date, int sampling_min)
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_data_report_app 
                SqlParameterCollection param = new SqlCommand().Parameters;
                param.AddWithValue("@selected_date", SqlDbType.DateTime).Value = selected_date;
                param.AddWithValue("@sampling_min", SqlDbType.DateTime).Value = sampling_min;
                ds = new DBClass().SqlExcSto("pGet_data_report_app", "DbSet", param);
                dataTable = ds.Tables[0];
            }
            catch (SqlException e)
            {
                dataTable = null;
                log.Error("Report pGet_data_report_app SqlException : " + e.Message);
            }
            catch (Exception ex)
            {
                dataTable = null;
                log.Error("Report pGet_data_report_app Exception : " + ex.Message);
            }
            return dataTable;
        }

        private static DataTable pGet_tool_name()
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_tool_name
                SqlParameterCollection param = new SqlCommand().Parameters;
                ds = new DBClass().SqlExcSto("pGet_tool_name", "DbSet", param);
                dataTable = ds.Tables[0];

            }
            catch (SqlException e)
            {
                dataTable = null;
                log.Error("pGet_tool_name SqlException : " + e.Message);
            }
            catch (Exception ex)
            {
                dataTable = null;
                log.Error("pGet_tool_name Exception : " + ex.Message);
            }
            return dataTable;
        }
       

        private static DataTable pGet_location_name()
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_location_name
                SqlParameterCollection param = new SqlCommand().Parameters;
                ds = new DBClass().SqlExcSto("pGet_location_name", "DbSet", param);
                dataTable = ds.Tables[0];

            }
            catch (SqlException e)
            {
                dataTable = null;
                log.Error("pGet_location_name SqlException : " + e.Message);
            }
            catch (Exception ex)
            {
                dataTable = null;
                log.Error("pGet_location_name Exception : " + ex.Message);
            }
            return dataTable;
        }

        private static DataTable pGet_foor_name()
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_foor
                SqlParameterCollection param = new SqlCommand().Parameters;
                ds = new DBClass().SqlExcSto("pGet_foor_name", "DbSet", param);
                dataTable = ds.Tables[0];

            }
            catch (SqlException e)
            {
                dataTable = null;
                log.Error("pGet_foor_name SqlException : " + e.Message);
            }
            catch (Exception ex)
            {
                dataTable = null;
                log.Error("pGet_foor Exception : " + ex.Message);
            }
            return dataTable;
        }

        private static DataTable pGet_temp_name(int tool_id, int location_id, int foor_id)
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_Temp_Name
                SqlParameterCollection param = new SqlCommand().Parameters;
                param.AddWithValue("@tool_id", SqlDbType.DateTime).Value = tool_id;
                param.AddWithValue("@location_id", SqlDbType.DateTime).Value = location_id;
                param.AddWithValue("@foor_id", SqlDbType.DateTime).Value = foor_id;
                ds = new DBClass().SqlExcSto("pGet_temp_name", "DbSet", param);
                dataTable = ds.Tables[0];
            }
            catch (SqlException e)
            {
                dataTable = null;
                log.Error("pGet_Temp_Name SqlException : " + e.Message);
            }
            catch (Exception ex)
            {
                dataTable = null;
                log.Error("pGet_Temp_Name Exception : " + ex.Message);
            }
            return dataTable;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                //  ติดต่อ Database อ่านค่าจาก pGet_data_report
                SqlParameterCollection param = new SqlCommand().Parameters;
                param.AddWithValue("@report_type", SqlDbType.Int).Value = cbbReportType.SelectedValue;
                param.AddWithValue("@temp_no", SqlDbType.Int).Value = cbbSelectedName.SelectedValue;
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
                    var worksheet = package.Workbook.Worksheets.Add("แบบบันทึกตู้อบ Oven ตู้เย็น");

                    //  Add the top headers
                    worksheet.Cells[1, 9].Value = "PIC";
                    worksheet.Cells[1, 10].Value = "Supervisor";
                    worksheet.Cells[1, 11].Value = "Div. Mgr";
                    worksheet.Cells[2, 10].Value = "-";
                    
                    //  Assign the top headers borders
                    var TopTableRange = worksheet.Cells[1, 9, 3, 11];
                    TopTableRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    TopTableRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    TopTableRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    TopTableRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    TopTableRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    TopTableRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center; 

                    //  Assign the top headers merge
                    worksheet.Cells[2, 9, 3, 9].Merge = true;
                    worksheet.Cells[2, 10, 3, 10].Merge = true;
                    worksheet.Cells[2, 11, 3, 11].Merge = true;

                    //  Assign the top headers color
                    worksheet.Cells[1, 9, 1, 11].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, 9, 1, 11].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(178, 178, 178));


                    int iColumn = 1;
                    string dateReport = "" + Convert.ToDateTime(dataTable.Rows[0][0]).ToString("dd-MM-yyyy");

                    worksheet.Cells[2, 2].Value = "For Internal use only";
                    worksheet.Cells[2, 2].Style.Font.Size = 14;
                    worksheet.Cells[2, 2].Style.Font.Bold = true;
                    //worksheet.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    //worksheet.Cells[2, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[4, 6].Value = "แบบบันทึกการตรวจสอบอุณหภูมิ ตู้อบ ตู้เย็น ตู้บ่มเชื้อ";
                    worksheet.Cells[4, 6].Style.Font.Size = 14;
                    worksheet.Cells[4, 6].Style.Font.Bold = true;

                    worksheet.Cells[5, 1].Value = "Date :";
                    worksheet.Cells[5, 2].Value = dateReport;
                    worksheet.Cells[5, 2].Style.Font.Color.SetColor(Color.Blue);

                    worksheet.Cells[5, 5].Value = "S/N:";
                    worksheet.Cells[5, 6].Value = cbbSelectedName.Text;
                    worksheet.Cells[5, 6].Style.Font.Color.SetColor(Color.Blue);

                    worksheet.Cells[5, 9].Value = "อุณหภูมิที่ควบคุม:";
                    worksheet.Cells[5, 11].Value = dataTable.Rows[0][6].ToString() + " \u00B1 " + dataTable.Rows[0][7].ToString();

                    //worksheet.Cells[5, 11].Value = dataTable.Rows[0][dataTable.Columns[6].ColumnName] != null ? Convert.ToDecimal(dataTable.Rows[0][dataTable.Columns[6].ColumnName]).ToString("N0") : '0' 
                    //    + " \u00B1 " + dataTable.Rows[0][dataTable.Columns[7].ColumnName] != null ? Convert.ToDecimal(dataTable.Rows[0][dataTable.Columns[7].ColumnName]).ToString("N0") : '0' + " °C";
                    worksheet.Cells[5, 11].Style.Font.Color.SetColor(Color.Blue);
                    _reportDate = dateReport;

                    //foreach (DataColumn column in dataTable.Columns)
                    for (int i = 1; i <= 11; i++)
                    {
                        //if (iColumn <= 2)
                        //    worksheet.Cells[3, iColumn].Value = column.ColumnName; 
                        //else if (iColumn == 3 || iColumn == 6 || iColumn == 9)
                        //    worksheet.Cells[4, iColumn].Value = "MIN";
                        //else if (iColumn == 4 || iColumn == 7 || iColumn == 10)
                        //    worksheet.Cells[4, iColumn].Value = "MAX";
                        //else if (iColumn == 5 || iColumn == 8 || iColumn == 11)
                        //    worksheet.Cells[4, iColumn].Value = "AVG";
                        worksheet.Column(i).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Column(i).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        //iColumn++;
                    }
                    //  Add data
                    int startRow = 8;
                    //string twoDecimal = "_( #,##0.00_);_( (#,##0.00);_( \"-\"??_);_(@_)";
                    string twoDecimal = "_( #,#0.0_);_( (#,#0.0);_( \"-\"??_);_(@_)";

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        worksheet.Cells["A" + (i + startRow).ToString()].Value = dataTable.Rows[i][dataTable.Columns[0].ColumnName] != null ? dataTable.Rows[i][dataTable.Columns[0].ColumnName] : 0;
                        worksheet.Cells["B" + (i + startRow).ToString()].Value = dataTable.Rows[i][dataTable.Columns[1].ColumnName] != null ? dataTable.Rows[i][dataTable.Columns[1].ColumnName] : 0;
                        worksheet.Cells["C" + (i + startRow).ToString()].Value = dataTable.Rows[i][dataTable.Columns[2].ColumnName] != null ? dataTable.Rows[i][dataTable.Columns[2].ColumnName] : 0;
                        worksheet.Cells["D" + (i + startRow).ToString()].Value = dataTable.Rows[i][dataTable.Columns[3].ColumnName] != null ? dataTable.Rows[i][dataTable.Columns[3].ColumnName] : 0;
                        worksheet.Cells["E" + (i + startRow).ToString()].Value = dataTable.Rows[i][dataTable.Columns[0].ColumnName] != null ? dataTable.Rows[i][dataTable.Columns[0].ColumnName] : 0;
                        worksheet.Cells["F" + (i + startRow).ToString()].Value = dataTable.Rows[i][dataTable.Columns[4].ColumnName] != null ? dataTable.Rows[i][dataTable.Columns[4].ColumnName] : 0;
                        worksheet.Cells["G" + (i + startRow).ToString()].Value = dataTable.Rows[i][dataTable.Columns[5].ColumnName] != null ? dataTable.Rows[i][dataTable.Columns[5].ColumnName] : 0;
                        worksheet.Cells["H" + (i + startRow).ToString()].Value = dataTable.Rows[i][dataTable.Columns[6].ColumnName] != null ? dataTable.Rows[i][dataTable.Columns[6].ColumnName] : 0;
                        //worksheet.Cells["I" + (i + startRow).ToString()].Value = dataTable.Rows[i][dataTable.Columns[4].ColumnName];
                        //worksheet.Cells["J" + (i + startRow).ToString()].Value = dataTable.Rows[i][dataTable.Columns[3].ColumnName];
                        //worksheet.Cells["K" + (i + startRow).ToString()].Value = dataTable.Rows[i][dataTable.Columns[4].ColumnName];

                        worksheet.Cells["A" + (i + startRow).ToString()].Style.Numberformat.Format = "DD/MM/YYYY";
                        worksheet.Cells["B" + (i + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                        worksheet.Cells["C" + (i + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                        worksheet.Cells["D" + (i + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                        worksheet.Cells["E" + (i + startRow).ToString()].Style.Numberformat.Format = "HH:mm";
                        worksheet.Cells["F" + (i + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                        worksheet.Cells["G" + (i + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                        worksheet.Cells["H" + (i + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                        //worksheet.Cells["J" + (i + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                        //worksheet.Cells["K" + (i + startRow).ToString()].Style.Numberformat.Format = twoDecimal;

                        double avg1, min1, max1;
                        avg1 = Convert.ToDouble(dataTable.Rows[i][1]);
                        min1 = Convert.ToDouble(dataTable.Rows[i][1]);
                        max1 = Convert.ToDouble(dataTable.Rows[i][1]);
                        if (avg1 < min1 || avg1 > max1)
                            worksheet.Cells["B" + (i + startRow).ToString()].Style.Font.Color.SetColor(Color.Red);
                    }

                    //  Average row
                    int totalRow = dataTable.Rows.Count;
                    //worksheet.Cells["A" + (totalRow + startRow).ToString()].Value = "Daily Average";
                    //worksheet.Cells["C" + (totalRow + startRow).ToString()].Formula = "AVERAGE(C" + (startRow).ToString() + ":C" + (totalRow + startRow - 1).ToString() + ")";
                    //worksheet.Cells["D" + (totalRow + startRow).ToString()].Formula = "AVERAGE(D" + (startRow).ToString() + ":D" + (totalRow + startRow - 1).ToString() + ")";
                    //worksheet.Cells["E" + (totalRow + startRow).ToString()].Formula = "AVERAGE(E" + (startRow).ToString() + ":E" + (totalRow + startRow - 1).ToString() + ")";
                    //worksheet.Cells["F" + (totalRow + startRow).ToString()].Formula = "AVERAGE(F" + (startRow).ToString() + ":F" + (totalRow + startRow - 1).ToString() + ")";
                    //worksheet.Cells["G" + (totalRow + startRow).ToString()].Formula = "AVERAGE(G" + (startRow).ToString() + ":G" + (totalRow + startRow - 1).ToString() + ")";
                    //worksheet.Cells["H" + (totalRow + startRow).ToString()].Formula = "AVERAGE(H" + (startRow).ToString() + ":H" + (totalRow + startRow - 1).ToString() + ")";
                    //worksheet.Cells["I" + (totalRow + startRow).ToString()].Formula = "AVERAGE(I" + (startRow).ToString() + ":I" + (totalRow + startRow - 1).ToString() + ")";
                    //worksheet.Cells["J" + (totalRow + startRow).ToString()].Formula = "AVERAGE(J" + (startRow).ToString() + ":J" + (totalRow + startRow - 1).ToString() + ")";
                    //worksheet.Cells["K" + (totalRow + startRow).ToString()].Formula = "AVERAGE(K" + (startRow).ToString() + ":K" + (totalRow + startRow - 1).ToString() + ")";

                    //  Set header color
                    //worksheet.Cells["A7:I7"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //worksheet.Cells["A7:I7"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(221, 235, 247));
                    worksheet.Cells["A7:I7"].Style.WrapText = true;

                    //  Set average style
                    //worksheet.Cells["C" + (totalRow + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                    //worksheet.Cells["D" + (totalRow + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                    //worksheet.Cells["E" + (totalRow + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                    //worksheet.Cells["F" + (totalRow + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                    //worksheet.Cells["G" + (totalRow + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                    //worksheet.Cells["H" + (totalRow + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                    //worksheet.Cells["I" + (totalRow + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                    //worksheet.Cells["J" + (totalRow + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                    //worksheet.Cells["K" + (totalRow + startRow).ToString()].Style.Numberformat.Format = twoDecimal;
                    //string lastRow = "A" + (totalRow + startRow).ToString() + ":K" + (totalRow + startRow).ToString();
                    //worksheet.Cells[lastRow].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //worksheet.Cells[lastRow].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(221, 235, 247));
                    //worksheet.Cells[lastRow].Style.WrapText = true;

                    //  Make all text fit the cells
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                    worksheet.Cells[worksheet.Dimension.Address].AutoFilter = false;

                    //  Set default column width
                    worksheet.Column(1).Width = 12;
                    int colWidth = 10;
                    for (int i = 2; i < 12; i++)
                    {
                        worksheet.Column(i).Width = colWidth;
                    }

                    //  Freeze
                    //worksheet.View.FreezePanes(3, 1);

                    //  Merge
                    worksheet.Cells[5, 2, 5, 3].Merge = true;
                    worksheet.Cells[5, 6, 5, 7].Merge = true;

                    for (int i = 0; i <= dataTable.Rows.Count ; i++)
                    {
                        worksheet.Cells[i+7, 9, i+7, 10].Merge = true;
                    }
                    
                    //  Assing Table Header
                    worksheet.Cells[7, 1].Value = "Date\nวันที่บันทึก";
                    worksheet.Cells[7, 2].Value = "อุณหภูมิ\n(°C)";
                    worksheet.Cells[7, 3].Value = "อุณหภูมิสูงสุด\n(°C)";
                    worksheet.Cells[7, 4].Value = "อุณหภูมิต่ำสุด\n(°C)";
                    worksheet.Cells[7, 5].Value = "Time\nเวลา";
                    worksheet.Cells[7, 6].Value = "Upper Control\nLimit(UCL)";
                    worksheet.Cells[7, 7].Value = "Lower Control\nLimit(LCL)";
                    worksheet.Cells[7, 8].Value = "Center Line\n(CL)";
                    worksheet.Cells[7, 9].Value = "Remarks\nหมายเหตุ";

                    //  Assign borders
                    var FirstTableRange = worksheet.Cells[7, 1, totalRow + startRow-1, 10];
                    FirstTableRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    FirstTableRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    FirstTableRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    FirstTableRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);

                    //  Create chart
                    ExcelChart chartChilledRoom = worksheet.Drawings.AddChart("chart1", eChartType.Line);
                    chartChilledRoom.Title.Text = cbbSelectedName.Text;
                    chartChilledRoom.Title.Font.Size = 12;
                    chartChilledRoom.Title.Font.Bold = true;
                    chartChilledRoom.YAxis.Title.Text = "Temperature(°C)";
                    chartChilledRoom.YAxis.Title.Font.Size = 10;
                    chartChilledRoom.YAxis.MajorTickMark = eAxisTickMark.None;
                    chartChilledRoom.YAxis.MinValue = Convert.ToDouble(dataTable.Compute("min([temp_min])", string.Empty)) - 2;
                    chartChilledRoom.SetSize(750, 400);
                    chartChilledRoom.SetPosition(totalRow + startRow, 5, 0, 0);
                    chartChilledRoom.YAxis.Orientation = eAxisOrientation.MinMax;
                    chartChilledRoom.Legend.Position = eLegendPosition.Bottom;

                    var avgSeries1 = chartChilledRoom.Series.Add(("B" + (startRow) + ":" + "B" + (totalRow + startRow - 1)), ((Convert.ToInt32(cbbReportType.SelectedValue) == 1 ? "E" : "A") + (startRow) + ":" + (Convert.ToInt32(cbbReportType.SelectedValue) == 1 ? "E" : "A") + (totalRow + startRow - 1)));
                    avgSeries1.Header = "อุณหภูมิ(°C)";
                    var minSeries1 = chartChilledRoom.Series.Add(("G" + (startRow) + ":" + "G" + (totalRow + startRow - 1)), ((Convert.ToInt32(cbbReportType.SelectedValue) == 1 ? "E" : "A") + (startRow) + ":" + (Convert.ToInt32(cbbReportType.SelectedValue) == 1 ? "E" : "A") + (totalRow + startRow - 1)));
                    minSeries1.Header = "Lower Control Limit(LCL)";
                    var maxSeries1 = chartChilledRoom.Series.Add(("F" + (startRow) + ":" + "F" + (totalRow + startRow - 1)), ((Convert.ToInt32(cbbReportType.SelectedValue) == 1 ? "E" : "A") + (startRow) + ":" + (Convert.ToInt32(cbbReportType.SelectedValue) == 1 ? "E" : "A") + (totalRow + startRow - 1)));
                    maxSeries1.Header = "Upper Control Limit(UCL)";
                    var centerSeries1 = chartChilledRoom.Series.Add(("H" + (startRow) + ":" + "H" + (totalRow + startRow - 1)), ((Convert.ToInt32(cbbReportType.SelectedValue) == 1 ? "E" : "A") + (startRow) + ":" + (Convert.ToInt32(cbbReportType.SelectedValue) == 1 ? "E" : "A") + (totalRow + startRow - 1)));
                    centerSeries1.Header = "Center Line(CL)";


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

                    string report_type = null;
                    if (Convert.ToInt32(cbbReportType.SelectedValue) == 1)
                        report_type = "DAILY_REPORT_";
                    else
                        report_type = "MONTHLY_REPORT_";

                    string dt = report_type + cbbSelectedName.Text + '_' + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";
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

        private void cbbSelectedName_Click(object sender, EventArgs e)
        {
            get_temp_name();
        }               
    }  
    
}
