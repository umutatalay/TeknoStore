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
    public partial class PersonelEkle : Form
    {
        public PersonelEkle()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-IT4752E;Initial Catalog=TeknoStore;Integrated Security=True");
        private void PersonelEkle_Load(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand kategorilistele = new SqlCommand("Select * from tbl_calisandurum", baglantı);
            SqlDataReader read = kategorilistele.ExecuteReader();
            while (read.Read())
            {
                comboBox1.Items.Add(read["durum_text"]);
            }
            baglantı.Close();

            Listele();
            panel1.BackColor = System.Drawing.Color.FromArgb(35, 30, 59);
     

            this.BackColor = Color.FromArgb(35, 30, 80);
        }

        public void Listele()
        {
            SqlDataAdapter listele = new SqlDataAdapter("Select Calisan_Adi,Calisan_Soyadi,durum_text,calisan_Eposta,Calisan_TelefonNo,Calisan_Adres from Calisan,tbl_calisandurum where Calisan.Calisan_Durum=tbl_calisandurum.Calisan_Durum", baglantı);
            DataSet tbl = new DataSet();
            listele.Fill(tbl);

            dataGridView1.DataSource = tbl.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand ekle = new SqlCommand("insert into Calisan (Calisan_Adi,Calisan_Soyadi,Calisan_TelefonNo,calisan_Eposta,Calisan_Adres,Calisan_Durum,Parola) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7)", baglantı);
            ekle.Parameters.AddWithValue("@p1", textBox1.Text);
            ekle.Parameters.AddWithValue("@p2", textBox2.Text);
            ekle.Parameters.AddWithValue("@p3", textBox3.Text);
            ekle.Parameters.AddWithValue("@p4", textBox4.Text);
            ekle.Parameters.AddWithValue("@p5", textBox5.Text);
            ekle.Parameters.AddWithValue("@p6", comboBox1.SelectedIndex);
            ekle.Parameters.AddWithValue("@p7", textBox6.Text);

            ekle.ExecuteNonQuery();
            baglantı.Close();

            Listele();
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
