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
using System.Windows.Navigation;
namespace TextEditor
{
    public partial class MainPage : PhoneApplicationPage
    {
        public App app = (App)Application.Current;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            try
            {
                ApplicationTitle.Text = "TEXT EDITOR";
                PageTitle.Text = "untitled.txt";
                Editor.Text = "";
                app.InitializeComponent();
                
                Loaded += (object sender, RoutedEventArgs e) =>
                  {
                      if (app.Content != null)
                      {
                          Editor.Text = (string)app.Content;
                      }
                      if (app.Filename != "")
                      {
                          PageTitle.Text = app.Filename;
                      }
                      else
                      {
                          PageTitle.Text = "untitled.txt";
                      }
                
                     
                  };
        
                
            }
            catch (Exception e)
            {
               // MessageBox.Show(e.Message.ToString());
            }
        }

        private void New_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Start a new Document?", "Text Editor",
            MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                PageTitle.Text = "untitled.txt";
                Editor.Text = "";
                app.Filename = PageTitle.Text;
                app.Content = Editor.Text;
            }
        }

        private void Open_Click(object sender, EventArgs e)
        {
            app.Content = Editor.Text;
            NavigationService.Navigate(new Uri("/OpenPage.xaml", UriKind.Relative));
        }

        private void View_Click(object sender, EventArgs e)
        {
            app.Content = Editor.Text;
            NavigationService.Navigate(new Uri("/OpenPage.xaml", UriKind.Relative));
        }


        private void Save_Click(object sender, EventArgs e)
        {
            app.Content = Editor.Text;
            NavigationService.Navigate(new Uri("/SavePage.xaml", UriKind.Relative));
        }

     

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        

    }
}
