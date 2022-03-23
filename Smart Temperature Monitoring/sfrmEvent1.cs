using System.Data.SqlClient;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows.Forms;
using static Smart_Temperature_Monitoring.InterfaceDB;

namespace Smart_Temperature_Monitoring
{
    public partial class sfrmEvent1 : Form
    {
        //  Declare Logging
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static DataTable _pGet_event = new DataTable();
        private static DataTable _pGet_tool_name = new DataTable();

        public sfrmEvent1()
        {
            InitializeComponent();
            get_tool_name();

        }
        private void sfrmEvent1_Load(object sender, EventArgs e)
        {           
            try
            {
                _get_event(sfrmOverview._EventTool);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Error("sfrmEvent1_Load Exception : " + ex.Message);
                this.Close();
            }
        }

        public void _get_event(int tool_id)
        {
            _pGet_event = new DataTable();
            _pGet_event = pGet_event(tool_id, dtDateFrom.Value.Date, dtDateTo.Value);
            if (_pGet_event != null)
            {
                gvEvent.DataSource = _pGet_event;
                gvEvent.Columns[0].Width = 200;
                gvEvent.Columns[1].Width = 100;
                gvEvent.Columns[2].Width = 150;
                gvEvent.Columns[3].Width = 100;
                gvEvent.Columns[4].Width = 100;
                gvEvent.Columns[5].Width = 100;
                gvEvent.Columns[6].Width = 100;
                gvEvent.Columns[7].Width = gvEvent.Width - (gvEvent.Columns[0].Width + gvEvent.Columns[1].Width + gvEvent.Columns[2].Width + gvEvent.Columns[3].Width +
                    gvEvent.Columns[4].Width + gvEvent.Columns[5].Width + gvEvent.Columns[6].Width);
               
                gvEvent.ClearSelection();
                
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
                cbbSelectedTool.SelectedValue = sfrmOverview._EventTool;
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
        private static DataTable pGet_event(int tool_id, DateTime start_date, DateTime end_date)
        {
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                //  อ่านค่าจาก Store pGet_event
                SqlParameterCollection param = new SqlCommand().Parameters;
                param.AddWithValue("@tool_id", SqlDbType.DateTime).Value = tool_id;
                param.AddWithValue("@start_date", SqlDbType.DateTime).Value = start_date;
                param.AddWithValue("@end_date", SqlDbType.DateTime).Value = end_date;
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                _get_event(Convert.ToInt32(cbbSelectedTool.SelectedValue));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Error("btnOk_Click Exception : " + ex.Message);
                this.Close();
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

                string dt = "EXPORT_EVENT_" + cbbSelectedTool.Text + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
                string filePath = DestinationPath + dt;
                CreateCSVFile(ref _pGet_event, filePath);

                MessageBox.Show("Event export by user");
                log.Info("Event export by user");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Error("Event btnExport_Click Exception : " + ex.Message);
            }
        }
    }
}
