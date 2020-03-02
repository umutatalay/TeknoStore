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
    public partial class MusteriEkle : Form
    {
        public MusteriEkle()
        {
            InitializeComponent();
        }

        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-IT4752E;Initial Catalog=TeknoStore;Integrated Security=True");

        public void Listele()
        {
            SqlDataAdapter listele = new SqlDataAdapter("Select * from Musteriler", baglantı);
            DataSet tbl = new DataSet();
            listele.Fill(tbl);
            dataGridView1.DataSource = tbl.Tables[0];
        }

        private void MusteriEkle_Load(object sender, EventArgs e)
        {
            Listele();
            panel1.BackColor = System.Drawing.Color.FromArgb(35, 30, 59);
        

            this.BackColor = Color.FromArgb(35, 30, 80);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand ekle = new SqlCommand("insert into Musteriler (Musteri_Ad,Musteri_Soyad,Musteri_Tel,Musteri_Eposta,Musteri_TCKN,Musteri_Adres) values(@p1,@p2,@p3,@p4,@p5,@p6)", baglantı);
            ekle.Parameters.AddWithValue("@p1", textBox1.Text);
            ekle.Parameters.AddWithValue("@p2", textBox2.Text);
            ekle.Parameters.AddWithValue("@p3", textBox3.Text);
            ekle.Parameters.AddWithValue("@p4", textBox4.Text);
            ekle.Parameters.AddWithValue("@p5", textBox5.Text);
            ekle.Parameters.AddWithValue("@p6", textBox6.Text);


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
