using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.BaseViewModels;
using Comunalka.Models;
using System.Collections.ObjectModel;

namespace Comunalka.ViewModels;

public class CounterHistoryViewModel : NotifyPropertyChangedBase
{
    public CounterHistory Model { get; set; }

    public CounterHistoryViewModel(CounterHistory model)
    {
        Model = model;
        _counter = new CounterViewModel(model.Counter);
    }

    private CounterViewModel _counter;
    public CounterViewModel Counter
    {
        get => _counter;
        set
        {
            _counter = value;
            Model.Counter = value.Model;
            OnPropertyChanged();
        }
    }

    
    public int Id
    {
        get { return Model.Id; }
        set { Model.Id = value; OnPropertyChanged(nameof(Id)); }
    }

    public int Value
    {
        get { return Model.Value; }
        set { Model.Value = value; OnPropertyChanged(nameof(Value)); }
    }

    public DateTime Date
    {
        get { return Model.Date; }
        set { Model.Date = value; OnPropertyChanged(nameof(Date)); }
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is CounterHistoryViewModel)) return false;
        return (obj as CounterHistoryViewModel).Model.Id == Model.Id;
    }

    public override int GetHashCode()
    {
        return Model.Id.GetHashCode();
    }
}


