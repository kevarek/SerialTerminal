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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_mainForm));
            this._terminal = new SerialTerminal.Terminal();
            this._trafficCounter = new SerialTerminal.TrafficCounter();
            this._smartSerialPort = new SerialTerminal.SmartSerialPort();
            this.SuspendLayout();
            // 
            // _terminal
            // 
            this._terminal.Enabled = false;
            this._terminal.EOLMark = SerialTerminal.Terminal.EOLMarks.crlf;
            this._terminal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._terminal.HexTagsReplacingEnabled = false;
            this._terminal.LocalEchoEnabled = false;
            this._terminal.Location = new System.Drawing.Point(161, 12);
            this._terminal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._terminal.Name = "_terminal";
            this._terminal.RedrawIntervalMs = 100;
            this._terminal.SendBtnWidth = 150;
            this._terminal.ShowNPC = false;
            this._terminal.Size = new System.Drawing.Size(690, 555);
            this._terminal.Spacing = 4;
            this._terminal.TabIndex = 1;
            this._terminal.TerminalWordWrap = true;

            // 
            // _trafficCounter
            // 
            this._trafficCounter.ChartXAxisRange = 60;
            this._trafficCounter.ChartYMax = 9600D;
            this._trafficCounter.Location = new System.Drawing.Point(5, 316);
            this._trafficCounter.Name = "_trafficCounter";
            this._trafficCounter.RefreshInterval = 1000;
            this._trafficCounter.Size = new System.Drawing.Size(150, 268);
            this._trafficCounter.Spacing = 4;
            this._trafficCounter.TabIndex = 2;
            this._trafficCounter.TabStop = false;
            // 
            // _smartSerialPort
            // 
            this._smartSerialPort.Location = new System.Drawing.Point(5, 5);
            this._smartSerialPort.Name = "_smartSerialPort";
            this._smartSerialPort.Size = new System.Drawing.Size(150, 263);
            this._smartSerialPort.TabIndex = 0;
            // 
            // _mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 588);
            this.Controls.Add(this._terminal);
            this.Controls.Add(this._trafficCounter);
            this.Controls.Add(this._smartSerialPort);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "_mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SerialTerminal v3.0 by Standa Subrt aka Stannis @ Brno 2015";
            this.ResumeLayout(false);

        }

        #endregion

        private SmartSerialPort _smartSerialPort;
        private TrafficCounter _trafficCounter;
        private Terminal _terminal;


    }
}