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
using System.Windows.Shapes;
using devDept.Eyeshot;
using devDept.Eyeshot.Entities;

namespace FirstPractice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int Height;
        private int Width;
        private int Length;
        private Mesh m = Mesh.CreateBox(1, 1, 1);
        public MainWindow()
        {
            InitializeComponent();
        }
        public void height_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Height = Convert.ToInt32(e.NewValue);
            Debug.WriteLine(Height);
        }

        public void width_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Width = Convert.ToInt32(e.NewValue);
            Debug.WriteLine(Width);
        }

        public void length_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Length = Convert.ToInt32(e.NewValue);
            Debug.WriteLine(Length);
        }

        protected override void OnContentRendered(EventArgs e)
        {
            Debug.WriteLine("Execution");
            // Creates a mesh with the shape of a box
            
            // Adds the mesh to the master entity collection
            drafting.Entities.Add(m, System.Drawing.Color.DarkRed);

            // Fits the drawing in the viewport
            //drafting.ZoomFit();
            
            
            base.OnContentRendered(e);
        }

        private void UpdateGeometry(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("Runn");
            drafting.Entities.Remove(m);
            m = Mesh.CreateBox(Height, Width, Length);
            drafting.Entities.Add(m, System.Drawing.Color.Red);
            drafting.Entities.Regen();
        }

        private void UpdateWindow(object sender, MouseEventArgs e)
        {
            if (drafting.Entities.Count > 0)
            {
                if (drafting.Entities[0] is Mesh)
                {
                    Mesh m = (Mesh)drafting.Entities[0];
                    Debug.WriteLine(m.BoxSize);
                }
            }
        }
    }
}