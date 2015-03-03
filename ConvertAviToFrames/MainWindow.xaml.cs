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
using AForge.Video.FFMPEG;
using System.Drawing;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ConvertAviToFrames
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string srcPath;
        private string dstPath;

        public MainWindow()
        {
            InitializeComponent();
            
            
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.Filter = "Archivos de video (.avi)|*.avi";
            openFileDialog1.FilterIndex = 1;
            bool? userClickedOK = openFileDialog1.ShowDialog();
            if (userClickedOK == true)
            {
                srcPath = openFileDialog1.FileName;
                txtOpen.Text = srcPath;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            DialogResult result = folder.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                dstPath = folder.SelectedPath;
                txtSave.Text = dstPath;
            }

        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            VideoFileReader reader = new VideoFileReader();
            reader.Open(srcPath);
            Console.WriteLine("width:  " + reader.Width);
            Console.WriteLine("height: " + reader.Height);
            Console.WriteLine("fps:    " + reader.FrameRate);
            Console.WriteLine("codec:  " + reader.CodecName);
            Console.WriteLine("nFrames:  " + reader.FrameCount);
            for (int i = 0; i < reader.FrameCount; i++)
            {
                Bitmap videoFrame = reader.ReadVideoFrame();
                videoFrame.Save(dstPath + "\\" + i + ".bmp", ImageFormat.Bmp);
                videoFrame.Dispose();
            }
            System.Windows.MessageBox.Show("El proceso ha terminado correctamente", "Proceso terminado", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
