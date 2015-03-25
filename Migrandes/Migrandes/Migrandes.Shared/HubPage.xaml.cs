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
using Migrandes.Data;
using Migrandes.Common;

// La plantilla de proyecto Aplicación Hub universal está documentada en http://go.microsoft.com/fwlink/?LinkID=391955

namespace Migrandes
{
    /// <summary>
    /// Página en la que muestra una colección de elementos agrupados.
    /// </summary>
    public sealed partial class HubPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// Obtiene la clase NavigationHelper que se usa para ayudar con la navegación y la administración de la duración de los procesos.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Obtiene DefaultViewModel. Este puede cambiarse a un modelo de vista fuertemente tipada.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public HubPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
        }

        /// <summary>
        /// Rellena la página con el contenido pasado durante la navegación.  Cualquier estado guardado se
        /// proporciona también al crear de nuevo una página a partir de una sesión anterior.
        /// </summary>
        /// <param name="sender">
        /// El origen del evento; suele ser <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Datos de evento que proporcionan tanto el parámetro de navegación pasado a
        /// <see cref="Frame.Navigate(Type, object)"/> cuando se solicitó inicialmente esta página y
        /// un diccionario del estado mantenido por esta página durante una sesión
        /// anterior. El estado será null la primera vez que se visite una página.</param>
        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
#if DESIGN_MODE
            var sampleDataGroup = await SampleDataSource.GetGroupAsync("Group-4");
            this.DefaultViewModel["Section3Items"] = sampleDataGroup;
#endif
        }

        /// <summary>
        /// Se invoca al hacer clic en un encabezado HubSection.
        /// </summary>
        /// <param name="sender">Hub que contiene el objeto HubSection en cuyo encabezado se hizo clic.</param>
        /// <param name="e">Datos de evento que describen cómo se inició el clic.</param>
        void Hub_SectionHeaderClick(object sender, HubSectionHeaderClickEventArgs e)
        {
            HubSection section = e.Section;
            var group = section.DataContext;
            this.Frame.Navigate(typeof(SectionPage), ((SampleDataGroup)group).UniqueId);
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
            var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
            this.Frame.Navigate(typeof(ItemPage), itemId);
        }
    }
}
