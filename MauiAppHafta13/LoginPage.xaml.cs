using MauiAppHafta13.Servisler;

namespace MauiAppHafta13;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();

    }
    private void Reg_Clicked(object sender, EventArgs e)
    {
        if (loginStack.IsVisible)
        {
            loginStack.IsVisible = false;
            registerStack.IsVisible = true;
        }
        else
        {
            loginStack.IsVisible = true;
            registerStack.IsVisible = false;

        }
    }

    private async void Login_Clicked(object sender, EventArgs e)
    {
        var res = await FireBaseServices.Login(Email.Text, Password.Text);
        if (res)
            await Navigation.PopModalAsync();
        else
            await DisplayAlert("Hata", "Kullanýcý adý veya þifre hatalý!", "OK");
    }


    private async void Register_Clicked(object sender, EventArgs e)
    {
        var res = await FireBaseServices.Register(rDispName.Text, rEmail.Text, rPassword.Text);
        if (res)
            await Navigation.PopModalAsync();
        else
            await DisplayAlert("Hata", "Kayýt baþarýsýz!", "OK");

    }
}