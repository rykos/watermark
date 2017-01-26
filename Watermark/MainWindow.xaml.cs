using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Activities;
using System.Diagnostics;
using System.IO;
using Forms = System.Windows.Forms;

namespace Watermark
{
    public partial class MainWindow : Window
    {
        string ExportPath;
        string[] supportedExtensions = new string[] { ".png", ".jpeg", ".jpg", ".bmp", ".gif"};
        Rectangle watermark = new Rectangle();
        string imagePath, watermarkPath;
        Image watermarkStruct;
        bool drag;
        Point mousePoint;
        List<Image> imagelist = new List<Image>();
        struct Image
        {
            public string extension;
            public string name { get; set; }
            public string path { get; set; }
            public int height;
            public int width;
            public int x;
            public int y;
        }
        enum Type{
            image,
            watermark
        }
        public MainWindow()
        {
            InitializeComponent();
            OnLoad();
        }
        string imagePathBind { get; set; }
        string Bind { get; set; }
        void BETA()
        {
            ImagesListViewer.ItemsSource = imagelist;
        }
        void OnLoad()
        {
            canvas.Children.Add(watermark);
            LoadSettings();
            BETA();
        }
        
        private void setImage_button_Click(object sender, RoutedEventArgs e)
        {
            string[] files = Get_File(sender as Button);
            if (files == null)
            {
                return;
            }
            string path = files[0];
            string name = (sender as Button).Name;
            if (path!=null)
            {
                string extension = System.IO.Path.GetExtension(path);
                if (Array.IndexOf(supportedExtensions, extension.ToLower())==-1)
                {
                    MessageBox.Show("Only images");
                    return;
                }
                if(name == "setWatermark_button")
                {
                    createImageStruct(path, name, Type.watermark); //Create watermark
                    watermark_view.Fill = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(path))
                    };
                    watermark_view.Visibility = Visibility.Visible;
                    watermarkPath = path;
                    loadResult();
                    return;
                }
                image_view.Visibility = Visibility.Visible;
                image_view.Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(path))
                };
                foreach(string filesName in files)
                {
                    string Filepath = filesName; //0,1,2
                    string Filename = System.IO.Path.GetFileNameWithoutExtension(Filepath);
                    createImageStruct(Filepath, Filename, Type.image);
                }
                imagePath = path;
                //createImageStruct(path, name, Type.image); //Create image
                //string filename = System.IO.Path.GetFileNameWithoutExtension(path);
                loadResult();
            }
        }

        string[] Get_File(Button btn)
        {
            string path;
            OpenFileDialog dialog = new OpenFileDialog();
            if (btn.Name == "setImage_button"){
                path = Properties.Settings.Default.ImagePath;
                dialog.Multiselect = true;
            }
            else{
                path = Properties.Settings.Default.WatermarkPath;
            }
            if (Directory.Exists(path)){
                dialog.InitialDirectory = path;
            }
            dialog.DefaultExt = ".png";
            var result = dialog.ShowDialog();
            if (result.Value)
            {
                string[] filenames = dialog.FileNames;
                return filenames;
                //return dialog.FileName;
            }
            return null;
        }
        void createImageStruct(string path, string name, Type type)
        {
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(path);
            if(type == Type.image)
            { 
                Image imageStruct = new Image();
                imageStruct.extension = System.IO.Path.GetExtension(path);
                imageStruct.name = System.IO.Path.GetFileNameWithoutExtension(path);
                imageStruct.path = path;
                imageStruct.height = image.Height;
                imageStruct.width = image.Width;
                imagelist.Add(imageStruct);
                listView.Items.Add(imageStruct.name);
                //
                ImagesListViewer.ItemsSource = imagelist;
                ImagesListViewer.Items.Refresh();
            }
            else
            {
                watermarkStruct.name = name;
                watermarkStruct.path = path;
                watermarkStruct.height = image.Height;
                watermarkStruct.width = image.Width;
            }
        }
        string Get_Path(string name)
        {
            foreach (Image element in imagelist)
            {
                if(element.name == name)
                {
                    return element.path;
                }
            }
            return null;
        }
        private void result_view_button_Click(object sender, RoutedEventArgs e)
        {
            result_view.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(imagePath))
            };
            result_view.Visibility = Visibility.Visible;
            createWatermark();
        }
        void createWatermark()
        {
            watermark.Height = 100;
            watermark.Width = 100;
            watermark.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(watermarkStruct.path))
            };
            Canvas.SetTop(watermark, watermarkStruct.y);
            Canvas.SetLeft(watermark, watermarkStruct.x);
            watermark.MouseMove += moveWatermark;
            watermark.MouseLeftButtonDown += delegate{ drag = true; };
            watermark.MouseLeftButtonUp += delegate{ drag = false; };
        }
        void loadResult()
        {
            if(imagelist.Count < 1 || watermarkStruct.path == null)
            {
                return;
            }
            result_view.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(imagePath))
            };
            result_view.Visibility = Visibility.Visible;
            createWatermark();
        }
        void moveWatermark(object sender, RoutedEventArgs e)
        {
            if (drag == false)
            {
                return;
            }
            //30
            mousePoint = Mouse.GetPosition(Application.Current.MainWindow);
            double waterX = Convert.ToInt32(mousePoint.X) - 561;
            double waterY = Convert.ToInt32(mousePoint.Y) - 64;
            if (mousePoint.X > 561 && mousePoint.X < 1200 - watermark.Width)
            {
                Canvas.SetLeft(watermark, Convert.ToDouble(mousePoint.X) - 561);
                int x = (Convert.ToInt32(mousePoint.X) - 531);
                watermarkStruct.x = x - 30;
            }
            if (mousePoint.Y > 64 && mousePoint.Y < 325)
            {
                Canvas.SetTop(watermark, Convert.ToDouble(mousePoint.Y) - 64);
                int y = (Convert.ToInt32(mousePoint.Y) - 34);
                watermarkStruct.y = y - 30;
            }
            DebugLabel.Content = watermarkStruct.x+"/"+watermarkStruct.y;
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string name = e.AddedItems[0].ToString();
            string path = Get_Path(name);
            drawIcon(path);
        }
        void drawIcon(string path)
        {
            try
            {
                result_view.Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(path))
                };
            }
            catch
            {

            }
        }
        int ValidIndex = 0;
        void Merge_Images(Image image, Image watermark, string savepath)
        {
            System.Drawing.Image obrazek = System.Drawing.Image.FromFile(image.path);
            System.Drawing.Image znak = System.Drawing.Image.FromFile(watermark.path);
            System.Drawing.Bitmap res = new System.Drawing.Bitmap( image.width, image.height);
            double x = watermark.x;
            x /= 640;
            double y = watermark.y;
            y /= 360;
            x *= obrazek.Width;
            y *= obrazek.Height;
            Math.Round(x);
            Math.Round(y);
            x += 100;
            y -= 100;
            using (var g = System.Drawing.Graphics.FromImage(res))
            {
                g.DrawImage(obrazek, 0, 0);
                g.DrawImage(znak, (float)x, (float)y);
            }
            SaveImage(res, savepath, image);
        }
        void SaveImage(System.Drawing.Bitmap result, string savepath, Image image)
        {
            bool Errors = false;
            string path = savepath + @"\" + image.name;
            string imageext = image.extension;
            var file = File.Exists((path + imageext));
            while (file==true)
            {
                ValidIndex++;
                file = File.Exists(path + ValidIndex.ToString() + imageext);
                Errors = true;
            }
            if (Errors == true)
            {
                result.Save(savepath + @"\" + image.name + ValidIndex + image.extension);
            }
            result.Save(savepath + @"\" + image.name + image.extension);
            ValidIndex = 0;
        }
        private void button_Click(object sender, RoutedEventArgs e) //EXPORT
        {
            string savepath = saveLocation();
            if(savepath == null)
            {
                return;
            }
            foreach (Image img in imagelist)
            {
                Merge_Images(img, watermarkStruct, savepath);
            }
            SaveSettings();
        }
        string saveLocation()
        {
            SaveFileDialog savedialog = new SaveFileDialog();
            if(Directory.Exists(ExportPath))
            {
                savedialog.InitialDirectory = ExportPath;
            }
            savedialog.FileName = "Save Here";
            if (savedialog.ShowDialog() == true)
            {
                ExportPath = System.IO.Path.GetDirectoryName(savedialog.FileName);
                return ExportPath;
            }
            return null;
        }
        void SaveSettings()
        {
            string ImagePath = System.IO.Path.GetDirectoryName(imagelist[0].path);
            Properties.Settings.Default.ImagePath = ImagePath;
            Properties.Settings.Default.WatermarkPath = watermarkStruct.path;
            Properties.Settings.Default.WatermarkPositionX = watermarkStruct.x;
            Properties.Settings.Default.WatermarkPositionY = watermarkStruct.y;
            Properties.Settings.Default.ExportPath = ExportPath;
            Properties.Settings.Default.Save();
        }
        void LoadSettings()
        {
            try
            {
                var properties = Properties.Settings.Default;
                if (SettingsExist() == false)
                {
                    return;
                }
                watermarkStruct.x = Convert.ToInt32(properties.WatermarkPositionX);
                watermarkStruct.y = Convert.ToInt32(properties.WatermarkPositionY);
                ExportPath = properties.ExportPath;
            }
            catch
            {

            }
        }
        bool SettingsExist()
        {
            var properties = Properties.Settings.Default;
            if (properties.WatermarkPath == null || properties.WatermarkPositionX < 0 || properties.WatermarkPositionY < 0 || properties.ImagePath==null)
            {
                return false;
            }
            return true;
        }
    }
}
