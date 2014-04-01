using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using System.Windows.Navigation;

namespace TextEditor
{
    public partial class SavePage : PhoneApplicationPage
    {
        public App app = (App)Application.Current;
       

        public SavePage()
        {
            InitializeComponent();
            ApplicationTitle.Text = "TEXT EDITOR";
            PageTitle.Text = "save";
           
            Loaded += (object sender, RoutedEventArgs e) =>
            {
                if (app.Filename == "")
                {
                    Filename.Text = "untitled.txt";

                }
                else
                {
                   
                    Filename.Text = app.Filename;
                }
            };
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            int f=0;
          
            if (Filename.Text != "")
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    foreach (string filename in storage.GetFileNames("*"))
                    {
                        if (filename.ToLower().Equals(Filename.Text))
                        {
                                f=1;
                        }
                    }
                }
            }
            if (f == 1)
            {
                if (MessageBox.Show("File with name " + Filename.Text + " already exits \n Are you want overwrite " + "?", "Text Editor", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    try
                    {
                        app.Filename = Filename.Text.Trim().ToLower();
                        using (IsolatedStorageFile storage2 = IsolatedStorageFile.GetUserStoreForApplication())
                        {
                            IsolatedStorageFileStream location = new IsolatedStorageFileStream(app.Filename, System.IO.FileMode.Create, storage2);
                            System.IO.StreamWriter file = new System.IO.StreamWriter(location);
                            file.Write(app.Content);
                            file.Dispose();
                            location.Dispose();
                        }
                      NavigationService.GoBack();
                     
                    }
                    catch(Exception ee)
                    {
                        // Ignore Errors
                        MessageBox.Show("File name is invalid \n Please enter file name without special characters");
                    }
                }
            }
            else
            {
                try
                {
                    app.Filename = Filename.Text.Trim().ToLower();
                    using (IsolatedStorageFile storage2 = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        IsolatedStorageFileStream location = new IsolatedStorageFileStream(app.Filename, System.IO.FileMode.Create, storage2);
                        System.IO.StreamWriter file = new System.IO.StreamWriter(location);
                        file.Write(app.Content);
                        file.Dispose();
                        location.Dispose();
                    }
                  NavigationService.GoBack();
                 
                }
                catch (Exception ee)
                {
                    // Ignore Errors
                    MessageBox.Show("File name is invalid \n Please enter file name without special characters");
                }
            }

                            
                        
                    

                
            
            
              
            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
          //  NavigationService.Navigate(new Uri("/MainPage.xaml?parameter=save", UriKind.Relative));
        }

    }
}
