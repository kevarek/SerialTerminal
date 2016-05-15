using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Timers;
using System.Diagnostics;

namespace SerialTerminal
{

    public partial class Terminal : UserControl
    {

        public enum EOLMarks
        {
            [Description("\r\n")]
            crlf,
            [Description("\r")]
            cr,
            [Description("\n")]
            lf
        }


        
        public delegate void NewDataEvH(string text);
        public event NewDataEvH NewData;
        private delegate void WriteDelegate(string data, bool atNewLine);

        private Control LastSelectedForm;
        private Logger LogFile;

        private EOLMarks _eOLMark;
        private string _eOLMarkConverted;
        private System.Timers.Timer PeriodTimer = new System.Timers.Timer();
        private string CmdToSendPeriodically;
        private bool _TerminalWordWrap = true;
        private int _RedrawIntervalMs = 100;
        private bool _ShowNPC;
        private bool _Enabled = false;
        private int _SendBtnWidth = 128;
        private int _Spacing = 4;
        private bool _LocalEchoEnabled = false;
        private bool _HexTagsReplacingEnabled;

        private List<string> CmdHistory = new List<string>(32);

        private System.Windows.Forms.Timer RefreshTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer RemoveOldDataTimer = new System.Windows.Forms.Timer();
        private StringBuilder OldDataBuffer = new StringBuilder();

        /// <summary>
        /// Saves string into command history
        /// </summary>
        /// <param name="cmd"></param>
        private void SaveToCmdHistory(string cmd)
        {
            CmdHistory.Add(cmd);
            LoadCmdHistoryIntoListbox(_cmdLine);

        }


        /// <summary>
        /// Loads history of commands into specified combobox. Commands will be sorted from newest to oldest
        /// </summary>
        /// <param name="lb"></param>
        private void LoadCmdHistoryIntoListbox(ComboBox lb)
        {
            lb.Items.Clear();

            lb.Items.Add("");
            for (int i = (CmdHistory.Count - 1); i >= 0; i--)
            {
                lb.Items.Add(CmdHistory[i]);
            }
            lb.SelectedIndex = 0;
        }



        private bool _SendPeriodically = false;
        #region Constructor
        public Terminal()
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();

            LogFile = new Logger();

            //Control is disabled by default
            this.Enabled = false;
            _terminalWin.MaxLength = 1000;

            #region EventHandlers



     /*       
            RefreshTimer.Tick += (s, e) =>
            {
                lock (DataBuffer)
                {
                    if(DataBuffer.Length>0)
                    {
                        _terminalWin.AppendText(DataBuffer.ToString());
                        DataBuffer.Clear();
                    }
                }
            };
    */
            int PrevDataBufferLength = 0;

            RefreshTimer.Tick += (s, e) =>
            {
                lock (DataBuffer)
                {
                    //New data received since last time
                    if (DataBuffer.Length > PrevDataBufferLength)
                    {
                        

                        try
                        {
             
                            _terminalWin.AppendText(DataBuffer.ToString(PrevDataBufferLength, DataBuffer.Length - PrevDataBufferLength));

                        }
                        catch
                        {
                            _terminalWin.Text = "";
                            _terminalWin.AppendText(DataBuffer.ToString());
                        }

                        PrevDataBufferLength = DataBuffer.Length;

                        //_terminalWin.SelectionStart = _terminalWin.TextLength;
                        //_terminalWin.ScrollToCaret();
                    }
                }
            };



            RemoveOldDataTimer.Interval = 1000;
            //RemoveOldDataTimer.Start();

            RemoveOldDataTimer.Tick += (s, e) =>
            {
                lock (OldDataBuffer)
                {
                    if (_terminalWin.TextLength > 10000)
                    {
                        OldDataBuffer.Append(_terminalWin.Text.Remove(5000));
                        _terminalWin.Text = _terminalWin.Text.Remove(0, 4999);
                    }
                }
            };
            //Fires an event in specified interval with data to be send periodically
            PeriodTimer.Elapsed += (s, e) =>
            {
                if (_SendPeriodically)
                {
                    FireNewDataEvent(CmdToSendPeriodically + _eOLMarkConverted);
                }
            };


            //Terminal windows keypress event - fires new data event and handles local echo if enabled
            _terminalWin.KeyPress += (s, e) =>
            {
                //Get pressed key from event args
                string PressedKey = e.KeyChar.ToString();

                //convert enter key to specified end of line mark (crlf for instance)
                if (PressedKey.Equals("\r"))
                {
                    PressedKey = _eOLMarkConverted;
                }
                //Escape press is always ignored as it is used as hotkey for closing serial port
                else if (e.KeyChar == (char)Keys.Escape)
                {
                    return;
                }

 
                //Fire new data event with pressed key and if local echo is enabled also write that char into terminal.
                if (Enabled)
                {
                    FireNewDataEvent(PressedKey);

                    if (LocalEchoEnabled)
                    {
                        Write(PressedKey, false);
                    }
                }
                e.Handled = true;
                
            };


            //Terminal window keybindings
            _terminalWin.KeyDown += (s, e) =>
            {
                if (e.Control)
                {
                    //ctrl+c
                    if (e.KeyCode == Keys.C)
                    {
                        if (_terminalWin.SelectedText.Length != 0)
                        {
                            Clipboard.SetText(_terminalWin.SelectedText);
                        }

                    }

                    //ctrl+a
                    if (e.KeyCode == Keys.A)
                    {
                        _terminalWin.SelectAll();
                    }

                    //ctrl+x
                    if (e.KeyCode == Keys.X)
                    {
                        if (_terminalWin.SelectedText.Length != 0)
                        {
                            Clipboard.SetText(_terminalWin.SelectedText);
                            _terminalWin.SelectedText = "";
                        }
                    }
                    e.SuppressKeyPress = true;
                }
            };


            //command from commandline is always sent after pressing enter
            _cmdLine.KeyPress += (s, e) =>
            {
                if ((Keys)e.KeyChar == Keys.Enter)
                {
                    SendOnce();
                }
            };


            _cmdLine.DropDown += (s, e) =>
            {
                LoadCmdHistoryIntoListbox(_cmdLine);
            };

            //If command line gets focus it is saved as target for next terminal focus.
            _cmdLine.GotFocus += (s, e) =>
            {
                LastSelectedForm = _cmdLine;
            };


            //If terminal gets focus it is saved as target for next terminal focus.
            _terminalWin.GotFocus += (s, e) =>
            {
                LastSelectedForm = _terminalWin;
            };





            _clearTerminalContextMenuItem.Click += (s, e) =>
            {
                _terminalWin.Clear();
            };


            _saveTerminalContextMenuItem.Click += (s, e) =>
            {

                Logger TerminalSaver = new Logger();

                try
                {
                    TerminalSaver.Open(Logger.OpenMode.Ask, null);
                    TerminalSaver.Write(_terminalWin.Text);
                    TerminalSaver.Close();
                }
                catch(Exception x)
                {
                    Write(x.Message + Environment.NewLine, true);
                }
            };


            _localEchoTerminalContextMenuItem.Click += (s, e) =>
            {
                LocalEchoEnabled = !LocalEchoEnabled;
            };

            _wordWrapTerminalContextMenuItem.Click += (s, e) =>
            {
                TerminalWordWrap = !TerminalWordWrap;
            };


            _cRLFTerminalContextMenuItem.Click += (s, e) =>
            {
                EOLMark = EOLMarks.crlf;
            };


            _cRTerminalContextMenuItem.Click += (s, e) =>
            {
                EOLMark = EOLMarks.cr;
            };


            _lFTerminalContextMenuItem.Click += (s, e) =>
            {
                EOLMark = EOLMarks.lf;
            };


            _showNPCTerminalContextMenuItem.Click += (s, e) =>
            {
                ShowNPC = !ShowNPC;
            };







           RedrawIntervalTextBox.LostFocus += (s, e) =>
            {
                try
                {
                    RedrawIntervalMs = Convert.ToInt32(RedrawIntervalTextBox.Text);
                }
                catch
                {
                    RedrawIntervalTextBox.Text = RedrawIntervalMs.ToString();
                    Write("Redraw interval [ms] must be integer.\r\n", true);
                }
            };

           RedrawIntervalTextBox.KeyDown += (s, e) =>
           {
               if (e.KeyCode == Keys.Enter)
               {
                   try
                   {
                       RedrawIntervalMs = Convert.ToInt32(RedrawIntervalTextBox.Text);
                       _terminalContextMenu.Close();
                   }
                   catch
                   {
                       Write("Redraw interval [ms] must be integer.\r\n", true);
                   }

               }
           };













            _selectAllCmdLineContextMenuItem.Click += (s, e) =>
            {
                _cmdLine.Focus();
                _cmdLine.SelectAll();
            };

            _clearAllCmdLineContextMenuItem.Click += (s, e) =>
            {
                _cmdLine.Text = "";
            };

            _copyCmdLineContextMenuItem.Click += (s, e) =>
            {
                try
                {
                    Clipboard.SetText(_cmdLine.SelectedText);
                }
                catch
                {
                }
            };

            _cutCmdLineContextMenuItem.Click += (s, e) =>
            {
                try
                {
                    Clipboard.SetText(_cmdLine.SelectedText);
                    _cmdLine.SelectedText = "";
                }
                catch
                {
                }
            };

            _pasteCmdLineContextMenuItem.Click += (s, e) =>
            {
                _cmdLine.SelectedText = Clipboard.GetText();
            };



            _sendOnceCmdLineContextMenuItem.Click += (s, e) =>
            {
                SendOnce();
            };

            _sendOnceSendBtnContextMenuItem.Click += (s, e) =>
            {
                SendOnce();
            };



            _sendPeriodicallySendBtnContextMenuItem.Click += (s, e) =>
            {
                _sendPeriodicallySendBtnContextMenuItem.Checked = !_sendPeriodicallySendBtnContextMenuItem.Checked;

                if (_sendPeriodicallySendBtnContextMenuItem.Checked)
                {
                    SendPeriodically();
                }
                else
                {
                    StopPeriodicAutosending();
                }
            };


            _sendPeriodicallyCmdLineContextMenuItem.Click += (s, e) =>
            {
                _sendPeriodicallySendBtnContextMenuItem.Checked = !_sendPeriodicallySendBtnContextMenuItem.Checked;

                if (_sendPeriodicallySendBtnContextMenuItem.Checked)
                {
                    SendPeriodically();
                }
                else
                {
                    StopPeriodicAutosending();
                }
            };










            _replaceHexTagsCmdLineContextMenuItem.Click += (s, e) =>
            {
                HexTagsReplacingEnabled = !HexTagsReplacingEnabled;
            };





            #endregion EventHandlers
        }
        #endregion Constructor


        #region RedrawFunction




        protected override void OnPaint(PaintEventArgs e)
        {
            Redraw();
            base.OnPaint(e);
        }


        protected override void OnLoad(EventArgs e)
        {
            Redraw();
            base.OnLoad(e);
        }


        protected override void OnResize(EventArgs e)
        {
            Redraw();
            base.OnResize(e);
        }

        /// <summary>
        /// Refraw function for this user controll.
        /// </summary>
        private void Redraw()
        {

            _terminalWin.Top = 0;
            _terminalWin.Left = 0;
            _terminalWin.Width = ClientRectangle.Width;
            _terminalWin.Height = ClientRectangle.Height - _sendBtn.Height - Spacing;

            _cmdLine.Top = _terminalWin.Bottom + Spacing;
            _cmdLine.Left = 0;
            _cmdLine.Width = ClientRectangle.Width - SendBtnWidth - Spacing;
            _cmdLine.Height = 21;


            _sendBtn.Top = _terminalWin.Bottom + Spacing - 2;
            _sendBtn.Left = _cmdLine.Right + Spacing;
            _sendBtn.Width = SendBtnWidth;
            _sendBtn.Height = 26;
            
        }
        #endregion RedrawFunction



        public new void Focus()
        {
            if (LastSelectedForm != null)
            {
                LastSelectedForm.Focus();
            }
            else
            {
                _cmdLine.Focus();
            }
            
        }



        [Category("Customization"), Description("Specifies end of line character for communication.")]
        public EOLMarks EOLMark
        {
            get
            {
                return _eOLMark;
            }
            set
            {
                _eOLMark = value;
                _eOLMarkConverted = Utilities.GetDescription(value);

                _cRLFTerminalContextMenuItem.Checked = false;
                _cRTerminalContextMenuItem.Checked = false;
                _lFTerminalContextMenuItem.Checked = false;

                if (value == EOLMarks.crlf)
                {
                    _cRLFTerminalContextMenuItem.Checked = true;
                }
                else if (value == EOLMarks.cr)
                {
                    _cRTerminalContextMenuItem.Checked = true;
                }
                else if (value == EOLMarks.lf)
                {
                    _lFTerminalContextMenuItem.Checked = true;
                }
            }
        }


        [Category("Customization"), Description("Enables or disables word wrap for terminal.")]
        public bool TerminalWordWrap
        {
            get
            {
                return _TerminalWordWrap;
            }
            set
            {
                _wordWrapTerminalContextMenuItem.Checked = value;
                _terminalWin.WordWrap = value;
                _TerminalWordWrap = value;
            }
        }


 


        [Category("Customization"), Description("Enables or disables control.")]
        public new bool Enabled
        {
            get
            {
                return _Enabled;
            }
            set
            {
                _Enabled = value;
                if (value)
                {
                    _cmdLine.BackColor = Color.White;
                    _terminalWin.BackColor = Color.White;
                    //_cmdLine.Enabled = true;
                    _sendBtn.Enabled = true;

                    _sendOnceCmdLineContextMenuItem.Enabled = true;
                    _sendPeriodicallyCmdLineContextMenuItem.Enabled = true;

                    _sendOnceSendBtnContextMenuItem.Enabled = true;
                    _sendPeriodicallySendBtnContextMenuItem.Enabled = true;
                }
                else
                {
                    _terminalWin.BackColor = SystemColors.Control;
                    _cmdLine.BackColor = SystemColors.Control;
                    //_cmdLine.Enabled = false;
                   _sendBtn.Enabled = false;

                   _sendOnceCmdLineContextMenuItem.Enabled = false;
                   _sendPeriodicallyCmdLineContextMenuItem.Enabled = false;

                   _sendOnceSendBtnContextMenuItem.Enabled = false;
                   _sendPeriodicallySendBtnContextMenuItem.Enabled = false;
                }
            }
        }


        [Category("Customization"), Description("Send buttons width.")]
        public int SendBtnWidth
        {
            get
            {
                return _SendBtnWidth;
            }
            set
            {
                _SendBtnWidth = value;
                Redraw();
            }
        }


        [Category("Customization"), Description("Console redraw interval [ms].")]
        public int RedrawIntervalMs
        {
            get
            {
                return _RedrawIntervalMs;
            }
            set
            {
                _RedrawIntervalMs = value;
                RedrawIntervalTextBox.Text = value.ToString();
                RefreshTimer.Interval = value;
                RefreshTimer.Start();
            }
        }


        [Category("Customization"), Description("Spacing of controls elements.")]
        public int Spacing
        {
            get
            {
                return _Spacing;
            }
            set
            {
                _Spacing = value;
                Redraw();
            }
        }


        [Category("Customization"), Description("Specifies whether outgoing data should be written into terminal window or not.")]
        public bool LocalEchoEnabled
        {
            get
            {
                return _LocalEchoEnabled;
            }
            set
            {
                _LocalEchoEnabled = value;
                _localEchoTerminalContextMenuItem.Checked = value;
            }
        }

        private void ClearTerminalWin()
        {
            _terminalWin.Text = "";
        }



        [Category("Customization"), Description("Specifies whether outgoing data from commandline should be parsed and hex tags <0x1B> replaced by char of its value.")]
        public bool HexTagsReplacingEnabled
        {
            get
            {
                return _HexTagsReplacingEnabled;
            }
            set
            {
                _HexTagsReplacingEnabled = value;
                _replaceHexTagsCmdLineContextMenuItem.Checked = value;
            }

        }


        [Category("Customization"), Description("Enables or disables showing of non printable characters.")]
        public bool ShowNPC
        {
            get
            {
                return _ShowNPC;
            }
            set
            {
                _ShowNPC = value;
                _showNPCTerminalContextMenuItem.Checked = value;
            }
        }



        private StringBuilder DataBuffer = new StringBuilder(1000000);
        /// <summary>
        /// Writes data into terminal with all terminal functionality like parsing etc
        /// </summary>
        /// <param name="text"></param>
        /// <param name="atNewLine"></param>
        public void Write(string text, bool atNewLine)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }



            lock (DataBuffer)
            {
                if (atNewLine)
                {
                    if (_terminalWin.Text.Length != 0 && _terminalWin.Text[_terminalWin.Text.Length - 1] != '\n')
                    {
                        DataBuffer.Append("\r\n");
                    }
                }
                DataBuffer.Append(text);
            }
        }


        public void WriteAsync(string data, bool atNewLine)
        {
            this.BeginInvoke(new WriteDelegate(Write), new object[] { data, atNewLine });
        }


        /// <summary>
        /// Fires new data event which handles outoing data from terminal (raw data or command mode with end of line appending)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="appendEOL"></param>
        public void FireNewDataEvent(string data)
        {
            if (NewData != null)
            {
                NewData(data);
            }
        }






        private void SendOnce()
        {

            string Command;

            if (!Enabled) return;

            if (HexTagsReplacingEnabled)
            {
                Command = Utilities.ReplaceHexTagsByItsValue(_cmdLine.Text);
            }
            else
            {
                Command = _cmdLine.Text;
            }

            Command += _eOLMarkConverted;

            if (LocalEchoEnabled)
            {
                Write(Command, false);
            }

            FireNewDataEvent(Command);
            SaveToCmdHistory(_cmdLine.Text);
            _cmdLine.Text = "";
        }


        /// <summary>
        /// Starts periodic autosending of specified command in specified interval
        /// </summary>
        private void SendPeriodically()
        {
            //If there is no command to autosend
            if (_cmdLine.Text.Length == 0)
            {
                Write("Nothing to autosend!", true);
                return;
            }


            //If there is something to autosend try to convert autosending interval and enable interval timer
            try
            {
                _SendPeriodically = true;
                _sendPeriodicallyCmdLineContextMenuItem.Checked = true;
                _sendPeriodicallySendBtnContextMenuItem.Checked = true;
                PeriodTimer.Interval = Convert.ToInt32(_periodTerminalContextMenuItem.Text);
                CmdToSendPeriodically = _cmdLine.Text;
                SaveToCmdHistory(_cmdLine.Text);
                _cmdLine.Text = "";
                PeriodTimer.Enabled = true;
            }
            //If interval is not a number stop timer and clear all checked buttons
            catch
            {
                _SendPeriodically = false;
                _sendPeriodicallyCmdLineContextMenuItem.Checked = false;
                _sendPeriodicallySendBtnContextMenuItem.Checked = false;
                PeriodTimer.Enabled = false;
                Write("Interval must be an integer!", true);
            }
        }


        public void StopPeriodicAutosending()
        {
            _SendPeriodically = false;
            _sendPeriodicallyCmdLineContextMenuItem.Checked = false;
            _sendPeriodicallySendBtnContextMenuItem.Checked = false;
            PeriodTimer.Enabled = false;
        }
    }
}
