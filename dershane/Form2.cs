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
    public partial class Form2 : Form
    {
        public Form1 frm1;
        public Form2()
        {
            InitializeComponent();

        }

        OleDbConnection burak = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=proje.accdb");
        OleDbConnection ramazan = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ders.accdb"); //öğrencinin yeniden aldığı dersleri tutan veri tabanı


        void baglan2()
        {
            try
            {
                if (ramazan.State == ConnectionState.Closed)
                    ramazan.Open();

            }
            catch (Exception hata)
            {

                MessageBox.Show(hata.Message);
            }

        }

        

        void sinif_ogren()
        {
            string numara = frm1.textBox2.Text.ToString();
            DataTable tablo3 = new DataTable();
            OleDbDataAdapter okuma = new OleDbDataAdapter("Select * From  " + numara + "   ", burak); //öğrencinin zorunlu derslerini veri tabanından çeker.
            okuma.Fill(tablo3);

            int sinif = Convert.ToInt32(tablo3.Rows[0]["sınıf"]);
            label4.Text = sinif.ToString();
        }

        void list_olustur () {
            
            listView1.View = View.Details;
            listView1.Columns.Add("Kaldığı Dersler", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("AKTS", 50, HorizontalAlignment.Left);
            listView1.Columns.Add("Sınıf", 50, HorizontalAlignment.Left);//kalan dersleri listeleyen listview oluşturduk.
            listView1.GridLines = true;

            listView2.View = View.Details;
            listView2.Columns.Add("Aldığı Dersler", 200, HorizontalAlignment.Left);
            listView2.Columns.Add("AKTS", 50, HorizontalAlignment.Left);  //aldığı dersleri listeleyen listview oluşturduk.
            listView2.GridLines = true;

            listView3.View = View.Details;
            listView3.Columns.Add("Zorunlu Dersler", 200, HorizontalAlignment.Left);
            listView3.Columns.Add("AKTS", 50, HorizontalAlignment.Left);  //zorunlu dersleri listeleyen listview oluşturduk.
            listView3.GridLines = true;

            listView4.View = View.Details;
            listView4.Columns.Add("Seçmeli Dersler", 200, HorizontalAlignment.Left);
            listView4.Columns.Add("AKTS", 50, HorizontalAlignment.Left);  //seçmeli dersleri listeleyen listview oluşturduk.
            listView4.GridLines = true;

            listView5.View = View.Details;
            listView5.Columns.Add("Bölüm Seçmeli Dersler", 200, HorizontalAlignment.Left);
            listView5.Columns.Add("AKTS", 50, HorizontalAlignment.Left);  //bölüm seçmeli dersleri listeleyen listview oluşturduk.
            listView5.GridLines = true;

            listView6.View = View.Details;
            listView6.Columns.Add("Üstten Dersler", 200, HorizontalAlignment.Left);
            listView6.Columns.Add("AKTS", 50, HorizontalAlignment.Left);  //bölüm seçmeli dersleri listeleyen listview oluşturduk.
            listView6.GridLines = true;


            listView7.View = View.Details;
            listView7.Columns.Add("İntibak Dersleri", 200, HorizontalAlignment.Left);
            listView7.Columns.Add("AKTS", 50, HorizontalAlignment.Left);  //bölüm seçmeli dersleri listeleyen listview oluşturduk.
            listView7.Columns.Add("Sınıf", 50, HorizontalAlignment.Left);
            listView7.GridLines = true;
         }

        void intibak_cek_goster()
        {
            DataTable tablo = new DataTable();
            string ogrenci = frm1.textBox2.Text.ToString();
            OleDbDataAdapter adtr = new OleDbDataAdapter("Select * From  "+ogrenci+" ", burak); 
            adtr.Fill(tablo);
            int gecis_mi = Convert.ToInt32(tablo.Rows[0]["intibak_mi"]);

            string a = 1 + "";
            string b = 2 + "";

            if (gecis_mi != 0)
            {
      
               
                DataTable tablo2 = new DataTable(); //alması gereken dersler birinci sınıf derssler
                OleDbDataAdapter adtr2 = new OleDbDataAdapter("Select * From  ders_listesi Where SINIF='" + a + "'    ", burak);
                adtr2.Fill(tablo2);
                

                if (gecis_mi == 2)//alması gereken dersler ikinci sınıf derssler
                {
                    OleDbDataAdapter adtr3 = new OleDbDataAdapter("Select * From  ders_listesi Where SINIF='" + b + "' And DURUM= 'ZORUNLU'  ", burak);
                    adtr3.Fill(tablo2);
                   
                }

                DataTable tablo3 = new DataTable(); //aldıgı dersler
                OleDbDataAdapter adtr4 = new OleDbDataAdapter("Select * From  " + ogrenci + " Where Ders_Sınıf='" + a + "'   ", burak);
                adtr4.Fill(tablo3);
                

                if (gecis_mi == 2)
                {
                    OleDbDataAdapter adtr5 = new OleDbDataAdapter("Select * From  " + ogrenci + " Where Ders_Sınıf='" + b + "'   ", burak);
                    adtr5.Fill(tablo3); 
                }

                
                for (int l = 0; l < tablo2.Rows.Count; l++)
                {
                    int sayac = 0;
                    for (int k = 0; k < tablo3.Rows.Count; k++)
                    {
                        if (tablo2.Rows[l]["ZORUNLU_DERS"].ToString() == tablo3.Rows[k]["Dersler"].ToString())
                        {
                            ++sayac;
                        }

                    }
                    
                    if (sayac == 0)
                    {
                        
                        String[] yazi = new String[3];
                        yazi[0] = tablo2.Rows[l]["ZORUNLU_DERS"].ToString();
                        yazi[1] = tablo2.Rows[l]["AKTS"].ToString();
                        yazi[2] = tablo2.Rows[l]["SINIF"].ToString();
                        ListViewItem itm = new ListViewItem(yazi);
                        listView7.Items.Add(itm);
                    }

                    
                }

            }

            buton7_kontrol();
        }

        

        void basarisiz_ders_cek_goster()
        {
            
            DataTable tablo = new DataTable();
            string ogrenci = frm1.textBox2.Text.ToString();
            OleDbDataAdapter adtr = new OleDbDataAdapter("Select * From  " + ogrenci + " Where  Durumu='Başarısız' AND Dönem = 'Güz'  ", burak); //öğrencinin başarısız ve devamsız derslerini veri tabanından çeker.
            adtr.Fill(tablo);

            OleDbDataAdapter adtr2 = new OleDbDataAdapter("Select * From  " + ogrenci + " Where  Durumu='Devamsız' AND Dönem = 'Güz' ", burak);
            adtr2.Fill(tablo);
           

            //başarılıları tablo 2 ye alyo
            DataTable tablo2 = new DataTable();
            OleDbDataAdapter adtr3 = new OleDbDataAdapter("Select * From  " + ogrenci + " Where  Durumu='Başarılı'  ", burak);
            adtr3.Fill(tablo2);

            ///önce başarısız sonra başarılı kontrol etyo
            for (int l = 0; l < tablo2.Rows.Count; l++)
            {
                for (int k = 0; k < tablo.Rows.Count; k++)
                {
                    if (tablo.Rows[k]["Dersler"].ToString() == tablo2.Rows[l]["Dersler"].ToString())
                    {
                        tablo.Rows[k].Delete();
                        tablo.AcceptChanges();
                    }
                }
            }

            //aynı başarısız ları kontrol etyo
            for (int l = 0; l < tablo.Rows.Count; l++)
            {
                for (int k = l+1; k < tablo.Rows.Count; k++)
                {
                    if (tablo.Rows[k]["Dersler"].ToString() == tablo.Rows[l]["Dersler"].ToString())
                    {
                        tablo.Rows[k].Delete();
                        tablo.AcceptChanges();
                    }
                }
            }
            for (int l = 0; l < tablo.Rows.Count; l++)
            {
                for (int k = l + 1; k < tablo.Rows.Count; k++)
                {
                    if (tablo.Rows[k]["Dersler"].ToString() == tablo.Rows[l]["Dersler"].ToString())
                    {
                        tablo.Rows[k].Delete();
                        tablo.AcceptChanges();
                    }
                }
            }

           
            for (int i = 0; i < tablo.Rows.Count; i++)
            {
                String[] yazi = new String[3];
                yazi[0] = tablo.Rows[i]["Dersler"].ToString();     //tabloya attığımız dersleri ve akts'leri listview'e geçirdik.
                yazi[1] = tablo.Rows[i]["AKTS"].ToString();
                yazi[2] = tablo.Rows[i]["Ders_Sınıf"].ToString();
                ListViewItem itm = new ListViewItem(yazi);
                listView1.Items.Add(itm);
            }
            buton1_kontrol();

        }
            
                                             

        void zorunlu_ders_cek_goster()
        {
            DataTable tablo4 = new DataTable();
            OleDbDataAdapter okuma2 = new OleDbDataAdapter("Select * From  ders_listesi Where SINIF= '" + label4.Text + "' And DURUM= 'ZORUNLU'  ", burak); //öğrencinin zorunlu derslerini veri tabanından çeker.
            okuma2.Fill(tablo4);

            for (int i = 0; i < tablo4.Rows.Count; i++)
            {

                String[] yazi2 = new String[2];
                yazi2[0] = tablo4.Rows[i]["ZORUNLU_DERS"].ToString();     //tabloya attığımız dersleri ve akts'leri listview'e geçirdik.
                yazi2[1] = tablo4.Rows[i]["AKTS"].ToString();
                ListViewItem itm2 = new ListViewItem(yazi2);
                listView3.Items.Add(itm2);
            }
            buton2_kontrol();
        }

        void secmeli_ders_cek_goster()
        {
            DataTable tablo5 = new DataTable();
            OleDbDataAdapter okuma3 = new OleDbDataAdapter("Select * From  ders_listesi Where SINIF= '" + label4.Text + "' And DURUM= 'SEÇMELİ'  ", burak); //öğrencinin zorunlu derslerini veri tabanından çeker.
            okuma3.Fill(tablo5);

            for (int i = 0; i < tablo5.Rows.Count; i++)
            {

                String[] yazi2 = new String[2];
                yazi2[0] = tablo5.Rows[i]["ZORUNLU_DERS"].ToString();     //tabloya attığımız dersleri ve akts'leri listview'e geçirdik.
                yazi2[1] = tablo5.Rows[i]["AKTS"].ToString();
                ListViewItem itm2 = new ListViewItem(yazi2);
                listView4.Items.Add(itm2);
            }
            buton3_kontrol();
        }

        void bolum_secmeli_ders_cek_goster()
        {
            DataTable tablo5 = new DataTable();
            OleDbDataAdapter okuma3 = new OleDbDataAdapter("Select * From  ders_listesi Where SINIF= '" + label4.Text + "' And DURUM= 'SEÇMELİ_DERS'  ", burak); //öğrencinin zorunlu derslerini veri tabanından çeker.
            okuma3.Fill(tablo5);

            for (int i = 0; i < tablo5.Rows.Count; i++)
            {

                String[] yazi2 = new String[2];
                yazi2[0] = tablo5.Rows[i]["ZORUNLU_DERS"].ToString();     //tabloya attığımız dersleri ve akts'leri listview'e geçirdik.
                yazi2[1] = tablo5.Rows[i]["AKTS"].ToString();
                ListViewItem itm2 = new ListViewItem(yazi2);
                listView5.Items.Add(itm2);
            }

            buton4_kontrol();
        }

        
        bool kontrol(string s) 
        {
            baglan2();
            string ogrenci = "s_" + frm1.textBox2.Text.ToString();
            DataTable tablo = new DataTable();
            OleDbDataAdapter adtr = new OleDbDataAdapter("Select * From  " + ogrenci + "  ", ramazan);  //öğrencinin aldığı dersleri okuduk
            adtr.Fill(tablo);

            DataTable tablo2 = new DataTable();
            OleDbDataAdapter adtr2 = new OleDbDataAdapter("Select * From  ders_programı Where LISTE = '"+ s +"'  ", ramazan);  //öğrencinin aldığı dersleri okuduk
            adtr2.Fill(tablo2);

            for (int l = 0; l < tablo.Rows.Count; l++)
            {
                if (tablo.Rows[l]["GUN"].ToString() == tablo2.Rows[0]["GUN"].ToString() && tablo.Rows[l]["DERS_ID"].ToString() == tablo2.Rows[0]["DERS_ID"].ToString())
                {
                    MessageBox.Show(tablo.Rows[l]["dersler"].ToString() + "  dersi ile  " + tablo2.Rows[0]["LISTE"].ToString() + "  dersi çakıştı..." );
                    cakisan_ekle(tablo.Rows[l]["dersler"].ToString(), tablo2.Rows[0]["LISTE"].ToString(), frm1.textBox2.Text.ToString());
                    return false;
                }
                
            }
        
            return true;
        }


        void cakisan_ekle(string a, string b, string c) 
        {
            baglan2();  //listview1 deki seçilen dersleri yeni veri tabanına atma işlemini gerçekleştirdik.
            OleDbCommand yaz = new OleDbCommand("Insert Into cakisan_dersler (d1_d2, ogr_no) Values ('" + a + " - " + b + "', '"+ c +"')", ramazan);
            yaz.ExecuteNonQuery();
        }

        int akts_say()
        {
            baglan2();
            string ogrenci =  frm1.textBox2.Text.ToString();
            DataTable tablo = new DataTable();
            OleDbDataAdapter adtr = new OleDbDataAdapter("Select * From  " + ogrenci + "  ", ramazan);  //öğrencinin aldığı dersleri okuduk
            adtr.Fill(tablo);

            int sayac = 0 ;
    
            for (int l = 0; l < tablo.Rows.Count; l++)
            {
                sayac = sayac + Convert.ToInt32(tablo.Rows[l]["akts"]);
            
            }
            
            label3.Text = "TOPLAM AKTS : " + sayac.ToString();
            return sayac;
        }


        bool ustten_alabilir_mi()
        {
            
            DataTable tablo = new DataTable();
            string ogrenci = frm1.textBox2.Text.ToString();
            OleDbDataAdapter adtr = new OleDbDataAdapter("Select * From  " + ogrenci + "   ", burak); //öğrencinin başarısız ve devamsız derslerini veri tabanından çeker.
            adtr.Fill(tablo);

            double ortalama = Convert.ToDouble( tablo.Rows[0]["Genel Not Ortalaması"].ToString());
            if (ortalama < 3)
            {
                return false;
            }
            return true;
        }

        bool ders_liste_sinif_kontrol_alttan()
        {
            for (int h = 0; h < listView1.Items.Count; h++)
            {
                if (Convert.ToInt32(listView1.SelectedItems[0].SubItems[2].Text) > Convert.ToInt32(listView1.Items[h].SubItems[2].Text))
                {
                    return false;
                }
            }
            return true;
        }

        bool ders_liste_sinif_kontrol_intibak()
        {
            for (int h = 0; h < listView7.Items.Count; h++)
            {
                if (Convert.ToInt32(listView7.SelectedItems[0].SubItems[2].Text) > Convert.ToInt32(listView7.Items[h].SubItems[2].Text))
                {
                    return false;
                }
            }
            return true;
        }

        void buton7_kontrol()
        {
            if (0 != listView7.Items.Count) button7.Enabled = true;
            if (0 == listView7.Items.Count) button7.Enabled = false;
        }
        void buton1_kontrol()
        {
            if (listView7.Items.Count == 0)
            {
                if (0 != listView1.Items.Count)
                {
                    button1.Enabled = true;
                }
            }

            if (listView1.Items.Count == 0)
            {

                button1.Enabled = false;
                     
                
            }
        }

        void buton2_kontrol()
        {
            if (listView1.Items.Count == 0 && 0 == listView7.Items.Count)
            {
                if (0 != listView3.Items.Count )
                {
               
                    button2.Enabled = true;
                }
            }

            if (listView3.Items.Count == 0)
            {
                button2.Enabled = false;

            }
        }
        void buton5_kontrol()
        {
            try
            {
                if (listView6.SelectedItems != null)
                {
                    int deneme1 = Convert.ToInt32(akts_say().ToString()) + Convert.ToInt32(listView6.SelectedItems[0].SubItems[1].Text);
                    int akts_sinir = 35;
                    if (checkBox1.Checked == true) akts_sinir = 39;
                    if (deneme1 <= akts_sinir)
                    {
                        baglan2();  //listview1 deki seçilen dersleri yeni veri tabanına atma işlemini gerçekleştirdik.
                        OleDbCommand yaz = new OleDbCommand("Insert Into " + frm1.textBox2.Text + " (Dersler, akts) Values ('" + listView6.SelectedItems[0].SubItems[0].Text + "',  '" + listView6.SelectedItems[0].SubItems[1].Text + "')", ramazan);
                        yaz.ExecuteNonQuery();

                        listView6.Items.Remove(listView6.SelectedItems[0]);  //listview1 deki seçilen satırı silme işlemi

                        DataTable tablo2 = new DataTable();
                        string ogrenci = frm1.textBox2.Text.ToString();
                        OleDbDataAdapter adtr = new OleDbDataAdapter("Select * From  " + ogrenci + "  ", ramazan);  //yeni veri tabanına eklediğimiz dersleri ve aktsleri okuma işlemini yaptık.
                        adtr.Fill(tablo2);

                        listView2.Items.Clear();  //listview2'in içindeki seçilen dersleri temizledik. 

                        for (int i = 0; i < tablo2.Rows.Count; i++)
                        {
                            String[] yazi = new String[2];
                            yazi[0] = tablo2.Rows[i]["dersler"].ToString();  //okuduğumuz dersleri listview2 ye yazdık
                            yazi[1] = tablo2.Rows[i]["akts"].ToString();
                            ListViewItem itm2 = new ListViewItem(yazi);
                            listView2.Items.Add(itm2);
                        }
                    }
                    else
                    {
                        MessageBox.Show("akts doldu...");
                    }

                    akts_say();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Seçmek istediğiniz dersi tıklayınız...");
            }
            

        }
        void buton4_kontrol()
        {
            if (listView1.Items.Count == 0 && 0 == listView7.Items.Count && listView3.Items.Count == 0)
            {
                if (0 != listView5.Items.Count)
                {
                    button4.Enabled = true;
                }
            }

            if (listView5.Items.Count == 0)
            {

                button4.Enabled = false;

            }
        }
        void buton3_kontrol()
        {
            if (label4.Text == "3")
            {
                if (listView1.Items.Count == 0 && 0 == listView7.Items.Count && listView3.Items.Count == 0 && 4 > listView5.Items.Count)
                {
                    if (0 != listView4.Items.Count)
                    {
                        button3.Enabled = true;
                    }
                }
            }
            else if (label4.Text == "4")
            {
                if (listView1.Items.Count == 0 && 0 == listView7.Items.Count && listView3.Items.Count == 0 && 8 > listView5.Items.Count)
                {
                    if (0 != listView4.Items.Count)
                    {
                        button3.Enabled = true;
                    }
                }
            }
            else
            {
                if (listView1.Items.Count == 0 && 0 == listView7.Items.Count && listView3.Items.Count == 0 )
                {
                    if (0 != listView4.Items.Count)
                    {
                        button3.Enabled = true;
                    }
                }
            }

            if (listView4.Items.Count == 0)
            {

                button3.Enabled = false;

            }
        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
            //
            // TODO: This line of code loads data into the 'projeDataSet1.ders_listesi' table. You can move, or remove it, as needed.
            this.ders_listesiTableAdapter.Fill(this.projeDataSet1.ders_listesi);



            sinif_ogren();

            list_olustur();
            intibak_cek_goster();
            if(label4.Text != "1")
            {
            basarisiz_ders_cek_goster();
            }
            zorunlu_ders_cek_goster();
            secmeli_ders_cek_goster();
            bolum_secmeli_ders_cek_goster();
            label7.Text = "Öğrenci No : " + frm1.textBox2.Text + "         Sınıf : " + label4.Text;

}
            

        private void button1_Click(object sender, EventArgs e)  //kalan ders ekle butonuna basılırsa
        {
            try
            {
                if (listView1.SelectedItems != null)
                {

                    if (ders_liste_sinif_kontrol_alttan())
                    {

                        int deneme1 = Convert.ToInt32(akts_say().ToString()) + Convert.ToInt32(listView1.SelectedItems[0].SubItems[1].Text);
                        int akts_sinir = 35;
                        if (checkBox1.Checked == true) akts_sinir = 39;
                        if (deneme1 <= akts_sinir)
                        {
                            if (kontrol(listView1.SelectedItems[0].Text.ToString()))
                            {

                                baglan2();  //listview1 deki seçilen dersleri yeni veri tabanına atma işlemini gerçekleştirdik.
                                OleDbCommand yaz = new OleDbCommand("Insert Into " + frm1.textBox2.Text + " (Dersler, akts) Values ('" + listView1.SelectedItems[0].SubItems[0].Text + "',  '" + listView1.SelectedItems[0].SubItems[1].Text + "')", ramazan);
                                yaz.ExecuteNonQuery();

                                listView1.Items.Remove(listView1.SelectedItems[0]);  //listview1 deki seçilen satırı silme işlemi

                                DataTable tablo2 = new DataTable();
                                string ogrenci = frm1.textBox2.Text.ToString();
                                OleDbDataAdapter adtr = new OleDbDataAdapter("Select * From  " + ogrenci + "  ", ramazan);  //yeni veri tabanına eklediğimiz dersleri ve aktsleri okuma işlemini yaptık.
                                adtr.Fill(tablo2);

                                listView2.Items.Clear();  //listview2'in içindeki seçilen dersleri temizledik. 

                                for (int i = 0; i < tablo2.Rows.Count; i++)
                                {
                                    String[] yazi = new String[2];
                                    yazi[0] = tablo2.Rows[i]["dersler"].ToString();  //okuduğumuz dersleri listview2 ye yazdık
                                    yazi[1] = tablo2.Rows[i]["akts"].ToString();
                                    ListViewItem itm2 = new ListViewItem(yazi);
                                    listView2.Items.Add(itm2);
                                }

                            }
                        }
                        else
                        {
                            MessageBox.Show("akts doldu...");
                        }

                        akts_say();

                    }
                    else
                    {
                        MessageBox.Show("Önce alt dönem derslerini seçiniz...");
                    }
                }
            }
            catch (Exception)
            {
               MessageBox.Show("Seçmek istediğiniz dersi tıklayınız...");
            }

            buton7_kontrol();
            buton1_kontrol();
            buton2_kontrol();
            buton4_kontrol();
            buton3_kontrol();
        }

        private void button2_Click(object sender, EventArgs e) //zorunlu ders ekle
        {
            try
            {
                if (listView3.SelectedItems != null)
                {
                    int deneme1 = Convert.ToInt32(akts_say().ToString()) + Convert.ToInt32(listView3.SelectedItems[0].SubItems[1].Text);
                    int akts_sinir = 35;
                    if (checkBox1.Checked == true) akts_sinir = 39;
                    if (deneme1 <= akts_sinir)
                    {
                        if (kontrol(listView3.SelectedItems[0].Text.ToString()))
                        {
                            baglan2();  //listview1 deki seçilen dersleri yeni veri tabanına atma işlemini gerçekleştirdik.
                            OleDbCommand yaz = new OleDbCommand("Insert Into " + frm1.textBox2.Text + " (Dersler, akts) Values ('" + listView3.SelectedItems[0].SubItems[0].Text + "',  '" + listView3.SelectedItems[0].SubItems[1].Text + "')", ramazan);
                            yaz.ExecuteNonQuery();

                            listView3.Items.Remove(listView3.SelectedItems[0]);  //listview1 deki seçilen satırı silme işlemi

                            DataTable tablo2 = new DataTable();
                            string ogrenci = frm1.textBox2.Text.ToString();
                            OleDbDataAdapter adtr = new OleDbDataAdapter("Select * From  " + ogrenci + "  ", ramazan);  //yeni veri tabanına eklediğimiz dersleri ve aktsleri okuma işlemini yaptık.
                            adtr.Fill(tablo2);

                            listView2.Items.Clear();  //listview2'in içindeki seçilen dersleri temizledik. 

                            for (int i = 0; i < tablo2.Rows.Count; i++)
                            {
                                String[] yazi = new String[2];
                                yazi[0] = tablo2.Rows[i]["dersler"].ToString();  //okuduğumuz dersleri listview2 ye yazdık
                                yazi[1] = tablo2.Rows[i]["akts"].ToString();
                                ListViewItem itm2 = new ListViewItem(yazi);
                                listView2.Items.Add(itm2);
                            }
                        
                        }
                    }
                    else
                    {
                        MessageBox.Show("akts doldu...");
                    }
                    akts_say();
                    
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Seçmek istediğiniz dersi tıklayınız...");
            }

            buton7_kontrol();
            buton1_kontrol();
            buton2_kontrol();
            buton4_kontrol();
            buton3_kontrol();

        }              

        private void button3_Click(object sender, EventArgs e)//seçmeli ders ekle
        {
            try
            {
                if (listView4.SelectedItems != null)
                {
                    int deneme1 = Convert.ToInt32(akts_say().ToString()) + Convert.ToInt32(listView4.SelectedItems[0].SubItems[1].Text);
                    int akts_sinir = 35;
                    if (checkBox1.Checked == true) akts_sinir = 39;
                    if (deneme1 <= akts_sinir)
                    {
                        baglan2();  //listview1 deki seçilen dersleri yeni veri tabanına atma işlemini gerçekleştirdik.
                        OleDbCommand yaz = new OleDbCommand("Insert Into " + frm1.textBox2.Text + " (Dersler, akts) Values ('" + listView4.SelectedItems[0].SubItems[0].Text + "',  '" + listView4.SelectedItems[0].SubItems[1].Text + "')", ramazan);
                        yaz.ExecuteNonQuery();

                        listView4.Items.Remove(listView4.SelectedItems[0]);  //listview1 deki seçilen satırı silme işlemi

                        DataTable tablo2 = new DataTable();
                        string ogrenci = frm1.textBox2.Text.ToString();
                        OleDbDataAdapter adtr = new OleDbDataAdapter("Select * From  " + ogrenci + "  ", ramazan);  //yeni veri tabanına eklediğimiz dersleri ve aktsleri okuma işlemini yaptık.
                        adtr.Fill(tablo2);

                        listView2.Items.Clear();  //listview2'in içindeki seçilen dersleri temizledik. 

                        for (int i = 0; i < tablo2.Rows.Count; i++)
                        {
                            String[] yazi = new String[2];
                            yazi[0] = tablo2.Rows[i]["dersler"].ToString();  //okuduğumuz dersleri listview2 ye yazdık
                            yazi[1] = tablo2.Rows[i]["akts"].ToString();
                            ListViewItem itm2 = new ListViewItem(yazi);
                            listView2.Items.Add(itm2);
                        }
                    }
                    else
                    {
                        MessageBox.Show("akts doldu...");
                    }
       
                    akts_say();
                    
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Seçmek istediğiniz dersi tıklayınız...");
            }
            buton7_kontrol();
            buton1_kontrol();
            buton2_kontrol();
            buton4_kontrol();
            buton3_kontrol();
        }

        private void button4_Click(object sender, EventArgs e)  //bolum seçmeli ders ekle
        {
            try
            {
                if (listView5.SelectedItems != null)
                {
                    int deneme1 = Convert.ToInt32(akts_say().ToString()) + Convert.ToInt32(listView5.SelectedItems[0].SubItems[1].Text);
                    int akts_sinir = 35;
                    if (checkBox1.Checked == true) akts_sinir = 39;
                    if (deneme1 <= akts_sinir)
                    {
                        if (kontrol(listView5.SelectedItems[0].Text.ToString()))
                        {
                            baglan2();  //listview1 deki seçilen dersleri yeni veri tabanına atma işlemini gerçekleştirdik.
                            OleDbCommand yaz = new OleDbCommand("Insert Into " + frm1.textBox2.Text + " (Dersler, akts) Values ('" + listView5.SelectedItems[0].SubItems[0].Text + "',  '" + listView5.SelectedItems[0].SubItems[1].Text + "')", ramazan);
                            yaz.ExecuteNonQuery();

                            listView5.Items.Remove(listView5.SelectedItems[0]);  //listview1 deki seçilen satırı silme işlemi

                            DataTable tablo2 = new DataTable();
                            string ogrenci = frm1.textBox2.Text.ToString();
                            OleDbDataAdapter adtr = new OleDbDataAdapter("Select * From  " + ogrenci + "  ", ramazan);  //yeni veri tabanına eklediğimiz dersleri ve aktsleri okuma işlemini yaptık.
                            adtr.Fill(tablo2);

                            listView2.Items.Clear();  //listview2'in içindeki seçilen dersleri temizledik. 

                            for (int i = 0; i < tablo2.Rows.Count; i++)
                            {
                                String[] yazi = new String[2];
                                yazi[0] = tablo2.Rows[i]["dersler"].ToString();  //okuduğumuz dersleri listview2 ye yazdık
                                yazi[1] = tablo2.Rows[i]["akts"].ToString();
                                ListViewItem itm2 = new ListViewItem(yazi);
                                listView2.Items.Add(itm2);
                            }

                            label1.Text = listView5.Items.Count.ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("akts doldu...");
                    }
                    akts_say();
                    
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Seçmek istediğiniz dersi tıklayınız...");
            }
            buton7_kontrol();
            buton1_kontrol();
            buton2_kontrol();
            buton4_kontrol();
            buton3_kontrol();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView6.SelectedItems != null)
                {
                    int deneme1 = Convert.ToInt32(akts_say().ToString()) + Convert.ToInt32(listView6.SelectedItems[0].SubItems[1].Text);
                    int akts_sinir = 35;
                    if (checkBox1.Checked == true) akts_sinir = 39;
                    if (deneme1 <= akts_sinir)
                    {
                        baglan2();  //listview1 deki seçilen dersleri yeni veri tabanına atma işlemini gerçekleştirdik.
                        OleDbCommand yaz = new OleDbCommand("Insert Into " + frm1.textBox2.Text + " (Dersler, akts) Values ('" + listView6.SelectedItems[0].SubItems[0].Text + "',  '" + listView6.SelectedItems[0].SubItems[1].Text + "')", ramazan);
                        yaz.ExecuteNonQuery();

                        listView6.Items.Remove(listView6.SelectedItems[0]);  //listview1 deki seçilen satırı silme işlemi

                        DataTable tablo2 = new DataTable();
                        string ogrenci = frm1.textBox2.Text.ToString();
                        OleDbDataAdapter adtr = new OleDbDataAdapter("Select * From  " + ogrenci + "  ", ramazan);  //yeni veri tabanına eklediğimiz dersleri ve aktsleri okuma işlemini yaptık.
                        adtr.Fill(tablo2);

                        listView2.Items.Clear();  //listview2'in içindeki seçilen dersleri temizledik. 

                        for (int i = 0; i < tablo2.Rows.Count; i++)
                        {
                            String[] yazi = new String[2];
                            yazi[0] = tablo2.Rows[i]["dersler"].ToString();  //okuduğumuz dersleri listview2 ye yazdık
                            yazi[1] = tablo2.Rows[i]["akts"].ToString();
                            ListViewItem itm2 = new ListViewItem(yazi);
                            listView2.Items.Add(itm2);
                        }
                    }
                    else
                    {
                        MessageBox.Show("akts doldu...");
                    }

                    akts_say();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Seçmek istediğiniz dersi tıklayınız...");
            }
            

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataTable tablo = new DataTable();
            string ogrenci = frm1.textBox2.Text.ToString();
            OleDbDataAdapter adtr = new OleDbDataAdapter("Select * From  " + ogrenci + " ", burak);
            adtr.Fill(tablo);
            int gecis_mi = Convert.ToInt32(tablo.Rows[0]["intibak_mi"]);

            if (ustten_alabilir_mi() && listView3.Items.Count == 0 && listView5.Items.Count == 0  && gecis_mi==0)
            {
                DataTable tablo5 = new DataTable();
                OleDbDataAdapter okuma3 = new OleDbDataAdapter("Select * From  ders_listesi Where SINIF= '" + (Convert.ToInt32(label4.Text) + 1).ToString() + "' And DURUM= 'ZORUNLU'  ", burak); //öğrencinin zorunlu derslerini veri tabanından çeker.
                okuma3.Fill(tablo5);

                for (int i = 0; i < tablo5.Rows.Count; i++)
                {
                    String[] yazi2 = new String[2];
                    yazi2[0] = tablo5.Rows[i]["ZORUNLU_DERS"].ToString();     //tabloya attığımız dersleri ve akts'leri listview'e geçirdik.
                    yazi2[1] = tablo5.Rows[i]["AKTS"].ToString();
                    ListViewItem itm2 = new ListViewItem(yazi2);
                    listView6.Items.Add(itm2);
                }
                button6.Enabled = false;
                button5.Enabled = true;
  
            }
            else {
                MessageBox.Show("üstten ders alamazsınız!..");
            }
            

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView7.SelectedItems != null)
                {

                    if (ders_liste_sinif_kontrol_intibak())
                    {

                        int deneme1 = Convert.ToInt32(akts_say().ToString()) + Convert.ToInt32(listView7.SelectedItems[0].SubItems[1].Text);
                        int akts_sinir = 35;
                        if (checkBox1.Checked == true) akts_sinir = 39;
                        if (deneme1 <= akts_sinir)
                        {
                            if (kontrol(listView7.SelectedItems[0].Text.ToString()))
                            {

                                baglan2();  //listview1 deki seçilen dersleri yeni veri tabanına atma işlemini gerçekleştirdik.
                                OleDbCommand yaz = new OleDbCommand("Insert Into " + frm1.textBox2.Text + " (Dersler, akts) Values ('" + listView7.SelectedItems[0].SubItems[0].Text + "',  '" + listView7.SelectedItems[0].SubItems[1].Text + "')", ramazan);
                                yaz.ExecuteNonQuery();

                                listView7.Items.Remove(listView7.SelectedItems[0]);  //listview7 deki seçilen satırı silme işlemi

                                DataTable tablo2 = new DataTable();
                                string ogrenci = frm1.textBox2.Text.ToString();
                                OleDbDataAdapter adtr = new OleDbDataAdapter("Select * From  " + ogrenci + "  ", ramazan);  //yeni veri tabanına eklediğimiz dersleri ve aktsleri okuma işlemini yaptık.
                                adtr.Fill(tablo2);

                                listView2.Items.Clear();  //listview2'in içindeki seçilen dersleri temizledik. 

                                for (int i = 0; i < tablo2.Rows.Count; i++)
                                {
                                    String[] yazi = new String[2];
                                    yazi[0] = tablo2.Rows[i]["dersler"].ToString();  //okuduğumuz dersleri listview2 ye yazdık
                                    yazi[1] = tablo2.Rows[i]["akts"].ToString();
                                    ListViewItem itm2 = new ListViewItem(yazi);
                                    listView2.Items.Add(itm2);
                                }

                            
                            }
                        }
                        else
                        {
                            MessageBox.Show("akts doldu...");
                        }

                        akts_say();

                    }
                    else
                    {
                        MessageBox.Show("Önce alt dönem derslerini seçiniz...");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Seçmek istediğiniz dersi tıklayınız...");
            }

            buton7_kontrol();
            buton1_kontrol();
            buton2_kontrol();
            buton4_kontrol();
            buton3_kontrol();
        }

        private void button8_Click(object sender, EventArgs e)          //RAPOR
        {
            frm1.frm3.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
            Application.Exit();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            frm1.frm4.ShowDialog();
        }

        
    }
}

