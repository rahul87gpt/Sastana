using MahApps.Metro.Controls;
using NPOI.Util;
using SastanaArt.APIModels.ResponseModel;
using SastanaArt.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SastanaArt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        LoginResponse loginData;
        public MainWindow(LoginResponse login)
        {
            InitializeComponent();
            loginData = login;
            lblUserName.Content = $"{login.data.first_name} {login.data.last_name}";
            lbldevice.Content = login.data.device_number;


            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(30);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            await LoadImagesAsync();
        }

        private  void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
             TouchImage();
           // getinfo();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadImagesAsync();
             TouchImage();
        }

        private  void TouchImage()
        {

            var visble = dckHeader.Visibility;
            if (visble.ToString() == "Visible")
            {
                
                dckHeader.Visibility = Visibility.Collapsed;
                //imgSlider.Visibility=Visibility.Collapsed;
               
            }
            else 
            {
                dckHeader.Visibility = Visibility.Visible;
               // imgSlider.Visibility = Visibility.Visible;
            }

        }

        //private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    TouchImage();
        //}

        private async Task LoadImagesAsync()
        {
            var model = new
            {
               // user_id = loginData.data.id,
                id= loginData.data.device_id

            };

            var result = await Helpers.ServiceClient.PostAsync<object, NFTResponse>(model, Constants.Devicesdetail);

            if(result.code == 200)
            {
                SliderStart(result.data);
                if (result.data.Count > 0)
                {
                    lblUserName.Content = result.data[0].user_id.username;
                    Miscellaneous.Content = result.data[0].user_id.wallet_details;
                }
            }
        }

        private void SliderStart(List<Datum> datum)
        {

            //while (true)
            {
                foreach (var item in datum)
                {
                    if (item.nft_id != null)
                    {
                        // Create source

                        try
                        {
                              BitmapImage myBitmapImage = new BitmapImage();
                              // BitmapImage.UriSource must be in a BeginInit/EndInit block
                                myBitmapImage.BeginInit();
                               myBitmapImage.UriSource = new Uri(item.nft_id.image_link);
                          //  myBitmapImage.UriSource = new Uri("pack://application:,,,/SastanaArt;component/Assets/Icon/gh.jfif");

                            myBitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                               myBitmapImage.EndInit();

                            // Create the TransformedBitmap to use as the Image source.
                            TransformedBitmap tb = new TransformedBitmap();

                            // Properties must be set between BeginInit and EndInit calls.
                            tb.BeginInit();
                            tb.Source = myBitmapImage;
                            // Set image rotation.
                            RotateTransform transform = new RotateTransform(item.rotation);
                            tb.Transform = transform;
                            tb.EndInit();
                            imgSlider.Source = tb;
                        }
                        catch 
                        {
                            imgSlider.Source = null;
                        }
                       
                        int[] array = item.user_id.avatar_img.data.ToArray();
                        byte[] bytes = ConvertIntArrayToByteArray(array);
                        imguser.Source = ConvertByteArrayToBitMapImage(bytes);
                        if (item.contain_mode.ToLower() == "contain")
                           imgSlider.Stretch = Stretch.None;
                        else if (item.contain_mode.ToLower() == "fill")
                            imgSlider.Stretch = Stretch.Fill;
                        else
                            imgSlider.Stretch = Stretch.Uniform;
                     }
                    else 
                    {
                        imgSlider.Source = null;
                    }
                }
            }
        }

        private async void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
           await logout();
        }

        private async Task logout() 
        {
            var model = new
            {
                device_id = loginData.data.device_id.ToString()
            };

            var result = await Helpers.ServiceClient.PostAsync<object, Logout>(model, Constants.Logout);
       
            if(result.code==200)
            {
                ServiceClient.ClearToken();
                Views.Login login = new Views.Login();
                login.Show();

                this.Close();
            }
        
        }

        public byte[] ConvertIntArrayToByteArray(int[] inputElements)
        {
            byte[] bytes = inputElements.Select(i => (byte)i).ToArray();
            return bytes;
        }

        public BitmapImage ConvertByteArrayToBitMapImage(byte[] imageByteArray)
        {
            BitmapImage img = new BitmapImage();
            using (MemoryStream memStream = new MemoryStream(imageByteArray))
            {
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.StreamSource = memStream;
                img.EndInit();
                img.Freeze();
            }
            return img;
        }

        public void getinfo()
        {
            List<string> items = new List<string>();
            var DiskDrive = "DiskDrive";
             ManagementObjectSearcher mSearchObj = new ManagementObjectSearcher("SELECT * FROM  " + DiskDrive);
            ManagementObjectCollection objCollection = mSearchObj.Get();

            foreach (ManagementObject mObject in objCollection)
            {
                foreach (PropertyData property in mObject.Properties)
                {
                    items.Add(string.Format("{0}:- {1}", property.Name, property.Value));
                }
            }

            //SystemInfoListBox.ItemsSource = items;
        }



    }
}
