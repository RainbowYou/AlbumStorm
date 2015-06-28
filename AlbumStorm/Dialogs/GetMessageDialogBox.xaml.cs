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

namespace AlbumStorm.Dialogs
{
    /// <summary>
    /// Interaction logic for GetMessageDialogBox.xaml
    /// </summary>
    public partial class GetMessageDialogBox : Window
    {
        public DateTime CreateTime
        {
            get
            {
                return _CreateTime;
            }
            private set
            {
                _CreateTime = value;
            }
        }
        private DateTime _CreateTime;

        public bool IsInputValid
        {
            get
            {
                return _IsInputValid;
            }
            private set
            {
                _IsInputValid = value;
            }
        }
        private bool _IsInputValid;

        public GetMessageDialogBox()
        {
            InitializeComponent();

            IsInputValid = false;
        }

        private void closeButto_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            IsInputValid = true;
            //CreateTime = (DateTime)createTimeTextBox.Text;
            this.DialogResult = true;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
