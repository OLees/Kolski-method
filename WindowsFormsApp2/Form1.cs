using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IronXL;


namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private static float[,] MatrixMultiplication(float[,] A, float[,] B)
        {
            int ARows = A.GetLength(0), AColumns = A.GetLength(1);
            int BRows = B.GetLength(0), BColumns = B.GetLength(1);

            if (AColumns != BRows)
                throw new Exception("Нельзя перемножить данные матрицы");

            float[,] matrix = new float[ARows, BColumns];
            for (int rowA = 0; rowA < ARows; rowA++)
                for (int columnB = 0; columnB < BColumns; columnB++)
                    for (int columnA = 0; columnA < AColumns; columnA++)
                        matrix[rowA, columnB] += A[rowA, columnA] * B[columnA, columnB];
            return matrix;
        }


        List<double> Time = new List<double>();
        List<double> LoadingImp = new List<double>();
        List<double> ReflectedImp = new List<double>();//двигаем 2
        List<double> PassedImp = new List<double>();//двигаем 1

        List<PointF> Deformation = new List<PointF>();
        List<PointF> Tension = new List<PointF>();


        Pen pen1 = new Pen(Color.Blue, 2), pen2 = new Pen(Color.Red, 2), pen3 = new Pen(Color.Green, 2), pen_ax = new Pen(Color.Gray, 2);

        Graphics user_Graphics, user_gr1;

        Font axFont = new Font("Tahoma", 10);
        Font StrFont = new Font("Times New Roman", 12);
        SolidBrush drawBrush = new SolidBrush(Color.Black);
        SolidBrush axBrush = new SolidBrush(Color.Gray);
        StringFormat drawFormat = new StringFormat();

        private float dx;
        private float dx1;

        double x0, y0, x0_, y0_;

        double Coef_Imp;
        double Time_Coef;
        int Points;

        float new_Time_coef, new_Imp_coef;

        Bitmap canvas, canvas1;

        WorkBook workbook = WorkBook.Load("Экспериментальные данные.xls");

        List<WorkSheet> worksheet = new List<WorkSheet>();


        private float[,] CreateTransformedPoints1()  
        {
            float dx_ = (float)(dx * 2);
            int Ind = Time.Count();

            float[,] temp = new float[3, Ind];

           int i;

            for (i = 0; i < Ind; i++)
            {
                temp[0, i] = (float)(x0 + Time[i] + dx_);
                temp[1, i] = (float)PassedImp[i];
                temp[2, i] = 1;
            }


            float[,] P = new float[,]
            {
                { 1, 0, 0},
                { 0, 1, 0},
                { dx_, 0, 1},
            };

            float[,] Rx = new float[,]
            {
                { 1, 0, 0},
                { 0, 1, 0},
                { 0, 0, 1},
            };

            float[,] T = MatrixMultiplication(Rx, P);

            return MatrixMultiplication(T, temp);
        }



        private float[,] CreateTransformedPoints2()
        {
            float dx_ = (float)(dx1 * 2.25);
            int Ind = Time.Count();

            float[,] temp = new float[3, Ind];
            int i;

            for (i = 0; i < Ind; i++) {
                temp[0, i] = (float)(x0 + Time[i] + dx_);
                temp[1, i] = (float)ReflectedImp[i];
                temp[2, i] = 1;
            }

            float[,] P = new float[,]
            {
                { 1, 0, 0},
                { 0, 1, 0},
                { dx_, 0, 1},
            };

            float[,] Rx = new float[,]
            {
                { 1, 0, 0},
                { 0, 1, 0},
                { 0, 0, 1},
            };

            float[,] T = MatrixMultiplication(Rx, P);

            return MatrixMultiplication(T, temp);
        }

        public Form1()
        {
            InitializeComponent();

            canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            user_Graphics = Graphics.FromImage(canvas);
            x0 = 30;
            y0 = pictureBox1.Height / 2;

            canvas1 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            user_gr1 = Graphics.FromImage(canvas1);
            x0_ = 30;
            y0_ = pictureBox2.Height / 2;

            dx =  trackBar1.Value - 100;
            dx1 = trackBar2.Value - 100;

            LoadImpulse();
            Axis();
            Draw();
        }


        void Axis() {

            user_Graphics.FillRectangle(Brushes.White, 0, 0, pictureBox1.Width, pictureBox1.Height);

            user_Graphics.DrawLine(pen_ax, (float)x0, (float)y0, pictureBox1.Width, (float)y0);
            user_Graphics.DrawLine(pen_ax, (float)x0, 0, (float)x0, pictureBox1.Height);

            double d1 = pictureBox1.Height / 2 - LoadingImp.Max() * Coef_Imp,
                    d2 = (pictureBox1.Height - LoadingImp.Max() * Coef_Imp) * 0.5;
            double d3 = pictureBox1.Height / 2 + LoadingImp.Max() * Coef_Imp,
                d4 = (pictureBox1.Height + LoadingImp.Max() * Coef_Imp) * 0.5;

            double Znach_y = Math.Round(LoadingImp.Max() * 100) / 100;

            user_Graphics.DrawString(Znach_y.ToString(), axFont, axBrush, 0, (float)d1, drawFormat);

            user_Graphics.DrawString((Znach_y / 2).ToString(), axFont, axBrush, 0, (float)d2, drawFormat);

            user_Graphics.DrawString((-Znach_y * 0.5).ToString(), axFont, axBrush, 0, (float)d4, drawFormat);

            user_Graphics.DrawString((-Znach_y).ToString(), axFont, axBrush, 0, (float)d3, drawFormat);

            user_Graphics.DrawString((-Znach_y).ToString(), axFont, axBrush, 0, (float)d3, drawFormat);

            pictureBox1.Image = canvas;
        }


        private void Change_dx(object sender, EventArgs e)
        {
            dx = trackBar1.Value - 100;
            Draw();
        }

        private void Strip_Save_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog() { Filter = @"PNG|*.png" })
            {

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    canvas.Save(dialog.FileName);

                    Invalidate();
                }
            }
        }

        private void Change_dx1(object sender, EventArgs e)
        {
            dx1 = trackBar2.Value - 100;
            Draw();
        }

        private void Strip_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void LoadImpulse() {
            Points = 2;
            double temp;
            WorkSheet worksheet_temp = workbook.WorkSheets.First();
            while (worksheet_temp[$"G{Points}"].IsEmpty == false)
            {
                Time.Add(Convert.ToDouble(worksheet_temp[$"G{Points}"].Value)); 

                temp = Convert.ToDouble(worksheet_temp[$"H{Points}"].Value);
                if (temp <= 0)
                {
                    LoadingImp.Add(temp);
                    ReflectedImp.Add(0);
                }
                else 
                {
                    LoadingImp.Add(0);
                    ReflectedImp.Add(temp);
                }

                temp = Convert.ToDouble(worksheet_temp[$"I{Points}"].Value);

                PassedImp.Add(temp);

                Points++;
            }
            Coef_Imp = -(pictureBox1.Height - 50) / (Math.Round(LoadingImp.Min() * 10000) / 5000);
            Time_Coef =  (pictureBox1.Width - 20) / Time.Max();
        }



        private PointF GetPoint(float x, float y) => new PointF((float)(x0 + x * Time_Coef), (float)(y0 - y * Coef_Imp));

        private PointF GetPoint_1(float x, float y) => new PointF((float)(x0_ + x * new_Time_coef), (float)(y0_ - y * new_Imp_coef));

        private PointF GetF(float x, float y) => new PointF((float)x, (float)y);

        private void Draw()
        {
            Axis();

            float[,] transformedPoints1 = CreateTransformedPoints1();
            float[,] transformedPoints2 = CreateTransformedPoints2();

            List<PointF> p0 = new List<PointF>();
            List<PointF> p1 = new List<PointF>();
            List<PointF> p2 = new List<PointF>();

            p0.Add(GetPoint(transformedPoints1[0, 0], transformedPoints1[1, 0]));
            p1.Add(GetPoint(transformedPoints2[0, 0], transformedPoints2[1, 0]));
            p2.Add(GetPoint((float)Time[0], (float)LoadingImp[0]));

            for (int i = 1; i < Time.Count(); i++)
            {
                p0.Add(GetPoint(transformedPoints1[0, i], transformedPoints1[1, i]));
                p1.Add(GetPoint(transformedPoints2[0, i], transformedPoints2[1, i]));
                p2.Add(GetPoint((float)Time[i], (float)LoadingImp[i]));

                user_Graphics.DrawLine(pen2, p0[i - 1], p0.Last());
                user_Graphics.DrawLine(pen1, p1[i - 1], p1.Last());
                user_Graphics.DrawLine(pen1, p2[i - 1], p2.Last());
            }

            pictureBox1.Image = canvas;
        }


        private void Kolski_Check_Click(object sender, EventArgs e)
        {
            float[,] transformedPoints1 = CreateTransformedPoints1();
            float[,] transformedPoints2 = CreateTransformedPoints2();

            List<PointF> temp1 = new List<PointF>();
            List<PointF> temp2 = new List<PointF>();

            List<PointF> op = new List<PointF>();

            float Deform_max = 0;
            float Time_max = 0;

            int i, j;

            for (i = 0; i < Time.Count(); i++)
            {
                if (transformedPoints1[0, i] >= x0)
                {
                    temp1.Add(GetF(transformedPoints1[0, i], transformedPoints1[1, i]));
                    if (transformedPoints1[0, i] > Time_max)
                        Time_max = transformedPoints1[0, i];
                    if (transformedPoints1[1, i] > Deform_max)
                        Deform_max = transformedPoints1[1, i];
                }

                if (transformedPoints2[0, i] >= x0)
                    temp2.Add(GetF(transformedPoints2[0, i], transformedPoints2[1, i]));
            }

            new_Imp_coef = (float)((pictureBox2.Height - 30) / Deform_max)/500000;
            new_Time_coef = (float)((pictureBox2.Width - 10) / Time_max);

            Tension = new List<PointF>(temp1);
            Deformation = new List<PointF>(temp2);

            op.Add(GetPoint(temp2[0].X, -temp2[0].Y + temp1[0].Y));

            for (j = 1; j < Math.Min(temp1.Count(), temp2.Count()); j++) {

                op.Add(GetPoint(temp2[j].X, -temp2[j].Y + temp1[j].Y));

                user_Graphics.DrawLine(pen3, op[j - 1], op.Last());
            }

            pictureBox1.Image = canvas;
        }


        float Integral(int index, List<PointF> op)
        {
            float temp = 0;
            for (int k = 1; k < 10000; k++) {
                temp += (float)(op[(int)index * (k - 1) / 10000].Y + op[(int)index * k / 10000].Y) * op[index].X / 10000;
            }

            return temp;
        }

        private void Deformation_Draw_Click(object sender, EventArgs e)
        {
            List<PointF> op = new List<PointF>();

            user_gr1.FillRectangle(Brushes.White, 0, 0, pictureBox2.Width, pictureBox2.Height);

            user_gr1.DrawLine(pen_ax, (float)x0_, (float)y0_, pictureBox2.Width, (float)y0_);
            user_gr1.DrawLine(pen_ax, (float)x0_, 0, (float)x0_, pictureBox2.Height);

            user_gr1.DrawString("Strain", StrFont, axBrush, pictureBox2.Width / 2 - 12, 6, drawFormat);
            user_gr1.DrawString("0", axFont, axBrush, 0, (float)y0_, drawFormat);

            double C = 5050, L0 = 10.22;

            double coef = -2 * C / L0;

            op.Add(GetPoint_1( Deformation[0].X, (float)coef * Integral(0, Deformation)));

            for (int i = 1; i < Deformation.Count(); i++) {
                op.Add(GetPoint_1(Deformation[i].X, (float)coef * Integral(i, Deformation)));

                user_gr1.DrawLine(pen1, op[i - 1], op.Last());
            }

            pictureBox2.Image = canvas1;
        }


        private void Deform_Speed_Draw_Click(object sender, EventArgs e)
        {
            List<PointF> op = new List<PointF>();

            user_gr1.FillRectangle(Brushes.White, 0, 0, pictureBox2.Width, pictureBox2.Height);

            user_gr1.DrawLine(pen_ax, (float)x0_, (float)y0_, pictureBox2.Width, (float)y0_);
            user_gr1.DrawLine(pen_ax, (float)x0_, 0, (float)x0_, pictureBox2.Height);

            user_gr1.DrawString("Speed of strain", StrFont, axBrush, pictureBox2.Width / 2 - 12, 6, drawFormat);
            user_gr1.DrawString("0", axFont, axBrush, 0, (float)y0_, drawFormat);

            double C = 5050, L0 = 10.22;

            double coef = -2 * C / L0;

            op.Add(GetPoint_1(Deformation[0].X, (float)coef * Deformation[0].Y));

            for (int i = 1; i < Deformation.Count(); i++)
            {
                op.Add(GetPoint_1(Deformation[i].X, (float)coef * Deformation[i].Y));

                user_gr1.DrawLine(pen1, op[i - 1], op.Last());
            }            


            pictureBox2.Image = canvas1;

        }


        private void Tension_Draw_Click(object sender, EventArgs e)
        {
            List<PointF> op = new List<PointF>();

            user_gr1.FillRectangle(Brushes.White, 0, 0, pictureBox2.Width, pictureBox2.Height);

            user_gr1.DrawLine(pen_ax, (float)x0_, (float)y0_, pictureBox2.Width, (float)y0_);
            user_gr1.DrawLine(pen_ax, (float)x0_, 0, (float)x0_, pictureBox2.Height);

            user_gr1.DrawString("Stress", StrFont, axBrush, pictureBox2.Width / 2 - 12, 6, drawFormat);
            user_gr1.DrawString("0", axFont, axBrush, 0, (float)y0_, drawFormat);

            double E = 71000, D = 20, D0 = 18.45;
            double A = Math.PI * Math.PI * D * 0.5;
            double A0 = Math.PI * Math.PI * D0 * 0.5;

            double coef = E * A / A0;

            op.Add(GetPoint_1(Tension[0].X, (float)coef * Tension[0].Y));

            for (int i = 1; i < Tension.Count(); i++)
            {
                op.Add(GetPoint_1(Tension[i].X, (float)coef * Tension[i].Y));

                user_gr1.DrawLine(pen2, op[i - 1], op.Last());
            }


            pictureBox2.Image = canvas1;
        }

    }
}
