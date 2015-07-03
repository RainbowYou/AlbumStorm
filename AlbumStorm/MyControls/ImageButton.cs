using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace AlbumStorm.MyControls
{
    class ImageButton : Button
    {
        public Brush MyNormalBrush
        {
            get { return (Brush)GetValue(MyNormalBrushProperty); }
            set { SetValue(MyNormalBrushProperty, value); }
        }
        public static readonly DependencyProperty MyNormalBrushProperty;

        public Brush MyHoverBrush
        {
            get { return (Brush)GetValue(MyHoverBrushProperty); }
            set { SetValue(MyHoverBrushProperty, value); }
        }
        public static readonly DependencyProperty MyHoverBrushProperty;

        public Brush MyPressedBrush
        {
            get { return (Brush)GetValue(MyPressedBrushProperty); }
            set { SetValue(MyPressedBrushProperty, value); }
        }
        public static readonly DependencyProperty MyPressedBrushProperty;


        static ImageButton()
        {
            ImageButton.MyNormalBrushProperty =
                DependencyProperty.Register("MyNormalBrush", typeof(Brush), typeof(ImageButton), new PropertyMetadata(null));
            ImageButton.MyHoverBrushProperty =
                DependencyProperty.Register("MyHoverBrush", typeof(Brush), typeof(ImageButton), new PropertyMetadata(null));
            ImageButton.MyPressedBrushProperty =
                DependencyProperty.Register("MyPressedBrush", typeof(Brush), typeof(ImageButton), new PropertyMetadata(null));
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton),
                                                                      new FrameworkPropertyMetadata(typeof(ImageButton)));

        }

    }
}
