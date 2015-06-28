using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Globalization;

namespace AlbumStorm.Classes
{
    class AlbumSet
    {
        public string AlbumSetName
        {
            get
            {
                return _AlbumSetName;
            }
            set
            {
                if(value==null||value=="")
                {
                    MessageBox.Show("Please enter a name for your album set!");
                }
                else
                {
                    _AlbumSetName = value;
                }
            }
        }
        private string _AlbumSetName;
        
        public DateTime CreateTime{get;set;}

        //If album set has no pictures in it,
        //then the default background is set by DefaultIamgePath
        public string DefaultImagePath { get; set; }

        //If album set has pictures in it,
        //then the first image will be set as background
        public string FirstIamgePath { get; set; }

        public string ImagePath
        {
            //get
            //{
            //    if (!HasImage)
            //    {
            //        return DefaultImagePath;
            //    }
            //    else
            //    {
            //        return FirstIamgePath;
            //    }
            //}
            //set
            //{
            //    if(!HasImage)
            //    {
            //        value = DefaultImagePath;
            //    }
            //    else
            //    {
            //        value = FirstIamgePath;
            //    }
            //}
            get;
            set;
        }

        public bool HasImage { get; set; }

        public AlbumSet()
        {
            _AlbumSetName = null;
            DefaultImagePath = null;
            FirstIamgePath = null;
            //ImagePath = null;
            HasImage = false;
        }

        public AlbumSet(string albumSetName):this()
        {
            AlbumSetName = albumSetName;
        }

        //public AlbumSet(string albumSetName,string imagePath=""):this(albumSetName)
        //{
        //    ImagePath = imagePath;
        //    HasImage = true;
        //}

    }

    //[ValueConversion(typeof(AlbumSet),typeof(string))]
    //public class AlbumSetBackgroundConverter:IValueConverter
    //{
    //    public object Convert(object value,Type targetType,object parameter,CultureInfo culture)
    //    {
    //        if(!((AlbumSet)value).HasImage)
    //        {
    //            //If album set has no images
    //            return ((AlbumSet)value).DefaultImagePath;
    //        }

    //        else
    //        {
    //            return ((AlbumSet)value).FirstIamgePath;
    //        }
    //    }

    //    public object ConverBack(object value,Type targetType,object paramter,CultureInfo culture)
    //    {
    //        //nothing should be done here
    //        return null;
    //    }
    //}
}
