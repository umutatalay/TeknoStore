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
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        public static int per_status = -1;

        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-IT4752E;Initial Catalog=TeknoStore;Integrated Security=True");
        private void Giris_Load(object sender, EventArgs e)
        {
            panel1.BackColor = System.Drawing.Color.FromArgb(35, 30, 59);

            this.BackColor = Color.FromArgb(35, 30, 80);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand sorgu = new SqlCommand("select * from Calisan where calisan_Eposta='" + textBox1.Text + "' and Calisan_Durum=1 AND Parola='" + textBox2.Text + "'", baglantı);
            SqlDataReader oku = sorgu.ExecuteReader();

            if (oku.Read())
            {

                Form1 frm = new Form1();
             //   per_status = 1;

                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı yada parola hatalı lütfen tekar deneyiniz. ");
            }
            baglantı.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand sorgu = new SqlCommand("select * from Calisan where calisan_Eposta='" + textBox3.Text + "' and Calisan_Durum=0 AND Parola='" + textBox4.Text + "'", baglantı);
            SqlDataReader oku = sorgu.ExecuteReader();

            if (oku.Read())
            {
                CalisanArayuzu clsa = new CalisanArayuzu();
                clsa.Show();
                this.Hide();
               // per_status = 0;
            }
            else
            {
                MessageBox.Show("Kullanıcı adı yada parola hatalı lütfen tekar deneyiniz. ");
            }
            baglantı.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
