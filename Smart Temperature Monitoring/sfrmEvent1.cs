using System.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using static Smart_Temperature_Monitoring.InterfaceDB;

namespace Smart_Temperature_Monitoring
{
    public partial class sfrmEvent1 : Form
    {
        //  Declare Logging
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static DataTable _pGet_event = new DataTable();

        public sfrmEvent1()
        {
            InitializeComponent();
        }
        private void sfrmEvent1_Load(object sender, EventArgs e)
        {           
            try
            {
                _get_event();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Error("sfrmEvent1_Load Exception : " + ex.Message);
                this.Close();
            }
        }

        public void _get_event()
        {
            _pGet_event = new DataTable();
            _pGet_event = pGet_event(sfrmOverview._EventZoneId);
            if (_pGet_event != null)
            {
                gvEvent.DataSource = _pGet_event;
                gvEvent.ClearSelection();
            }
        }
        ////////////////////////////////////////////////////////////
        ///////////////// SQL interface section  ///////////////////
        ////////////////////////////////////////////////////////////
        private static DataTable pGet_event(int zone_id)
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_actual_value
                SqlParameterCollection param = new SqlCommand().Parameters;
                param.AddWithValue("@zone_id", SqlDbType.DateTime).Value = zone_id;
                ds = new DBClass().SqlExcSto("pGet_event", "DbSet", param);
                dataTable = ds.Tables[0];
            }
            catch (SqlException e)
            {
                dataTable = null;
                log.Error("pGet_event SqlException : " + e.Message);
            }
            catch (Exception ex)
            {
                dataTable = null;
                log.Error("pGet_event Exception : " + ex.Message);
            }
            return dataTable;
        }


    }
}
