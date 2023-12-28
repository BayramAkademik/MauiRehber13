using Rehber_EL;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace MauiAppHafta13.Servisler
{
    public static class KurServis
    {
        private static async Task<string> GetKurData(string url = "https://hasanadiguzel.com.tr/api/kurgetir")
        {
            string jsondata = null;


            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync(url);

            message.EnsureSuccessStatusCode();

            jsondata = await message.Content.ReadAsStringAsync();

            return jsondata;
        }


        public static async Task<List<TCMBAnlikKurBilgileri>> GetKurlar()
        {
            //List<KurData> list = new List<KurData>();
            var jsondata = await GetKurData();
            var Kurlar = JsonSerializer.Deserialize<Root>(jsondata);
            //foreach(var i in Kurlar.TCMB_AnlikKurBilgileri)
            //{
            //    list.Add(new KurData()
            //    {
            //        Isim = i.Isim,
            //        ForexBuying = i.ForexBuying.ToString(),
            //        ForexSelling = i.ForexSelling.ToString(),
            //    });
            //}

            //return list;
            return Kurlar.TCMB_AnlikKurBilgileri;
        }

    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Developer
    {
        public string name { get; set; }
        public string website { get; set; }
        public string email { get; set; }
    }

    public class Root
    {
        public Developer Developer { get; set; }
        public List<TCMBAnlikKurBilgileri> TCMB_AnlikKurBilgileri { get; set; }
    }

    public class TCMBAnlikKurBilgileri
    {
        public string Isim { get; set; }
        public string CurrencyName { get; set; }
        public object ForexBuying { get; set; }
        public object ForexSelling { get; set; }
        public object BanknoteBuying { get; set; }
        public object BanknoteSelling { get; set; }
        public object CrossRateUSD { get; set; }
        public object CrossRateOther { get; set; }
    }

    //public class KurData 
    //{
    //    public string Isim { get; set; }
    //    public object ForexBuying { get; set; }
    //    public object ForexSelling { get; set; }

    //}


}
