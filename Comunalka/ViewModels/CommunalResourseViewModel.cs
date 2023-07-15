using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.BaseViewModels;
using Comunalka.Models;

namespace Comunalka.ViewModels;

public class CommunalResourseViewModel : NotifyPropertyChangedBase
{
    public CommunalResource Model { get; set; }

    public CommunalResourseViewModel(CommunalResource model)
    {
        Model = model;
    }

    public int Id
    {
        get { return Model.Id; }
        set { Model.Id = value; OnPropertyChanged(nameof(Id)); }
    }

    public string Title
    {
        get { return Model.Title; }
        set { Model.Title = value; OnPropertyChanged(nameof(Title)); }
    }
}


