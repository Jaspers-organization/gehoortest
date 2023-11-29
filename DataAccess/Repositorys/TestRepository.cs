using BusinessLogic.IModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositorys
{
    public class TestRepository : ITestRepository
    {
        public  ITest? Get(int id)
        {
            List<string> hearTypesList = new();
            hearTypesList.Add("Uitstekend");
            hearTypesList.Add("Goed");
            hearTypesList.Add("Matig");
            hearTypesList.Add("Slecht");

            List<string> agesList = new();
            agesList.Add("-18");
            agesList.Add("19-29");
            agesList.Add("30-49");
            agesList.Add("50-69");
            agesList.Add("70-79");
            agesList.Add("80-89");

            List<string> workFieldList = new();
            workFieldList.Add("Kantoor baan");
            workFieldList.Add("Op de bouw");

            List<string> optionsList = new();
            ITest test = new Test()
            {
                Id = 1,
                Title = "Test voor Tsja groep",
                Active = true,
                TextQuestions = new List<ITextQuestion>
                {
                    new TextQuestion(1,1,1, "In welke leeftijdsgroep bevindt u zich?",hearTypesList, true, false),
                    new TextQuestion(2,1,2, "Wat is uw naam?",optionsList, true, false),
                    new TextQuestion(3,1,3, "In welk werkgebied werkt u?",workFieldList, true, false),
                    new TextQuestion(4,1,4, "Hoe omschrijft u uw gehoor?",hearTypesList, true, false),
                    new TextQuestion(5,1,5, "Hoe omschrijft u uw gehoor?",hearTypesList, true, false),
                    new TextQuestion(6,1,6, "Hoe omschrijft u uw gehoor?",hearTypesList, true, false),
                    new TextQuestion(7,1,7, "Hoe omschrijft u uw gehoor?",hearTypesList, true, false),
                },
                ToneAudiometryQuestions = new List<IToneAudiometryQuestion>
                {
                    new ToneAudiometryQuestion(1, 1, 4, 500, 40),
                    new ToneAudiometryQuestion(2, 1, 5, 500, 40),
                    new ToneAudiometryQuestion(3, 1, 6, 500, 40),
                    new ToneAudiometryQuestion(4, 1, 7, 500, 40),
                }
            };
            return test;
        }
    }
}
