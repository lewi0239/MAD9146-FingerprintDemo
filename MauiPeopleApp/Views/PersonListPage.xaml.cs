using MauiPeopleApp.Models;
using MauiPeopleApp.ViewModels;

namespace MauiPeopleApp.Views;

public partial class PersonListPage : ContentPage
{
    private PersonListViewModel ViewModel => BindingContext as PersonListViewModel;

    public PersonListPage()
    {
        InitializeComponent();
        BindingContext = new PersonListViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (ViewModel?.People?.Count == 0)
            ViewModel?.LoadPeopleCommand?.Execute(null);
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection?.FirstOrDefault() as Person;
        if (selected is null) return;

        // clear selection so it isn’t stuck highlighted on back nav
        if (sender is CollectionView cv) cv.SelectedItem = null;

        await Navigation.PushAsync(new PersonDetailPage(selected));
    }
}
