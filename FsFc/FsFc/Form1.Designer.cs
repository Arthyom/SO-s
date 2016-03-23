namespace FsFc
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.gStdcs = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPromedio = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.gGant = new System.Windows.Forms.GroupBox();
            this.gant = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label3 = new System.Windows.Forms.Label();
            this.gStdcs.SuspendLayout();
            this.gGant.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gant)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(469, 45);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(129, 21);
            this.comboBox1.TabIndex = 18;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(469, 72);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(129, 23);
            this.button2.TabIndex = 17;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // gStdcs
            // 
            this.gStdcs.Controls.Add(this.label3);
            this.gStdcs.Controls.Add(this.button4);
            this.gStdcs.Controls.Add(this.button2);
            this.gStdcs.Controls.Add(this.comboBox1);
            this.gStdcs.Controls.Add(this.label4);
            this.gStdcs.Controls.Add(this.lblPromedio);
            this.gStdcs.Controls.Add(this.listView1);
            this.gStdcs.Location = new System.Drawing.Point(2, 2);
            this.gStdcs.Name = "gStdcs";
            this.gStdcs.Size = new System.Drawing.Size(598, 170);
            this.gStdcs.TabIndex = 1;
            this.gStdcs.TabStop = false;
            this.gStdcs.Text = "groupBox2";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(497, 142);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(72, 22);
            this.button4.TabIndex = 18;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(503, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 16);
            this.label4.TabIndex = 14;
            this.label4.Tag = "Nombre";
            this.label4.Text = "Promedio";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPromedio
            // 
            this.lblPromedio.AutoSize = true;
            this.lblPromedio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPromedio.ForeColor = System.Drawing.Color.Red;
            this.lblPromedio.Location = new System.Drawing.Point(512, 119);
            this.lblPromedio.Name = "lblPromedio";
            this.lblPromedio.Size = new System.Drawing.Size(57, 20);
            this.lblPromedio.TabIndex = 13;
            this.lblPromedio.Text = "label1";
            // 
            // listView1
            // 
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(6, 10);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(454, 154);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // gGant
            // 
            this.gGant.Controls.Add(this.gant);
            this.gGant.Location = new System.Drawing.Point(2, 172);
            this.gGant.Name = "gGant";
            this.gGant.Size = new System.Drawing.Size(598, 310);
            this.gGant.TabIndex = 2;
            this.gGant.TabStop = false;
            this.gGant.Text = "groupBox2";
            // 
            // gant
            // 
            chartArea2.Name = "ChartArea1";
            this.gant.ChartAreas.Add(chartArea2);
            legend2.DockedToChartArea = "ChartArea1";
            legend2.IsDockedInsideChartArea = false;
            legend2.Name = "Legend1";
            this.gant.Legends.Add(legend2);
            this.gant.Location = new System.Drawing.Point(0, 10);
            this.gant.Name = "gant";
            this.gant.Size = new System.Drawing.Size(598, 292);
            this.gant.TabIndex = 0;
            this.gant.Text = "chart1";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(466, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 32);
            this.label3.TabIndex = 19;
            this.label3.Tag = "Nombre";
            this.label3.Text = "Metodo De Planificacion";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 483);
            this.Controls.Add(this.gGant);
            this.Controls.Add(this.gStdcs);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gStdcs.ResumeLayout(false);
            this.gStdcs.PerformLayout();
            this.gGant.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gant)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox gStdcs;
        private System.Windows.Forms.GroupBox gGant;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPromedio;
        private System.Windows.Forms.DataVisualization.Charting.Chart gant;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label3;
    }
}

