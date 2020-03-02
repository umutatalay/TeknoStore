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
    public partial class UrunEkleSil : Form
    {
        public UrunEkleSil()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-IT4752E;Initial Catalog=TeknoStore;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text==""|| textBox2.Text == ""  || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == ""  || textBox8.Text == "")
            {
                MessageBox.Show("Tüm alanları doldurmalısınız.");
            }
            else
            {

             baglantı.Open();
            SqlCommand ekle = new SqlCommand("insert into Urun (Urun_Marka,Urun_Adi,kategoriID,Urun_Fiyat,Urun_Adet,Urun_Renk,Urun_Ozellik) values(@p1,@p2,@p3,@p4,@p5,@p6,@p8)", baglantı);
            ekle.Parameters.AddWithValue("@p1", textBox1.Text);
            ekle.Parameters.AddWithValue("@p2", textBox2.Text);
            ekle.Parameters.AddWithValue("@p3", comboBox1.SelectedIndex +1);
            ekle.Parameters.AddWithValue("@p4", Convert.ToDouble(textBox4.Text));
            ekle.Parameters.AddWithValue("@p5", (textBox5.Text));
            ekle.Parameters.AddWithValue("@p6", textBox6.Text);
            ekle.Parameters.AddWithValue("@p8", textBox8.Text);

            ekle.ExecuteNonQuery();
            baglantı.Close();
            Listele();
        }

        }

        public void Listele()
        {
            SqlDataAdapter listele = new SqlDataAdapter("Select Urun_Id,Urun_Marka,Urun_Adi,Urun_Renk,Urun_Adet from Urun", baglantı);
            DataSet tbl = new DataSet();
            listele.Fill(tbl);
            dataGridView1.DataSource = tbl.Tables[0];
        }

        private void UrunEkleSil_Load(object sender, EventArgs e)
        {
            panel1.BackColor = System.Drawing.Color.FromArgb(35, 30, 59);
      

            this.BackColor = Color.FromArgb(35, 30, 80);
            baglantı.Open();
            SqlCommand kategorilistele = new SqlCommand("Select * from Kategoriler",baglantı);
            SqlDataReader read = kategorilistele.ExecuteReader();
            while (read.Read())
            {
                comboBox1.Items.Add(read["Kategori_Adi"]);
            }
            baglantı.Close();


            Listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand sil = new SqlCommand("DELETE From Urun where Urun_Id=@Barkod",baglantı);
            sil.Parameters.AddWithValue("@barkod", textBox9.Text);
            sil.ExecuteNonQuery();
            baglantı.Close();
            Listele();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Giris.per_status==1)
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
