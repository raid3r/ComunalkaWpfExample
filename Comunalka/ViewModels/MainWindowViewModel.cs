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
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Markup;

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
    private List<Counter> AllCounters;

    private async void LoadData()
    {
        AllResourses = await context.Resources.ToListAsync();
        OnPropertyChanged(nameof(Resources));

        AllTariffs = await context.Tariffs.ToListAsync();
        OnPropertyChanged(nameof(Tariffs));

        AllCounters = await context.Counters.Include(x => x.Histories).ToListAsync();
        OnPropertyChanged(nameof(Counters));

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

    private CounterViewModel? _selectedCounter;
    public CounterViewModel? SelectedCounter
    {
        get => _selectedCounter;
        set
        {
            _selectedCounter = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(MonthStatistic));
            OnPropertyChanged(nameof(MonthStatisticLabels));
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

    public ObservableCollection<string> MonthStatisticLabels
    {
        get
        {
            var data = SelectedCounter?.Histories
                .GroupBy(x => new { Year = x.Date.Year, Month = x.Date.Month, Tariff = x.Counter.Tariff })
                .Select(g => new
                {
                    Title = string.Format("{0}.{1}", g.Key.Month, g.Key.Year),
                    Value = (decimal)(g.ToList().Max(i => i.Value) - g.ToList().Min(i => i.Value)) * g.Key.Tariff.Price
                });

            return new ObservableCollection<string>(data.Select(x => x.Title));
        }
    }

    public SeriesCollection MonthStatistic { get {

            var data = SelectedCounter?.Histories
                .GroupBy(x => new { Year = x.Date.Year, Month = x.Date.Month, Tariff = x.Counter.Tariff })
                .Select(g => new
                {
                    Title = g.Key.Month,
                    Value = (decimal)(g.ToList().Max(i => i.Value) - g.ToList().Min(i => i.Value)) * g.Key.Tariff.Price
                });

            var collection = new SeriesCollection() { 
            
            new LineSeries
            {
                Values = new ChartValues<decimal>((IEnumerable<decimal>)data?.Select(x => x.Value)),
            }
            };
            return collection;
        } }

    public ObservableCollection<CounterViewModel> Counters
    {
        get
        {
            return new ObservableCollection<CounterViewModel>(
                AllCounters.Select(c =>
                {
                    var m = new CounterViewModel(c)
                    {
                        
                    };
                    m.AddHistory = new RelayCommand(x =>
                    {
                        var history = new CounterHistory()
                        {
                            Date = DateTime.Now,
                            Counter = c
                        };
                        var viewModel = new CounterHistoryViewModel(history);

                        var window = new AddHistory(viewModel);
                        if ((bool)window.ShowDialog())
                        {
                            context.Histories.Add(history);
                            c.Histories.Add(history);
                            OnPropertyChanged(nameof(SelectedCounter.Histories));
                            m.OnPropertyChanged();
                            SelectedCounter = m;
                        };

                    }, x => SelectedCounter != null);
                    return m;
                    }
                ));
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

    public ICommand AddTariff => new RelayCommand(x =>
    {
        var tariff = new Tariff() { Price = 0, Resource = AllResourses.First() };
        AllTariffs.Add(tariff);
        context.Tariffs.Add(tariff);
        OnPropertyChanged(nameof(Tariffs));
    }, x => true);

    public ICommand AddCounter => new RelayCommand(x =>
    {
        var counter = new Counter() { Number = "", Tariff = AllTariffs.First() };
        AllCounters.Add(counter);
        context.Counters.Add(counter);
        OnPropertyChanged(nameof(Counters));
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
