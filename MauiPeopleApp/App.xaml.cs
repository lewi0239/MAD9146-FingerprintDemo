namespace MauiPeopleApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new MauiPeopleApp.Views.AuthPage();
    }
}
