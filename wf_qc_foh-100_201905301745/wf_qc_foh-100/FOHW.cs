using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing.Drawing2D;

using System.Threading;
using System.IO;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web;
using System.IO.Ports;

namespace wf_qc_foh_100
{
    /// <summary>
    /// 线程测试待优化
    /// </summary>
    public partial class FOHW : Form
    {

        #region  instrumentation command
        //PMT-300温度、电压读取：
        string pmt300_state_command = "system,state" + "\r\n";

        //FOH-100
        string foh100_state_command = "system state" + "\r\n";
        string foh100_onu_command = "sys onu";


        //FHP2A04   AA 01 00 00 00 00 00 00 00 00 00 00 00
        public byte[] a = new byte[13];

        //DWDM 光功率
        string dwdm_allPower = "dbm ? i     " + "\r\n";  //i:1~32    dbm ? set  
        string dwdm_getOnePower = "dbm ? 31     " + "\r\n";   //31 _1530_00     17 _1543.73_00

        //       public enum dwdmOpticalPower
        //       {

        ////_0000_00,
        ////           _1552_52,
        ////           _1295_56,
        ////           _1553_33,
        ////           _1304_58,
        ////           _1309_14,
        ////           _1554_94,
        ////           _1300_05,
        ////           _1554_13,
        ////           _1551_72,
        ////           _1291_10,
        ////           _1550_92,
        ////           _1286_66,
        ////           _1549_32,
        ////           _1570_00,
        ////           _1590_00,
        ////           _1550_12,

        ////           _1543_73,
        ////           _1556_55,
        ////           _1542_94,
        ////           _1555_75,
        ////           _1430_00,
        ////           _1544_53,
        ////           _1450_00,
        ////           _1545_32,
        ////           _1470_00,
        ////           _1546_12,
        ////           _1490_00,
        ////           _1546_92,
        ////           _1510_00,
        ////           _1547_72,
        ////           _1530_00,
        ////           _1548_51,
        ////           _9999_99,

        //       };
        #endregion

        //serial communication part           



        //  private SerialPort com = new SerialPort();    
        public USB usb;
        private Thread thread;

        int sendCount = 1;   //send command times
        public FOHW()
        {
            InitializeComponent();


            //graph part
            f_saveReadFirst(false);

            //serial communiacation part            
            usb = new USB();
            Init();
        }
        ///test timer
        ///用TimeSpan将计数器的整数转化为DateTime日期
        ///定义Timer类变量
        System.Timers.Timer test_timer;
        long TimeCount;
        //定义委托
        public delegate void SetControlValue(long value);

        //graph part
        private Color[] m_colors;
        private float m_fstyle;
        private int[] m_istyle;

        private void f_saveReadFirst(bool isRead)
        {
            if (!isRead)
            {
                m_colors = new Color[18];
                m_istyle = new int[2];
                m_istyle[0] = LGraphTest.m_titleSize;
                m_fstyle = LGraphTest.m_titlePosition;
                m_colors[0] = LGraphTest.m_titleColor;
                m_colors[1] = LGraphTest.m_titleBorderColor;
                m_colors[2] = LGraphTest.m_backColorL;
                m_colors[3] = LGraphTest.m_backColorH;
                m_colors[4] = LGraphTest.m_coordinateLineColor;
                m_colors[5] = LGraphTest.m_coordinateStringColor;
                m_colors[6] = LGraphTest.m_coordinateStringTitleColor;
                m_istyle[1] = LGraphTest.m_iLineShowColorAlpha;
                m_colors[7] = LGraphTest.m_iLineShowColor;
                m_colors[8] = LGraphTest.m_GraphBackColor;
                m_colors[9] = LGraphTest.m_ControlItemBackColor;
                m_colors[10] = LGraphTest.m_ControlButtonBackColor;
                m_colors[11] = LGraphTest.m_ControlButtonForeColorL;
                m_colors[12] = LGraphTest.m_ControlButtonForeColorH;
                m_colors[13] = LGraphTest.m_DirectionBackColor;
                m_colors[14] = LGraphTest.m_DirectionForeColor;
                m_colors[15] = LGraphTest.m_BigXYBackColor;
                m_colors[16] = LGraphTest.m_BigXYButtonBackColor;
                m_colors[17] = LGraphTest.m_BigXYButtonForeColor;

                LGraphTest.m_SyStitle = "Sample  Graph";     //LGraphTest.m_SyStitle = tb_title.Text.ToString();  
                LGraphTest.m_SySnameX = "Count";                         // LGraphTest.m_SySnameX = tb_x.Text.ToString();
                LGraphTest.m_SySnameY = "Data";                   //LGraphTest.m_SySnameY = tb_y.Text.ToString();


                //this.groupBox1.Paint += groupBox1_Paint;

                //graph show 
                ToggleControls(true);
                lbl_test_time.Visible = false;
                btn_stop_sample.Enabled = false;

                //test timer
                //设置时间间隔ms
                int interval = 1000;
                test_timer = new System.Timers.Timer(interval);
                //设置重复计时
                test_timer.AutoReset = true;
                //设置执行System.Timers.Timer.Elapsed事件
                test_timer.Elapsed += new System.Timers.ElapsedEventHandler(test_timer_tick);

                //textbox view log
                tbxView.Text = System.Environment.CurrentDirectory + "\\fohlog.txt";

                //init_comboBox_devices
                init_comboBox_devices();
                comboBox_devices.SelectedIndex = 0;

            }

        }


        // reset groupbox1 
        //private void groupBox1_Paint(object sender, PaintEventArgs e)
        //{
        //    e.Graphics.Clear(this.BackColor);            
        //    SizeF fontSize = e.Graphics.MeasureString(groupBox1.Text, groupBox1.Font);
        //    //font etc.
        //    e.Graphics.DrawString(groupBox1.Text, groupBox1.Font, Brushes.DarkGray, (groupBox1.Width - fontSize.Width) / 2, 1);
        //    //recolor DarkGray
        //    e.Graphics.DrawLine(Pens.DarkGray, 1, 10, (groupBox1.Width - fontSize.Width) / 2, 10);
        //    e.Graphics.DrawLine(Pens.DarkGray, (groupBox1.Width + fontSize.Width) / 2 - 4, 10, groupBox1.Width - 2, 10);

        //    //Transparency
        //    //this.groupBox1.BackColor = Color.Gray;
        //    //this.TransparencyKey = Color.Gray;           
        //}

        private void ToggleControls(bool value)
        {
            checkBox_manul.Enabled = !value;
            checkBox_scatter.Enabled = !value;
            btn_clear_graph.Enabled = !value;
            // btn_stop_sample.Enabled = !value;
            btn_start_sample.Enabled = !value;
            tb_sample_frequency.Enabled = !value;

            // Color.FromArgb(204, 204, 204)
            //rtbShowInfo.BackColor = Color.FromArgb(204, 204, 204);
            //tbxSendCommand.BackColor = Color.FromArgb(245, 245, 243);


        }

        private void TextboxControls(bool value)
        {
            tb_sample_frequency.Text = "";

        }


        #region **测试数据**
        public List<float> x1 = new List<float>();
        public List<float> y1 = new List<float>();
        public List<float> x2 = new List<float>();
        public List<float> y2 = new List<float>();
        public List<float> x3 = new List<float>();
        public List<float> y3 = new List<float>();
        public List<float> x4 = new List<float>();
        public List<float> y4 = new List<float>();

        private int timerDrawI = 0;

        //线程测试
        private static bool _isDone = false;
        private static object _lock = new object();
        private bool done;
        static object locker = new object();
        private delegate void txtHandeler(object obj);
        #endregion




        //private void start_sample()
        //{

        //    int i = 0;


        //    if (checkBox_manul.Checked)
        //    {
        //        Action<int> action = (data) =>
        //        {

        //            ToggleControls(true);
        //            lbl_test_time.Visible = true;


        //            this.Focus();

        //            //int current;
        //            //if (int.TryParse(tb_sample_frequency.Text.ToString(), out current))
        //            //{
        //            //    if (current > 0)  // && current < 300
        //            //    {
        //            //        timerDraw.Interval = (current * 6000);
        //            //    }
        //            //    else
        //            //    {
        //            //        tb_sample_frequency.Text = "1";
        //            //    }
        //            //}
        //            //else
        //            //{
        //            //    tb_sample_frequency.Text = "1";
        //            //}
        //            //  x1.Clear();
        //            // y1.Clear();

        //            // x2.Clear();
        //            // y2.Clear();
        //            //  x3.Clear();
        //            //  y3.Clear();
        //            //  x4.Clear();
        //            //  y4.Clear();
        //            //  LGraphTest.f_ClearAllPix();
        //            LGraphTest.f_reXY();
        //            LGraphTest.f_LoadOnePix(ref x1, ref y1, Color.Red, 2);




        //            //    LGraphTest.f_AddPix(ref x2, ref y2, Color.Blue, 3);
        //            //  LGraphTest.f_AddPix(ref x3, ref y3, Color.FromArgb(0, 128, 192), 2);
        //            //  LGraphTest.f_AddPix(ref x4, ref y4, Color.Yellow, 3);

        //            // f_timerDrawStart(); //start sample timer

        //            timerDrawI = 0;
        //            x1.Add(timerDrawI);
        //            y1.Add(float.Parse(tb_mcu_temperate.Text));
        //            timerDrawI++;
        //            LGraphTest.f_Refresh();

        //            lbl_status.Text = "SAMPLING[Period" + tb_sample_frequency.Text + "s]...";

        //            //test timer label
        //            //开始计时
        //            test_timer.Start();
        //            TimeCount = 0;
        //        };
        //        Invoke(action, i);
        //        i++;
        //    }

        //    else
        //    {
        //        Action<int> action = (data) =>
        //    {
        //        ToggleControls(true);
        //        lbl_test_time.Visible = false;

        //        //list add serial data 

        //        this.Focus();

        //        tb_sample_frequency.Text = "";
        //        //  tb_sample_frequency.Enabled = false;


        //        // x1.Clear();
        //        // y1.Clear();
        //        // LGraphTest.f_ClearAllPix();
        //        //LGraphTest.f_reXY();
        //        LGraphTest.f_LoadOnePix(ref x1, ref y1, Color.Red, 2);


        //        //foh100_state();




        //        x1.Add(timerDrawI++);
        //        //y1.Add(timerDrawI);
        //        y1.Add(float.Parse(tb_mcu_temperate.Text));
        //        ReceivedTextBox.Text += (float.Parse(tb_mcu_temperate.Text)).ToString() + "     ";
        //        // timerDrawI++;             


        //        LGraphTest.f_Refresh();


        //    };
        //        Invoke(action, i);
        //        i++;

        //    }
        //}
        private void timerDraw_Tick(object sender, EventArgs e)
        {
            ///TIME增加数据
            //x1.Add(timerDrawI);
            // y1.Add(timerDrawI % 100);
            //  x2.Add(timerDrawI);
            //  y2.Add((float)Math.Sin(timerDrawI / 10f) * 200);
            // x3.Add(timerDrawI);
            //  y3.Add(50);
            //  x4.Add(timerDrawI);
            //   y4.Add((float)Math.Sin(timerDrawI / 10) * 200);           
            // timerDrawI++;
            //LGraphTest.f_Refresh();
            if (comboBox_devices.Text == "FOH-100")
            {
                foh100_state();
                Thread.Sleep(500);
                foh100_onu();
                //  lbl_status.Text = "SAMPLING[Period" + tb_sample_frequency.Text + "s]";
            }
            if (comboBox_devices.Text == "FHP2A04")
            {
                fhp2a04_power();
                Thread.Sleep(500);
            }
            if (comboBox_devices.Text == "DWDM")
            {
                dwdm_oneOpticalPower();
                Thread.Sleep(500);

            }
        }

        private void f_timerDrawStart()
        {
            timerDrawI = 0;

            //set  timerDraw.Interval
            int current;
            if (int.TryParse(tb_sample_frequency.Text.ToString(), out current))
            {
                if (current > 1)  // && current < 300
                {
                    timerDraw.Interval = current * 1000;
                }
                else
                {
                    tb_sample_frequency.Text = "2";
                    timerDraw.Interval = 2000; ;
                }
            }
            else
            {
                tb_sample_frequency.Text = "2";
                timerDraw.Interval = 2000; ;
            }
            timerDraw.Start();

            tb_sample_frequency.ReadOnly = true;

            btn_clear_graph.Enabled = false;
        }


        private void f_timerDrawStop()
        {
            timerDraw.Stop();
            tb_sample_frequency.ReadOnly = false;
            // textBox数值.ReadOnly = false;
            btn_start_sample.Enabled = true;
            btn_clear_graph.Enabled = true;
        }


        private void stop_sample()
        {
            ///关闭TIMER
            btn_stop_sample.Enabled = false;
            this.Focus();
            f_timerDrawStop();
            lbl_status.Text = "SAMPLING STOPED.";
            btn_stop_sample.Enabled = true;
            //ToggleControls(false);


            //test timer label
            //停止计时
            test_timer.Stop();
        }


        private void clear_sample()
        {
            timerDraw.Stop();
            timerDrawI = 0;

            x1.Clear();
            y1.Clear();
            LGraphTest.f_ClearAllPix();
            lbl_status.Text = "SAMPLING DATA CLEARED.";

        }



        #region  test timer label
        private void test_timer_tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Invoke(new SetControlValue(ShowTime), TimeCount);
            TimeCount++;
        }

        private void ShowTime(long t)
        {
            TimeSpan temp = new TimeSpan(0, 0, (int)t);
            lbl_test_time.Text = "Timer：" + string.Format("{0:00}:{1:00}:{2:00}", temp.Hours, temp.Minutes, temp.Seconds);
        }

        #endregion



        //线程测试：


        private void Btn_stop_sample_Click(object sender, EventArgs e)
        {
            stop_sample();
            ToggleControls(false);

            //save curve or scatter
            string path = System.Windows.Forms.Application.StartupPath + "\\Screenshot\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string ScreenshotPath = path + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_SampleGraph.png";//path + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt"


            Bitmap bit = new Bitmap(this.Width, this.Height);//实例化一个和窗体一样大的bitmap
            Graphics g = Graphics.FromImage(bit);
            g.CompositingQuality = CompositingQuality.HighQuality;//质量设为最高
                                                                  // g.CopyFromScreen(this.Left, this.Top, 0, 0, new Size(this.Width, this.Height));//保存整个窗体为图片
            g.CopyFromScreen(splitter1.PointToScreen(Point.Empty), Point.Empty, splitter1.Size);//只保存某个控件（splitter1）
            bit.Save(ScreenshotPath);//默认保存格式为PNG，保存成jpg格式质量不是很好

        }

        private void Btn_clear_graph_Click(object sender, EventArgs e)
        {
            stop_sample();
            clear_sample();
            ReceivedTextBox.Text = "";

        }



        #region serial communication part

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

        private void Btn_portSearch_Click(object sender, EventArgs e)
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

        private void Btn_portOpen_Click(object sender, EventArgs e)
        {
            if (!isOpen)
            {
                try
                {
                    if (comboBox_devices.Text == "FOH-100")
                    {
                        Boolean isOpenSuccess = usb.SetCom(PortcomboBox.SelectedItem.ToString());
                        if (isOpenSuccess)
                        {
                            isOpen = true;
                            btn_portOpen.Text = "Close";

                            lbl_status.Text = "The device is active";
                            //btn_start_sample.Enabled = true;
                            ToggleControls(false);

                            comboBox_devices.Enabled = false;

                            sendCount = 1;
                        }
                        else
                        {
                            isOpen = false;
                            btn_portOpen.Text = "Open";
                            lbl_status.Text = "Could not read any response";
                            ToggleControls(true);
                            btn_stop_sample.Enabled = false;

                            comboBox_devices.Enabled = true;
                            //  myTimer.Stop();
                        }

                    }
                    if (comboBox_devices.Text == "FHP2A04")
                    {
                        panel3.Visible = false;

                        Boolean isOpenSuccess = usb.SetCom_fph(PortcomboBox.SelectedItem.ToString());
                        if (isOpenSuccess)
                        {
                            isOpen = true;
                            btn_portOpen.Text = "Close";

                            lbl_status.Text = "The device is active";
                            //btn_start_sample.Enabled = true;
                            ToggleControls(false);
                            userControl1_temperature.Visible = false;
                            comboBox_devices.Enabled = false;

                            sendCount = 1;
                        }
                        else
                        {
                            isOpen = false;
                            btn_portOpen.Text = "Open";
                            lbl_status.Text = "Could not read any response";
                            ToggleControls(true);
                            btn_stop_sample.Enabled = false;
                            userControl1_temperature.Visible = true;
                            comboBox_devices.Enabled = true;
                            //  myTimer.Stop();
                        }

                    }

                    if (comboBox_devices.Text == "DWDM")
                    {
                        Boolean isOpenSuccess = usb.SetCom(PortcomboBox.SelectedItem.ToString());
                        if (isOpenSuccess)
                        {
                            isOpen = true;
                            btn_portOpen.Text = "Close";

                            lbl_status.Text = "The device is active";
                            //btn_start_sample.Enabled = true;
                            ToggleControls(false);

                            comboBox_devices.Enabled = false;

                            sendCount = 1;
                        }
                        else
                        {
                            isOpen = false;
                            btn_portOpen.Text = "Open";
                            lbl_status.Text = "Could not read any response";
                            ToggleControls(true);
                            btn_stop_sample.Enabled = false;

                            comboBox_devices.Enabled = true;
                            //  myTimer.Stop();
                        }

                    }
                    //Timer tick
                    //System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
                    //myTimer.Tick += new EventHandler(timer1_Tick);
                    //myTimer.Enabled = true;
                    //myTimer.Interval = 10000;
                    //myTimer.Start();

                    //serial graph
                    //graph_serial();



                }
                catch
                {
                    lbl_status.Text = "Check COM connection";

                }

            }
            else
            {
                stop_sample();   //stop sample
                usb.CloseCom();
                btn_portOpen.Text = "Open";
                isOpen = false;

                ToggleControls(true);
                btn_stop_sample.Enabled = false;
                comboBox_devices.Enabled = true;
            }
        }
        Boolean isExit = false;

        private void error_ui() {
            stop_sample();   //stop sample
            usb.CloseCom();
            btn_portOpen.Text = "Open";
            isOpen = false;

            ToggleControls(true);
            btn_stop_sample.Enabled = false;
            comboBox_devices.Enabled = true;
        }
        private void foh100_state()
        {
            if (isOpen)
            {
                try
                {
                    //usb.SendData(tb_send.Text);
                    usb.SendData(foh100_state_command);
                    thread = new Thread(new ThreadStart(GetData_foh100_state));
                    thread.Start();
                }
                catch
                {
                    //usb.CloseCom();
                    error_ui();

                    MessageBox.Show("Serial write wrong", "Warn");                   
                }
            }
        }
        private void foh100_onu()
        {
            if (isOpen)
            {
                try
                {
                    //usb.SendData(tb_send.Text);
                    usb.SendData(foh100_onu_command);
                    thread = new Thread(new ThreadStart(GetData_foh100_onu));
                    thread.Start();
                }
                catch
                {
                    //usb.CloseCom();
                    stop_sample();
                    MessageBox.Show("Serial write wrong", "Warn");

                }
            }
        }

        private void fhp2a04_power()
        {
            if (isOpen)
            {
                try
                {
                    //AA 01 00 00 00 00 00 00 00 00 00 00 00
                    a[0] = 0xaa;
                    a[1] = 0x01;
                    a[2] = 0x00;
                    a[3] = 0x00;
                    a[4] = 0x00;
                    a[5] = 0x00;
                    a[6] = 0x00;
                    a[7] = 0x00;
                    a[8] = 0x00;
                    a[9] = 0x00;
                    a[10] = 0x00;
                    a[11] = 0x00;
                    a[12] = 0x00;
                    usb.SendHex(a);
                    Thread.Sleep(100);
                    //  String msg = "";
                    thread = new Thread(new ThreadStart(GetData_fhp2a04_power));
                    thread.Start();
                }
                catch
                {
                    //usb.CloseCom();
                  
                    error_ui();
                    MessageBox.Show("Serial write wrong", "Warn");

                }
            }
        }

        //dwdm power
       // string dwdm_getOnePower = "dbm ? 31     " + "\r\n";   //_1530_00

        private void dwdm_oneOpticalPower()
        {
            if (isOpen)
            {
                try
                {
                    ////指定波长
                    //string threadName =   "1530     ";
                    //usb.SendData(dwdm_getOnePower);
                    //thread = new Thread(new ThreadStart(GetData_dwdm_oneOpticalPower(threadName)));
                    //thread.Start();

                    for (int i = 1; i < 33; i++)
                    {
                        string dwdmAllOpticalPower = "dbm ? " + i + "     " + "\r\n";
                        string threadName =  i + "  ";

                        usb.SendData(dwdmAllOpticalPower);
                        var t = new Thread(() => GetData_dwdm_oneOpticalPower(threadName));  
                        t.Start();
                        Thread.Sleep(200);
                      //  _dwdmAllOpticalEvent.WaitOne();
                    }
                }
                catch
                {
                    //usb.CloseCom();
                    stop_sample();
                    MessageBox.Show("Serial write wrong", "Warn");

                }
            }
        }

        ////ReceivedTextBox auto scroll
        //private delegate void delInfoList(string text);
        //private void SetrichTextBox(string value)
        //{

        //    if (ReceivedTextBox.InvokeRequired)//其它线程调用
        //    {
        //        delInfoList d = new delInfoList(SetrichTextBox);
        //        ReceivedTextBox.Invoke(d, value);
        //    }
        //    else//本线程调用
        //    {
        //        if (ReceivedTextBox.Lines.Length > 100)
        //        {
        //            ReceivedTextBox.Clear();
        //        }

        //        ReceivedTextBox.Focus(); //让文本框获取焦点 
        //        ReceivedTextBox.Select(ReceivedTextBox.TextLength, 0);//设置光标的位置到文本尾
        //        ReceivedTextBox.ScrollToCaret();//滚动到控件光标处 
        //        ReceivedTextBox.AppendText(value);//添加内容
        //    }
        //}


        public void GetData_foh100_state()
        {
            String msg = "";

            Thread.Sleep(100);


            int i = 0;

            msg = usb.ReadData();


            if (msg.Contains("sys_power_flg")) // || msg.Contains( "onu_info.rx_lenth")
            {

                Action<int> action = (data) =>
                {
                    int start0 = msg.IndexOf("mcu_temperate");
                    int length0 = " = 36.71".Length;
                    String tmpValue0 = msg.Substring(start0 + 15, length0 - 1).Trim();

                    int start1 = msg.IndexOf("battary_volatge ");
                    int length1 = " = 4.30".Length;
                    String tmpValue1 = msg.Substring(start1 + 17, length1).Trim();

                    int start2 = msg.IndexOf("fan_speed_test");
                    int length2 = " = 0  ".Length;
                    String tmpValue2 = msg.Substring(start2 + 16, length2 - 2).Trim();

                    // ReceivedTextBox.Text += msg + "\r\n";



                    //usercontrol_temperature graph show
                    userControl1_temperature.CurValue = float.Parse(tmpValue0);
                    userControl1_temperature.Refresh();

                    //temperature max label show
                    if (float.Parse(tb_temperature_max.Text) < float.Parse(tmpValue0))
                    { tb_temperature_max.Text = tmpValue0; }



                    String responseoutput = String.Empty;
                    responseoutput = msg;
                    //save info log
                    //StreamWriter file = new StreamWriter("fohlog.txt", true);
                    //file.WriteLine(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") + "        " + responseoutput);
                    //file.Close();
                    string path = System.Windows.Forms.Application.StartupPath + "\\LogFile\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string postPath = path + "FOH_LOG.txt";//path + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt"
                    StreamWriter file = new StreamWriter(postPath, true);
                    file.WriteLine(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") + "        " + responseoutput);
                    file.Close();


                    //graph list
                    //string amount = string.Empty;
                    //if (!string.IsNullOrEmpty(tmpValue0) && (Regex.IsMatch(tmpValue0, @"^[1-9]\d*|0$"))) // || Regex.IsMatch(tmpValue0, @"^[1-9]\d*\.\d*|0\.\d*[1-9]\d*$")
                    //{
                    //    amount = Convert.ToDecimal(tmpValue0).ToString();

                    //}
                    //else
                    //{
                    //    amount = "0.00";
                    //}


                    //string amount = string.Empty;
                    //amount = tmpValue0.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");

                    y1.Add(float.Parse(tmpValue0));

                    ReceivedTextBox.Text += "temperature: " + float.Parse(tmpValue0) + "\r\n" + "battary_volatge: " + tmpValue1 + "\r\n" + "fan_speed: " + tmpValue2 + "\r\n";
                    //ReceivedTextBox get focus, end of ReceivedTextBox.TextLength,ScrollToCaret 
                    this.ReceivedTextBox.Focus();
                    this.ReceivedTextBox.Select(this.ReceivedTextBox.TextLength, 0);
                    this.ReceivedTextBox.ScrollToCaret();

                    x1.Add(timerDrawI);

                    // y1.Add(float.Parse(tb_mcu_temperate.Text));
                    timerDrawI++;
                    LGraphTest.f_Refresh();

                    lbl_send_count.Text = "Count: " + sendCount++.ToString();
                };
                Invoke(action, i);
                i++;

                //  }
                Thread.Sleep(200);
                //    }   
            }
        }

        public void GetData_foh100_onu()
        {
            //try
            //{

            String msg = "";
            Thread.Sleep(100);
            int i = 0;
            msg = usb.ReadData();
            if (msg.Contains("onu_info.rx_lenth"))
            {

                Action<int> action = (data) =>
                {
                    int start0 = msg.IndexOf("onu_info.totality: ");    //present
                    int length0 = " 0".Length;
                    String numValue0 = msg.Substring(start0 + 18, length0).Trim();
                    // ReceivedTextBox.Text += msg + "\r\n";

                    //onu min label show
                    if (Convert.ToInt32(tb_onu_min.Text) > Convert.ToInt32(numValue0))// if (Convert.ToInt32(tb_onu_min.Text) > Convert.ToInt32(numValue0))
                    { tb_onu_min.Text = numValue0; }

                    String responseoutput = String.Empty;
                    responseoutput = msg;
                    //save info log
                    string path = System.Windows.Forms.Application.StartupPath + "\\LogFile\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string postPath = path + "FOH_LOG.txt";//path + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt"
                    StreamWriter file = new StreamWriter(postPath, true);
                    file.WriteLine(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") + "        " + responseoutput);
                    file.Close();

                    //  y1.Add(float.Parse(tmpValue0));

                    ReceivedTextBox.Text += "onu number: " + numValue0 + "\r\n";
                    //ReceivedTextBox get focus, end of ReceivedTextBox.TextLength,ScrollToCaret 
                    this.ReceivedTextBox.Focus();
                    this.ReceivedTextBox.Select(this.ReceivedTextBox.TextLength, 0);
                    this.ReceivedTextBox.ScrollToCaret();

                    //  x1.Add(timerDrawI);

                    // y1.Add(float.Parse(tb_mcu_temperate.Text));
                    //  timerDrawI++;
                    //  LGraphTest.f_Refresh();

                    //  lbl_send_count.Text = "采样计数: " + sendCount++.ToString();
                };
                Invoke(action, i);
                i++;

            }
            //}
            //catch
            //{
            //    timerDraw.Stop();
            //    MessageBox.Show("Serial data receive error", "Warn");
            //}
            Thread.Sleep(200);
            //    }   
        }



        //GetData_fhp2a04_power
        public void GetData_fhp2a04_power()
        {
            try
            {
                String msg = "";

                Thread.Sleep(500);
                int i = 0;
                msg = usb.ReadByte();
                //if (msg.Contains("dBm"))
                //{
                //    if (!msg.Contains("=")) continue;
                Action<int> action = (data) =>
                {

                    //        int start = msg.IndexOf("=");
                    //        int length = "-00.0".Length;
                    //        String tmpValue = msg.Substring(start + 2, length).Trim();
                    //        float value = float.Parse(tmpValue);
                    //        if (value > -18 || value < -22)
                    //        {
                    //            valueLable.ForeColor = Color.Red;
                    //        }
                    //        else
                    //        {
                    //            valueLable.ForeColor = Color.Black;


                    // string to hex
                    string hexString = msg.Replace(" ", "");
                    if ((hexString.Length % 2) != 0)
                        hexString += " ";
                    byte[] returnBytes = new byte[hexString.Length / 2];
                    for (int ti = 0; ti < returnBytes.Length; ti++)
                        returnBytes[ti] = Convert.ToByte(hexString.Substring(ti * 2, 2), 16);
                    //     return returnBytes;

                    //// union
                    PowerUnion.Union u = new PowerUnion.Union();
                    u.b0 = returnBytes[5];  //Tx_Buf[5  6  7  8 ]
                    u.b1 = returnBytes[6];
                    u.b2 = returnBytes[7];
                    u.b3 = returnBytes[8];
                    PowerUnion.Union voltage = new PowerUnion.Union();
                    voltage.b0 = returnBytes[9];  //Tx_Buf[5  6  7  8 ]
                    voltage.b1 = returnBytes[10];
                    voltage.b2 = 0;
                    voltage.b3 = 0;

                    string msg1 = Double.Parse(Convert.ToString(10 * Math.Log10(u.f))).ToString("F2");   //dBm

                    if (float.Parse(msg1) > -30)
                    {
                        y1.Add(float.Parse(msg1));

                        ReceivedTextBox.Text += msg1 + " dBm     " + Convert.ToString(u.f) + " mW    " + Convert.ToString(voltage.i) + "    " + returnBytes[12] + "\n";       //mW;
                                                                                                                                                                              //ReceivedTextBox get focus, end of ReceivedTextBox.TextLength,ScrollToCaret 
                        this.ReceivedTextBox.Focus();
                        this.ReceivedTextBox.Select(this.ReceivedTextBox.TextLength, 0);
                        this.ReceivedTextBox.ScrollToCaret();

                        x1.Add(timerDrawI);

                        // y1.Add(float.Parse(tb_mcu_temperate.Text));
                        timerDrawI++;
                        LGraphTest.f_Refresh();

                        lbl_send_count.Text = "Count: " + sendCount++.ToString();


                        String responseoutput0 = String.Empty;
                        String responseoutput1 = String.Empty;
                        responseoutput0 = msg;
                        responseoutput1 = msg1 + " dBm     " + Convert.ToString(u.f) + " mW    " + Convert.ToString(voltage.i) + "    " + returnBytes[12] + "\n";
                        //save info log
                        string path = System.Windows.Forms.Application.StartupPath + "\\LogFile\\";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string postPath = path + "FHP_LOG.txt";//path + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt"
                        StreamWriter file = new StreamWriter(postPath, true);

                        file.WriteLine(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") + "        " + responseoutput0 + "\n" + responseoutput1);
                        file.Close();
                    }
                    else {
                        object obj = new object();
                        EventArgs ea = new EventArgs();
                        Btn_stop_sample_Click(obj, ea); //调用stop
                    }
                };
                Invoke(action, i);
                i++;
            }

            catch
            {
                timerDraw.Stop();
                MessageBox.Show("Serial data receive error", "Warn");
            }

            Thread.Sleep(1000);
        }


        //dwdm all power
        private static AutoResetEvent _dwdmAllOpticalEvent = new AutoResetEvent(false);
        public void GetData_dwdm_oneOpticalPower(string name)
        {
            //try
            //{

            String msg = "";
            Thread.Sleep(100);
            int i = 0;
            msg = usb.ReadData();
            if (msg.Contains("dbm"))
            {

                Action<int> action = (data) =>
                {
                    msg = Regex.Replace(msg, @"[^\d.\d]", "");
                    // 如果是数字，则转换为decimal类型
                    if (Regex.IsMatch(msg, @"^[+-]?\d*[.]?\d*$"))
                    {
                        decimal result = decimal.Parse(msg);
                        //    //    string oneOpticalPower = Double.Parse(Convert.ToString(10 * Math.Log10((Convert.ToDouble(result) / 1000) / (Math.Pow(10, -3))))).ToString("F3");

                    //    int start0 = msg.IndexOf("dbm ? ");    //present
                    //int length0 = "0.000000".Length;
                    //String result = msg.Substring(start0 + 6, length0).Trim();
                    if ((Convert.ToDouble(result) / 1000) > 0)
                        {
                            string oneOpticalPower = Double.Parse(Convert.ToString(10 * Math.Log10((Convert.ToDouble(result) / 1000) / (Math.Pow(10, -3))))).ToString("F3");

                            y1.Add(float.Parse(oneOpticalPower));
                            x1.Add(timerDrawI);

                            timerDrawI++;
                            LGraphTest.f_Refresh();

                            lbl_send_count.Text = "Count: " + sendCount++.ToString();


                            String responseoutput = String.Empty;
                            responseoutput = name + msg + "W    " + oneOpticalPower + "dBm";
                            //save info log
                            string path = System.Windows.Forms.Application.StartupPath + "\\LogFile\\";
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            string postPath = path + "dwdm.txt";//path + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt"
                            StreamWriter file = new StreamWriter(postPath, true);
                            file.WriteLine(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") + "        " + responseoutput);
                            file.Close();

                            //  y1.Add(float.Parse(tmpValue0));

                            ReceivedTextBox.Text += name + "Optical Power: " +  msg + "W  " + oneOpticalPower +"dBm" + "\r\n";
                            //ReceivedTextBox get focus, end of ReceivedTextBox.TextLength,ScrollToCaret 
                            this.ReceivedTextBox.Focus();
                            this.ReceivedTextBox.Select(this.ReceivedTextBox.TextLength, 0);
                            this.ReceivedTextBox.ScrollToCaret();

                            //  x1.Add(timerDrawI);

                            // y1.Add(float.Parse(tb_mcu_temperate.Text));
                            //  timerDrawI++;
                            //  LGraphTest.f_Refresh();

                            //  lbl_send_count.Text = "采样计数: " + sendCount++.ToString();
                        }
                    else{
                        string oneOpticalPower1 = "-100";

                        y1.Add(float.Parse(oneOpticalPower1));
                        x1.Add(timerDrawI);

                        timerDrawI++;
                        LGraphTest.f_Refresh();

                        lbl_send_count.Text = "Count: " + sendCount++.ToString();


                        String responseoutput = String.Empty;
                        responseoutput = name + "0W" + "      " + oneOpticalPower1  ;
                        //save info log
                        string path = System.Windows.Forms.Application.StartupPath + "\\LogFile\\";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string postPath = path + "dwdm.txt";//path + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt"
                        StreamWriter file = new StreamWriter(postPath, true);
                        file.WriteLine(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") + "        " + responseoutput);
                        file.Close();

                        //  y1.Add(float.Parse(tmpValue0));

                        ReceivedTextBox.Text += name +  "Optical Power: "  + "0W  " + "LOW（-100dBm）" + "\r\n";
                        //ReceivedTextBox get focus, end of ReceivedTextBox.TextLength,ScrollToCaret 
                        this.ReceivedTextBox.Focus();
                        this.ReceivedTextBox.Select(this.ReceivedTextBox.TextLength, 0);
                        this.ReceivedTextBox.ScrollToCaret();

                    }
                     }
                };
                Invoke(action, i);
                i++;
            }
            //}
            //catch
            //{
            //    timerDraw.Stop();
            //    MessageBox.Show("Serial data receive error", "Warn");
            //}
            Thread.Sleep(200);
           // _dwdmAllOpticalEvent.Set();
            //    }   
        }

        #endregion

        private void Btn_start_sample_Click(object sender, EventArgs e)   //   Button1_Click
        {
            btn_start_sample.Enabled = false;
            Thread.Sleep(100);
            tb_onu_min.Text = "256";    //重新初始ONU数量


            if (checkBox_manul.Checked)  //sample period
            {
                sendCount = 1;
                ToggleControls(true);
                btn_start_sample.Enabled = false;
                btn_stop_sample.Enabled = true;
                lbl_test_time.Visible = true;


                this.Focus();


                x1.Clear();
                y1.Clear();

                // x2.Clear();
                // y2.Clear();
                //  x3.Clear();
                //  y3.Clear();
                //  x4.Clear();
                //  y4.Clear();
                LGraphTest.f_ClearAllPix();
                LGraphTest.f_reXY();
                if (checkBox_scatter.Checked) { LGraphTest.f_LoadOnePix(ref x1, ref y1, Color.Red, 2, LineJoin.Round, LineCap.NoAnchor, LILEI.UI.LGraph.DrawStyle.dot); }   // scatter
                else { LGraphTest.f_LoadOnePix(ref x1, ref y1, Color.Red, 2); }    //curve

                //    LGraphTest.f_AddPix(ref x2, ref y2, Color.Blue, 3);
                //  LGraphTest.f_AddPix(ref x3, ref y3, Color.FromArgb(0, 128, 192), 2);
                //  LGraphTest.f_AddPix(ref x4, ref y4, Color.Yellow, 3);

                f_timerDrawStart(); //start sample timer

                lbl_status.Text = "SAMPLING[Period " + tb_sample_frequency.Text + "s]";

                //test timer label
                //开始计时
                test_timer.Start();
                TimeCount = 0;
            }
            else       //onece click
            {
                if (comboBox_devices.Text == "FOH-100")
                {
                    //ToggleControls(true);
                    lbl_test_time.Visible = false;

                    //list add serial data 

                    this.Focus();

                    tb_sample_frequency.Text = "";
                    //  tb_sample_frequency.Enabled = false;


                    // x1.Clear();
                    // y1.Clear();
                    // LGraphTest.f_ClearAllPix();
                    LGraphTest.f_reXY();
                    if (checkBox_scatter.Checked) { LGraphTest.f_LoadOnePix(ref x1, ref y1, Color.Red, 2, LineJoin.Round, LineCap.NoAnchor, LILEI.UI.LGraph.DrawStyle.dot); }   // scatter
                    else
                    {
                        LGraphTest.f_LoadOnePix(ref x1, ref y1, Color.Red, 2);
                    }


                    foh100_state();
                    Thread.Sleep(500);
                    foh100_onu();


                }
                if (comboBox_devices.Text == "FHP2A04")
                {
                    //ToggleControls(true);
                    lbl_test_time.Visible = false;

                    //list add serial data 

                    this.Focus();

                    tb_sample_frequency.Text = "";
                    //  tb_sample_frequency.Enabled = false;


                    // x1.Clear();
                    // y1.Clear();
                    // LGraphTest.f_ClearAllPix();
                    LGraphTest.f_reXY();
                    if (checkBox_scatter.Checked) { LGraphTest.f_LoadOnePix(ref x1, ref y1, Color.Red, 2, LineJoin.Round, LineCap.NoAnchor, LILEI.UI.LGraph.DrawStyle.dot); }   // scatter
                    else
                    {
                        LGraphTest.f_LoadOnePix(ref x1, ref y1, Color.Red, 2);
                    }


                    fhp2a04_power();
                    Thread.Sleep(500);

                }

                if (comboBox_devices.Text == "DWDM")
                {
                    lbl_test_time.Visible = false;
                    this.Focus();

                    tb_sample_frequency.Text = "";

                    LGraphTest.f_reXY();
                    if (checkBox_scatter.Checked) { LGraphTest.f_LoadOnePix(ref x1, ref y1, Color.Red, 2, LineJoin.Round, LineCap.NoAnchor, LILEI.UI.LGraph.DrawStyle.dot); }   // scatter
                    else
                    {
                        LGraphTest.f_LoadOnePix(ref x1, ref y1, Color.Red, 2);
                    }


                    dwdm_oneOpticalPower();
                    Thread.Sleep(500);

                }

                btn_start_sample.Enabled = true;
                btn_clear_graph.Enabled = true;
            }
        }

        private void BtnView_Click(object sender, EventArgs e)
        {

            try
            {
                if (comboBox_devices.Text == "FOH-100")
                {
                    string LogPath = System.Windows.Forms.Application.StartupPath + "\\LogFile\\" + "//FOH_LOG.txt";  //System.Windows.Forms.Application.StartupPath + "\\LogFile\\";
                    System.Diagnostics.Process.Start(LogPath);
                }
                if (comboBox_devices.Text == "FHP2A04")
                {
                    string LogPath = System.Windows.Forms.Application.StartupPath + "\\LogFile\\" + "//FHP_LOG.txt";
                    System.Diagnostics.Process.Start(LogPath);
                }

            }
            catch
            {
                MessageBox.Show("Did not find fohlog.txt");
            }
        }


        //comboBox_devices
        private void init_comboBox_devices()
        {
            string item_1 = "FOH-100";
            // string item_2 = "PMT-300";
            string item_3 = "FHP2A04";
            string item_4 = "DWDM";
            this.comboBox_devices.Items.Add(item_1);
            //this.comboBox_devices.Items.Add(item_2);
            this.comboBox_devices.Items.Add(item_3);
            this.comboBox_devices.Items.Add(item_4);

        }

        private void Btn_report_Click(object sender, EventArgs e)
        {
            if (tb_report.Text.Trim() == "")
            {
                tb_report.Text = "测试报告";
            }
            string ReportPath = System.Windows.Forms.Application.StartupPath + "\\ReportFile\\";// + tb_report.Text.Trim()+".pdf";
            if (!Directory.Exists(ReportPath))
            {
                Directory.CreateDirectory(ReportPath);
            }
            string Fname = ReportPath + tb_report.Text.Trim() + DateTime.Now.ToString("_yyyyMMddHHmmssfff") + ".pdf";
            //pdf size
            iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(595, 824);
            Document document = new Document(PageSize.A4, 36, 36, 126, 10);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(Fname, FileMode.Create));
            document.Open();
            document.Add(new iTextSharp.text.Paragraph("Hello World! Hello People! " +
                        "Hello Sky! Hello Sun! Hello Moon! Hello Stars!"));

            //new page
            document.NewPage();
            document.ResetPageCount();
            //save usercontrol_splitter1 screenshot :curve or scatter
            string path = System.Windows.Forms.Application.StartupPath + "\\Screenshot\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string ScreenshotPath = path + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_SampleGraph.png";//path + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt"
            Bitmap bit = new Bitmap(this.Width, this.Height);//实例化一个和窗体一样大的bitmap
            Graphics g = Graphics.FromImage(bit);
            g.CompositingQuality = CompositingQuality.HighQuality;//质量设为最高
                                                                  // g.CopyFromScreen(this.Left, this.Top, 0, 0, new Size(this.Width, this.Height));//保存整个窗体为图片
            g.CopyFromScreen(splitter1.PointToScreen(Point.Empty), Point.Empty, splitter1.Size);//只保存某个控件（splitter1）
            bit.Save(ScreenshotPath);//默认保存格式为PNG，保存成jpg格式质量不是很好

            //add image to pdf 
            //string imgurl = @System.Web.HttpContext.Current.Server.MapPath(Fimg);
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(ScreenshotPath);
            img.SetAbsolutePosition(50, 0);
            writer.DirectContent.AddImage(img);

            //new page
            document.NewPage();
            //reset page count 
            document.ResetPageCount();
            document.Add(new iTextSharp.text.Paragraph("Hello World! Hello People! " +
                    "Hello Sky! Hello Sun! Hello Moon! Hello Stars!"));

            document.Close();
            writer.Close();
            MessageBox.Show(tb_report.Text.Trim() + " Success !", "Prompt");
        }

        private void Btn_itext_Click(object sender, EventArgs e)
        {
            try
            {
                string path = System.Windows.Forms.Application.StartupPath + "\\ReportFile\\";
                if (String.IsNullOrWhiteSpace(path))
                {
                    MessageBox.Show("请输入路径");
                    return;
                }

                if (!Directory.Exists(Path.GetDirectoryName(path)))
                {
                    MessageBox.Show("指定的目录不存在");
                    return;
                }

                if (".PDF" != Path.GetExtension(path).ToUpper())
                {
                    path = path + ".pdf";
                }
                PDFReport.ExportPDF(path);
                

                MessageBox.Show("导出成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// save usercontrol_splitter1 screenshot :curve or scatter
        /// </summary>
        /// <param name="screenshot">save usercontrol_splitter1 screenshot</param>

        public void ExportPDF()
        {
            BaseFont bfSunx = BaseFont.CreateFont(System.Windows.Forms.Application.StartupPath + "\\Fonts\\SIMSUN.TTC,1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);  //songti
            iTextSharp.text.Font font_itemSun_bold = new iTextSharp.text.Font(bfSunx, 20, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font font_itemSun_momal20 = new iTextSharp.text.Font(bfSunx, 20);
            iTextSharp.text.Font font_itemSun_nomal15 = new iTextSharp.text.Font(bfSunx, 15);
            iTextSharp.text.Font font_itemSun_nomal12 = new iTextSharp.text.Font(bfSunx, 12);

            PDFReport pdfReport = new PDFReport();

            if (tb_report.Text.Trim() == "")
            {
                tb_report.Text = "测试报告";
            }
            string ReportPath = System.Windows.Forms.Application.StartupPath + "\\ReportFile\\";// + tb_report.Text.Trim()+".pdf";
            if (!Directory.Exists(ReportPath))
            {
                Directory.CreateDirectory(ReportPath);
            }
            string Fname = ReportPath + tb_report.Text.Trim() + DateTime.Now.ToString("_yyyyMMddHHmmssfff") + ".pdf";
            //first page
            iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(595, 824);
            Document document = new Document(PageSize.A4, 36, 36, 126, 10);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(Fname, FileMode.Create));
            document.Open();
            pdfReport.AddBody(document);

            //new page
            document.NewPage();
            writer.PageEvent = pdfReport;//触发页眉和页脚
            //  document.ResetPageCount();//reset page count 
            Chapter chapter1 = new Chapter(new Paragraph("概述", font_itemSun_nomal15), 1);
            Section section1 = chapter1.AddSection(20f, "结论", 2);          
           // document.Add(chapter1);
            document.Add(section1);
            document.Add(new iTextSharp.text.Paragraph("Hello World! Hello People! " +
                "Hello Sky! Hello Sun! Hello Moon! Hello Stars!"));

            Section section2 = chapter1.AddSection(20f, "说明", 2);             
            document.NewPage();  //new page
            document.Add(section2);
            document.Add(new iTextSharp.text.Paragraph("Hello World! Hello People! " +
                "Hello Sky! Hello Sun! Hello Moon! Hello Stars!"));
            document.Add(new iTextSharp.text.Paragraph("Hello World! Hello People! " +
                        "Hello Sky! Hello Sun! Hello Moon! Hello Stars!"));

            
            // save usercontrol_splitter1 screenshot :curve or scatter
           
            //string path = System.Windows.Forms.Application.StartupPath + "\\Screenshot\\";
            if (!Directory.Exists(ReportPath))
            {
                Directory.CreateDirectory(ReportPath);
            }
            string ScreenshotPath = System.Windows.Forms.Application.StartupPath + "\\Screenshot\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_SampleGraph.png";//path + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt"
            Bitmap bit = new Bitmap(this.Width, this.Height);//实例化一个和窗体一样大的bitmap
            Graphics g = Graphics.FromImage(bit);
            g.CompositingQuality = CompositingQuality.HighQuality;//质量设为最高
                                                                  // g.CopyFromScreen(this.Left, this.Top, 0, 0, new Size(this.Width, this.Height));//保存整个窗体为图片
            g.CopyFromScreen(splitter1.PointToScreen(Point.Empty), Point.Empty, splitter1.Size);//只保存某个控件（splitter1）
            bit.Save(ScreenshotPath);//默认保存格式为PNG，保存成jpg格式质量不是很好

            //add image to pdf 
            //string imgurl = @System.Web.HttpContext.Current.Server.MapPath(Fimg);
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(ScreenshotPath);
            img.SetAbsolutePosition(50, 0);
            writer.DirectContent.AddImage(img);

          
            document.NewPage();  //new page
           // document.ResetPageCount();//reset page count 
            document.Add(new iTextSharp.text.Paragraph("Hello World! Hello People! " +
                    "Hello Sky! Hello Sun! Hello Moon! Hello Stars!"));

            document.NewPage();  //new page

            //  document.ResetPageCount();//reset page count 
            document.Add(new iTextSharp.text.Paragraph("Hello World! Hello People! " +
                    "Hello Sky! Hello Sun! Hello Moon! Hello Stars!"));

            document.NewPage();  //new page

            //  document.ResetPageCount();//reset page count 
            document.Add(new iTextSharp.text.Paragraph("Hello World! Hello People! " +
                    "Hello Sky! Hello Sun! Hello Moon! Hello Stars!"));

            document.NewPage();  //new page


            //Section subsection1 = section2.AddSection(20f, "Subsection 1.2.1", 3);
            //Section subsection2 = section2.AddSection(20f, "Subsection 1.2.2", 3);
            //Section subsubsection = subsection2.AddSection(20f, "Sub Subsection 1.2.2.1", 4);
            //Chapter chapter2 = new Chapter(new Paragraph("This is Chapter 2"), 1);
            //Section section3 = chapter2.AddSection("Section 2.1", 2);
            //Section subsection3 = section3.AddSection("Subsection 2.1.1", 3);
            //Section section4 = chapter2.AddSection("Section 2.2", 2);
            //chapter1.BookmarkTitle = "Changed Title";
            //chapter1.BookmarkOpen = true;
            //chapter2.BookmarkOpen = false;
            //document.Add(chapter1);
            //document.Add(chapter2);

            document.Close();
            MessageBox.Show("Report Success!","Prompt ");

        }

        private void Pdfcreat_Click(object sender, EventArgs e)
        {
            ExportPDF();
        }
    }
}

