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
using AlbumStorm.Classes;
using AlbumStorm.Dialogs;
using Microsoft.Win32;

namespace AlbumStorm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region
        private List<UserControl> familyAlbumSets;
        private List<UserControl> travelAlbumSets;
        private List<UserControl> sceneAlbumSets;
        private List<UserControl> campusAlbumSets;
        private List<UserControl> personalAlbumSets;
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            familyAlbumSets = new List<UserControl>();
            travelAlbumSets = new List<UserControl>();
            sceneAlbumSets = new List<UserControl>();
            campusAlbumSets = new List<UserControl>();
            personalAlbumSets = new List<UserControl>();
        }

        #region
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //For the window style has been set to NoneBorder, 
            //so the window's MouseLeftButtonDown event should be handled,
            //or the window could not be dragged
            base.DragMove();
        }
        #endregion

        /// <summary>
        /// Updating AlbumSets and showing them to window
        /// </summary>
        private void UpdateAlbumSets(List<UserControl> albumSets,WrapPanel wrapPanel)
        {
            wrapPanel.Children.Clear();

            foreach(UserControl uc in albumSets)
            {
                wrapPanel.Children.Add(uc);
            }
        }


        private string GetSelectedTabItem()
        {
            if (familyTabItem.IsSelected)
            {
                return "familyTabItem";
            }

            else if (travelTabItem.IsSelected)
            {
                return "travelTabItem";
            }

            else if (sceneTabItem.IsSelected)
            {
                return "sceneTabItem";
            }

            else if (campusTabItem.IsSelected)
            {
                return "campusTabItem";
            }

            else if (personalTabItem.IsSelected)
            {
                return "personalTabItem";
            }

            return null;
        }


        private void addAlbumButton_Click(object sender, RoutedEventArgs e)
        {
            //When addAlnbumButton is clicked, a dialog frame should appear
            //and then creating an UserControl
            
            //Using a self-defined dialog to select and validate the creation messages of AlbumSet
            GetMessageDialogBox dialogBox = new GetMessageDialogBox();
            dialogBox.Owner = this;
            dialogBox.ShowDialog();

            if(dialogBox.IsInputValid)
            {
                //Getting which TabItem is selected
                string selectedTabItemName = GetSelectedTabItem();

                switch (selectedTabItemName)
                {
                    case "familyTabItem": CreateAlbumSets(familyAlbumSets, (WrapPanel)this.FindName("familyWrapPanel"),dialogBox); break;

                    case "travelTabItem": CreateAlbumSets(travelAlbumSets, (WrapPanel)this.FindName("travelWrapPanel"), dialogBox); break;

                    case "sceneTabItem": CreateAlbumSets(sceneAlbumSets, (WrapPanel)this.FindName("sceneWrapPanel"), dialogBox); break;

                    case "campusTabItem": CreateAlbumSets(campusAlbumSets, (WrapPanel)this.FindName("campusWrapPanel"), dialogBox); break;

                    case "personalTabItem": CreateAlbumSets(personalAlbumSets, (WrapPanel)this.FindName("personalWrapPanel"), dialogBox); break;
                }
            }
        }


        private void CreateAlbumSets(List<UserControl> albumSets,WrapPanel wrapPanel,GetMessageDialogBox dialogBox)
        {
            //Adding AlbumSet to panel
            AlbumSet albumSet = new AlbumSet();
            albumSet.AlbumSetName = dialogBox.albumNameTextBox.Text;
            //Having not getting and dealing with the message
            //albumSet.CreateTime=;

            UserControl albumUserControl = new AlbumUserControl();

            /*Here has a bug*/
            albumUserControl.Name = albumSet.AlbumSetName;
            albumUserControl.Content = albumSet;
            albumUserControl.ContentTemplate = (DataTemplate)FindResource("AlbumSetDataTemplate");
            
            //AlbumSet's event handlers
            albumUserControl.MouseDown+=albumUserControl_MouseDown;
            albumUserControl.MouseDoubleClick+=albumUserControl_MouseDoubleClick;

            albumSets.Add(albumUserControl);

            //Every time adding an AlbumSets, updating AlbumSets is needed
            UpdateAlbumSets(albumSets,wrapPanel);
        }

        private void albumUserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void albumUserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AlbumUserControl uc = (AlbumUserControl)sender;
            //If current album is clicked
            uc.IsSelected = true;

            string selectedTabItem = GetSelectedTabItem();
            //Resetting unselected AlbumUserControl's state
            switch(selectedTabItem)
            {
                case "familyTabItem": ResetUnselectedState(familyAlbumSets, uc); break;
                case "travelTabItem": ResetUnselectedState(travelAlbumSets, uc); break;
                case "sceneTabItem": ResetUnselectedState(sceneAlbumSets, uc); break;
                case "campusTabItem": ResetUnselectedState(campusAlbumSets, uc); break;
                case "personalTabItem": ResetUnselectedState(personalAlbumSets, uc); break;
            }
        }


        private void ResetUnselectedState(List<UserControl> albumSets,UserControl uc)
        {
            foreach(AlbumUserControl asuc in albumSets)
            {
                if(asuc.Name!=uc.Name)
                {
                    asuc.IsSelected = false;
                }
            }
        }

        private void importPictureButton_Click(object sender, RoutedEventArgs e)
        {
            //Instantiate the dialog box
            OpenFileDialog dialog = new OpenFileDialog();

            //Configuring open file dialog box
            //dialog.FileName = "Select Pictures";
            dialog.DefaultExt = ".jpg";
            dialog.Filter = "Select pictures(JPEG)|*.jpg;*.jpeg;*.pge;*.jfif|" +
                                            "位图文件|*.bmp;*.dib|" +
                                            "GIF|*.gif|" +
                                            "TIFF|*.tif;*.tiff|" +
                                            "PNG|*.png|" +
                                            "ICO|*.ico|"+
                                            "所有文件|*.*";
            dialog.Multiselect = true;

            //Opening the dialog box modally
            Nullable<bool> result = dialog.ShowDialog();

            //Processing open file dialog box results
            if (result == true)
            {
                //Opening document
                string[] filenames = dialog.FileNames;
                string selectedTabItem = GetSelectedTabItem();
                
                switch(selectedTabItem)
                {
                    case "familyTabItem": ImportPictures(familyAlbumSets,familyWrapPanel, filenames); break;
                    case "travelTabItem": ImportPictures(travelAlbumSets,travelWrapPanel, filenames); break;
                    case "sceneTabItem": ImportPictures(sceneAlbumSets,sceneWrapPanel, filenames); break;
                    case "campusTabItem": ImportPictures(campusAlbumSets, campusWrapPanel,filenames); break;
                    case "personalTabItem": ImportPictures(personalAlbumSets,personalWrapPanel, filenames); break;
                }
            }
        }


        private void ImportPictures(List<UserControl> albumSets,WrapPanel wrapPanel,string[] fileNames)
        {
            #region
            /*
            foreach(AlbumUserControl auc in albumSets)
            {
                //Finding selected AlbumSets
                if(auc.IsSelected)
                {
                    AlbumSet aSet = new AlbumSet();
                    aSet.ImagePath = fileNames[0];
                    aSet.AlbumSetName = auc.Name;

                    AlbumUserControl userControl = new AlbumUserControl();
                    userControl.Name = aSet.AlbumSetName;
                    userControl.Content = aSet;
                    userControl.ContentTemplate = (DataTemplate)FindResource("AlbumSetDataTemplate");
                    userControl.IsSelected = true;

                    //auc = userControl;
                    
                    //Adding pictures
                    List<Image> il = new List<Image>();
                    foreach(string s in fileNames)
                    {
                        Image image = new Image();
                        image.Stretch = Stretch.UniformToFill;
                        image.Source = new BitmapImage(new Uri(s, UriKind.Absolute));

                        il.Add(image);
                    }
                    auc.PictureList = il;
                    break;
                }
            }*/
            #endregion
            for(int i=0;i<albumSets.Count;i++)
            {
                if(((AlbumUserControl)albumSets[i]).IsSelected)
                {
                    AlbumSet aSet = new AlbumSet();
                    aSet.ImagePath = fileNames[0];
                    aSet.AlbumSetName = albumSets[i].Name;

                    AlbumUserControl userControl = new AlbumUserControl();
                    userControl.Name = aSet.AlbumSetName;
                    userControl.Content = aSet;
                    userControl.ContentTemplate = (DataTemplate)FindResource("AlbumSetDataTemplate");
                    userControl.IsSelected = true;
                    userControl.MouseDown += albumUserControl_MouseDown;
                    userControl.MouseDoubleClick += albumUserControl_MouseDoubleClick;

                    //auc = userControl;
                    albumSets[i] = userControl;

                    //Adding pictures
                    List<Image> il = new List<Image>();
                    foreach (string s in fileNames)
                    {
                        Image image = new Image();
                        image.Stretch = Stretch.UniformToFill;
                        image.Source = new BitmapImage(new Uri(s, UriKind.Absolute));

                        il.Add(image);
                    }
                    ((AlbumUserControl)albumSets[i]).PictureList = il;
                    //After importing pictures, updating whole panel is required
                    UpdateAlbumSets(albumSets, wrapPanel);

                    break;
                }
            }
        }

        private void enterAlbumButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
