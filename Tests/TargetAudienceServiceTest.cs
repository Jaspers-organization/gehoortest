using BusinessLogic.Models;
using BusinessLogic.Services;
using DataAccess.Repositories;
using System.Reflection;

namespace Tests;

public class TargetAudienceServiceTest
{
    [Fact]
    public void It_updates_the_target_audience_label()
    {
        TargetAudience targetAudience = new TargetAudience() { From = 25, To = 35, Label = "" };

        Type type = typeof(TargetAudienceService);
        var service = Activator.CreateInstance(type, [new TargetAudienceRepository(), new TestRepository()]);

        MethodInfo method = type.GetMethod("UpdateLabel", BindingFlags.NonPublic | BindingFlags.Instance)!;
        method.Invoke(service, [targetAudience]);

        Assert.Equal("25-35", targetAudience.Label);
    }
}
