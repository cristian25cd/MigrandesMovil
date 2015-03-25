using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=234238

namespace Migrandes
{

    public sealed partial class Login : Page
    {

        private static String SERVIDOR = "https://vast-eyrie-7226.herokuapp.com";

        private static String URI_AUTENTICAR = "/login";

        public Login()
        {
            this.InitializeComponent();
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            login();
        }

        private void passwordBox_Loaded(object sender, RoutedEventArgs e)
        {
            cmdBar.IsOpen = true;
        }


        private async void login()
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(SERVIDOR + URI_AUTENTICAR);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            
            using (var stream = await Task.Factory.FromAsync<Stream>(httpRequest.BeginGetRequestStream, httpRequest.EndGetRequestStream, null))
            {
                String postD = "{'usuario':'" + LoginField.Text + "','password':'"+passwordBox.Password+"'}";
                byte[] byteArray = Encoding.UTF8.GetBytes(postD);

                await stream.WriteAsync(byteArray, 0, byteArray.Length);               
            }

            try
            {
                var v = await httpRequest.GetResponseAsync();

                this.Frame.Navigate(typeof(Principal), v);
            }
            catch (WebException e)
            {
                await new Windows.UI.Popups.MessageDialog(e.GetBaseException().Message).ShowAsync();
            }
        }

        private void passwordBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key.ToString().Equals("Enter"))
            {
                AcceptButton_Click(sender, e);
            }
        }
    }
}
