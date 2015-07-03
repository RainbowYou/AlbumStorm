using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace AlbumStorm.MyControls
{
    class AlbumUserControl :UserControl
    {
        public List<Image> PictureList
        {
            get
            {
                return _PictureList;
            }
            set
            {
                _PictureList = value;
            }
        }
        private List<Image> _PictureList;

        public string BelongsTo { get; set; }

        //Storage selected pictures' path 
        //public List<string> PicturePathList { get; set; }
        public List<string> PicturePathList;

        public bool IsSelected { get; set; }

        public AlbumUserControl()
        {
            IsSelected = false;
            PicturePathList = new List<string>();
        }
    }
}
