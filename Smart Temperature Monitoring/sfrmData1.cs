using LiveCharts; //Core of the library
using LiveCharts.Wpf; //The WPF controls
using LiveCharts.Configurations;
using System.Data.SqlClient;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows.Forms;
using static Smart_Temperature_Monitoring.InterfaceDB;
using Brushes = System.Windows.Media.Brushes;
using System.Collections.Generic;



namespace Smart_Temperature_Monitoring
{
    public partial class sfrmData1 : Form
    {
        //  Declare Logging
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //  Global varriable
        private static DataTable _pGet_Temp_data = new DataTable();
        private static DataTable _pGet_tool_name = new DataTable();
        private static DataTable _pGet_location_name = new DataTable();
        private static DataTable _pGet_foor_name = new DataTable();
        private static DataTable _pGet_temp_name = new DataTable();
        private static DataTable _pGet_setting = new DataTable();

        public sfrmData1()
        {
            InitializeComponent();
            initTempData();
                     

            List<sampling_time> list = new List<sampling_time>();
            list.Add(new sampling_time() { No = "1", Name = "5 Minute"});
            list.Add(new sampling_time() { No = "2", Name = "15 Minute" });
            list.Add(new sampling_time() { No = "3", Name = "30 Minute" });
            list.Add(new sampling_time() { No = "4", Name = "1 Hour" });

            //Display member and value for combobox Sampling Time
            cbbSampling.DataSource = list;
            cbbSampling.ValueMember = "No";
            cbbSampling.DisplayMember = "Name";

        }

        private void initTempData()
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
        {
            Get_setting();

            _pGet_Temp_data = new DataTable();
            _pGet_Temp_data = pGet_data_with_sampling_time(Convert.ToInt32(sfrmOverview._selectedTempNoData), dtDateFrom.Value.Date, dtDateTo.Value, 1);
            if (_pGet_Temp_data != null && _pGet_Temp_data.Rows.Count > 0)
            {
                var temp = new ChartValues<double>();
                var ahi = new ChartValues<double>();
                var alo = new ChartValues<double>();
                var whi = new ChartValues<double>();
                var wlo = new ChartValues<double>();

                for (var i = 0; i < _pGet_Temp_data.Rows.Count; i++)
                {
                    temp.Add(Convert.ToDouble(_pGet_Temp_data.Rows[i]["avg_temp"]));
                    ahi.Add(Convert.ToDouble(_pGet_Temp_data.Rows[i]["temp_ahi"]));
                    alo.Add(Convert.ToDouble(_pGet_Temp_data.Rows[i]["temp_alo"]));
                    whi.Add(Convert.ToDouble(_pGet_Temp_data.Rows[i]["temp_whi"]));
                    wlo.Add(Convert.ToDouble(_pGet_Temp_data.Rows[i]["temp_wlo"]));
                }                

                cartesianChart1.Series.Add(new LineSeries
                {
                    Name = "LimitHigh",
                    Title = "Limit High",
                    Values = ahi,
                    Fill = Brushes.Transparent,
                    PointGeometrySize = 0,
                    Stroke = Brushes.DarkRed,
                    StrokeThickness = 1
                });

                cartesianChart1.Series.Add(new LineSeries
                {
                    Name = "WarningHigh",
                    Title = "Warning High",
                    Values = whi,
                    Fill = Brushes.Transparent,
                    PointGeometrySize = 0,
                    Stroke = Brushes.DarkOrange,
                    StrokeThickness = 1
                });
                
                cartesianChart1.Series.Add(new LineSeries
                {
                    Name = "TempValue",
                    Title = "Temp Value",
                    Values = temp,
                    Fill = Brushes.Transparent,
                    PointGeometrySize = 0,
                    Stroke = Brushes.DarkGreen,
                    StrokeThickness = 2
                });                

                cartesianChart1.Series.Add(new LineSeries
                {
                    Name = "WarningLow",
                    Title = "Warning Low",
                    Values = wlo,
                    Fill = Brushes.Transparent,
                    PointGeometrySize = 0,
                    Stroke = Brushes.DarkOrange,
                    StrokeThickness = 1
                });

                cartesianChart1.Series.Add(new LineSeries
                {
                    Name = "LimitLow",
                    Title = "Limi tLow",
                    Values = alo,
                    Fill = Brushes.Transparent,
                    PointGeometrySize = 0,
                    Stroke = Brushes.DarkRed,
                    StrokeThickness = 1
                });



                IList<string> labelX = new List<string>();
                for (int i = 0; i < _pGet_Temp_data.Rows.Count; i++)
                    labelX.Add(System.DateTime.Today.AddMinutes(i * 5).ToString("dd-MM-yy HH:mm"));

                cartesianChart1.AxisX.Add(new Axis
                {
                    MinValue = 0,
                    MaxValue = _pGet_Temp_data.Rows.Count,
                    Labels = labelX
                });
            }
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
                cbbSelectedName.DisplayMember = "temp_name";
                cbbSelectedName.ValueMember = "temp_number";
                cbbSelectedName.DataSource = _pGet_temp_name;
            }
        }

        private void Get_setting()
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
        {
            _pGet_setting = new DataTable();
            _pGet_setting = pGet_setting(sfrmOverview._selectedTempNoData);
            if (_pGet_setting != null)
            {
                cbbSelectedTool.DisplayMember = "tool_name";
                cbbSelectedTool.ValueMember = "tool_id";
                cbbSelectedTool.DataSource = _pGet_setting;

                cbbSelectedLocation.DisplayMember = "location_name";
                cbbSelectedLocation.ValueMember = "location_id";
                cbbSelectedLocation.DataSource = _pGet_setting;

                cbbSelectedFoor.DisplayMember = "foor_name";
                cbbSelectedFoor.ValueMember = "foor_id";
                cbbSelectedFoor.DataSource = _pGet_setting;

                cbbSelectedName.DisplayMember = "temp_name";
                cbbSelectedName.ValueMember = "temp_number";
                cbbSelectedName.DataSource = _pGet_setting;
            }
        }

        // Export DataTable into an excel file with field names in the header line
        // - Save excel file without ever making it visible if filepath is given
        // - Don't save excel file, just make it visible if no filepath is given
        private static void CreateCSVFile(ref DataTable dt, string strFilePath)
        {
            try
            {
                // Create the CSV file to which grid data will be exported.
                StreamWriter sw = new StreamWriter(strFilePath, false);
                // First we will write the headers.
                //DataTable dt = m_dsProducts.Tables[0];
                int iColCount = dt.Columns.Count;
                for (int i = 0; i < iColCount; i++)
                {
                    sw.Write(dt.Columns[i]);
                    if (i < iColCount - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);

                // Now write all the rows.
                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < iColCount; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            sw.Write(dr[i].ToString());
                        }
                        if (i < iColCount - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.Write(sw.NewLine);
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                log.Error("CreateCSVFile Exception : " + ex.Message);
                throw ex;                
            }
        }

        ////////////////////////////////////////////////////////////
        ///////////////// SQL interface section  ///////////////////
        ////////////////////////////////////////////////////////////
        private static DataTable pGet_data_with_sampling_time(int temp_number, DateTime start_date, DateTime end_date, int sampling_no)
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_data_with_sampling_time
                SqlParameterCollection param = new SqlCommand().Parameters;
                param.AddWithValue("@temp_number", SqlDbType.DateTime).Value = temp_number;
                param.AddWithValue("@start_date", SqlDbType.DateTime).Value = start_date;
                param.AddWithValue("@end_date", SqlDbType.DateTime).Value = end_date;
                param.AddWithValue("@sampling_no", SqlDbType.DateTime).Value = sampling_no;
                ds = new DBClass().SqlExcSto("pGet_data_with_sampling_time", "DbSet", param);
                dataTable = ds.Tables[0];
            }
            catch (SqlException e)
            {
                dataTable = null;
                log.Error("pGet_data_with_sampling_time SqlException : " + e.Message);
            }
            catch (Exception ex)
            {
                dataTable = null;
                log.Error("pGet_data_with_sampling_time Exception : " + ex.Message);
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
        

        private static DataTable pGet_setting(int tempNumber)
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_setting_by_no
                SqlParameterCollection param = new SqlCommand().Parameters;
                param.AddWithValue("@temp_number", SqlDbType.Int).Value = tempNumber;
                ds = new DBClass().SqlExcSto("pGet_setting_by_no", "DbSet", param);
                dataTable = ds.Tables[0];
            }
            catch (SqlException e)
            {
                dataTable = null;
                log.Error("Setting pGet_setting SqlException : " + e.Message);
            }
            catch (Exception ex)
            {
                dataTable = null;
                log.Error("Setting pGet_setting Exception : " + ex.Message);
            }
            return dataTable;
        }

        ////////////////////////////////////////////////////////////
        //////////////////////  Button event  //////////////////////
        ////////////////////////////////////////////////////////////
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                int sampling_minutes = 0;

                _pGet_Temp_data = new DataTable();
                _pGet_Temp_data = pGet_data_with_sampling_time(Convert.ToInt32(cbbSelectedName.SelectedValue), dtDateFrom.Value.Date, dtDateTo.Value, Convert.ToInt32(cbbSampling.SelectedValue));

                //Clear chart
                cartesianChart1.Series.Clear();
                cartesianChart1.AxisX.Clear();
                cartesianChart1.AxisY.Clear();

                if (_pGet_Temp_data != null && _pGet_Temp_data.Rows.Count > 0)
                {
                    cartesianChart1.Series.Clear();

                    var temp = new ChartValues<double>();
                    var ahi = new ChartValues<double>();
                    var alo = new ChartValues<double>();
                    var whi = new ChartValues<double>();
                    var wlo = new ChartValues<double>();

                    for (var i = 0; i < _pGet_Temp_data.Rows.Count; i++)
                    {
                        temp.Add(Convert.ToDouble(_pGet_Temp_data.Rows[i]["avg_temp"]));
                        ahi.Add(Convert.ToDouble(_pGet_Temp_data.Rows[i]["temp_ahi"]));
                        alo.Add(Convert.ToDouble(_pGet_Temp_data.Rows[i]["temp_alo"]));
                        whi.Add(Convert.ToDouble(_pGet_Temp_data.Rows[i]["temp_whi"]));
                        wlo.Add(Convert.ToDouble(_pGet_Temp_data.Rows[i]["temp_wlo"]));
                    }

                    cartesianChart1.Series.Add(new LineSeries
                    {
                        Name = "TempValue",
                        Title = "Temp Value",
                        Values = temp,
                        Fill = Brushes.Transparent,
                        PointGeometrySize = 0,
                        Stroke = Brushes.DarkGreen,
                        StrokeThickness = 2
                    });

                    cartesianChart1.Series.Add(new LineSeries
                    {
                        Name = "LimitHigh",
                        Title = "Limit High",
                        Values = ahi,
                        Fill = Brushes.Transparent,
                        PointGeometrySize = 0,
                        Stroke = Brushes.DarkRed,
                        StrokeThickness = 1
                    });

                    cartesianChart1.Series.Add(new LineSeries
                    {
                        Name = "LimitLow",
                        Title = "Limi tLow",
                        Values = alo,
                        Fill = Brushes.Transparent,
                        PointGeometrySize = 0,
                        Stroke = Brushes.DarkRed,
                        StrokeThickness = 1
                    });

                    cartesianChart1.Series.Add(new LineSeries
                    {
                        Name = "WarningHigh",
                        Title = "Warning High",
                        Values = whi,
                        Fill = Brushes.Transparent,
                        PointGeometrySize = 0,
                        Stroke = Brushes.DarkOrange,
                        StrokeThickness = 1
                    });

                    cartesianChart1.Series.Add(new LineSeries
                    {
                        Name = "WarningLow",
                        Title = "Warning Low",
                        Values = wlo,
                        Fill = Brushes.Transparent,
                        PointGeometrySize = 0,
                        Stroke = Brushes.DarkOrange,
                        StrokeThickness = 1
                    });

                    switch (Convert.ToInt32(cbbSampling.SelectedValue))
                    {
                        case 1: sampling_minutes = 5; break;
                        case 2: sampling_minutes = 15; break;
                        case 3: sampling_minutes = 30; break;
                        case 4: sampling_minutes = 60; break;
                        default: sampling_minutes = 5; break;
                    }

                    IList<string> labelX = new List<string>();
                    for (int i = 0; i < _pGet_Temp_data.Rows.Count; i++)
                        labelX.Add((dtDateFrom.Value.Date.AddMinutes(i * sampling_minutes)).ToString("dd-MM-yy HH:mm"));

                    cartesianChart1.AxisX.Add(new Axis
                    {
                        MinValue = 0,
                        MaxValue = _pGet_Temp_data.Rows.Count,
                        Labels = labelX
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Error("btnOk_Click Exception : " + ex.Message);
            }

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                
                // Check folder path 
                string DestinationPath = ConfigurationManager.AppSettings["ExportDestination"];
                if (!Directory.Exists(DestinationPath))
                {
                    // Create folder
                    Directory.CreateDirectory(DestinationPath);
                }

                string dt = "EXPORT_TEMP_" + cbbSelectedTool.Text + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
                string filePath = DestinationPath + dt ;
                CreateCSVFile(ref _pGet_Temp_data, filePath);

                MessageBox.Show("Data export by user");
                log.Info("Data export by user");
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Error("btnExport_Click Exception : " + ex.Message);
            }
        }

        private void dtDateFrom_ValueChanged(object sender, EventArgs e)
        {
            if (dtDateFrom.Value > dtDateTo.Value)
            {
                MessageBox.Show("DATE FROM should be less than DATE TO");
                dtDateFrom.Value = dtDateTo.Value;
            }
        }

        private void dtDateTo_ValueChanged(object sender, EventArgs e)
        {
            if (dtDateTo.Value < dtDateFrom.Value)
            {
                MessageBox.Show("DATE TO should be more than DATE FROM");
                dtDateTo.Value = dtDateFrom.Value;
            }
        }

        private void cbbSelectedName_Click(object sender, EventArgs e)
        {
            get_temp_name();
        }

        private void cbbSelectedTool_Click(object sender, EventArgs e)
        {
            get_tool_name();
        }

        private void cbbSelectedLocation_Click(object sender, EventArgs e)
        {
            get_location_name();
        }

        private void cbbSelectedFoor_Click(object sender, EventArgs e)
        {
            get_foor_name();
        }
    }
}
