using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeknoStore
{
    public partial class Satis : Form

    {
        int urunstok = 0;
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-IT4752E;Initial Catalog=TeknoStore;Integrated Security=True");
        public Satis()
        {
            InitializeComponent();
        }
        double d = 0;
        private void sepetilistele()
        {
            SqlDataAdapter listele = new SqlDataAdapter("Select * from Sepet", baglanti);
            DataSet tbl = new DataSet();
            listele.Fill(tbl);
            dataGridView1.DataSource = tbl.Tables[0];
            baglanti.Close();
        }
        private void Satis_Load(object sender, EventArgs e)
        {
            panel1.BackColor = System.Drawing.Color.FromArgb(35, 30, 59);

            this.BackColor = Color.FromArgb(35, 30, 80);
            SepetiTemizle();
            sepetilistele();
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
        }
        int cıkartılacak = 0;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand bul = new SqlCommand("Select * from Musteriler where Musteri_TCKN like '" + textBox1.Text.ToString() + "'",baglanti);
            SqlDataReader yazdirid = bul.ExecuteReader();
            while (yazdirid.Read())
            {
                textBox3.Text = yazdirid["Musteri_Ad"].ToString() +" " + yazdirid["Musteri_Soyad"].ToString();
                textBox2.Text = yazdirid["Musteri_Tel"].ToString();
                textBox9.Text = yazdirid["Musteri_Id"].ToString();
            }

            baglanti.Close();
        }
        double S_EUA = 0;
        private void button3_Click(object sender, EventArgs e)

        {
            double tekilurunfiyatı = double.Parse(textBox7.Text);
            double EUA = double.Parse(textBox8.Text);
            double c = tekilurunfiyatı * EUA;
            if (textBox4.Text=="" || textBox7.Text=="")
            {
                MessageBox.Show("Ürünü yada ürün adeti seçimi yapılmadı.");
            }
            else if (EUA > urunstok)
            {
                MessageBox.Show(" Stokta bulunandan daha fazla ürün sayısı girdiniz. ");
            }
            else
            {
                cıkartılacak = Convert.ToInt32(textBox8.Text);
                EUA = EUA - double.Parse(textBox8.Text);
                S_EUA = EUA;
            baglanti.Open();
            SqlCommand sepeteekle = new SqlCommand("insert into Sepet (Urun_Id,Urun_Marka,Urun_Adi,Urun_Fiyat,Urun_TopSatAdet,Urun_ToplamFiyat,Tarih) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7)", baglanti);
            sepeteekle.Parameters.AddWithValue("@p1", textBox4.Text);
            sepeteekle.Parameters.AddWithValue("@p2", textBox5.Text);
            sepeteekle.Parameters.AddWithValue("@p3", textBox6.Text);
            sepeteekle.Parameters.AddWithValue("@p4", double.Parse(textBox7.Text));
            sepeteekle.Parameters.AddWithValue("@p5", textBox8.Text);
            sepeteekle.Parameters.AddWithValue("@p6", c);
            sepeteekle.Parameters.AddWithValue("@p7", DateTime.Now.ToString());

                d = d + c;
            
            label5.Text = d.ToString();
            sepeteekle.ExecuteNonQuery();
            baglanti.Close();
            sepetilistele();

                int degisken = globalurunstok - Convert.ToInt32(textBox8.Text);
                label12.Text=("Bu üründen ekleebilecek max adet " + degisken);
                

            }
        }
        public void sepetstokdus()
        {
            baglanti.Open();
            SqlCommand gecicidus = new SqlCommand("UPDATE Urun SET Urun_Adet = Urun_Adet '"+textBox8.Text+"'  Where Urun_Id='"+textBox4.Text+"'");
            baglanti.Close();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
            for (int i = 0; i <= dataGridView1.Rows.Count-1; i++)
            {
                baglanti.Open();
                SqlCommand sepeteekle = new SqlCommand("insert into Satis (UrunID,MusteriID,STarih) values(@p1,@p2,@p3)", baglanti);
                sepeteekle.Parameters.AddWithValue("@p1", dataGridView1.Rows[i].Cells["Urun_Id"].Value.ToString());
                sepeteekle.Parameters.AddWithValue("@p2", textBox9.Text);
                sepeteekle.Parameters.AddWithValue("@p3", dataGridView1.Rows[i].Cells["Tarih"].Value.ToString());
                label5.Text = d.ToString();
                sepeteekle.ExecuteNonQuery();
                int AzUrunID = 0;
                int AzUrunMiktar = 0;
                AzUrunID = int.Parse(dataGridView1.Rows[i].Cells["Urun_Id"].Value.ToString());
                AzUrunMiktar = int.Parse(dataGridView1.Rows[i].Cells["Urun_TopSatAdet"].Value.ToString());

                SqlCommand stoktandus = new SqlCommand("UPDATE Urun SET Urun_Adet = Urun_Adet - '"+AzUrunMiktar+ "' Where Urun_Id='"+AzUrunID+"'",baglanti);
                stoktandus.ExecuteNonQuery();
               //  SqlCommand stokdus = new SqlCommand("Update Urun SET Urun_Adet = Urun_Adet - '" + int.Parse(dataGridView1.Rows[i].Cells["Urun_TopSatAdet"].Value.ToString())+ "'  where Urun_Id='" + dataGridView1.Rows[i].Cells["Urun_Id"].Value.ToString() +"'",baglanti);
                baglanti.Close();
            }
            SepetiTemizle();
            EklemeyiTemizle();
           
        }
        private void EklemeyiTemizle()
        {
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            label12.Text = "";
        }

        int globalurunstok = 0;
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
            baglanti.Open();
            SqlCommand bul = new SqlCommand("Select * from Urun where Urun_Id like '" + textBox4.Text.ToString() + "'", baglanti);
            SqlDataReader yazdir = bul.ExecuteReader();
            while (yazdir.Read())
            {
                textBox5.Text = yazdir["Urun_Marka"].ToString();
                textBox6.Text = yazdir["Urun_Adi"].ToString();
                textBox7.Text = yazdir["Urun_Fiyat"].ToString();
                urunstok = Convert.ToInt32(yazdir["Urun_Adet"].ToString());
            }

            baglanti.Close();
            int bir = 1;
            textBox8.Text = bir.ToString();
            if (urunstok==0)
            {
                label12.Text=(" Şu anda bu ürün stokta bulunmamaktadır. ");
            }
            else
            {
               
                label12.Text = "Bu üründen en fazla " + urunstok + " adet eklenebilir. ! ";
                globalurunstok = urunstok;
            }
            
        }
       
        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            //baglanti.Open();
            //SqlCommand bul = new SqlCommand("Select * from Musteriler where Musteri_Id like '" + textBox9.Text.ToString() + "'", baglanti);
            //SqlDataReader yazdir = bul.ExecuteReader();
            //while (yazdir.Read())
            //{
            //    textBox3.Text = yazdir["Musteri_Ad"].ToString() + " " + yazdir["Musteri_Soyad"].ToString();
            //    textBox2.Text = yazdir["Musteri_Tel"].ToString();
            //    textBox1.Text = yazdir["Musteri_TCKN"].ToString();
            //}

            //baglanti.Close();
        }

        private void SepetiTemizle()
        {
            baglanti.Open();
            SqlCommand sil = new SqlCommand("Delete from Sepet", baglanti);
            sil.ExecuteNonQuery();
            sepetilistele();
            baglanti.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            SepetiTemizle();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MusteriEkle me = new MusteriEkle();
            me.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
        Font baslik = new Font("Verdana",12,FontStyle.Bold);
        Font Icerik = new Font("Verdana", 8,FontStyle.Bold);
        Font Top = new Font("Verdana", 12, FontStyle.Bold);
        Font Tarih = new Font("Verdana", 12);
        SolidBrush sb = new SolidBrush(Color.Black);

        String yazdirici; int aralık = 50;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            StringFormat sformat = new StringFormat();
            sformat.Alignment = StringAlignment.Near;

            e.Graphics.DrawString("Müsteri Adı : "+textBox3.Text+" ", baslik, sb, 375, 50);
            e.Graphics.DrawString(" FATURA ",baslik,sb,375,150);
            e.Graphics.DrawString("UBN", baslik, sb, 35, 250);
            e.Graphics.DrawString("Marka", baslik, sb, 125, 250);
            e.Graphics.DrawString("Urun Adi ", baslik, sb, 200, 250);
            e.Graphics.DrawString("Fiyatı", baslik, sb, 475, 250);
            e.Graphics.DrawString("Adet", baslik, sb, 550, 250);
            e.Graphics.DrawString("Toplam Fiyat", baslik, sb, 650, 250);
            for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
            {
                
                yazdirici = dataGridView1.Rows[i].Cells["Urun_Id"].Value.ToString();
                e.Graphics.DrawString(yazdirici, Icerik, sb, 35, 250+aralık);
                yazdirici = dataGridView1.Rows[i].Cells["Urun_Marka"].Value.ToString();
                e.Graphics.DrawString(yazdirici, Icerik, sb, 125, 250+aralık);
                yazdirici = dataGridView1.Rows[i].Cells["Urun_Adi"].Value.ToString();
                e.Graphics.DrawString(yazdirici, Icerik, sb, 200, 250 + aralık);
                yazdirici = dataGridView1.Rows[i].Cells["Urun_Fiyat"].Value.ToString();
                e.Graphics.DrawString(yazdirici, Icerik, sb, 475, 250 + aralık);
                yazdirici = dataGridView1.Rows[i].Cells["Urun_TopSatAdet"].Value.ToString();
                e.Graphics.DrawString(yazdirici, Icerik, sb, 550, 250 + aralık);
                yazdirici = dataGridView1.Rows[i].Cells["Urun_ToplamFiyat"].Value.ToString();
                e.Graphics.DrawString(yazdirici, Icerik, sb, 650, 250 + aralık); 

                aralık = aralık + 50;



            }

            e.Graphics.DrawString("Toplam Fiyat : "+ label5.Text+" TL" , Top, sb, 375,1000);
            e.Graphics.DrawString("Tarih : " + DateTime.Now.ToString() + " ", Tarih, sb, 50, 50);

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            String urun_id = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            label14.Text = urun_id;
            

           

        }

        private void button7_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cıkar = new SqlCommand("DELETE from Sepet Where Urun_Id='"+label14.Text+"'",baglanti);
            cıkar.ExecuteNonQuery();
            baglanti.Close();
            sepetilistele();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "00000000000";
        }

        private void button10_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            if (Giris.per_status == 1)
            {
                Form1 frm = new Form1();
                frm.Show();
                this.Hide();
            }
            else if (Giris.per_status == 0)
            {
                CalisanArayuzu ca = new CalisanArayuzu();
                ca.Show();
                this.Hide();
                
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
