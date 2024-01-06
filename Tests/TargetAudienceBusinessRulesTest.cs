using BusinessLogic.BusinessRules;
using BusinessLogic.Models;

namespace Tests;

public class TargetAudienceBusinessRulesTest
{
    [Theory]
    [InlineData(20, 30, true)]
    [InlineData(20, 20, false)]
    [InlineData(25, 30, false)]
    [InlineData(30, 25, false)]
    [InlineData(0, 15, false)]
    [InlineData(15, 0, false)]
    public void It_validates_the_target_audience_range(int from, int to, bool isValid)
    {
        TargetAudience targetAudience = new TargetAudience() { Id = Guid.NewGuid(), From = from, To = to };

        List<TargetAudience> targetAudiences = new List<TargetAudience>()
        {
            targetAudience,
            new TargetAudience() { Id = Guid.NewGuid(), From = 20, To = 25 },
            new TargetAudience() { Id = Guid.NewGuid(), From = 25, To = 30 },
            new TargetAudience() { Id = Guid.NewGuid(), From = 25, To = 35 },
        };

        Assert.Equal(isValid, TargetAudienceBusinessRules.IsValidRange(targetAudience, targetAudiences));
    }

    [Theory]
    [InlineData(20, 30, false)]
    [InlineData(20, 20, true)]
    [InlineData(25, 30, true)]
    [InlineData(30, 25, true)]
    [InlineData(0, 15, true)]
    [InlineData(15, 0, true)]
    public void It_throws_an_exception_when_the_target_audience_range_is_invalid(int from, int to, bool throwsException)
    {
        TargetAudience targetAudience = new TargetAudience() { Id = Guid.NewGuid(), From = from, To = to };

        List<TargetAudience> targetAudiences = new List<TargetAudience>()
        {
            targetAudience,
            new TargetAudience() { Id = Guid.NewGuid(), From = 20, To = 25 },
            new TargetAudience() { Id = Guid.NewGuid(), From = 25, To = 30 },
            new TargetAudience() { Id = Guid.NewGuid(), From = 25, To = 35 },
        };

        if (!throwsException)
        {
            TargetAudienceBusinessRules.AssertValidRange(targetAudience, targetAudiences);
            return;
        }

        Assert.Throws<ArgumentException>(() => TargetAudienceBusinessRules.AssertValidRange(targetAudience, targetAudiences));
    }
}
