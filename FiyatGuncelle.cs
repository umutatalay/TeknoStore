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
    public partial class FiyatGuncelle : Form
    {
        public FiyatGuncelle()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-IT4752E;Initial Catalog=TeknoStore;Integrated Security=True");
        private void FiyatGuncelle_Load(object sender, EventArgs e)
        {
           
            Listele();
            panel1.BackColor = System.Drawing.Color.FromArgb(35, 30, 59);
           
            this.BackColor = Color.FromArgb(35, 30, 80);
        }
        public void Listele()
        {
            SqlDataAdapter listele = new SqlDataAdapter("Select * from Urun", baglantı);
            DataSet tbl = new DataSet();
            listele.Fill(tbl);
            dataGridView1.DataSource = tbl.Tables[0];
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            String barkod = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            String marka = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            String Ad = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            String kategoriID = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            String Fiyat = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            String Adet = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            String Renk = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            String Ozellik = dataGridView1.Rows[secilen].Cells[7].Value.ToString();

            label10.Text = barkod;
            textBox2.Text = marka;
            textBox3.Text = Ad;
            textBox4.Text = kategoriID;
            textBox5.Text = Fiyat;
            textBox6.Text = Adet;
            textBox7.Text = Renk;
            textBox9.Text = Ozellik;

           //pictureBox1.ImageLocation = "Resources\cola.png ";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand guncelle = new SqlCommand("UPDATE Urun set Urun_Fiyat=@p1 where Urun_Id=@p2",baglantı);
            guncelle.Parameters.AddWithValue("@p1", textBox5.Text);
            guncelle.Parameters.AddWithValue("@p2", label10.Text);
            guncelle.ExecuteNonQuery();
            baglantı.Close();
            Listele();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
