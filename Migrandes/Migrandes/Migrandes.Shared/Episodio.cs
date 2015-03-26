using System;
using System.Collections.Generic;
using System.Text;

namespace Migrandes
{
    class Episodio
    {
        private String fecha;

        private int intensidad;

        private String ubicacion;

        private String descripcion;

        private List<Medicamento> medicamentos;

        private String notaVoz;
    
        public string Fecha
        {
            get
            {
                return fecha;
            }

            set
            {
                fecha = value;
            }
        }

        public int Intensidad
        {
            get
            {
                return intensidad;
            }

            set
            {
                intensidad = value;
            }
        }

        public string Ubicacion
        {
            get
            {
                return ubicacion;
            }

            set
            {
                ubicacion = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return descripcion;
            }

            set
            {
                descripcion = value;
            }
        }

        internal List<Medicamento> Medicamentos
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

        public string NotaVoz
        {
            get
            {
                return notaVoz;
            }

            set
            {
                notaVoz = value;
            }
        }

        public Episodio(String fecha, int intensidad, String ubicacion, String descripcion, List<Medicamento> medicamentos, String notaVoz)
        {

        }
    }
}
