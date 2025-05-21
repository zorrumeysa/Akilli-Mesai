using System;

namespace AkilliMesaiPlanlayici.Services
{
    public static class MesaiHesaplayici
    {
        /// <summary>
        /// Fazla mesai ücreti hesaplar. 
        /// Fazla mesai primi 1.5x, resmi tatil ise 2x uygulanır.
        /// </summary>
        /// <param name="saatSayisi">Çalışılan fazla saat</param>
        /// <param name="saatlikUcret">Personelin saatlik ücreti</param>
        /// <param name="isResmiTatil">Resmi tatil mi?</param>
        /// <returns>Fazla mesai tutarı</returns>
        public static decimal HesaplaFazlaMesai(decimal saatSayisi, decimal saatlikUcret, bool isResmiTatil = false)
        {
            // Resmi tatil günlerindeki mesai 2x, normal günlerde 1.5x
            var katsayi = isResmiTatil ? 2m : 1.5m;
            return Math.Round(saatSayisi * saatlikUcret * katsayi, 2);
        }
    }
}
