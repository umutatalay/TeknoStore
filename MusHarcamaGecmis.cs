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
    public partial class MusHarcamaGecmis : Form
    {
        public MusHarcamaGecmis()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-IT4752E;Initial Catalog=TeknoStore;Integrated Security=True");
        private void MusHarcamaGecmis_Load(object sender, EventArgs e)
        {
            panel1.BackColor = System.Drawing.Color.FromArgb(35, 30, 59);
         

            this.BackColor = Color.FromArgb(35, 30, 80);
        }
        public void Listele()
        {
            SqlDataAdapter listele = new SqlDataAdapter("Select * from Satis", baglantı);
            DataSet tbl = new DataSet();
            listele.Fill(tbl);

            dataGridView1.DataSource = tbl.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label5.Text=="")
            {
                MessageBox.Show("Müsteri numarası yada TCKN girilmedi ! ");
            }
            SqlDataAdapter listele = new SqlDataAdapter("Select STarih,Urun_Adi from Satis,Urun where Urun.Urun_Id=Satis.UrunID AND MusteriID='"+label5.Text.ToString()+"'", baglantı);
            DataSet tbl = new DataSet();
            listele.Fill(tbl);

            dataGridView1.DataSource = tbl.Tables[0];
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand bul = new SqlCommand("Select * from Musteriler where Musteri_TCKN like '" + textBox1.Text.ToString() + "'", baglantı);
            SqlDataReader yazdirid = bul.ExecuteReader();
            while (yazdirid.Read())
            {
                label4.Text =  yazdirid["Musteri_Ad"].ToString() + " " + yazdirid["Musteri_Soyad"].ToString()+ " için harcama kayıtları dökümü";
                label5.Text = yazdirid["Musteri_Id"].ToString();
            }

            baglantı.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        

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
