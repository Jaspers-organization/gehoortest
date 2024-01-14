using BusinessLogic.HelperClasses;
using BusinessLogic.Enums;
using BusinessLogic.Guards;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models;
using System.Text;

namespace BusinessLogic.Services;

public class EmailService
{
    #region dependencies
    private ITestResultRepository testResultRepository;
    private ISettingsRepository settingsRepository;
    private IEmailProvider emailProvider;
    #endregion 

    public EmailService(
        ITestResultRepository testResultRepository, 
        ISettingsRepository settingsRepository, 
        IEmailProvider emailProvider
    ) {
        this.testResultRepository = testResultRepository;
        this.settingsRepository = settingsRepository;
        this.emailProvider = emailProvider;
    }

    public void SendEmail(string reciever, Guid testResultId)
    {
        Guard.AssertValidEmail(reciever);

        TestResult testResult = testResultRepository.GetById(testResultId);

        string date = DateTime.Now.ToString("dd-MM-yyyy");
        string subject = $"Testresultaat {date}"; 
        string body = CreateEmailBody(testResult, date);

        emailProvider.SendEmail(reciever, subject, body);
    }
  
    private string CreateEmailBody(TestResult testResult, string date)
    {
        string result = testResult.HasHearingLoss ? "Mogelijke gehoorschade" : "Gezond gehoor";
        string color = settingsRepository.GetSettings().Color.Remove(1,2);
        
        List<ToneAudiometryQuestionResult> rightEarAnswers = testResult.ToneAudiometryQuestions.Where(answer => answer.Ear == Ear.Right).ToList();
        string rightEarTable = CreateResultTable(rightEarAnswers);

        List<ToneAudiometryQuestionResult> leftEarAnswers = testResult.ToneAudiometryQuestions.Where(answer => answer.Ear == Ear.Left).ToList();
        string leftEarTable = CreateResultTable(leftEarAnswers);

        return $@"
            <html>
                <body>
                    <h1 style='text-align:center;'>Resultaten gehoortest</h1>
                    <h2 style='text-align:center;color:{color};'>{result}</h2>
                    <br>
                    <span>U heeft een gehoortest bij ons gedaan op:</span>
                    <span style='display:block;'>{date}</span>
                    <br>
                    <span style='display:block;'>De resultaten duiden op <span style='color:{color};'>{result.ToLower()}</span>.</span>
                    <br>
                    <span style='display:block;'>U gaf aan de getestte frequenties op onderstaand decibel niveau te horen:</span>
                    <br>
                    <h3 style='margin-bottom:0;color:{color};'>Rechter Oor</h3>
                    {rightEarTable}
                    <br>
                    <h3 style='margin-bottom:0;color:{color};'>Linker Oor</h3>
                    {leftEarTable}
                    <br>
                    <p>De audicien kan u advies geven op basis van de testresultaten. maak een afspraak met de audicien.</p>
                </body>
            </html>         
        ";
    }

    private string CreateResultTable(List<ToneAudiometryQuestionResult> answers)
    {
        StringBuilder stringBuilder = new StringBuilder();

        // Open the table HTML
        stringBuilder.Append(@"
            <p style='margin:0;'>HL (dB)</p> 
            <table style='border-top:1px solid;border-right:1px solid;border-spacing:0;border-collapse:collapse;'>
        ");

        // Create the result rows for each decibel
        for (int row = 0; row <= 26; row++)
        {
            stringBuilder.Append("<tr>");

            int currentDecibels = (row - 1) * 5;
            if (currentDecibels % 2 == 0) 
            {
                stringBuilder.Append($"<td style='height:20px;text-align:right;'>{currentDecibels}</td>");
            } 
            else {
                stringBuilder.Append($"<td style='height:20px'></td>");
            }

            // Create columns with the result for each frequency
            CreateResultColumns(currentDecibels, answers, stringBuilder);

            stringBuilder.Append("</tr>");
        }

        // Create the last row with the frequencies
        CreateLastFrequencyRow(stringBuilder);

        // Close the table HTML
        stringBuilder.Append(@"
            </table>
            <p style='margin:0;'>Frequentie (Hz)</p> 
        ");

        return stringBuilder.ToString();
    }

    private void CreateResultColumns(int currentDecibels, List<ToneAudiometryQuestionResult> answers, StringBuilder stringBuilder)
    {
        for (int column = 0; column <= 8; column++)
        {
            FrequencyMap currentFrequency = FrequencyMapping.Frequencies[column];
            ToneAudiometryQuestionResult? answer = answers.FirstOrDefault(item => item.Frequency == currentFrequency.Frequency);

            int min = currentFrequency.HearingLoss.Min;
            int max = currentFrequency.HearingLoss.Max;

            string background = "";
            if (min <= currentDecibels && max >= currentDecibels)
            {
                background = "background-color:red;";
            }

            if (answer == null || currentDecibels != answer.LowestDecibels)
            {
                stringBuilder.Append($"<td style='border:1px solid;{background}'></td>");
                continue;
            }

            stringBuilder.Append($"<td style='border:1px solid;text-align:center;font-weight:bold;{background}'>x</td>");
        }
    }

    private void CreateLastFrequencyRow(StringBuilder stringBuilder)
    {
        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td></td>");
        foreach (FrequencyMap frequencyMap in FrequencyMapping.Frequencies)
        {
            stringBuilder.Append($"<td style='width:35px;text-align:center;'>{frequencyMap.FrequencyString}</td>");
        }
        stringBuilder.Append("<tr>");
    }
}
