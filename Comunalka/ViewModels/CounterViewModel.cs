using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.BaseViewModels;
using Comunalka.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Comunalka.ViewModels;

public class CounterViewModel : NotifyPropertyChangedBase
{
    public Counter Model { get; set; }

    public CounterViewModel(Counter model)
    {
        Model = model;
        _tariff = new TariffViewModel(model.Tariff);
        //OnPropertyChanged(nameof(Histories));
    }

    public ObservableCollection<CounterHistoryViewModel> Histories
    {
        get
        {
            return new ObservableCollection<CounterHistoryViewModel>(
                Model.Histories.OrderBy(x => x.Date).Select(r => new CounterHistoryViewModel(r))
                );

        }
    }

    public ICommand AddHistory { get; set; }


    public int Id
    {
        get { return Model.Id; }
        set { Model.Id = value; OnPropertyChanged(nameof(Id)); }
    }

    public string Number
    {
        get { return Model.Number; }
        set { Model.Number = value; OnPropertyChanged(nameof(Number)); }
    }

    private TariffViewModel _tariff;
    public TariffViewModel Tariff
    {
        get => _tariff;
        set
        {
            _tariff = value;
            Model.Tariff = value.Model;
            OnPropertyChanged();
        }
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is CounterViewModel)) return false;
        return (obj as CounterViewModel).Model.Id == Model.Id;
    }

    public override int GetHashCode()
    {
        return Model.Id.GetHashCode();
    }

    public int Begin { get
        {
            return Model.Histories.Min(x => x.Value);
        }
    }

    public int End
    {
        get
        {
            return Model.Histories.Max(x => x.Value);
        }
    }

    public decimal TotalPrice
    {
        get {
            return (decimal)(End - Begin) * Model.Tariff.Price;
        }
    }
}


