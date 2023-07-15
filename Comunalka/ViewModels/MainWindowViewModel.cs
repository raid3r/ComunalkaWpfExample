using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.BaseViewModels;
using Comunalka.Models;
using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;

namespace Comunalka.ViewModels;

public class MainWindowViewModel : NotifyPropertyChangedBase
{
    private ComunalkaContext context;
    public MainWindowViewModel()
    {
        AllResourses = new List<CommunalResource>();
        context = new ComunalkaContext();
        Task.Run(() => { LoadResourses(); }); ;
    }

    private List<CommunalResource> AllResourses;

    private async void LoadResourses()
    {
        AllResourses = await context.Resources.ToListAsync();
        OnPropertyChanged(nameof(Resources));
    }

    private CommunalResourseViewModel _selectedResource;
    public CommunalResourseViewModel SelectedResource
    {
        get => _selectedResource;
        set
        {
            _selectedResource = value;
            OnPropertyChanged(nameof(SelectedResource));
        }
    }

    public ObservableCollection<CommunalResourseViewModel> Resources
    {
        get
        {
            return new ObservableCollection<CommunalResourseViewModel>(
                AllResourses.Select(r => new CommunalResourseViewModel(r))
                );

        }
    }

    public ICommand AddResource => new RelayCommand(x => {
        var resource = new CommunalResource() { Title = "Новий ресурс" };
        AllResourses.Add(resource);
        context.Resources.Add(resource);
        OnPropertyChanged(nameof(Resources)); 
    }, x => true);

    public ICommand Save => new RelayCommand(x => {
        context.SaveChangesAsync().ContinueWith(t =>
        {
            MessageBox.Show("Saved");
            LoadResourses();
            OnPropertyChanged(nameof(Resources));
        });
    
    }, x => true);

}
