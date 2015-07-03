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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AlbumStorm.MyControls
{
    /// <summary>
    /// Interaction logic for Picture.xaml
    /// </summary>
    public partial class Picture : UserControl
    {
        public Picture()
        {
            InitializeComponent();
        }

        public Picture(Image i):this()
        {
            Binding binding = new Binding();
            binding.Source = i.Source;
            this.pictureImage.SetBinding(Image.SourceProperty, binding);
        }
    }
}
