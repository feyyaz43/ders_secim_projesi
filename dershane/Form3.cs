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
    public partial class Form3 : Form
    {
        public Form1 frm1;
        public Form3()
        {
            InitializeComponent();
        }

        OleDbConnection burak = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=proje.accdb");
        OleDbConnection ramazan = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ders.accdb");

        private void Form3_Load(object sender, EventArgs e)
        {
            ramazan.Open();
            DataTable tablo = new DataTable();
            OleDbDataAdapter adtr = new OleDbDataAdapter("Select * From cakisan_dersler ", ramazan);
            adtr.Fill(tablo);

            for (int l = 0; l < tablo.Rows.Count; l++)
            {
                for (int k = 0; k < tablo.Rows.Count; k++)
                {
                    if (l != k)
                    {
                        if (tablo.Rows[k]["ogr_no"].ToString() == tablo.Rows[l]["ogr_no"].ToString())
                        {
                            if (tablo.Rows[k]["d1_d2"].ToString() == tablo.Rows[l]["d1_d2"].ToString())
                            {
                                tablo.Rows[k].Delete();
                                tablo.AcceptChanges();
                            }

                        }
                    }

                }
            }



            OleDbCommand yaz = new OleDbCommand("Delete * from cakisan_dersler ", ramazan);
            yaz.ExecuteNonQuery();

            for (int l = 0; l < tablo.Rows.Count; l++)
            {
                OleDbCommand yaz1 = new OleDbCommand("Insert Into cakisan_dersler (d1_d2, ogr_no) Values ('" + tablo.Rows[l]["d1_d2"].ToString() + "', '" + tablo.Rows[l]["ogr_no"].ToString() + "')", ramazan);
                yaz1.ExecuteNonQuery();
            }

            DataTable tablo2 = new DataTable();
            OleDbDataAdapter adtr2 = new OleDbDataAdapter("Select * From cakisan_dersler ", ramazan);
            adtr2.Fill(tablo2);

            CrystalReport1 rapor = new CrystalReport1();
            rapor.SetDataSource(tablo2);
            crystalReportViewer1.ReportSource = rapor;
            
        }
    }
}
