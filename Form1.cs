using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Cuda;

namespace GUItest_0811
{
    public partial class MatchTemplate : Form
    {
        public MatchTemplate()
        {
            InitializeComponent();
        }


        Stopwatch GPUtime1 = new Stopwatch();
        Stopwatch GPUtime2 = new Stopwatch();
        Stopwatch GPUtime3 = new Stopwatch();

        private void Form1_Load(object sender, EventArgs e)
        {
            
            Image<Bgr, byte> sourceimage = new Image<Bgr, byte>(@"C:\\MatchSource.jpg");
            Image<Bgr, byte> template = new Image<Bgr, byte>(@"C:\\Template.jpg");
            Image<Bgr, byte> imagetoshow = sourceimage.Copy();

            
           

            GpuMat gsourceimage = new GpuMat(sourceimage);
            GpuMat gtemplate = new GpuMat(template);
            GpuMat gimagetoshow = new GpuMat(imagetoshow);

            GPUtime1.Start();

            double gpuminval = 0, gpumaxval = 0;
            Point gpuminloc = Point.Empty, gpumaxloc = Point.Empty;

            using (CudaTemplateMatching buffer = new CudaTemplateMatching(DepthType.Cv8U, 3, TemplateMatchingType.CcorrNormed))

                buffer.Match(gsourceimage, gtemplate, gimagetoshow);

            CudaInvoke.MinMaxLoc(gimagetoshow, ref gpuminval, ref gpumaxval, ref gpuminloc, ref gpumaxloc, null);

            Console.WriteLine("minval:" + gpuminval + "maxval:" + gpumaxval + "maxloc:(" + gpumaxloc.X + "," + gpumaxloc.Y + ")" + "minloc:(" + gpuminloc.X + "," + gpuminloc.Y + ")");

            GPUtime1.Stop();

            if (gpumaxval >0.9)
            {
                Rectangle match = new Rectangle(gpumaxloc, template.Size);

                imagetoshow.Draw(match, new Bgr(Color.Red), 15);

                imageBox1.Image = imagetoshow;

                textBox1.Text ="Success" + Environment.NewLine + "정확도:" +gpumaxval.ToString("0.###")
                                + Environment.NewLine + "소요시간:"+ GPUtime1.ElapsedMilliseconds.ToString() + "ms";
                
            }
            else
            {
                textBox1.AppendText("Fail");
            }
            
            imagetoshow.Dispose();
            template.Dispose();
            gtemplate.Dispose();
            gimagetoshow.Dispose();
           
            
            //try
            //{
            //CvInvoke.Imshow("test", imagetoshow);
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}

            ////////////////////////////2nd Camera////////////////////////////////////

            template = new Image<Bgr, byte>(@"C:\\Template.jpg");
            imagetoshow = sourceimage.Copy();

            gtemplate = new GpuMat(template);
            gimagetoshow = new GpuMat(imagetoshow);

            GPUtime2.Start();

            gpuminval = 0;
            gpumaxval = 0;
            gpuminloc = Point.Empty;
            gpumaxloc = Point.Empty;

            using (CudaTemplateMatching buffer = new CudaTemplateMatching(DepthType.Cv8U, 3, TemplateMatchingType.CcorrNormed))

                buffer.Match(gsourceimage, gtemplate, gimagetoshow);

            CudaInvoke.MinMaxLoc(gimagetoshow, ref gpuminval, ref gpumaxval, ref gpuminloc, ref gpumaxloc, null);

            Console.WriteLine("minval:" + gpuminval + "maxval:" + gpumaxval + "maxloc:(" + gpumaxloc.X + "," + gpumaxloc.Y + ")" + "minloc:(" + gpuminloc.X + "," + gpuminloc.Y + ")");

            GPUtime2.Stop();

            if (gpumaxval > 0.9)
            {
                Rectangle match = new Rectangle(gpumaxloc, template.Size);

                imagetoshow.Draw(match, new Bgr(Color.Red), 15);

                imageBox2.Image = imagetoshow;

                textBox2.Text = "Success" + Environment.NewLine + "정확도:"+ gpumaxval.ToString("0.###")
                                + Environment.NewLine + "소요시간:"+ GPUtime2.ElapsedMilliseconds.ToString() + "ms";

            }
            else
            {
                textBox2.AppendText("Fail");
            }
            //CvInvoke.cvReleaseImage(ref imagetoshow);

            imagetoshow.Dispose();
            template.Dispose();
            gtemplate.Dispose();
            gimagetoshow.Dispose();

            ////////////////////////////////////3rd Camera///////////////////////////////////

            template = new Image<Bgr, byte>(@"C:\\Template.jpg");
            imagetoshow = sourceimage.Copy();

            gtemplate = new GpuMat(template);
            gimagetoshow = new GpuMat(imagetoshow);

            GPUtime3.Start();

            gpuminval = 0;
            gpumaxval = 0;
            gpuminloc = Point.Empty;
            gpumaxloc = Point.Empty;

            using (CudaTemplateMatching buffer = new CudaTemplateMatching(DepthType.Cv8U, 3, TemplateMatchingType.CcoeffNormed))

                buffer.Match(gsourceimage, gtemplate, gimagetoshow);

            CudaInvoke.MinMaxLoc(gimagetoshow, ref gpuminval, ref gpumaxval, ref gpuminloc, ref gpumaxloc, null);

            Console.WriteLine("minval:" + gpuminval + "maxval:" + gpumaxval + "maxloc:(" + gpumaxloc.X + "," + gpumaxloc.Y + ")" + "minloc:(" + gpuminloc.X + "," + gpuminloc.Y + ")");

            GPUtime3.Stop();

            if (gpumaxval > 0.9)
            {
                Rectangle match = new Rectangle(gpumaxloc, template.Size);

                imagetoshow.Draw(match, new Bgr(Color.Red), 15);

                imageBox3.Image = imagetoshow;

                textBox3.Text = "Success" + Environment.NewLine + "정확도:"+ gpumaxval.ToString("0.###")
                                + Environment.NewLine +"소요시간:"+ GPUtime2.ElapsedMilliseconds.ToString() + "ms";

            }
            else
            {
                textBox3.AppendText("Fail");
            }
            //CvInvoke.cvReleaseImage(ref imagetoshow);

            imagetoshow.Dispose();
            template.Dispose();
            gtemplate.Dispose();
            gimagetoshow.Dispose();


            //int i = 0;
            //Image<Bgr, byte>[] ttt = new Image<Bgr, byte>[3];
            //Mat[] ww = new Mat[5];

            //for(i = 0; i<3; i++)
            //{ 
            //ttt[i] =[@"C:\\template2.jpg", @"C:\\template3.jpg", @"C:\\template4.jpg"];
            //ww[i] = [1, 2, 3, 4, 5];
            //}


        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
