using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=234238

namespace Migrandes
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Principal : Page
    {
        private Cliente usr;
        public Principal()
        {
            this.InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            usr = new Cliente();
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

    }
}
