using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace FsFc
{


   
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.gin.Enabled = false;
            // inicair elementos del formulario
            this.gin.Text = " ";
            this.button1.Text = " ->";
            button3.Text = ">>";
            this.button2.Text = "Procesar";

            this.lbProcesos.Text = " ";
            this.txtDurProc.BackColor = Color.Gray;
            this.txtNombreProc.BackColor = Color.Gray;
            chkNombre.Text = "Nombre Automatico";

            chckOrdAl.Checked = true;
            chkDurAl.Checked = true;
            chkNombre.Checked = true;

            // agregar elementos a la lista
            
            listView1.Columns.Add("1" ,"Nombre", listView1.Width / 3);
            listView1.Columns.Add("2", "Duracion", listView1.Width / 3);
            listView1.Columns.Add("3","Tiempo de espera", listView1.Width / 3);

            gGant.Text = " ";
            gin.Text = " ";
            gStdcs.Text = " ";
            
            




        }

        private void chckOrdAl_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.gin.Enabled == false)
                gin.Enabled = true;


        }

        private void chkDurAl_CheckedChanged(object sender, EventArgs e)
        {
            this.txtDurProc.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int numerProcesos = Convert.ToInt16(numeroProcesos.Text);
            int tiempoEspera = 0;
            int tiempoProm = 0;
            Random r = new Random();
            Planificador pln1 = new Planificador();


            // si todo es automatico
            if ( chkNombre.Checked && chkDurAl.Checked)
            {
                Proceso[] vectProcesos = new Proceso[numerProcesos];

                // iterar para cada uno de los procesos 
                for ( int i = 0; i < vectProcesos.Length; i ++)
                {
                    int cuenta = i + 1;

                    lbProcesos.Text = "Proceso " + cuenta.ToString();
                    Thread.Sleep(600);
                    lbProcesos.Refresh();

                    vectProcesos[i] = new Proceso();
                    vectProcesos[i].GSnombre = "p" + cuenta.ToString();
                    vectProcesos[i].GSduracion = r.Next(1, 25);

                    // procesar cada proceso del vector 
                    pln1.planificarFcFs(vectProcesos[i]);

                    // agregar al list view
                    listView1.View = View.Details;

                    ListViewItem it = new ListViewItem(vectProcesos[i].GSnombre);
         
                    it.SubItems.Add(vectProcesos[i].GSduracion.ToString());
                    it.SubItems.Add(tiempoEspera.ToString() );

                    listView1.Items.Add(it);

                    Thread.Sleep(600);
                    listView1.Refresh();

                    tiempoProm += tiempoEspera;
                    tiempoEspera += vectProcesos[i].GSduracion;
                    
               

                }
                lblPromedio.Text = Convert.ToString((float) tiempoProm / vectProcesos.Length);
            }

            
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
    }

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

        public Proceso()
        {
            /* 0-en espera, 1-en ejecucion 2- listo  -1- bloqueado 4-listo*/
            this.estado = 0;
            this.nombre = " ";
            this.duracion = 0;
            this.faltate = 10;
        }


    }

    public class Planificador
    {
        /* 1 ocupado, 0 libre */
        private int estado;
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

        public void planificarFcFs( Proceso pActual)
        {
            // procesar hasta que el proceso termine 
            while(pActual.faltate >= 3)
            {
                pActual.faltate -= 3;
                pActual.GSestado = 1;
            }

            pActual.GSestado = 4;
        }
    }

}
