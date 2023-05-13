using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeritabaninaBaglanma
{
    internal class Ogrenci
    {

        // Entity Class : Verileri taşıdığımız class'lara entity class denir. Veritabanındaki alanlara karşılık gelen değişkenlerin tanımlandığı class..

        public int Id { get; set; }

        public string Adi { get; set; }

        public string Soyadi { get; set; }

        public char Cinsiyet { get; set; }

        public string Sinif { get; set; }

        public DateTime DogumTarihi { get; set; }

        public bool AktifMi { get; set; }

        public DateTime KayitTarihi { get; set; }





    }
}
