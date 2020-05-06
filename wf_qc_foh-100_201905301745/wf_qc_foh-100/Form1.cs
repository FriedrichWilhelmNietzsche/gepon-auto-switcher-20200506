using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;


using System.IO.Ports;




namespace wf_qc_foh_100
{
    public partial class Form1 : Form
    {
        List<string> expressions = new List<string>();  //save config

        //telnet part copy
        public static NetworkStream stream;
        public static TcpClient tcpclient;
        //private readonly int BuffSize = 1024 * 4;

        private bool isDeviceConnected = false;

        //Device Connect state 
        public bool IsDeviceConnected
        {
            get { return isDeviceConnected; }
            set
            {
                isDeviceConnected = value;
                if (isDeviceConnected)
                {
                    ShowStatusBar("The device is connected !!", true);
                    btnConnect.Text = "Disconnect";
                    ToggleControls(true);
                }
                else
                {
                    tcpclient.Close();
                    ShowStatusBar("The device is diconnected !!", true);
                    btnConnect.Text = "Connect";
                    ToggleControls(false);
                }
            }
        }

        //ToggleControls
        private void ToggleControls(bool value)
        {
            //btnDeviceInfo.Enabled = value;
            //  btnGetONU.Enabled = value;
            //  btnGetLoid.Enabled = value;
            //    btnGetFPGAVersion.Enabled = value;
            //  btnUpgradeGPON.Enabled = value;
            //    btnUpgradeEPON.Enabled = value;
            //  btnLogData.Enabled = value;
            //  btnRestartDevice.Enabled = value;
            //  btnLogin.Enabled = value;
            //btnAbout.Enabled = value;
            //btnSendCommand.Enabled = value;
            //  tbxSendCommand.Enabled = value;
            tbxPort.Enabled = !value;
            tbxDeviceIP.Enabled = !value;
            tbxLogin.Enabled = !value;
            tbxPassword.Enabled = !value;
            // Color.FromArgb(204, 204, 204)
            //rtbShowInfo.BackColor = Color.FromArgb(204, 204, 204);
            //tbxSendCommand.BackColor = Color.FromArgb(245, 245, 243);


        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        { UniversalStatic.DrawLineInFooter(pnlHeader, Color.FromArgb(204, 204, 204), 2); }
        //telnet part copy


        //SERIAL
        public byte[] a = new byte[13];
        //  private SerialPort com = new SerialPort();
        public USB usb;
        private Thread thread;

        public Form1()
        {
            InitializeComponent();

            //telnet part copy
            ToggleControls(false);
            ShowStatusBar(string.Empty, true);
            //telnet part copy
            ReadString();


            //SERIAL
            usb = new USB();
            Init();
        }

        //telnet part copy
        public void ShowStatusBar(string message, bool type)
        {
            if (message.Trim() == string.Empty)
            {
                lblStatus.Visible = false;
                lblResult.Visible = false;
                return;
            }

            lblStatus.Visible = true;
            lblStatus.Text = message;
            lblStatus.ForeColor = Color.White;



            if (type)
            {
                lblStatus.BackColor = Color.FromArgb(79, 208, 154);
                lblHeader.ForeColor = Color.FromArgb(79, 208, 154);

                lblResult.Visible = true;
                lblResult.Text = "PASS";
                lblResult.ForeColor = Color.FromArgb(79, 208, 154);
            }
            else
            {
                lblStatus.BackColor = Color.FromArgb(230, 112, 134);
                lblHeader.ForeColor = Color.FromArgb(230, 112, 134);

                lblResult.Visible = true;
                lblResult.Text = "FAIL";
                lblResult.ForeColor = Color.FromArgb(230, 112, 134);
            }

        }

        //btnConnect_Click

        private void btnConnect_Click(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;
                ShowStatusBar(string.Empty, true);

                if (IsDeviceConnected)
                {
                    IsDeviceConnected = false;
                    this.Cursor = Cursors.Default;

                    return;
                }

                string ipAddress = tbxDeviceIP.Text.Trim();
                string port = tbxPort.Text.Trim();
                string login = tbxLogin.Text.Trim();
                string password = tbxPassword.Text.Trim();
                if (ipAddress == string.Empty || port == string.Empty || login == string.Empty || password == string.Empty)
                    throw new Exception("The Device IP Port Login and Password is mandotory !!");

                //int portNumber = 23;
                //if (!int.TryParse(port, out portNumber))
                //    throw new Exception("Not a valid port number");

                bool isValidIpA = UniversalStatic.ValidateIP(ipAddress);
                if (!isValidIpA)
                    throw new Exception("The Device IP is invalid !!");

                isValidIpA = UniversalStatic.PingTheDevice(ipAddress);
                if (!isValidIpA)
                    throw new Exception("The device at " + ipAddress + ":" + port + " did not respond!!");

                tcpclient = new TcpClient(ipAddress, int.Parse(port));  // connect server
                stream = tcpclient.GetStream();   // get net stream
                IsDeviceConnected = true;

                //objZkeeper = new ZkemClient(RaiseDeviceEvent);
                //IsDeviceConnected = objZkeeper.Connect_Net(ipAddress, portNumber);
                //string result = string.Empty;
                if (IsDeviceConnected && stream.DataAvailable)     //connected and receive data
                {
                    Byte[] output = new Byte[1024];
                    String responseoutput = String.Empty;
                    Byte[] cmd = System.Text.Encoding.ASCII.GetBytes(""); //"\n"
                    stream.Write(cmd, 0, cmd.Length);

                    cmd = System.Text.Encoding.ASCII.GetBytes("get device info" + "\r\n");       //first try to get info 
                    stream.Write(cmd, 0, cmd.Length);

                    Thread.Sleep(100);
                    Int32 bytes = stream.Read(output, 0, output.Length);
                    responseoutput = System.Text.Encoding.ASCII.GetString(output, 0, bytes);
                    // rtbShowInfo.Text += responseoutput;

                    Regex objToMatch = new Regex("Login:");
                    if (objToMatch.IsMatch(responseoutput))
                    {
                        cmd = System.Text.Encoding.ASCII.GetBytes(login + "\r\n");
                        stream.Write(cmd, 0, cmd.Length);
                    }
                    Thread.Sleep(100);
                    bytes = stream.Read(output, 0, output.Length);
                    responseoutput = System.Text.Encoding.ASCII.GetString(output, 0, bytes);
                    //  rtbShowInfo.Text += responseoutput;

                    objToMatch = new Regex("Passwd:");
                    if (objToMatch.IsMatch(responseoutput))
                    {
                        cmd = System.Text.Encoding.ASCII.GetBytes(password + "\r\n");
                        stream.Write(cmd, 0, cmd.Length);
                    }
                    Thread.Sleep(100);
                    bytes = stream.Read(output, 0, output.Length);
                    responseoutput = System.Text.Encoding.ASCII.GetString(output, 0, bytes);
                    // rtbShowInfo.Text += responseoutput;

                    objToMatch = new Regex("-->");
                    if (objToMatch.IsMatch(responseoutput))
                    {
                        cmd = System.Text.Encoding.ASCII.GetBytes("get device info" + "\r\n");
                        stream.Write(cmd, 0, cmd.Length);
                    }
                    Thread.Sleep(100);
                    bytes = stream.Read(output, 0, output.Length);
                    responseoutput = System.Text.Encoding.ASCII.GetString(output, 0, bytes);
                    //rtbShowInfo.Text += responseoutput;





                    //save info log
                    StreamWriter file = new StreamWriter("log.txt", true);
                    file.WriteLine(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") + "        " + responseoutput);
                    file.Close();
                }
            }
            catch (Exception ex)
            {
                ShowStatusBar(ex.Message, false);
                //rtbShowInfo.Text += "Device has been connected, and connot be connected twice at one time!!";
            }
            this.Cursor = Cursors.Default;
            Thread.Sleep(200);
        }

        #region   read & write config info
        private void writeInfo()
        {
            //save config log
            try
            {
                FileStream SaveFile = new FileStream(System.Environment.CurrentDirectory + "\\info.txt", FileMode.Append);
                StreamWriter streamWriter = new StreamWriter(SaveFile);                // StreamWriter streamWriter = new StreamWriter(history.txt,true);
                foreach (string a in expressions)
                {
                    streamWriter.WriteLine(a);
                }
                streamWriter.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("An IO exception has been thrown!");
                MessageBox.Show(ex.ToString());
            }
        }
        //  tBx_EponSoftware    tBx_EponFPGA    tBx_GponSoftware    tBx_GponFPGA
        private void ReadString()
        {
            try
            {
                FileStream afile = new FileStream(System.Environment.CurrentDirectory + "\\info.txt", FileMode.OpenOrCreate);
                StreamReader sr = new StreamReader(afile);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("EPON"))   //EPON
                    {
                        string[] sline = line.Trim().Split(' ');    //
                        tBx_EponSoftware.Text = sline[2];
                        tBx_EponFPGA.Text = sline[4];
                        //sr.Close();
                        //}
                        //if (line.Contains("GPON"))       //GPON
                        //{
                        //string s = sr.ReadLine();  这句是读取当前行的下一行                          
                        //    string[] sline = line.Trim().Split(' ');    //
                        tBx_GponSoftware.Text = sline[7];


                    }
                }
                sr.Close();
            }
            catch (Exception e)
            {
                //MessageBox.Show("404");
                MessageBox.Show(e.ToString());
            }
        }
        #endregion

        //Ping
        private void btnPingDevice_Click(object sender, EventArgs e)
        {
            //save compare config info
            expressions.Add("EPON" + " Software: " + tBx_EponSoftware.Text.ToString() + " FPGA: " + tBx_EponFPGA.Text.ToString()
                + " GPON" + " Software: " + tBx_GponSoftware.Text.ToString() + " FPGA: " + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff"));

            Thread.Sleep(10);
            writeInfo();

            ShowStatusBar(string.Empty, true);

            string ipAddress = tbxDeviceIP.Text.Trim();

            bool isValidIpA = UniversalStatic.ValidateIP(ipAddress);
            if (!isValidIpA)
                throw new Exception("The Device IP is invalid !!");

            isValidIpA = UniversalStatic.PingTheDevice(ipAddress);
            if (isValidIpA)
                ShowStatusBar("The device is active", true);
            else
                ShowStatusBar("Could not read any response", false);

        }


        #region   btn view & label time

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                string LogPath = System.Environment.CurrentDirectory + "//log.txt";
                System.Diagnostics.Process.Start(LogPath);
            }
            catch
            {
                MessageBox.Show("未找到 log.txt");
            }
        }

        //time label
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblTime.Text = DateTime.Now.ToString();
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            //log path
            tbxView.Text = System.Environment.CurrentDirectory + "\\log.txt";

            //Timer tick
            System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
            myTimer.Tick += new EventHandler(timer1_Tick);
            myTimer.Enabled = true;
            myTimer.Interval = 1000;
            myTimer.Start();

            SendCommand.Enabled = false;
            btn_switchGEPON.Enabled = false;
            btn_Power.Enabled = false;
        }

        #region  serial
        //initial serial port
        private void Init()
        {
            try
            {
                List<string> list = new List<string>();
                string[] ports = USB.GetPorts(); //SerialPort.GetPortNames();//

                PortcomboBox.Items.Clear();
                for (int i = 0; i < ports.Length; i++)
                {
                    PortcomboBox.Items.Add(ports[i]);
                }
                if (ports.Length > 0)
                {
                    PortcomboBox.SelectedIndex = ports.Length - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PortSearch_Click(object sender, EventArgs e)
        {
            Init();
        }

        //force quit form 
        protected override void WndProc(ref Message msg)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;

            if (msg.Msg == WM_SYSCOMMAND && ((int)msg.WParam == SC_CLOSE))
            {
                // 点击winform右上关闭按钮 
                usb.CloseCom();
                isExit = true;
            }
            base.WndProc(ref msg);
        }

        //open event 
        private Boolean isOpen = false;
        private void PortOpen_Click(object sender, EventArgs e)
        {
            if (!isOpen)
            {
                try
                {
                    ShowStatusBar("The device is active", true);

                    SendCommand.Enabled = true;  //button sendcommand enable
                    btn_switchGEPON.Enabled = true;
                    btn_Power.Enabled = true;

                   
                    String comName = PortcomboBox.SelectedItem.ToString();
                    Boolean isOpenSuccess = usb.SetCom(PortcomboBox.SelectedItem.ToString());
                    if (isOpenSuccess)
                    {
                        isOpen = true;
                        PortOpen.Text = "Close";
                    }
                    else
                    {
                        isOpen = false;
                        PortOpen.Text = "Open";
                        ShowStatusBar("Could not read any response", false);
                    }
                }
                catch { ShowStatusBar("Check COM connection", true); }
            }
            else
            {
                usb.CloseCom();
                PortOpen.Text = "Open";
                isOpen = false;

            }
        }
        Boolean isExit = false;

        private void SendCommand_Click(object sender, EventArgs e)
        {
            vertest();

            //egpon();
            //vertest_again();
        }

        string fohVer = "ver";
        string fohEgpon = "*key 5";
        string fohPower = "*key 11";
        private void vertest()
        {


            if (isOpen)
            {
                try
                {
                    //usb.SendData(tb_send.Text);
                    usb.SendData(fohVer);
                    thread = new Thread(new ThreadStart(GetData));
                    thread.Start();

                }
                catch
                {
                    MessageBox.Show("Check serial communication ", "Warn");
                    usb.CloseCom();
                }
            }
        }

        private void egpon()
        {
            Thread.Sleep(5000);

            if (isOpen)
            {
                try
                {
                    //usb.SendData(tb_send.Text);
                    usb.SendData(fohEgpon);
                    thread = new Thread(new ThreadStart(GetData));
                    thread.Start();

                }
                catch
                {
                    MessageBox.Show("Check serial communication ", "Warn");
                    usb.CloseCom();
                }
            }
        }



        public void GetData()
        {
            String msg = "";

            Thread.Sleep(500);


            int i = 0;

            msg = usb.ReadData();


            if (msg.Contains("V"))
            {
                Action<int> action = (data) =>
                {
                    int start0 = msg.IndexOf("V");
                    int length0 = "V1.3.6".Length;
                    String tmpValue0 = msg.Substring(start0, length0).Trim();

                    int start1 = msg.IndexOf("PAM3001.ver:");
                    int length1 = "V2.2_1905220".Length;
                    String tmpValue1 = msg.Substring(start1 + 13, length1).Trim();

                    int start2 = msg.IndexOf("PAM3001.fpga: ");
                    int length2 = "19042400    ".Length;
                    String tmpValue2 = msg.Substring(start2 + 13, length2).Trim();

                    // ReceivedTextBox.Text += msg;


                    tBx_EpSoftware.Text = tmpValue1;
                    tBx_VOA.Text = tmpValue0;
                    tBx_EpFPGA.Text = tmpValue2;

                    String responseoutput = String.Empty;
                    responseoutput = msg;
                    //save info log
                    StreamWriter file = new StreamWriter("log.txt", true);
                    file.WriteLine(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") + "        " + responseoutput);
                    file.Close();

                };
                Invoke(action, i);
                i++;
                //  }
                Thread.Sleep(1000);
                //    }
            }
            //else
            //{
            //    try
            //    {

            //        vertest_again();
            //    }
            //    catch {
            //        MessageBox.Show("123123");
            //    }

            //}
        }

        private void btn_switchGEPON_Click(object sender, EventArgs e)
        {
            egpon();
        }

        private void btn_Compare_Click(object sender, EventArgs e)
        {
            //save compare config info
            expressions.Add("EPON" + " Software: " + tBx_EponSoftware.Text.ToString() + " FPGA: " + tBx_EponFPGA.Text.ToString()
                + " GPON" + " Software: " + tBx_GponSoftware.Text.ToString() + " FPGA: " + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff"));

            Thread.Sleep(10);

            writeInfo();





            if (tBx_EpSoftware.Text.Trim() == tBx_EponSoftware.Text.Trim() && tBx_VOA.Text.Trim() == tBx_GponSoftware.Text.Trim() && tBx_EpFPGA.Text.Trim() == tBx_EponFPGA.Text.Trim())
            {
                ShowStatusBar("The device is active", true);

            }

            // 

            else
            {
                ShowStatusBar("Compare Fail", false);
            }
        }



        private void btn_Power_Click(object sender, EventArgs e)
        {
            Thread.Sleep(5000);

            if (isOpen)
            {
                try
                {
                    //usb.SendData(tb_send.Text);
                    usb.SendData(fohPower);
                    thread = new Thread(new ThreadStart(GetData));
                    thread.Start();

                }
                catch
                {
                    MessageBox.Show("Check serial communication ", "Warn");
                    usb.CloseCom();
                }
            }
        }






        //GetData
        //public void GetData()
        //{
        //    String msg = "";

        //    Thread.Sleep(500);

        //               //    if (usb.ReadData().Contains("set ok"))
        //    //            isExit = false;
        //    //      else return;
        //    int i = 0;
        //    //  while (!isExit)
        //    //     {
        //    msg = usb.ReadData();


        //    //if (msg.Contains("PAM3001.ver:"))
        //    //{
        //        //if (!msg.Contains("=")) ;
        //    Action<int> action = (data) =>
        //    {
        //        //int start = msg.IndexOf(" ");
        //        //        int length = "V2.2_1905220".Length;
        //        //        String tmpValue = msg.Substring(start + 2, length).Trim();
        //        //        richTextBox1_valnue.Text += tmpValue;
        //        richTextBox1_valnue.Text += msg;

        //    //if (msg.Contains("dBm"))
        //    //{
        //    ////    if (!msg.Contains("=")) continue;
        //    //Action<int> action = (data) =>
        //    //{

        //        //        int start = msg.IndexOf("=");
        //        //        int length = "-00.0".Length;
        //        //        String tmpValue = msg.Substring(start + 2, length).Trim();
        //        //        float value = float.Parse(tmpValue);
        //        //        if (value > -18 || value < -22)
        //        //        {
        //        //            valueLable.ForeColor = Color.Red;
        //        //        }
        //        //        else
        //        //        {
        //        //            valueLable.ForeColor = Color.Black;


        //        //字符串转16进制字节
        //        //string hexString = msg.Replace(" ", "");
        //        //if ((hexString.Length % 2) != 0)
        //        //    hexString += " ";
        //        //byte[] returnBytes = new byte[hexString.Length / 2];
        //        //for (int ti = 0; ti < returnBytes.Length; ti++)
        //        //    returnBytes[ti] = Convert.ToByte(hexString.Substring(ti * 2, 2), 16);
        //        ////     return returnBytes;

        //        //////// union
        //        //Union u = new Union();
        //        //u.b0 = returnBytes[5];  //Tx_Buf[5  6  7  8 ]
        //        //u.b1 = returnBytes[6];
        //        //u.b2 = returnBytes[7];
        //        //u.b3 = returnBytes[8];

        //        //Union voltage = new Union();
        //        //voltage.b0 = returnBytes[9];  //Tx_Buf[5  6  7  8 ]
        //        //voltage.b1 = returnBytes[10];
        //        //voltage.b2 = 0;
        //        //voltage.b3 = 0;

        //    //    string msg1 = Double.Parse(Convert.ToString(10 * Math.Log10(u.f))).ToString("F2");   //dBm
        //    //    richTextBox1_valnue.Text += msg1 + " dBm" + "\n";
        //    //    richTextBox2_valnue.Text += msg1 + " dBm     " + Convert.ToString(u.f) + " mW    " + Convert.ToString(voltage.i) + "    " + returnBytes[12] + "\n";       //mW;

        //    };
        //    Invoke(action, i);
        //    i++;
        //  //  }
        //    Thread.Sleep(1000);
        //    //    }
        //}



        #endregion

        #region  MenuItem

     
        private ABOUT AboutForm = new ABOUT();

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm.ShowInTaskbar = false;
            AboutForm.ShowDialog();
        }

        #endregion

        private void WANToolStripMenuItem_Click(object sender, EventArgs e)
        {
       
        }


        //private ONU OnuForm = new ONU();
        private void USBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //OnuForm.ShowInTaskbar = false;
            //OnuForm.ShowDialog();
            //加载去除边框
            panel1.Controls.Clear();   //clear gradientPanel1
            ////ONU OnuForm = new ONU();
            //OnuForm.TopLevel = false;   //not TopLevel
            //OnuForm.FormBorderStyle = FormBorderStyle.None;      //donnot show FormBorderStyle
            //OnuForm.Dock = System.Windows.Forms.DockStyle.Fill;  //fill dock
            //panel1.Controls.Add(OnuForm);    //add  IAPform
            //OnuForm.Show();
        }


        #region dwdm power





        #endregion
    }
}
