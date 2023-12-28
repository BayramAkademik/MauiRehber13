using Rehber_EL;

namespace MauiAppHafta13
{

    public partial class KisiDetay : ContentPage
    {
        public bool Result = false;
        public Kisi mKisi = null;
        bool edit = false;

        public Action<Kisi> AddMethod = null;


        public KisiDetay(Kisi kisi=null)
        {
            InitializeComponent();

            if (kisi == null)
            {
                mKisi = new Kisi();
                edit = false;
            }
            else
            {
                txtAd.Text = kisi.Ad;
                txtSoy.Text = kisi.Soyad;
                txtTel.Text = kisi.Telefon;
                txtMail.Text = kisi.Mail;
                txtAddr.Text = kisi.Adres;
                mKisi = kisi;
                edit = true;

            }
        }

        private void OKClicked(object sender, EventArgs e)
        {
            Result = true;
            mKisi.Ad = txtAd.Text;
            mKisi.Soyad = txtSoy.Text;
            mKisi.Telefon = txtTel.Text;
            mKisi.Mail = txtMail.Text;
            mKisi.Adres = txtAddr.Text;

            //if (!edit)
            {
                if (AddMethod != null)
                    AddMethod(mKisi);

            }

            Navigation.PopModalAsync();
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            Result = false;
            Navigation.PopModalAsync();
        }
    }
}
