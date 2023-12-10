using BusinessLogic.Enums;
using BusinessLogic.IModels;

namespace BusinessLogic.Models
{
    public class ToneAudiometryQuestion : IModel, IQuestion
    {
        public Guid Id { get; set; }
        public int Frequency { get; set; }
        public int StartingDecibels { get; set; }
        public int QuestionNumber { get; set; }
        public QuestionType QuestionType { get; set; }

        public Guid TestId { get; set; }
        public Test? Test { get; set; }
    }
}
