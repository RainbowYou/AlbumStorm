using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AlbumStorm.MyControls
{
	public class MyTabItem : TabItem
	{
		public static readonly DependencyProperty MyMoverBrushProperty;
		public static readonly DependencyProperty MyEnterBrushProperty;
        public static readonly DependencyProperty MyNormalBrushProperty;

		public Brush MyMoverBrush
		{
			get { return base.GetValue(MyTabItem.MyMoverBrushProperty) as Brush; }
			set { base.SetValue(MyTabItem.MyMoverBrushProperty, value); }
		}

		public Brush MyEnterBrush
		{
            get { return base.GetValue(MyTabItem.MyEnterBrushProperty) as Brush; }
			set { base.SetValue(MyTabItem.MyEnterBrushProperty, value); }
		}

        public Brush MyNormalBrush
        {
            get { return base.GetValue(MyTabItem.MyNormalBrushProperty) as Brush; }
            set { base.SetValue(MyTabItem.MyNormalBrushProperty, value); }
        }

		static MyTabItem()
		{
			MyTabItem.MyMoverBrushProperty = 
                DependencyProperty.Register("MyMoverBrush", typeof(Brush), typeof(MyTabItem), new PropertyMetadata(null));
			MyTabItem.MyEnterBrushProperty = 
                DependencyProperty.Register("MyEnterBrush", typeof(Brush), typeof(MyTabItem), new PropertyMetadata(null));
            MyTabItem.MyNormalBrushProperty =
                DependencyProperty.Register("MyNormalBrush", typeof(Brush), typeof(MyTabItem), new PropertyMetadata(null));
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(MyTabItem),
                                                                      new FrameworkPropertyMetadata(typeof(MyTabItem)));
		}

	}
}
