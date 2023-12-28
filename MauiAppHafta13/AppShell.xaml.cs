namespace MauiAppHafta13
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            Navigation.PushModalAsync(new LoginPage());

            base.OnAppearing();
        }
    }
}