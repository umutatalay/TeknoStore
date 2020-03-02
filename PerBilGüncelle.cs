using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TeknoStore
{
    public partial class PerBilGüncelle : Form
    {
        public PerBilGüncelle()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-IT4752E;Initial Catalog=TeknoStore;Integrated Security=True");
        public void Listele()
        {
            SqlDataAdapter listele = new SqlDataAdapter("Select CalisanID,Calisan_Adi,Calisan_Soyadi,durum_text,calisan_Eposta,Calisan_TelefonNo,Calisan_Adres from Calisan,tbl_calisandurum where Calisan.Calisan_Durum=tbl_calisandurum.Calisan_Durum", baglantı);
            DataSet tbl = new DataSet();
            listele.Fill(tbl);

            dataGridView1.DataSource = tbl.Tables[0];
        }
        private void PerBilGüncelle_Load(object sender, EventArgs e)
        {
            panel1.BackColor = System.Drawing.Color.FromArgb(35, 30, 59);
           

            this.BackColor = Color.FromArgb(35, 30, 80);
            Listele();
            baglantı.Open();
            SqlCommand kategorilistele = new SqlCommand("Select * from tbl_calisandurum", baglantı);
            SqlDataReader read = kategorilistele.ExecuteReader();
            while (read.Read())
            {
                comboBox1.Items.Add(read["durum_text"]);
            }
            baglantı.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            String c_id = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            String Ad = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            String Soyad = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            String Telefon = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            String Eposta = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            String Adres = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            String Durum = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            // String Parola = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
            int status = -1;
            if (Durum=="Yönetici")
            {
                status = 1;
            }
            else if (Durum=="Çalışan")
            {
                status = 0;
            }

            comboBox1.SelectedItem = status;
            comboBox1.SelectedIndex = status;
            textBox1.Text = Ad;
            textBox2.Text = Soyad;
            textBox3.Text = Telefon;
            textBox4.Text = Eposta;
            textBox5.Text = Adres;
            label9.Text = c_id;
            // textBox7.Text = Parola;
            label10.Text = label9.Text + " Numaralı " + textBox1.Text+ " " + textBox2.Text+  " için \n yeni parolayı belirleyin. ";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand guncelle = new SqlCommand("UPDATE Calisan set Calisan_Adi=@p1,Calisan_Soyadi=@p2,Calisan_TelefonNo=@p3,calisan_Eposta=@p4,Calisan_Adres=@p5,Calisan_Durum=@p7 where CalisanID=@p6", baglantı);
            guncelle.Parameters.AddWithValue("@p1", textBox1.Text);
            guncelle.Parameters.AddWithValue("@p2", textBox2.Text);
            guncelle.Parameters.AddWithValue("@p3", textBox3.Text);
            guncelle.Parameters.AddWithValue("@p4", textBox4.Text);
            guncelle.Parameters.AddWithValue("@p5", textBox5.Text);
            guncelle.Parameters.AddWithValue("@p6", label9.Text);
            guncelle.Parameters.AddWithValue("@p7", comboBox1.SelectedIndex);

            guncelle.ExecuteNonQuery();
            baglantı.Close();
            Listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand sguncelle = new SqlCommand("UPDATE Calisan set Parola=@p8 where CalisanID=@p6", baglantı);
            sguncelle.Parameters.AddWithValue("@p8", textBox7.Text);
            sguncelle.Parameters.AddWithValue("@p6", label9.Text);
            
            sguncelle.ExecuteNonQuery();
            baglantı.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
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
    }
}
