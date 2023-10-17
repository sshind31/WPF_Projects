using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using devDept.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace LineDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool CanDrawLine = false;

        private List<Point3D> Points = new List<Point3D>();

        private Point3D MouseLoc3D;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EnableUserToDrawALine(object sender, RoutedEventArgs e)
        {
            Points.Clear();
            CanDrawLine = true;
            DrawingLinePractice.SetView(viewType.Top, false, true);
        }
        protected override void OnContentRendered(EventArgs e)
        {
            Debug.WriteLine("Execution");
            // Creates a mesh with the shape of a box

            // Adds the mesh to the master entity collection
            //DrawingLinePractice.Entities.Add(m, System.Drawing.Color.DarkRed);

            // Fits the drawing in the viewport
            DrawingLinePractice.ZoomFit();


            base.OnContentRendered(e);
        }

        private void GetCoOrdOfClick(object sender, MouseButtonEventArgs e)
        {
            if (CanDrawLine)
            {

                if (Points.Count < 2)
                {
                    var mousePos = RenderContextUtility.ConvertPoint(DrawingLinePractice.GetMousePosition(e)); //to get value of mouse loc on design window
                    DrawingLinePractice.ScreenToPlane(mousePos, Plane.XY, out MouseLoc3D); //to convert mouse loc to Point3D on the XY plane
                    Points.Add(MouseLoc3D);
                }
                if (Points.Count == 2)
                {
                    Line line = new Line(Points[0], Points[1]);
                    DrawingLinePractice.Entities.Add(line);
                    CanDrawLine = false;
                    Debug.WriteLine("Line is drawn");
                    Points.Clear();
                }
            }
            DrawingLinePractice.Entities.Regen();
            Debug.WriteLine("plane updated");
        }

        private void UpdaetStatus(object sender, MouseEventArgs e)
        {
            if (CanDrawLine)
            {
                msg.Text = "Active";
                msg.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                msg.Text = "DeActive";
                msg.Foreground = new SolidColorBrush(Colors.Red);
            }
        }


        //If you wrote specific class for design window of eyeshot then that class is going to be self Workspace class
        //i.e. you can access regen method directly like Entities.Regen(). And simply Invalidate().
        //Otherwise Name of ddes design window name will be object can access workspace class methods.

    }
}
