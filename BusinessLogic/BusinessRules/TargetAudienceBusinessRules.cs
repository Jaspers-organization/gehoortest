using BusinessLogic.Models;
using BusinessLogic.Stores;

namespace BusinessLogic.BusinessRules;

public class TargetAudienceBusinessRules
{
    public static bool IsValidRange(TargetAudience targetAudience, List<TargetAudience> existingTargetAudiences) 
    {
        Guid id = targetAudience.Id;
        int from = targetAudience.From;
        int to = targetAudience.To;

        if (from >= to || from <= 0 || to <= 0) return false;

        if (existingTargetAudiences.Count == 0) return true;
 
        return existingTargetAudiences.FirstOrDefault(item => item.Id != id && item.From == from && item.To == to) == null;
    }

    public static void AssertValidRange(TargetAudience targetAudience, List<TargetAudience> existingTargetAudiences)
    {
        if (!IsValidRange(targetAudience, existingTargetAudiences))
        {
            throw new ArgumentException(ErrorMessageStore.ErrorSameAges);
        }
    }
}
