using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsFc
{
    public class Proceso
    {
        private int estado;
        public int GSestado
        {
            get
            {
                return this.estado;
            }

            set
            {
                this.estado = value;
            }
        }

        private string nombre;
        public string GSnombre
        {
            get
            {
                return this.nombre;
            }
            set
            {
                this.nombre = value;
            }
        }

        private int tiempoLLegada;
        public  int GSTiempoLLegada
        {
            set
            {
                this.tiempoLLegada = value;
            }

            get
            {
                return this.tiempoLLegada;
            }

        }

        public int Tinicio;
        public int Tfinal;
        public int Tretorno;

        private int duracion;
        public int GSduracion
        {
            set
            {
                this.duracion = value;
            }
            get
            {
                return this.duracion;
            }
        }

        public int faltate;
        public int tEspera;

        public Proceso()
        {
            /* 0-en espera, 1-ejecucion 2-listo  -1 -bloqueado 4-listo*/
            this.estado = 0;
            this.nombre = " ";
            this.duracion = 0;

            int d = this.duracion;
            this.faltate = d;
            this.tiempoLLegada = 0;
            this.tEspera = 0;
            Tfinal = 0;
            Tinicio = 0;
            Tretorno = 0;
        }


    }
}
