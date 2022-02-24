using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Configuration;

namespace Smart_Temperature_Monitoring
{
    public partial class frmMain : Form
    {
        //  Declare Logging
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DateTime dt = DateTime.Now;

        private static int _monitorCount = 0;

        //  Local varriables
        private Form _activeForm = null;
        Color[] _blinkBgTimeColor = { Color.FromArgb(37, 37, 38), Color.FromArgb(186, 218, 85) };

        //  Form load
        public frmMain()
        {
            InitializeComponent();

            // Count monitor
            foreach (var screen in Screen.AllScreens)
                _monitorCount++;

            // Set bounds
            if (_monitorCount == 2)
                this.Bounds = Screen.AllScreens[Convert.ToInt32(ConfigurationManager.AppSettings["ScreenPosition"].ToString())].Bounds;
            else
                this.Bounds = Screen.AllScreens[0].Bounds;
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            //  Thread GUI-link Config
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;

            //  สร้าง Thread threadUpdateTime
            Thread threadUpdateTime = new Thread(ThreadUpdateTime);
            threadUpdateTime.IsBackground = true;
            threadUpdateTime.Start();            

            //  Open main child form
            openChildForm(new sfrmOverview());
        }
        //  Thread Portion
        private void ThreadUpdateTime()
        {

            while (true)
            {
                try
                {
                    dt = DateTime.Now;
                    BtnCurrentTime.Text = dt.ToString("HH:mm:ss");
                    LbDate.Text = dt.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);

                    //  Blink LbTimeBlink
                    if (LbTimeBlink.BackColor == _blinkBgTimeColor[0])
                        LbTimeBlink.BackColor = _blinkBgTimeColor[1];

                    else
                        LbTimeBlink.BackColor = _blinkBgTimeColor[0];

                    //Reset graph & status
                    //if(dt.ToString("HH:mm:") == "0:40")
                    //{
                    //    sfrmOverview f = new sfrmOverview();
                    //    f.initTempData();
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ThreadUpdateTime Exception : " + ex.Message);
                    log.Error("ThreadUpdateTime Exception : " + ex.Message);
                }
                finally
                {
                    //  Delay
                    Thread.Sleep(1000);
                }
            }
        }        

        //  Button event
       
        private void btn_minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //  Subform section
        internal void SelectSubAdminMenu(Form childForm)
        {
            openChildForm(childForm);
        }
        private void openChildForm(Form childForm)
        {
            if (_activeForm != null) _activeForm.Close();
            _activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
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

        private void btn_exit_Click(object sender, EventArgs e)
        {
            log.Info("Program exit by user");
            System.Windows.Forms.Application.ExitThread();
            System.Environment.Exit(0);
        }
    }
}
