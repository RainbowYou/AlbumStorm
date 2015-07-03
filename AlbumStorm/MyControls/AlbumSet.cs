using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Globalization;
 
namespace AlbumStorm.MyControls
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
                //if (value == "")
                //{
                //    MessageBox.Show("Please enter a name for your album set!");
                //}
                //else
                //{
                //    _AlbumSetName = value;
                //}
                _AlbumSetName = value;

            }
        }
        private string _AlbumSetName;
        
        public DateTime CreateTime{get;set;}

        //If album set has no pictures in it,
        //then the default background is set by DefaultImagePath
        public string DefaultImagePath { get; set; }

        //If album set has pictures in it,
        //then the first image will be set as background
        public string FirstIamgePath { get; set; }

        public string ImagePath
        {
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

    }
}
