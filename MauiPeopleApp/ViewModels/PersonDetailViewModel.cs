using MauiPeopleApp.Models;

namespace MauiPeopleApp.ViewModels;

public class PersonDetailViewModel
{
    public Person Person { get; }

    public PersonDetailViewModel(Person person)
    {
        Person = person;
    }
}