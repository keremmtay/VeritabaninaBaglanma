using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace VeritabaninaBaglanma
{
    internal class AdoNetDersleriGiris
    {
        static void Main(string[] args)
        {
            #region Ado.Net Dersleri Giriş

            OgrenciListele();

            //OgrenciEkle();

            //OgrenciEkle1("Ayşe", "Yıldız", "K", "205", "02/02/2001", "True", DateTime.Now.ToString());

            //OgrenciVerisiAl();

            //OgrenciSil(11);

            Ogrenci ogrenci = new Ogrenci();

            ogrenci.Id = 8;

            ogrenci.Adi = "Aslan";

            ogrenci.Soyadi = "Kaplan";

            ogrenci.Cinsiyet = 'E';

            ogrenci.Sinif = "505";

            //ogrenci.DogumTarihi = new DateTime (03/02/1999);

            ogrenci.AktifMi = true;

            OgrenciGuncelle(ogrenci);

            #endregion



        }

        private static void OgrenciVerisiAl()
        {
            Ogrenci ogrenci = new Ogrenci();

            Console.Write("Öğrenci Adı");
            ogrenci.Adi = Console.ReadLine();
            Console.Write("Öğrenci Soyadı");
            ogrenci.Soyadi = Console.ReadLine();
            Console.Write("Cinsiyeti");
            ogrenci.Cinsiyet = Convert.ToChar(Console.ReadLine());
            Console.Write("Sınıfı");
            ogrenci.Sinif = Console.ReadLine();
            Console.Write("Doğum Tarihi");
            ogrenci.DogumTarihi = Convert.ToDateTime(Console.ReadLine());
            ogrenci.AktifMi = true;
            ogrenci.KayitTarihi = DateTime.Now;

            OgrenciEkle2(ogrenci);
        }

        public static void OgrenciListele()
        {
            // Sql Server'a bağlanmak için bir kütüphanemiz var, bunu sayfamıza eklememiz gerekiyor. Bu kütüphane içindeki bazı Class'lara ihtiyacımız olacak.
            // System.Data.SqlClient isimli kütüphanemizi Sql Server'a bağlanmak için kullanıyoruz.
            // Kütüphaneyi ekledikten sonra Veritabanına bağlanmak için kullanacağım bir bağlantı nesnesine ihtiyacım var. Bunu da SqlConnection isimli sınıftan oluşturuyorum.

            SqlConnection baglanti = new SqlConnection();

            // Bağlantı nesnesini oluşturduk fakat hangi servera, hangi veritabanına bağlanacağım henüz belli değil. Bunun için gerekli olan adresi yani ConnectionString'i, oluşturduğumuz bu nesneye vermemiz gerekiyor.

            // Server adında \ işareti var ise yanına bir adet \ daha ekleyerek sorun çözülebilir.

            baglanti.ConnectionString = "Server=ASUS;Database=OkulKitapligi;Integrated Security=true";

            // Şu aşamaya kadar veritabanımıza bağlanmak için gerekli kodları yazdık.. Ama henüz bir işlem yapmadık. 
            // CRUD işlemleri yapmak için kullandığımız komutları çalıştırmak için SqlCommand türünde bir nesneye ihtiyacımız var.

            SqlCommand komut = new SqlCommand();

            komut.CommandText = "select * from Ogrenciler";

            // SqlCommand sınıfındaki CommandText property'sine SQL komutunu set ediyorum.

            // Komut nesnesinin Connection isimli property'sine benim bir bağlantı nesnesi vermem gerekiyor. Yukarıda oluşturduğum bağlantı nesnesini veriyorum.

            komut.Connection = baglanti;

            // Artık bizim komutumuz ve baglantı için gerekli ayarlarımız hazır.
            // Bu aşamada bağlantımı Open() metodu ile açmalıyım ve komutlarımı çalıştırmalıyım.
            // Öncelikle bağlantımızı kontrol edyoruz. Eğer açık bağlantı var ise bunu kullanabiliriz. Yok ise bağlantımızı açmalıyız.

            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }

            // Çalışan komutlar sonucunda gelen veriyi ekrana yazdıracağım. Bunun için gelen veriyi ilgili SqlDataReader class nesnesi ile almam gerekiyor.

            SqlDataReader sqlData = komut.ExecuteReader();

            // Öncelikle Veritabanından veri gelip gelmediğini kontrol etmem gerekiyor.

            if (sqlData.HasRows)
            {
                while (sqlData.Read())
                {
                    string Id = sqlData["Id"].ToString();
                    string Adi = sqlData["Adi"].ToString();
                    string Soyadi = sqlData["Soyadi"].ToString();
                    string Cinsiyet = sqlData["Cinsiyet"].ToString();
                    string Sinif = sqlData["Sinif"].ToString();
                    string DogumTarihi = sqlData["DogumTarihi"].ToString();
                    string AktifMi = sqlData["AktifMi"].ToString();
                    string KayitTarihi = sqlData["KayitTarihi"].ToString();

                    Console.WriteLine($"Id..: {Id} - Adı..: {Adi} - Soyadı..: {Soyadi} - Cinsiyeti..: {Cinsiyet} - Sınıfı..: {Sinif}");

                }
            }
            else
            {
                Console.WriteLine("Öğrenciler tablosunda listelenecek veri yok.");
            }

            // İlgili komutu çalıştırdıktan sonra ve verileri aldıktan sonra açmış olduğum bağlantıyı kapatmam gerekir.

            // Bağlantıyı kapatmadan önce if ile açık olup olmadığını kontrol ediyorum.

            if (baglanti.State == ConnectionState.Open)
            {
                baglanti.Close();
            }
        }

        public static void OgrenciEkle()
        {

            Console.WriteLine("Öğrenci Ekleniyor......");

            SqlConnection baglanti = new SqlConnection();

            baglanti.ConnectionString = "Server=ASUS;Database=OkulKitapligi;Integrated Security=true";

            SqlCommand komut = new SqlCommand();

            komut.CommandText = "Insert Into Ogrenciler (Adi, Soyadi, Cinsiyet, Sinif, DogumTarihi, AktifMi, KayitTarihi)" +
                "Values (@adi, @soyadi, @cinsiyet, @sinif, @dogumTarihi, @aktifMi, @kayitTarihi)";

            komut.Parameters.AddWithValue("@adi", "Ahmet");
            komut.Parameters.AddWithValue("@soyadi", "Çiçek");
            komut.Parameters.AddWithValue("@cinsiyet", "E");
            komut.Parameters.AddWithValue("@sinif", "203");
            komut.Parameters.AddWithValue("@dogumTarihi", "01/01/2000");
            komut.Parameters.AddWithValue("@aktifMi", "True");
            komut.Parameters.AddWithValue("@kayitTarihi", "10/03/2023");

            komut.Connection = baglanti;

            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }

            komut.ExecuteNonQuery();

            if (baglanti.State == ConnectionState.Open)
            {
                baglanti.Close();
            }

            Console.WriteLine("Öğrenci Eklendi......");

            OgrenciListele();

        }

        public static void OgrenciEkle1(string adi, string soyadi, string cinsiyet, string sinif, string dogumTarihi, string aktifMi, string kayitTarihi)
        {

            Console.WriteLine("Öğrenci Ekleniyor......");

            SqlConnection baglanti = new SqlConnection();

            baglanti.ConnectionString = "Server=ASUS;Database=OkulKitapligi;Integrated Security=true";

            SqlCommand komut = new SqlCommand();

            komut.CommandText = "Insert Into Ogrenciler (Adi, Soyadi, Cinsiyet, Sinif, DogumTarihi, AktifMi, KayitTarihi)" +
                "Values (@adi, @soyadi, @cinsiyet, @sinif, @dogumTarihi, @aktifMi, @kayitTarihi)";

            komut.Parameters.AddWithValue("@adi", adi);
            komut.Parameters.AddWithValue("@soyadi", soyadi);
            komut.Parameters.AddWithValue("@cinsiyet", cinsiyet);
            komut.Parameters.AddWithValue("@sinif", sinif);
            komut.Parameters.AddWithValue("@dogumTarihi", dogumTarihi);
            komut.Parameters.AddWithValue("@aktifMi", aktifMi);
            komut.Parameters.AddWithValue("@kayitTarihi", kayitTarihi);

            komut.Connection = baglanti;

            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }

            komut.ExecuteNonQuery();

            if (baglanti.State == ConnectionState.Open)
            {
                baglanti.Close();
            }

            Console.WriteLine("Öğrenci Eklendi......");

            OgrenciListele();

        }

        public static void OgrenciEkle2(Ogrenci ogrenci)
        {

            Console.WriteLine("Öğrenci Ekleniyor......");

            SqlConnection baglanti = new SqlConnection();

            baglanti.ConnectionString = "Server=ASUS;Database=OkulKitapligi;Integrated Security=true";

            SqlCommand komut = new SqlCommand();

            komut.CommandText = "Insert Into Ogrenciler (Adi, Soyadi, Cinsiyet, Sinif, DogumTarihi, AktifMi, KayitTarihi)" +
                "Values (@adi, @soyadi, @cinsiyet, @sinif, @dogumTarihi, @aktifMi, @kayitTarihi)";

            komut.Parameters.AddWithValue("@adi", ogrenci.Adi);
            komut.Parameters.AddWithValue("@soyadi", ogrenci.Soyadi);
            komut.Parameters.AddWithValue("@cinsiyet", ogrenci.Cinsiyet);
            komut.Parameters.AddWithValue("@sinif", ogrenci.Sinif);
            komut.Parameters.AddWithValue("@dogumTarihi", ogrenci.DogumTarihi);
            komut.Parameters.AddWithValue("@aktifMi", ogrenci.AktifMi);
            komut.Parameters.AddWithValue("@kayitTarihi", ogrenci.KayitTarihi);

            komut.Connection = baglanti;

            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }

            komut.ExecuteNonQuery();

            if (baglanti.State == ConnectionState.Open)
            {
                baglanti.Close();
            }

            Console.WriteLine("Öğrenci Eklendi......");

            OgrenciListele();

        }

        private static void OgrenciSil(int ogrenciId)
        {
            Console.WriteLine("Öğrenci Siliniyor......");

            SqlConnection baglanti = new SqlConnection();

            baglanti.ConnectionString = "Server=ASUS;Database=OkulKitapligi;Integrated Security=true";

            SqlCommand komut = new SqlCommand();

            komut.CommandText = "Delete from Ogrenciler where Id = @ogrId";

            komut.Parameters.AddWithValue("@ogrId", ogrenciId);

            komut.Connection = baglanti;

            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }

            komut.ExecuteNonQuery();

            if (baglanti.State == ConnectionState.Open)
            {
                baglanti.Close();
            }

            Console.WriteLine("Öğrenci Silindi......");

            OgrenciListele();
        }

        public static void OgrenciGuncelle(Ogrenci ogrenci)
        {
            Console.WriteLine("Öğrenci Güncelleniyor......");

            SqlConnection baglanti = new SqlConnection();

            baglanti.ConnectionString = "Server=ASUS;Database=OkulKitapligi;Integrated Security=true";

            SqlCommand komut = new SqlCommand();

            komut.CommandText = "Update Ogrenciler set Adi = @adi, Soyadi = @soyadi, Cinsiyet = @cinsiyet, Sinif = @sinif, DogumTarihi = @dogumTarihi, AktifMi = @aktifMi";

            komut.Parameters.AddWithValue("@adi", ogrenci.Adi);
            komut.Parameters.AddWithValue("@soyadi", ogrenci.Soyadi);
            komut.Parameters.AddWithValue("@cinsiyet", ogrenci.Cinsiyet);
            komut.Parameters.AddWithValue("@sinif", ogrenci.Sinif);
            //komut.Parameters.AddWithValue("@dogumTarihi", ogrenci.DogumTarihi);
            komut.Parameters.AddWithValue("@aktifMi", ogrenci.AktifMi);

            komut.Connection = baglanti;

            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }

            komut.ExecuteNonQuery();

            if (baglanti.State == ConnectionState.Open)
            {
                baglanti.Close();
            }

            Console.WriteLine("Öğrenci Güncellendi......");

            OgrenciListele();


        }
    }
}
