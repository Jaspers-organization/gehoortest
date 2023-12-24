using BusinessLogic.DataMappings;
using BusinessLogic.Enums;
using BusinessLogic.Guards;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.IRepositories;
using BusinessLogic.Models;

namespace BusinessLogic.Services;

public class EmailService
{
    #region dependencies
    private ITestResultRepository repository;
    private IEmailProvider provider;
    #endregion 

    public EmailService(ITestResultRepository repository, IEmailProvider provider)
    {
        this.repository = repository;
        this.provider = provider;
    }

    public bool SendEmail(string reciever, Guid testResultId)
    {
        Guard.AssertValidEmail(reciever);

        TestResult testResult = repository.GetById(testResultId);

        string date = DateTime.Now.ToString("dd-MM-yyyy");
        string subject = $"Testresultaat {date}"; 
        string body = CreateEmailBody(testResult, date);

       return provider.SendEmail(reciever, subject, body);
    }
  
    private string CreateEmailBody(TestResult testResult, string date)
    {
        string result = testResult.HasHearingLoss ? "Mogelijke gehoorschade" : "Gezond gehoor";

        List<ToneAudiometryQuestionResult> rightEarAnswers = testResult.ToneAudiometryQuestions.Where(answer => answer.Ear == Ear.Right).ToList();
        string rightEarTable = CreateResultTable(rightEarAnswers);

        List<ToneAudiometryQuestionResult> leftEarAnswers = testResult.ToneAudiometryQuestions.Where(answer => answer.Ear == Ear.Left).ToList();
        string leftEarTable = CreateResultTable(leftEarAnswers);

        return $@"
            <html>
                <body>
                    <h1 style='text-align:center'>Resultaten gehoortest</h1>
                    <h2 style='text-align:center;color:#DA0063'>{result}</h2>
                    <br>
                    <span>U heeft een gehoortest bij ons gedaan op:</span>
                    <span style='display:block'>{date}</span>
                    <br>
                    <span style='display:block'>De resultaten duiden op <span style='color:#DA0063'>{result.ToLower()}</span>.</span>
                    <br>
                    <span style='display:block'>U gaf aan de getestte frequenties op onderstaand decibel niveau te horen:</span>
                    <br>
                    <h3 style='margin-bottom: 0;'>Rechter Oor</h3>
                    {rightEarTable}
                    <h3 style='margin-bottom: 0;'>Linker Oor</h3>
                    {leftEarTable}
                    <br>
                    <p>De audicien kan u advies geven op basis van de testresultaten. maak een afspraak met de audicien.</p>
                </body>
            </html>         
        ";
    }

    private string CreateResultTable(List<ToneAudiometryQuestionResult> answers)
    {
        string tableString = $@"
            <p style='margin: 0;'>HL (dB)</p> 
            <table style='border-top: 1px solid; border-right: 1px solid; border-spacing: 0; border-collapse: collapse;'>
        ";
        
        // Create table rows for answers (decibel range 0 - 120)
        for (int row = 1; row <= 27; row++)
        {
            int currentDecibels = (row - 2) * 5;

            tableString += "<tr>";
            for (int column = 1; column <= 13; column++)
            {
                switch (column)
                {
                    // Add decibels or empty column
                    case 1:
                        tableString += (row % 2 == 0)
                            ? ColumnBuilder(content: currentDecibels.ToString())
                            : ColumnBuilder();
                        continue;

                    // Add empty column
                    case 2:
                        tableString += ColumnBuilder(border: true);
                        continue;

                    // Add column in frequency range (column 3 to 13)
                    default:
                        tableString += CreateFrequencyRangeColumn(column, currentDecibels, answers);
                        continue;
                }
            }
            tableString += "</tr>";
        }

        // Create last table row with all frequencies
        tableString += CreateFrequencyRow();
        
        tableString += @"
            </table>
            <p style='margin: 0;'>Frequentie (Hz)</p> 
        ";

        return tableString;
    }

    private string CreateFrequencyRangeColumn(int column, int currentDecibels, List<ToneAudiometryQuestionResult> answers)
    {
        FrequencyMap frequencyMap = FrequencyMapping.Frequencies[column - 3];

        ToneAudiometryQuestionResult? answer = answers.FirstOrDefault(r => r.Frequency == frequencyMap.Frequency);
        if (answer == null) return ColumnBuilder(border: true);

        string content = "";
        if (answer.LowestDecibels == currentDecibels) content = "X";

        // Determine whether the answer is in hearing loss range
        int min = frequencyMap.HearingLoss.Min;
        int max = frequencyMap.HearingLoss.Max;

        if (min == 0 && max == 0) return ColumnBuilder(content: content, border: true);

        return (min <= answer.LowestDecibels && max >= answer.LowestDecibels)
            ? ColumnBuilder(content: content, border: true, background: true)
            : ColumnBuilder(content: content, border: true);
    }

    private string CreateFrequencyRow()
    {
        string rowString = "<tr>";
        for (int column = 1; column <= 13; column++)
        {
            switch (column)
            {
                // Add empty column
                case 1: 
                case 2:
                    rowString += ColumnBuilder();
                    continue;

                // Add column with frequency
                default:
                    FrequencyMap frequencyMap = FrequencyMapping.Frequencies[column - 3];
                    rowString += $"<td style='text-align: center; width: 25px'>{frequencyMap.FrequencyString}</td>";
                    continue;
            }
        }
        rowString += "</tr>";

        return rowString;
    }

    private string ColumnBuilder(string? content = null, bool border = false, bool background = false)
    {
        if (content != null && border && background) return $"<td style='font-weight: bold; border: 1px solid; background-color: red; text-align: center;'>{content}</td>";

        if (content != null && border) return $"<td style='font-weight: bold; border: 1px solid; text-align: center;'>{content}</td>";

        if (content != null) return $"<td style='text-align: end;'>{content}</td>";

        if (border) return $"<td style='border: 1px solid; height: 10px; width: 10px;'></td>";
      
        // Empty filler column
        return $"<td style='height: 10px; width: 10px;'></td>";
    }
}
