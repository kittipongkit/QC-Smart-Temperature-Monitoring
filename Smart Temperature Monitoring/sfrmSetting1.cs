using System.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using static Smart_Temperature_Monitoring.InterfaceDB;

namespace Smart_Temperature_Monitoring
{
    public partial class sfrmSetting1 : Form
    {
        //  Declare Logging
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //  Local varriable
        private static DataTable _pGet_setting = new DataTable();
        private static DataTable _pUpdate_setting = new DataTable();

        private static ushort MaxHigh = 50;
        //private static ushort MinHigh = 0;
        //private static ushort MaxLow = 50;
        private static ushort MinLow = 0;

        public sfrmSetting1()
        {
            InitializeComponent();
        }

        private void sfrmSetting1_Load(object sender, EventArgs e)
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

            // Limit value
            //numHi.Maximum = MaxHigh;
            //numHi.Minimum = numLo.Value;
            //numLo.Maximum = numHi.Value;
            //numLo.Minimum = MinLow;
        }

        private void initSetting()
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
        {
            _pGet_setting = new DataTable();
            _pGet_setting = pGet_setting(sfrmOverview._SettingNumber);
            if (_pGet_setting != null)
            {
                txtSetting.Text = "Setting Temperature Number " + _pGet_setting.Rows[0]["temp_number"].ToString();
                //lbNumber1.Text = _pGet_setting.Rows[0]["temp_number"].ToString();
                lbTool1.Text = _pGet_setting.Rows[0]["tool_name"].ToString();
                lbLocation1.Text = _pGet_setting.Rows[0]["location_name"].ToString();
                lbFoor1.Text = _pGet_setting.Rows[0]["foor_name"].ToString();

                cbLine1.Checked = _pGet_setting.Rows[0]["line_active"].ToString() == "Y" ? true : false;
                cbUse1.Checked = _pGet_setting.Rows[0]["temp_active"].ToString() == "Y" ? true : false;
                lbName1.Text = _pGet_setting.Rows[0]["temp_name"].ToString();

                cbAL1.Checked = _pGet_setting.Rows[0]["enable_alow"].ToString() == "Y" ? true : false;
                cbAH1.Checked = _pGet_setting.Rows[0]["enable_ahi"].ToString() == "Y" ? true : false;
                cbWL1.Checked = _pGet_setting.Rows[0]["enable_wlow"].ToString() == "Y" ? true : false;
                cbWH1.Checked = _pGet_setting.Rows[0]["enable_whi"].ToString() == "Y" ? true : false;

                numAL1.Value = Convert.ToDecimal(_pGet_setting.Rows[0]["temp_alow"]);
                numAH1.Value = Convert.ToDecimal(_pGet_setting.Rows[0]["temp_ahi"]);
                numWL1.Value = Convert.ToDecimal(_pGet_setting.Rows[0]["temp_wlow"]);
                numWH1.Value = Convert.ToDecimal(_pGet_setting.Rows[0]["temp_whi"]);

            }
        }

        private void SaveSetting(int i)
        {
            try
           {
            //    var lbName = (TextBox)Controls.Find("lbName" + i, true)[0];
            //    var numAL = (NumericUpDown)Controls.Find("numAL" + i, true)[0];
            //    var numAH = (NumericUpDown)Controls.Find("numAH" + i, true)[0];
            //    var numWL = (NumericUpDown)Controls.Find("numWL" + i, true)[0];
            //    var numWH = (NumericUpDown)Controls.Find("numWH" + i, true)[0];
            //    var cbAL = (CheckBox)Controls.Find("cbAH" + i, true)[0];
            //    var cbAH = (CheckBox)Controls.Find("cbAH" + i, true)[0];
            //    var cbWL = (CheckBox)Controls.Find("cbWH" + i, true)[0];
            //    var cbWH = (CheckBox)Controls.Find("cbWH" + i, true)[0];
            //    var cbLine = (CheckBox)Controls.Find("cbLine" + i, true)[0];
            //    var cbUse = (CheckBox)Controls.Find("cbUse" + i, true)[0];


                //Update setting table            
                //pUpdate_setting(i, lbName1.Text, Convert.ToDouble(numAL1.Value), Convert.ToDouble(numAH1.Value), Convert.ToDouble(numWL1.Value), Convert.ToDouble(numWH1.Value),
                //        cbAH1.Checked == true ? 'Y' : 'N', cbAL1.Checked == true ? 'Y' : 'N', cbWH1.Checked == true ? 'Y' : 'N', cbWL1.Checked == true ? 'Y' : 'N',
                //        cbLine1.Checked == true ? 'Y' : 'N', cbUse1.Checked == true ? 'Y' : 'N');

                pUpdate_setting(i, lbName1.Text, Convert.ToDouble(numAH1.Value), Convert.ToDouble(numAL1.Value), Convert.ToDouble(numWH1.Value), Convert.ToDouble(numWL1.Value),
                        cbAH1.Checked == true ? 'Y' : 'N', cbAL1.Checked == true ? 'Y' : 'N', cbWH1.Checked == true ? 'Y' : 'N', cbWL1.Checked == true ? 'Y' : 'N',
                        cbLine1.Checked == true ? 'Y' : 'N', cbUse1.Checked == true ? 'Y' : 'N');



                MessageBox.Show("แก้ไขข้อมูลเรียบร้อยแล้ว", "ข้อความจากระบบ");
                this.Hide();
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
        private static DataTable pGet_setting(int tempNumber)
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_actual_value
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
        private void btnSave_MouseDown(object sender, MouseEventArgs e)
        {
            //try
            //{
            //    // Check invalid
            //    if (string.IsNullOrEmpty(txtZone.Text) || txtZone.Text == "-")
            //    {
            //        MessageBox.Show("กรูณาใส่ ZONE NAME ให้ครบถ้วน", "ข้อความจากระบบ");
            //        return;
            //    }

            //    if (string.IsNullOrEmpty(numHi.Value.ToString()) || numHi.Value.ToString() == "-")
            //    {
            //        MessageBox.Show("กรูณาใส่ TEMP. HIGH LIMIT ให้ครบถ้วน", "ข้อความจากระบบ");
            //        return;
            //    }

            //    if (string.IsNullOrEmpty(numLo.Value.ToString()) || numLo.Value.ToString() == "-")
            //    {
            //        MessageBox.Show("กรูณาใส่ TEMP. LOW LIMIT ให้ครบถ้วน", "ข้อความจากระบบ");
            //        return;
            //    }

            //    //Update setting table            
            //    pUpdate_setting(sfrmOverview._SettingZoneId, txtZone.Text, Convert.ToDouble(numHi.Value), Convert.ToDouble(numLo.Value));

            //    //Get new setting
            //    //sfrmOverview f = new sfrmOverview();
            //    //f._actual_setting();

            //    MessageBox.Show("แก้ไขข้อมูลเรียบร้อยแล้ว", "ข้อความจากระบบ");
            //    this.Hide();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "ข้อความจากระบบ");
            //    log.Error("Setting btnSave_MouseDown Exception : " + ex.Message);
            //}
        }

        private void btSave1_Click(object sender, EventArgs e)
        {
            SaveSetting(Convert.ToInt32(_pGet_setting.Rows[0]["temp_number"]));
        }
    }
}
