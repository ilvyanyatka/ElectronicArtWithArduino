using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sharer;
using Sharer.Command;

namespace ElectronicArt1
{
    public enum DrawType
    {
        Line,
        Arc
    }

    public enum ExecState
    {
        Stop,
        Go
    }
    public partial class ArtForm : Form
    {
        private bool fullScreen = true;
        private Point startPoint = new Point(0, 0);
        double angleInRadians = 110;
        double initialAngleInRadians = 110;
        int lineWidth = 3;
        const int INITIAL_LINE_LENGTH = 10;
        int lineLength = INITIAL_LINE_LENGTH;
        DrawType drawType = DrawType.Line;
        ExecState prevExecState = ExecState.Stop;
        ExecState currentExecState = ExecState.Stop;
        bool clearState = false;
        bool prevClearState = false;
        bool saveState = false;
        bool prevSaveState = false;
        int drawRate = 500;
        bool readingData = false;

        List<string> varNames = new List<string>() {
            "ClearButtonState",
            "LineTypeButtonState",
            "ExecButtonState",
            "LineWidthValue",
            "AngleValue"};

        Thread drawingThread = null;
        public ArtForm()
        {
            InitializeComponent();
        }

        #region Form event handlers
        private void Form1_Load(object sender, EventArgs e)
        {
            ArduinoSettings.Connection.UserDataReceived += _connection_UserDataReceived;
            
            GoFullscreen(fullScreen);
            SetCenter();
            UpdateLabels();
            tmrUnqueue.Start();
        }


        private void GoFullscreen(bool fullscreen)
        {
            fullScreen = fullscreen;
            if (fullscreen)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Bounds = Screen.PrimaryScreen.Bounds;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            }

        }

        private void SetCenter()
        {
            Rectangle scrBounds = this.Bounds;
            startPoint.X = scrBounds.X + scrBounds.Width / 2;
            startPoint.Y = scrBounds.Y + scrBounds.Height / 2;
            lineLength = INITIAL_LINE_LENGTH;
        }

        private bool PointInScreenBounds(Point p)
        {
            bool result = true;
            Rectangle scrBounds = this.Bounds;

            result = p.X > scrBounds.X
                && p.X < scrBounds.Right
                && p.Y > scrBounds.Y
                && p.Y < scrBounds.Bottom;
            return result;

        }

        private bool PointInDoubleScreenBounds(Point p)
        {
            bool result = true;
            Rectangle scrBounds = this.Bounds;

            result = p.X > scrBounds.X - scrBounds.Right
                && p.X < scrBounds.Right * 2
                && p.Y > scrBounds.Y - scrBounds.Bottom
                && p.Y < scrBounds.Bottom * 2;
            return result;

        }
        public Point DrawLineFromPoint(Point p, double angle, int length, Color color, int width, DrawType drawType)
        {

            //cosA = x/h
            //AND sinX = y/h


            // Create pen.
            Pen pen = new Pen(color, width);

            Point point2 = new Point((int)Math.Floor(startPoint.X + Math.Cos(angle) * lineLength), (int)Math.Floor(startPoint.Y + Math.Sin(angle) * length));

            // Draw line to screen.



            if (!this.IsDisposed)
            {
                Graphics g = this.CreateGraphics();
                if (g != null)
                {

                    switch (drawType)
                    {
                        case DrawType.Arc:
                            Point[] bezierPoints = GetBezier(p, point2);
                            g.DrawBezier(pen, bezierPoints[0], bezierPoints[1], bezierPoints[2], bezierPoints[3]);
                            break;
                        case DrawType.Line:
                            g.DrawLine(pen, p, point2);
                            break;
                    }
                }
            }


            return point2;
        }
        private Rectangle GetRectangle(Point p1, Point p2)
        {
            Point p = new Point();
            Size s = new Size();
            p.X = (p1.X > p2.X) ? p1.X : p2.X;
            p.Y = (p1.Y > p2.Y) ? p1.Y : p2.Y;

            s.Width = Math.Abs(p1.X - p2.X) + 1;
            s.Height = Math.Abs(p1.Y - p2.Y) + 1;

            return new Rectangle(p, s);

        }

        private Point[] GetBezier(Point p1, Point p2)
        {
            Point[] result = new Point[4];
            result[0] = (p1);
            Point pNext = new Point(p1.X, p2.Y);
            result[1] = pNext;
            pNext = new Point(p2.X, p1.Y);
            result[2] = p2; // pNext;
            result[3] = p2;
            return result;

        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            fullScreen = !fullScreen;
            GoFullscreen(fullScreen);
        }


        private void DrawGeometricFigure()
        {


            Color color = Color.Yellow;

            while (true)
            {
                Point p = startPoint;
                while (PointInScreenBounds(p))
                {

                    Debug.WriteLine("color: " + color.ToString());
                    Debug.WriteLine("angle in radians: " + angleInRadians.ToString());
                    Debug.Write("angle in degrees: "); Debug.WriteLine(angleInRadians * 180 / Math.PI);
                    Debug.WriteLine("length: " + lineLength.ToString());
                    Thread.Sleep(drawRate);

                    p = DrawLineFromPoint(p, angleInRadians, lineLength, color, lineWidth, drawType);
                    lineLength += (int)Math.Floor(lineWidth * 1.5);

                    angleInRadians = angleInRadians + initialAngleInRadians;
                    while (angleInRadians > Math.PI * 2)
                    {
                        angleInRadians = angleInRadians - Math.PI * 2;
                    }
                    while (angleInRadians < 0)
                    {
                        angleInRadians = angleInRadians + Math.PI * 2;
                    }
                    Random random = new Random();
                    int r = random.Next(0, 255);
                    int g = random.Next(0, 255);
                    int b = random.Next(0, 255);
                    color = Color.FromArgb(r, g, b);

                }
                SetCenter();

                p = startPoint;
            }
        }


        /// <summary>
        /// Popup a message box on exception
        /// </summary>
        private void handleException(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #region User Messages
        /// <summary>
        /// Enqueue received user message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _connection_UserDataReceived(object sender, Sharer.UserData.UserDataReceivedEventArgs e)
        {
            try
            {
                var str = e.GetReader().ReadString();
                lock (_receivedData) _receivedData.Enqueue(str);
            }
            catch (Exception ex)
            {
                handleException(ex);
            }
        }

        /// <summary>
        /// Enqueued user message. We use a queue to separate serial thread from GUI
        /// </summary>
        private readonly Queue<string> _receivedData = new Queue<string>();

        /// <summary>
        /// Dequeue and display user messages if any
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrUnqueue_Tick(object sender, EventArgs e)
        {
            //while (_receivedData.Any())
            //{
            if (!readingData)
            {
                readingData = true;
                ReadArduinoData();
                UpdateLabels();
                Execute();
                readingData = false;
            }
            //}

        }

        private void Execute()
        {
            try
            {
                // process save button, just in case draw rate is very slow we do it before execution
                if (saveState != prevSaveState)
                {

                    // process start (in the beginning, or after stop was pressed)
                    if (saveState)
                    {
                        SaveScreenshot();
                    }
                }

                // process start/go button
                if (currentExecState != prevExecState) {

                    // process start (in the beginning, or after stop was pressed)
                    if (currentExecState == ExecState.Go)
                    {
                        if (drawingThread == null ||
                            !(drawingThread.ThreadState == System.Threading.ThreadState.Suspended) ||
                            !drawingThread.IsAlive)
                        {
                            initialAngleInRadians = angleInRadians;
                            drawingThread = new Thread(DrawGeometricFigure);
                            drawingThread.Start();
                        }
                        else
                        {
                            if (drawingThread.ThreadState != System.Threading.ThreadState.Running)
                                drawingThread.Resume();
                        }
                    }

                    // process stop
                    if (currentExecState == ExecState.Stop)
                    {
                        if (drawingThread != null)
                        {
                            drawingThread.Abort();
                        }
                    }

                }

                // process clear
                if (clearState != prevClearState)
                {
                    if (clearState)
                    {
                        Graphics g = this.CreateGraphics();
                        g.Clear(this.BackColor);
                    }
                }

            }
            catch (ThreadAbortException ex)
            {
                lbError.Text = ("drawing thread aborted");
                drawingThread = null;
            }
        }

        private void ReadArduinoData()
        {
            /*
            "ClearButtonState", 
            "SaveButtonState",
            "LineTypeButtonState",
            "ExecButtonState",
            "LineWidthValue",
            "AngleValue",
            "DrawRateValue"
        */
            try
            {
                int temp = 0;
                int.TryParse(ArduinoSettings.Connection.ReadVariable("LineTypeButtonState").ToString(), out temp);
                drawType = temp == 0 ? DrawType.Line : DrawType.Arc;

                prevExecState = currentExecState;
                int.TryParse(ArduinoSettings.Connection.ReadVariable("ExecButtonState").ToString(), out temp);
                currentExecState = temp == 1 ? ExecState.Stop : ExecState.Go;

                prevClearState = clearState;
                int.TryParse(ArduinoSettings.Connection.ReadVariable("ClearButtonState").ToString(), out temp);
                clearState = temp == 0;

                prevSaveState = saveState;
                int.TryParse(ArduinoSettings.Connection.ReadVariable("SaveButtonState").ToString(), out temp);
                saveState = temp == 0;

                int.TryParse(ArduinoSettings.Connection.ReadVariable("LineWidthValue").ToString(), out lineWidth);

                int.TryParse(ArduinoSettings.Connection.ReadVariable("AngleValue").ToString(), out temp);
                //angleInRadians = temp * Math.PI / 180;
                initialAngleInRadians = temp * Math.PI / 180;

                int.TryParse(ArduinoSettings.Connection.ReadVariable("DrawRateValue").ToString(), out drawRate);
            }
            catch (Exception ex) { }
        }

        private void UpdateLabels()
        {
            lbDrawTypeInfo.Text = drawType.ToString();
            lbWidthInfo.Text = lineWidth.ToString();
            // convert from radians to degrees
            lbAngleInfo.Text = (initialAngleInRadians * 180 / Math.PI).ToString();
            lbDrawRate.Text = (((double)drawRate)/1000).ToString() + " s";
        }
        #endregion

        private void ArtForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (drawingThread != null)
            {
                drawingThread.Abort();
                drawingThread = null;
            }
        }

        private void ArtForm_KeyDown(object sender, KeyEventArgs e)
        {

        }

        // System.Windows.Forms.Internal.IntUnsafeNativeMethods
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "GetCurrentObject", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr IntGetCurrentObject(HandleRef hDC, int uObjectType);

        /// <summary>
        /// saves the screenshot with datetime stamp
        /// </summary>
        private void SaveScreenshot()
        {
            Graphics g = this.CreateGraphics();
            // NOTE: You cannot use the graphics object before ReleaseHdc is called.
            IntPtr hdc = g.GetHdc();
            Bitmap bitmap = null;
            try
            {
                // This is a HBITMAP, which is the actual buffer that is being drawn to by hdc.
                IntPtr hbitmap = IntGetCurrentObject(new HandleRef(null, hdc), 7 /*OBJ_BITMAP*/);
                bitmap = Image.FromHbitmap(hbitmap);
                // file name is time stamp
                string bitmapFilename = DateTime.Now.ToString("MM-dd-yyyy hh-mm-ss") + ".png";
                bitmapFilename = ArduinoSettings.ImageFolder + "\\" + bitmapFilename;
                bitmap.Save(bitmapFilename, ImageFormat.Png);
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                bitmap.Dispose();
                g.ReleaseHdc(hdc);
            }
            
        }

        private void ArtForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            // if key is S - save the screenshot
            if (e.KeyChar == 's' || e.KeyChar == 'S')
            {
                SaveScreenshot();
            }
        }
    }

}
