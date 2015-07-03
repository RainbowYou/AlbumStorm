using System;
using System.Windows;
using System.Windows.Controls;

namespace AlbumStorm.MyControls
{
	public class MyTabControl : TabControl
	{
        static MyTabControl()
		{
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(MyTabControl), 
                                                                      new FrameworkPropertyMetadata(typeof(MyTabControl)));
		}
	}
}
