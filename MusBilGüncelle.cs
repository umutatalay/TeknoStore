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
    public partial class MusBilGüncelle : Form
    {
        public MusBilGüncelle()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-IT4752E;Initial Catalog=TeknoStore;Integrated Security=True");
        private void label2_Click(object sender, EventArgs e)
        {

        }

        public void Listele()
        {
            SqlDataAdapter listele = new SqlDataAdapter("Select * from Musteriler", baglantı);
            DataSet tbl = new DataSet();
            listele.Fill(tbl);
            dataGridView1.DataSource = tbl.Tables[0];
        }

        private void MusBilGüncelle_Load(object sender, EventArgs e)
        {
            panel1.BackColor = System.Drawing.Color.FromArgb(35, 30, 59);
            this.BackColor = Color.FromArgb(35, 30, 80);
            Listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            String mid = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            String Ad = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            String soyad = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            String Telefon = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            String eposta = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            String Adres = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            String TCKN = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
           
            label8.Text = mid;
            textBox1.Text = Ad;
            textBox2.Text = soyad;
            textBox3.Text = TCKN;
            textBox4.Text = Telefon;
            textBox5.Text = eposta;
            textBox6.Text = Adres;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand guncelle = new SqlCommand("UPDATE Musteriler set Musteri_Ad=@p1,Musteri_Soyad=@p2,Musteri_Tel=@p3,Musteri_Eposta=@p4,Musteri_Adres=@p5,Musteri_TCKN=@p6 where Musteri_Id=@p7", baglantı);
            guncelle.Parameters.AddWithValue("@p1", textBox1.Text);
            guncelle.Parameters.AddWithValue("@p2", textBox2.Text);
            guncelle.Parameters.AddWithValue("@p3", textBox4.Text);
            guncelle.Parameters.AddWithValue("@p4", textBox5.Text);
            guncelle.Parameters.AddWithValue("@p5", textBox6.Text);
            guncelle.Parameters.AddWithValue("@p6", textBox3.Text);
            guncelle.Parameters.AddWithValue("@p7", label8.Text);
            guncelle.ExecuteNonQuery();
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
