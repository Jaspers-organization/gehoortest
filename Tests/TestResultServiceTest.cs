using BusinessLogic.Classes;
using BusinessLogic.DataMappings;
using BusinessLogic.Enums;
using BusinessLogic.Models;
using BusinessLogic.Services;
using DataAccess.Repositories;
using System.Reflection;

namespace Tests;

public class TestResultServiceTest
{
    [Theory]
    [InlineData(250)]
    [InlineData(500)]
    [InlineData(750)]
    [InlineData(1000)]
    [InlineData(1500)]
    [InlineData(2000)]
    [InlineData(3000)]
    [InlineData(4000)]
    [InlineData(6000)]
    public void It_calculates_hearing_loss_when_there_is_no_hearing_loss(int frequency)
    {
        Type type = typeof(TestResultService);
        var service = Activator.CreateInstance(type, [new TestResultRepository()]);
        MethodInfo method = type.GetMethod("CalculateHearingLoss", BindingFlags.NonPublic | BindingFlags.Instance)!;

        List<ToneAudiometryQuestionResult> answers = new() { new(Guid.NewGuid(), frequency, 30, 0, Ear.Left) };

        bool hasHearingLoss = (bool) method.Invoke(service, [answers])!;

        Assert.False(hasHearingLoss);
    }

    [Theory]
    [InlineData(250)]
    [InlineData(500)]
    [InlineData(750)]
    [InlineData(1000)]
    [InlineData(1500)]
    [InlineData(2000)]
    [InlineData(3000)]
    [InlineData(4000)]
    [InlineData(6000)]
    public void It_calculates_hearing_loss_when_there_is_minimal_hearing_loss(int frequency)
    {
        Type type = typeof(TestResultService);
        var service = Activator.CreateInstance(type, [new TestResultRepository()]);
        MethodInfo method = type.GetMethod("CalculateHearingLoss", BindingFlags.NonPublic | BindingFlags.Instance)!;

        int minHearingLoss = FrequencyMapping.Frequencies.Find(x => x.Frequency == frequency)!.HearingLoss.Min;
        List<ToneAudiometryQuestionResult> answers = new() { new(Guid.NewGuid(), frequency, 30, minHearingLoss, Ear.Left) };

        bool hasHearingLoss = (bool)method.Invoke(service, [answers])!;

        Assert.True(hasHearingLoss);
    }

    [Theory]
    [InlineData(250)]
    [InlineData(500)]
    [InlineData(750)]
    [InlineData(1000)]
    [InlineData(1500)]
    [InlineData(2000)]
    [InlineData(3000)]
    [InlineData(4000)]
    [InlineData(6000)]
    public void It_calculates_hearing_loss_when_there_is_maximal_hearing_loss(int frequency)
    {
        Type type = typeof(TestResultService);
        var service = Activator.CreateInstance(type, [new TestResultRepository()]);
        MethodInfo method = type.GetMethod("CalculateHearingLoss", BindingFlags.NonPublic | BindingFlags.Instance)!;

        int maxHearingLoss = FrequencyMapping.Frequencies.Find(x => x.Frequency == frequency)!.HearingLoss.Max;
        List<ToneAudiometryQuestionResult> answers = new() { new(Guid.NewGuid(), frequency, 30, maxHearingLoss, Ear.Left) };

        bool hasHearingLoss = (bool)method.Invoke(service, [answers])!;

        Assert.True(hasHearingLoss);
    }
}
