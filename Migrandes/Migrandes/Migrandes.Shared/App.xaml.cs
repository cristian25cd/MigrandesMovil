using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Migrandes.Common;
using Windows.UI.Xaml.Media.Imaging;

// La plantilla de proyecto Aplicación Hub universal está documentada en http://go.microsoft.com/fwlink/?LinkID=391955

namespace Migrandes
{
    /// <summary>
    /// Proporciona un comportamiento específico de la aplicación para complementar la clase Application predeterminada.
    /// </summary>
    public sealed partial class App : Application
    {
#if WINDOWS_PHONE_APP
        private TransitionCollection transitions;
#endif

        /// <summary>
        /// Inicializa la instancia Singleton de la clase <see cref="App"/>. Esta es la primera línea de código creado
        /// ejecutado y, como tal, es el equivalente lógico de main() o WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
        }

        /// <summary>
        /// Se invoca cuando la aplicación la inicia normalmente el usuario final. Se usarán otros puntos
        /// de entrada cuando la aplicación se inicie para abrir un archivo específico, para mostrar
        /// resultados de la búsqueda, etc.
        /// </summary>
        /// <param name="e">Información detallada acerca de la solicitud y el proceso de inicio.</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // No repetir la inicialización de la aplicación si la ventana tiene contenido todavía,
            // solo asegurarse de que la ventana está activa.
            if (rootFrame == null)
            {
                // Crear un marco para que actúe como contexto de navegación y navegar a la primera página.
                rootFrame = new Frame();

                rootFrame.Background = new ImageBrush
                {
                    Stretch = Windows.UI.Xaml.Media.Stretch.UniformToFill,
                    ImageSource =
              new BitmapImage { UriSource = new Uri("ms-appx:///Assets/bg.jpg") }
                };




                //Asociar el marco con una clave SuspensionManager.                                
                SuspensionManager.RegisterFrame(rootFrame, "AppFrame");

                // TODO: Cambiar este valor a un tamaño de caché adecuado para la aplicación
                rootFrame.CacheSize = 1;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // Restaurar el estado de sesión guardado solo si procede
                    try
                    {
                        await SuspensionManager.RestoreAsync();
                    }
                    catch (SuspensionManagerException)
                    {
                        // Se produjo un error al restaurar el estado.
                        // Asumir que no hay estado y continuar.
                    }
                }

                // Poner el marco en la ventana actual.
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
#if WINDOWS_PHONE_APP
                // Quita la navegación de transición en el inicio.
                if (rootFrame.ContentTransitions != null)
                {
                    this.transitions = new TransitionCollection();
                    foreach (var c in rootFrame.ContentTransitions)
                    {
                        this.transitions.Add(c);
                    }
                }

                rootFrame.ContentTransitions = null;
                rootFrame.Navigated += this.RootFrame_FirstNavigated;
#endif

                // Cuando no se restaura la pila de navegación para navegar a la primera página,
                // configurar la nueva página al pasar la información requerida como parámetro
                // de navegación
                if (!rootFrame.Navigate(typeof(Login), e.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            // Asegurarse de que la ventana actual está activa.
            Window.Current.Activate();
        }

#if WINDOWS_PHONE_APP
        /// <summary>
        /// Restaura las transiciones de contenido después de iniciar la aplicación.
        /// </summary>
        /// <param name="sender">Objeto al que se asocia el controlador.</param>
        /// <param name="e">Detalles sobre el evento de navegación.</param>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        }
#endif

        /// <summary>
        /// Se invoca al suspender la ejecución de la aplicación.  El estado de la aplicación se guarda
        /// sin saber si la aplicación se terminará o se reanudará con el contenido
        /// de la memoria aún intacto.
        /// </summary>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }
    }
}