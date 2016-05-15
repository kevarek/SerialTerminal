namespace SerialTerminal
{
    partial class Terminal
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
            this._terminalWin = new System.Windows.Forms.TextBox();
            this._terminalContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._clearTerminalContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this._saveTerminalContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._localEchoTerminalContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._wordWrapTerminalContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._endOfLineCharTerminalContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._cRLFTerminalContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._cRTerminalContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._lFTerminalContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._showNPCTerminalContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._disableRedrawTerminalContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RedrawIntervalTextBox = new System.Windows.Forms.ToolStripTextBox();
            this._cmdLine = new System.Windows.Forms.ComboBox();
            this._cmdLineContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._selectAllCmdLineContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._clearAllCmdLineContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this._copyCmdLineContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._cutCmdLineContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._pasteCmdLineContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this._sendOnceCmdLineContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._sendPeriodicallyCmdLineContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._periodTerminalContextMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this._replaceHexTagsCmdLineContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._sendBtn = new System.Windows.Forms.Button();
            this._sendBtnContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._sendOnceSendBtnContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._sendPeriodicallySendBtnContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._terminalContextMenu.SuspendLayout();
            this._cmdLineContextMenu.SuspendLayout();
            this._sendBtnContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _terminalWin
            // 
            this._terminalWin.ContextMenuStrip = this._terminalContextMenu;
            this._terminalWin.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this._terminalWin.Location = new System.Drawing.Point(114, 48);
            this._terminalWin.MaxLength = 100;
            this._terminalWin.Multiline = true;
            this._terminalWin.Name = "_terminalWin";
            this._terminalWin.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._terminalWin.Size = new System.Drawing.Size(578, 484);
            this._terminalWin.TabIndex = 1;
            // 
            // _terminalContextMenu
            // 
            this._terminalContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._clearTerminalContextMenuItem,
            this.toolStripSeparator2,
            this.toolStripMenuItem1,
            this._saveTerminalContextMenuItem,
            this.toolStripSeparator1,
            this._localEchoTerminalContextMenuItem,
            this._wordWrapTerminalContextMenuItem,
            this._endOfLineCharTerminalContextMenuItem,
            this._showNPCTerminalContextMenuItem,
            this._disableRedrawTerminalContextMenuItem});
            this._terminalContextMenu.Name = "_terminalContextMenu";
            this._terminalContextMenu.Size = new System.Drawing.Size(241, 192);
            // 
            // _clearTerminalContextMenuItem
            // 
            this._clearTerminalContextMenuItem.Name = "_clearTerminalContextMenuItem";
            this._clearTerminalContextMenuItem.Size = new System.Drawing.Size(240, 22);
            this._clearTerminalContextMenuItem.Text = "Clear all";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(237, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(240, 22);
            this.toolStripMenuItem1.Text = "Copy all";
            // 
            // _saveTerminalContextMenuItem
            // 
            this._saveTerminalContextMenuItem.Name = "_saveTerminalContextMenuItem";
            this._saveTerminalContextMenuItem.Size = new System.Drawing.Size(240, 22);
            this._saveTerminalContextMenuItem.Text = "Save to file";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(237, 6);
            // 
            // _localEchoTerminalContextMenuItem
            // 
            this._localEchoTerminalContextMenuItem.Name = "_localEchoTerminalContextMenuItem";
            this._localEchoTerminalContextMenuItem.Size = new System.Drawing.Size(240, 22);
            this._localEchoTerminalContextMenuItem.Text = "Local echo";
            // 
            // _wordWrapTerminalContextMenuItem
            // 
            this._wordWrapTerminalContextMenuItem.Checked = true;
            this._wordWrapTerminalContextMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this._wordWrapTerminalContextMenuItem.Name = "_wordWrapTerminalContextMenuItem";
            this._wordWrapTerminalContextMenuItem.Size = new System.Drawing.Size(240, 22);
            this._wordWrapTerminalContextMenuItem.Text = "Word wrap";
            // 
            // _endOfLineCharTerminalContextMenuItem
            // 
            this._endOfLineCharTerminalContextMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._cRLFTerminalContextMenuItem,
            this._cRTerminalContextMenuItem,
            this._lFTerminalContextMenuItem});
            this._endOfLineCharTerminalContextMenuItem.Name = "_endOfLineCharTerminalContextMenuItem";
            this._endOfLineCharTerminalContextMenuItem.Size = new System.Drawing.Size(240, 22);
            this._endOfLineCharTerminalContextMenuItem.Text = "End of line char";
            // 
            // _cRLFTerminalContextMenuItem
            // 
            this._cRLFTerminalContextMenuItem.Checked = true;
            this._cRLFTerminalContextMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this._cRLFTerminalContextMenuItem.Name = "_cRLFTerminalContextMenuItem";
            this._cRLFTerminalContextMenuItem.Size = new System.Drawing.Size(101, 22);
            this._cRLFTerminalContextMenuItem.Text = "CRLF";
            // 
            // _cRTerminalContextMenuItem
            // 
            this._cRTerminalContextMenuItem.Name = "_cRTerminalContextMenuItem";
            this._cRTerminalContextMenuItem.Size = new System.Drawing.Size(101, 22);
            this._cRTerminalContextMenuItem.Text = "CR";
            // 
            // _lFTerminalContextMenuItem
            // 
            this._lFTerminalContextMenuItem.Name = "_lFTerminalContextMenuItem";
            this._lFTerminalContextMenuItem.Size = new System.Drawing.Size(101, 22);
            this._lFTerminalContextMenuItem.Text = "LF";
            // 
            // _showNPCTerminalContextMenuItem
            // 
            this._showNPCTerminalContextMenuItem.Enabled = false;
            this._showNPCTerminalContextMenuItem.Name = "_showNPCTerminalContextMenuItem";
            this._showNPCTerminalContextMenuItem.Size = new System.Drawing.Size(240, 22);
            this._showNPCTerminalContextMenuItem.Text = "Show Non-Printable Characters";
            // 
            // _disableRedrawTerminalContextMenuItem
            // 
            this._disableRedrawTerminalContextMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RedrawIntervalTextBox});
            this._disableRedrawTerminalContextMenuItem.Name = "_disableRedrawTerminalContextMenuItem";
            this._disableRedrawTerminalContextMenuItem.Size = new System.Drawing.Size(240, 22);
            this._disableRedrawTerminalContextMenuItem.Text = "Redraw interval [ms]";
            // 
            // RedrawIntervalTextBox
            // 
            this.RedrawIntervalTextBox.Name = "RedrawIntervalTextBox";
            this.RedrawIntervalTextBox.Size = new System.Drawing.Size(152, 23);
            this.RedrawIntervalTextBox.Text = "100";
            // 
            // _cmdLine
            // 
            this._cmdLine.ContextMenuStrip = this._cmdLineContextMenu;
            this._cmdLine.FormattingEnabled = true;
            this._cmdLine.Location = new System.Drawing.Point(114, 538);
            this._cmdLine.Name = "_cmdLine";
            this._cmdLine.Size = new System.Drawing.Size(444, 21);
            this._cmdLine.TabIndex = 5;
            // 
            // _cmdLineContextMenu
            // 
            this._cmdLineContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._selectAllCmdLineContextMenuItem,
            this._clearAllCmdLineContextMenuItem,
            this.toolStripSeparator5,
            this._copyCmdLineContextMenuItem,
            this._cutCmdLineContextMenuItem,
            this._pasteCmdLineContextMenuItem,
            this.toolStripSeparator4,
            this._sendOnceCmdLineContextMenuItem,
            this._sendPeriodicallyCmdLineContextMenuItem,
            this.toolStripSeparator8,
            this._replaceHexTagsCmdLineContextMenuItem});
            this._cmdLineContextMenu.Name = "_cmdLineContextMenu";
            this._cmdLineContextMenu.Size = new System.Drawing.Size(165, 198);
            // 
            // _selectAllCmdLineContextMenuItem
            // 
            this._selectAllCmdLineContextMenuItem.Name = "_selectAllCmdLineContextMenuItem";
            this._selectAllCmdLineContextMenuItem.Size = new System.Drawing.Size(164, 22);
            this._selectAllCmdLineContextMenuItem.Text = "Select all";
            // 
            // _clearAllCmdLineContextMenuItem
            // 
            this._clearAllCmdLineContextMenuItem.Name = "_clearAllCmdLineContextMenuItem";
            this._clearAllCmdLineContextMenuItem.Size = new System.Drawing.Size(164, 22);
            this._clearAllCmdLineContextMenuItem.Text = "Clear all";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(161, 6);
            // 
            // _copyCmdLineContextMenuItem
            // 
            this._copyCmdLineContextMenuItem.Name = "_copyCmdLineContextMenuItem";
            this._copyCmdLineContextMenuItem.Size = new System.Drawing.Size(164, 22);
            this._copyCmdLineContextMenuItem.Text = "Copy";
            // 
            // _cutCmdLineContextMenuItem
            // 
            this._cutCmdLineContextMenuItem.Name = "_cutCmdLineContextMenuItem";
            this._cutCmdLineContextMenuItem.Size = new System.Drawing.Size(164, 22);
            this._cutCmdLineContextMenuItem.Text = "Cut";
            // 
            // _pasteCmdLineContextMenuItem
            // 
            this._pasteCmdLineContextMenuItem.Name = "_pasteCmdLineContextMenuItem";
            this._pasteCmdLineContextMenuItem.Size = new System.Drawing.Size(164, 22);
            this._pasteCmdLineContextMenuItem.Text = "Paste";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(161, 6);
            // 
            // _sendOnceCmdLineContextMenuItem
            // 
            this._sendOnceCmdLineContextMenuItem.Name = "_sendOnceCmdLineContextMenuItem";
            this._sendOnceCmdLineContextMenuItem.Size = new System.Drawing.Size(164, 22);
            this._sendOnceCmdLineContextMenuItem.Text = "Send once";
            // 
            // _sendPeriodicallyCmdLineContextMenuItem
            // 
            this._sendPeriodicallyCmdLineContextMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._periodTerminalContextMenuItem});
            this._sendPeriodicallyCmdLineContextMenuItem.Name = "_sendPeriodicallyCmdLineContextMenuItem";
            this._sendPeriodicallyCmdLineContextMenuItem.Size = new System.Drawing.Size(164, 22);
            this._sendPeriodicallyCmdLineContextMenuItem.Text = "Send periodically";
            // 
            // _periodTerminalContextMenuItem
            // 
            this._periodTerminalContextMenuItem.Name = "_periodTerminalContextMenuItem";
            this._periodTerminalContextMenuItem.Size = new System.Drawing.Size(100, 23);
            this._periodTerminalContextMenuItem.Text = "1000";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(161, 6);
            // 
            // _replaceHexTagsCmdLineContextMenuItem
            // 
            this._replaceHexTagsCmdLineContextMenuItem.Name = "_replaceHexTagsCmdLineContextMenuItem";
            this._replaceHexTagsCmdLineContextMenuItem.Size = new System.Drawing.Size(164, 22);
            this._replaceHexTagsCmdLineContextMenuItem.Text = "Replace hex tags";
            // 
            // _sendBtn
            // 
            this._sendBtn.Location = new System.Drawing.Point(564, 538);
            this._sendBtn.Name = "_sendBtn";
            this._sendBtn.Size = new System.Drawing.Size(128, 21);
            this._sendBtn.TabIndex = 6;
            this._sendBtn.TabStop = false;
            this._sendBtn.Text = "Send";
            this._sendBtn.UseVisualStyleBackColor = true;
            // 
            // _sendBtnContextMenu
            // 
            this._sendBtnContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._sendOnceSendBtnContextMenuItem,
            this._sendPeriodicallySendBtnContextMenuItem});
            this._sendBtnContextMenu.Name = "_sendBtnContextMenu";
            this._sendBtnContextMenu.Size = new System.Drawing.Size(165, 48);
            // 
            // _sendOnceSendBtnContextMenuItem
            // 
            this._sendOnceSendBtnContextMenuItem.Name = "_sendOnceSendBtnContextMenuItem";
            this._sendOnceSendBtnContextMenuItem.Size = new System.Drawing.Size(164, 22);
            this._sendOnceSendBtnContextMenuItem.Text = "Send once";
            // 
            // _sendPeriodicallySendBtnContextMenuItem
            // 
            this._sendPeriodicallySendBtnContextMenuItem.Name = "_sendPeriodicallySendBtnContextMenuItem";
            this._sendPeriodicallySendBtnContextMenuItem.Size = new System.Drawing.Size(164, 22);
            this._sendPeriodicallySendBtnContextMenuItem.Text = "Send periodically";
            // 
            // Terminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._cmdLine);
            this.Controls.Add(this._sendBtn);
            this.Controls.Add(this._terminalWin);
            this.Name = "Terminal";
            this.Size = new System.Drawing.Size(887, 596);
            this._terminalContextMenu.ResumeLayout(false);
            this._cmdLineContextMenu.ResumeLayout(false);
            this._sendBtnContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _terminalWin;
        private System.Windows.Forms.ComboBox _cmdLine;
        private System.Windows.Forms.Button _sendBtn;
        private System.Windows.Forms.ContextMenuStrip _terminalContextMenu;
        private System.Windows.Forms.ToolStripMenuItem _clearTerminalContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _saveTerminalContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _disableRedrawTerminalContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _localEchoTerminalContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _wordWrapTerminalContextMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip _cmdLineContextMenu;
        private System.Windows.Forms.ToolStripMenuItem _copyCmdLineContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _cutCmdLineContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _pasteCmdLineContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _selectAllCmdLineContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _sendOnceCmdLineContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _sendPeriodicallyCmdLineContextMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ContextMenuStrip _sendBtnContextMenu;
        private System.Windows.Forms.ToolStripMenuItem _sendOnceSendBtnContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _sendPeriodicallySendBtnContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _endOfLineCharTerminalContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _cRLFTerminalContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _cRTerminalContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _lFTerminalContextMenuItem;
        private System.Windows.Forms.ToolStripTextBox _periodTerminalContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _clearAllCmdLineContextMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem _showNPCTerminalContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _replaceHexTagsCmdLineContextMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripTextBox RedrawIntervalTextBox;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}
