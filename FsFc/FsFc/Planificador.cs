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
            if ( Thrzntl == 0 )
                this.gant[Iesimo, Thrzntl] = "x";
            else
            {
                if (  Thrzntl > 0 )
                    this.gant[Iesimo, Thrzntl] = "x";
            }
            
            for( int i = 0; i < Procesado.GSTiempoLLegada -1; i ++ )
                if (this.gant[Iesimo, i] != "x")
                    this.gant[Iesimo, i] = ".";
          
            for (int i = 0; i < Procesado.Tinicio; i++)
                if (this.gant[Iesimo, i] != "x" && this.gant[Iesimo, i ] != "." )
                    this.gant[Iesimo, i] = "E"; 

            for (int i = Thrzntl-1; i > 0; i--)
            {
                if (this.gant[Iesimo, i - 1] == "-" && this.gant[Iesimo, i - 1] != "." && this.gant[Iesimo, i - 1] != "x")  
                    this.gant[Iesimo, i] = "E";
                 
            }

            for (int i = 0; i < Thrzntl-1; i++)
            {
                if (this.gant[Iesimo, i + 1] == "-" && this.gant[Iesimo, i + 1] != "."  && this.gant[Iesimo, i + 1] != "x") 
                    this.gant[Iesimo, i+1] = "E";

            }
        }

        public void ImprimirGantTxt(StreamWriter Escritor, int Columnas, int Filas, Proceso[] VectorProc)
        {
            // imprimir el gant en un archivo de texto
            //StreamWriter Escritor = new StreamWriter(@"C: \Users\frodo\Desktop\Planificacion\GantAlterno.txt");

            // escribir con formato
            for (int i = 0; i < Columnas; i++)
            {
                for (int j = 0; j < Filas; j++)
                    Escritor.Write(this.gant[i, j]);

                Escritor.Write(" ");
                Escritor.WriteLine(VectorProc[i].GSnombre);


            }
            Escritor.Close();

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
                    this.gant[i, j] = "-";
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
            this.Quantum = 2;

            ArrayList ListaEstatica = new ArrayList();
            Queue ColaEnTiempo = new Queue();
            Queue ColaListos = new Queue();

            foreach (Proceso k in ListaEntrada)
                ListaEstatica.Add(k);

            int tiempo = 1;

            // iterar hasta qe terminen los procesos 
            while( ListaEntrada.Count > 0)
            {
                // meter procesos en la cola para el tiempo T
                foreach (Proceso Proc in ListaEntrada)
                    if (Proc.GSTiempoLLegada == tiempo && !ColaEnTiempo.Contains(Proc) )   
                        ColaEnTiempo.Enqueue(Proc);

                // ejecitar un proceso si es que hay minimo 1
                if (ColaEnTiempo.Count > 0)
                {
                    Proceso ProcesoEnTurno = (Proceso)ColaEnTiempo.Dequeue();

                    // ejecutar por todo el quantum
                    for ( int i = 0; i < this.Quantum; i ++)
                    {
                        if (ProcesoEnTurno.Tinicio == 0)
                            ProcesoEnTurno.Tinicio = tiempo;
                        this.PushGant(ProcesoEnTurno, this.ConvertIndx(ProcesoEnTurno, ListaEstatica), tiempo - 1);
                        ProcesoEnTurno.faltate -= 1;

                        if( ProcesoEnTurno.faltate == 0 )
                        {
                            // el proceso ha terminado 
                            ProcesoEnTurno.Tfinal = tiempo;
                            ProcesoEnTurno.tEspera = this.ContarEs(this.ConvertIndx(ProcesoEnTurno, ListaEstatica), tiempo );
                            ProcesoEnTurno.Tretorno = this.ContarTodo(this.ConvertIndx(ProcesoEnTurno, ListaEstatica), tiempo );
                            ColaListos.Enqueue(ProcesoEnTurno);
                            ListaEntrada.Remove(ProcesoEnTurno);

                            Queue ncl = new Queue();

                            // copiar la cola y devolver sin el proceso indicado
                            foreach (Proceso b in ColaEnTiempo)
                                if (string.Compare(b.GSnombre, ProcesoEnTurno.GSnombre) != 0)
                                    ncl.Enqueue(b);

                            ColaEnTiempo = ncl;
                        
                            tiempo++;
                            break;

                        }

                        // el proceso no ha terminado, debe seguirse ejecutando, meterlo de nuevo a la cola 
                        if (!ColaEnTiempo.Contains(ProcesoEnTurno) && ProcesoEnTurno.faltate > 0)
                            ColaEnTiempo.Enqueue(ProcesoEnTurno);

                        tiempo++;

                        // el tiempo ha aumentado, meter a todos los procesos que llegaron en el nuevo tiempo t + 1
                        foreach (Proceso p in ListaEntrada)
                            if (p.GSTiempoLLegada == tiempo && ! ColaEnTiempo.Contains(p) && p.faltate > 0  )
                                ColaEnTiempo.Enqueue(p);

                    }
               

                }
                else
                    tiempo++;
            }
            return ColaListos;

            /*
            this.Quantum = 4;
            ArrayList ListaEstatica = ListaEntrada;
            ArrayList ListaEnTiempo = new ArrayList();
            Queue ColaListos = new Queue();
            int borrados = 0;
            int tiempo = 1;

            foreach (Proceso P in ListaEntrada)
                if (P.GSTiempoLLegada == tiempo)
                    ListaEnTiempo.Add(P);

            while( ListaEntrada.Count > borrados )
            {
                if(ListaEnTiempo.Count != 0)
                {
                    // itarear para cada uno de los procesos
                    for (int i = 0; i <= ListaEnTiempo.Count; i++)
                    {
                        Proceso ProcesoEnTurno;

                        // tomar al Iesimo proceso
                        if (i == ListaEnTiempo.Count)
                            ProcesoEnTurno = (Proceso)ListaEnTiempo[i - 1];
                        else
                            ProcesoEnTurno = (Proceso)ListaEnTiempo[i];

                        // porcesarlo por lo que dura el quantum
                        for (int k = 0; k < Quantum; k++)
                        {

                            ProcesoEnTurno.faltate -= 1;
                            if (ProcesoEnTurno.Tinicio == 0)
                                ProcesoEnTurno.Tinicio = tiempo;
                            this.PushGant(ProcesoEnTurno, this.ConvertIndx(ProcesoEnTurno, ListaEstatica), tiempo - 1);
                            tiempo++;

                            // ver si el proceso ha terminado
                            if (ProcesoEnTurno.faltate == 0)
                            {
                                ProcesoEnTurno.Tfinal = tiempo - 1;
                                ProcesoEnTurno.Tretorno = this.ContarTodo(this.ConvertIndx(ProcesoEnTurno, ListaEstatica), tiempo);
                                ProcesoEnTurno.tEspera = this.ContarEs(this.ConvertIndx(ProcesoEnTurno, ListaEstatica), tiempo);
                                ListaEnTiempo.Remove(ProcesoEnTurno);
                                ColaListos.Enqueue(ProcesoEnTurno);
                                //ListaEntrada.Remove(ProcesoEnTurno);
                                borrados++;
                                break;
                            }

                            foreach (Proceso P in ListaEntrada)
                                if (P.GSTiempoLLegada == tiempo)
                                    ListaEnTiempo.Add(P);
                        }
                    }

                }
                else
                {
                    tiempo++;
                    foreach (Proceso P in ListaEntrada)
                        if (P.GSTiempoLLegada == tiempo)
                            ListaEnTiempo.Add(P);
                }
            }
            return ColaListos;*/
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

            // meter proceso a la lista en el tiempo t
            foreach (Proceso P in ListaEntrada)
                if (P.GSTiempoLLegada == tiempo && !ListaEnTiempo.Contains(P))
                    ListaEnTiempo.Add(P);

            while ( ListaEntrada.Count > borrados)
            {
                if (ListaEnTiempo.Count > 0)
                {
                    // iterar hasta que termine la lista de entrada 
                    while (ListaEnTiempo.Count > 0)
                    {
                        // meter proceso a la lista en el tiempo t
                        foreach (Proceso P in ListaEntrada)
                            if (P.GSTiempoLLegada == tiempo && ! ListaEnTiempo.Contains(P))
                                ListaEnTiempo.Add(P);

                        // buscar al proceso mas importante 
                        Proceso MasImportante = this.MasImportante(ListaEnTiempo);

                        // ejecutar al mas importante hasta qe se acabe 
                        while (MasImportante.faltate > 0)
                        {
                            if (MasImportante.Tinicio == 0)
                                MasImportante.Tinicio = tiempo;
                            this.PushGant(MasImportante, this.ConvertIndx(MasImportante, ListaEstatica), tiempo-1);

                            MasImportante.faltate -= 1;
                            tiempo++;

                            // meter proceso a la lista en el tiempo t
                            foreach (Proceso P in ListaEntrada )
                                if (P.GSTiempoLLegada == tiempo && !ListaEnTiempo.Contains(P) )
                                    ListaEnTiempo.Add(P);
                        }

                        // conseguir datos estadisticos 
                        MasImportante.Tfinal = tiempo - 1;
                        MasImportante.Tretorno = this.ContarTodo(this.ConvertIndx(MasImportante, ListaEstatica), tiempo);
                        MasImportante.tEspera = this.ContarEs(this.ConvertIndx(MasImportante, ListaEstatica), tiempo);

                        // meter en la cola de listos
                        ColaListos.Enqueue(MasImportante);

                        // eliminar de las lista 
                        ListaEnTiempo.Remove(MasImportante);
                        //  ListaEntrada.Remove(MasImportante);
                        borrados++;
                    }
                }
                else
                {
                    // meter proceso a la lista en el tiempo t
                    foreach (Proceso P in ListaEntrada)
                        if (P.GSTiempoLLegada == tiempo && !ListaEnTiempo.Contains(P))
                            ListaEnTiempo.Add(P);
                    tiempo++;

                }


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

        public Proceso MasViejo(ArrayList ListaEntrada)
        {
            if (ListaEntrada.Count == 0)
                return null;
            // buscar al proceso mas viejo en la lista
            Proceso ProcesoMenor = (Proceso)ListaEntrada[0];
            foreach (Proceso p in ListaEntrada)
                if (p.Vejes  > ProcesoMenor.Vejes)
                    ProcesoMenor = p;
            return ProcesoMenor;
        }

        public Queue PlanificarPrioridadX(ArrayList ListaEntrada)
        {
            int tiempo = 1, borrados = 0;
            Queue ColaListos = new Queue();
            ArrayList ListaEnTiempo = new ArrayList();
            ArrayList ListaEstatica = ListaEntrada;

            // meter proceso a la lista en el tiempo t
            foreach (Proceso P in ListaEntrada)
                if (P.GSTiempoLLegada == tiempo && !ListaEnTiempo.Contains(P))               
                    ListaEnTiempo.Add(P);

            while (ListaEntrada.Count > borrados )
            {
                if (ListaEnTiempo.Count > 0)
                {
                    // iterar hasta que termine la lista en tiempo
                    while (ListaEnTiempo.Count > 0)
                    {
                        // meter proceso a la lista en el tiempo t
                        foreach (Proceso P in ListaEntrada  )
                        {
                            if (P.GSTiempoLLegada == tiempo  && ! ListaEnTiempo.Contains(P))
                            {
                                ListaEnTiempo.Add(P);
                                // this.PushGant(P, this.ConvertIndx(P, ListaEstatica), tiempo - 1);

                            }

                             
                        }

                        // buscar al proceso mas viejo
                        Proceso MasImportante = this.MasViejo(ListaEnTiempo);

                        // ejecutar al mas viejo, mientras sea el mas importante  
                        while (MasImportante.faltate > 0)
                        {
                            if (MasImportante.Tinicio == 0)
                                MasImportante.Tinicio = tiempo;
                            this.PushGant(MasImportante, this.ConvertIndx(MasImportante, ListaEstatica), tiempo - 1);

                            MasImportante.faltate -= 1;

                            // actualizar la vejes del resto de procesos
                            for (int i = 0; i < ListaEnTiempo.Count; i++)
                            {
                                Proceso P = (Proceso)ListaEnTiempo[i];
                                P.Vejes += this.ContarVejes(P, this.ConvertIndx(P, ListaEstatica), tiempo);
                            }

                            // poner es espera todos los otros procesos
                            foreach (Proceso P in ListaEnTiempo)
                            {
                                if (P != MasImportante )
                                {
                                    // poner E's   
                                    if (P.faltate > 0)
                                        this.gant[this.ConvertIndx(P, ListaEstatica), tiempo] = "E";
                                }
                            }


                            tiempo++;

                            // meter proceso a la lista en el tiempo t
                            foreach (Proceso P in ListaEntrada  ) 
                            {
                                if (P.GSTiempoLLegada == tiempo && !ListaEnTiempo.Contains(P))
                                {
                                    ListaEnTiempo.Add(P);
                                    //this.PushGant(P, this.ConvertIndx(P, ListaEstatica), tiempo - 1);

                                }


                            }

                            // buscar otra vez al mas viejo
                            MasImportante = this.MasViejo(ListaEnTiempo);

                        }

                        // conseguir datos estadisticos 
                        MasImportante.Tfinal = tiempo - 1;
                        MasImportante.Tretorno = this.ContarTodo(this.ConvertIndx(MasImportante, ListaEstatica), tiempo);
                        MasImportante.tEspera = this.ContarEs(this.ConvertIndx(MasImportante, ListaEstatica), tiempo);

                        // meter en la cola de listos
                        ColaListos.Enqueue(MasImportante);

                        // eliminar de las lista 
                        ListaEnTiempo.Remove(MasImportante);
                        //  ListaEntrada.Remove(MasImportante);
                        borrados++;
                    }
                }
                else
                {
                   


                    // meter proceso a la lista en el tiempo t
                    foreach (Proceso P in ListaEntrada)
                    {
                        if (P.GSTiempoLLegada == tiempo && !ListaEnTiempo.Contains(P))
                        {
                            ListaEnTiempo.Add(P);
                            //this.PushGant(P, this.ConvertIndx(P, ListaEstatica), tiempo - 1);

                        }

                       


                    }

                    tiempo++;

                }
            }
            return ColaListos;

        }

        public int ContarVejes ( Proceso Procesado, int Iesimo , int tiempo)
        {

            int EsAnterior = this.ContarEs(Iesimo, tiempo)/2;
            int EsTotal = Math.Abs(EsAnterior - Procesado.Vejes);

            return EsTotal;

        }

        public void AgregarEnTiempo ( ArrayList ListaEntrada, int Tiempo, ArrayList ListaEnTiempo )
        {
            foreach (Proceso P in ListaEntrada)
                if (P.GSTiempoLLegada == Tiempo && !ListaEnTiempo.Contains(P))
                    ListaEnTiempo.Add(P);   
        }

        public void Procesar(  Proceso Procesado, int Tiempo , ArrayList ListaEstatica , ArrayList ListaEntiempo )
        {
            if (Procesado.Tinicio == 0)
                Procesado.Tinicio = Tiempo+1;

            this.PushGant(Procesado, this.ConvertIndx(Procesado,ListaEstatica), Tiempo);

            // poner en esperar todos los procesos en la cola de tiempo 
            foreach (Proceso p in ListaEntiempo)
                if (string.Compare(Procesado.GSnombre, p.GSnombre) != 0 && this.gant[this.ConvertIndx(p, ListaEstatica), Tiempo] != "x")
                    this.gant[this.ConvertIndx(p, ListaEstatica), Tiempo] = "|";

            Procesado.faltate -= 1;
        }

        public void Finalizar ( Proceso Procesado, int Tiempo, ArrayList ListaEntrada, ArrayList ListaEntiempo, Queue ColaListos, ArrayList ListaEstatica )
        {
            Procesado.Tfinal = Tiempo;
            Procesado.tEspera = this.ContarEs(this.ConvertIndx(Procesado, ListaEstatica), Tiempo);
            Procesado.Tretorno = this.ContarTodo(this.ConvertIndx(Procesado, ListaEstatica), Tiempo);

            ListaEntrada.Remove(Procesado);
            ListaEntiempo.Remove(Procesado);
            ColaListos.Enqueue(Procesado);
        }

        public void CalcularTaza ( int Tiempo  , ArrayList ListaEnTiempo, ArrayList ListaEstatica )
        {
            foreach( Proceso P in ListaEnTiempo )
            {
                int Tespera = this.ContarEs(this.ConvertIndx(P, ListaEstatica), Tiempo);
                int TServicio = P.GSduracion;

                P.TazaRespuesta = (float) (Tespera + TServicio) / TServicio;

            }
        }

        public Proceso MayorTaza (ArrayList ListaEnTiempo)
        {
            // buscar al proceso con mayor raza de respues 
            Proceso ProcesoMayor = (Proceso)ListaEnTiempo[0];
            foreach (Proceso p in ListaEnTiempo)
                if ( p.TazaRespuesta > ProcesoMayor.TazaRespuesta )
                    ProcesoMayor = p;

            return ProcesoMayor;
        }

        public ArrayList CopiarLista ( ArrayList ListaEntrada)
        {
            ArrayList ListaSalida = new ArrayList();

            foreach (Proceso p in ListaEntrada)
                ListaSalida.Add(p);
            return ListaSalida;
        }

        public Queue PlanificarHRRN ( ArrayList ListaEntrada)
        {
            ArrayList ListaEstatica = this.CopiarLista(ListaEntrada);
            ArrayList ListaEnTiempo = new ArrayList();
            Queue ColaListos = new Queue();
            int tiempo = 0;

            // iniciar las variables
            this.AgregarEnTiempo(ListaEntrada, tiempo, ListaEnTiempo);

            // iterar hasta qe termine la lisgta de entrada 
            while ( ListaEntrada.Count > 0)
            {
                // si hay algo que ejecutar
                if ( ListaEnTiempo.Count > 0)
                {
                    /* decidir que proceso a ejecutar */
                    this.CalcularTaza( tiempo -1, ListaEnTiempo, ListaEstatica );
                    Proceso MayorTaza = this.MayorTaza( ListaEnTiempo );

                    // ejecutar el proceso hasta que se acabe 
                    while (MayorTaza.faltate > 0)
                    {
                        this.Procesar(MayorTaza, tiempo-1, ListaEstatica, ListaEnTiempo);
                        tiempo++;
                        // agregar a al lista en tiempo
                        this.AgregarEnTiempo(ListaEntrada, tiempo, ListaEnTiempo);                      
                    }
                    // eliminar al proceso terminado 
                    this.Finalizar(MayorTaza, tiempo-1, ListaEntrada, ListaEnTiempo, ColaListos, ListaEstatica);

                    // agregar a al lista en tiempo
                    this.AgregarEnTiempo(ListaEntrada, tiempo, ListaEnTiempo);
                }
                else
                {
                    tiempo++;
                    // aumentar tiempo hasta que lleguen procesos 
                    this.AgregarEnTiempo(ListaEntrada, tiempo, ListaEnTiempo);
                }

            }
            return ColaListos;
        }

    }

    
}