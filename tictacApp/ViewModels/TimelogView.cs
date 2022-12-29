using tictacApp.Data;
using tictacApp.Interfaces;
using tictacApp.Helpers;

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace tictacApp.ViewModels;

public class TimeLogView : TimelogObservation, INotifyPropertyChanged
{
    private int? _projectId;
    private int? _objectiveId;

    public DateTime? StartDate { get; set; }
    public int TimeSpentInMin { get; set; }

    //TODO: when stable, test ObservableProperty attribute instead of that mechanism
    public int? ProjectId
    {
        get { return _projectId; }
        set
        {
            _projectId = value;
            // Call OnPropertyChanged whenever the property is updated
            OnPropertyChanged("ProjectId");
        }
    }
    
    public int? ObjectiveId
    {
        get { return _objectiveId; }
        set
        {
            _objectiveId = value;
            // Call OnPropertyChanged whenever the property is updated
            OnPropertyChanged("ObjectiveId");
        }
    }

    //Calculated fields, for display only
    public string TimeSpentInHHMM { get; set; } = string.Empty;
    public TimeSpan? TimeSpan { get; set; } 
    public string ProjectAsText { get; private set; } = string.Empty;
    public string ObjectiveAsText { get; private set; } = string.Empty;

    public event PropertyChangedEventHandler PropertyChanged;

    private IGenericCRUDServiceWithParents _crudService;


    public TimeLogView(IGenericCRUDServiceWithParents crudService): base(crudService)
    {
         _crudService = crudService;

        this.PropertyChanged += DependencyIdPropertyChanged;
    }

    private async void DependencyIdPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e != null)
        {
            switch (e.PropertyName)
            {
                case "ProjectId":
                    await SetProjectAsText();
                    break;

                case "ObjectiveId":
                    await SetObjectiveAsText();
                    break;

                default:
                    throw new NotImplementedException("Case for the construction of the object path not managed");
            }
        }
    }

    private async Task SetProjectAsText()
    {
        if (ProjectId.HasValue)
        {
            this.ProjectAsText = await BreadcrumbHelper.BuildSimpifiedBreadcrumb<Project>(_crudService, ProjectId.Value);
        }
        else
        {
            this.ProjectAsText = string.Empty;
        }
    }

    private async Task SetObjectiveAsText()
    {
        if (ObjectiveId.HasValue)
        {
            this.ObjectiveAsText = await BreadcrumbHelper.BuildSimpifiedBreadcrumb<Objective>(_crudService, ObjectiveId.Value);
        }
        else
        {
            this.ObjectiveAsText = string.Empty;
        }
    }

    // Create the OnPropertyChanged method to raise the event
    // The calling member's name will be used as the parameter.
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
