using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WndADO_NET
{
    class Veritabani
    {
        //App.config dosyasından al...
        
        string strConn;

        public Veritabani()
        {
            strConn = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            
        }

        public void KitapEkle(Kitap kitap) 
        {
            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Kitaplar VALUES(@ad, @yazar, @fiyat, @yayinevi)", conn)) {
                
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ad", kitap.KitapAdi);
                cmd.Parameters.AddWithValue("@yazar", kitap.Yazar);
                cmd.Parameters.AddWithValue("@fiyat", kitap.Fiyat);
                cmd.Parameters.AddWithValue("@yayinevi", kitap.YayinEvi);

                cmd.ExecuteNonQuery();
            }
        }

        public Kitap KitapAra(int kitapID)
        {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Kitaplar WHERE KitapID = @ID", conn);

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ID", kitapID);

            SqlDataReader dr = cmd.ExecuteReader();

            Kitap kitap = new Kitap();
            kitap.KitapID = -1;

            dr.Read();
            if (dr.HasRows)
            {
                kitap.KitapID = Convert.ToInt32(dr[0]);
                kitap.KitapAdi = dr[1].ToString();
                kitap.Yazar = dr[2].ToString();
                kitap.Fiyat = Convert.ToDecimal(dr[3]);
                kitap.YayinEvi = dr[4].ToString();
            }           
            conn.Close();

            return kitap;
        }

        public List<Kitap> KitapAdaGoreAra(string ara)
        {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Kitaplar WHERE KitapAdi LIKE '%" + ara + "%'", conn);

            cmd.Parameters.Clear();
           // cmd.Parameters.AddWithValue("@ara", ara);

            SqlDataReader dr = cmd.ExecuteReader();

            List<Kitap> kitaplar = new List<Kitap>();
            while (dr.Read())
            {
                Kitap kitap = new Kitap();
               
                kitap.KitapID = Convert.ToInt32(dr[0]);
                kitap.KitapAdi = dr[1].ToString();
                kitap.Yazar = dr[2].ToString();
                kitap.Fiyat = Convert.ToDecimal(dr[3]);
                kitap.YayinEvi = dr[4].ToString();

                kitaplar.Add(kitap);
            }
            conn.Close();

            return kitaplar;
        }

        public void KitapGuncelle(Kitap kitap)
        {
            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();
            using (SqlCommand cmd = new SqlCommand("UPDATE Kitaplar SET KitapAdi = @ad, Yazar = @yazar, Fiyat = @fiyat, YayinEvi = @yayinevi WHERE KitapID = @ID", conn))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID", kitap.KitapID);
                cmd.Parameters.AddWithValue("@ad", kitap.KitapAdi);
                cmd.Parameters.AddWithValue("@yazar", kitap.Yazar);
                cmd.Parameters.AddWithValue("@fiyat", kitap.Fiyat);
                cmd.Parameters.AddWithValue("@yayinevi", kitap.YayinEvi);
                cmd.ExecuteNonQuery();
            }
        }

        public void KitapSil(int kitapID)
        {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Kitaplar WHERE KitapID = @ID ", conn))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID", kitapID);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable ListeleDT()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Kitaplar", strConn);
            DataTable dt = new DataTable();

            da.Fill(dt);
            return dt;
        }

        public List<Kitap> Listele()
        {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Kitaplar", conn);

            SqlDataReader dr = cmd.ExecuteReader();

            List<Kitap> kitaplar = new List<Kitap>();
            while (dr.Read())
            {
                Kitap kitap = new Kitap();

                kitap.KitapID = Convert.ToInt32(dr[0]);
                kitap.KitapAdi = dr[1].ToString();
                kitap.Yazar = dr[2].ToString();
                kitap.Fiyat = Convert.ToDecimal(dr[3]);
                kitap.YayinEvi = dr[4].ToString();

                kitaplar.Add(kitap);
            }
            conn.Close();

            return kitaplar;
        }
    }
}
