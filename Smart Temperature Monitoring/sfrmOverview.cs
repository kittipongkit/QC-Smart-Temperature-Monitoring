using LiveCharts; //Core of the library
using LiveCharts.Wpf; //The WPF controls
using System.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static Smart_Temperature_Monitoring.InterfaceDB;
using Brushes = System.Windows.Media.Brushes;
using System.Collections.Generic;

namespace Smart_Temperature_Monitoring
{

    public partial class sfrmOverview : Form
    {
        //  Declare Logging
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //  Global varriable
        public static int _selectedData = 0;
        public static int _SettingZoneId = 0;
        public static int _EventZoneId = 0;

        //  Local varriable
        private static DataTable _pGet_Temp_actual = new DataTable();
        private static DataTable _pGet_setting_actual = new DataTable();
        private static DataTable _pGet_Temp_data = new DataTable();
        private static DataTable _pGet_event_all = new DataTable();

        private static int sampling_time = 5;   // sampling_time in minute
        private static int sampling_all_day = 24 * 60 / sampling_time;

        private static int actualGvCell = 0;
        private static int _EventId = 0;
        private static int _currentCntPoint = 0;       


        //  Form load
        public sfrmOverview()
        {
            InitializeComponent();
                       
        }
        private void sfrmOverview_Load(object sender, EventArgs e)
        {
            //  Thread GUI-link Config
            //System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;

            //  Intial chart & data grid view
            initTempData();

            // clear selection on status
            gvData1.ClearSelection();
            gvData2.ClearSelection();
            gvData3.ClearSelection();


            //Create Thread threadSamplingTime --> Update Actual temp. & Trend & Grid status
            Thread threadSamplingTime = new Thread(ThreadSamplingTime);
            threadSamplingTime.IsBackground = true;
            threadSamplingTime.Start();

            //  Create Thread threadUpdateSetting --> Update setting
            Thread threadUpdateSetting = new Thread(ThreadUpdateSetting);
            threadUpdateSetting.IsBackground = true;
            threadUpdateSetting.Start();

        }


        //  Thread Portion
        private void ThreadSamplingTime()
        {
            while (true)
            {
                try
                {
                    _actualTemp();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ThreadSamplingTime Exception : " + ex.Message);
                    log.Error("ThreadSamplingTime Exception : " + ex.Message);
                }
                finally
                {
                    //  Delay
                    Thread.Sleep(1000);
                }
            }
        }
        private void ThreadUpdateSetting()
        {
            while (true)
            {
                try
                {
                    _actual_setting();
                    _get_event_all();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ThreadUpdateSetting Exception : " + ex.Message);
                    log.Error("ThreadUpdateSetting Exception : " + ex.Message);
                }
                finally
                {
                    //  Delay
                    Thread.Sleep(5000);
                }
            }
        }


        //  Display function

        // Run every sampling time : plot data 1 sampling
        public void _actualTemp()
        {                        
            //  EDIT 2020-02-10 : Check _currentCntPoint
            //  ADD AUTO CHECK DATA POINT OF LAST ROW FOR UPDATE
            int no_pGet_Temp_data = 0;
            DataTable dt = pGet_Temp_data();  // Get today data
            if (dt != null && dt.Rows.Count > 0)
                no_pGet_Temp_data = dt.Rows.Count; // if have today data --> no_pGet_Temp_data = today data count
            else
            {
                if (actualGvCell != 0)
                    clearAndInit();
                return;
            }

            _pGet_Temp_actual = new DataTable();
            _pGet_Temp_actual = pGet_Temp_actual();

            if (_pGet_Temp_actual != null && _pGet_Temp_actual.Rows.Count > 0)
            { 
                // actual temp
                lbValue1.Text = _pGet_Temp_actual.Rows[0]["temp1"].ToString();
                lbValue2.Text = _pGet_Temp_actual.Rows[0]["temp2"].ToString();
                lbValue3.Text = _pGet_Temp_actual.Rows[0]["temp3"].ToString();

                // change background coler
                if ((_pGet_Temp_actual.Rows[0]["temp1_result"]).ToString() == "OK")
                {
                    panelMain1.BackColor = Color.FromArgb(128, 255, 128);
                    lbZone1.BackColor = Color.FromArgb(0, 192, 0);
                }
                else
                {
                    panelMain1.BackColor = Color.FromArgb(255, 128, 128);
                    lbZone1.BackColor = Color.Red;
                }

                if ((_pGet_Temp_actual.Rows[0]["temp2_result"]).ToString() == "OK")
                {
                    panelMain2.BackColor = Color.FromArgb(128, 255, 128);
                    lbZone2.BackColor = Color.FromArgb(0, 192, 0);
                }
                else
                {
                    panelMain2.BackColor = Color.FromArgb(255, 128, 128);
                    lbZone2.BackColor = Color.Red;
                }

                if ((_pGet_Temp_actual.Rows[0]["temp3_result"]).ToString() == "OK")
                {
                    panelMain3.BackColor = Color.FromArgb(128, 255, 128);
                    lbZone3.BackColor = Color.FromArgb(0, 192, 0);
                }
                else
                {
                    panelMain3.BackColor = Color.FromArgb(255, 128, 128);
                    lbZone3.BackColor = Color.Red;
                }

                // Check new data
                if (_currentCntPoint != no_pGet_Temp_data)
                    _currentCntPoint = no_pGet_Temp_data;
                else
                    return;   // Not have new data  

                // plot graph
                if (chTemp1.Series[0].Values.Count <= sampling_all_day)
                {
                    chTemp1.Series[0].Values.Add(Convert.ToDouble((_pGet_Temp_actual.Rows[0]["temp1"])));
                    chTemp1.Series[1].Values.Add(Convert.ToDouble((_pGet_Temp_actual.Rows[0]["temp1_hi"])));
                    chTemp1.Series[2].Values.Add(Convert.ToDouble((_pGet_Temp_actual.Rows[0]["temp1_lo"])));

                    chTemp2.Series[0].Values.Add(Convert.ToDouble((_pGet_Temp_actual.Rows[0]["temp2"])));
                    chTemp2.Series[1].Values.Add(Convert.ToDouble((_pGet_Temp_actual.Rows[0]["temp2_hi"])));
                    chTemp2.Series[2].Values.Add(Convert.ToDouble((_pGet_Temp_actual.Rows[0]["temp2_lo"])));

                    chTemp3.Series[0].Values.Add(Convert.ToDouble((_pGet_Temp_actual.Rows[0]["temp3"])));
                    chTemp3.Series[1].Values.Add(Convert.ToDouble((_pGet_Temp_actual.Rows[0]["temp3_hi"])));
                    chTemp3.Series[2].Values.Add(Convert.ToDouble((_pGet_Temp_actual.Rows[0]["temp3_lo"])));

                }

                // plot status
                try
                {
                    if ((_pGet_Temp_actual.Rows[0]["temp1_result"]).ToString() == "NG")
                    {
                        gvData1.Rows[0].Cells[actualGvCell].Style.BackColor = Color.FromArgb(255, 128, 128);
                        gvData1.Rows[0].Cells[actualGvCell].Style.ForeColor = Color.FromArgb(255, 128, 128);
                    }
                    else
                    {
                        gvData1.Rows[0].Cells[actualGvCell].Style.BackColor = Color.FromArgb(128, 255, 128);
                        gvData1.Rows[0].Cells[actualGvCell].Style.ForeColor = Color.FromArgb(128, 255, 128);
                    }

                    if ((_pGet_Temp_actual.Rows[0]["temp2_result"]).ToString() == "NG")
                    {
                        gvData2.Rows[0].Cells[actualGvCell].Style.BackColor = Color.FromArgb(255, 128, 128);
                        gvData2.Rows[0].Cells[actualGvCell].Style.ForeColor = Color.FromArgb(255, 128, 128);
                    }
                    else
                    {
                        gvData2.Rows[0].Cells[actualGvCell].Style.BackColor = Color.FromArgb(128, 255, 128);
                        gvData2.Rows[0].Cells[actualGvCell].Style.ForeColor = Color.FromArgb(128, 255, 128);
                    }

                    if ((_pGet_Temp_actual.Rows[0]["temp3_result"]).ToString() == "NG")
                    {
                        gvData3.Rows[0].Cells[actualGvCell].Style.BackColor = Color.FromArgb(255, 128, 128);
                        gvData3.Rows[0].Cells[actualGvCell].Style.ForeColor = Color.FromArgb(255, 128, 128);
                    }
                    else
                    {
                        gvData3.Rows[0].Cells[actualGvCell].Style.BackColor = Color.FromArgb(128, 255, 128);
                        gvData3.Rows[0].Cells[actualGvCell].Style.ForeColor = Color.FromArgb(128, 255, 128);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ThreadSamplingTime Exception (gvData1) : " + ex.Message);
                    log.Error("ThreadSamplingTime Exception (gvData1)  : " + ex.Message);
                }

                actualGvCell += 1;
            }
        }

        // Get real-time setting
        public void _actual_setting()
        {
            _pGet_setting_actual = new DataTable();
            _pGet_setting_actual = pGet_setting_actual();
            if (_pGet_setting_actual != null && _pGet_setting_actual.Rows.Count > 0)
            {
                lbZone1.Text = _pGet_setting_actual.Rows[0]["zone_name"].ToString();
                lbLow1.Text = _pGet_setting_actual.Rows[0]["limit_low"].ToString();
                lbHigh1.Text = _pGet_setting_actual.Rows[0]["limit_hi"].ToString();

                lbZone2.Text = _pGet_setting_actual.Rows[1]["zone_name"].ToString();
                lbLow2.Text = _pGet_setting_actual.Rows[1]["limit_low"].ToString();
                lbHigh2.Text = _pGet_setting_actual.Rows[1]["limit_hi"].ToString();

                lbZone3.Text = _pGet_setting_actual.Rows[2]["zone_name"].ToString();
                lbLow3.Text = _pGet_setting_actual.Rows[2]["limit_low"].ToString();
                lbHigh3.Text = _pGet_setting_actual.Rows[2]["limit_hi"].ToString();
            }
        }

        // Get real-time event
        public void _get_event_all()
        {
            _pGet_event_all = new DataTable();
            _pGet_event_all = pGet_event_all();

            if (_pGet_event_all != null)
            {
                if (_EventId != Convert.ToInt32(_pGet_event_all.Rows[0]["ID"]))
                {

                    //  Clear gv
                    gvEventAll.Rows.Clear();

                    // Plot data to gridView
                    for (int i = 0; i < _pGet_event_all.Rows.Count; i++)
                        gvEventAll.Rows.Add(_pGet_event_all.Rows[i]["create_datetime"], _pGet_event_all.Rows[i]["zone_name"], _pGet_event_all.Rows[i]["event_detail"]);

                    // Keep Id for check next time
                    _EventId = Convert.ToInt32(_pGet_event_all.Rows[0]["ID"]);

                    gvEventAll.ClearSelection();
                }
            }
        }

        // First time start up : plot data all day
        public void initTempData()
        {
            DataTable dt = pGet_Temp_data();  // Get today data
            if (dt != null && dt.Rows.Count > 0)
                _currentCntPoint = dt.Rows.Count; // if have today data --> no_pGet_Temp_data = today data count

            _pGet_Temp_data = new DataTable();
            _pGet_Temp_data = pGet_Temp_data();
            if (_pGet_Temp_data != null && _pGet_Temp_data.Rows.Count > 0)
            {
                var values1 = new ChartValues<double>();
                var values2 = new ChartValues<double>();
                var values3 = new ChartValues<double>();

                var hi1 = new ChartValues<double>();
                var lo1 = new ChartValues<double>();

                var hi2 = new ChartValues<double>();
                var lo2 = new ChartValues<double>();

                var hi3 = new ChartValues<double>();
                var lo3 = new ChartValues<double>();

                

                // plot graph
                for (var i = 0; (i < _pGet_Temp_data.Rows.Count && i < sampling_all_day); i++)
                {
                    values1.Add(Convert.ToDouble(_pGet_Temp_data.Rows[i]["temp1"]));
                    hi1.Add(Convert.ToDouble(_pGet_Temp_data.Rows[i]["temp1_hi"]));
                    lo1.Add(Convert.ToDouble(_pGet_Temp_data.Rows[i]["temp1_lo"]));

                    values2.Add(Convert.ToDouble(_pGet_Temp_data.Rows[i]["temp2"]));
                    hi2.Add(Convert.ToDouble(_pGet_Temp_data.Rows[i]["temp2_hi"]));
                    lo2.Add(Convert.ToDouble(_pGet_Temp_data.Rows[i]["temp2_lo"]));

                    values3.Add(Convert.ToDouble(_pGet_Temp_data.Rows[i]["temp3"]));
                    hi3.Add(Convert.ToDouble(_pGet_Temp_data.Rows[i]["temp3_hi"]));
                    lo3.Add(Convert.ToDouble(_pGet_Temp_data.Rows[i]["temp3_lo"]));
                }

                chTemp1.Series.Add(new LineSeries
                {
                    Name = "Actual",
                    Title = "Actual",
                    Values = values1,
                    Fill = Brushes.Transparent,
                    PointGeometrySize = 0,
                    StrokeThickness = 2
                });

                chTemp1.Series.Add(new LineSeries
                {
                    Name = "LimitHigh",
                    Title = "Limit High",
                    Values = hi1,
                    Fill = Brushes.Transparent,
                    PointGeometrySize = 0,
                    Stroke = Brushes.Salmon,
                    StrokeThickness = 1
                });

                chTemp1.Series.Add(new LineSeries
                {
                    Name = "LimitLow",
                    Title = "Limit Low",
                    Values = lo1,
                    Fill = Brushes.Transparent,
                    PointGeometrySize = 0,
                    Stroke = Brushes.Salmon,
                    StrokeThickness = 1
                });

                chTemp2.Series.Add(new LineSeries
                {
                    Name = "Actual",
                    Title = "Actual",
                    Values = values2,
                    Fill = Brushes.Transparent,
                    PointGeometrySize = 0,
                    StrokeThickness = 2
                });

                chTemp2.Series.Add(new LineSeries
                {
                    Name = "LimitHigh",
                    Title = "Limit High",
                    Values = hi2,
                    Fill = Brushes.Transparent,
                    PointGeometrySize = 0,
                    Stroke = Brushes.Salmon,
                    StrokeThickness = 1
                });

                chTemp2.Series.Add(new LineSeries
                {
                    Name = "LimitLow",
                    Title = "Limit Low",
                    Values = lo2,
                    Fill = Brushes.Transparent,
                    PointGeometrySize = 0,
                    Stroke = Brushes.Salmon,
                    StrokeThickness = 1
                });

                chTemp3.Series.Add(new LineSeries
                {
                    Name = "Actual",
                    Title = "Actual",
                    Values = values3,
                    Fill = Brushes.Transparent,
                    PointGeometrySize = 0,
                    StrokeThickness = 2
                });

                chTemp3.Series.Add(new LineSeries
                {
                    Name = "LimitHigh",
                    Title = "Limit High",
                    Values = hi3,
                    Fill = Brushes.Transparent,
                    PointGeometrySize = 0,
                    Stroke = Brushes.Salmon,
                    StrokeThickness = 1
                });

                chTemp3.Series.Add(new LineSeries
                {
                    Name = "LimitLow",
                    Title = "Limit Low",
                    Values = lo3,
                    Fill = Brushes.Transparent,
                    PointGeometrySize = 0,
                    Stroke = Brushes.Salmon,
                    StrokeThickness = 1
                });

                IList<string> labelX = new List<string>();
                for (int i = 0; i <= sampling_all_day; i++)
                    labelX.Add(System.DateTime.MinValue.AddMinutes(i * sampling_time).ToString("HH:mm"));

                chTemp1.AxisX.Add(new Axis
                {
                    MinValue = 0,
                    MaxValue = 288,
                    Labels = labelX
                });

                //chTemp1.AxisY.Add(new Axis
                //{
                //    MinValue = 20,
                //    MaxValue = 30
                //});

                chTemp2.AxisX.Add(new Axis
                {
                    MinValue = 0,
                    MaxValue = 288,
                    Labels = labelX
                });

                //chTemp2.AxisY.Add(new Axis
                //{
                //    MinValue = 20,
                //    MaxValue = 30
                //});

                chTemp3.AxisX.Add(new Axis
                {
                    MinValue = 0,
                    MaxValue = 288,
                    Labels = labelX
                });

                //chTemp3.AxisY.Add(new Axis
                //{
                //    MinValue = 20,
                //    MaxValue = 30
                //});


                // plot gv status

                //Declare array for keep data
                string[] status1 = new string[sampling_all_day];
                string[] status2 = new string[sampling_all_day];
                string[] status3 = new string[sampling_all_day];

                //Keep data to array
                for (int i = 0; i < _pGet_Temp_data.Rows.Count && i < sampling_all_day; i++)
                {
                    status1[i] = _pGet_Temp_data.Rows[i]["temp1_result"].ToString();
                    status2[i] = _pGet_Temp_data.Rows[i]["temp2_result"].ToString();
                    status3[i] = _pGet_Temp_data.Rows[i]["temp3_result"].ToString();
                }

                gvData1.Rows.Clear();
                gvData2.Rows.Clear();
                gvData3.Rows.Clear();

                //Add array to DataGridView
                gvData1.Rows.Add(status1);
                gvData2.Rows.Add(status2);
                gvData3.Rows.Add(status3);

                for (int i = 0; i < _pGet_Temp_data.Rows.Count && i < sampling_all_day; i++)
                {
                    if (status1[i] == "NG")
                    {
                        gvData1.Rows[0].Cells[i].Style.BackColor = Color.FromArgb(255, 128, 128);
                        gvData1.Rows[0].Cells[i].Style.ForeColor = Color.FromArgb(255, 128, 128);
                    }
                    else
                    {
                        gvData1.Rows[0].Cells[i].Style.BackColor = Color.FromArgb(128, 255, 128);
                        gvData1.Rows[0].Cells[i].Style.ForeColor = Color.FromArgb(128, 255, 128);
                    }

                    if (status2[i] == "NG")
                    {
                        gvData2.Rows[0].Cells[i].Style.BackColor = Color.FromArgb(255, 128, 128);
                        gvData2.Rows[0].Cells[i].Style.ForeColor = Color.FromArgb(255, 128, 128);
                    }
                    else
                    {
                        gvData2.Rows[0].Cells[i].Style.BackColor = Color.FromArgb(128, 255, 128);
                        gvData2.Rows[0].Cells[i].Style.ForeColor = Color.FromArgb(128, 255, 128);
                    }

                    if (status3[i] == "NG")
                    {
                        gvData3.Rows[0].Cells[i].Style.BackColor = Color.FromArgb(255, 128, 128);
                        gvData3.Rows[0].Cells[i].Style.ForeColor = Color.FromArgb(255, 128, 128);
                    }
                    else
                    {
                        gvData3.Rows[0].Cells[i].Style.BackColor = Color.FromArgb(128, 255, 128);
                        gvData3.Rows[0].Cells[i].Style.ForeColor = Color.FromArgb(128, 255, 128);
                    }

                    actualGvCell = _pGet_Temp_data.Rows.Count;
                }

            }

        }

        // First time of day : clear and intial chart & status
        private void clearAndInit()
        {
            string[] status1 = new string[] { };

            // Clear data grid view
            gvData1.Rows.Clear();
            gvData2.Rows.Clear();
            gvData3.Rows.Clear();

            // Add empty list into data grid view
            gvData1.Rows.Add(status1);
            gvData1.ClearSelection();

            gvData2.Rows.Add(status1);
            gvData2.ClearSelection();

            gvData3.Rows.Add(status1);
            gvData3.ClearSelection();

            actualGvCell = 0;

            // Clear chart series
            chTemp1.Series[0].Values.Clear();
            chTemp1.Series[1].Values.Clear();
            chTemp1.Series[2].Values.Clear();

            chTemp2.Series[0].Values.Clear();
            chTemp2.Series[1].Values.Clear();
            chTemp2.Series[2].Values.Clear();

            chTemp3.Series[0].Values.Clear();
            chTemp3.Series[1].Values.Clear();
            chTemp3.Series[2].Values.Clear();

            // Add data to initial series
            chTemp1.Series[0].Values.Add(Convert.ToDouble((_pGet_Temp_actual.Rows[0]["temp1"])));
            chTemp1.Series[1].Values.Add(Convert.ToDouble((_pGet_Temp_actual.Rows[0]["temp1_hi"])));
            chTemp1.Series[2].Values.Add(Convert.ToDouble((_pGet_Temp_actual.Rows[0]["temp1_lo"])));

            chTemp2.Series[0].Values.Add(Convert.ToDouble((_pGet_Temp_actual.Rows[0]["temp2"])));
            chTemp2.Series[1].Values.Add(Convert.ToDouble((_pGet_Temp_actual.Rows[0]["temp2_hi"])));
            chTemp2.Series[2].Values.Add(Convert.ToDouble((_pGet_Temp_actual.Rows[0]["temp2_lo"])));

            chTemp3.Series[0].Values.Add(Convert.ToDouble((_pGet_Temp_actual.Rows[0]["temp3"])));
            chTemp3.Series[1].Values.Add(Convert.ToDouble((_pGet_Temp_actual.Rows[0]["temp3_hi"])));
            chTemp3.Series[2].Values.Add(Convert.ToDouble((_pGet_Temp_actual.Rows[0]["temp3_lo"])));
        }

        ////////////////////////////////////////////////////////////
        ///////////////// SQL interface section  ///////////////////
        ////////////////////////////////////////////////////////////
        private static DataTable pGet_Temp_actual()
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_Temp_actual
                SqlParameterCollection param = new SqlCommand().Parameters;
                ds = new DBClass().SqlExcSto("pGet_Temp_actual", "DbSet", param);
                dataTable = ds.Tables[0];
            }
            catch (SqlException e)
            {
                dataTable = null;
                log.Error("pGet_Temp_actual SqlException : " + e.Message);
            }
            catch (Exception ex)
            {
                dataTable = null;
                log.Error("pGet_Temp_actual Exception : " + ex.Message);
            }
            return dataTable;
        }

        private static DataTable pGet_Temp_data()
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_Temp_data
                SqlParameterCollection param = new SqlCommand().Parameters;
                ds = new DBClass().SqlExcSto("pGet_Temp_data", "DbSet", param);
                dataTable = ds.Tables[0];
            }
            catch (SqlException e)
            {
                dataTable = null;
                log.Error("pGet_Temp_data SqlException : " + e.Message);
            }
            catch (Exception ex)
            {
                dataTable = null;
                log.Error("pGet_Temp_data Exception : " + ex.Message);
            }
            return dataTable;
        }

        private static DataTable pGet_setting_actual()
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_actual_value
                SqlParameterCollection param = new SqlCommand().Parameters;
                ds = new DBClass().SqlExcSto("pGet_setting_actual", "DbSet", param);
                dataTable = ds.Tables[0];
            }
            catch (SqlException e)
            {
                dataTable = null;
                log.Error("pGet_setting_actual SqlException : " + e.Message);
            }
            catch (Exception ex)
            {
                dataTable = null;
                log.Error("pGet_setting_actual Exception : " + ex.Message);
            }
            return dataTable;
        }

        private static DataTable pGet_event_all()
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_actual_value
                SqlParameterCollection param = new SqlCommand().Parameters;
                ds = new DBClass().SqlExcSto("pGet_event_all", "DbSet", param);
                dataTable = ds.Tables[0];
            }
            catch (SqlException e)
            {
                dataTable = null;
                log.Error("pGet_event_all SqlException : " + e.Message);
            }
            catch (Exception ex)
            {
                dataTable = null;
                log.Error("pGet_event_all Exception : " + ex.Message);
            }
            return dataTable;
        }

        ////////////////////////////////////////////////////////////
        //////////////////////  Button event  //////////////////////
        ////////////////////////////////////////////////////////////
        private void btnEven1_Click(object sender, EventArgs e)
        {
            _EventZoneId = 1;
            sfrmEvent1 sfrmEvent1 = new sfrmEvent1();
            sfrmEvent1.Show();
        }

        private void btnEven2_Click(object sender, EventArgs e)
        {
            _EventZoneId = 2;
            sfrmEvent1 sfrmEvent1 = new sfrmEvent1();
            sfrmEvent1.Show();
        }

        private void btnEven3_Click(object sender, EventArgs e)
        {
            _EventZoneId = 3;
            sfrmEvent1 sfrmEvent1 = new sfrmEvent1();
            sfrmEvent1.Show();
        }

        private void btnReport1_Click(object sender, EventArgs e)
        {
            sfrmReport1 sfrmExport1 = new sfrmReport1();
            sfrmExport1.Show();
        }

        private void btnReport2_MouseClick(object sender, MouseEventArgs e)
        {
            sfrmReport1 sfrmExport1 = new sfrmReport1();
            sfrmExport1.Show();
        }

        private void btnReport3_MouseClick(object sender, MouseEventArgs e)
        {
            sfrmReport1 sfrmExport1 = new sfrmReport1();
            sfrmExport1.Show();
        }
        private void btnData1_Click(object sender, EventArgs e)
        {
            _selectedData = 1;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }
        private void btnData2_Click(object sender, EventArgs e)
        {
            _selectedData = 2;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }
        private void btnData3_Click(object sender, EventArgs e)
        {
            _selectedData = 3;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void btnSetting1_Click(object sender, EventArgs e)
        {
            _SettingZoneId = 1;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }
        private void btnSetting2_Click(object sender, EventArgs e)
        {
            _SettingZoneId = 2;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }
        private void btnSetting3_Click(object sender, EventArgs e)
        {
            _SettingZoneId = 3;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        //  Local function
        private void LineNotifyMsg(string lineToken, string message)
        {
            try
            {
                //  hj1TGTJOwYq8L78D2fYbhPKQhOAsgaG1KfJ1QRLa3Tb
                //message = System.Web.HttpUtility.UrlEncode(message, Encoding.UTF8);
                var request = (HttpWebRequest)WebRequest.Create("https://notify-api.line.me/api/notify");
                var postData = string.Format("message={0}", message);
                var data = Encoding.UTF8.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                request.Headers.Add("Authorization", "Bearer " + lineToken);
                var stream = request.GetRequestStream();
                stream.Write(data, 0, data.Length);
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //  Label test line notify
        private void lbHigh_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var label = (Label)sender;
            string zone = string.Empty;
            if (label.Name == "lbHigh1") { zone = "Zone A"; }
            else if (label.Name == "lbHigh2") { zone = "Zone B"; }
            else if (label.Name == "lbHigh3") { zone = "Zone C"; }

            string message = string.Format("\r\n------Temperature Over Notify !!!------\r\n" +
                "Datetime : {0}\r\n" +
                "{1} has value over than {2}.", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                , zone, label.Text);
            LineNotifyMsg("hj1TGTJOwYq8L78D2fYbhPKQhOAsgaG1KfJ1QRLa3Tb", message);
        }
        private void lbLow_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var label = (Label)sender;
            string zone = string.Empty;
            if (label.Name == "lbLow1") { zone = "Zone A"; }
            else if (label.Name == "lbLow2") { zone = "Zone B"; }
            else if (label.Name == "lbLow3") { zone = "Zone C"; }

            string message = string.Format("\r\n------Temperature Lower Notify !!!------\r\n" +
                "Datetime : {0}\r\n" +
                "{1} has value lower than {2}.", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                , zone, label.Text);
            LineNotifyMsg("hj1TGTJOwYq8L78D2fYbhPKQhOAsgaG1KfJ1QRLa3Tb", message);
        }
        private void lbValue_DoubleClick(object sender, EventArgs e)
        {
            var label = (Label)sender;
            string zone = string.Empty;
            if (label.Name == "lbValue1") { zone = "Zone A"; }
            else if (label.Name == "lbValue2") { zone = "Zone B"; }
            else if (label.Name == "lbValue3") { zone = "Zone C"; }

            string message = string.Format("\r\n------Temperature Back Notify !!!------\r\n" +
                "Datetime : {0}\r\n" +
                "{1} has value back to lenght at {2}.", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                , zone, label.Text);
            LineNotifyMsg("hj1TGTJOwYq8L78D2fYbhPKQhOAsgaG1KfJ1QRLa3Tb", message);
        }




    }
}
