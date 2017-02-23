using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace pSupport
{
    /// <summary>
    /// Interaction logic for MsgForm.xaml
    /// </summary>
    public partial class MsgForm : Window
    {
        public MsgForm(string value)
        {
            InitializeComponent();
            lblMessage.Content = value; 
            
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
          
            this.Close();
           

        }
    }
}
