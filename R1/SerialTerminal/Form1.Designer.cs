namespace SerialTerminal
{
    partial class _mainForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this._sendBtn = new System.Windows.Forms.Button();
            this.SendBtnContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.onceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inIntervalmsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripTextBox();
            this.afterSpecialCharToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.appendEndOfLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cRLFToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cRToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._portSettingsGrp = new System.Windows.Forms.GroupBox();
            this._openBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this._portName = new System.Windows.Forms.ComboBox();
            this._portRate = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this._bandwidthGrp = new System.Windows.Forms.GroupBox();
            this.TransferedContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.resetCounterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._dnSpeed = new System.Windows.Forms.Label();
            this._upSpeed = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._transferedGrp = new System.Windows.Forms.GroupBox();
            this._downloaded = new System.Windows.Forms.Label();
            this._uploaded = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._bandwidthGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this._cmdBox = new System.Windows.Forms.ComboBox();
            this.CmdLineContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._consoleBox = new System.Windows.Forms.TextBox();
            this.ConsoleContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logtxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clearLogFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.wordWrapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localEchoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nonPrintableCharsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transmitEnterAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cRLFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SendBtnContextMenu.SuspendLayout();
            this._portSettingsGrp.SuspendLayout();
            this._bandwidthGrp.SuspendLayout();
            this.TransferedContextMenu.SuspendLayout();
            this._transferedGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._bandwidthGraph)).BeginInit();
            this.CmdLineContextMenu.SuspendLayout();
            this.ConsoleContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _sendBtn
            // 
            this._sendBtn.ContextMenuStrip = this.SendBtnContextMenu;
            this._sendBtn.Enabled = false;
            this._sendBtn.Location = new System.Drawing.Point(608, 503);
            this._sendBtn.Name = "_sendBtn";
            this._sendBtn.Size = new System.Drawing.Size(128, 23);
            this._sendBtn.TabIndex = 2;
            this._sendBtn.TabStop = false;
            this._sendBtn.Text = "Send";
            this._sendBtn.UseVisualStyleBackColor = true;
            this._sendBtn.Click += new System.EventHandler(this._sendBtn_Click);
            // 
            // SendBtnContextMenu
            // 
            this.SendBtnContextMenu.BackColor = System.Drawing.SystemColors.MenuBar;
            this.SendBtnContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onceToolStripMenuItem,
            this.inIntervalmsToolStripMenuItem,
            this.afterSpecialCharToolStripMenuItem1,
            this.toolStripSeparator5,
            this.appendEndOfLineToolStripMenuItem});
            this.SendBtnContextMenu.Name = "SendBtnContextMenu";
            this.SendBtnContextMenu.Size = new System.Drawing.Size(179, 98);
            // 
            // onceToolStripMenuItem
            // 
            this.onceToolStripMenuItem.Checked = true;
            this.onceToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.onceToolStripMenuItem.Name = "onceToolStripMenuItem";
            this.onceToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.onceToolStripMenuItem.Text = "Once";
            this.onceToolStripMenuItem.Click += new System.EventHandler(this.onceToolStripMenuItem_Click);
            // 
            // inIntervalmsToolStripMenuItem
            // 
            this.inIntervalmsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2});
            this.inIntervalmsToolStripMenuItem.Name = "inIntervalmsToolStripMenuItem";
            this.inIntervalmsToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.inIntervalmsToolStripMenuItem.Text = "Periodically";
            this.inIntervalmsToolStripMenuItem.Click += new System.EventHandler(this.inIntervalmsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 23);
            this.toolStripMenuItem2.Text = "1000";
            this.toolStripMenuItem2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripMenuItem2_KeyPress);
            // 
            // afterSpecialCharToolStripMenuItem1
            // 
            this.afterSpecialCharToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3});
            this.afterSpecialCharToolStripMenuItem1.Name = "afterSpecialCharToolStripMenuItem1";
            this.afterSpecialCharToolStripMenuItem1.Size = new System.Drawing.Size(178, 22);
            this.afterSpecialCharToolStripMenuItem1.Text = "After Special Char";
            this.afterSpecialCharToolStripMenuItem1.Click += new System.EventHandler(this.afterSpecialCharToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(152, 23);
            this.toolStripMenuItem3.Text = "*";
            this.toolStripMenuItem3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripMenuItem3_KeyPress);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(175, 6);
            // 
            // appendEndOfLineToolStripMenuItem
            // 
            this.appendEndOfLineToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cRLFToolStripMenuItem1,
            this.cRToolStripMenuItem1,
            this.lFToolStripMenuItem,
            this.toolStripSeparator6,
            this.noneToolStripMenuItem});
            this.appendEndOfLineToolStripMenuItem.Name = "appendEndOfLineToolStripMenuItem";
            this.appendEndOfLineToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.appendEndOfLineToolStripMenuItem.Text = "Append End of Line";
            // 
            // cRLFToolStripMenuItem1
            // 
            this.cRLFToolStripMenuItem1.Name = "cRLFToolStripMenuItem1";
            this.cRLFToolStripMenuItem1.Size = new System.Drawing.Size(103, 22);
            this.cRLFToolStripMenuItem1.Text = "CRLF";
            this.cRLFToolStripMenuItem1.Click += new System.EventHandler(this.cRLFToolStripMenuItem1_Click);
            // 
            // cRToolStripMenuItem1
            // 
            this.cRToolStripMenuItem1.Name = "cRToolStripMenuItem1";
            this.cRToolStripMenuItem1.Size = new System.Drawing.Size(103, 22);
            this.cRToolStripMenuItem1.Text = "CR";
            this.cRToolStripMenuItem1.Click += new System.EventHandler(this.cRToolStripMenuItem1_Click);
            // 
            // lFToolStripMenuItem
            // 
            this.lFToolStripMenuItem.Name = "lFToolStripMenuItem";
            this.lFToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.lFToolStripMenuItem.Text = "LF";
            this.lFToolStripMenuItem.Click += new System.EventHandler(this.lFToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(100, 6);
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.noneToolStripMenuItem.Text = "None";
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.noneToolStripMenuItem_Click);
            // 
            // _portSettingsGrp
            // 
            this._portSettingsGrp.Controls.Add(this._openBtn);
            this._portSettingsGrp.Controls.Add(this.label5);
            this._portSettingsGrp.Controls.Add(this._portName);
            this._portSettingsGrp.Controls.Add(this._portRate);
            this._portSettingsGrp.Controls.Add(this.label6);
            this._portSettingsGrp.Location = new System.Drawing.Point(13, 13);
            this._portSettingsGrp.Name = "_portSettingsGrp";
            this._portSettingsGrp.Size = new System.Drawing.Size(139, 130);
            this._portSettingsGrp.TabIndex = 3;
            this._portSettingsGrp.TabStop = false;
            this._portSettingsGrp.Text = "Port Settings (8-N-1)";
            // 
            // _openBtn
            // 
            this._openBtn.Location = new System.Drawing.Point(10, 99);
            this._openBtn.Name = "_openBtn";
            this._openBtn.Size = new System.Drawing.Size(121, 23);
            this._openBtn.TabIndex = 0;
            this._openBtn.TabStop = false;
            this._openBtn.Text = "Open";
            this._openBtn.UseVisualStyleBackColor = true;
            this._openBtn.Click += new System.EventHandler(this._connectBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Name";
            // 
            // _portName
            // 
            this._portName.FormattingEnabled = true;
            this._portName.Location = new System.Drawing.Point(10, 32);
            this._portName.Name = "_portName";
            this._portName.Size = new System.Drawing.Size(121, 21);
            this._portName.TabIndex = 1;
            this._portName.TabStop = false;
            this._portName.DropDown += new System.EventHandler(this._portName_DropDown);
            // 
            // _portRate
            // 
            this._portRate.FormattingEnabled = true;
            this._portRate.Location = new System.Drawing.Point(10, 72);
            this._portRate.Name = "_portRate";
            this._portRate.Size = new System.Drawing.Size(121, 21);
            this._portRate.TabIndex = 2;
            this._portRate.TabStop = false;
            this._portRate.DropDown += new System.EventHandler(this._portRate_DropDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Baudrate";
            // 
            // _bandwidthGrp
            // 
            this._bandwidthGrp.ContextMenuStrip = this.TransferedContextMenu;
            this._bandwidthGrp.Controls.Add(this._dnSpeed);
            this._bandwidthGrp.Controls.Add(this._upSpeed);
            this._bandwidthGrp.Controls.Add(this.label2);
            this._bandwidthGrp.Controls.Add(this.label1);
            this._bandwidthGrp.Location = new System.Drawing.Point(12, 252);
            this._bandwidthGrp.Name = "_bandwidthGrp";
            this._bandwidthGrp.Size = new System.Drawing.Size(139, 50);
            this._bandwidthGrp.TabIndex = 4;
            this._bandwidthGrp.TabStop = false;
            this._bandwidthGrp.Text = "Speed [KB/s]";
            // 
            // TransferedContextMenu
            // 
            this.TransferedContextMenu.BackColor = System.Drawing.SystemColors.MenuBar;
            this.TransferedContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetCounterToolStripMenuItem});
            this.TransferedContextMenu.Name = "TransferedContextMenu";
            this.TransferedContextMenu.Size = new System.Drawing.Size(103, 26);
            // 
            // resetCounterToolStripMenuItem
            // 
            this.resetCounterToolStripMenuItem.Name = "resetCounterToolStripMenuItem";
            this.resetCounterToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.resetCounterToolStripMenuItem.Text = "Reset";
            this.resetCounterToolStripMenuItem.Click += new System.EventHandler(this.resetCounterToolStripMenuItem_Click);
            // 
            // _dnSpeed
            // 
            this._dnSpeed.AutoSize = true;
            this._dnSpeed.Location = new System.Drawing.Point(69, 29);
            this._dnSpeed.Name = "_dnSpeed";
            this._dnSpeed.Size = new System.Drawing.Size(34, 13);
            this._dnSpeed.TabIndex = 9;
            this._dnSpeed.Text = "0,000";
            // 
            // _upSpeed
            // 
            this._upSpeed.AutoSize = true;
            this._upSpeed.Location = new System.Drawing.Point(69, 16);
            this._upSpeed.Name = "_upSpeed";
            this._upSpeed.Size = new System.Drawing.Size(34, 13);
            this._upSpeed.TabIndex = 8;
            this._upSpeed.Text = "0,000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Download:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Upload:";
            // 
            // _transferedGrp
            // 
            this._transferedGrp.ContextMenuStrip = this.TransferedContextMenu;
            this._transferedGrp.Controls.Add(this._downloaded);
            this._transferedGrp.Controls.Add(this._uploaded);
            this._transferedGrp.Controls.Add(this.label3);
            this._transferedGrp.Controls.Add(this.label4);
            this._transferedGrp.Location = new System.Drawing.Point(13, 308);
            this._transferedGrp.Name = "_transferedGrp";
            this._transferedGrp.Size = new System.Drawing.Size(139, 50);
            this._transferedGrp.TabIndex = 5;
            this._transferedGrp.TabStop = false;
            this._transferedGrp.Text = "Transfered [KB]";
            // 
            // _downloaded
            // 
            this._downloaded.AutoSize = true;
            this._downloaded.Location = new System.Drawing.Point(68, 29);
            this._downloaded.Name = "_downloaded";
            this._downloaded.Size = new System.Drawing.Size(34, 13);
            this._downloaded.TabIndex = 11;
            this._downloaded.Text = "0,000";
            // 
            // _uploaded
            // 
            this._uploaded.AutoSize = true;
            this._uploaded.Location = new System.Drawing.Point(68, 16);
            this._uploaded.Name = "_uploaded";
            this._uploaded.Size = new System.Drawing.Size(34, 13);
            this._uploaded.TabIndex = 10;
            this._uploaded.Text = "0,000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Download:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Upload:";
            // 
            // _bandwidthGraph
            // 
            this._bandwidthGraph.AccessibleName = "";
            this._bandwidthGraph.BackColor = System.Drawing.SystemColors.Control;
            this._bandwidthGraph.BorderlineColor = System.Drawing.Color.LightGray;
            this._bandwidthGraph.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea2.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea2.AxisX.Interval = 10D;
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisX.Maximum = 20D;
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.AxisX.ScaleBreakStyle.Enabled = true;
            chartArea2.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea2.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea2.AxisY.IsLabelAutoFit = false;
            chartArea2.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea2.AxisY.MajorGrid.Enabled = false;
            chartArea2.AxisY.MajorTickMark.Enabled = false;
            chartArea2.AxisY.Maximum = 60D;
            chartArea2.AxisY.Minimum = 0D;
            chartArea2.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea2.BackColor = System.Drawing.SystemColors.Control;
            chartArea2.Name = "ChartArea1";
            this._bandwidthGraph.ChartAreas.Add(chartArea2);
            this._bandwidthGraph.ContextMenuStrip = this.TransferedContextMenu;
            legend2.Alignment = System.Drawing.StringAlignment.Center;
            legend2.AutoFitMinFontSize = 5;
            legend2.BackColor = System.Drawing.SystemColors.Control;
            legend2.DockedToChartArea = "ChartArea1";
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend2.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;
            legend2.Name = "Legend1";
            this._bandwidthGraph.Legends.Add(legend2);
            this._bandwidthGraph.Location = new System.Drawing.Point(12, 366);
            this._bandwidthGraph.Name = "_bandwidthGraph";
            this._bandwidthGraph.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Fire;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Color = System.Drawing.Color.Red;
            series3.Legend = "Legend1";
            series3.Name = "Up";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            series4.Legend = "Legend1";
            series4.Name = "Down";
            this._bandwidthGraph.Series.Add(series3);
            this._bandwidthGraph.Series.Add(series4);
            this._bandwidthGraph.Size = new System.Drawing.Size(139, 160);
            this._bandwidthGraph.TabIndex = 6;
            this._bandwidthGraph.Text = "chart1";
            title2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            title2.Name = "Title1";
            title2.Text = "Speed [KB/s] vs Time [s]";
            this._bandwidthGraph.Titles.Add(title2);
            // 
            // _cmdBox
            // 
            this._cmdBox.ContextMenuStrip = this.CmdLineContextMenu;
            this._cmdBox.Enabled = false;
            this._cmdBox.FormattingEnabled = true;
            this._cmdBox.Location = new System.Drawing.Point(158, 505);
            this._cmdBox.Name = "_cmdBox";
            this._cmdBox.Size = new System.Drawing.Size(444, 21);
            this._cmdBox.TabIndex = 1;
            this._cmdBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._cmdBox_KeyPress);
            // 
            // CmdLineContextMenu
            // 
            this.CmdLineContextMenu.BackColor = System.Drawing.SystemColors.MenuBar;
            this.CmdLineContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem1,
            this.selectAllToolStripMenuItem1,
            this.pasteToolStripMenuItem});
            this.CmdLineContextMenu.Name = "CmdLineContextMenu";
            this.CmdLineContextMenu.Size = new System.Drawing.Size(123, 70);
            // 
            // copyToolStripMenuItem1
            // 
            this.copyToolStripMenuItem1.Name = "copyToolStripMenuItem1";
            this.copyToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
            this.copyToolStripMenuItem1.Text = "Copy";
            this.copyToolStripMenuItem1.Click += new System.EventHandler(this.copyToolStripMenuItem1_Click);
            // 
            // selectAllToolStripMenuItem1
            // 
            this.selectAllToolStripMenuItem1.Name = "selectAllToolStripMenuItem1";
            this.selectAllToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
            this.selectAllToolStripMenuItem1.Text = "Select All";
            this.selectAllToolStripMenuItem1.Click += new System.EventHandler(this.selectAllToolStripMenuItem1_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // _consoleBox
            // 
            this._consoleBox.ContextMenuStrip = this.ConsoleContextMenu;
            this._consoleBox.Location = new System.Drawing.Point(158, 13);
            this._consoleBox.Multiline = true;
            this._consoleBox.Name = "_consoleBox";
            this._consoleBox.ReadOnly = true;
            this._consoleBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._consoleBox.Size = new System.Drawing.Size(578, 484);
            this._consoleBox.TabIndex = 0;
            this._consoleBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this._consoleBox_KeyDown);
            this._consoleBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._consoleBox_KeyPress);
            // 
            // ConsoleContextMenu
            // 
            this.ConsoleContextMenu.BackColor = System.Drawing.SystemColors.MenuBar;
            this.ConsoleContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.toolStripSeparator4,
            this.logToolStripMenuItem,
            this.toolStripSeparator2,
            this.clearToolStripMenuItem,
            this.toolStripSeparator3,
            this.wordWrapToolStripMenuItem,
            this.localEchoToolStripMenuItem,
            this.nonPrintableCharsToolStripMenuItem,
            this.transmitEnterAsToolStripMenuItem});
            this.ConsoleContextMenu.Name = "contextMenuStrip1";
            this.ConsoleContextMenu.Size = new System.Drawing.Size(215, 198);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(211, 6);
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enabledToolStripMenuItem,
            this.logtxtToolStripMenuItem,
            this.toolStripSeparator1,
            this.clearLogFileToolStripMenuItem});
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.logToolStripMenuItem.Text = "Log to File";
            // 
            // enabledToolStripMenuItem
            // 
            this.enabledToolStripMenuItem.Name = "enabledToolStripMenuItem";
            this.enabledToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.enabledToolStripMenuItem.Text = "Enabled";
            this.enabledToolStripMenuItem.Click += new System.EventHandler(this.enabledToolStripMenuItem_Click);
            // 
            // logtxtToolStripMenuItem
            // 
            this.logtxtToolStripMenuItem.Name = "logtxtToolStripMenuItem";
            this.logtxtToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.logtxtToolStripMenuItem.Text = "log.txt";
            this.logtxtToolStripMenuItem.Click += new System.EventHandler(this.logtxtToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(142, 6);
            // 
            // clearLogFileToolStripMenuItem
            // 
            this.clearLogFileToolStripMenuItem.Name = "clearLogFileToolStripMenuItem";
            this.clearLogFileToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.clearLogFileToolStripMenuItem.Text = "Clear Log File";
            this.clearLogFileToolStripMenuItem.Click += new System.EventHandler(this.clearLogFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(211, 6);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.clearToolStripMenuItem.Text = "Clear Console";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(211, 6);
            // 
            // wordWrapToolStripMenuItem
            // 
            this.wordWrapToolStripMenuItem.Name = "wordWrapToolStripMenuItem";
            this.wordWrapToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.wordWrapToolStripMenuItem.Text = "Word Wrap";
            this.wordWrapToolStripMenuItem.Click += new System.EventHandler(this.wordWrapToolStripMenuItem_Click);
            // 
            // localEchoToolStripMenuItem
            // 
            this.localEchoToolStripMenuItem.Name = "localEchoToolStripMenuItem";
            this.localEchoToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.localEchoToolStripMenuItem.Text = "Local Echo";
            this.localEchoToolStripMenuItem.Click += new System.EventHandler(this.localEchoToolStripMenuItem_Click);
            // 
            // nonPrintableCharsToolStripMenuItem
            // 
            this.nonPrintableCharsToolStripMenuItem.Name = "nonPrintableCharsToolStripMenuItem";
            this.nonPrintableCharsToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.nonPrintableCharsToolStripMenuItem.Text = "Show Non-Printable Chars";
            this.nonPrintableCharsToolStripMenuItem.Click += new System.EventHandler(this.nonPrintableCharsToolStripMenuItem_Click);
            // 
            // transmitEnterAsToolStripMenuItem
            // 
            this.transmitEnterAsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cRLFToolStripMenuItem,
            this.cRToolStripMenuItem,
            this.nToolStripMenuItem});
            this.transmitEnterAsToolStripMenuItem.Name = "transmitEnterAsToolStripMenuItem";
            this.transmitEnterAsToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.transmitEnterAsToolStripMenuItem.Text = "Transmit Enter as";
            // 
            // cRLFToolStripMenuItem
            // 
            this.cRLFToolStripMenuItem.Name = "cRLFToolStripMenuItem";
            this.cRLFToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.cRLFToolStripMenuItem.Text = "CRLF";
            this.cRLFToolStripMenuItem.Click += new System.EventHandler(this.cRLFToolStripMenuItem_Click);
            // 
            // cRToolStripMenuItem
            // 
            this.cRToolStripMenuItem.Name = "cRToolStripMenuItem";
            this.cRToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.cRToolStripMenuItem.Text = "CR";
            this.cRToolStripMenuItem.Click += new System.EventHandler(this.cRToolStripMenuItem_Click);
            // 
            // nToolStripMenuItem
            // 
            this.nToolStripMenuItem.Name = "nToolStripMenuItem";
            this.nToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.nToolStripMenuItem.Text = "LF";
            this.nToolStripMenuItem.Click += new System.EventHandler(this.nToolStripMenuItem_Click);
            // 
            // _mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 535);
            this.Controls.Add(this._cmdBox);
            this.Controls.Add(this._bandwidthGraph);
            this.Controls.Add(this._transferedGrp);
            this.Controls.Add(this._bandwidthGrp);
            this.Controls.Add(this._portSettingsGrp);
            this.Controls.Add(this._sendBtn);
            this.Controls.Add(this._consoleBox);
            this.Name = "_mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SerialTerminal v1.0 by Standa Subrt aka Stannis @ Brno 2014";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this._mainForm_FormClosed);
            this.Load += new System.EventHandler(this._mainForm_Load);
            this.Resize += new System.EventHandler(this._mainForm_Resize);
            this.SendBtnContextMenu.ResumeLayout(false);
            this._portSettingsGrp.ResumeLayout(false);
            this._portSettingsGrp.PerformLayout();
            this._bandwidthGrp.ResumeLayout(false);
            this._bandwidthGrp.PerformLayout();
            this.TransferedContextMenu.ResumeLayout(false);
            this._transferedGrp.ResumeLayout(false);
            this._transferedGrp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._bandwidthGraph)).EndInit();
            this.CmdLineContextMenu.ResumeLayout(false);
            this.ConsoleContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _sendBtn;
        private System.Windows.Forms.GroupBox _portSettingsGrp;
        private System.Windows.Forms.ComboBox _portRate;
        private System.Windows.Forms.ComboBox _portName;
        private System.Windows.Forms.Button _openBtn;
        private System.Windows.Forms.GroupBox _bandwidthGrp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox _transferedGrp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataVisualization.Charting.Chart _bandwidthGraph;
        private System.Windows.Forms.ComboBox _cmdBox;
        private System.Windows.Forms.TextBox _consoleBox;
        private System.Windows.Forms.Label _dnSpeed;
        private System.Windows.Forms.Label _upSpeed;
        private System.Windows.Forms.Label _downloaded;
        private System.Windows.Forms.Label _uploaded;
        private System.Windows.Forms.ContextMenuStrip ConsoleContextMenu;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip TransferedContextMenu;
        private System.Windows.Forms.ToolStripMenuItem resetCounterToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip SendBtnContextMenu;
        private System.Windows.Forms.ToolStripMenuItem logtxtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearLogFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem onceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inIntervalmsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem afterSpecialCharToolStripMenuItem1;
        private System.Windows.Forms.ToolStripTextBox toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem localEchoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transmitEnterAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cRLFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cRToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem wordWrapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nonPrintableCharsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ContextMenuStrip CmdLineContextMenu;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enabledToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem appendEndOfLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cRLFToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cRToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem lFToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}

