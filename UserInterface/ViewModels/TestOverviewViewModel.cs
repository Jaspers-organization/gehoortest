using BusinessLogic.Controllers;
using DataAccess.MockData;
using Service.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface.Stores;

namespace UserInterface.ViewModels;

internal class TestOverviewViewModel: ViewModelBase
{
    public List<ITest> Tests {  get; set; }
    private readonly NavigationStore? _navigationStore;
    private readonly TargetAudienceRepository _targetAudienceRepository;
    public TestOverviewViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
        _targetAudienceRepository = new TargetAudienceRepository();

        AudiencesList = _targetAudienceRepository.GetAllAudiences().Select(audience => audience.Label).ToList();
    }
    private List<string>? _audiencesList;
    public List<string>? AudiencesList
    {
        get { return _audiencesList; }
        set { _audiencesList = value; OnPropertyChanged(nameof(AudiencesList)); }
    }
    
    public void GetTests(ITargetAudience targetAudience)
    {

    }
}
