using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FractalAss
{
    public partial class FractalAss : Form
    {
        private const int MAX = 256;      // max iterations
        private const double SX = -2.025; // start value real
        private const double SY = -1.125; // start value imaginary
        private const double EX = 0.6;    // end value real
        private const double EY = 1.125;  // end value imaginary
        private static int x1, y1, xs, ys, xe, ye;
        private static double xstart, ystart, xende, yende, xzoom, yzoom;
        private static bool action, rectangle, finished;
        private static float xy;
        private Bitmap picture, area;
        private Graphics g1, g2;
        private Cursor c1, c2;

        public object TextBox1 { get; private set; }

        private void FractalAss_Load(object sender, EventArgs e)
        {
           
        }
        private void ConstructFromResourceSaveAsJpeg(PaintEventArgs e)
        {
            // Construct a bitmap from the button image resource.
            Bitmap bmp1 = new Bitmap(typeof(Button), "Button.bmp");
            // Save the image as a Jpeg.
            bmp1.Save("c:\\button.Jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            // Construct a new image from the Jpeg file.
            Bitmap bmp2 = new Bitmap("c:\\button.Jpeg");
            // Draw the two images.
            e.Graphics.DrawImage(bmp1, new Point(10, 10));
            e.Graphics.DrawImage(bmp2, new Point(10, 40));
            // Dispose of the image files.
            bmp1.Dispose();
            bmp2.Dispose();
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)// New file button
        {
            omoboBox.Text = string.Empty;
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)// Open file button
        {
            openFileDialog1.Filter = "Text Document (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.Title = "Open";
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.RestoreDirectory = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                string text = System.IO.File.ReadAllText(file);
                omoboBox.Text = text;
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e) // Save File button
        {
            string buffer = omoboBox.Text;
            // Available file extensions
            saveFileDialog1.Filter = "Text Document (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.Title = "Save As";
            // Startup directory
            saveFileDialog1.InitialDirectory = @"C:\";
            // Restores the selected directory, next time
            saveFileDialog1.RestoreDirectory = false;
            // Display the Save file dialog.
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Get file name from the user.
                string name = saveFileDialog1.FileName;
                System.IO.File.WriteAllText(name, buffer);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) //Exit button
        {
            {
                Application.Exit();
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show the color dialog.
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                // Set form background to the selected color.
                this.BackColor = colorDialog1.Color;
            }
        }

        public FractalAss()
        {
            InitializeComponent();
            DoubleBuffered = true;
            init();
            start();
        }
        public struct HSBColor
        {
            float h;
            float s;
            float b;
            int a;

            public HSBColor(float h, float s, float b)
            {
                this.a = 0xff;
                this.h = Math.Min(Math.Max(h, 0), 255);
                this.s = Math.Min(Math.Max(s, 0), 255);
                this.b = Math.Min(Math.Max(b, 0), 255);
            }

            public HSBColor(int a, float h, float s, float b)
            {
                this.a = a;
                this.h = Math.Min(Math.Max(h, 0), 255);
                this.s = Math.Min(Math.Max(s, 0), 255);
                this.b = Math.Min(Math.Max(b, 0), 255);
            }

            public float H
            {
                get { return h; }
            }

            public float S
            {
                get { return s; }
            }

            public float B
            {
                get { return b; }
            }

            public int A
            {
                get { return a; }
            }

            public Color Color
            {
                get
                {
                    return FromHSB(this);
                }
            }

            public static Color FromHSB(HSBColor hsbColor)
            {
                float r = hsbColor.b;
                float g = hsbColor.b;
                float b = hsbColor.b;
                if (hsbColor.s != 0)
                {
                    float max = hsbColor.b;
                    float dif = hsbColor.b * hsbColor.s / 255f;
                    float min = hsbColor.b - dif;

                    float h = hsbColor.h * 360f / 255f;

                    if (h < 60f)
                    {
                        r = max;
                        g = h * dif / 60f + min;
                        b = min;
                    }
                    else if (h < 120f)
                    {
                        r = -(h - 120f) * dif / 60f + min;
                        g = max;
                        b = min;
                    }
                    else if (h < 180f)
                    {
                        r = min;
                        g = max;
                        b = (h - 120f) * dif / 60f + min;
                    }
                    else if (h < 240f)
                    {
                        r = min;
                        g = -(h - 240f) * dif / 60f + min;
                        b = max;
                    }
                    else if (h < 300f)
                    {
                        r = (h - 240f) * dif / 60f + min;
                        g = min;
                        b = max;
                    }
                    else if (h <= 360f)
                    {
                        r = max;
                        g = min;
                        b = -(h - 360f) * dif / 60 + min;
                    }
                    else
                    {
                        r = 0;
                        g = 0;
                        b = 0;
                    }
                }

                return Color.FromArgb
                    (
                        hsbColor.a,
                        (int)Math.Round(Math.Min(Math.Max(r, 0), 255)),
                        (int)Math.Round(Math.Min(Math.Max(g, 0), 255)),
                        (int)Math.Round(Math.Min(Math.Max(b, 0), 255))
                        );
            }

        }




        private void FractalAss_MouseUp(object sender, MouseEventArgs e)
        {
            int z, w;

            //  e.consume();
            if (action)
            {
                xe = e.X;
                ye = e.Y;
                if (xs > xe)
                {
                    z = xs;
                    xs = xe;
                    xe = z;
                }
                if (ys > ye)
                {
                    z = ys;
                    ys = ye;
                    ye = z;
                }
                w = (xe - xs);
                z = (ye - ys);
                if ((w < 2) && (z < 2))
                {
                    initvalues();
                }
                else
                {
                    if (((float)w > (float)z * xy))
                    {
                        ye = (int)((float)ys + (float)w / xy);
                    }
                    else
                    {
                        xe = (int)((float)xs + (float)z * xy);
                    }
                    xende = xstart + xzoom * (double)xe;
                    yende = ystart + yzoom * (double)ye;
                    xstart += xzoom * (double)xs;
                    ystart += yzoom * (double)ys;
                }
                xzoom = (xende - xstart) / (double)x1;
                yzoom = (yende - ystart) / (double)y1;
                g2.Clear(Color.Transparent);
                mandelbrot();
                rectangle = false;
                Refresh();
            }
        }

        private void FractalAss_MouseMove(object sender, MouseEventArgs e)
        {
            // e.consume();
            if (action == true && e.Button == MouseButtons.Left)
            {
                xe = e.X;
                ye = e.Y;
                //rectangle = true;
                drawRectangle();
                //Refresh();

            }
        }


        public void init() // all instances will be prepared
        {

            //setSize(640,480);
            finished = false;
            //addMouseListener(this);
            //addMouseMotionListener(this);
            c1 = Cursors.WaitCursor;
            c2 = Cursors.Cross;
            x1 = Width;
            y1 = Height;
            xy = (float)x1 / (float)y1;
            picture = new Bitmap(x1, y1);
            g1 = Graphics.FromImage(picture);
            area = new Bitmap(x1, y1);
            g2 = Graphics.FromImage(area);
            finished = true;
        }

        public virtual void destroy() // delete all instances
        {
            if (finished)
            {
                // removeMouseListener(this);
                //removeMouseMotionListener(this);
                picture = null;
                g1 = null;
                c1 = null;
                c2 = null;
                GC.Collect(); // garbage collection
            }
        }

        public void start()
        {
            action = false;
            rectangle = false;
            initvalues();
            xzoom = (xende - xstart) / (double)x1;
            yzoom = (yende - ystart) / (double)y1;
            mandelbrot();
        }

        public void stop()
        {
        }

        protected override void OnPaint(PaintEventArgs g)
        {
            g.Graphics.DrawImageUnscaled(picture, 0, 0);
            g.Graphics.DrawImageUnscaled(area, 0, 0);
        }

        public void update(Graphics g)
        {

            g.DrawImage(picture, 0, 0);
            if (rectangle)
            {
                //g.Color = Color.white;
                Pen djpen = new Pen(Color.Green);
                if (xs < xe)
                {
                    if (ys < ye)
                    {
                        g.DrawRectangle(djpen, xs, ys, (xe - xs), (ye - ys));
                    }
                    else
                    {
                        g.DrawRectangle(djpen, xs, ye, (xe - xs), (ys - ye));
                    }
                }
                else
                {
                    if (ys < ye)
                    {
                        g.DrawRectangle(djpen, xe, ys, (xs - xe), (ye - ys));
                    }
                    else
                    {
                        g.DrawRectangle(djpen, xe, ye, (xs - xe), (ys - ye));
                    }
                }
            }
        } //end update()

        public void drawRectangle()
        {
            //Graphics g = Graphics.FromImage(picture);
            g2.Clear(Color.Transparent);
            Pen p = new Pen(Color.Blue);
            ///g.DrawImage(picture, 0, 0, this);
            if (rectangle)
            {
                ///g.setColor(Color.white);
                if (xs < xe)
                {
                    if (ys < ye) g2.DrawRectangle(p, xs, ys, (xe - xs), (ye - ys));
                    else g2.DrawRectangle(p, xs, ye, (xe - xs), (ys - ye));
                }
                else
                {
                    if (ys < ye) g2.DrawRectangle(p, xe, ys, (xs - xe), (ye - ys));
                    else g2.DrawRectangle(p, xe, ye, (xs - xe), (ys - ye));
                }
            }
            Refresh();
        }



        private void mandelbrot() // calculate all points
        {
            Color col = Color.Blue;
            Pen omobopen = new Pen(col);
            int x, y;
            float h, b, alt = 0.0f;

            action = false;
            this.Cursor = c1;
            omoboBox.Text = "Mandelbrot-Set will be produced - please wait...";
            for (x = 0; x < x1; x += 2)
            {
                for (y = 0; y < y1; y++)
                {
                    h = pointcolour(xstart + xzoom * (double)x, ystart + yzoom * (double)y); // color value
                    if (h != alt)
                    {
                        b = 1.0f - h * h; // brightnes
                                          ///djm added
                                          ///HSBcol.fromHSB(h,0.8f,b); //convert hsb to rgb then make a Java Color
                                          ///Color col = new Color(0,HSBcol.rChan,HSBcol.gChan,HSBcol.bChan);
                                          ///g1.setColor(col);
                        //djm end
                        //djm added to convert to RGB from HSB

                        //g1.Color = Color.getHSBColor(h, 0.8f, b);
                        //djm test
                        // omoboBox.Text = "x1 * y1";
                        HSBColor Ebibos = new HSBColor(h * 255, 0.8f * 255, b * 255);
                        // int red = col.Red;
                        //int green = col.Green;
                        //int blue = col.Blue;
                        //djm 
                        alt = h;
                        omobopen = new Pen(Ebibos.Color);
                    }
                    g1.DrawLine(omobopen, x, y, x + 1, y);
                }
            }
            omoboBox.Text = "Mandelbrot-Set ready - please select zoom area with pressed mouse.";
            this.Cursor = c2;
            action = true;
        }

        private float pointcolour(double xwert, double ywert) // color value from 0.0 to 1.0 by iterations
        {
            double r = 0.0, i = 0.0, m = 0.0;
            int j = 0;

            while ((j < MAX) && (m < 4.0))
            {
                j++;
                m = r * r - i * i;
                i = 2.0 * r * i + ywert;
                r = m + xwert;
            }
            return (float)j / (float)MAX;
        }

        private void initvalues() // reset start values
        {
            xstart = SX;
            ystart = SY;
            xende = EX;
            yende = EY;
            if ((float)((xende - xstart) / (yende - ystart)) != xy)
            {
                xstart = xende - (yende - ystart) * (double)xy;
            }
        }
        private void FractalAss_MouseDown(object sender, MouseEventArgs e)
        {
            //e.consume();
            //paint(g2);
            rectangle = true;
            if (action)
            {
                xs = e.X;
                ys = e.Y;
            }
        }

        }
    }

