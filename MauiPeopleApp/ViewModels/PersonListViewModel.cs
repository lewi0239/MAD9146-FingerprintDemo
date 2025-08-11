using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using MauiPeopleApp.Models;
using MauiPeopleApp.Services;
using MauiPeopleApp.Views;

namespace MauiPeopleApp.ViewModels;

public class PersonListViewModel : BaseViewModel
{
    private readonly PersonService _personService;

    public ObservableCollection<Person> People { get; } = new();

    public ICommand LoadPeopleCommand { get; }

    public PersonListViewModel()
    {
        _personService = new PersonService();
        LoadPeopleCommand = new Command(async () => await LoadPeople());
    }

    
    
    private async Task LoadPeople()
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            People.Clear();
            var people = await _personService.GetPeopleAsync();
            foreach (var person in people)
                People.Add(person);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
        finally
        {
            IsBusy = false;
        }
    }
}