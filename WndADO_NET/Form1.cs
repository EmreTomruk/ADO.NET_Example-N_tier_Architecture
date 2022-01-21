using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WndADO_NET
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            Veritabani vt = new Veritabani();
            Kitap kitap = new Kitap();
            kitap.KitapAdi = txtKitapAdi.Text;
            kitap.Yazar = txtYazar.Text;
            kitap.Fiyat = decimal.Parse(txtFiyat.Text);
            kitap.YayinEvi = txtYayinEvi.Text;

            vt.KitapEkle(kitap);
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            Veritabani vt = new Veritabani();
            //dataGridView1.DataSource = vt.ListeleDT();

            dataGridView1.DataSource = vt.Listele();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            Veritabani vt = new Veritabani();
            Kitap kitap = vt.KitapAra(int.Parse(txtKitapID.Text));

            if (kitap.KitapID < 0)
            {
                MessageBox.Show("Aradığınız kayıt bulunamadı...");
            }
            else
            {
                AktifPasif(true);
                Ata(kitap);               
            }
        }

        private void AktifPasif(bool deger)
        {
            btnGuncelle.Enabled = deger;
            btnSil.Enabled = deger;          
        }

        private void btnAdaGoreAra_Click(object sender, EventArgs e)
        {
            Veritabani vt = new Veritabani();
            List<Kitap> kitaplar = vt.KitapAdaGoreAra(txtKitapAdi.Text);

            if (kitaplar.Count == 0)
                MessageBox.Show("Kritere uygun kitap bulunamadi...");
            else {
                dataGridView1.DataSource = kitaplar;
                AktifPasif(true);
            }
        }

        private void Ata(Kitap kitap)
        {
            txtKitapID.Text = kitap.KitapID.ToString();
            txtKitapAdi.Text = kitap.KitapAdi;
            txtYazar.Text = kitap.Yazar;
            txtFiyat.Text = kitap.Fiyat.ToString();
            txtYayinEvi.Text = kitap.YayinEvi;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //  MessageBox.Show(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            //if (dataGridView1.Rows.Count > 0)
            //{
                
            //}
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int kitapID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            Veritabani vt = new Veritabani();
            Kitap kitap = vt.KitapAra(kitapID);
            Ata(kitap);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Veritabani vt = new Veritabani();
            Kitap kitap = new Kitap();

            kitap.KitapID = int.Parse(txtKitapID.Text);
            kitap.KitapAdi = txtKitapAdi.Text;
            kitap.Yazar = txtYazar.Text;
            kitap.Fiyat = decimal.Parse(txtFiyat.Text);
            kitap.YayinEvi = txtYayinEvi.Text;
            vt.KitapGuncelle(kitap);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
           DialogResult dr = MessageBox.Show("İlgili Kaydı silmek istiyor musunuz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes) {
                Veritabani vt = new Veritabani();
                vt.KitapSil(int.Parse(txtKitapID.Text));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder scb = new SqlConnectionStringBuilder();

            scb.DataSource = @"(localdb)\MSSQLLocalDB";
            scb.InitialCatalog = "Kitaplar";
            scb.IntegratedSecurity = true;

            MessageBox.Show(scb.ConnectionString);
        }
    }
}
