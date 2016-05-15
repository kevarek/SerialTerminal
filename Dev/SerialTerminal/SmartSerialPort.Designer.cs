namespace SerialTerminal
{
    partial class SmartSerialPort
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
            this._frame = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this._portStopbits = new System.Windows.Forms.ComboBox();
            this._portDatabits = new System.Windows.Forms.ComboBox();
            this._portBaudRateContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this._openBtn = new System.Windows.Forms.Button();
            this._portNameLabel = new System.Windows.Forms.Label();
            this._portName = new System.Windows.Forms.ComboBox();
            this._portNameContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._portBaudRate = new System.Windows.Forms.ComboBox();
            this._baudRateLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._portParity = new System.Windows.Forms.ComboBox();
            this._frame.SuspendLayout();
            this.SuspendLayout();
            // 
            // _frame
            // 
            this._frame.Controls.Add(this.label1);
            this._frame.Controls.Add(this._portParity);
            this._frame.Controls.Add(this.label2);
            this._frame.Controls.Add(this._portStopbits);
            this._frame.Controls.Add(this._portDatabits);
            this._frame.Controls.Add(this.label3);
            this._frame.Controls.Add(this._openBtn);
            this._frame.Controls.Add(this._portNameLabel);
            this._frame.Controls.Add(this._portName);
            this._frame.Controls.Add(this._portBaudRate);
            this._frame.Controls.Add(this._baudRateLabel);
            this._frame.Location = new System.Drawing.Point(3, 3);
            this._frame.Name = "_frame";
            this._frame.Size = new System.Drawing.Size(141, 253);
            this._frame.TabIndex = 2;
            this._frame.TabStop = false;
            this._frame.Text = "Serial Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Stopbits";
            // 
            // _portStopbits
            // 
            this._portStopbits.FormattingEnabled = true;
            this._portStopbits.Location = new System.Drawing.Point(10, 192);
            this._portStopbits.Name = "_portStopbits";
            this._portStopbits.Size = new System.Drawing.Size(121, 21);
            this._portStopbits.TabIndex = 7;
            // 
            // _portDatabits
            // 
            this._portDatabits.ContextMenuStrip = this._portBaudRateContextMenu;
            this._portDatabits.FormattingEnabled = true;
            this._portDatabits.Location = new System.Drawing.Point(9, 112);
            this._portDatabits.Name = "_portDatabits";
            this._portDatabits.Size = new System.Drawing.Size(121, 21);
            this._portDatabits.TabIndex = 6;
            // 
            // _portBaudRateContextMenu
            // 
            this._portBaudRateContextMenu.Name = "_portBaudRateContextMenu";
            this._portBaudRateContextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Databits";
            // 
            // _openBtn
            // 
            this._openBtn.Location = new System.Drawing.Point(10, 219);
            this._openBtn.Name = "_openBtn";
            this._openBtn.Size = new System.Drawing.Size(121, 23);
            this._openBtn.TabIndex = 0;
            this._openBtn.Text = "Open";
            this._openBtn.UseVisualStyleBackColor = true;
            // 
            // _portNameLabel
            // 
            this._portNameLabel.AutoSize = true;
            this._portNameLabel.Location = new System.Drawing.Point(7, 16);
            this._portNameLabel.Name = "_portNameLabel";
            this._portNameLabel.Size = new System.Drawing.Size(35, 13);
            this._portNameLabel.TabIndex = 3;
            this._portNameLabel.Text = "Name";
            // 
            // _portName
            // 
            this._portName.ContextMenuStrip = this._portNameContextMenu;
            this._portName.FormattingEnabled = true;
            this._portName.Location = new System.Drawing.Point(10, 32);
            this._portName.Name = "_portName";
            this._portName.Size = new System.Drawing.Size(121, 21);
            this._portName.TabIndex = 1;
            // 
            // _portNameContextMenu
            // 
            this._portNameContextMenu.Name = "_portNameContextMenu";
            this._portNameContextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // _portBaudRate
            // 
            this._portBaudRate.ContextMenuStrip = this._portBaudRateContextMenu;
            this._portBaudRate.FormattingEnabled = true;
            this._portBaudRate.Location = new System.Drawing.Point(10, 72);
            this._portBaudRate.Name = "_portBaudRate";
            this._portBaudRate.Size = new System.Drawing.Size(121, 21);
            this._portBaudRate.TabIndex = 2;
            // 
            // _baudRateLabel
            // 
            this._baudRateLabel.AutoSize = true;
            this._baudRateLabel.Location = new System.Drawing.Point(7, 56);
            this._baudRateLabel.Name = "_baudRateLabel";
            this._baudRateLabel.Size = new System.Drawing.Size(50, 13);
            this._baudRateLabel.TabIndex = 4;
            this._baudRateLabel.Text = "Baudrate";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Parity";
            // 
            // _portParity
            // 
            this._portParity.FormattingEnabled = true;
            this._portParity.Location = new System.Drawing.Point(9, 152);
            this._portParity.Name = "_portParity";
            this._portParity.Size = new System.Drawing.Size(121, 21);
            this._portParity.TabIndex = 10;
            // 
            // SmartSerialPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._frame);
            this.Name = "SmartSerialPort";
            this.Size = new System.Drawing.Size(207, 309);
            this._frame.ResumeLayout(false);
            this._frame.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox _frame;
        private System.Windows.Forms.Button _openBtn;
        private System.Windows.Forms.Label _portNameLabel;
        private System.Windows.Forms.ComboBox _portName;
        private System.Windows.Forms.ComboBox _portBaudRate;
        private System.Windows.Forms.Label _baudRateLabel;
        private System.Windows.Forms.ContextMenuStrip _portNameContextMenu;
        private System.Windows.Forms.ContextMenuStrip _portBaudRateContextMenu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _portStopbits;
        private System.Windows.Forms.ComboBox _portDatabits;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _portParity;
    }
}
