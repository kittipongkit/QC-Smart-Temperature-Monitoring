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
        private static DataTable _pGet_zone_name = new DataTable();
        public sfrmData1()
        {
            InitializeComponent();
            get_zone_name();
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
            _pGet_Temp_data = new DataTable();
            _pGet_Temp_data = pGet_Temp_Range(Convert.ToInt32(cbbSelectedZone.SelectedValue), dtDateFrom.Value.Date, dtDateTo.Value, Convert.ToInt32(cbbSampling.SelectedValue));
            if (_pGet_Temp_data != null && _pGet_Temp_data.Rows.Count > 0)
            {
                var values1 = new ChartValues<double>();
                for (var i = 0; i < _pGet_Temp_data.Rows.Count; i++)
                {
                    values1.Add(Convert.ToDouble(_pGet_Temp_data.Rows[i]["avg_temp"]));
                }

                cartesianChart1.Series.Add(new LineSeries
                {
                    Name = "TempValue",
                    Title = "Temp Value",
                    Values = values1
                });

                IList<string> labelX = new List<string>();
                for (int i = 0; i <= _pGet_Temp_data.Rows.Count; i++)
                    labelX.Add(System.DateTime.Today.AddMinutes(i * 5).ToString("dd-MM-yy HH:mm"));

                cartesianChart1.AxisX.Add(new Axis
                {
                    MinValue = 0,
                    MaxValue = _pGet_Temp_data.Rows.Count,
                    Labels = labelX
                });
            }
        }

        private void get_zone_name()
        {
            
            _pGet_zone_name = new DataTable();
            _pGet_zone_name = pGet_zone_name();
            if (_pGet_zone_name != null)
            {
                cbbSelectedZone.DisplayMember = "zone_name";
                cbbSelectedZone.ValueMember = "zone_id";
                cbbSelectedZone.DataSource = _pGet_zone_name;
                cbbSelectedZone.SelectedValue = sfrmOverview._selectedData;
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
        private static DataTable pGet_Temp_Range(int zone_id, DateTime start_date, DateTime end_date, int sampling_no)
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_actual_value 2022-01-15 00:00:00.000 pGet_Temp_data
                SqlParameterCollection param = new SqlCommand().Parameters;
                param.AddWithValue("@zone_id", SqlDbType.DateTime).Value = zone_id;
                param.AddWithValue("@start_date", SqlDbType.DateTime).Value = start_date;
                param.AddWithValue("@end_date", SqlDbType.DateTime).Value = end_date;
                param.AddWithValue("@sampling_no", SqlDbType.DateTime).Value = sampling_no;
                ds = new DBClass().SqlExcSto("pGet_data_with_sampling_time", "DbSet", param);
                dataTable = ds.Tables[0];
            }
            catch (SqlException e)
            {
                dataTable = null;
                log.Error("pGet_Temp_Range SqlException : " + e.Message);
            }
            catch (Exception ex)
            {
                dataTable = null;
                log.Error("pGet_Temp_Range Exception : " + ex.Message);
            }
            return dataTable;
        }

        private static DataTable pGet_zone_name()
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_zone_name
                SqlParameterCollection param = new SqlCommand().Parameters;
                ds = new DBClass().SqlExcSto("pGet_zone_name", "DbSet", param);
                dataTable = ds.Tables[0];

            }
            catch (SqlException e)
            {
                dataTable = null;
                log.Error("pGet_zone_name SqlException : " + e.Message);
            }
            catch (Exception ex)
            {
                dataTable = null;
                log.Error("pGet_zone_name Exception : " + ex.Message);
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
                _pGet_Temp_data = pGet_Temp_Range(Convert.ToInt32(cbbSelectedZone.SelectedValue), dtDateFrom.Value.Date, dtDateTo.Value, Convert.ToInt32(cbbSampling.SelectedValue));

                //Clear chart
                cartesianChart1.Series.Clear();
                cartesianChart1.AxisX.Clear();
                cartesianChart1.AxisY.Clear();

                if (_pGet_Temp_data != null && _pGet_Temp_data.Rows.Count > 0)
                {
                    cartesianChart1.Series.Clear();
                    var values1 = new ChartValues<double>();
                    for (var i = 0; i < _pGet_Temp_data.Rows.Count; i++)
                    {
                        values1.Add(Convert.ToDouble(_pGet_Temp_data.Rows[i]["avg_temp"]));
                    }

                    cartesianChart1.Series.Add(new LineSeries
                    {
                        Name = "TempValue",
                        Title = "Temp Value",
                        Values = values1,
                        //Fill = Brushes.Transparent,
                        PointGeometrySize = 0,
                        StrokeThickness = 2
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
                    for (int i = 0; i <= _pGet_Temp_data.Rows.Count; i++)
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

                string dt = "EXPORT_TEMP_EX1" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
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
    }
}
