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

namespace SudokuSolverApp
{
    public partial class Form1 : Form
    {
        int reihe = 10;
        int spalte = 10;
        int anzahl = 0;
        int wert = 0;
        int Zahl;
        Label label1;
        private Button button1;
        Button[,] buttons = new Button[10,10];
        Button[] zifferButton = new Button[10];
        int starti = 100;
        int startj = 100;

        public Form1()
        {
            InitializeComponent();
            Form1_Load(null, null);
            //KontrolleSudoku();

        }

        

        public void Form1_Load(object sender, EventArgs e)
        {

           
            

            {
                
                //Button[,] buttons = new Button[reihe, spalte];

               

                //Buttons für Sudoku 9x9
                for (int j = 1; j < spalte; j++)
                {
                    starti = 100;
                    if (j > 3)
                        startj = 110;
                    if (j > 6)
                        startj = 120;
                    for (int i = 1; i < reihe; i++)
                    {
                        if (i > 3)
                            starti = 110;
                        if (i > 6)
                            starti = 120;
                        anzahl++;
                        buttons[i, j] = new Button
                        {
                            Text = "123456789",
                            Name = "" + j + i,
                            Size = new Size(61, 61),
                            Location = new Point(starti + i * 60, startj + j * 60),
                            Tag = 0
                        };
                        buttons[i, j].Click += Form1_Click;
                        this.Controls.Add(buttons[i, j]);

                    }
                }

                //Buttons für Ziffern
                //Button[] zifferButton = new Button[10];
                for (int i = 1; i < 10; i++)
                {
                    zifferButton[i] = new Button
                    {
                        Location = new Point(800, 100 + i * 60),
                        Size = new Size(60, 60),
                        Text = "" + i,
                        Name = "" + i,
                        BackColor = Color.FloralWhite
                    };
                    zifferButton[i].Click += Form1_Click;
                    this.Controls.Add(zifferButton[i]);
                }
                

            }

        }

        private bool SetzenMoeglich(string wert1,string wert2)
        {
            
            return wert1.Contains(wert2);
        }

        private int FindeReihe(int a)
        {
            int Reihe = a / 10;
            return Reihe;
                
        }

        private int FindeSpalte(int a)
        {
            int Spalte = a % 10;
            
            return Spalte;

        }

        private int FindeQuadratx(int a)
        {
            int x = 0;
            if (a < 70)            
                x = 4;           
            if (a < 40)            
                x = 1;          
            if (a > 70)
                x = 7;

            return x;     
        }

        private int FindeQuadraty(int a)
        {
            int y = a % 10;
            y = y + 2;
            y = y / 3;
            y = y * 3 - 2;

            return y;
        }


        private void Form1_Click(object sender, EventArgs e)
        {
            
           
            string Wert = (sender as Button).Name;
            int Zahl = Int32.Parse((sender as Button).Name);

            // wenn ziffer gedrückt wurde dann merken
            if (Zahl < 10)
            {
                for (int i = 1; i < 10; i++)
                {
                    zifferButton[i].BackColor = Color.FloralWhite;  // alle Zahlenbuttons eine Farbe
                }
                this.wert = Zahl;
                
                (sender as Button).BackColor = Color.DarkGray;  // aktiver Zahlenbutton eine Farbe
                
            }

            // wenn sudokufeld gedrückt wurde dann eintragen
            if ((Zahl >= 10) & (Zahl < 100))
            {
                string txt = this.wert.ToString();
               // KontrolleSudoku();
                if (SetzenMoeglich((sender as Button).Text,txt ))
                {
                    (sender as Button).Font = new System.Drawing.Font("Segoe UI", 20F);
                    (sender as Button).Text = txt;
                    for (int i = 1; i < 10; i++)
                    {
                        int Reihe = FindeReihe(Zahl);       // komplette Reihe prüfen und aus String entfernen
                        string tmp = buttons[i,Reihe].Text  ;

                        //wenn noch keine lösung
                        if (tmp.Length > 1)
                        {
                            tmp = tmp.Replace(txt, "");
                            buttons[i, Reihe].Text = tmp;
                        }
                        
                    }

                    (sender as Button).Text = txt;
                    for (int i = 1; i < 10; i++)
                    {
                        int Spalte = FindeSpalte(Zahl);     //komplette Spalte prüfen  und aus String entfernen
                        string tmp = buttons[Spalte,i].Text;

                        //wenn noch keine lösung
                        if (tmp.Length > 1)
                        {
                            tmp = tmp.Replace(txt, "");
                            buttons[Spalte,i].Text = tmp;
                        }
                    }

                    (sender as Button).Text = txt;
                    for (int j = 0; j < 3; j++)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            int quadratx = FindeQuadratx(Zahl);
                            int quadraty = FindeQuadraty(Zahl);

                            string tmp = buttons[quadraty + i, quadratx + j].Text;

                            //wenn noch keine lösung
                            if (tmp.Length > 1)
                            {
                                tmp = tmp.Replace(txt, "");
                                buttons[quadraty + i, quadratx + j].Text = tmp;
                            }
                            label1.Text = quadratx + " " + quadraty;


                        }
                    }
                        
                    
               

                    


                    //SetzeSpalte();
                    //SetzeQuadrat();
                }
                
            }
                

            // MessageBox.Show(""+wert);
           // MessageBox.Show((sender as Button).Name);
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(960, 219);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(981, 94);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 82);
            this.button1.TabIndex = 1;
            this.button1.Text = "Versuch zu Lösen";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.button1_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void KontrolleSudoku()
        {
            anzahl = 0;
            for (int j = 1; j < spalte; j++)
            {
                for (int i = 1; i < reihe; i++)
                {
                    anzahl++;
                    label1.Text = anzahl.ToString();
                    //buttons[i,j].BackColor = Color.Red ;
                    int Zahl = Int32.Parse((buttons[i, j]).Name);
                    string tmp2 = (buttons[i, j].Text);
                    //MessageBox.Show(buttons[i, j].Text);
                    if (tmp2.Length == 1)

                    {
                       // MessageBox.Show(tmp2 +" "+(buttons[i,j].Name + " " + anzahl.ToString()));
                        string txt = tmp2;
                        for (int x = 1; x < 10; x++)
                        {
                            int Reihe = FindeReihe(Zahl);
                            tmp2 = buttons[x, Reihe].Text;

                            //wenn noch keine lösung
                            if (tmp2.Length > 1)
                            {
                                tmp2 = tmp2.Replace(txt, "");
                                buttons[x, Reihe].Text = tmp2;
                            }
                            if (tmp2.Length == 1)
                                buttons[i,j].Font = new System.Drawing.Font("Segoe UI", 20F);
                        }



                        for (int y = 1; y < 10; y++)
                        {
                            int Spalte = FindeSpalte(Zahl);
                            tmp2 = buttons[Spalte, y].Text;

                            //wenn noch keine lösung
                            if (tmp2.Length > 1)
                            {
                                tmp2 = tmp2.Replace(txt, "");
                                buttons[Spalte, y].Text = tmp2;
                            }
                        }


                        for (int y = 0; y < 3; y++)
                        {
                            for (int x = 0; x < 3; x++)
                            {
                                int quadratx = FindeQuadratx(Zahl);
                                int quadraty = FindeQuadraty(Zahl);

                                 tmp2 = buttons[quadraty + x, quadratx + y].Text;

                                //wenn noch keine lösung
                                if (tmp2.Length > 1)
                                {
                                    tmp2 = tmp2.Replace(txt, "");
                                    buttons[quadraty + x, quadratx + y].Text = tmp2;
                                }
                                //label1.Text = quadratx + " " + quadraty;


                            }
                        }
                    }



                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KontrolleSudoku();


        }
    }
}
