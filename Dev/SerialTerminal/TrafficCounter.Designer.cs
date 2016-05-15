namespace SerialTerminal
{
    partial class TrafficCounter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this._graph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this._downloadGrp = new System.Windows.Forms.GroupBox();
            this._downloadTotal = new System.Windows.Forms.Label();
            this._downloadSpeed = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._uploadGrp = new System.Windows.Forms.GroupBox();
            this._uploadTotal = new System.Windows.Forms.Label();
            this._uploadSpeed = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this._graph)).BeginInit();
            this._downloadGrp.SuspendLayout();
            this._uploadGrp.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _graph
            // 
            this._graph.AccessibleName = "";
            this._graph.BackColor = System.Drawing.SystemColors.Control;
            this._graph.BorderlineColor = System.Drawing.Color.LightGray;
            this._graph.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea1.AxisX.Interval = 10D;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.Maximum = 20D;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.ScaleBreakStyle.Enabled = true;
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea1.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorTickMark.Enabled = false;
            chartArea1.AxisY.Maximum = 60D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea1.BackColor = System.Drawing.SystemColors.Control;
            chartArea1.Name = "ChartArea1";
            this._graph.ChartAreas.Add(chartArea1);
            this._graph.ContextMenuStrip = this.contextMenuStrip1;
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.AutoFitMinFontSize = 5;
            legend1.BackColor = System.Drawing.SystemColors.Control;
            legend1.DockedToChartArea = "ChartArea1";
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;
            legend1.Name = "Legend1";
            this._graph.Legends.Add(legend1);
            this._graph.Location = new System.Drawing.Point(85, 165);
            this._graph.Name = "_graph";
            this._graph.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Fire;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Color = System.Drawing.Color.Red;
            series1.Legend = "Legend1";
            series1.Name = "Up";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            series2.Legend = "Legend1";
            series2.Name = "Down";
            this._graph.Series.Add(series1);
            this._graph.Series.Add(series2);
            this._graph.Size = new System.Drawing.Size(139, 160);
            this._graph.TabIndex = 9;
            this._graph.Text = "chart1";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            title1.Name = "Title1";
            title1.Text = "Speed [KB/s] vs Time [s]";
            this._graph.Titles.Add(title1);
            // 
            // _downloadGrp
            // 
            this._downloadGrp.ContextMenuStrip = this.contextMenuStrip1;
            this._downloadGrp.Controls.Add(this._downloadTotal);
            this._downloadGrp.Controls.Add(this._downloadSpeed);
            this._downloadGrp.Controls.Add(this.label3);
            this._downloadGrp.Controls.Add(this.label4);
            this._downloadGrp.Location = new System.Drawing.Point(86, 107);
            this._downloadGrp.Name = "_downloadGrp";
            this._downloadGrp.Size = new System.Drawing.Size(139, 50);
            this._downloadGrp.TabIndex = 8;
            this._downloadGrp.TabStop = false;
            this._downloadGrp.Text = "Download";
            // 
            // _downloadTotal
            // 
            this._downloadTotal.AutoSize = true;
            this._downloadTotal.Location = new System.Drawing.Point(80, 29);
            this._downloadTotal.Name = "_downloadTotal";
            this._downloadTotal.Size = new System.Drawing.Size(34, 13);
            this._downloadTotal.TabIndex = 11;
            this._downloadTotal.Text = "0,000";
            // 
            // _downloadSpeed
            // 
            this._downloadSpeed.AutoSize = true;
            this._downloadSpeed.Location = new System.Drawing.Point(80, 16);
            this._downloadSpeed.Name = "_downloadSpeed";
            this._downloadSpeed.Size = new System.Drawing.Size(34, 13);
            this._downloadSpeed.TabIndex = 10;
            this._downloadSpeed.Text = "0,000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Total [KB]:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Speed [KB/s]:";
            // 
            // _uploadGrp
            // 
            this._uploadGrp.ContextMenuStrip = this.contextMenuStrip1;
            this._uploadGrp.Controls.Add(this._uploadTotal);
            this._uploadGrp.Controls.Add(this._uploadSpeed);
            this._uploadGrp.Controls.Add(this.label2);
            this._uploadGrp.Controls.Add(this.label1);
            this._uploadGrp.Location = new System.Drawing.Point(85, 51);
            this._uploadGrp.Name = "_uploadGrp";
            this._uploadGrp.Size = new System.Drawing.Size(139, 50);
            this._uploadGrp.TabIndex = 7;
            this._uploadGrp.TabStop = false;
            this._uploadGrp.Text = "Upload";
            // 
            // _uploadTotal
            // 
            this._uploadTotal.AutoSize = true;
            this._uploadTotal.Location = new System.Drawing.Point(80, 29);
            this._uploadTotal.Name = "_uploadTotal";
            this._uploadTotal.Size = new System.Drawing.Size(34, 13);
            this._uploadTotal.TabIndex = 9;
            this._uploadTotal.Text = "0,000";
            // 
            // _uploadSpeed
            // 
            this._uploadSpeed.AutoSize = true;
            this._uploadSpeed.Location = new System.Drawing.Point(80, 16);
            this._uploadSpeed.Name = "_uploadSpeed";
            this._uploadSpeed.Size = new System.Drawing.Size(34, 13);
            this._uploadSpeed.TabIndex = 8;
            this._uploadSpeed.Text = "0,000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Total [KB]:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Speed [KB/s]:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(103, 26);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.resetToolStripMenuItem.Text = "Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // TrafficCounter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._graph);
            this.Controls.Add(this._downloadGrp);
            this.Controls.Add(this._uploadGrp);
            this.Name = "TrafficCounter";
            this.Size = new System.Drawing.Size(313, 377);
            ((System.ComponentModel.ISupportInitialize)(this._graph)).EndInit();
            this._downloadGrp.ResumeLayout(false);
            this._downloadGrp.PerformLayout();
            this._uploadGrp.ResumeLayout(false);
            this._uploadGrp.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart _graph;
        private System.Windows.Forms.GroupBox _downloadGrp;
        private System.Windows.Forms.Label _downloadTotal;
        private System.Windows.Forms.Label _downloadSpeed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox _uploadGrp;
        private System.Windows.Forms.Label _uploadTotal;
        private System.Windows.Forms.Label _uploadSpeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
    }
}
