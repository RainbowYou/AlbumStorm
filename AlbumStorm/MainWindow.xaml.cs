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
using AlbumStorm.MyControls;
using AlbumStorm.Dialogs;
using Microsoft.Win32;
using System.IO;

namespace AlbumStorm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region
        private List<AlbumUserControl> familyAlbumSets;
        private List<AlbumUserControl> travelAlbumSets;
        private List<AlbumUserControl> sceneAlbumSets;
        private List<AlbumUserControl> campusAlbumSets;
        private List<AlbumUserControl> personalAlbumSets;    
        private PictureViewer pictureViewer;

        public MainWindow()
        {
            InitializeComponent();

            familyAlbumSets = new List<AlbumUserControl>();
            travelAlbumSets = new List<AlbumUserControl>();
            sceneAlbumSets = new List<AlbumUserControl>();
            campusAlbumSets = new List<AlbumUserControl>();
            personalAlbumSets = new List<AlbumUserControl>();

            pictureViewer = new PictureViewer();
            Grid.SetRow(pictureViewer, 2);
            mainGrid.Children.Add(pictureViewer);
            pictureViewer.Visibility = Visibility.Hidden;

            LoadAlbumSets();
        }


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


        private void LoadAlbumSets()
        {
            AddAlbumSets(familyAlbumSets, familyWrapPanel);
            AddAlbumSets(travelAlbumSets, travelWrapPanel);
            AddAlbumSets(sceneAlbumSets, sceneWrapPanel);
            AddAlbumSets(campusAlbumSets, campusWrapPanel);
            AddAlbumSets(personalAlbumSets, personalWrapPanel);
        }
        #endregion

        private void AddAlbumSets(List<AlbumUserControl> albumSets,WrapPanel wrapPanel)
        {
            //Getting fileNames in the directory which storage the paths of pictures
            string[] fileNames = GetFileNames(albumSets);
            //There is no files in the directory
            if(fileNames.Count()==0)
            {
                return;
            }

            //Creating AlbumSet
            foreach(string s in fileNames)
            {
                AlbumUserControl albumSet = CreateAlbumSet(wrapPanel, s);
                #region
                if (albumSets.Equals(familyAlbumSets))
                {
                    albumSet.BelongsTo = "familyAlbumSets";
                }

                else if (albumSets.Equals(travelAlbumSets))
                {
                    albumSet.BelongsTo = "travelAlbumSets";
                }

                else if (albumSets.Equals(sceneAlbumSets))
                {
                    albumSet.BelongsTo = "sceneAlbumSets";
                }

                else if (albumSets.Equals(campusAlbumSets))
                {
                    albumSet.BelongsTo = "campusAlbumSets";
                }

                else if (albumSets.Equals(personalAlbumSets))
                {
                    albumSet.BelongsTo = "personalAlbumSets";
                }

                ReadPicturePaths(albumSet);
                #endregion

                AlbumSet aSet = new AlbumSet();
                aSet.ImagePath = albumSet.PicturePathList[0];
                aSet.AlbumSetName = albumSet.Name;

                //Creating a new AlbumSet,
                //for the purpose to add the first picture as the AlbumSet's background
                AlbumUserControl userControl = new AlbumUserControl();
                userControl.Name = aSet.AlbumSetName;
                userControl.Content = aSet;
                userControl.ContentTemplate = (DataTemplate)FindResource("AlbumSetDataTemplate");
                userControl.IsSelected = true;
                userControl.PicturePathList = albumSet.PicturePathList;
                userControl.BelongsTo = albumSet.BelongsTo;
                userControl.MouseDown += albumUserControl_MouseDown;
                userControl.MouseDoubleClick += albumUserControl_MouseDoubleClick;

                albumSet = userControl;//Referencing to the new AlbumSet
                

                List<Image> pictureList=new List<Image>();              
                foreach(string ss in albumSet.PicturePathList)
                {
                    Image i=new Image();
                    i.Source=new BitmapImage(new Uri(ss,UriKind.Absolute));
                    i.Stretch = Stretch.UniformToFill;
                    pictureList.Add(i);
               }
                albumSet.PictureList = pictureList;    

                albumSets.Add(albumSet);

                UpdateAlbumSets(albumSets, wrapPanel);
            }
        }


        private string[] GetFileNames(List<AlbumUserControl> albumsets)
        {
            string[] fileNames;
            List<string> files = new List<string>();
            if(albumsets.Equals(familyAlbumSets))
            {
                fileNames = Directory.GetFiles(@"familyAlbumSets");
                foreach(string s in fileNames)
                {
                    files.Add(System.IO.Path.GetFileNameWithoutExtension(s));
                }
            }

            else if(albumsets.Equals(travelAlbumSets))
            {
                fileNames = Directory.GetFiles(@"travelAlbumSets");
                foreach (string s in fileNames)
                {
                    files.Add(System.IO.Path.GetFileNameWithoutExtension(s));
                }
            }

            else if(albumsets.Equals(sceneAlbumSets))
            {
                fileNames = Directory.GetFiles(@"sceneAlbumSets");
                foreach (string s in fileNames)
                {
                    files.Add(System.IO.Path.GetFileNameWithoutExtension(s));
                }
            }

            else if(albumsets.Equals(campusAlbumSets))
            {
                fileNames = Directory.GetFiles(@"campusAlbumSets");
                foreach (string s in fileNames)
                {
                    files.Add(System.IO.Path.GetFileNameWithoutExtension(s));
                }
            }

            else if(albumsets.Equals(personalAlbumSets))
            {
                fileNames = Directory.GetFiles(@"personalAlbumSets");
                foreach (string s in fileNames)
                {
                    files.Add(System.IO.Path.GetFileNameWithoutExtension(s));
                }
            }
            return files.ToArray();
        }


        private void ReadPicturePaths(AlbumUserControl albumSet)
        {
            using(StreamReader reader=new StreamReader(albumSet.BelongsTo+@"\"+albumSet.Name+".txt",Encoding.Default))
            {
                string line;
                while((line=reader.ReadLine())!=null)
                {
                    albumSet.PicturePathList.Add(line);
                }
            }
        }

        /// <summary>
        /// Updating AlbumSets and showing them to window
        /// </summary>
        private void UpdateAlbumSets(List<AlbumUserControl> albumSets, WrapPanel wrapPanel)
        {
            wrapPanel.Children.Clear();

            foreach (AlbumUserControl uc in albumSets)
            {
                wrapPanel.Children.Add(uc);
            }
        }

        #region
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
                string name = dialogBox.albumNameTextBox.Text;
                switch (selectedTabItemName)
                {
                    case "familyTabItem": CreateAlbumSets(familyAlbumSets, (WrapPanel)this.FindName("familyWrapPanel"), name); break;
                    case "travelTabItem": CreateAlbumSets(travelAlbumSets, (WrapPanel)this.FindName("travelWrapPanel"), name); break;
                    case "sceneTabItem": CreateAlbumSets(sceneAlbumSets, (WrapPanel)this.FindName("sceneWrapPanel"), name); break;
                    case "campusTabItem": CreateAlbumSets(campusAlbumSets, (WrapPanel)this.FindName("campusWrapPanel"), name); break;
                    case "personalTabItem": CreateAlbumSets(personalAlbumSets, (WrapPanel)this.FindName("personalWrapPanel"), name); break;
                }
            }
        }


        private void CreateAlbumSets(List<AlbumUserControl> albumSets, WrapPanel wrapPanel, string name)
        {
            AlbumUserControl albumUserControl = CreateAlbumSet(wrapPanel, name);

            albumSets.Add(albumUserControl);
            //Every time adding an AlbumSets, updating AlbumSets is needed
            UpdateAlbumSets(albumSets,wrapPanel);
        }


        private AlbumUserControl CreateAlbumSet(WrapPanel wrapPanel,string name)
        {
            //Adding AlbumSet to panel
            AlbumSet albumSet = new AlbumSet();
            albumSet.AlbumSetName = name;
            //Having not getting and dealing with the message
            //albumSet.CreateTime=;

            AlbumUserControl albumUserControl = new AlbumUserControl();

            albumUserControl.Name = albumSet.AlbumSetName;
            albumUserControl.Content = albumSet;
            albumUserControl.ContentTemplate = (DataTemplate)FindResource("AlbumSetDataTemplate");

            switch (GetSelectedTabItem())
            {
                case "familyTabItem": albumUserControl.BelongsTo = "familyAlbumSets"; break;
                case "travelTabItem": albumUserControl.BelongsTo = "travelAlbumSets"; break;
                case "sceneTabItem": albumUserControl.BelongsTo = "sceneAlbumSets"; break;
                case "campusTabItem": albumUserControl.BelongsTo = "campusAlbumSets"; break;
                case "personalTabItem": albumUserControl.BelongsTo = "personalAlbumSets"; break;
            }
            //AlbumSet's event handlers
            albumUserControl.MouseDown += albumUserControl_MouseDown;
            albumUserControl.MouseDoubleClick += albumUserControl_MouseDoubleClick;

            return albumUserControl;
        }


        private void albumUserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.albumUserControl_MouseDown(sender,e);
            

            this.AlbumTabControl.Visibility = Visibility.Hidden;
            this.backButton.IsEnabled = true;

            string selectedTabItem = GetSelectedTabItem();
            switch (selectedTabItem)
            {
                case "familyTabItem": ShowPictures((AlbumUserControl)sender); break;
                case "travelTabItem": ShowPictures((AlbumUserControl)sender); break;
                case "sceneTabItem": ShowPictures((AlbumUserControl)sender); break;
                case "campusTabItem": ShowPictures((AlbumUserControl)sender); break;
                case "personalTabItem": ShowPictures((AlbumUserControl)sender); break;
            }

            pictureViewer.Visibility = Visibility.Visible;

            this.importPictureButton.IsEnabled = false;
            this.enterAlbumButton.IsEnabled = false;
        }

        #endregion
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


        private void ResetUnselectedState(List<AlbumUserControl> albumSets, AlbumUserControl uc)
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
                {//Importing selected pictures
                    case "familyTabItem": ImportPictures(familyAlbumSets,familyWrapPanel, filenames); break;
                    case "travelTabItem": ImportPictures(travelAlbumSets,travelWrapPanel, filenames); break;
                    case "sceneTabItem": ImportPictures(sceneAlbumSets,sceneWrapPanel, filenames); break;
                    case "campusTabItem": ImportPictures(campusAlbumSets, campusWrapPanel,filenames); break;
                    case "personalTabItem": ImportPictures(personalAlbumSets,personalWrapPanel, filenames); break;
                }
            }
        }
 

        private void ImportPictures(List<AlbumUserControl> albumSets, WrapPanel wrapPanel, string[] fileNames)
        {
            for(int i=0;i<albumSets.Count;i++)
            {
                if(albumSets[i].IsSelected)
                {                   
                    albumSets[i] = ReferenceNewAlbumSet(albumSets[i], fileNames);

                    foreach (string s in fileNames)
                    {       
                        albumSets[i].PicturePathList.Add(s);
                    }
                    
                    //Adding pictures
                    List<Image> il = new List<Image>();
                    using (StreamWriter writer = new StreamWriter(albumSets[i].BelongsTo + @"\" + albumSets[i].Name + ".txt"))
                    {
                        foreach (string s in albumSets[i].PicturePathList)
                        {
                            writer.WriteLine(s);

                            Image image = new Image();
                            image.Stretch = Stretch.UniformToFill;
                            image.Source = new BitmapImage(new Uri(s, UriKind.Absolute));

                            il.Add(image);
                        }
                    }
                     
                    albumSets[i].PictureList = il;
                    //After importing pictures, updating whole panel is required
                    UpdateAlbumSets(albumSets, wrapPanel);
                    break;
                }
            }
        }


        private AlbumUserControl ReferenceNewAlbumSet(AlbumUserControl albumSet,string[] fileNames)
        {
            AlbumSet aSet = new AlbumSet();
            aSet.ImagePath = fileNames[0];//Image path has been set to AlbumSet
            aSet.AlbumSetName = albumSet.Name;

            //Creating a new AlbumSet,
            //for the purpose to add the first picture as the AlbumSet's background
            AlbumUserControl userControl = new AlbumUserControl();
            userControl.Name = aSet.AlbumSetName;
            userControl.Content = aSet;
            userControl.ContentTemplate = (DataTemplate)FindResource("AlbumSetDataTemplate");
            userControl.IsSelected = true;
            userControl.PicturePathList = albumSet.PicturePathList;
            userControl.BelongsTo = albumSet.BelongsTo;
            userControl.MouseDown += albumUserControl_MouseDown;
            userControl.MouseDoubleClick += albumUserControl_MouseDoubleClick;

            albumSet = userControl;//Referencing to the new AlbumSet
            return albumSet;
        }


        private void enterAlbumButton_Click(object sender, RoutedEventArgs e)
        {
            this.AlbumTabControl.Visibility = Visibility.Hidden;
            this.backButton.IsEnabled = true;

            string selectedTabItem = GetSelectedTabItem();
            switch(selectedTabItem)
            {
                case "familyTabItem": ShowPictures(familyAlbumSets); break;
                case "travelTabItem": ShowPictures(travelAlbumSets); break;
                case "sceneTabItem": ShowPictures(sceneAlbumSets); break;
                case "campusTabItem": ShowPictures(campusAlbumSets); break;
                case "personalTabItem": ShowPictures(personalAlbumSets); break;
            }

            pictureViewer.Visibility = Visibility.Visible;
            this.importPictureButton.IsEnabled = false;
            this.enterAlbumButton.IsEnabled = false;
        }


        private void ShowPictures(List<AlbumUserControl> albumSets)
        {
            foreach(AlbumUserControl auc in albumSets)
            {
                if(auc.IsSelected)
                {
                    ShowPictures(auc);
                    break;
                }
            }
        }


        private void ShowPictures(AlbumUserControl userControl)
        {
            pictureViewer.pictureWrapPanel.Children.Clear();

            WrapPanel wp = pictureViewer.pictureWrapPanel;
            foreach(Image i in userControl.PictureList)
            {
                Picture p = new Picture(i);
                wp.Children.Add(p);
            }
        }


        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.AlbumTabControl.Visibility = Visibility.Visible;
            this.importPictureButton.IsEnabled = true;
            pictureViewer.Visibility = Visibility.Hidden;
            this.backButton.IsEnabled = false;
            this.enterAlbumButton.IsEnabled = true;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }
    }
}
