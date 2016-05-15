using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;


namespace SerialTerminal
{
    public partial class TrafficCounter : UserControl
    {
        private System.Timers.Timer RefreshTimer;

        private delegate void GraphHandler();




        private int PrevUploadTotal = 0;
        private int UploadTotal = 0;
        private double UploadSpeed = 0;

        private int PrevDownloadTotal = 0;
        private int DownloadTotal = 0;
        private double DownloadSpeed = 0;

        private double UploadXAxisCntr = 0;
        private double DownloadXAxisCntr = 0;


        public TrafficCounter()
        {
            InitializeComponent();

            //initialize bandwidth graph
            _graph.ChartAreas[0].AxisY.IsLabelAutoFit = false;
            _graph.ChartAreas[0].AxisY.LabelStyle.Format = "{0.0}";
            _graph.ChartAreas[0].AxisX.IsLabelAutoFit = false;
            _graph.ChartAreas[0].AxisX.LabelStyle.Format = "{0}";

            ChartXAxisRange = 60;       //Default value for chart X axis range
            ChartYMax = 9600;             //Default value for chart Y axis range

            //Add zero point into graph to initialize axes titles
            _graph.Series[0].Points.Add(new System.Windows.Forms.DataVisualization.Charting.DataPoint(0, 0));
            
            //Initialize timer
            RefreshInterval = 1000;
            RefreshTimer = new System.Timers.Timer();
            RefreshTimer.Interval = RefreshInterval;
            RefreshTimer.Elapsed += new ElapsedEventHandler(RefreshIntervalElapsed);
            RefreshTimer.Start();
        }


        private void RefreshIntervalElapsed(object sender, ElapsedEventArgs e)
        {
            //update speed and total
            UploadSpeed = (double)( UploadTotal - PrevUploadTotal ) / ( (double)RefreshInterval / 1000 );
            PrevUploadTotal = UploadTotal;
            if (UploadSpeed < 0) UploadSpeed = 0;

            //update speed and total
            DownloadSpeed = (double)(DownloadTotal - PrevDownloadTotal) / ( (double)RefreshInterval / 1000);
            PrevDownloadTotal = DownloadTotal;
            if (DownloadSpeed < 0) DownloadSpeed = 0;

            //Update forms
            this.BeginInvoke(new GraphHandler(HandleUpdatedValues));
        }


        private void HandleUpdatedValues()
        {
            //Update text values
            _uploadSpeed.Text = string.Format("{0:0.000}", UploadSpeed / 1000);
            _uploadTotal.Text = string.Format("{0:0.000}", (double)UploadTotal / 1000);

            _downloadSpeed.Text = string.Format("{0:0.000}", DownloadSpeed / 1000);
            _downloadTotal.Text = string.Format("{0:0.000}", (double)DownloadTotal / 1000);


            //add new speed into graph and increment timer
            _graph.Series[0].Points.Add(new System.Windows.Forms.DataVisualization.Charting.DataPoint(UploadXAxisCntr, UploadSpeed / 1000));    //Add upload speed[KB] into graph
            UploadXAxisCntr += RefreshInterval / 1000;                                                                                          //Increment timer value

            //if timer is out of X axis range, clear graph and reset timer
            if (UploadXAxisCntr > ChartXAxisRange)
            {
                UploadXAxisCntr = 0;
                _graph.Series[0].Points.Clear();
                _graph.Series[0].Points.Add(new System.Windows.Forms.DataVisualization.Charting.DataPoint(0, 0));
            }
        

            //add new speed into graph and increment timer
            _graph.Series[1].Points.Add(new System.Windows.Forms.DataVisualization.Charting.DataPoint(DownloadXAxisCntr, DownloadSpeed / 1000));

            //if timer is out of X axis range, clear graph and reset timer
            DownloadXAxisCntr += RefreshInterval / 1000;
            if (DownloadXAxisCntr > ChartXAxisRange)
            {
                DownloadXAxisCntr = 0;
                _graph.Series[1].Points.Clear();
                _graph.Series[1].Points.Add(new System.Windows.Forms.DataVisualization.Charting.DataPoint(0, 0));
            }

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

        /// <summary>
        /// Refraw function for this user controll.
        /// </summary>
        private void Redraw()
        {
            //Set vertical positions
            _uploadGrp.Top = 0;
            _downloadGrp.Top = _uploadGrp.Bottom + Spacing;
            _graph.Top = _downloadGrp.Bottom + Spacing;

            //Set horizontal positions
            _uploadGrp.Left = 0;
            _downloadGrp.Left = 0;
            _graph.Left = 0;

            //and extend it over all controls client rectangle
            _uploadGrp.Width = ClientRectangle.Width - 2 * Spacing;
            _downloadGrp.Width = ClientRectangle.Width - 2 * Spacing;
            _graph.Width = ClientRectangle.Width - 2 * Spacing;



            //_frame.Width = ClientRectangle.Width;
            //_frame.Height = ClientRectangle.Height;







            //Extend elements over whole usercontrols width


            //Restrict height changes
            SetClientSizeCore(ClientRectangle.Width, _graph.Bottom);
        }


        private int _Spacing;
        [DefaultValue("4"), Category("Appearance"), Description("Spacing of controls elements.")]
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

        [DefaultValue("30"), Category("Appearance"), Description("Range of charts X axis.")]
        private int _ChartXAxisRange = 99;
        public int ChartXAxisRange
        {
            get
            {
                return _ChartXAxisRange;
            }
            set
            {
                _ChartXAxisRange = value;
                _graph.ChartAreas[0].AxisX.Maximum = value;
            }
        }




        private double _ChartYMax;
        //Represents maximum baudrate in traffic graph in KB/s
        public double ChartYMax
        {
            get
            {
                return _ChartYMax;
            }
            set
            {
                _ChartYMax = value;
                _graph.ChartAreas[0].AxisY.Maximum = 1.3 * value / 8000;
            }
        }


        [DefaultValue("1000"), Category("Appearance"), Description("Traffic speed refresh interval.")]
        private int _RefreshInterval = 1000;
        public int RefreshInterval
        {
            get
            {
                return _RefreshInterval;
            }
            set
            {
                _RefreshInterval = value;
            }
        }





        public void IncrementUploadCounter(int incrementValue)
        {
            UploadTotal += incrementValue;
        }

        public void IncrementDownloadCounter(int incrementValue)
        {
            DownloadTotal += incrementValue;
        }


        public void ResetTrafficCounter()
        {
            UploadTotal = 0;
            PrevUploadTotal = 0;
            DownloadTotal = 0;
            PrevDownloadTotal = 0;

            UploadSpeed = 0;
            DownloadSpeed = 0;

            _graph.Series[0].Points.Clear();
            _graph.Series[1].Points.Clear();
            _graph.Series[1].Points.Add(new System.Windows.Forms.DataVisualization.Charting.DataPoint(0, 0));
            DownloadXAxisCntr = 0;
            UploadXAxisCntr = 0;
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetTrafficCounter();
        }
    }
}
