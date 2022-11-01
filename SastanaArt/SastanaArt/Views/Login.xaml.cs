using MahApps.Metro.Controls;
using SastanaArt.APIModels.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SastanaArt.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : MetroWindow
    {
        public bool Checked { get; set; }
        public Login()
        {
           
            InitializeComponent();
            txtUser.Text = SastanaArt.Properties.Settings.Default.UserName;
            txtpwd.Password = SastanaArt.Properties.Settings.Default.Password;
            if (txtUser.Text != "")
            chkRemember.IsChecked = true;
            else
                chkRemember.IsChecked = false;
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string userName = string.Empty;
                string password = string.Empty;
                lblError.Content = "";

                int  screenWidth = Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenWidth);
                int screenHeight = Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenHeight);
                var Resolution = $"Resolution :{screenWidth} X {screenHeight}";
               string Aspectratio =  string.Format("{0}:{1}", screenWidth / GCD(screenWidth, screenHeight), screenHeight / GCD(screenWidth, screenHeight));

               
                Dispatcher.Invoke(() =>
                {
                    userName = txtUser.Text;
                    password = txtpwd.Password;
                });

                var model = new
                {
                    username = userName,
                    password = password,
                    device_name = "THOMSON",
                    os = "windows",
                    type = "windows",
                    app_version = "1",
                    mac_id = "sv2425255245b435643trre54543543543t435435435345",
                    resolution= Resolution,
                    aspect_ratio= Aspectratio,
                    device_token="12345"
                   
                };


                var result = await Helpers.ServiceClient.AuthenticateAsync(model);

                if (result.code == 200)
                {
                    if (chkRemember.IsChecked==true)
                    {
                        SastanaArt.Properties.Settings.Default.UserName = userName;
                        SastanaArt.Properties.Settings.Default.Password = password;
                    }
                    MainWindow mainWindow = new MainWindow(result);
                    mainWindow.Show();
                    await Task.Delay(100);
                    this.Close();
                }
                else
                {
                    if (result.message == "Cannot read properties of null (reading 'id')")
                    {
                        lblError.Content = "Invalid login credentials";
                    }
                    else
                    {
                        lblError.Content = result.message;
                    }
                    
                   // MessageBox.Show(result.message);
                    txtUser.Text = "";
                    txtpwd.Password = "";
                }
            }
            catch (Exception ex)
            {
                lblError.Content = ex.Message;
              //  MessageBox.Show(ex.Message);
                txtUser.Text = "";
                txtpwd.Password = "";
            }
        }

        // helper to hide watermark hint in password field
        private void passwordChanged(object sender, RoutedEventArgs e)
        {
        }


      public int GCD(int a, int b)
        {
            int Remainder;

            while (b != 0)
            {
                Remainder = a % b;
                a = b;
                b = Remainder;
            }

            return a;
        } 
    }
}
