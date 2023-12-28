using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

using Rehber_BL;
using Rehber_EL;

namespace MauiAppHafta13
{
    public partial class KisiListesi : ContentPage
    {
        public KisiListesi()
        {
            InitializeComponent();

            //if (File.Exists(dataFile))
            //{
            //    var data = File.ReadAllText(dataFile);
            //    Kisiler = JsonSerializer.Deserialize<ObservableCollection<Kisi>>(data);
            //}

            if(!BL.KisileriYukle(ref message))
                 DisplayAlert("Error", message, "Cancel");

            listKisiler.ItemsSource = BL.Kisiler;
        }

        
        //string dataFile = Path.Combine( FileSystem.Current.AppDataDirectory, "kisilerData.json");

        private async void KisiEkleEvent(object sender, EventArgs e)
        {
            KisiDetay page = new KisiDetay()
            {
                Title="Kişi Ekle",
                AddMethod = AddKisi
            };
            await Navigation.PushModalAsync(page, true);

        }
        private async void KisiDuzenleEvent(object sender, EventArgs e)
        {
            var ib = sender as MenuItem;
            var kisi = BL.Kisiler.First(o=>o.ID == ib.CommandParameter.ToString());
            KisiDetay page = new KisiDetay(kisi)
            {
                Title="Kişi Düzenle",
                AddMethod = EditKisi
            };
            await Navigation.PushModalAsync(page, true);
        }

        private async void KisiSilEvent(object sender, EventArgs e)
        {
            var ib = sender as MenuItem;
            var kisi = BL.Kisiler.First(o=>o.ID == ib.CommandParameter.ToString());
           
            bool answer = await DisplayAlert("Silmeyi Onayla", $"{kisi.AdSoyad} silinsin mi", "Evet", "Hayır");

            if (answer)
                //BL.Kisiler.Remove(kisi);
                if(!BL.KisiSil(kisi, ref message))
                    await DisplayAlert("Error", message, "Cancel");
        }


        private async void ResimEkleEvent(object sender, EventArgs e)
        {
            var ib = sender as MenuItem;
            var kisi = BL.Kisiler.First(o=>o.ID == ib.CommandParameter.ToString());

            var res = await DisplayActionSheet("Resim seç:", null, null, "Galeri", "Kamera");
            if (res == "Galeri")
            {
                var value= await MediaPicker.Default.PickPhotoAsync();
                kisi.Resim = value.FullPath;

                EditKisi(kisi);
            }
            else if (res == "Kamera")
            {

                if (MediaPicker.Default.IsCaptureSupported)
                {
                    var value =await  MediaPicker.Default.CapturePhotoAsync();
                    if (value != null)
                        kisi.Resim = value.FullPath;

                    EditKisi(kisi);

                }
            }
        }


        private void AddKisi(Kisi kisi)
        {
            if (!BL.KisiEkle(kisi, ref message))
                DisplayAlert("Error", message, "Cancel");
        }

        string message = "";
        private void EditKisi(Kisi kisi)
        {
            if (!BL.KisiDuzenle(kisi, ref message))
                DisplayAlert("Error", message, "Cancel");
        }



        //private void SaveList(object sender, EventArgs e)
        //{
        //    var jsonData = JsonSerializer.Serialize<ObservableCollection<Kisi>>(Kisiler);

        //    File.WriteAllText(dataFile, jsonData);
        //}

        //private async void DosyaPaylas(object sender, EventArgs e)
        //{
        //    var file = new ShareFileRequest()
        //    {
        //        Title = "Kişiler",
        //        File= new ShareFile( dataFile),
        //    };

        //    await Share.RequestAsync(file);
        //}

        //private void DosyaKaydet(object sender, EventArgs e)
        //{
        //    SaveList(null, null);
        //}



    }


}
