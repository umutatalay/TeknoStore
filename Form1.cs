using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeknoStore
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stok stk = new Stok();
            stk.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UrunEkleSil ues = new UrunEkleSil();
            ues.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FiyatGuncelle FG = new FiyatGuncelle();
            FG.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Satis sts = new Satis();

            sts.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MusteriEkle me = new MusteriEkle();
            me.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PersonelEkle pe = new PersonelEkle();
            pe.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            PerBilGüncelle pbg = new PerBilGüncelle();
            pbg.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MusBilGüncelle mbg = new MusBilGüncelle();
            mbg.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MusHarcamaGecmis mhg = new MusHarcamaGecmis();
            mhg.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.BackColor= System.Drawing.Color.FromArgb(35, 30,59);

            Giris.per_status = 1;
            this.BackColor = Color.FromArgb(35, 30, 80);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
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
    }
}
