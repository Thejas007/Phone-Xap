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

namespace TextEditor
{
    public partial class OpenPage : PhoneApplicationPage
    {
        public App app = (App)Application.Current;

        public OpenPage()
        {
            InitializeComponent();
            ApplicationTitle.Text = "TEXT EDITOR";
            PageTitle.Text = "Select file";
            Loaded += (object sender, RoutedEventArgs e) =>
            {
                Files.Items.Clear();
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    foreach (string filename in storage.GetFileNames("*"))
                    {
                        Files.Items.Add(filename.ToLower());
                    }
                }
            };
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if (Files.SelectedItem != null)
            {
                app.Filename = (string)Files.SelectedItem;
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    IsolatedStorageFileStream location = new IsolatedStorageFileStream(app.Filename,
                      System.IO.FileMode.Open, storage);
                    System.IO.StreamReader file = new System.IO.StreamReader(location);
                    app.Content = file.ReadToEnd();
                    file.Dispose();
                    location.Dispose();
                }
            NavigationService.GoBack();
       
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Files.SelectedItem != null)
            {
                string _selected = (string)Files.SelectedItem;
                if (MessageBox.Show("Delete selected Document " + _selected + "?", "Text Editor",
                  MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        if (storage.FileExists(_selected))
                        {
                            storage.DeleteFile(_selected);
                        }
                    }
                    app.Content = "";
                    app.Filename = "untitled.txt";
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                   
              
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
           NavigationService.GoBack();
         
        }

    }
}
