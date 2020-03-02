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
    public partial class CalisanArayuzu : Form
    {
        public CalisanArayuzu()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MusteriEkle me = new MusteriEkle();
            me.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MusBilGüncelle mbG = new MusBilGüncelle();
            mbG.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Satis sts = new Satis();
            sts.Show();
            this.Hide();
        }

        private void CalisanArayuzu_Load(object sender, EventArgs e)
        {
            Giris.per_status = 0;
            panel1.BackColor = System.Drawing.Color.FromArgb(35, 30, 59);
             
            this.BackColor=Color.FromArgb(35, 30,80);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UrunEkleSil ues = new UrunEkleSil();
            ues.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stok stk = new Stok();
            stk.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MusHarcamaGecmis mhg = new MusHarcamaGecmis();
            mhg.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }
    }
}
