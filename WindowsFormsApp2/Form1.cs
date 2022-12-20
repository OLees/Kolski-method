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

        List<PointF> Strain_Rate = new List<PointF>();
        List<PointF> Strain = new List<PointF>();
        List<PointF> Stress = new List<PointF>();


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

        double Imp_Coef, Time_Coef;
        double Time_Div, Imp_Div;
        int Points;

        float new_Time_coef, new_Imp_coef, Imp_Coef_1, Imp_Coef_2;

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

            return MatrixMultiplication(P, temp);
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

            return MatrixMultiplication(P, temp);
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

            user_Graphics.Clear(Color.White);

            user_Graphics.DrawLine(pen_ax, (float)x0, (float)y0, pictureBox1.Width, (float)y0);
            user_Graphics.DrawLine(pen_ax, (float)x0, 0, (float)x0, pictureBox1.Height);

            user_Graphics.DrawLine(pen_ax, (float)x0, 5, pictureBox1.Width, 5);
            user_Graphics.DrawLine(pen_ax, (float)x0, (float)y0 - pictureBox1.Height / 4, pictureBox1.Width, (float)y0 - pictureBox1.Height/4);
            user_Graphics.DrawLine(pen_ax, (float)x0, (float)y0 + pictureBox1.Height / 4, pictureBox1.Width, (float)y0 + pictureBox1.Height / 4);
            user_Graphics.DrawLine(pen_ax, (float)x0, pictureBox1.Height-5, pictureBox1.Width, pictureBox1.Height-5);

            user_Graphics.DrawLine(pen_ax, pictureBox1.Width * (float)0.25, (float)y0 - 3, pictureBox1.Width * (float)0.25, (float)y0 + 3);
            user_Graphics.DrawLine(pen_ax, pictureBox1.Width * (float)0.5, (float)y0 - 3, pictureBox1.Width * (float)0.5, (float)y0 + 3);
            user_Graphics.DrawLine(pen_ax, pictureBox1.Width * (float)0.75, (float)y0 - 3, pictureBox1.Width * (float)0.75, (float)y0 + 3);
            user_Graphics.DrawLine(pen_ax, pictureBox1.Width - 5, (float)y0 - 3, pictureBox1.Width - 5, (float)y0 + 3);

            double Znach_y = Math.Round(Imp_Div * 1000) / 1000;

            user_Graphics.DrawString(Znach_y.ToString(), axFont, axBrush, -5, 5, drawFormat);
            user_Graphics.DrawString((Znach_y / 2).ToString(), axFont, axBrush, -5, (pictureBox1.Height - 15) * (float)0.25, drawFormat);
            user_Graphics.DrawString(0.ToString(), axFont, axBrush, 0, pictureBox1.Height / 2 - 5, drawFormat);
            user_Graphics.DrawString((-Znach_y / 2).ToString(), axFont, axBrush, -5, (pictureBox1.Height - 15) * (float)0.75, drawFormat);
            user_Graphics.DrawString((-Znach_y).ToString(), axFont, axBrush, -5, pictureBox1.Height - 15, drawFormat);

            pictureBox1.Image = canvas;
        }

        void Axis_1()
        {

            user_gr1.Clear(Color.White);

            user_gr1.DrawLine(pen_ax, (float)x0_, (float)y0_, pictureBox2.Width, (float)y0_);
            user_gr1.DrawLine(pen_ax, (float)x0_, 0, (float)x0_, pictureBox2.Height);

            user_gr1.DrawString("0", axFont, axBrush, 0, (float)y0_, drawFormat);

            user_gr1.DrawLine(pen_ax, (float)x0_, 5, pictureBox2.Width, 5);
            user_gr1.DrawLine(pen_ax, (float)x0_, (float)y0_ - pictureBox2.Height / 4, pictureBox2.Width, (float)y0_ - pictureBox2.Height / 4);
            user_gr1.DrawLine(pen_ax, (float)x0_, (float)y0_ + pictureBox2.Height / 4, pictureBox2.Width, (float)y0_ + pictureBox2.Height / 4);
            user_gr1.DrawLine(pen_ax, (float)x0_, pictureBox2.Height - 5, pictureBox2.Width, pictureBox2.Height - 5);

            user_gr1.DrawLine(pen_ax, pictureBox2.Width * (float)0.25, (float)y0_ - 3, pictureBox2.Width * (float)0.25, (float)y0_ + 3);
            user_gr1.DrawLine(pen_ax, pictureBox2.Width * (float)0.5, (float)y0_ - 3, pictureBox2.Width * (float)0.5, (float)y0_ + 3);
            user_gr1.DrawLine(pen_ax, pictureBox2.Width * (float)0.75, (float)y0_ - 3, pictureBox2.Width * (float)0.75, (float)y0_ + 3);
            user_gr1.DrawLine(pen_ax, pictureBox2.Width - 5, (float)y0_ - 3, pictureBox2.Width - 5, (float)y0_ + 3);

            pictureBox2.Image = canvas1;

        }

        void Divisions_1(List<PointF> p, float c) {
            double Znach_x=0, Znach_y=0;

            for (int i = 0; i < p.Count(); i++) {
                if (p[i].X > Znach_x)
                    Znach_x = p[i].X;
                if (p[i].Y > Znach_y)
                    Znach_y = p[i].Y;
            }

            Znach_y *= c;

            Znach_y = Math.Round(Znach_y * 100) / 100;

            Znach_x = Math.Round(Znach_x * 1000) / 1000;

            user_gr1.DrawString(Znach_y.ToString(), axFont, axBrush, -3, 5, drawFormat);
            user_gr1.DrawString((Znach_y / 2).ToString(), axFont, axBrush, -3, (pictureBox2.Height - 15) * (float)0.25, drawFormat);

            user_gr1.DrawString((-Znach_y / 2).ToString(), axFont, axBrush, -3, (pictureBox2.Height - 15) * (float)0.75, drawFormat);
            user_gr1.DrawString((-Znach_y).ToString(), axFont, axBrush, -3, pictureBox2.Height - 15, drawFormat);

            user_gr1.DrawString(Znach_x.ToString(), axFont, axBrush,  pictureBox2.Width - 15, (float)y0_ + 5, drawFormat);
            user_gr1.DrawString((Znach_x * 0.75).ToString(), axFont, axBrush,  (pictureBox2.Width) * (float)0.75, (float)y0_ + 5, drawFormat);

            user_gr1.DrawString((Znach_x * 0.5).ToString(), axFont, axBrush,  pictureBox2.Width  * (float)0.5, (float)y0_ + 5, drawFormat);
            user_gr1.DrawString((Znach_x * 0.25).ToString(), axFont, axBrush, (pictureBox2.Width) * (float)0.25, (float)y0_ + 5, drawFormat);

            pictureBox2.Image = canvas1;
        }

        void Divisions_2(List<PointF> p)
        {
            double Znach_x = 0, Znach_y = 0;

            for (int i = 0; i < p.Count(); i++)
            {
                if (p[i].X > Znach_x)
                    Znach_x = p[i].X;
                if (p[i].Y > Znach_y)
                    Znach_y = p[i].Y;
            }

            Znach_y = Math.Round(Znach_y * 100000) / 100000;

            Znach_x = Math.Round(Znach_x * 10000) / 10000;

            user_gr1.DrawString(Znach_y.ToString(), axFont, axBrush, -3, 5, drawFormat);
            user_gr1.DrawString((Znach_y / 2).ToString(), axFont, axBrush, -3, (pictureBox2.Height - 15) * (float)0.25, drawFormat);

            user_gr1.DrawString((-Znach_y / 2).ToString(), axFont, axBrush, -3, (pictureBox2.Height - 15) * (float)0.75, drawFormat);
            user_gr1.DrawString((-Znach_y).ToString(), axFont, axBrush, -3, pictureBox2.Height - 15, drawFormat);

            user_gr1.DrawString(Znach_x.ToString(), axFont, axBrush, pictureBox2.Width - 15, (float)y0_ + 5, drawFormat);
            user_gr1.DrawString((Znach_x * 0.75).ToString(), axFont, axBrush, (pictureBox2.Width) * (float)0.75, (float)y0_ + 5, drawFormat);

            user_gr1.DrawString((Znach_x * 0.5).ToString(), axFont, axBrush, pictureBox2.Width * (float)0.5, (float)y0_ + 5, drawFormat);
            user_gr1.DrawString((Znach_x * 0.25).ToString(), axFont, axBrush, (pictureBox2.Width) * (float)0.25, (float)y0_ + 5, drawFormat);

            pictureBox2.Image = canvas1;
        }


        private void Change_dx(object sender, EventArgs e)
        {
            dx = trackBar1.Value - 100;
            Draw();
        }

        private void Change_dx1(object sender, EventArgs e)
        {
            dx1 = trackBar2.Value - 100;
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

            Time_Div = Time.Max();
            Imp_Div = Math.Max(Math.Max(LoadingImp.Max(), -LoadingImp.Min()), Math.Max(-ReflectedImp.Min(), ReflectedImp.Max()));

            Imp_Coef = -(pictureBox1.Height - 50) / (Math.Round(LoadingImp.Min() * 10000) / 5000);
            Time_Coef =  (pictureBox1.Width - 20) / Time.Max();
        }



        private PointF GetPoint(float x, float y) => new PointF((float)(x0 + x * Time_Coef), (float)(y0 - y * Imp_Coef));

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

            Kolski_Check();

            pictureBox1.Image = canvas;
        }

        private void Kolski_Check() {
            float[,] transformedPoints1 = CreateTransformedPoints1();
            float[,] transformedPoints2 = CreateTransformedPoints2();

            float temp_1;
            double Check_Value = 0;
            int count, index;

            List<PointF> temp1 = new List<PointF>();
            List<PointF> temp2 = new List<PointF>();

            List<PointF> op = new List<PointF>();
            float Strain_max = 0;
            float Time_max = 0;

            int i, j;

            for (i = 0; i < Time.Count(); i++)
            {
                if (transformedPoints1[0, i] >= x0)
                {
                    temp_1 = transformedPoints1[1, i];
                    
                    temp1.Add(GetF(transformedPoints1[0, i], temp_1));
                    if (transformedPoints1[0, i] > Time_max)
                        Time_max = transformedPoints1[0, i];
                    if (temp_1 > Strain_max)
                        Strain_max = transformedPoints1[1, i];
                }

                if (transformedPoints2[0, i] >= x0)
                    temp2.Add(GetF(transformedPoints2[0, i], transformedPoints2[1, i]));
                
            }

            new_Imp_coef = (float)((pictureBox2.Height - 30) / Strain_max) / 600000; 
            new_Time_coef = (pictureBox2.Width - 30) / Time_max;

            Stress = new List<PointF>(temp1);
            Strain_Rate = new List<PointF>();
            Strain = new List<PointF>();

            index = Time.IndexOf(temp2[0].X - 30);

            Check_Value += Math.Pow(-temp2[0].Y + temp1[0].Y - LoadingImp[index], 2);

            Strain_Rate.Add(GetF(temp2[0].X, -temp2[0].Y + temp1[0].Y));

            op.Add(GetPoint(temp2[0].X, -temp2[0].Y + temp1[0].Y));

            count = Math.Min(temp1.Count(), temp2.Count());

            for (j = 1; j < count; j++)
            {
                Strain_Rate.Add(GetF(temp2[j].X, -temp2[j].Y + temp1[j].Y));

                op.Add(GetPoint(temp2[j].X, -temp2[j].Y + temp1[j].Y));

                if (index + j < Time.Count())
                    Check_Value += Math.Pow(-temp2[j].Y + temp1[j].Y - LoadingImp[index + j], 2);

                user_Graphics.DrawLine(pen3, op[j - 1], op.Last());
            }

            Check_Value /= count;
            Check_Value = Math.Sqrt(Check_Value);
            Middle_Sq_Value.Text = Check_Value.ToString();

            pictureBox1.Image = canvas;
        }

        float Integral(int index, List<PointF> op)
        {
            float temp = 0;
            int k;

            for (k = 1; k < index; k++) {
                temp += (float)((op[k-1].Y + op[k].Y) * (op[k].X - op[k-1].X) / 2);
            }

            return temp;
        }

        private void Strain_Draw_Click(object sender, EventArgs e)
        {
            Axis_1();

            user_gr1.DrawString("Strain", StrFont, axBrush, pictureBox2.Width / 2 - 12, 6, drawFormat);
            user_gr1.DrawString("Время, мкс", axFont, axBrush, pictureBox2.Width / 2 - 12, pictureBox2.Height - 13, drawFormat);

            Strain.Add(GetPoint_1(Strain_Rate[0].X,  Integral(0, Strain_Rate) * 2000));

            for (int i = 1; i < Strain_Rate.Count(); i++)
            {
                Strain.Add(GetPoint_1(Strain_Rate[i].X,  Integral(i, Strain_Rate) * 2000));

                user_gr1.DrawLine(pen1, Strain[i - 1], Strain.Last());
            }

            Divisions_1(Strain, 1);

            pictureBox2.Image = canvas1;
        }

        private void Strain_Rate_Draw_Click(object sender, EventArgs e)
        {
            Axis_1();

            List<PointF> op = new List<PointF>();

            user_gr1.DrawString("Strain Rate", StrFont, axBrush, pictureBox2.Width / 2 - 12, 6, drawFormat);
            user_gr1.DrawString("Время, мкс", axFont, axBrush, pictureBox2.Width / 2 - 12, pictureBox2.Height - 13, drawFormat);


            double C = 5050, L0 = 10.22;

            float coef = (float)(-2 * C / L0);

            op.Add(GetPoint_1(Strain_Rate[0].X, (float)coef * Strain_Rate[0].Y * 200));

            for (int i = 1; i < Strain_Rate.Count(); i++)
            {
                op.Add(GetPoint_1(Strain_Rate[i].X, (float)coef * Strain_Rate[i].Y * 200));

                user_gr1.DrawLine(pen1, op[i - 1], op.Last());
            }

            Divisions_1(Strain_Rate, -coef);

            pictureBox2.Image = canvas1;

        }

        private void Stress_Draw_Click(object sender, EventArgs e)
        {
            List<PointF> op = new List<PointF>();

            Axis_1();

            user_gr1.DrawString("Stress", StrFont, axBrush, pictureBox2.Width / 2 - 12, 6, drawFormat);
            user_gr1.DrawString("Время, мкс", axFont, axBrush, pictureBox2.Width / 2 - 12, pictureBox2.Height - 13, drawFormat);

            double E = 71000, D = 20, D0 = 18.45;
            double A = Math.PI * Math.PI * D * 0.5;
            double A0 = Math.PI * Math.PI * D0 * 0.5;

            float coef = (float)(E * A / A0);

            op.Add(GetPoint_1(Stress[0].X, 3 * (float)coef * Stress[0].Y));

            for (int i = 1; i < Stress.Count(); i++)
            {
                op.Add(GetPoint_1(Stress[i].X, 3 * (float)coef * Stress[i].Y));

                user_gr1.DrawLine(pen2, op[i - 1], op.Last());
            }

            Divisions_1(Stress, coef);

            pictureBox2.Image = canvas1;
        }

        private PointF GetPoint_2(float x, float y) => new PointF((float)(x0_ + x * Imp_Coef_1), (float)(y0_ - y * Imp_Coef_2*10000));

        private void Stress_Strain_Btn_Click(object sender, EventArgs e)
        {

            List<PointF> op = new List<PointF>();

            int index = Math.Min(Stress.Count(), Strain.Count());

            Axis_1();

            user_gr1.DrawString("Strain ~ Stress", StrFont, axBrush, pictureBox2.Width / 2 - 12, 6, drawFormat);
            user_gr1.DrawString("0", axFont, axBrush, 0, (float)y0_, drawFormat);
            user_gr1.DrawString("Strain", axFont, axBrush, pictureBox2.Width - 35, pictureBox2.Height / 2,  drawFormat);
            user_gr1.DrawString("Stress", axFont, axBrush, 6, 6, drawFormat);


            Imp_Coef_1 = Imp_Coef_2 = 1;

            op.Add(GetPoint_2(Imp_Coef_1 * Strain[0].Y, Imp_Coef_1 * Stress[0].Y / (float)1.1));

            for (int i = 1; i < index; i++)
            {
                op.Add(GetPoint_2(Imp_Coef_1 * Strain[i].Y , Imp_Coef_1 * Stress[i].Y / (float)1.1));

                user_gr1.DrawLine(pen2, op[i - 1], op.Last());
            }

            pictureBox2.Image = canvas1;

        }

    }
}
