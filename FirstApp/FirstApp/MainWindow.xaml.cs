using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace FirstApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Saurabh is here");
            Thread t1 = new Thread(new ThreadStart(PrintMsg));
            t1.Start();
        }
        public void PrintMsg()
        {
            MessageBox.Show("Hi thread");
            //tbox.Text = "Hello"; // This will give exception because another thread is maintaining that UI component so we have to use dispatcher invoke method
            //Exception=The calling thread cannot access this object because a different thread owns it
            this.Dispatcher.Invoke(()=> { tbox.Text = "Hello Saurabh"; });
        }
        private void Go2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Dhananjay is here");
            tbox.Text = "Hello Dhananjay";
        }

        private void Go1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Vivek is here");
        }

        private void Go3(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ninad is here");
        }

        private void Go4(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Proto is here");
        }
    }
}
