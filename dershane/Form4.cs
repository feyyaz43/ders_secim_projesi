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
    public partial class Form4 : Form
    {
        public Form1 frm1;
        public Form4()
        {
            InitializeComponent();
        }

        OleDbConnection ramazan = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ders.accdb");

        private void Form4_Load(object sender, EventArgs e)
        {
            ramazan.Open();
            string ogrenci = frm1.textBox2.Text.ToString();
            DataTable tablo1 = new DataTable();
            OleDbDataAdapter adtr1 = new OleDbDataAdapter("Select * From  " + ogrenci + "  ", ramazan);
           
            adtr1.Fill(tablo1);
            CrystalReport2 rapor1 = new CrystalReport2();
            rapor1.SetDataSource(tablo1);
            crystalReportViewer1.ReportSource = rapor1;
            ramazan.Close();
           
        }
    }
}
