using gehoortest_application.Repository;
using BusinessLogic.Classes;

namespace DataAccess.Models.TestData_Management;

public class TestRepository : Repository
{
    public TestRepository(string connectionString) : base(connectionString)
    {
    }

    //example for getting data as anoynmous type (if you want to select certain fields.)
    public List<object> GetAllActiveTests(int id)
    {
        return Get<Test>(t => t.Id == id)
            .Select(v => new { v.Active, v.TestData })
            .ToList<object>();
    }

    public List<Test> GetAllActiveTests()
    {
        return Get<Test>(t => t.Active);
    }

    public static BusinessLogic.Classes.Test GetTest()
    {
        // Create dummy data
        BusinessLogic.Classes.Test test = new();

        // Create questions text
        List<string> agesList = new();
        agesList.Add("-18");
        agesList.Add("19-29");
        agesList.Add("30-49");
        agesList.Add("50-69");
        agesList.Add("70-79");
        agesList.Add("80-89");
        TextQuestion textQuestion1 = new(1, "In welke leeftijdsgroep bevindt u zich?", agesList, true, false);
        test.AddTextQuestion(textQuestion1);

        //List<string> optionsList = new();
        //TextQuestion textQuestion2 = new(2, "Wat is uw naam?", optionsList, false, true);
        //test.AddTextQuestion(textQuestion2);

        List<string> workFieldList = new();
        workFieldList.Add("Kantoor baan");
        workFieldList.Add("Op de bouw");
        TextQuestion textQuestion3 = new(3, "In welk werkgebied werkt u?", workFieldList, true, true);
        test.AddTextQuestion(textQuestion3);

        List<string> hearTypesList = new();
        hearTypesList.Add("Uitstekend");
        hearTypesList.Add("Goed");
        hearTypesList.Add("Matig");
        hearTypesList.Add("Slecht");
        TextQuestion textQuestion4 = new(4, "Hoe omschrijft u uw gehoor?", hearTypesList, true, false);
        test.AddTextQuestion(textQuestion4);


        // Create questionsaudimetry
        AudiometryQuestion audiometryQuestion1 = new(5, 500, 40);
        test.AddAudiometryQuestion(audiometryQuestion1);

        AudiometryQuestion audiometryQuestion2 = new(6, 1500, 30);
        test.AddAudiometryQuestion(audiometryQuestion2);

        AudiometryQuestion audiometryQuestion3 = new(7, 750, 30);
        test.AddAudiometryQuestion(audiometryQuestion3);

        return test;
    }

}
