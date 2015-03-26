using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;
using Windows.Storage.Pickers;
using Windows.Storage;

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=234238

namespace Migrandes
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Principal : Page
    {
        private MediaCapture CaptureMedia;
        private IRandomAccessStream AudioStream;
        private FileSavePicker FileSave;
        private DispatcherTimer DishTimer;
        private TimeSpan SpanTime;
  

        public Principal()
        {
            this.InitializeComponent();
            
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            Cliente c = (Cliente)Resources["cliente"];
            c =(Cliente) e.Content;
            
            base.OnNavigatedTo(e);
        }

        //Eventos

        void Hub_SectionHeaderClick(object sender, HubSectionHeaderClickEventArgs e)
        {
            HubSection section = e.Section;
            var group = section.DataContext;
            this.Frame.Navigate(typeof(SectionPage));
        }

        /// <summary>
        /// Se invoca al hacer clic en un elemento contenido en una sección.
        /// </summary>
        /// <param name="sender">Objeto GridView o ListView
        /// que muestra el elemento en el que se hizo clic.</param>
        /// <param name="e">Datos de evento que describen el elemento en el que se hizo clic.</param>
        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navegar a la página de destino adecuada y configurar la nueva página
            // al pasar la información requerida como parámetro de navegación
            this.Frame.Navigate(typeof(ItemPage), e.ClickedItem);
        }

        private void notaVozAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            grabar();
        }

        private async void grabar()
        {
            MediaEncodingProfile encodingProfile = MediaEncodingProfile.CreateMp3(AudioEncodingQuality.High);
            AudioStream = new InMemoryRandomAccessStream();
            await CaptureMedia.StartRecordToStreamAsync(encodingProfile, AudioStream);
            DishTimer.Start();
        }

        private async void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            await CaptureMedia.StopRecordAsync();
            DishTimer.Stop();
        }

        private async void guardarAudio()
        {
            var mediaFile = await FileSave.PickSaveFileAsync();

            if (mediaFile != null)
            {
                using (var dataReader = new DataReader(AudioStream.GetInputStreamAt(0)))
                {
                    await dataReader.LoadAsync((uint)AudioStream.Size);
                    byte[] buffer = new byte[(int)AudioStream.Size];
                    dataReader.ReadBytes(buffer);
                    await FileIO.WriteBytesAsync(mediaFile, buffer);
                }
            }
        }

        private void guardarNewNotaVozAppBarButton_Click(object sender, RoutedEventArgs e)
        {


        }

        private void LimpiarAppBarButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
