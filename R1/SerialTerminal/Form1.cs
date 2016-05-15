using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;


namespace SerialTerminal
{
    public partial class _mainForm : Form
    {

        //oh how I love classic c #define directive :)
        const int BAUDRATEUPDATEINTERVAL = 1000;
        const int CHARTXAXISRANGE = 60;
        const int DEFAULTFORMSPACE = 5;




        private delegate void StringDelegate(string text);






        private StreamWriter LogFile;


        public _mainForm()
        {
            this.Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
        }










/*
 * 
 *      ---------------MAINFORM EVENTS AND REDRAW-------------------------------
 * 
 * */



        private void _mainForm_Load(object sender, EventArgs e)
        {
            LoadSettings();     //load stored settings
            RedrawForm();       //place controls over form

            //load available portnames and baudrates
            LoadAvailablePorts(_portName);
            LoadAvailableBaudRates(_portRate);

            //register events for traffis counting
            UploadCounter.ValueUpdated += new DataTraficCounter.ValueUpdatedEventHandler(Upload_ValueUpdated);
            DownloadCounter.ValueUpdated += new DataTraficCounter.ValueUpdatedEventHandler(Download_ValueUpdated);

            //initialize bandwidth graph
            _bandwidthGraph.ChartAreas[0].AxisY.IsLabelAutoFit = false;
            _bandwidthGraph.ChartAreas[0].AxisY.LabelStyle.Format = "{0.0}";
            _bandwidthGraph.Series[0].Points.Add(new System.Windows.Forms.DataVisualization.Charting.DataPoint(0, 0));
            _bandwidthGraph.ChartAreas[0].AxisX.Maximum = CHARTXAXISRANGE;

            //register event of backgroundworker SerialWriter for async serial port writing - no GUI freezing that way
            SerialWriter.DoWork += SerialWriterDoWorkHandler;
            SerialWriter.RunWorkerCompleted += SerialWriterCompletedHandler;

            //register event of backgroundworker SerialPortOpenner to asynchronously open or close serial port - no GUI freezing
            SerialPortOpenner.DoWork += SerialPortOpennerDoWorkHandler;

            //register event when SerialPortOpenner completes
            SerialPortOpenner.RunWorkerCompleted += SerialPortOpennerCompletedHandler;

            //register event for sending commands in specified interval
            AutoSendingTimer.Tick += new EventHandler(AutoSendingTimer_Tick);

            SpecialCharReceived += ReplyToReceivedSpecialChar;

            //try to accomodate bandwidth graphs Y axis range, if portrate is not a number do nothing
            try
            {
                _bandwidthGraph.ChartAreas[0].AxisY.Maximum = 1.2 * Convert.ToDouble(_portRate.Text) / (8 * 1000);
            }
            catch
            {
            }
        }



        private void _mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //save form settings
            SaveSettings();

            //flush and close log file
            try
            {
                LogFile.Flush();
                LogFile.Close();
            }
            catch
            {

            }
        }



        private void _mainForm_Resize(object sender, EventArgs e)
        {
            RedrawForm();
        }



        private void RedrawForm()
        {
            Rectangle WinRect = this.ClientRectangle;

            int WinWidth = WinRect.Width;
            int WinHeight = WinRect.Height;
            int TitleBarHeight = this.Height - WinHeight;

            int DefaultSpace = DEFAULTFORMSPACE;

            Size WinMinSize = new Size(500, (TitleBarHeight + 5 * DefaultSpace + _portSettingsGrp.Height + _bandwidthGrp.Height + _transferedGrp.Height + _bandwidthGraph.Height));
            this.MinimumSize = WinMinSize;


            //place port settings groupbox at top left
            _portSettingsGrp.Top = DefaultSpace;
            _portSettingsGrp.Left = DefaultSpace;


            //place graph at bottom left
            _bandwidthGraph.Top = WinHeight - (DefaultSpace + _bandwidthGraph.Height);
            _bandwidthGraph.Left = _portSettingsGrp.Left;

            //and place statistics above graph
            _bandwidthGrp.Top = WinHeight - (DefaultSpace + _bandwidthGraph.Height + DefaultSpace + _transferedGrp.Height);
            _bandwidthGrp.Left = _portSettingsGrp.Left;
            _transferedGrp.Top = WinHeight - (DefaultSpace + _bandwidthGraph.Height + DefaultSpace + _transferedGrp.Height + DefaultSpace + _bandwidthGrp.Height);
            _transferedGrp.Left = _portSettingsGrp.Left;



            //draw console textbox
            _consoleBox.Top = DefaultSpace;
            _consoleBox.Left = 2 * DefaultSpace + _portSettingsGrp.Width;
            _consoleBox.Width = WinWidth - (_consoleBox.Left + DefaultSpace);
            _consoleBox.Height = WinHeight - (3 * DefaultSpace + _cmdBox.Height);

            //draw cmd box and send button
            _cmdBox.Top = 2 * DefaultSpace + _consoleBox.Height;
            _cmdBox.Left = _consoleBox.Left;
            _cmdBox.Width = WinWidth - (4 * DefaultSpace + _bandwidthGraph.Width + _sendBtn.Width);
            _sendBtn.Top = _cmdBox.Top;
            _sendBtn.Left = _cmdBox.Left + _cmdBox.Width + DefaultSpace;

        }










/*
* 
*     ------------------------SETTINGS------------------------------------------------------------
* 
* */



        private string EOLConsole;
        private string EOLCommand;


        private enum SendingMode
        {
            Once, Interval, SpecialChar
        };


        private enum EOL
        {
            None, CRLF, CR, LF
        };

        /// <summary>
        /// Saves users form settings
        /// </summary>
        private void SaveSettings()
        {
            Properties.Settings.Default.PortName = _portName.Text;
            Properties.Settings.Default.BaudRate = _portRate.Text;
            Properties.Settings.Default.AutoSendingInterval = toolStripMenuItem2.Text;
            Properties.Settings.Default.AutoSendingSpecialChar = toolStripMenuItem3.Text;


            Properties.Settings.Default.Save();
        }


        /// <summary>
        /// Loads users form settings
        /// </summary>
        private void LoadSettings()
        {
            _portName.Text = Properties.Settings.Default.PortName;
            _portRate.Text = Properties.Settings.Default.BaudRate;

            toolStripMenuItem2.Text = Properties.Settings.Default.AutoSendingInterval;
            toolStripMenuItem3.Text = Properties.Settings.Default.AutoSendingSpecialChar;

            wordWrapToolStripMenuItem.Checked = Properties.Settings.Default.IsWordWrapEnabled;
            localEchoToolStripMenuItem.Checked = Properties.Settings.Default.IsLocalEchoEnabled;
            nonPrintableCharsToolStripMenuItem.Checked = Properties.Settings.Default.IsShowNonPrintableCharsEnabled;


            Properties.Settings.Default.IsLogToFileEnabled = false; //logging is always turned off after startup


            //load console end of line setting
            int EOLCons = Properties.Settings.Default.EOLConsoleSettings;
            if ((EOL)EOLCons == EOL.CRLF)
            {
                cRLFToolStripMenuItem.Checked = true;
                EOLConsole = "\r\n";
            }
            else if ((EOL)EOLCons == EOL.CR)
            {
                cRToolStripMenuItem.Checked = true;
                EOLConsole = "\r";
            }
            else if ((EOL)EOLCons == EOL.LF)
            {
                nToolStripMenuItem.Checked = true;
                EOLConsole = "\n";
            }



            //load command box end of line setting
            int EOLCmd = Properties.Settings.Default.EOLCmdSettings;
            if ((EOL)EOLCmd == EOL.CRLF)
            {
                cRLFToolStripMenuItem1.Checked = true;
                EOLCommand = "\r\n";
            }
            else if ((EOL)EOLCmd == EOL.CR)
            {
                cRToolStripMenuItem1.Checked = true;
                EOLCommand = "\r";
            }
            else if ((EOL)EOLCmd == EOL.LF)
            {
                lFToolStripMenuItem.Checked = true;
                EOLCommand = "\n";
            }
            else if ((EOL)EOLCmd == EOL.None)
            {
                noneToolStripMenuItem.Checked = true;
                EOLConsole = "";
            }

            string[] Tokens = Properties.Settings.Default.LogFilePath.Split('\\');
            string Name = Tokens[Tokens.Length - 1];    //last token contains name and extension
            logtxtToolStripMenuItem.Text = Name;
        }











/*
 * 
 *      -----------------------SERIAL PORT UTILITIES-------------------------------------
 * 
 * */



        /// <summary>
        /// Loads all currently available system serial port names into specified combobox
        /// </summary>
        private void LoadAvailablePorts(ComboBox box)
        {
            string[] AvailablePorts = SerialPort.GetPortNames();

            _portName.Items.Clear();
            foreach (string PortName in AvailablePorts)
            {
                box.Items.Add(PortName);
            }
        }



        /// <summary>
        /// Loads all currently available serial port baudrates into specified combobox
        /// So far hardcoded - engineers lazyness :)
        /// </summary>
        /// <param name="box"></param>
        private void LoadAvailableBaudRates(ComboBox box)
        {
            box.Items.Clear();
            box.Items.Add("1200");
            box.Items.Add("2400");
            box.Items.Add("4800");
            box.Items.Add("9600");
            box.Items.Add("19200");
            box.Items.Add("38400");
            box.Items.Add("57600");
            box.Items.Add("115200");
        }











/*
 * 
 *      ---------------------------SERIAL PORT OPEN WITH BACKGROUND WORKER----------------
 * 
 * */


        //create new instance of serial port
        private SerialPort ComPort = new SerialPort();		    
        
        //create instance of background worker that will handle opening/closing port in non-gui thread
        BackgroundWorker SerialPortOpenner = new BackgroundWorker();

        //create instances of traffic counter for upload and download
        private DataTraficCounter UploadCounter = new DataTraficCounter(BAUDRATEUPDATEINTERVAL);
        private DataTraficCounter DownloadCounter = new DataTraficCounter(BAUDRATEUPDATEINTERVAL);


        private enum SerialPortStatus
        {
            Openned, Closed, ClosingError, OpenningError
        };



        /// <summary>
        /// Opens serial port via non-gui thread, event is raised when operation completes
        /// </summary>
        private void OpenSerialPort()
        {

            if (_portName.Text == "T")
            {

                HandleReceivedSerialData("This is testing received string \r\nHexa code will follow:\r\n");
                string HexaString = "";
                for (int i = 1; i < 255; i++)
                {
                    HexaString += (char)i;
                }
                HandleReceivedSerialData(HexaString);

                return;
            }

            //if serial port is not openned
            if (!ComPort.IsOpen)    
            {
                //set serial port settings
                try
                {
                    ComPort.PortName = _portName.Text;
                }
                catch
                {
                    WriteToConsole("Invalid port name!", true);
                    return;
                }
                try
                {
                    ComPort.BaudRate = Convert.ToInt32(_portRate.Text);
                }
                catch
                {
                    WriteToConsole("Invalid baudrate!", true);
                    return;
                }
                //ComPort.DataBits
                ComPort.Parity = Parity.None;
                ComPort.Handshake = Handshake.None;
                ComPort.StopBits = StopBits.One;
                ComPort.DataReceived += new SerialDataReceivedEventHandler(SerialDataReceivedHandler);
                ComPort.ReadTimeout = 300;
                ComPort.WriteTimeout = 300;
            }

            _openBtn.Text = "---";
            //and open port via nongui thread
            try
            {
                SerialPortOpenner.RunWorkerAsync();
            }
            catch
            {
                WriteToConsole("Cannot open port now!", true);
            }
        }



        /// <summary>
        /// Non-gui thread which tries to open serial port, event is raised when operation completes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialPortOpennerDoWorkHandler(object sender, DoWorkEventArgs e)
        {
           if (ComPort.IsOpen)
            {
                try
                {
                    ComPort.Close();
                    e.Result = (object)SerialPortStatus.Closed;
                    return;
                }
                catch
                {
                    e.Result = (object)SerialPortStatus.ClosingError;
                    return;
                }
            }
            else
            {
                try
                {
                    ComPort.Open();		//open port
                    e.Result = (object)SerialPortStatus.Openned;
                    return;
                }
                catch
                {
                    e.Result = (object)SerialPortStatus.OpenningError;
                    return;
                }
            }
        }



        /// <summary>
        /// Event raised by background worker, announcing status of port opening
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialPortOpennerCompletedHandler(object sender, RunWorkerCompletedEventArgs e)
        {
            SerialPortStatus PortState = (SerialPortStatus)e.Result;

            if (PortState == SerialPortStatus.Openned)
            {
                _openBtn.Text = "Close";		//update button caption
                WriteToConsole("Serial port " + ComPort.PortName + " opened!", true);

                EnableOrDisableControls(true);
                EnableOrDisablePortSettings(false);
                _bandwidthGraph.ChartAreas[0].AxisY.Maximum = 1.2 * ComPort.BaudRate / (8 * 1000);
            }
            else if (PortState == SerialPortStatus.Closed)
            {
                _openBtn.Text = "Open";		//update button caption
                WriteToConsole("Serial port " + ComPort.PortName + " closed!", true);
                EnableOrDisableControls(false);
                EnableOrDisablePortSettings(true);
            }
            else if (PortState == SerialPortStatus.OpenningError)
            {
                _openBtn.Text = "Open";
                WriteToConsole("Serial port " + ComPort.PortName + " could NOT be opened!", true);
            }
            else if (PortState == SerialPortStatus.ClosingError)
            {
                _openBtn.Text = "Close";
                WriteToConsole("Serial port " + ComPort.PortName + " could not be closed!", true);
            }
        }










/*
* 
*          --------------SERIAL PORT WRITE WITH BACKGROUND WORKER---------------------------    
* 
* */



        private enum SerialWriterStatus
        {
            OK, DataCouldNotBeSent
        };



        BackgroundWorker SerialWriter = new BackgroundWorker();
        string SerialWriterBuffer;


        /// <summary>
        /// Tries to send data via serial port on non-gui thread
        /// </summary>
        /// <param name="dataToSend">Represents data that should be sent</param>
        private void SendSerialData(string dataToSend)
        {


            SerialWriterBuffer += ReplaceHexTags(dataToSend);   //add data to send into buffer
            //SerialWriterBuffer += dataToSend;   //add data to send into buffer

            if (SerialWriterBuffer == "")
            {
                return;                     //if write buffer is empty just return
            }

            try
            {
                SerialWriter.RunWorkerAsync(SerialWriterBuffer);    //and try to send it
                SerialWriterBuffer = "";        //and if it works clear buffer
            }
            catch
            {
                //in case of write failure data is stored in buffer and after worker is ready and buffer is not empty this method is called again
            }
        }



        /// <summary>
        /// Handles DoWork event raised by SerialWriter (background worker)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialWriterDoWorkHandler(object sender, DoWorkEventArgs e)
        {
            try
            {
                string DataToSend = (string)e.Argument;
                ComPort.Write(DataToSend);
                UploadCounter.IncrementCounter(DataToSend.Length);
                e.Result = SerialWriterStatus.OK;
                return;
            }
            catch
            {
                e.Result = SerialWriterStatus.DataCouldNotBeSent;
                return;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialWriterCompletedHandler(object sender, RunWorkerCompletedEventArgs e)
        {
            SerialWriterStatus TransmissionState = (SerialWriterStatus)e.Result;

            if (TransmissionState == SerialWriterStatus.DataCouldNotBeSent)
            {
                WriteToConsole("Data could NOT be sent!", true);
            }

            if (SerialWriterBuffer != "")
            {
                SendSerialData("");
            }
        }





        /// <summary>
        /// Replaces hexa tags in format <0x0D>, <0xFF>, <0xB1> etc with char by its value
        /// </summary>
        /// <param name="textToParse"></param>
        /// <returns></returns>
        private string ReplaceHexTags(string textToParse)
        {
            return Regex.Replace(textToParse, "<0x..>", new MatchEvaluator(MatchToHexConverter));
        }



        public static string MatchToHexConverter(Match m){
            byte HexValue = 0;
            string HexString = m.ToString().Substring(3, 2);
            try
            {
                HexValue = Convert.ToByte(HexString, 16);
            }
            catch
            {
            }

            return Convert.ToString(Convert.ToChar(HexValue));
        }


        public string ReplaceNonPrintableCharsByHexTag(string textToParse)
        {
            StringBuilder ModifiedText = new StringBuilder();
            char c;
            for (int i = 0; i < textToParse.Length; i++)
            {
                c = textToParse[i];

                //if char is printable
                if (c > 0x20 && c < 0x7F)
                {
                    //just print it
                    //ModifiedData += c;
                    ModifiedText.Append(textToParse[i]);
                }
                else
                {
                    //else char is non printable, print it as <0x??>
                    ModifiedText.Append(string.Format("<0x{0:X2}>", Convert.ToUInt32(c)));
                }
            }
            return ModifiedText.ToString();
        }


/*
 * 
 *      -------------------------SERIAL DATA RECEPTION, PARSING AND HANDLING-----------------------------
 * 
 * */


        /// <summary>
        /// Event raised when serial data have been received - reads data and invokes handler on another thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialDataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort Port = (SerialPort)sender;
            string ReceivedData;

            try
            {
                ReceivedData = Port.ReadExisting();
                this.BeginInvoke(new StringDelegate(HandleReceivedSerialData), new object[] { ReceivedData });
            }
            catch
            {
                //port is not openned so just dont read it :)
            }
        }



        private void HandleReceivedSerialData(string receivedData)
        {
            bool SpecialCharFlag = false;


            //Increment traffic counter
            DownloadCounter.IncrementCounter(receivedData.Length);



            //if showing of non printable characters is enabled, replace all of them by hex tag in form <0xAB> etc
            if (Properties.Settings.Default.IsShowNonPrintableCharsEnabled)
            {
                WriteToConsole(ReplaceNonPrintableCharsByHexTag(receivedData), false);
            }
            //else just print received data
            else
            {
                WriteToConsole(receivedData, false);
            }


            //if special char reply is enabled check if special char has been received and in positive case raise event
            if(Properties.Settings.Default.IsSpecialCharReplyEnabled)
            {
                SpecialCharFlag = receivedData.Contains(Properties.Settings.Default.AutoSendingSpecialChar);      //beware this wont determine amount of special chars received.... only one event is fired
 
                if(SpecialCharFlag){
                    if (SpecialCharReceived != null) SpecialCharReceived(null, null);     //raise event for each received special char
                }
            }


        }









        /*
* 
*       -------------------DATA TRAFFIC COUNTING----------------------
* 
* */



        void Download_ValueUpdated(object sender, DataTraficCounter.MyEventArgs e)
        {
            try
            {
                this.BeginInvoke(new DataTraficCounter.ValueUpdatedEventHandler(HandleDownloadTraficCounterUpdate), sender, e);
            }
            catch
            {

            }
        }


        void Upload_ValueUpdated(object sender, DataTraficCounter.MyEventArgs e)
        {
            try
            {
                this.BeginInvoke(new DataTraficCounter.ValueUpdatedEventHandler(HandleUploadTraficCounterUpdate), sender, e);
            }
            catch
            {

            }
        }



        double UploadXAxisCntr = 0;
        void HandleUploadTraficCounterUpdate(object sender, DataTraficCounter.MyEventArgs e)
        {
            //update speed and total textbox
            _upSpeed.Text = string.Format("{0:0.000}", e.Rate / 1000);
            _uploaded.Text = string.Format("{0:0.000}", e.Total / 1000);

            //add new speed into graph and increment timer
            _bandwidthGraph.Series[0].Points.Add(new System.Windows.Forms.DataVisualization.Charting.DataPoint(UploadXAxisCntr, e.Rate / 1000));
            UploadXAxisCntr += BAUDRATEUPDATEINTERVAL * 0.001;

            //if timer is out of X axis range, clear graph and reset timer
            if (UploadXAxisCntr > CHARTXAXISRANGE)
            {
                UploadXAxisCntr = 0;
                _bandwidthGraph.Series[0].Points.Clear();
                _bandwidthGraph.Series[0].Points.Add(new System.Windows.Forms.DataVisualization.Charting.DataPoint(0, 0));
            }
        }



        double DownloadXAxisCntr = 0;
        void HandleDownloadTraficCounterUpdate(object sender, DataTraficCounter.MyEventArgs e)
        {
            //update speed and total textbox
            _dnSpeed.Text = string.Format("{0:0.000}", e.Rate / 1000);
            _downloaded.Text = string.Format("{0:0.000}", e.Total / 1000);


            //add new speed into graph and increment timer
            _bandwidthGraph.Series[1].Points.Add(new System.Windows.Forms.DataVisualization.Charting.DataPoint(DownloadXAxisCntr, e.Rate / 1000));

            //if timer is out of X axis range, clear graph and reset timer
            DownloadXAxisCntr += BAUDRATEUPDATEINTERVAL * 0.001;
            if (DownloadXAxisCntr > CHARTXAXISRANGE)
            {
                DownloadXAxisCntr = 0;
                _bandwidthGraph.Series[1].Points.Clear();
                _bandwidthGraph.Series[1].Points.Add(new System.Windows.Forms.DataVisualization.Charting.DataPoint(0, 0));
            }
        }



        private void ClearTraficGraph()
        {
            _bandwidthGraph.Series[0].Points.Clear();
            _bandwidthGraph.Series[1].Points.Clear();
            _bandwidthGraph.Series[1].Points.Add(new System.Windows.Forms.DataVisualization.Charting.DataPoint(0, 0));
            DownloadXAxisCntr = 0;
            UploadXAxisCntr = 0;
        }










/*
 * 
 * -------------------------------FORM METHODS-----------------------------------------
 * 
 * */



        /// <summary>
        /// Writes text to main console optionaly at new line
        /// </summary>
        /// <param name="text">Text to write</param>
        /// <param name="atNewLine">Flag if text should be placed new line</param>
        private void WriteToConsole(string text, bool atNewLine)
        {
            string TextToAppend;

            if (string.IsNullOrEmpty(text)) return;

            //if text should be placed at new line
            if (atNewLine)
            {
                char LastCharInConsole;

                //if there is nothing in console we consider it as new line
                if (_consoleBox.Text.Length == 0)
                {
                    LastCharInConsole = '\n';
                }
                //else there is something in console so read last char
                else
                {
                    LastCharInConsole = _consoleBox.Text[_consoleBox.Text.Length - 1];
                }

                //and if last char is one of EOL chars line is empty and text just goes to end
                if (LastCharInConsole == '\r' || LastCharInConsole == '\n')
                {
                    TextToAppend = text + Environment.NewLine;
                }
                //else line is not empty and we have to add newline char
                else
                {
                    TextToAppend = Environment.NewLine + text + Environment.NewLine;
                }
            }
            //else text should be just appended so do it
            else
            {
                TextToAppend = text;
            }


            /*

            //if show nonprintable characters is disabled - we dont want to see hex tags so we have to convert them into char values
            if (!Properties.Settings.Default.IsShowNonPrintableCharsEnabled)
            {
                TextToAppend = ReplaceHexTags(TextToAppend);
            }
             * 
             * 
             */

            if (Properties.Settings.Default.IsLogToFileEnabled)
            {
                try
                {
                    LogFile.Write(TextToAppend);
                }
                catch
                {

                }
            }

            _consoleBox.AppendText(TextToAppend);
        }



        /// <summary>
        /// Enables or disables controls that should be active only while serial port is openned
        /// </summary>
        /// <param name="enableControls"></param>
        private void EnableOrDisableControls(bool enableControls)
        {
            if (enableControls)
            {
                _cmdBox.Enabled = true;
                _sendBtn.Enabled = true;

                _consoleBox.ReadOnly = false;

                _cmdBox.Focus();
            }
            else
            {
                _cmdBox.Enabled = false;
                _sendBtn.Enabled = false;

                _consoleBox.ReadOnly = true;
            }
        }



        /// <summary>
        /// Enables or disables serial port settings that should not be changed while port is openned
        /// </summary>
        /// <param name="enableSettings"></param>
        private void EnableOrDisablePortSettings(bool enableSettings)
        {
            if (enableSettings)
            {
                _portName.Enabled = true;
                _portRate.Enabled = true;
            }
            else
            {
                _portName.Enabled = false;
                _portRate.Enabled = false;
            }
        }



        /// <summary>
        /// Saves command into combobox history
        /// </summary>
        /// <param name="cmd"></param>
        private void SaveCmdHistory(string cmd)
        {
            _cmdBox.Items.Insert(0, cmd);
            if (_cmdBox.Items.Count > 5)
            {
                _cmdBox.Items.RemoveAt(_cmdBox.Items.Count - 1);
            }
            _cmdBox.SelectedIndex = -1;
            _cmdBox.Text = "";
        }










/*
 * 
 * ----------------------------AUTOSENDING: INTERVAL----------------------------------------------------
 * 
 * */



        private Timer AutoSendingTimer = new Timer();
        private string AutoSendingIntervalCommand = "";



        /// <summary>
        /// Event to autosend data in specified interval
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AutoSendingTimer_Tick(object sender, EventArgs e)
        {
            //if serial port is openned send data
            if (ComPort.IsOpen)
            {
                //print local echo into console if enabled
                if (Properties.Settings.Default.IsLocalEchoEnabled)
                {
                    if (Properties.Settings.Default.IsShowNonPrintableCharsEnabled)
                    {
                        WriteToConsole(ReplaceNonPrintableCharsByHexTag(AutoSendingIntervalCommand + EOLCommand), false);                  
                    }
                    else
                    {
                        WriteToConsole(ReplaceHexTags(AutoSendingIntervalCommand + EOLCommand), false);
                    }
                }

                //send data via serial port
                SendSerialData(AutoSendingIntervalCommand + EOLCommand);
            }
            /*
            //else stop autosending
            else
            {
                inIntervalmsToolStripMenuItem.Checked = false;
                AutoSendingTimer.Stop();
            }
            */


        }






/*
* 
* ----------------------------AUTOSENDING: SPECIAL CHAR RECEIVED----------------------------------------------------
* 
* */

        private event EventHandler SpecialCharReceived;
        private string AutoSendingSpecialCharCommand = "";

        private void ReplyToReceivedSpecialChar(object sender, EventArgs a)
        {
            //if serial port is openned send data
            if (ComPort.IsOpen)
            {
                //print local echo into console if enabled
                if (Properties.Settings.Default.IsLocalEchoEnabled)
                {

                    if (Properties.Settings.Default.IsShowNonPrintableCharsEnabled)
                    {
                        WriteToConsole(ReplaceNonPrintableCharsByHexTag(AutoSendingSpecialCharCommand + EOLCommand), false);
                    }
                    else
                    {
                        WriteToConsole(ReplaceHexTags(AutoSendingSpecialCharCommand + EOLCommand), false);
                    }
                }

                //send data via serial port
                SendSerialData(AutoSendingSpecialCharCommand + EOLCommand);
            }
            /*
            //else stop autosending
            else
            {
            }
             * */
        }
















/*
 * 
 * -------------------OTHER FORM CONTROLS-------------------------------------------------
 * 
 * */


        /// <summary>
        /// Loads names of available serial ports after openning Portname combobox 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _portName_DropDown(object sender, EventArgs e)
        {
            LoadAvailablePorts(_portName);
        }



        /// <summary>
        /// Loads available baudrates after openning baudrate combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _portRate_DropDown(object sender, EventArgs e)
        {
            LoadAvailableBaudRates(_portRate);
        }



        /// <summary>
        /// Handles serial port openning, closing and enabling/disabling of associated controls after pressing open/close port button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _connectBtn_Click(object sender, EventArgs e)
        {
            OpenSerialPort();
        }



        /// <summary>
        /// Sends command after pressing send button next to cmdbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _sendBtn_Click(object sender, EventArgs e)
        {
            string Command = _cmdBox.Text;
            //print local echo into console if enabled
            if (Properties.Settings.Default.IsLocalEchoEnabled)
            {
                if (Properties.Settings.Default.IsShowNonPrintableCharsEnabled)
                {
                    WriteToConsole(ReplaceNonPrintableCharsByHexTag(Command + EOLCommand), false);
                }
                else
                {
                    WriteToConsole(ReplaceHexTags(Command + EOLCommand), false);
                }
            }

            SendSerialData(Command + EOLCommand);
            SaveCmdHistory(Command);
            _cmdBox.Select();
        }



        /// <summary>
        /// Pressing enter in cmdbox sends command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cmdBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                _sendBtn_Click(sender, e);
            }
        }



        /// <summary>
        /// Pressing key in console window sends char over serial
        /// If enter is pressed, it is replaced by EOL char specified in settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _consoleBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //this causes that key is not directly pasted into consolebox
            e.Handled = true;

            string Key = e.KeyChar.ToString();

            if (ComPort.IsOpen)
            {
                if (Convert.ToChar(Key) == '\r')
                {
                    Key = EOLConsole;
                }

                if (Properties.Settings.Default.IsLocalEchoEnabled)    //if local echo is enabled
                {
                    WriteToConsole(Key, false);                        //print char into console window
                }

                SendSerialData(Key);
            }
        }










/*
 * 
 *      ------------------------CONTEXT MENU CLICKS---------------------------------
 * 
 * */



        /// <summary>
        /// Traffic counters or graph -> reset click
        /// Resets all traffic counting and graph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetCounterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UploadCounter.ClearTotal();
            DownloadCounter.ClearTotal();
            ClearTraficGraph();
        }



        /// <summary>
        /// Send button -> Once click
        /// Same as clicking on send button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sendBtn_Click(sender, e);
        }



        /// <summary>
        /// Send button -> interval click
        /// Handles toggling of autosending in specified interval
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inIntervalmsToolStripMenuItem_Click(object sender, EventArgs e)
        {


            inIntervalmsToolStripMenuItem.Checked = !inIntervalmsToolStripMenuItem.Checked;
            bool IsIntervalSending = inIntervalmsToolStripMenuItem.Checked;


            if (IsIntervalSending)
            {
                try
                {
                    string Command = _cmdBox.Text;

                    if (Command == "")
                    {
                        WriteToConsole("Nothing to autosend!", true);
                        inIntervalmsToolStripMenuItem.Checked = false;
                        return;
                    }

                    AutoSendingTimer.Interval = Convert.ToInt32(toolStripMenuItem2.Text);
                    AutoSendingTimer.Enabled = true;
                    AutoSendingIntervalCommand = Command;
                    SaveCmdHistory(Command);
                    _cmdBox.Text = "";
                    SendBtnContextMenu.Close();
                }
                catch
                {
                    WriteToConsole("Interval must be integer!", true);
                    inIntervalmsToolStripMenuItem.Checked = false;
                    return;
                }
            }
            else
            {
                AutoSendingTimer.Enabled = false;
            }
        }

























        /// <summary>
        /// Console->Clear click
        /// Clears console text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _consoleBox.Clear();
        }



        /// <summary>
        /// Console->Select All click
        /// Selects all text from console
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _consoleBox.SelectAll();
        }



        /// <summary>
        /// Console -> Copy click
        /// Copies selected text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(_consoleBox.SelectedText);
            }
            catch
            {

            }
        }



        /// <summary>
        /// Console -> Word Wrap click
        /// Toggles word wrapping in console window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsWordWrapEnabled = !(Properties.Settings.Default.IsWordWrapEnabled);   //toggle setting
            _consoleBox.WordWrap = Properties.Settings.Default.IsWordWrapEnabled;
            wordWrapToolStripMenuItem.Checked = Properties.Settings.Default.IsWordWrapEnabled;
        }



        /// <summary>
        /// Console -> Local Echo click
        /// Toggles local echo of sent serial data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void localEchoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsLocalEchoEnabled = !(Properties.Settings.Default.IsLocalEchoEnabled);
            localEchoToolStripMenuItem.Checked = Properties.Settings.Default.IsLocalEchoEnabled;
        }



        /// <summary>
        /// Console -> EOL -> CRLF
        /// End of line character is set to CRLF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cRLFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.EOLConsoleSettings = (int)EOL.CRLF;
            EOLConsole = "\r\n";
            cRLFToolStripMenuItem.Checked = true;
            cRToolStripMenuItem.Checked = false;
            nToolStripMenuItem.Checked = false;
        }



        /// <summary>
        /// Console -> EOL -> CR
        /// End of line character is set to CR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.EOLConsoleSettings = (int)EOL.CR;
            EOLConsole = "\r";
            cRLFToolStripMenuItem.Checked = false;
            cRToolStripMenuItem.Checked = true;
            nToolStripMenuItem.Checked = false;
        }



        /// <summary>
        /// Console -> EOL -> LF
        /// End of line character is set to LF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.EOLConsoleSettings = (int)EOL.LF;
            EOLConsole = "\n";
            cRLFToolStripMenuItem.Checked = false;
            cRToolStripMenuItem.Checked = false;
            nToolStripMenuItem.Checked = true;
        }



        /// <summary>
        /// Console -> Show non printable characters as hex 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nonPrintableCharsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsShowNonPrintableCharsEnabled = !(Properties.Settings.Default.IsShowNonPrintableCharsEnabled);
            nonPrintableCharsToolStripMenuItem.Checked = Properties.Settings.Default.IsShowNonPrintableCharsEnabled;
        }



        /// <summary>
        /// Console -> Log to File -> Enabled click
        /// Toggles logging of comunication into log file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsLogToFileEnabled = !(Properties.Settings.Default.IsLogToFileEnabled);
            enabledToolStripMenuItem.Checked = Properties.Settings.Default.IsLogToFileEnabled;


            if (Properties.Settings.Default.IsLogToFileEnabled)  //this means that logging has just been turned on
            {
                try
                {
                    LogFile = File.AppendText(Properties.Settings.Default.LogFilePath);
                    logtxtToolStripMenuItem.Enabled = false;
                    clearLogFileToolStripMenuItem.Enabled = false;
                }
                catch
                {
                    WriteToConsole("Invalid log file has been specified", true);
                    logtxtToolStripMenuItem.Enabled = true;
                    clearLogFileToolStripMenuItem.Enabled = true;

                    Properties.Settings.Default.IsLogToFileEnabled = !(Properties.Settings.Default.IsLogToFileEnabled);
                    enabledToolStripMenuItem.Checked = Properties.Settings.Default.IsLogToFileEnabled;
                }
            }
            else     //logging has just been turned off
            {
                try
                {
                    LogFile.Flush();
                    LogFile.Close();
                }
                catch
                {
                }

                logtxtToolStripMenuItem.Enabled = true;
                clearLogFileToolStripMenuItem.Enabled = true;
            }
        }



        /// <summary>
        /// Console -> Log to File -> Clear logfile click
        /// Clears content of logfile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearLogFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream OutputFile = File.Open(Properties.Settings.Default.LogFilePath, FileMode.Create);
                OutputFile.Flush();
                OutputFile.Close();
            }
            catch
            {
                WriteToConsole("Invalid log file has been specified", true);
            }
        }



        /// <summary>
        /// Cmdbox -> Select all
        /// Selects all text in cmdbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _cmdBox.SelectAll();
        }



        /// <summary>
        /// Cmdbox -> Copy
        /// Copies selected box into clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(_cmdBox.SelectedText);
            }
            catch
            {

            }
        }



        /// <summary>
        /// Cmdbox -> Paste
        /// Pastes content of clipboard into cmdbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.GetText();
        }



        /// <summary>
        /// Console -> Log to file -> name.ext click
        /// Opens file explorer  to select destination log file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logtxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.RestoreDirectory = true;
            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.LogFilePath = FileDialog.FileName;
                string[] Tokens = FileDialog.FileName.Split('\\');
                string Name = Tokens[Tokens.Length - 1];    //last token contains name and extension
                logtxtToolStripMenuItem.Text = Name;
            }
        }



        private void afterSpecialCharToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsSpecialCharReplyEnabled = !Properties.Settings.Default.IsSpecialCharReplyEnabled;
            bool IsEnabled = Properties.Settings.Default.IsSpecialCharReplyEnabled;

            afterSpecialCharToolStripMenuItem1.Checked = IsEnabled;

            if (IsEnabled)
            {
                try
                {
                    string Command = _cmdBox.Text;
                    Properties.Settings.Default.AutoSendingSpecialChar = toolStripMenuItem3.Text[0].ToString();

                    AutoSendingSpecialCharCommand = Command;
                    toolStripMenuItem3.Text = Properties.Settings.Default.AutoSendingSpecialChar;
                    SaveCmdHistory(_cmdBox.Text);
                    _cmdBox.Text = "";
                    SendBtnContextMenu.Close();
                }
                catch
                {
                    afterSpecialCharToolStripMenuItem1.Checked = false;
                    WriteToConsole("You have to specify special reply char first!", true);
                }
            }
        }


        /// <summary>
        /// Send btn -> Interval -> textbox keypress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char PressedKey = Convert.ToChar(e.KeyChar);
            if (PressedKey == '\r')
            {
                inIntervalmsToolStripMenuItem_Click(sender, e);
                e.Handled = true;
            }
            else
            {
                AutoSendingTimer.Stop();
            }
        }



        /// <summary>
        /// Send btn -> Special Char -> textbox keypress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char PressedKey = Convert.ToChar(e.KeyChar);
            if (PressedKey == '\r')
            {
                //if enter is pressed while writing in special char textbox virtually press previous toolstrip to enable autosending
                afterSpecialCharToolStripMenuItem1_Click(sender, e);
                e.Handled = true;
            }
            else
            {
                Properties.Settings.Default.IsSpecialCharReplyEnabled = false;
                afterSpecialCharToolStripMenuItem1.Checked = false;
            }
        }

        private void cRLFToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.EOLCmdSettings = (int)EOL.CRLF;
            EOLCommand = "\r\n";
            cRLFToolStripMenuItem1.Checked = true;
            cRToolStripMenuItem1.Checked = false;
            lFToolStripMenuItem.Checked = false;
            noneToolStripMenuItem.Checked = false;
        }

        private void cRToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.EOLCmdSettings = (int)EOL.CR;
            EOLCommand = "\r";
            cRLFToolStripMenuItem1.Checked = false;
            cRToolStripMenuItem1.Checked = true;
            lFToolStripMenuItem.Checked = false;
            noneToolStripMenuItem.Checked = false;
        }

        private void lFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.EOLCmdSettings = (int)EOL.LF;
            EOLCommand = "\n";
            cRLFToolStripMenuItem1.Checked = false;
            cRToolStripMenuItem1.Checked = false;
            lFToolStripMenuItem.Checked = true;
            noneToolStripMenuItem.Checked = false;
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.EOLCmdSettings = (int)EOL.None;
            EOLCommand = "";
            cRLFToolStripMenuItem1.Checked = false;
            cRToolStripMenuItem1.Checked = false;
            lFToolStripMenuItem.Checked = false;
            noneToolStripMenuItem.Checked = true;

        }

        private void _consoleBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.C)
                {
                    if (_consoleBox.SelectedText.Length != 0)
                    {
                        Clipboard.SetText(_consoleBox.SelectedText);
                    }

                }


                if (e.KeyCode == Keys.A)
                {
                    _consoleBox.SelectAll();
                }

                if (e.KeyCode == Keys.X)
                {
                    if (_consoleBox.SelectedText.Length != 0)
                    {
                        Clipboard.SetText(_consoleBox.SelectedText);
                        _consoleBox.SelectedText = "";
                    }
                }
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.Back)
            {
                _consoleBox.SelectedText = "";
                e.SuppressKeyPress = true;
            }
        }
    }
}
