using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace AutoUpdate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnGetVersion_Click(object sender, RoutedEventArgs e)
        {
            //Get local project's current version
            string currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            MessageBox.Show("Current version is: " + currentVersion);
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //Downloads a file from the server that is used to determine what files to update
            string sourcePath = System.IO.File.ReadAllText(@".\ServerPath.txt");
            string targetPath = @".\serverXML.txt";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(sourcePath);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            //request.Credentials = new NetworkCredential("anonymous", "janeDoe@contoso.com");

            using (Stream ftpStream = request.GetResponse().GetResponseStream())
            {
                using (Stream fileStream = File.Create(targetPath))
                {
                    ftpStream.CopyTo(fileStream);
                }
            }
            MessageBox.Show("File downloaded!");
        }

        //Creates an xml file containing paths to all files in the project
        private void btnCreateXML_Click(object sender, RoutedEventArgs e)
        {
            //@".\"
            string path = @"C:\Users\anto6789\Desktop\SKP\AutoUpdate\";
            DirectoryInfo rootPath = new DirectoryInfo(path);
            int prePathLength = rootPath.FullName.ToString().Length - rootPath.Name.Length - 1;

            XmlHandler xmlObj = new XmlHandler(rootPath, prePathLength);
            xmlObj.XmlDoc.Save(@".\clientXML.xml");
            MessageBox.Show("XML file created!");
        }
        private void btnCompareXML_Click(object sender, RoutedEventArgs e)
        {
            //Compares two xml files and finds the differences between them
        }
    }
}
