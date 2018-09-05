using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace dershane
{
    public partial class Form1 : Form
    {
        public Form2 frm2;
        public Form3 frm3;
        public Form4 frm4;
        public Form1()
        {
            InitializeComponent();
            frm2 = new Form2();
            frm2.frm1 = this;

            frm3 = new Form3();
            frm3.frm1 = this;

            frm4 = new Form4();
            frm4.frm1 = this;

        }

        OleDbConnection burak = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=proje.accdb");

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void baglan()
        {
            try
            {
                if (burak.State == ConnectionState.Closed)
                    burak.Open();

            }
            catch (Exception hata)
            {

                MessageBox.Show(hata.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglan();
                DataTable tablo = new DataTable();
                OleDbDataAdapter adtr = new OleDbDataAdapter("Select * From  " + textBox2.Text + " Where Ad = '" + textBox1.Text + "' And Soyad = '" + textBox3.Text + "'  ", burak); // 
                adtr.Fill(tablo);



                if (tablo.Rows[0]["No"].ToString() == textBox2.Text)
                {

                    if (tablo.Rows.Count > 0)
                    {


                        if (frm2.IsDisposed)
                        {
                            frm2 = new Form2();
                            frm2.Show();
                            this.Hide();
                        }
                        else
                        {
                            frm2.Show();
                            frm2.Activate();
                            this.Hide();
                        }
                    }

                }

            }
            catch 
            {

                MessageBox.Show("hata");
            }
            
        }

        
    }
}
