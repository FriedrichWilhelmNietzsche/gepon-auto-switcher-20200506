namespace wf_qc_foh_100
{
    partial class FOHW
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel7 = new System.Windows.Forms.Panel();
            this.userControl1_temperature = new wf_qc_foh_100.UserControl1_temperature();
            this.LGraphTest = new LILEI.UI.LGraph();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel6 = new System.Windows.Forms.Panel();
            this.ReceivedTextBox = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tb_onu_min = new System.Windows.Forms.TextBox();
            this.tb_temperature_max = new System.Windows.Forms.TextBox();
            this.lbl_onu_min = new System.Windows.Forms.Label();
            this.lbl_temperature_max = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_devices = new System.Windows.Forms.ComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pdfcreat = new System.Windows.Forms.Button();
            this.btn_itext = new System.Windows.Forms.Button();
            this.tb_report = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_report = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.tbxView = new System.Windows.Forms.TextBox();
            this.btnView = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_portOpen = new System.Windows.Forms.Button();
            this.btn_portSearch = new System.Windows.Forms.Button();
            this.PortcomboBox = new System.Windows.Forms.ComboBox();
            this.lbl_status = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_test_time = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.checkBox_manul = new System.Windows.Forms.CheckBox();
            this.lbl_send_count = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.checkBox_scatter = new System.Windows.Forms.CheckBox();
            this.tb_sample_frequency = new System.Windows.Forms.TextBox();
            this.btn_clear_graph = new System.Windows.Forms.Button();
            this.btn_stop_sample = new System.Windows.Forms.Button();
            this.btn_start_sample = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.timerDraw = new System.Windows.Forms.Timer(this.components);
            this.timerRandom = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Panel2.Controls.Add(this.panel5);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(5);
            this.splitContainer1.Size = new System.Drawing.Size(792, 573);
            this.splitContainer1.SplitterDistance = 578;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel7);
            this.splitContainer2.Panel1.Controls.Add(this.LGraphTest);
            this.splitContainer2.Panel1.Controls.Add(this.splitter1);
            this.splitContainer2.Panel1.Controls.Add(this.splitter2);
            this.splitContainer2.Panel1.Padding = new System.Windows.Forms.Padding(10);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel6);
            this.splitContainer2.Panel2.Controls.Add(this.panel3);
            this.splitContainer2.Size = new System.Drawing.Size(578, 573);
            this.splitContainer2.SplitterDistance = 426;
            this.splitContainer2.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.Controls.Add(this.userControl1_temperature);
            this.panel7.Location = new System.Drawing.Point(459, 8);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(109, 408);
            this.panel7.TabIndex = 4;
            // 
            // userControl1_temperature
            // 
            this.userControl1_temperature.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userControl1_temperature.AutoScroll = true;
            this.userControl1_temperature.BackGroundColor = System.Drawing.Color.WhiteSmoke;
            this.userControl1_temperature.CurValue = 0F;
            this.userControl1_temperature.Interval = 10;
            this.userControl1_temperature.Location = new System.Drawing.Point(0, 0);
            this.userControl1_temperature.MaxValue = 80;
            this.userControl1_temperature.MinValue = -20;
            this.userControl1_temperature.Name = "userControl1_temperature";
            this.userControl1_temperature.ShowTip = false;
            this.userControl1_temperature.Size = new System.Drawing.Size(109, 408);
            this.userControl1_temperature.TabIndex = 4;
            this.userControl1_temperature.ThermoColor = System.Drawing.Color.Red;
            this.userControl1_temperature.ThermoFont = new System.Drawing.Font("宋体", 10F);
            this.userControl1_temperature.ThermoTitle = "   ";
            // 
            // LGraphTest
            // 
            this.LGraphTest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LGraphTest.BackColor = System.Drawing.Color.White;
            this.LGraphTest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LGraphTest.Location = new System.Drawing.Point(10, 9);
            this.LGraphTest.m_backColorH = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.LGraphTest.m_backColorL = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.LGraphTest.m_BigXYBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.LGraphTest.m_BigXYButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.LGraphTest.m_BigXYButtonForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LGraphTest.m_ControlButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LGraphTest.m_ControlButtonForeColorH = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.LGraphTest.m_ControlButtonForeColorL = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.LGraphTest.m_ControlItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LGraphTest.m_coordinateLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LGraphTest.m_coordinateStringColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LGraphTest.m_coordinateStringTitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LGraphTest.m_DirectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.LGraphTest.m_DirectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.LGraphTest.m_fXBeginSYS = 0F;
            this.LGraphTest.m_fXEndSYS = 60F;
            this.LGraphTest.m_fYBeginSYS = 0F;
            this.LGraphTest.m_fYEndSYS = 1F;
            this.LGraphTest.m_GraphBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LGraphTest.m_iLineShowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.LGraphTest.m_iLineShowColorAlpha = 100;
            this.LGraphTest.m_SySnameX = "X";
            this.LGraphTest.m_SySnameY = "Y";
            this.LGraphTest.m_SyStitle = "Graph";
            this.LGraphTest.m_titleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.LGraphTest.m_titleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LGraphTest.m_titlePosition = 0.4F;
            this.LGraphTest.m_titleSize = 14;
            this.LGraphTest.Margin = new System.Windows.Forms.Padding(0);
            this.LGraphTest.MinimumSize = new System.Drawing.Size(390, 270);
            this.LGraphTest.Name = "LGraphTest";
            this.LGraphTest.Size = new System.Drawing.Size(443, 407);
            this.LGraphTest.TabIndex = 3;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(10, 10);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(435, 406);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(459, 10);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(109, 406);
            this.splitter2.TabIndex = 1;
            this.splitter2.TabStop = false;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel6.Controls.Add(this.ReceivedTextBox);
            this.panel6.Location = new System.Drawing.Point(10, 4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(307, 136);
            this.panel6.TabIndex = 918;
            // 
            // ReceivedTextBox
            // 
            this.ReceivedTextBox.AutoWordSelection = true;
            this.ReceivedTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ReceivedTextBox.EnableAutoDragDrop = true;
            this.ReceivedTextBox.Location = new System.Drawing.Point(19, 9);
            this.ReceivedTextBox.Name = "ReceivedTextBox";
            this.ReceivedTextBox.Size = new System.Drawing.Size(275, 119);
            this.ReceivedTextBox.TabIndex = 2;
            this.ReceivedTextBox.Text = "";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel3.Controls.Add(this.tb_onu_min);
            this.panel3.Controls.Add(this.tb_temperature_max);
            this.panel3.Controls.Add(this.lbl_onu_min);
            this.panel3.Controls.Add(this.lbl_temperature_max);
            this.panel3.Location = new System.Drawing.Point(334, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(226, 136);
            this.panel3.TabIndex = 3;
            // 
            // tb_onu_min
            // 
            this.tb_onu_min.BackColor = System.Drawing.SystemColors.Window;
            this.tb_onu_min.Location = new System.Drawing.Point(120, 74);
            this.tb_onu_min.Name = "tb_onu_min";
            this.tb_onu_min.ReadOnly = true;
            this.tb_onu_min.Size = new System.Drawing.Size(48, 21);
            this.tb_onu_min.TabIndex = 917;
            this.tb_onu_min.Text = "256";
            // 
            // tb_temperature_max
            // 
            this.tb_temperature_max.Location = new System.Drawing.Point(120, 25);
            this.tb_temperature_max.Name = "tb_temperature_max";
            this.tb_temperature_max.Size = new System.Drawing.Size(48, 21);
            this.tb_temperature_max.TabIndex = 916;
            this.tb_temperature_max.Text = "0";
            // 
            // lbl_onu_min
            // 
            this.lbl_onu_min.Location = new System.Drawing.Point(5, 77);
            this.lbl_onu_min.Name = "lbl_onu_min";
            this.lbl_onu_min.Size = new System.Drawing.Size(109, 12);
            this.lbl_onu_min.TabIndex = 901;
            this.lbl_onu_min.Text = "ONU Min:";
            // 
            // lbl_temperature_max
            // 
            this.lbl_temperature_max.Location = new System.Drawing.Point(5, 28);
            this.lbl_temperature_max.Name = "lbl_temperature_max";
            this.lbl_temperature_max.Size = new System.Drawing.Size(109, 12);
            this.lbl_temperature_max.TabIndex = 900;
            this.lbl_temperature_max.Text = "Temperature Max:";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.comboBox_devices);
            this.panel4.Location = new System.Drawing.Point(2, 10);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(204, 86);
            this.panel4.TabIndex = 909;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(8, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 12);
            this.label1.TabIndex = 911;
            this.label1.Text = "Devices select:";
            // 
            // comboBox_devices
            // 
            this.comboBox_devices.FormattingEnabled = true;
            this.comboBox_devices.Location = new System.Drawing.Point(8, 37);
            this.comboBox_devices.Name = "comboBox_devices";
            this.comboBox_devices.Size = new System.Drawing.Size(182, 20);
            this.comboBox_devices.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel5.Controls.Add(this.pdfcreat);
            this.panel5.Controls.Add(this.btn_itext);
            this.panel5.Controls.Add(this.tb_report);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.btn_report);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.tbxView);
            this.panel5.Controls.Add(this.btnView);
            this.panel5.Location = new System.Drawing.Point(2, 384);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(204, 175);
            this.panel5.TabIndex = 908;
            // 
            // pdfcreat
            // 
            this.pdfcreat.Location = new System.Drawing.Point(8, 67);
            this.pdfcreat.Name = "pdfcreat";
            this.pdfcreat.Size = new System.Drawing.Size(41, 23);
            this.pdfcreat.TabIndex = 918;
            this.pdfcreat.Text = "full";
            this.pdfcreat.UseVisualStyleBackColor = true;
            this.pdfcreat.Click += new System.EventHandler(this.Pdfcreat_Click);
            // 
            // btn_itext
            // 
            this.btn_itext.Location = new System.Drawing.Point(59, 67);
            this.btn_itext.Name = "btn_itext";
            this.btn_itext.Size = new System.Drawing.Size(75, 23);
            this.btn_itext.TabIndex = 917;
            this.btn_itext.Text = "itextsharp";
            this.btn_itext.UseVisualStyleBackColor = true;
            this.btn_itext.Click += new System.EventHandler(this.Btn_itext_Click);
            // 
            // tb_report
            // 
            this.tb_report.Location = new System.Drawing.Point(10, 37);
            this.tb_report.Name = "tb_report";
            this.tb_report.Size = new System.Drawing.Size(186, 21);
            this.tb_report.TabIndex = 916;
            this.tb_report.Text = "Test Report";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(6, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 12);
            this.label2.TabIndex = 913;
            this.label2.Text = "Report:";
            // 
            // btn_report
            // 
            this.btn_report.Location = new System.Drawing.Point(140, 67);
            this.btn_report.Name = "btn_report";
            this.btn_report.Size = new System.Drawing.Size(56, 23);
            this.btn_report.TabIndex = 912;
            this.btn_report.Text = "Report";
            this.btn_report.UseVisualStyleBackColor = true;
            this.btn_report.Click += new System.EventHandler(this.Btn_report_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(6, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 12);
            this.label8.TabIndex = 911;
            this.label8.Text = "Log:";
            // 
            // tbxView
            // 
            this.tbxView.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tbxView.ForeColor = System.Drawing.SystemColors.InfoText;
            this.tbxView.Location = new System.Drawing.Point(3, 118);
            this.tbxView.Name = "tbxView";
            this.tbxView.ReadOnly = true;
            this.tbxView.Size = new System.Drawing.Size(195, 21);
            this.tbxView.TabIndex = 893;
            this.tbxView.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(130, 143);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(56, 23);
            this.btnView.TabIndex = 894;
            this.btnView.Text = "View";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.BtnView_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.btn_portOpen);
            this.panel1.Controls.Add(this.btn_portSearch);
            this.panel1.Controls.Add(this.PortcomboBox);
            this.panel1.Controls.Add(this.lbl_status);
            this.panel1.Location = new System.Drawing.Point(2, 102);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(204, 100);
            this.panel1.TabIndex = 907;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(8, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 12);
            this.label7.TabIndex = 911;
            this.label7.Text = "Serial status:";
            // 
            // btn_portOpen
            // 
            this.btn_portOpen.Location = new System.Drawing.Point(152, 28);
            this.btn_portOpen.Name = "btn_portOpen";
            this.btn_portOpen.Size = new System.Drawing.Size(48, 23);
            this.btn_portOpen.TabIndex = 895;
            this.btn_portOpen.Text = "Open";
            this.btn_portOpen.UseVisualStyleBackColor = true;
            this.btn_portOpen.Click += new System.EventHandler(this.Btn_portOpen_Click);
            // 
            // btn_portSearch
            // 
            this.btn_portSearch.Location = new System.Drawing.Point(91, 29);
            this.btn_portSearch.Name = "btn_portSearch";
            this.btn_portSearch.Size = new System.Drawing.Size(55, 23);
            this.btn_portSearch.TabIndex = 721;
            this.btn_portSearch.Text = "Search";
            this.btn_portSearch.UseVisualStyleBackColor = true;
            this.btn_portSearch.Click += new System.EventHandler(this.Btn_portSearch_Click);
            // 
            // PortcomboBox
            // 
            this.PortcomboBox.FormattingEnabled = true;
            this.PortcomboBox.Location = new System.Drawing.Point(10, 31);
            this.PortcomboBox.Name = "PortcomboBox";
            this.PortcomboBox.Size = new System.Drawing.Size(75, 20);
            this.PortcomboBox.TabIndex = 720;
            // 
            // lbl_status
            // 
            this.lbl_status.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lbl_status.Location = new System.Drawing.Point(7, 60);
            this.lbl_status.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(171, 25);
            this.lbl_status.TabIndex = 719;
            this.lbl_status.Text = "label3";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.lbl_test_time);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.checkBox_manul);
            this.panel2.Controls.Add(this.lbl_send_count);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.checkBox_scatter);
            this.panel2.Controls.Add(this.tb_sample_frequency);
            this.panel2.Controls.Add(this.btn_clear_graph);
            this.panel2.Controls.Add(this.btn_stop_sample);
            this.panel2.Controls.Add(this.btn_start_sample);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Location = new System.Drawing.Point(2, 208);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(204, 161);
            this.panel2.TabIndex = 906;
            // 
            // lbl_test_time
            // 
            this.lbl_test_time.AutoSize = true;
            this.lbl_test_time.Location = new System.Drawing.Point(8, 109);
            this.lbl_test_time.Name = "lbl_test_time";
            this.lbl_test_time.Size = new System.Drawing.Size(47, 12);
            this.lbl_test_time.TabIndex = 897;
            this.lbl_test_time.Text = "Timer：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(8, 40);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 920;
            this.label10.Text = "Mode：";
            // 
            // checkBox_manul
            // 
            this.checkBox_manul.AutoSize = true;
            this.checkBox_manul.CheckAlign = System.Drawing.ContentAlignment.BottomRight;
            this.checkBox_manul.Location = new System.Drawing.Point(73, 39);
            this.checkBox_manul.Name = "checkBox_manul";
            this.checkBox_manul.Size = new System.Drawing.Size(54, 16);
            this.checkBox_manul.TabIndex = 919;
            this.checkBox_manul.Text = "Manul";
            this.checkBox_manul.UseVisualStyleBackColor = true;
            // 
            // lbl_send_count
            // 
            this.lbl_send_count.Location = new System.Drawing.Point(8, 86);
            this.lbl_send_count.Name = "lbl_send_count";
            this.lbl_send_count.Size = new System.Drawing.Size(89, 12);
            this.lbl_send_count.TabIndex = 898;
            this.lbl_send_count.Text = "Count：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 917;
            this.label11.Text = "Period(s):";
            // 
            // checkBox_scatter
            // 
            this.checkBox_scatter.AutoSize = true;
            this.checkBox_scatter.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox_scatter.Location = new System.Drawing.Point(130, 39);
            this.checkBox_scatter.Name = "checkBox_scatter";
            this.checkBox_scatter.Size = new System.Drawing.Size(66, 16);
            this.checkBox_scatter.TabIndex = 918;
            this.checkBox_scatter.Text = "Scatter";
            this.checkBox_scatter.UseVisualStyleBackColor = true;
            // 
            // tb_sample_frequency
            // 
            this.tb_sample_frequency.Location = new System.Drawing.Point(98, 62);
            this.tb_sample_frequency.Name = "tb_sample_frequency";
            this.tb_sample_frequency.Size = new System.Drawing.Size(48, 21);
            this.tb_sample_frequency.TabIndex = 915;
            // 
            // btn_clear_graph
            // 
            this.btn_clear_graph.Location = new System.Drawing.Point(10, 135);
            this.btn_clear_graph.Name = "btn_clear_graph";
            this.btn_clear_graph.Size = new System.Drawing.Size(43, 23);
            this.btn_clear_graph.TabIndex = 913;
            this.btn_clear_graph.Text = "Clear";
            this.btn_clear_graph.UseVisualStyleBackColor = true;
            this.btn_clear_graph.Click += new System.EventHandler(this.Btn_clear_graph_Click);
            // 
            // btn_stop_sample
            // 
            this.btn_stop_sample.Location = new System.Drawing.Point(75, 135);
            this.btn_stop_sample.Name = "btn_stop_sample";
            this.btn_stop_sample.Size = new System.Drawing.Size(46, 23);
            this.btn_stop_sample.TabIndex = 912;
            this.btn_stop_sample.Text = "Stop";
            this.btn_stop_sample.UseVisualStyleBackColor = true;
            this.btn_stop_sample.Click += new System.EventHandler(this.Btn_stop_sample_Click);
            // 
            // btn_start_sample
            // 
            this.btn_start_sample.Location = new System.Drawing.Point(145, 135);
            this.btn_start_sample.Name = "btn_start_sample";
            this.btn_start_sample.Size = new System.Drawing.Size(45, 23);
            this.btn_start_sample.TabIndex = 911;
            this.btn_start_sample.Text = "Start";
            this.btn_start_sample.UseVisualStyleBackColor = true;
            this.btn_start_sample.Click += new System.EventHandler(this.Btn_start_sample_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(8, 11);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 12);
            this.label12.TabIndex = 910;
            this.label12.Text = "Graph status:";
            // 
            // timerDraw
            // 
            this.timerDraw.Interval = 200;
            this.timerDraw.Tick += new System.EventHandler(this.timerDraw_Tick);
            // 
            // FOHW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FOHW";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GraphTest";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Timer timerDraw;
        private System.Windows.Forms.Timer timerRandom;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBox_manul;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tb_sample_frequency;
        private System.Windows.Forms.Button btn_clear_graph;
        private System.Windows.Forms.Button btn_stop_sample;
        private System.Windows.Forms.Button btn_start_sample;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_send_count;
        private System.Windows.Forms.Button btn_portOpen;
        private System.Windows.Forms.Button btn_portSearch;
        private System.Windows.Forms.ComboBox PortcomboBox;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lbl_test_time;
        private System.Windows.Forms.TextBox tbxView;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox ReceivedTextBox;
        private System.Windows.Forms.CheckBox checkBox_scatter;
        private LILEI.UI.LGraph LGraphTest;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private UserControl1_temperature userControl1_temperature;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbl_onu_min;
        private System.Windows.Forms.Label lbl_temperature_max;
        private System.Windows.Forms.TextBox tb_onu_min;
        private System.Windows.Forms.TextBox tb_temperature_max;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_devices;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btn_report;
        private System.Windows.Forms.TextBox tb_report;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_itext;
        private System.Windows.Forms.Button pdfcreat;
    }
}