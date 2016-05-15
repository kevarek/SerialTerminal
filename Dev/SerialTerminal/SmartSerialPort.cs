using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;


namespace SerialTerminal
{
    public partial class SmartSerialPort :  UserControl 
    {
        //Creates new instance of serial port
        public SerialPort ComPort = new SerialPort();


        public enum DataBits
        {
            Seven = 7,
            Eight = 8,
        }


        //Possible states of serial port
        public enum SerialPortStatus
        {
            Openned, Closed, ClosingError, OpenningError, DataSendingError
        };


        //Event which is fired each time port status is changed (useful for making console window of serial port inactive, writing port status changes into console etc etc.).
        public class PortStatusUpdatedEvArgs
        {
            public SerialPortStatus PortStatus;
            public string Message;

            public PortStatusUpdatedEvArgs(SerialPortStatus portStatus, string message)
            {
                this.PortStatus = portStatus;
                this.Message = message;
            }
        }

        public delegate void PortStatusUpdatedEvH(object sender, PortStatusUpdatedEvArgs e);
        public event PortStatusUpdatedEvH PortStatusUpdated;

        //Event which is fired each time serial data is received
        public delegate void DataReceivedEvH(object sender, string data);
        public event DataReceivedEvH DataReceived;


/*
 * 
 * ---------COMPONENT INITIALIZATION AND DRAWING-----------
 * 
 */

        public SmartSerialPort()
        {
            InitializeComponent();


            //register event of backgroundworker SerialPortOpenner to asynchronously open or close serial port - no GUI freezing
            SerialPortOpenner.DoWork += SerialPortOpennerDoWorkHandler;
            SerialPortOpenner.RunWorkerCompleted += SerialPortOpennerCompletedHandler;

            //register event of backgroundworker SerialWriter for async serial port writing - no GUI freezing that way
            SerialWriter.DoWork += SerialWriterDoWorkHandler;
            SerialWriter.RunWorkerCompleted += SerialWriterCompletedHandler;

            //Load stuff from settings storage
            LoadAvailablePorts(_portName);
            LoadAvailableBaudRates(_portBaudRate);
            LoadAvailableDatabits(_portDatabits);
            LoadAvailableParityOptions(_portParity);
            LoadAvailableStopBitsOption(_portStopbits);

            //Focus open button so port can be openned immediately after program starts by pressing enter or space
            _openBtn.Focus();                               

            //Assign some events
            _openBtn.Click += (s, e) =>
            {
                OpenClose();
            };

            _portName.DropDown += (s, e) =>
            {
                LoadAvailablePorts((ComboBox)s);
            };

            //port is openned when enter is pressed while cursor is in portname or port baudrate
            _portName.KeyPress += (s, e) =>
            {
                if ((Keys)e.KeyChar == Keys.Enter)
                {
                    OpenClose();
                }
            };

            _portBaudRate.KeyPress += (s, e) =>
            {
                if ((Keys)e.KeyChar == Keys.Enter)
                {
                    OpenClose();
                }
            };

            _portDatabits.KeyPress += (s, e) =>
            {
                if ((Keys)e.KeyChar == Keys.Enter)
                {
                    OpenClose();
                }
            };

            _portStopbits.KeyPress += (s, e) =>
            {
                if ((Keys)e.KeyChar == Keys.Enter)
                {
                    OpenClose();
                }
            };

            _portParity.KeyPress += (s, e) =>
            {
                if ((Keys)e.KeyChar == Keys.Enter)
                {
                    OpenClose();
                }
            };

            this.HandleCreated += (s, e) =>
            {
                //load settings
                _portName.Text = Properties.Settings.Default.PortName;
                _portBaudRate.Text = Properties.Settings.Default.PortBaudRate;
                _portDatabits.Text = Properties.Settings.Default.PortDatabits;
                _portStopbits.Text = Properties.Settings.Default.PortStopBits;
                _portParity.Text = Properties.Settings.Default.PortParity;
            };

            this.HandleDestroyed += (s, e) =>
            {
                //save settings
                Properties.Settings.Default.PortName = _portName.Text;
                Properties.Settings.Default.PortBaudRate = _portBaudRate.Text;
                Properties.Settings.Default.PortDatabits = _portDatabits.Text;
                Properties.Settings.Default.PortStopBits = _portStopbits.Text;
                Properties.Settings.Default.PortParity = _portParity.Text;
                Properties.Settings.Default.Save();
            };
        }



/*
 * 
 * ---------COMPONENT PROPERTIES-----------
 * 
 */

        public string BaudRate
        {
            get
            {
                return _portBaudRate.Text; 
            }
        }

/*
* 
*---------------------------SERIAL PORT OPEN WITH BACKGROUND WORKER----------------
* 
*/

        //create instance of background worker that will handle opening/closing port in non-gui thread
        private BackgroundWorker SerialPortOpenner = new BackgroundWorker();
        private BackgroundWorker SerialPortCloser = new BackgroundWorker();


        /// <summary>
        /// Opens serial port via non-gui thread, event is raised when operation completes
        /// </summary>
        public void OpenClose()
        {
            //if serial port is not openned
            if (ComPort.IsOpen == false)
            {
                //set serial port settings
                try
                {
                    ComPort.PortName = _portName.Text;
                }
                catch
                {
                    //Fire status update event if specified portname is invalid
                    if (PortStatusUpdated != null)
                    {
                        PortStatusUpdated(this, new PortStatusUpdatedEvArgs(SerialPortStatus.OpenningError, "Invalid port name!" + Environment.NewLine));
                    }
                    return;
                }

                try
                {
                    ComPort.BaudRate = Convert.ToInt32(_portBaudRate.Text);
                }
                catch
                {
                    //Fire status update event if specified baudrate is invalid
                    if (PortStatusUpdated != null)
                    {
                        PortStatusUpdated(this, new PortStatusUpdatedEvArgs(SerialPortStatus.OpenningError, "Invalid baudrate!" + Environment.NewLine));
                    }
                    return;
                }

                try
                {
                    ComPort.Parity = (Parity)Enum.Parse(typeof(Parity), _portParity.SelectedItem.ToString());
                }
                catch
                {
                    //Fire status update event if specified parity is invalid
                    if (PortStatusUpdated != null)
                    {
                        PortStatusUpdated(this, new PortStatusUpdatedEvArgs(SerialPortStatus.OpenningError, "Invalid parity!" + Environment.NewLine));
                    }
                    return;
                }


                try
                {
                    ComPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), _portStopbits.SelectedItem.ToString());
                }
                catch
                {
                    //Fire status update event if specified stopbits value is invalid
                    if (PortStatusUpdated != null)
                    {
                        PortStatusUpdated(this, new PortStatusUpdatedEvArgs(SerialPortStatus.OpenningError, "Stopbits invalid!" + Environment.NewLine));
                    }
                    return;
                }

                try
                {
                    ComPort.DataBits = (int)Enum.Parse(typeof(DataBits), _portDatabits.SelectedItem.ToString());
                }
                catch
                {
                    //Fire status update event if specified stopbits value is invalid
                    if (PortStatusUpdated != null)
                    {
                        PortStatusUpdated(this, new PortStatusUpdatedEvArgs(SerialPortStatus.OpenningError, "Databits invalid!" + Environment.NewLine));
                    }
                    return;
                }

                ComPort.Handshake = Handshake.None;
                ComPort.DataReceived += new SerialDataReceivedEventHandler(SerialDataReceivedHandler);
                ComPort.ReadTimeout = 500;
                ComPort.WriteTimeout = 500;
            }

            _openBtn.Text = "---";
            //and open port via nongui thread
            try
            {
                SerialPortOpenner.RunWorkerAsync();
            }
            catch
            {
                //Fire status update event if specified baudrate is invalid
                if (PortStatusUpdated != null)
                {
                    PortStatusUpdated(this, new PortStatusUpdatedEvArgs(SerialPortStatus.OpenningError, "Serial port openning in progress!" + Environment.NewLine));
                }

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
                //update button caption and disable settings forms
                _openBtn.Text = "Close";		
                _portName.Enabled = false;
                _portBaudRate.Enabled = false;
                _portDatabits.Enabled = false;
                _portStopbits.Enabled = false;
                _portParity.Enabled = false;

                //Fire status update event if port has been openned
                if(PortStatusUpdated != null){
                    PortStatusUpdated(this, new PortStatusUpdatedEvArgs(PortState, "Serial port " + ComPort.PortName + " opened!" + Environment.NewLine));
                }
            }
            else if (PortState == SerialPortStatus.Closed)
            {
                _openBtn.Text = "Open";		//update button caption and set focus to it
                _portName.Enabled = true;
                _portBaudRate.Enabled = true;
                _portDatabits.Enabled = true;
                _portStopbits.Enabled = true;
                _portParity.Enabled = true;
                _openBtn.Focus();

                //Fire status update event if port has been closed
                if (PortStatusUpdated != null)
                {
                    PortStatusUpdated(this, new PortStatusUpdatedEvArgs(PortState, "Serial port " + ComPort.PortName + " closed!" + Environment.NewLine));
                }
            }
            else if (PortState == SerialPortStatus.OpenningError)
            {
                _openBtn.Text = "Open";
                _portName.Enabled = true;
                _portBaudRate.Enabled = true;
                _portDatabits.Enabled = true;
                _portStopbits.Enabled = true;
                _portParity.Enabled = true;

                //Fire status update event if port could not be openned
                if (PortStatusUpdated != null)
                {
                    PortStatusUpdated(this, new PortStatusUpdatedEvArgs(PortState, "Serial port " + ComPort.PortName + " could NOT be opened!" + Environment.NewLine));
                }
            }
            else if (PortState == SerialPortStatus.ClosingError)
            {
                _openBtn.Text = "Close";
                _portName.Enabled = false;
                _portBaudRate.Enabled = false;
                _portDatabits.Enabled = false;
                _portStopbits.Enabled = false;
                _portParity.Enabled = false;

                //Fire status update event if port could not be openned
                if (PortStatusUpdated != null)
                {
                    PortStatusUpdated(this, new PortStatusUpdatedEvArgs(PortState, ("Serial port " + ComPort.PortName + " could NOT be closed!" + Environment.NewLine)));
                }
            }
        }


        /// <summary>
        /// Gets actual state of serial port
        /// </summary>
        public bool IsOpen
        {
            get { return ComPort.IsOpen; }
        }


        //Compare function for port name sorting
        private static int ComparePortNumbers(string x, string y)
        {
            if (x == null && y == null)
            {
                return 0;
            }

            else if (x == null && y != null)
            {
                return -1;
            }
            else if (x != null && y == null){
                return 1;
            }
            else
            {
                try
                {
                    x = x.Remove(0, 3);
                    int ValX = Convert.ToInt32(x);


                    y = y.Remove(0, 3);
                    int ValY = Convert.ToInt32(y);

                    if (ValX > ValY)
                    {
                        return 1;
                    }
                    else if (ValX < ValY)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch
                {
                    return 0;
                }
            }
        }


        /// <summary>
        /// Loads all currently available system serial port names into specified combobox
        /// </summary>
        private void LoadAvailablePorts(ComboBox box)
        {
            List<string> AvailablePortsSorted = new List<string>(SerialPort.GetPortNames());
            AvailablePortsSorted.Sort(ComparePortNumbers);
            box.Items.Clear();
            box.Items.AddRange(AvailablePortsSorted.ToArray<string>());
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
            box.Items.Add("230400");
            box.Items.Add("460800");
            box.Items.Add("921600");
            box.Items.Add("1843200");
            box.Items.Add("3686400");
        }


        private void LoadAvailableParityOptions(ComboBox box)
        {
            box.Items.Clear();

            foreach(var s in Enum.GetNames(typeof(Parity)))
            {
                box.Items.Add(s);
            }
        }

        private void LoadAvailableStopBitsOption(ComboBox box)
        {
            box.Items.Clear();

            foreach(var s in Enum.GetNames(typeof(StopBits)))
            {
                box.Items.Add(s);
            }
        }


        private void LoadAvailableDatabits(ComboBox box)
        {
            box.Items.Clear();

            foreach (var s in Enum.GetNames(typeof(DataBits)))
            {
                box.Items.Add(s);
            }
        }

/*
* 
*--------------SERIAL PORT WRITE WITH BACKGROUND WORKER---------------------------    
* 
* */

        private enum SerialWriterStatus
        {
            OK, DataCouldNotBeSent
        };


        private BackgroundWorker SerialWriter = new BackgroundWorker();
        private StringBuilder SerialWriterBuffer = new StringBuilder("", 16384);


        /// <summary>
        /// Tries to send data via serial port on non-gui thread
        /// </summary>
        /// <param name="dataToSend">Represents data that should be sent</param>
        public void Send(string dataToSend)
        {
            SerialWriterBuffer.Append(dataToSend);   //add data to send into buffer

            if (SerialWriterBuffer.Length == 0)
            {
                return;                              //if write buffer is empty just return
            }

            try
            {
                SerialWriter.RunWorkerAsync(SerialWriterBuffer.ToString());    //and try to send it
                SerialWriterBuffer.Clear();          //and if it works clear buffer
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
        /// Handles SerialWriter complete event, Fires warning if data could not be sent or issues another write if buffer is not empty.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialWriterCompletedHandler(object sender, RunWorkerCompletedEventArgs e)
        {
            SerialWriterStatus TransmissionState = (SerialWriterStatus)e.Result;

            if (TransmissionState == SerialWriterStatus.DataCouldNotBeSent)
            {

                //Fire status update event if data could not be sent - means jammed, stucked or expired serial port
                if (PortStatusUpdated != null)
                {
                    PortStatusUpdated(this, new PortStatusUpdatedEvArgs(SerialPortStatus.DataSendingError, ("Data could NOT be sent!" + Environment.NewLine)));
                }
            }

            //if output buffer is not empty issue serial writer again
            if (SerialWriterBuffer.ToString() != "")
            {
                Send("");
            }
        }


/*
* 
*-------------------------SERIAL DATA RECEPTION, PARSING AND HANDLING-----------------------------
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

            try
            {
                if (DataReceived != null)
                {
                    DataReceived(this, Port.ReadExisting());
                }
            }
            catch
            {
                //port is not openned so just dont read it :)
            }
        }






        }

}
