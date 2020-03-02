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
    public partial class Stok : Form
    {
        public Stok()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-IT4752E;Initial Catalog=TeknoStore;Integrated Security=True");

        private void Stok_Load(object sender, EventArgs e)
        {
            panel1.BackColor = System.Drawing.Color.FromArgb(35, 30, 59);
          

            this.BackColor = Color.FromArgb(35, 30, 80);
            SqlDataAdapter listele = new SqlDataAdapter("select Urun_Id,Urun_Marka,Urun_Adi,Kategori_Adi,Urun_Renk,Urun_Adet" +
                " from Urun,Kategoriler where Urun.kategoriID=Kategoriler.KategoriID", baglantı);
            DataSet tbl = new DataSet();
            listele.Fill(tbl);
            
            dataGridView1.DataSource = tbl.Tables[0];
           
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                // dataGridView1.Rows[i].Cells["Urun_Gorsel"].Value= Image.FromFile("Resources\cola.png");
            }


            //
            baglantı.Open();
            SqlCommand kategorilistele = new SqlCommand("Select * from Kategoriler", baglantı);
            SqlDataReader read = kategorilistele.ExecuteReader();
            while (read.Read())
            {
                comboBox1.Items.Add(read["Kategori_Adi"]);
            }
            baglantı.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter ara = new SqlDataAdapter("Select * from Urun Where Urun_Marka LIKE '%"+textBox1.Text+ "%' OR Urun_Adi LIKE '%" + textBox1.Text + "%'", baglantı);
            DataSet tablo = new DataSet();
            ara.Fill(tablo);
            dataGridView1.DataSource = tablo.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlDataAdapter filtrele = new SqlDataAdapter("select Urun_Id,Urun_Marka,Urun_Adi,Kategori_Adi,Urun_Renk,Urun_Adet from Urun,Kategoriler where Urun.kategoriID=Kategoriler.KategoriID AND Kategori_Adi='" + (comboBox1.SelectedItem).ToString()+"'", baglantı);
            label3.Text = (comboBox1.SelectedItem).ToString();
            DataSet tablo = new DataSet();
            filtrele.Fill(tablo);
            dataGridView1.DataSource = tablo.Tables[0];
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
