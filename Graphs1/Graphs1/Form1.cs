using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphs1
{
    public partial class Form1 : Form
    {
        List<Edge> Edges = new List<Edge>();
        Form newForm;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Edges.Clear();
            AddEdgesToList();
            CreateNewForm();
            DrawGraph();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    TextBox tbToEdit = tableLayoutPanel1.GetControlFromPosition(i, j) as TextBox;
                    if (i == j)
                    {
                        tbToEdit.Text = "0";
                        tbToEdit.ReadOnly = true;
                    }
                }
            }
        }

        private void CreateNewForm()
        {
            Form Form2 = new Form();
            Form2.Size = new Size(500, 500);
            newForm = Form2;
            newForm.Show();
        }

        private void AddEdgesToList()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    TextBox tb = tableLayoutPanel1.GetControlFromPosition(i, j) as TextBox;
                    int value;
                    bool parseInt = int.TryParse(tb.Text, out value);
                    if (!parseInt)
                    {
                        value = 0;
                    }
                    Edges.Add(new Edge(value, j, i));
                }
            }
        }

        private void DrawGraph()
        {
            Graphics graphics = newForm.CreateGraphics();
            int numberOfVertices = CalculateNumberOfVertices();

            PointF[] vertices = new PointF[numberOfVertices];
            float radius = 100;
            float x = 200;
            float y = 200;
            float angle = 360.0f / numberOfVertices;

            for (int i = 0; i < numberOfVertices; i++)
            {
                float theta = i * angle * (float)Math.PI / 180.0f;
                float pointX = x + radius * (float)Math.Cos(theta);
                float pointY = y + radius * (float)Math.Sin(theta);
                vertices[i] = new PointF(pointX, pointY);
            }

            DrawEdges(graphics, vertices);
            DrawVertices(graphics, vertices);
        }

        private void DrawVertices(Graphics graphics, PointF[] vertexArray)
        {
            Brush brush = Brushes.Black;
            foreach (PointF vertex in vertexArray)
            {
                graphics.FillEllipse(brush, vertex.X - 5, vertex.Y - 5, 10, 10);
            }
        }

        private void DrawEdges(Graphics graphics, PointF[] vertexArray)
        {
            Pen pen = new Pen(Color.Blue, 2);
            foreach (Edge edge in Edges)
            {
                if (edge.Value == 1)
                {
                    graphics.DrawLine(pen, vertexArray[edge.Column], vertexArray[edge.Row]);
                    DrawArrow(graphics, vertexArray[edge.Column], vertexArray[edge.Row]);
                }
            }
        }

        private void DrawArrow(Graphics graphics, PointF startPoint, PointF endPoint)
        {
            Brush brush = Brushes.Blue;
            double angleInRadians = GetAngle(startPoint, endPoint);
            PointF pointA = new PointF((float)(startPoint.X + 5 * Math.Cos(angleInRadians)), (float)(startPoint.Y + 5 * Math.Sin(angleInRadians)));
            PointF pointB = new PointF((float)(startPoint.X - 15 * Math.Cos(angleInRadians - 60)), (float)(startPoint.Y - 15 * Math.Sin(angleInRadians - 60)));
            PointF pointC = new PointF((float)(startPoint.X - 15 * Math.Cos(angleInRadians + 60)), (float)(startPoint.Y - 15 * Math.Sin(angleInRadians + 60)));
            PointF[] points = new PointF[] { pointA, pointB, pointC };

            graphics.FillPolygon(brush, points);
        }

        private double GetAngle(PointF point1, PointF point2)
        {
            double xValue = point2.X - point1.X;
            double yValue = point2.Y - point1.Y;
            double radians = Math.Atan2(yValue, xValue);
            return radians;
        }

        private int CalculateNumberOfVertices()
        {
            int number = 0;
            foreach (Edge edge in Edges)
            {
                if (edge.Value == 1)
                {
                    if ((edge.Row + 1) > number)
                    {
                        number = (edge.Row + 1);
                    }
                    if ((edge.Column + 1) > number)
                    {
                        number = (edge.Column + 1);
                    }
                }
            }
            return number;
        }
    }
}
