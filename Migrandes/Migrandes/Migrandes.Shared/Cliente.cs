using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace Migrandes
{
    public class Cliente
    {
        private static String SERVIDOR = "https://boiling-dusk-7953.herokuapp.com";
        //GET
        private static String URI_EPISODIO_COMPLETO = "/paciente/episodioCompleto?id=";
        private static String URI_TODOS_LOS_EPISODIOS = "/paciente/getAllEpisodios?id=";
        private static String URI_VER_EPISODIOS_RANGO_FECHAS = "/paciente/getEpisodios";
        private static String URI_MEDIAMENTOS_PACIENTE = "/paciente/medicamentos?id=";

        //POST
        private static String URI_CREAR_PACIENTE = "/paciente/new";
        private static String URI_CREAR_EPISODIO = "/episodio/new";
        private static String URI_CREAR_EPISODIO_VOZ = "/episodio/newVoz";
        private static String URI_CREAR_DOCTOR = "/doctor";
        private static String URI_AGREGAR_MEDICAMENTO = "/paciente/medicamento/add?id=";
        //DELETE
        private static String URI_ELIMINAR_PACIENTE = "/paciente?id=";

        private String usuario;
        private String nombres;
        private String password;
        private ObservableCollection<Medicamento> medicamentos;
        private ObservableCollection<Actividad> actividades;
        private ObservableCollection<Episodio> episodios;
        private String foto;

        public string Usuario
        {
            get
            {
                return usuario;
            }

            set
            {
                usuario = value;
            }
        }

        public string Nombres
        {
            get
            {
                return nombres;
            }

            set
            {
                nombres = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public string Foto
        {
            get
            {
                return foto;
            }

            set
            {
                foto = value;
            }
        }

        internal ObservableCollection<Medicamento> Medicamentos
        {
            get
            {
                return medicamentos;
            }

            set
            {
                medicamentos = value;
            }
        }

        internal ObservableCollection<Actividad> Actividades
        {
            get
            {
                return actividades;
            }

            set
            {
                actividades = value;
            }
        }

        internal ObservableCollection<Episodio> Episodios
        {
            get
            {
                return episodios;
            }

            set
            {
                episodios = value;
            }
        }





        //TODO Lista medicamentos, actividades, episodios

        public Cliente()
        {
            Medicamentos = new ObservableCollection<Medicamento>();
            Actividades = new ObservableCollection<Actividad>();
            Episodios = new ObservableCollection<Episodio>();
        }
        public Cliente(String userName, String password)
        {
            usuario = userName;
            this.Password = password;
        }

        //GET METHODS
        public async void getEpisodioCompleto(String id)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(SERVIDOR + URI_EPISODIO_COMPLETO + id);
            httpRequest.ContentType = "text/json";
            httpRequest.Method = "GET";
            httpRequest.Accept = "application/json;odata=verbose";

            HttpWebResponse resp = (HttpWebResponse)await httpRequest.GetResponseAsync();

            Debug.WriteLine(resp.ContentType);

        }
        //POST METHODS

        public async void createEpisodioVoz(String notaVoz)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(SERVIDOR + URI_CREAR_EPISODIO);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            using (var stream = await Task.Factory.FromAsync<Stream>(httpRequest.BeginGetRequestStream,
                                                         httpRequest.EndGetRequestStream, null))
            {
                String postD = "{\"notaVoz\":\"" + notaVoz + "\"}";
                byte[] byteArray = Encoding.UTF8.GetBytes(postD);

                await stream.WriteAsync(byteArray, 0, byteArray.Length);

            }
        }
        public async void createEpisodio(String fecha)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(SERVIDOR + URI_CREAR_EPISODIO);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            using (var stream = await Task.Factory.FromAsync<Stream>(httpRequest.BeginGetRequestStream,
                                                         httpRequest.EndGetRequestStream, null))
            {
                String postD = "{\"fecha\":\""+fecha+"\"}";
                byte[] byteArray = Encoding.UTF8.GetBytes(postD);

                await stream.WriteAsync(byteArray, 0, byteArray.Length);

            }

        }

        public async void createPaciente(String id, String nombre, String usuario, String perfil, String foto, String telefono)
        {


            var httpRequest = (HttpWebRequest)WebRequest.Create(SERVIDOR + URI_CREAR_PACIENTE);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            using (var stream = await Task.Factory.FromAsync<Stream>(httpRequest.BeginGetRequestStream,
                                                         httpRequest.EndGetRequestStream, null))
            {
                String postD = "{\"id\":\"" + id + "\",\"nombres\":\"" + nombre + "\",\"login\":\"" + usuario + "\",\"perfil\":\"" + perfil + "\",\"foto\":\"" + foto + "\",\"telefono\":\""+telefono+"\"}";
                byte[] byteArray = Encoding.UTF8.GetBytes(postD);

                await stream.WriteAsync(byteArray, 0, byteArray.Length);

            }

        }

        public async void createDoctor(String id, String nombre, String usuario, String perfil, String foto)
        {


            var httpRequest = (HttpWebRequest)WebRequest.Create(SERVIDOR + URI_CREAR_DOCTOR);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            using (var stream = await Task.Factory.FromAsync<Stream>(httpRequest.BeginGetRequestStream,
                                                         httpRequest.EndGetRequestStream, null))
            {
                String postD = "{\"id\":\"" + id + "\",\"nombres\":\"" + nombre + "\",\"login\":\"" + usuario + "\",\"perfil\":\"" + perfil + "\",\"foto\":\"" + foto + "\"}";
                byte[] byteArray = Encoding.UTF8.GetBytes(postD);

                await stream.WriteAsync(byteArray, 0, byteArray.Length);

            }

        }

        private DateTime darFecha()
        {
            return DateTime.Now;
        }
    }
}
