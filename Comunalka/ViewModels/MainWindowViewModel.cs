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
        Task.Run(() => { LoadData(); });
    }

    private List<CommunalResource> AllResourses;
    private List<Tariff> AllTariffs;

    private async void LoadData()
    {
        AllResourses = await context.Resources.ToListAsync();
        OnPropertyChanged(nameof(Resources));

        AllTariffs = await context.Tariffs.ToListAsync();
        OnPropertyChanged(nameof(Tariffs));
    }


    private CommunalResourseViewModel? _selectedResource;
    public CommunalResourseViewModel? SelectedResource
    {
        get => _selectedResource;
        set
        {
            _selectedResource = value;
            OnPropertyChanged(nameof(SelectedResource));
        }
    }

    private TariffViewModel? _selectedTariff;
    public TariffViewModel? SelectedTariff
    {
        get => _selectedTariff;
        set
        {
            _selectedTariff = value;
            OnPropertyChanged(nameof(SelectedTariff));
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

    public ObservableCollection<TariffViewModel> Tariffs
    {
        get
        {
            return new ObservableCollection<TariffViewModel>(
                AllTariffs.Select(r => new TariffViewModel(r))
                );

        }
    }

    public ICommand AddResource => new RelayCommand(x =>
    {
        var resource = new CommunalResource() { Title = "Новий ресурс" };
        AllResourses.Add(resource);
        context.Resources.Add(resource);
        OnPropertyChanged(nameof(Resources));
    }, x => true);

    public ICommand Save => new RelayCommand(x =>
    {
        context.SaveChangesAsync().ContinueWith(t =>
        {
            MessageBox.Show("Saved");
            LoadData();
            OnPropertyChanged(nameof(Resources));
        });

    }, x => true);

}
