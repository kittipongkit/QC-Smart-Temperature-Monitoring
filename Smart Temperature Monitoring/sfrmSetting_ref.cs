using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using static Smart_Temperature_Monitoring.InterfaceDB;

namespace Smart_Temperature_Monitoring
{
    public partial class sfrmSetting_ref : Form
    {
        //  Declare Logging
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        //  Local varriable
        private static DataTable _pGet_setting = new DataTable();

        public sfrmSetting_ref()
        {
            InitializeComponent();
        }

        private void sfrmSetting_ref_Load(object sender, EventArgs e)
        {
            try
            {
                initSetting();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Error("sfrmSetting1_Load Exception : " + ex.Message);
                this.Close();
            }
        }

        private void initSetting()
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
        {
            _pGet_setting = new DataTable();
            _pGet_setting = pGet_setting(sfrmOverview._SettingTool);
            //_pGet_setting = pGet_setting(1);
            if (_pGet_setting != null)
            {
                for (int i = 1; i <= _pGet_setting.Rows.Count; i++)
                {
                    
                    //show  panel
                       var pnSetting = Controls.Find("pnSetting" + i, true);
                    if (pnSetting.Length > 0)
                    {
                        var panel = (Panel)pnSetting[0];
                        panel.Visible = true;
                    }

                    //show  tool
                    var lbTool = Controls.Find("lbTool" + i, true);
                    if (lbTool.Length > 0)
                    {
                        var label = (Label)lbTool[0];
                        label.Text = _pGet_setting.Rows[i - 1]["tool_name"].ToString();
                    }

                    //show  location
                    var lbLocation = Controls.Find("lbLocation" + i, true);
                    if (lbLocation.Length > 0)
                    {
                        var label = (Label)lbLocation[0];
                        label.Text = _pGet_setting.Rows[i - 1]["location_name"].ToString();
                    }

                    //show  foor
                    var lbFoor = Controls.Find("lbFoor" + i, true);
                    if (lbFoor.Length > 0)
                    {
                        var label = (Label)lbFoor[0];
                        label.Text = _pGet_setting.Rows[i - 1]["foor_name"].ToString();
                    }

                    //show temp number
                    var lbTempNo = Controls.Find("lbTempNo" + i, true);
                    if (lbTempNo.Length > 0)
                    {
                        var label = (Label)lbTempNo[0];
                        label.Text = _pGet_setting.Rows[i-1]["temp_number"].ToString();
                    }

                    //show Line active
                    var cbLine = Controls.Find("cbLine" + i, true);
                    if (cbLine.Length > 0)
                    {
                        var checkbox = (CheckBox)cbLine[0];
                        checkbox.Checked = _pGet_setting.Rows[i - 1]["line_active"].ToString() == "Y" ? true : false;
                    }

                    //show Temp active
                    var cbUse = Controls.Find("cbUse" + i, true);
                    if (cbUse.Length > 0)
                    {
                        var checkbox = (CheckBox)cbUse[0];
                        checkbox.Checked = _pGet_setting.Rows[i - 1]["temp_active"].ToString() == "Y" ? true : false;
                    }

                    //show temp name
                    var lbName = Controls.Find("lbName" + i, true);
                    if (lbName.Length > 0)
                    {
                        var textbox = (TextBox)lbName[0];
                        textbox.Text = _pGet_setting.Rows[i - 1]["temp_name"].ToString();
                    }

                    //show setting alarm low
                    var numAL = Controls.Find("numAL" + i, true);
                    if (numAL.Length > 0)
                    {
                        var num = (NumericUpDown)numAL[0];
                        num.Value = Convert.ToDecimal(_pGet_setting.Rows[i - 1]["temp_alow"]);
                    }

                    //show setting alarm hi
                    var numAH = Controls.Find("numAH" + i, true);
                    if (numAH.Length > 0)
                    {
                        var num = (NumericUpDown)numAH[0];
                        num.Value = Convert.ToDecimal(_pGet_setting.Rows[i - 1]["temp_ahi"]);
                    }

                    //show setting warning low
                    var numWL = Controls.Find("numWL" + i, true);
                    if (numWL.Length > 0)
                    {
                        var num = (NumericUpDown)numWL[0];
                        num.Value = Convert.ToDecimal(_pGet_setting.Rows[i - 1]["temp_wlow"]);
                    }

                    //show setting warning hi
                    var numWH = Controls.Find("numWH" + i, true);
                    if (numWH.Length > 0)
                    {
                        var num = (NumericUpDown)numWH[0];
                        num.Value = Convert.ToDecimal(_pGet_setting.Rows[i - 1]["temp_whi"]);
                    }

                    //show alarm low active
                    var cbAL = Controls.Find("cbAL" + i, true);
                    if (cbAL.Length > 0)
                    {
                        var checkbox = (CheckBox)cbAL[0];
                        checkbox.Checked = _pGet_setting.Rows[i - 1]["enable_alow"].ToString() == "Y" ? true : false;
                    }

                    //show alarm hi active
                    var cbAH = Controls.Find("cbAH" + i, true);
                    if (cbAH.Length > 0)
                    {
                        var checkbox = (CheckBox)cbAH[0];
                        checkbox.Checked = _pGet_setting.Rows[i - 1]["enable_ahi"].ToString() == "Y" ? true : false;
                    }

                    //show warning low active
                    var cbWL = Controls.Find("cbWL" + i, true);
                    if (cbWL.Length > 0)
                    {
                        var checkbox = (CheckBox)cbWL[0];
                        checkbox.Checked = _pGet_setting.Rows[i - 1]["enable_wlow"].ToString() == "Y" ? true : false;
                    }

                    //show warning hi active
                    var cbWH = Controls.Find("cbWH" + i, true);
                    if (cbWH.Length > 0)
                    {
                        var checkbox = (CheckBox)cbWH[0];
                        checkbox.Checked = _pGet_setting.Rows[i - 1]["enable_whi"].ToString() == "Y" ? true : false;
                    }

                }
            }
        }

        private void SaveSetting(int i, int temp_number)
        {
            try
            {
                var lbName = (TextBox)Controls.Find("lbName" + i, true)[0];
                var numAL = (NumericUpDown)Controls.Find("numAL" + i, true)[0];
                var numAH = (NumericUpDown)Controls.Find("numAH" + i, true)[0];
                var numWL = (NumericUpDown)Controls.Find("numWL" + i, true)[0];
                var numWH = (NumericUpDown)Controls.Find("numWH" + i, true)[0];
                var cbAL = (CheckBox)Controls.Find("cbAH" + i, true)[0];
                var cbAH = (CheckBox)Controls.Find("cbAH" + i, true)[0];
                var cbWL = (CheckBox)Controls.Find("cbWH" + i, true)[0];
                var cbWH = (CheckBox)Controls.Find("cbWH" + i, true)[0];
                var cbLine = (CheckBox)Controls.Find("cbLine" + i, true)[0];
                var cbUse = (CheckBox)Controls.Find("cbUse" + i, true)[0];


                //Update setting table            
                pUpdate_setting(temp_number, lbName.Text, Convert.ToDouble(numAH.Value), Convert.ToDouble(numAL.Value), Convert.ToDouble(numWH.Value), Convert.ToDouble(numWL.Value),
                        cbAH.Checked == true ? 'Y' : 'N', cbAL.Checked == true ? 'Y' : 'N', cbWH.Checked == true ? 'Y' : 'N', cbWL.Checked == true ? 'Y' : 'N',
                        cbLine.Checked == true ? 'Y' : 'N', cbUse.Checked == true ? 'Y' : 'N');


                MessageBox.Show("แก้ไขข้อมูลเรียบร้อยแล้ว", "ข้อความจากระบบ");
                //this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ข้อความจากระบบ");
                log.Error("Setting btnSave_MouseDown Exception : " + ex.Message);
            }
        }

        ////////////////////////////////////////////////////////////
        ///////////////// SQL interface section  ///////////////////
        ////////////////////////////////////////////////////////////
        private static DataTable pGet_setting(int tool_id)
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_actual_value
                SqlParameterCollection param = new SqlCommand().Parameters;
                param.AddWithValue("@tool_id", SqlDbType.Int).Value = tool_id;
                ds = new DBClass().SqlExcSto("pGet_setting", "DbSet", param);
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

        private static DataTable pUpdate_setting(int temp_number, string temp_name, double limit_hi, double limit_low, double warning_hi, double warning_low, 
            char enable_limit_hi, char enable_limit_low, char enable_warning_hi, char enable_warning_low, char line_active, char temp_active)
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_actual_value
                SqlParameterCollection param = new SqlCommand().Parameters;
                param.AddWithValue("@temp_number", SqlDbType.Int).Value = temp_number;
                param.AddWithValue("@temp_name", SqlDbType.NVarChar).Value = temp_name;
                param.AddWithValue("@limit_hi", SqlDbType.Decimal).Value = limit_hi;
                param.AddWithValue("@limit_low", SqlDbType.Decimal).Value = limit_low;
                param.AddWithValue("@warning_hi", SqlDbType.Decimal).Value = warning_hi;
                param.AddWithValue("@warning_low", SqlDbType.Decimal).Value = warning_low;
                param.AddWithValue("@enable_limit_hi", SqlDbType.NVarChar).Value = enable_limit_hi;
                param.AddWithValue("@enable_limit_low", SqlDbType.NVarChar).Value = enable_limit_low;
                param.AddWithValue("@enable_warning_hi", SqlDbType.NVarChar).Value = enable_warning_hi;
                param.AddWithValue("@enable_warning_low", SqlDbType.NVarChar).Value = enable_warning_low;
                param.AddWithValue("@line_active", SqlDbType.NVarChar).Value = line_active;
                param.AddWithValue("@temp_active", SqlDbType.NVarChar).Value = temp_active;
                ds = new DBClass().SqlExcSto("pUpdate_setting", "DbSet", param);
                dataTable = ds.Tables[0];
            }
            catch (SqlException e)
            {
                dataTable = null;
                log.Error("Setting pUpdate_setting SqlException : " + e.Message);
            }
            catch (Exception ex)
            {
                dataTable = null;
                log.Error("Setting pUpdate_setting Exception : " + ex.Message);
            }
            return dataTable;
        }

        ////////////////////////////////////////////////////////////
        //////////////////////  Button event  //////////////////////
        ////////////////////////////////////////////////////////////
        private void btSave1_Click(object sender, EventArgs e)
        {
            SaveSetting(1, Convert.ToInt32(lbTempNo1.Text));
        }

        private void btSave2_Click(object sender, EventArgs e)
        {
            SaveSetting(2, Convert.ToInt32(lbTempNo2.Text));
        }

        private void btSave3_Click(object sender, EventArgs e)
        {
            SaveSetting(3, Convert.ToInt32(lbTempNo3.Text));
        }

        private void btSave4_Click(object sender, EventArgs e)
        {
            SaveSetting(4,  Convert.ToInt32(lbTempNo4.Text));
        }

        private void btSave5_Click(object sender, EventArgs e)
        {
            SaveSetting(5, Convert.ToInt32(lbTempNo5.Text));
        }

        private void btSave6_Click(object sender, EventArgs e)
        {
            SaveSetting(6, Convert.ToInt32(lbTempNo6.Text));
        }

        private void btSave7_Click(object sender, EventArgs e)
        {
            SaveSetting(7, Convert.ToInt32(lbTempNo7.Text));
        }

        private void btSave8_Click(object sender, EventArgs e)
        {
            SaveSetting(8, Convert.ToInt32(lbTempNo8.Text));
        }

        private void btSave9_Click(object sender, EventArgs e)
        {
            SaveSetting(9, Convert.ToInt32(lbTempNo9.Text));
        }

        private void btSave10_Click(object sender, EventArgs e)
        {
            SaveSetting(10, Convert.ToInt32(lbTempNo10.Text));
        }

        private void btSave11_Click(object sender, EventArgs e)
        {
            SaveSetting(11, Convert.ToInt32(lbTempNo11.Text));
        }

        private void btSave12_Click(object sender, EventArgs e)
        {
            SaveSetting(12, Convert.ToInt32(lbTempNo12.Text));
        }

        private void btSave13_Click(object sender, EventArgs e)
        {
            SaveSetting(13, Convert.ToInt32(lbTempNo13.Text));
        }

        private void btSave14_Click(object sender, EventArgs e)
        {
            SaveSetting(14, Convert.ToInt32(lbTempNo14.Text));
        }

        private void btSave15_Click(object sender, EventArgs e)
        {
            SaveSetting(15, Convert.ToInt32(lbTempNo15.Text));
        }
    }
}
