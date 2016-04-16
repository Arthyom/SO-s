using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using System.IO;
using System.Collections;

namespace FsFc
{


   
    public partial class Form1 : Form
    {
        string RutaARchivos;

        public Form1()
        {
            InitializeComponent();
        }

        string ruta;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width -= txtEdit.Width ;
            gant.Titles.Add("Diagrama de Gant de Procesos");
            gant.ChartAreas["ChartArea1"].AxisY.Title = "Procesos";
            gant.ChartAreas["ChartArea1"].AxisX.Title = "Tiempo";
            

            this.button2.Text = "Procesar";
           
            

          
            button4.BackColor = Color.Red;
            button4.Text = "Borrar";
           

            
            // agregar elementos a la lista
            
            listView1.Columns.Add("1" ,"Nombre", listView1.Width / 9);
            listView1.Columns.Add("2", "To", listView1.Width / 9);
            listView1.Columns.Add("3", "Duracion", listView1.Width /9);
            listView1.Columns.Add("4", "Prioridad", listView2.Width /9);
            listView1.Columns.Add("5", "Veges", listView2.Width / 9);
            listView1.Columns.Add("6","Ti", listView1.Width / 9);
            listView1.Columns.Add("7", "Tf", listView1.Width / 9);
            listView1.Columns.Add("8", "Tr", listView1.Width / 9);
            listView1.Columns.Add("9", "Te", listView1.Width / 9);


            listView2.Columns.Add("0", "Nombre", listView2.Width / 5);
            listView2.Columns.Add("0", "Prioridad", listView2.Width / 5);
            listView2.Columns.Add("0", "Veges", listView2.Width / 5);
            listView2.Columns.Add("0", "To", listView2.Width / 5);
            listView2.Columns.Add("0", "Duracion", listView2.Width / 5);



            // fijar propiedades de la combobox
            comboBox1.Items.Add("FcFs");
            comboBox1.Items.Add("SJF");
            comboBox1.Items.Add("SJFX");
            comboBox1.Items.Add("SRTF");
            comboBox1.Items.Add("Priority");
            comboBox1.Items.Add("PriorityX");
            comboBox1.Items.Add("Round Robin");
            comboBox1.Items.Add("HRRN");
            

            gGant.Text = " ";
           // gin.Text = " ";
            gStdcs.Text = " ";

            this.lnomb.Text = " ";

            this.button1.BackColor = Color.Green;
            this.listView2.BackColor = Color.DarkGray;
            
            




        }

        private void chckOrdAl_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            


        }

        // mostrar dentro del listView
        private void display ( Proceso Procesado)
        {

            ListViewItem it = new ListViewItem(Procesado.GSnombre);
            it.SubItems.Add(Procesado.GSTiempoLLegada.ToString());
            it.SubItems.Add(Procesado.GSduracion.ToString());

            it.SubItems.Add(Procesado.Prioridad.ToString());
            it.SubItems.Add(Procesado.Vejes.ToString());

            it.SubItems.Add(Procesado.Tinicio.ToString());
            it.SubItems.Add(Procesado.Tfinal.ToString());
            it.SubItems.Add(Procesado.Tretorno.ToString());
            it.SubItems.Add(Procesado.tEspera.ToString());

            listView1.Items.Add(it);
            listView1.Refresh();
            
            
        }

        private void Graficar ( Proceso ProcesoListo, int Yvalue, int acumulado)
        {
            // crear series 
            Series serie = gant.Series.Add(ProcesoListo.GSnombre);
            serie.ChartType = SeriesChartType.Point;

            for (int i = acumulado+1; i <= ProcesoListo.GSduracion + acumulado; i++)
                serie.Points.AddXY(i, Yvalue);


        }

        private void GraficarProceso(Proceso ProcesoListo, int Yvalue, int inicio)
        {
            // crear series 
            Series serie = gant.Series.Add(ProcesoListo.GSnombre);
            serie.ChartType = SeriesChartType.Point;

            for (int i = inicio ; i <= ProcesoListo.Tfinal ; i++)
                serie.Points.AddXY(i, Yvalue);


        }


        private void creatGrafico ( Proceso [] vectorProc )
        {
            

            // graficar el vector de procesos 
      

            int acumulado = 0;
            int anterior = 0;
            // graficar cada proceso en el vector
            for (int i = 0; i < vectorProc.Length; i++)
            {
                // agregar serie al grafico
                Series serie = gant.Series.Add(vectorProc[i].GSnombre);

                // cambiar tipo de grafico
                serie.ChartType = SeriesChartType.Point;
                int exp = acumulado  + 1 ;

                // poner todos los puntos en la grafica para una serie
                for (int j = (acumulado + 1); j <= (acumulado + vectorProc[i].GSduracion); j++)
                {
                    serie.Points.AddXY(j, i + 1);
                }
                acumulado += (vectorProc[i].GSduracion); 
            }
        }

        private void chkDurAl_CheckedChanged(object sender, EventArgs e)
        {
           // this.txtDurProc.Enabled = false;
        }

        private void CargarArchivo ( string ruta)
        {
            StreamReader lr = new StreamReader(ruta);

            int numProc = Convert.ToInt32(lr.ReadLine());
            lr.Close();

            Proceso[] VectorProc = new Proceso[numProc];
            for (int i = 0; i < numProc; i++)
                VectorProc[i] = new Proceso();
            
            Planificador.leerArchivo(VectorProc,ruta);

            foreach( Proceso p in VectorProc)
            {
                ListViewItem it = new ListViewItem(p.GSnombre);
                it.SubItems.Add(p.Prioridad.ToString());
                it.SubItems.Add(p.Vejes.ToString());
                it.SubItems.Add(p.GSTiempoLLegada.ToString());
                it.SubItems.Add(p.GSduracion.ToString());
                listView2.Items.Add(it);
                listView2.Refresh();

            } 
        }

        private void DsplGantAlt()
        {
            this.txtAlt.Font = new Font(Font.FontFamily.Name,20, FontStyle.Bold);

            StreamReader Lector = new StreamReader(@"C: \Users\frodo\Desktop\Planificacion\GantAlterno.txt");

            // leer el archivo y meterlo en la caja de texto
            string linea;
            while ( (linea = Lector.ReadLine() ) != null)
            {
                this.txtAlt.Text += linea;
                this.txtAlt.Text += @"
";
                


            }
            Lector.Close();

            
        }

        private void CargaAlterno(Planificador Pactual,Proceso Procesado, int numProc, int maximo, int Iesimo)
        {
            // generar grafica alterna obligada
            this.GantAlterno.Checked = true;
            Pactual.GantAlterno(Procesado, numProc, maximo, Iesimo);

            // desplegar el gant en la caja de texto
            for (int i = 0; i < numProc; i++)
            {
           
                for (int j = 0; j < maximo; j++)
                {
                    // mostrar el gant en la caja de texto 
                   
                    this.txtAlt.Text += String.Format("{0}",Pactual.gant[i,j]);
                    txtAlt.Refresh();
                }
                txtAlt.Text += @"
                    ";
         

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ruta = this.ruta;
            StreamReader lectr = new StreamReader(ruta);
            int numProc = Convert.ToInt16(lectr.ReadLine());
            lectr.Close();

            StreamWriter Escritor = new StreamWriter(@"C: \Users\frodo\Desktop\Planificacion\GantAlterno.txt");

            // cola de usados 
            Queue usados = new Queue(numProc);

            // crear vector de procesos 
            Proceso[] vectProc = new Proceso[numProc];
            for (int i = 0; i < vectProc.Length; i++)
                vectProc[i] = new Proceso();

            // crear cola de procesos 
            Queue colaProc = new Queue(numProc);
            Queue colaCop = new Queue(numProc);

            // crear un planificador 
            Planificador plnfcdr1 = new Planificador();

            // leer archivos y meterlos en el vector
            Planificador.leerArchivo(vectProc,ruta);
            int llegada = vectProc[0].GSTiempoLLegada;
            

            int maximo = 0;

            foreach (Proceso P in vectProc)
                            maximo += P.GSduracion;

            plnfcdr1.gant = new string[numProc, maximo*2];
            plnfcdr1.InicarGant(numProc, maximo*2);

            // decidir segund el algoritmo de planificacion
            switch (this.comboBox1.Text)
            {

                case "SJF":
                    int espera = 1;
                    int Tinicio = 1;
                    int Tfinal = 1;
                    int yValue = 1;
                    int cont = 0;

                    while ( cont < numProc  )
                     {
                        Proceso Procesado = plnfcdr1.PlanificarSJF(colaProc, vectProc, Tinicio,Tfinal, espera);
                        plnfcdr1.LLenarGant(Procesado, cont, maximo);
                        

                     
                            if ( !this.GantAlterno.Checked )
                                Graficar(Procesado, yValue, espera);
                        


                      

                        colaCop.Enqueue(Procesado);
                        if (Procesado != null)
                        {
                          
                            Tinicio += Procesado.GSTiempoLLegada;
                            Tfinal += Procesado.GSduracion;
                            espera += Procesado.GSduracion;

                            this.display(Procesado);

                            yValue++;
                            cont++;

                        }
                        Tfinal++; 
                      }                      
                break;

                case "FcFs":
                  
                    yValue = 1;
                    espera = 1;
                    foreach(Proceso p in vectProc)
                    {
                        colaProc.Enqueue(p);
                        Proceso Procesado = plnfcdr1.PlanificarFcFs(colaProc, espera);
                        plnfcdr1.LLenarGant(Procesado, yValue -1, maximo);

                        colaCop.Enqueue(Procesado);
                        Graficar(Procesado, yValue, espera);
                        espera += Procesado.GSduracion  ;
                        yValue++;
                        this.display(Procesado);
                    }


                    break;

                case "SJFX":
             
                    ArrayList l = new ArrayList();
                    foreach (Proceso p in vectProc)
                        l.Add(p);
                    int i = 0;
                   Queue Procesados =  plnfcdr1.PlanificarSJFX(l);
                    foreach (Proceso p in Procesados)
                    {
                        this.display(p);
                        try
                        {
                            this.GraficarProceso(p, i + 1 , p.Tinicio);
                            i++;
                        }
                        catch(Exception Ex)
                        {
                            i++;
                        }
                    }
                       
                    colaCop = Procesados;

                    break;

                case "SRTF":
                    // meter procesos a la lista
                    ArrayList ListaProc = new ArrayList();
                    foreach (Proceso P in vectProc )
                        ListaProc.Add(P);

                    i = 0;
                    Queue ProcesadosSRTF = plnfcdr1.PlanificarSRFT(ListaProc);

                    // dar salida grafica 
                    foreach( Proceso P in ProcesadosSRTF )
                    {
                        this.display(P);
                        try
                        {
                            this.GraficarProceso(P, i + 1, P.Tinicio);
                        }
                        finally
                        {
                            i++;
                        }
                    }
                    colaCop = ProcesadosSRTF;
                    break;

                case "Round Robin":
                    // meter procesos a la lista
                    ArrayList ListaProcRR = new ArrayList();
                    foreach (Proceso P in vectProc)
                        ListaProcRR.Add(P);

                    i = 0;

                    // planificar RR
                    Queue ProcesadosRR = plnfcdr1.PlanificarRR(ListaProcRR);

                    // graficar procesos 
                    foreach (Proceso P in ProcesadosRR)
                    {
                        this.display(P);
                        try
                        {
                            this.GraficarProceso(P, i + 1, P.Tinicio);
                        }
                        finally
                        {
                            i++;
                        }
                    }
                    colaCop = ProcesadosRR;
                    break;

                case "Priority":
                    ArrayList lentrada = new ArrayList();
                    foreach (Proceso p in vectProc)
                        lentrada.Add(p);

                    colaCop = plnfcdr1.PlanificarPrioridad(lentrada);
                    i = 1;
                    foreach (Proceso p in colaCop)
                    {
                        this.display(p);
                        this.GraficarProceso(p, i, p.Tinicio);
                        i++;

                    }
                    break;

                case "PriorityX":
                    ArrayList lentradaP = new ArrayList();
                    foreach (Proceso p in vectProc)
                        lentradaP.Add(p);

                    colaCop = plnfcdr1.PlanificarPrioridadX(lentradaP);
                    i = 1;
                    foreach (Proceso p in colaCop)
                    {
                        this.display(p);
                        this.GraficarProceso(p, i, p.Tinicio);
                        i++;

                    }
                    break;

                case "HRRN":

                    ArrayList ListaEntrada  = new ArrayList();
                    ArrayList ListaEstatica = new ArrayList();
                    foreach (Proceso p in vectProc)
                    {
                        ListaEntrada.Add(p);
                        ListaEstatica.Add(p);
                    }

                    colaCop = plnfcdr1.PlanificarHRRN(ListaEntrada);

                    foreach( Proceso p in colaCop)
                    {
                        this.display(p);
                        this.GraficarProceso(p, 1+ plnfcdr1.ConvertIndx(p, ListaEstatica), p.Tinicio);
                    }

                    break;
            }

            // poner el tiempo promedio en la etiqeta

            plnfcdr1.ImprimirGantTxt(Escritor, numProc, maximo*2, vectProc);
            Escritor.Close();
            if (this.GantAlterno.Checked)
                this.DsplGantAlt();
            
            this.lblPromedio.Text = string.Format("{0:C2}" , Convert.ToString( (float)plnfcdr1.GetStdstcs(colaCop, numProc) ));
           
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // limpiar elementos 
            lnomb.Text = " ";
            this.lblPromedio.Text = " ";
            this.listView2.Items.Clear();
            this.ruta = " ";
            foreach (Control c in this.Controls)
            {
                if (c is GroupBox)
                {
                    foreach (Control ce in c.Controls)
                    {


                        if (ce is TextBox)
                            ce.Text = " ";
                        else
                        {
                            if (ce is ListView)
                            {
                                listView1.Items.Clear();
                            }
                            else {
                                if (ce is Chart)
                                {
                                    foreach (Series s in gant.Series)
                                        s.Points.Clear();
                                    gant.Series.Clear();
                                    gant.Titles.Clear();

                                }
                            }
                        }
                    }  

                }

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            op.ShowDialog();
            this.ruta = op.FileName;
            try
            {
                if (!this.ruta.Equals(null))
                {
                    this.CargarArchivo(ruta);
                    this.lnomb.Text = op.SafeFileName;

                }
            }
            catch
            {
                MessageBox.Show("No se ha podido abrir el archivo!!!!");
            }


              
                
        }

        private void GantAlterno_CheckedChanged(object sender, EventArgs e)
        {
            if (this.GantAlterno.Checked)
            {
                gant.Visible = false;
                txtAlt.Visible = true;
            }
            else
            {
                gant.Visible = true;
                txtAlt.Visible = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void editorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditVerBtn.Checked = false;
            editOclt.Checked = true;
            EditVerBtn.Enabled = true;
            editOclt.Enabled = false;
            int suma = this.Width - txtEdit.Width;

            // ocultar el editor 
            for (int i = 10 ; i < txtEdit.Width + 10; i+=10 )
            {
                this.Width -= 10;
               
                this.Refresh();
            }
        }

        private void EditVerBtn_Click(object sender, EventArgs e)
        {
            EditVerBtn.Checked = true;
            editOclt.Checked = false;
            EditVerBtn.Enabled = false;
            editOclt.Enabled = true;
            int suma = this.Width + txtEdit.Width;

            // ver el editor
            for ( int i = 10;  i< txtEdit.Width+10; i +=10  )
            {
                this.Width += 10;
                
                this.Refresh();
            }

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditVerBtn.Checked = true;
            editOclt.Checked = false;
            EditVerBtn.Enabled = false;
            editOclt.Enabled = true;
            int suma = this.Width + txtEdit.Width;

            // ver el editor
            for (int i = 10; i < txtEdit.Width + 10; i += 10)
            {
                this.Width += 10;

                this.Refresh();
            }


        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Desea Cerrar Sin Guardar???", "  ",MessageBoxButtons.OKCancel);
            if ( r == DialogResult.OK) ;
                this.CerrarEditor();
        }

        public void CerrarEditor()
        {
            EditVerBtn.Checked = false;
            editOclt.Checked = true;
            EditVerBtn.Enabled = true;
            editOclt.Enabled = false;
            int suma = this.Width - txtEdit.Width;

            // ocultar el editor 
            for (int i = 10; i < txtEdit.Width + 10; i += 10)
            {
                this.Width -= 10;

                this.Refresh();
            }

            this.txtEdit.Clear();

        }

        public void AbrirEditor()
        {
            EditVerBtn.Checked = true;
            editOclt.Checked = false;
            EditVerBtn.Enabled = false;
            editOclt.Enabled = true;
            int suma = this.Width + txtEdit.Width;

            // ver el editor
            for (int i = 10; i < txtEdit.Width + 10; i += 10)
            {
                this.Width += 10;

                this.Refresh();
            }
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // abrir un archivo en el editor
            OpenFileDialog explorador = new OpenFileDialog();
            explorador.ShowDialog();

            try
            {
                string ruta = explorador.FileName;
                StreamReader Lector = new StreamReader(ruta);

                // leer el acrchivo

                int car;
                while( (car = Lector.Read()) != -1 )
                {
                    this.txtEdit.Text += Convert.ToChar(car);

                }
                Lector.Close();
                this.AbrirEditor();
                this.lblArc.Text = explorador.SafeFileName;
                this.RutaARchivos = ruta;

            }
            catch( Exception exp)
            {
                MessageBox.Show("No se pudo abrir el archivo");
                return;
            }

            // leer el archivo
           

        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // guardar el archivo 
            StreamWriter Escritor = new StreamWriter(this.RutaARchivos);

            
            foreach(char c in this.txtEdit.Text)
            {
                Escritor.Write(Convert.ToChar(c));         
            }

            Escritor.Close();
            this.CerrarEditor();


        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            byte[] chars = new byte[this.txtEdit.Text.Length];
            for (int i = 0; i < this.txtEdit.Text.Length; i++)
                chars[i] = Convert.ToByte( this.txtEdit.Text[i] );

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {

                        myStream.Write(chars, 0, this.txtEdit.Text.Length);

                    // Code to write the stream goes here.
                    myStream.Close();
                    this.CerrarEditor();
                }
            }


        }

        private void borrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.txtEdit.Clear();
        }
    }
}
