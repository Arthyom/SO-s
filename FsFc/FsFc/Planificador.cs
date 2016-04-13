using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace FsFc
{
    public class Planificador
    {
        /* 1 ocupado, 0 libre */
        private int estado;

        public string[,] gant;

        public int GSestado
        {
            set
            {
                this.estado = value;
            }
            get
            {
                return this.estado;
            }
        }

        public int rafagaCpu = 3;

        public int Quantum;

        // conseguir las estadisticas de la cola 
        public float GetStdstcs(Queue ColaProceso, int NumProc)
        {
            int TProm = 0;
            foreach (Proceso p in ColaProceso)
                TProm += p.tEspera;

            return (float)TProm / NumProc;
        }

        public int Prioridad;
        public int Envejecimiento;

        // buscar al proceso mas corto en una cola de procesos
        public Proceso BuscarMenor(Queue ColaProcesos)
        {

            if (ColaProcesos.Count > 0)
            {
                Proceso procesoMenor = (Proceso)ColaProcesos.Peek();

                while (procesoMenor.GSestado == 4)
                {
                    procesoMenor = (Proceso)ColaProcesos.Peek();
                    if (procesoMenor.GSestado == 4)
                        ColaProcesos.Dequeue();

                }



                if (ColaProcesos.Count > 0)
                    procesoMenor = (Proceso)ColaProcesos.Peek();

                // iterar para cada elemento de la cola 
                foreach (Proceso p in ColaProcesos)
                {
                    if (p.GSduracion < procesoMenor.GSduracion && p.GSestado != 4)
                        procesoMenor = p;
                }

                return procesoMenor;
            }
            return null;

        }

        // encolar un intervalo 
        public void EncolarIntervalor(Queue ColaProc, int Tinicio, int Tfin, Proceso[] VectorProc)
        {
            // encolar un intervalo de procesos y regresarlo por referencia
            foreach (Proceso p in VectorProc)
            {
                if (ColaProc.Contains(p)) continue;

                if (p.GSTiempoLLegada >= Tinicio && p.GSTiempoLLegada <= Tfin && p.GSestado != 4)
                    ColaProc.Enqueue(p);
            }

        }

        // procesar un proceso hasta que se acabe
        public Proceso procesar(Proceso pActual, int espera)
        {
            Proceso pSelec = pActual;
            // procesar hasta que el proceso termine 
            while (pSelec.faltate >= 3)
            {
                pSelec.faltate -= 3;
                pSelec.GSestado = 1;
            }

            //pSelec.tEspera = espera + 1;
            pSelec.GSestado = 4;
            pSelec.faltate = 0;
            this.estado = 0;
            //espera += pSelec.GSduracion ;

            pActual.Tinicio = espera;
            pActual.Tfinal = (pActual.GSduracion + pActual.Tinicio) - 1;

            pActual.tEspera = pActual.Tinicio - (pActual.GSTiempoLLegada - 1);
            pActual.tEspera -= 1;
            pActual.Tretorno = pActual.Tfinal - (pActual.GSTiempoLLegada - 1); ;

            espera += (pActual.GSduracion);

            return pActual;
        }

        public Proceso[] planificarFcFs(Proceso[] pActual)
        {
            Proceso[] atendidos = new Proceso[pActual.Length];
            int espera = 1;


            // procesar el proceso i-esimo y ponerlo en la cola de atendidos 
            for (int i = 0; i < pActual.Length; i++)
            {
                atendidos[i] = procesar(pActual[i], espera);
                espera += atendidos[i].GSduracion;
            }


            return atendidos;


        }

        // generar un gant alterno con la caja de texto 
        public void GantAlterno(Proceso Procesado, int NumProc, int Maximo, int ProcesoIesimo)
        {
            // meter el proceso en el gant 
            for (int j = Procesado.GSTiempoLLegada; j <= Procesado.Tfinal; j++)
            {
                // graficar E's
                if (j < Procesado.Tinicio)
                    this.gant[ProcesoIesimo, j] = "-";
                else
                    this.gant[ProcesoIesimo, j] = "X";
            }
        }

        public Proceso[] PlanificarSJF(Proceso[] vectorProc)
        {
            // declarar una coleccion generica 
            Proceso[] listaSalida = new Proceso[vectorProc.Length];
            Proceso min = vectorProc[0];
            int espera = 1;

            for (int k = 0; k < vectorProc.Length; k++)
            {
                // buscar el minimo para cada elemtneto del vector 
                for (int i = 0; i < vectorProc.Length; i++)
                {
                    min = vectorProc[i];
                    // verifiar que el elemnto exista
                    if (vectorProc[i].GSestado != 4)
                    {
                        // compararlo con todo el vector buscando el minimo 
                        for (int j = 0; j < vectorProc.Length; j++)
                        {
                            // comparar min con V[j] siempre y cuando V[j] exista 
                            if ((min.GSduracion > vectorProc[j].GSduracion) && vectorProc[j].GSestado != 4)
                                min = vectorProc[j];
                        }

                        break;
                    }
                }



                // buscar el elemento minimo en el vector y eliminarlo
                for (int i = 0; i < vectorProc.Length; i++)
                {
                    if (vectorProc[i].GSnombre.CompareTo(min.GSnombre) == 0)
                    {
                        vectorProc[i].GSestado = 4;
                        min.GSestado = 4;
                        break;
                    }
                }

                listaSalida[k] = procesar(min, espera);
                espera += listaSalida[k].GSduracion;
            }
            return listaSalida;
        }

        public static void leerArchivo(Proceso[] vectorProceso, string ruta)
        {
            // leer procesos en el archivo

            // crear un lector de archivo 

            StreamReader lector = new StreamReader(ruta);

            // leer dato del archivo de text linea a linea 
            string linea;
            int[] indxEspacios = new int[4];

            string nombre;
            int prioridad;
            int duracion;
            int inicio;
            int j = 0;
            int k = 0;
            // desplazar la primera linea 
            lector.ReadLine();
            while (k < vectorProceso.Length)
            {
                linea = lector.ReadLine();
                j = 0;
                // buscar espacios 
                for (int i = 0; i < linea.Length; i++)
                {
                    if (linea[i] == ' ')
                    {
                        indxEspacios[j] = i;
                        j += 1;
                    }

                }

                nombre = linea.Substring(0, indxEspacios[0]);
                prioridad = Convert.ToInt16(linea.Substring(indxEspacios[0] + 1, (indxEspacios[1] - indxEspacios[0]) - 1));
                inicio = Convert.ToInt16(linea.Substring(indxEspacios[1] + 1, (indxEspacios[2] - indxEspacios[1]) - 1));
                duracion = Convert.ToInt16(linea.Substring(indxEspacios[2]));

                /// meter al vector
                vectorProceso[k].GSnombre = nombre;
                vectorProceso[k].Prioridad = prioridad;
                vectorProceso[k].GSduracion = duracion;
                vectorProceso[k].faltate = duracion;
                vectorProceso[k].GSTiempoLLegada = inicio;
                k++;
            }

            lector.Close();
        }

        public Queue EliminarProc(Proceso ProcesoDelete, Queue ColaProcesos)
        {
            // buscar en cada proceso en la cola y eliminarlo
            Queue ColaCopia = new Queue(ColaProcesos.Count);
            foreach (Proceso p in ColaProcesos)
            {
                if (p.GSnombre.CompareTo(ProcesoDelete.GSnombre) != 0)
                    ColaCopia.Enqueue(p);
            }
            return ColaCopia;
        }

        public Proceso PlanificarSJF(Queue ColaProc, Proceso[] VectorProc, int TInicio, int TFinal, int TEspera)
        {

            // planificar con sjf 
            while (true)
            {
                // buscar procesos en un intervalo indicado  y encolarlos 
                this.EncolarIntervalor(ColaProc, TInicio, TFinal, VectorProc);

                // buscar al proceso menor en la cola 
                Proceso Menor = this.BuscarMenor(ColaProc);
                if (Menor != null)
                {
                    // procesar al menor y aumentar el rango de duracion
                    this.procesar(Menor, TEspera);

                    // eliminar el proceso de la cola 
                    ColaProc = this.EliminarProc(Menor, ColaProc);


                    return Menor;
                }
                return null;
            }
        }

        // planificar con algoritmo FsFc
        public Proceso PlanificarFcFs(Queue ColaProc, int TEspera)
        {
            Proceso elegido = (Proceso)ColaProc.Peek();
            ColaProc.Dequeue();
            Proceso Procesado = this.procesar(elegido, TEspera);
            return Procesado;
        }

        public Proceso MenorEnTiempo(Queue ColaPro, Proceso ProcesoActual, int Tiempo)
        {
            if (ColaPro.Count == 0)
                return null;

            // crear una cola para los procesos en el tiempo T
            Queue ColaAlterna = new Queue();

            // meter procesos en la cola alterna, para el tiempo T
            foreach (Proceso p in ColaPro)
                if (p.GSTiempoLLegada >= Tiempo && p.GSTiempoLLegada <= Tiempo)
                    ColaAlterna.Enqueue(p);

            // buscar un proceso mas pequeño que p
            Proceso Menor = this.BuscarMenor(ColaAlterna);

            if (ProcesoActual.GSduracion > Menor.GSduracion)
                return Menor;

            return null;

        }

        /*
        public void PlanificarSJFX ( Queue ColaProc, int Tiempo)
        {

            // obtener las dimencioes del gant 
            int DimsGant = 0;
            int proc = ColaProc.Count;
            foreach (Proceso p in ColaProc)
                DimsGant += p.GSTiempoLLegada;

            // dimencionar el gant
            this.gant = new string[ColaProc.Count,DimsGant+1];

            // iterar para cada linea del gant 
            for ( int i = 0; i < DimsGant; i ++)
            {
                for ( int j = 0; j <= DimsGant;  j ++ )
                {
                    Proceso actual = (Proceso)ColaProc.Peek();

                    // coneguir al proceso mas corto en el tiempo T
                    Proceso menor = this.MenorEnTiempo(ColaProc, actual, Tiempo);

                    // si se ha encontrado un proceso mas corto
                    if (menor != null)
                    {
                        MessageBox.Show("se ha encontrado un proceso mas corto"+menor.GSnombre);
                    }
                    else
                    {
                        // llenar el gant, no se ha encontrado ningun proceso mas corto
                        MessageBox.Show("ejecutando" + actual.GSnombre);
                        this.gant[i, Tiempo] = "X";
                        actual.GSduracion -= 1;

                        if (actual.GSduracion < 0)
                            ColaProc.Dequeue();

             

                    }
                    Tiempo++;
                }
               

            }

            for (int i = 0; i < proc; i++)
                for( int j = 0; j < DimsGant; j ++)
                MessageBox.Show(this.gant[i,j].ToString());
            
        }
        */

        // buscar un proceso con menor tiempo o con mayor prioridad
        public Proceso MenorEnTiempo(ArrayList ListaEntrada, int Tiempo)
        {
            ArrayList ListaAlterna = new ArrayList();

            foreach (Proceso p in ListaEntrada)
                if (p.GSTiempoLLegada >= 1 && p.GSTiempoLLegada <= Tiempo)
                    ListaAlterna.Add(p);

            // buscar al proceso mas corto en la lista
            Proceso ProcesoMenor = (Proceso)ListaAlterna[0];
            foreach (Proceso p in ListaAlterna)
                if (p.GSduracion < ProcesoMenor.GSduracion || p.Prioridad > ProcesoMenor.Prioridad)
                    ProcesoMenor = p;

            return ProcesoMenor;
        }

        public Queue PlanificarSJFX(ArrayList ListaEntrada)
        {
            int tiempo = 1;

            Queue colaListo = new Queue();
            ArrayList ListaEstatica = new ArrayList();

            foreach (Proceso p in ListaEntrada)
                ListaEstatica.Add(p);

            while (ListaEntrada.Count > 0)
            {
                // conseguir al proces mas corto para el tiempo T
                Proceso Menor = this.MenorEnTiempo(ListaEntrada, tiempo);

                // meter en el gant
                this.PushGant(Menor, this.ConvertIndx(Menor, ListaEstatica), tiempo);

                // simular la ejecucion del proceso
                Menor.GSduracion -= 1;
                //Menor.faltate -= 1;
                Menor.GSestado = 1;

                if (Menor.Tinicio == 0)
                {
                    Menor.Tinicio = tiempo;
                }

                // quitar al proceso cuando halla terminado
                if (Menor.GSduracion <= 0)
                {

                    Menor.Tfinal = tiempo;
                    ListaEntrada.Remove(Menor);
                    //Menor.tEspera = this.ContarEs(this.ConvertIndx(Menor, ListaEstatica), tiempo);
                    Menor.GSestado = 4;
                    // Menor.GSduracion = duracion;
                    Menor.GSduracion = Menor.faltate;
                    colaListo.Enqueue(Menor);


                }
                tiempo++;
            }


            foreach (Proceso p in colaListo)
            {


                p.tEspera = this.ContarEs(this.ConvertIndx(p, ListaEstatica), tiempo - 1);
                p.Tretorno = this.ContarTodo(this.ConvertIndx(p, ListaEstatica), tiempo);

            }


            return colaListo;
        }

        public void LLenarGant(Proceso Procesado, int Iesimo, int MaxVertil)
        {
            // llenar la fila [Iesima,j] del gant 

            for (int j = 0; j <= MaxVertil; j++)
            {
                if (j < Procesado.GSTiempoLLegada)
                    this.gant[Iesimo, j] = "-";
                else
                    if (j < Procesado.Tinicio)
                    this.gant[Iesimo, j] = "E";
                else
                    if (j >= Procesado.Tinicio && j <= Procesado.Tfinal)
                    this.gant[Iesimo, j] = "X";
            }
        }

        public void PushGant(Proceso Procesado, int Iesimo, int Thrzntl)
        {
            this.gant[Iesimo, Thrzntl] = "x";
          
        }

        public void ImprimirGantTxt(StreamWriter Escritor, int Columnas, int Filas, Proceso[] VectorProc)
        {
            // imprimir el gant en un archivo de texto
            //StreamWriter Escritor = new StreamWriter(@"C: \Users\frodo\Desktop\Planificacion\GantAlterno.txt");

            // escribir con formato
            for (int i = 0; i < Columnas; i++)
            {
                for (int j = 0; j <= Filas; j++)
                    Escritor.Write(this.gant[i, j]);

                Escritor.Write(" ");
                Escritor.WriteLine(VectorProc[i].GSnombre);


            }

        }

        // regresar el indice Iesimo asociado al proceso de entrada en relacion con la coleccion de entrada
        public int ConvertIndx(Proceso ProcesoEntrada, ArrayList ListaEntrada)
        {
            int indx = 0;
            foreach (Proceso p in ListaEntrada)
            {
                // comparar todos los procesos hasta encontrar el proceso de entrada 
                if (string.Compare(ProcesoEntrada.GSnombre, p.GSnombre) == 0)
                {
                    return indx;
                }
                indx++;
            }
            return -1;
        }

        public void InicarGant(int MaximoH, int MaximoV)
        {
            for (int i = 0; i < MaximoH; i++)
                for (int j = 0; j < MaximoV; j++)
                    this.gant[i, j] = " ";
        }

        public int ContarEs(int Iesimo, int Tiempo)
        {
            int es = 0;
            for (int j = 0; j < Tiempo; j++)
            {
                string car = this.gant[Iesimo, j];
                string e = "E";
                if (string.Compare(car, e) == 0)
                    es++;

            }

            return es;
        }

        public int ContarTodo(int Iesimo, int Tiempo)
        {
            int retorno = 0;
            // contar todo Exepto #
            for (int j = 0; j < Tiempo; j++)
                if (this.gant[Iesimo, j] == "E" || this.gant[Iesimo, j] == "x")
                    retorno++;
            return retorno;
        }

        // Planificar con SRTF
        public Queue PlanificarSRFT(ArrayList ListaEntrada)
        {
            int tiempo = 1;

            Queue colaListo = new Queue();
            ArrayList ListaEstatica = new ArrayList();

            foreach (Proceso p in ListaEntrada)
                ListaEstatica.Add(p);

            while (ListaEntrada.Count > 0)
            {
                // conseguir al proces mas corto para el tiempo T
                Proceso Menor = this.MenorEnTiempo(ListaEntrada, tiempo);

                // meter en el gant
                this.PushGant(Menor, this.ConvertIndx(Menor, ListaEstatica), tiempo);

                // simular la ejecucion del proceso
                Menor.GSduracion -= 1;
                //Menor.faltate -= 1;
                Menor.GSestado = 1;

                if (Menor.Tinicio == 0)
                {
                    Menor.Tinicio = tiempo;
                }

                // quitar al proceso cuando halla terminado
                if (Menor.GSduracion <= 0)
                {

                    Menor.Tfinal = tiempo;
                    ListaEntrada.Remove(Menor);
                    //Menor.tEspera = this.ContarEs(this.ConvertIndx(Menor, ListaEstatica), tiempo);
                    Menor.GSestado = 4;
                    // Menor.GSduracion = duracion;
                    Menor.GSduracion = Menor.faltate;
                    colaListo.Enqueue(Menor);


                }
                tiempo++;
            }


            foreach (Proceso p in colaListo)
            {
                p.tEspera = this.ContarEs(this.ConvertIndx(p, ListaEstatica), tiempo - 1);
                p.Tretorno = this.ContarTodo(this.ConvertIndx(p, ListaEstatica), tiempo);

            }


            return colaListo;
        }

        // planificar con RR
        public Queue PlanificarRR(ArrayList ListaEntrada)
        {
            this.Quantum = 4;
            // crear colas de trabajo
            ArrayList ListaEstatica = ListaEntrada;
            Queue ColaeEnTiempo = new Queue();
            Queue ColaTerminados = new Queue();
            int tiempo = 1;
            int borrados = 0;

            // iterar hasta que termine la lista de entrada 
            while( ListaEntrada.Count != borrados)
            {
                // conseguir procesos para el tiempo T
                foreach (Proceso P in ListaEntrada)
                    if (P.GSTiempoLLegada == tiempo)
                        ColaeEnTiempo.Enqueue(P);
                if( ColaeEnTiempo.Count > 0)
                {
                    // tomar al primero de la cola 
                    Proceso ProcesoEnTurno = (Proceso)ColaeEnTiempo.Peek();

                    // ejecutar al proceso hasta que termine el quantum
                    for (int i = 0; i < this.Quantum; i++)
                    {
                        if (ProcesoEnTurno.Tinicio == 0)
                            ProcesoEnTurno.Tinicio = tiempo;
                        this.PushGant(ProcesoEnTurno, this.ConvertIndx(ProcesoEnTurno, ListaEstatica), tiempo-1);

                        ProcesoEnTurno.faltate -= 1;

      

                        // verificar si ha terminado un proceso
                        if (ProcesoEnTurno.faltate == 0)
                        {
                            ColaeEnTiempo.Dequeue();
                            ColaTerminados.Enqueue(ProcesoEnTurno);
                            borrados++;
                            break;
                        }

                        tiempo++;

                        // conseguir procesos para el tiempo T
                        foreach (Proceso P in ListaEntrada)
                            if (P.GSTiempoLLegada == tiempo)
                                ColaeEnTiempo.Enqueue(P);
                    }

                }
                else
                {
                    // conseguir procesos para el tiempo T
                    foreach (Proceso P in ListaEntrada)
                        if (P.GSTiempoLLegada == tiempo)
                            ColaeEnTiempo.Enqueue(P);
                    tiempo++;
                }
                    
            }
            return ColaTerminados;

        }

        public int ContarXs ( int Iesimo, int tiempo)
        {
            int x = 0;
            for (int i = 1; i <= tiempo; i++)
                if (this.gant[Iesimo, i] == "x")
                    return i;
            return -1;
        }

        public Queue PlanificarPrioridad( ArrayList ListaEntrada )
        {
            int tiempo = 1,borrados = 0;
            Queue ColaListos = new Queue();
            ArrayList ListaEnTiempo = new ArrayList();
            ArrayList ListaEstatica = ListaEntrada;


            // iterar hasta que termine la lista de entrada 
            while( ListaEntrada.Count !=  borrados )
            {
                // meter proceso a la lista en el tiempo t
                foreach (Proceso P in ListaEntrada)
                    if (P.GSTiempoLLegada == tiempo)
                        ListaEnTiempo.Add(P);

                // buscar al proceso mas importante 
                Proceso MasImportante = this.MasImportante(ListaEnTiempo);

                // ejecutar al mas importante hasta qe se acabe 
                while( MasImportante.faltate > 0)
                {
                    if (MasImportante.Tinicio == 0)
                        MasImportante.Tinicio = tiempo;
                    this.PushGant(MasImportante, this.ConvertIndx(MasImportante, ListaEstatica), tiempo);

                    MasImportante.faltate -= 1;
                    tiempo++;

                    // meter proceso a la lista en el tiempo t
                    foreach (Proceso P in ListaEntrada)
                        if (P.GSTiempoLLegada == tiempo)
                            ListaEnTiempo.Add(P);
                }

                // conseguir datos estadisticos 
                MasImportante.Tfinal = tiempo - 1 ;
                MasImportante.Tretorno = this.ContarTodo( this.ConvertIndx(MasImportante, ListaEstatica), tiempo);
                MasImportante.tEspera = this.ContarEs(this.ConvertIndx(MasImportante, ListaEstatica), tiempo);

                // meter en la cola de listos
                ColaListos.Enqueue(MasImportante);

                // eliminar de las lista 
                ListaEnTiempo.Remove(MasImportante);
                //  ListaEntrada.Remove(MasImportante);
                borrados++;

            }
            return ColaListos;

        }

        public Proceso MasImportante (ArrayList ListaEntrada)
        {
            if (ListaEntrada.Count == 0)
                return null;
            // buscar al proceso mas importante en la lista
            Proceso ProcesoMenor = (Proceso)ListaEntrada[0];
            foreach (Proceso p in ListaEntrada)
                if ( p.Prioridad < ProcesoMenor.Prioridad)
                    ProcesoMenor = p;
            return ProcesoMenor;
        }

    }

    
}