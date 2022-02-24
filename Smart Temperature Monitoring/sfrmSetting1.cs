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
            numHi.Maximum = MaxHigh;
            numHi.Minimum = numLo.Value;
            numLo.Maximum = numHi.Value;
            numLo.Minimum = MinLow;
        }

        private void initSetting()
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
        {
            _pGet_setting = new DataTable();
            _pGet_setting = pGet_setting(sfrmOverview._SettingZoneId);
            if (_pGet_setting != null)
            {
                txtSetting.Text = "SYSTEM SETTING " + _pGet_setting.Rows[0]["zone_name"].ToString();
                txtZone.Text = _pGet_setting.Rows[0]["zone_name"].ToString();
                numLo.Value = Convert.ToDecimal(_pGet_setting.Rows[0]["limit_low"]);
                numHi.Value = Convert.ToDecimal(_pGet_setting.Rows[0]["limit_hi"]);
            }
        }

        ////////////////////////////////////////////////////////////
        ///////////////// SQL interface section  ///////////////////
        ////////////////////////////////////////////////////////////
        private static DataTable pGet_setting(int ZoneId)
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_actual_value
                SqlParameterCollection param = new SqlCommand().Parameters;
                param.AddWithValue("@zone_id", SqlDbType.Int).Value = ZoneId;
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

        private static DataTable pUpdate_setting(int zone_id, string zone_name, double temp_hi, double temp_lo)
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_actual_value
                SqlParameterCollection param = new SqlCommand().Parameters;
                param.AddWithValue("@zone_id", SqlDbType.Int).Value = zone_id;
                param.AddWithValue("@zone_name", SqlDbType.NVarChar).Value = zone_name;
                param.AddWithValue("@temp_hi", SqlDbType.Decimal).Value = temp_hi;
                param.AddWithValue("@temp_lo", SqlDbType.Decimal).Value = temp_lo;
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
            try
            {
                // Check invalid
                if (string.IsNullOrEmpty(txtZone.Text) || txtZone.Text == "-")
                {
                    MessageBox.Show("กรูณาใส่ ZONE NAME ให้ครบถ้วน", "ข้อความจากระบบ");
                    return;
                }

                if (string.IsNullOrEmpty(numHi.Value.ToString()) || numHi.Value.ToString() == "-")
                {
                    MessageBox.Show("กรูณาใส่ TEMP. HIGH LIMIT ให้ครบถ้วน", "ข้อความจากระบบ");
                    return;
                }

                if (string.IsNullOrEmpty(numLo.Value.ToString()) || numLo.Value.ToString() == "-")
                {
                    MessageBox.Show("กรูณาใส่ TEMP. LOW LIMIT ให้ครบถ้วน", "ข้อความจากระบบ");
                    return;
                }

                //Update setting table            
                pUpdate_setting(sfrmOverview._SettingZoneId, txtZone.Text, Convert.ToDouble(numHi.Value), Convert.ToDouble(numLo.Value));

                //Get new setting
                //sfrmOverview f = new sfrmOverview();
                //f._actual_setting();

                MessageBox.Show("แก้ไขข้อมูลเรียบร้อยแล้ว", "ข้อความจากระบบ");
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ข้อความจากระบบ");
                log.Error("Setting btnSave_MouseDown Exception : " + ex.Message);
            }
        }


    }
}
