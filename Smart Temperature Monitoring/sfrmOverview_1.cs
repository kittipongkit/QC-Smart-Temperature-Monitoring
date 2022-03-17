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

    public partial class sfrmOverview_1 : Form
    {
        //  Declare Logging
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //  Global varriable
        public static int _selectedTempNoData = 0;
        public static int _SettingNumber = 0;
        public static int _EventTool = 0;
        public static int _SettingTool = 0;
        public static int _DataTool = 0;
        public static int _ReportTool = 0;

        //  Local varriable
        private static DataTable _pGet_Temp_actual = new DataTable();
        private static DataTable _pGet_setting_actual = new DataTable();
        private static DataTable _pGet_Temp_data = new DataTable();
        private static DataTable _pGet_event_all = new DataTable();
        private static DataTable _pGet_status = new DataTable();
        private static DataTable _pGet_status_tool = new DataTable();

        private static int sampling_time = 5;   // sampling_time in minute
        private static int sampling_all_day = 24 * 60 / sampling_time;

        private static int actualGvCell = 0;
        private static int _EventId = 0;
        private static int _currentCntPoint = 0;       


        //  Form load
        public sfrmOverview_1()
        {
            InitializeComponent();
                       
        }
        private void sfrmOverview_Load(object sender, EventArgs e)
        {
            //  Thread GUI-link Config
            //System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;

            //  Intial chart & data grid view
            //initTempData();

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
                    _actual_setting();
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
                    _get_event_all();
                    _get_status();
                    _get_status_tool();
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
        public void _actualTemp()
        {                       
            //declare list to keep data
            List<DataTable> tempdata = new List<DataTable>();

            //keep data to list
            for(int i=1; i<=35; i++)
            {
                _pGet_Temp_actual = new DataTable();
                _pGet_Temp_actual = pGet_Temp_actual(i);
                if (_pGet_Temp_actual != null && _pGet_Temp_actual.Rows.Count > 0)
                {
                    tempdata.Add(_pGet_Temp_actual);
                }      
            }

            //read data from list
            for (int i=1; i<=35; i++)
            {
                //change temp value
                var lbTemp = Controls.Find("lbTemp" + i, true);
                if (lbTemp.Length > 0)
                {
                    var label = (Label)lbTemp[0];
                    label.Text = tempdata[i-1].Rows[0]["temp_actual"].ToString();
                }

                //change background color
                DataTable dt = pGet_setting_actual();
                if(dt.Rows[i-1]["temp_active"].ToString() == "Y")  //check temp_active 
                {
                    //panel
                    var pnTemp = Controls.Find("pnTemp" + i, true);
                    if (pnTemp.Length > 0)
                    {
                        var panel = (Panel)pnTemp[0];                        
                        if (tempdata[i - 1].Rows[0]["temp_result"].ToString() == "OK")
                            panel.BackColor = Color.FromArgb(128, 255, 128);
                        else if ((tempdata[i - 1].Rows[0]["temp_result"].ToString() == "WH") || (tempdata[i - 1].Rows[0]["temp_result"].ToString() == "WL"))
                            panel.BackColor = Color.FromArgb(255, 192, 128);
                        else
                            panel.BackColor = Color.FromArgb(255, 128, 128);
                    }

                    //lbName
                    var lbName = Controls.Find("lbName" + i, true);
                    if (lbName.Length > 0)
                    {
                        var label = (Label)lbName[0];
                        if (tempdata[i - 1].Rows[0]["temp_result"].ToString() == "OK")
                            label.BackColor = Color.FromArgb(0, 192, 0);
                        else if ((tempdata[i - 1].Rows[0]["temp_result"].ToString() == "WH") || (tempdata[i - 1].Rows[0]["temp_result"].ToString() == "WL"))
                            label.BackColor = Color.FromArgb(255, 128, 0);
                        else
                            label.BackColor = Color.Red;
                    }

                    //lbFoor
                    var lbFoor = Controls.Find("lbFoor" + i, true);
                    if (lbFoor.Length > 0)
                    {
                        var label = (Label)lbFoor[0];
                        if (tempdata[i - 1].Rows[0]["temp_result"].ToString() == "OK")
                            label.BackColor = Color.FromArgb(128, 255, 128);
                        else if ((tempdata[i - 1].Rows[0]["temp_result"].ToString() == "WH") || (tempdata[i - 1].Rows[0]["temp_result"].ToString() == "WL"))
                            label.BackColor = Color.FromArgb(255, 192, 128);
                        else
                            label.BackColor = Color.FromArgb(255, 128, 128);
                    }
                }                         
            }
        }

        // Get real-time setting
        public void _actual_setting()
        {           
            _pGet_setting_actual = new DataTable();
            _pGet_setting_actual = pGet_setting_actual();
            if (_pGet_setting_actual != null && _pGet_setting_actual.Rows.Count > 0)
            {
                //read data from list 
                for (int i = 1; i <= 35; i++) 
                {
                    //change panel background color 
                    var pnTemp = Controls.Find("pnTemp" + i, true);
                    if (pnTemp.Length > 0)
                    {                        
                        if (_pGet_setting_actual.Rows[i - 1]["temp_active"].ToString() == "N")
                        {
                            var panel = (Panel)pnTemp[0];
                            panel.BackColor = Color.FromArgb(224, 224, 224);
                        }
                    }

                    //change temp setting name
                    var lbName = Controls.Find("lbName" + i, true);
                    if (lbName.Length > 0)
                    {
                        var label = (Label)lbName[0];
                        label.Text = _pGet_setting_actual.Rows[i - 1]["temp_name"].ToString();

                        if (_pGet_setting_actual.Rows[i - 1]["temp_active"].ToString() == "N")
                        {
                            label.BackColor = Color.Gray;
                        }
                    }

                    //change temp foor_name 
                    var lbFoor = Controls.Find("lbFoor" + i, true);
                    if (lbFoor.Length > 0)
                    {
                        var label = (Label)lbFoor[0];
                        label.Text = _pGet_setting_actual.Rows[i - 1]["foor_name"].ToString();

                        if (_pGet_setting_actual.Rows[i - 1]["temp_active"].ToString() == "N")
                        {
                            label.BackColor = Color.FromArgb(224, 224, 224);
                        }
                    }

                    //change temp setting warning hi and visible
                    var lbWH = Controls.Find("lbWH" + i, true);
                    var label_lbWH = (Label)lbWH[0];
                    if (lbWH.Length > 0)
                    {
                        if (_pGet_setting_actual.Rows[i - 1]["enable_warning_hi"].ToString() == "Y")
                        {                            
                            label_lbWH.Text = _pGet_setting_actual.Rows[i - 1]["warning_hi"].ToString();
                            label_lbWH.Visible = true;
                        }
                        else
                        {
                            label_lbWH.Visible = false;
                        }
                    }

                    //change temp setting warning low and visible
                    var lbWL = Controls.Find("lbWL" + i, true);
                    var label_lbWL = (Label)lbWL[0];
                    if (lbWL.Length > 0)
                    {
                        if (_pGet_setting_actual.Rows[i - 1]["enable_warning_low"].ToString() == "Y")
                        {
                            label_lbWL.Text = _pGet_setting_actual.Rows[i - 1]["warning_low"].ToString();
                            label_lbWL.Visible = true;
                        }
                        else
                        {
                            label_lbWL.Visible = false;
                        }
                    }

                    //change temp setting warning hi and visible
                    var lbAH = Controls.Find("lbAH" + i, true);
                    var label_lbAH = (Label)lbAH[0];
                    if (lbAH.Length > 0)
                    {
                        if (_pGet_setting_actual.Rows[i - 1]["enable_limit_hi"].ToString() == "Y")
                        {
                            label_lbAH.Text = _pGet_setting_actual.Rows[i - 1]["limit_hi"].ToString();
                            label_lbAH.Visible = true;
                        }
                        else
                        {
                            label_lbAH.Visible = false;
                        }
                    }

                    //change temp setting warning low and visible
                    var lbAL = Controls.Find("lbAL" + i, true);
                    var label_lbAL = (Label)lbAL[0];
                    if (lbAL.Length > 0)
                    {
                        if (_pGet_setting_actual.Rows[i - 1]["enable_limit_low"].ToString() == "Y")
                        {
                            label_lbAL.Text = _pGet_setting_actual.Rows[i - 1]["limit_low"].ToString();
                            label_lbAL.Visible = true;
                        }
                        else
                        {
                            label_lbAL.Visible = false;
                        }
                    }

                }                    
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

                    gvEventAll.Columns[1].Width = 150;
                    gvEventAll.Columns[2].Width = 100;
                    gvEventAll.Columns[3].Width = 100;
                    gvEventAll.Columns[0].Width = gvEventAll.Width - (gvEventAll.Columns[1].Width + gvEventAll.Columns[2].Width + gvEventAll.Columns[3].Width);

                    // Plot data to gridView
                    for (int i = 0; i < _pGet_event_all.Rows.Count; i++)
                        gvEventAll.Rows.Add(_pGet_event_all.Rows[i]["create_datetime"], _pGet_event_all.Rows[i]["temp_name"]
                            , _pGet_event_all.Rows[i]["tool_name"], _pGet_event_all.Rows[i]["event_type"]);

                    // Keep Id for check next time
                    _EventId = Convert.ToInt32(_pGet_event_all.Rows[0]["ID"]);

                    gvEventAll.ClearSelection();
                }
            }
        }

        // Get status alarm/warning for tool  
        public void _get_status()
        {
            _pGet_status = new DataTable();
            _pGet_status = pGet_status();

            if (_pGet_status != null)
            {
                tool1_avg.Text = "Average : " + _pGet_status.Rows[0]["tool1avg"].ToString() + " C";
                lbTool1_warning.Text = "Warning: " + _pGet_status.Rows[0]["tool1_warning"].ToString() + " Times";
                lbTool1_alarm.Text  = "Out of range: " + _pGet_status.Rows[0]["tool1_alarm"].ToString() + " Times";

                tool2_avg.Text = "Average : " + _pGet_status.Rows[0]["tool2avg"].ToString() + " C";
                lbTool2_warning.Text = "Warning: " + _pGet_status.Rows[0]["tool2_warning"].ToString() + " Times";
                lbTool2_alarm.Text = "Out of range: " + _pGet_status.Rows[0]["tool2_alarm"].ToString() + " Times";

                tool3_avg.Text = "Average : " + _pGet_status.Rows[0]["tool3avg"].ToString() + " C";
                lbTool3_warning.Text = "Warning: " + _pGet_status.Rows[0]["tool3_warning"].ToString() + " Times";
                lbTool3_alarm.Text = "Out of range: " + _pGet_status.Rows[0]["tool3_alarm"].ToString() + " Times";
            }
        }

        // Get status alarm/warning for tool  
        public void _get_status_tool()
        {
            //Declare array for keep data
            int[] status1 = new int[24];
            int[] status2 = new int[24];
            int[] status3 = new int[24];

            _pGet_status_tool = new DataTable();
            _pGet_status_tool = pGet_status_tool();

                if (_pGet_status_tool != null && actualGvCell != _pGet_status_tool.Rows.Count)
                {   //Keep data to array
                    for (int i = 0; i < _pGet_status_tool.Rows.Count && i < 24; i++)
                    {
                        status1[i] = Convert.ToInt32(_pGet_status_tool.Rows[i]["tool_status1"]);
                        status2[i] = Convert.ToInt32(_pGet_status_tool.Rows[i]["tool_status2"]);
                        status3[i] = Convert.ToInt32(_pGet_status_tool.Rows[i]["tool_status3"]);
                    }

                    gvData1.Rows.Clear();
                    gvData2.Rows.Clear();
                    gvData3.Rows.Clear();
                    

                    //Add array to DataGridView
                    gvData1.Rows.Add(status1);
                    gvData2.Rows.Add(status2);
                    gvData3.Rows.Add(status3);

                    for (int i = 0; i < _pGet_status_tool.Rows.Count && i < 24; i++)
                    {
                        if (status1[i] == 3)
                        {
                            gvData1.Rows[0].Cells[i].Style.BackColor = Color.FromArgb(255, 128, 128);
                            gvData1.Rows[0].Cells[i].Style.ForeColor = Color.FromArgb(255, 128, 128);
                        }
                        else if (status1[i] == 2)
                        {
                            gvData1.Rows[0].Cells[i].Style.BackColor = Color.FromArgb(255, 192, 128);
                            gvData1.Rows[0].Cells[i].Style.ForeColor = Color.FromArgb(255, 192, 128);
                        }
                        else
                        {
                            gvData1.Rows[0].Cells[i].Style.BackColor = Color.FromArgb(128, 255, 128);
                            gvData1.Rows[0].Cells[i].Style.ForeColor = Color.FromArgb(128, 255, 128);
                        }

                        if (status2[i] == 3)
                        {
                            gvData2.Rows[0].Cells[i].Style.BackColor = Color.FromArgb(255, 128, 128);
                            gvData2.Rows[0].Cells[i].Style.ForeColor = Color.FromArgb(255, 128, 128);
                        }
                        else if (status2[i] == 2)
                        {
                            gvData2.Rows[0].Cells[i].Style.BackColor = Color.FromArgb(255, 192, 128);
                            gvData2.Rows[0].Cells[i].Style.ForeColor = Color.FromArgb(255, 192, 128);
                        }
                        else
                        {
                            gvData2.Rows[0].Cells[i].Style.BackColor = Color.FromArgb(128, 255, 128);
                            gvData2.Rows[0].Cells[i].Style.ForeColor = Color.FromArgb(128, 255, 128);
                        }

                        if (status3[i] == 3)
                        {
                            gvData3.Rows[0].Cells[i].Style.BackColor = Color.FromArgb(255, 128, 128);
                            gvData3.Rows[0].Cells[i].Style.ForeColor = Color.FromArgb(255, 128, 128);
                        }
                        else if (status3[i] == 2)
                        {
                            gvData3.Rows[0].Cells[i].Style.BackColor = Color.FromArgb(255, 192, 128);
                            gvData3.Rows[0].Cells[i].Style.ForeColor = Color.FromArgb(255, 192, 128);
                        }
                        else
                        {
                            gvData3.Rows[0].Cells[i].Style.BackColor = Color.FromArgb(128, 255, 128);
                            gvData3.Rows[0].Cells[i].Style.ForeColor = Color.FromArgb(128, 255, 128);
                        }

                        actualGvCell = _pGet_status_tool.Rows.Count;
                    }
                    gvData1.ClearSelection();
                    gvData2.ClearSelection();
                    gvData3.ClearSelection();
                }                  
        }        

        ////////////////////////////////////////////////////////////
        ///////////////// SQL interface section  ///////////////////
        ////////////////////////////////////////////////////////////
        private static DataTable pGet_Temp_actual(int temp_number)
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_Temp_actual
                SqlParameterCollection param = new SqlCommand().Parameters;
                param.AddWithValue("@temp_number", SqlDbType.Int).Value = temp_number;
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


        private static DataTable pGet_setting_actual()
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_setting_actual
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
                //  อ่านค่าจาก Store pGet_event_all
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

        private static DataTable pGet_status()
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_status
                SqlParameterCollection param = new SqlCommand().Parameters;
                ds = new DBClass().SqlExcSto("pGet_status", "DbSet", param);
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

        private static DataTable pGet_status_tool()
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_status_tool
                SqlParameterCollection param = new SqlCommand().Parameters;
                ds = new DBClass().SqlExcSto("pGet_status_tool", "DbSet", param);
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

        private static DataTable pAutoInsert_tr_temp()
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  สุ่มค่าด้วย Store pAutoInsert_tr_temp
                SqlParameterCollection param = new SqlCommand().Parameters;
                ds = new DBClass().SqlExcSto("pAutoInsert_tr_temp", "DbSet", param);
                dataTable = ds.Tables[0];
            }
            catch (SqlException e)
            {
                dataTable = null;
                log.Error("pAutoInsert_tr_temp SqlException : " + e.Message);
            }
            catch (Exception ex)
            {
                dataTable = null;
                log.Error("pAutoInsert_tr_temp Exception : " + ex.Message);
            }
            return dataTable;
        }

        ////////////////////////////////////////////////////////////
        //////////////////////  Button event  //////////////////////
        ////////////////////////////////////////////////////////////            
        private void panel14_Click(object sender, EventArgs e)
        {
            _EventTool = 0;
            sfrmEvent1 sfrmEvent1 = new sfrmEvent1();
            sfrmEvent1.Show();
        }   

        private void btData1_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 1;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void btData2_Click(object sender, EventArgs e)
        {
            _selectedTempNoData  = 4;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void btData3_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 15;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void btEvent1_Click(object sender, EventArgs e)
        {
            _EventTool = 1;
            sfrmEvent1 sfrmEvent1 = new sfrmEvent1();
            sfrmEvent1.Show();
        }

        private void btEvent2_Click(object sender, EventArgs e)
        {
            _EventTool = 2;
            sfrmEvent1 sfrmEvent1 = new sfrmEvent1();
            sfrmEvent1.Show();
        }

        private void btEvent3_Click(object sender, EventArgs e)
        {
            _EventTool = 3;
            sfrmEvent1 sfrmEvent1 = new sfrmEvent1();
            sfrmEvent1.Show();
        }

        private void btReport1_Click(object sender, EventArgs e)
        {
            _ReportTool = 1;
            sfrmReport1 sfrmReport1 = new sfrmReport1();
            sfrmReport1.Show();
        }

        private void btReport2_Click(object sender, EventArgs e)
        {
            _ReportTool = 2;
            sfrmReport1 sfrmReport1 = new sfrmReport1();
            sfrmReport1.Show();
        }

        private void btReport3_Click(object sender, EventArgs e)
        {
            _ReportTool = 3;
            sfrmReport1 sfrmReport1 = new sfrmReport1();
            sfrmReport1.Show();
        }

        private void btSetting1_Click(object sender, EventArgs e)
        {
            _SettingTool = 1;
            sfrmSetting_ref sfrmSetting_ref = new sfrmSetting_ref();
            sfrmSetting_ref.Show();
        }

        private void btSetting2_Click(object sender, EventArgs e)
        {
            _SettingTool = 2;
            sfrmSetting_ref sfrmSetting_ref = new sfrmSetting_ref();
            sfrmSetting_ref.Show();
        }

        private void btSetting3_Click(object sender, EventArgs e)
        {
            _SettingTool = 3;
            sfrmSetting_ref sfrmSetting_ref = new sfrmSetting_ref();
            sfrmSetting_ref.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pAutoInsert_tr_temp();
        }

        private void lbTemp1_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 1;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp2_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 2;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp3_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 3;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp28_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 28;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp29_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 29;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp30_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 30;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp31_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 31;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp32_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 32;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp33_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 33;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp34_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 34;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp35_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 35;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp4_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 4;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp5_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 5;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp6_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 6;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp7_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 7;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp8_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 8;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp9_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 9;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp10_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 10;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp11_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 11;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp12_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 12;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp13_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 13;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp14_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 14;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp26_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 26;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp27_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 27;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp15_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 15;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp16_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 16;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp17_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 17;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp18_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 18;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp19_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 19;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp20_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 20;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp21_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 21;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp22_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 22;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp23_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 23;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp24_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 24;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbTemp25_Click(object sender, EventArgs e)
        {
            _selectedTempNoData = 25;
            sfrmData1 sfrmData1 = new sfrmData1();
            sfrmData1.Show();
        }

        private void lbName1_Click(object sender, EventArgs e)
        {
            _SettingNumber = 1;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName2_Click(object sender, EventArgs e)
        {
            _SettingNumber = 2;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName3_Click(object sender, EventArgs e)
        {
            _SettingNumber = 3;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName28_Click(object sender, EventArgs e)
        {
            _SettingNumber = 28;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName29_Click(object sender, EventArgs e)
        {
            _SettingNumber = 29;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName30_Click(object sender, EventArgs e)
        {
            _SettingNumber = 30;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName31_Click(object sender, EventArgs e)
        {
            _SettingNumber = 31;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName32_Click(object sender, EventArgs e)
        {
            _SettingNumber = 32;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName33_Click(object sender, EventArgs e)
        {
            _SettingNumber = 33;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName34_Click(object sender, EventArgs e)
        {
            _SettingNumber = 34;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName35_Click(object sender, EventArgs e)
        {
            _SettingNumber = 35;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName4_Click(object sender, EventArgs e)
        {
            _SettingNumber = 4;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName5_Click(object sender, EventArgs e)
        {
            _SettingNumber = 5;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName6_Click(object sender, EventArgs e)
        {
            _SettingNumber = 6;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName7_Click(object sender, EventArgs e)
        {
            _SettingNumber = 7;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName8_Click(object sender, EventArgs e)
        {
            _SettingNumber = 8;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName9_Click(object sender, EventArgs e)
        {
            _SettingNumber = 9;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName10_Click(object sender, EventArgs e)
        {
            _SettingNumber = 10;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName11_Click(object sender, EventArgs e)
        {
            _SettingNumber = 11;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName12_Click(object sender, EventArgs e)
        {
            _SettingNumber = 12;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName13_Click(object sender, EventArgs e)
        {
            _SettingNumber = 13;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName14_Click(object sender, EventArgs e)
        {
            _SettingNumber = 14;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName26_Click(object sender, EventArgs e)
        {
            _SettingNumber = 26;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName27_Click(object sender, EventArgs e)
        {
            _SettingNumber = 27;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName15_Click(object sender, EventArgs e)
        {
            _SettingNumber = 15;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName16_Click(object sender, EventArgs e)
        {
            _SettingNumber = 16;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName17_Click(object sender, EventArgs e)
        {
            _SettingNumber = 17;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName18_Click(object sender, EventArgs e)
        {
            _SettingNumber = 18;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName19_Click(object sender, EventArgs e)
        {
            _SettingNumber = 19;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName20_Click(object sender, EventArgs e)
        {
            _SettingNumber = 20;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName21_Click(object sender, EventArgs e)
        {
            _SettingNumber = 21;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName22_Click(object sender, EventArgs e)
        {
            _SettingNumber = 22;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName23_Click(object sender, EventArgs e)
        {
            _SettingNumber = 23;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName24_Click(object sender, EventArgs e)
        {
            _SettingNumber = 24;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }

        private void lbName25_Click(object sender, EventArgs e)
        {
            _SettingNumber = 25;
            sfrmSetting1 sfrmSetting1 = new sfrmSetting1();
            sfrmSetting1.Show();
        }
    }
}
