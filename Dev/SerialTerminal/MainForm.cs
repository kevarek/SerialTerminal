using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SerialTerminal
{
    public partial class _mainForm : Form
    {
        const int Spacing = 5;
        const int LeftColumnWidth = 150;

        public _mainForm()
        {
            InitializeComponent();

            Load += (s, e) =>
            {


                _terminal.EOLMark = (Terminal.EOLMarks)Properties.Settings.Default.EOLMark;
                _terminal.RedrawIntervalMs = Properties.Settings.Default.RedrawIntervalMs;
                _terminal.ShowNPC = Properties.Settings.Default.ShowNPCEnabled;
                _terminal.HexTagsReplacingEnabled = Properties.Settings.Default.ReplaceHexTagsEnabled;
                _terminal.TerminalWordWrap = Properties.Settings.Default.TerminalWordWrap;
                _terminal.LocalEchoEnabled = Properties.Settings.Default.LocalEcho;

                //Set default traffic chart axis range
                try
                {
                    _trafficCounter.ChartYMax = Convert.ToDouble(_smartSerialPort.BaudRate);
                }
                catch
                {
                    _trafficCounter.ChartYMax = 99;
                }
                this.MinimumSize = new Size(4 * _smartSerialPort.Width, _smartSerialPort.Height + _trafficCounter.Height + 3 * Spacing + (this.Height - this.ClientRectangle.Height));

            };

            FormClosed += (s, e) =>
            {


                Properties.Settings.Default.EOLMark = (int)_terminal.EOLMark;
                Properties.Settings.Default.RedrawIntervalMs = _terminal.RedrawIntervalMs;
                Properties.Settings.Default.ShowNPCEnabled = _terminal.ShowNPC;
                Properties.Settings.Default.ReplaceHexTagsEnabled = _terminal.HexTagsReplacingEnabled;
                Properties.Settings.Default.TerminalWordWrap = _terminal.TerminalWordWrap;
                Properties.Settings.Default.LocalEcho = _terminal.LocalEchoEnabled;

                Properties.Settings.Default.Save();
            };



            KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape && _smartSerialPort.IsOpen == true ) 
                {
                    _smartSerialPort.OpenClose();
                }

            };


            //serial port status has been changed (openned, closed, error etc)
            _smartSerialPort.PortStatusUpdated += (s, e) =>
            {
                _terminal.Write(e.Message, true);

                if (e.PortStatus == SmartSerialPort.SerialPortStatus.Openned || e.PortStatus == SmartSerialPort.SerialPortStatus.ClosingError)
                {
                    _terminal.Enabled = true;
                    _terminal.Focus();
                    try
                    {
                        _trafficCounter.ChartYMax = Convert.ToDouble(_smartSerialPort.BaudRate);
                    }
                    catch
                    {

                    }
                }
                else if (e.PortStatus == SmartSerialPort.SerialPortStatus.DataSendingError)
                {
                    //just nothing maybe throw a warning in future
                }
                else
                {
                    _terminal.Enabled = false;
                    //_smartSerialPort.Close();
                    _terminal.StopPeriodicAutosending();
                }
            };

            //outgoing data from terminal are sent by serial port
            _terminal.NewData += (data) =>
            {
                _smartSerialPort.Send(data);
                _trafficCounter.IncrementUploadCounter(data.Length);
            };

            //Incoming data from serial port are redirected to terminal window
            _smartSerialPort.DataReceived += (s, data) =>
            {
                _terminal.WriteAsync(data, false);
                _trafficCounter.IncrementDownloadCounter(data.Length);
            };
        }

        #region RedrawFunction

        /// <summary>
        /// Redraw function of mainform
        /// </summary>
        private void Redraw()
        {
            //Place smart serial port into top left corner
            _smartSerialPort.Top = Spacing;
            _smartSerialPort.Left = Spacing;
            _smartSerialPort.Width = LeftColumnWidth;

            _terminal.Top = _smartSerialPort.Top + 6;
            _terminal.Left = _smartSerialPort.Right + Spacing;

            //Place terminal window in top right corner

            _terminal.Width = ClientRectangle.Width - _terminal.Left - Spacing;
            _terminal.Height = ClientRectangle.Height - _terminal.Top - Spacing;

            //Place traffic counter window to bottom left corner
            _trafficCounter.Top = ClientRectangle.Height - _trafficCounter.Height - Spacing;
            _trafficCounter.Left = _smartSerialPort.Left;
            _trafficCounter.Width = _smartSerialPort.Width;
        }


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

        #endregion RedrawFunction

    }
}
