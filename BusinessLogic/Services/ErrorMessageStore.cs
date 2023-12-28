using BusinessLogic.IModels;
using BusinessLogic.Models;
using BusinessLogic.Projections;
using System.Security.Principal;

namespace UserInterface.Stores;

public class ErrorMessageStore
{
    public const string IllegalCharacters = "@#$%^&*()[]{};:'`|<>";
    public const string ErrorTestName = "Het invoerveld is leeg.\n Geef de test een naam a.u.b.";
    public const string ErrorAudience = "De doelgroep is leeg. \n Kies een doelgroep a.u.b.";
    public const string ErrorTestQuestion = "De vraag is leeg.\n Vul alsjeblieft een vraag in.";
    public const string ErrorOptions = "De doelgroep is leeg. \n Kies een doelgroep a.u.b.";
    public const string ErrorIllegalCharacters = $"Er zijn karakters aanwezig die niet toegestaan zijn. \n Deze karakters zijn: {IllegalCharacters}";
    public const string ErrorFrequencyLimit = "De opgegeven frequentie valt buiten de grenzen die toegestaan is. \n De waarde moet tussen 125 HZ en 8000 HZ vallen.";
    public const string ErrorStartingDecibels = "De opgegeven start volume valt buiten de grenzen die toegestaan is. \n De waarde moet tussen 0 en 120 decibel vallen.";
    public const string ErrorNotValidInteger = "Er mogen alleen hele getallen ingevoerd worden.";
    public const string ErrorEmpty = "Alle velden moeten ingevuld worden.";
    public const string ErrorQuestionAnwserType = "Een van de twee antwoord types moet actief zijn.";
    public const string ErrorMultipleChoiceOptions = "Er moeten minstens 2 opties beschikbaar zijn voor een meerkeuze vraag.";
    public const string ErrorAdministratorChanged = "Het is niet toegestaan uw eigen rol aan te passen.";
    public const string ErrorFirstName = "De ingevoerde voornaam bevat niet-toegestane tekens, is te lang of is leeg.";
    public const string ErrorLastName = "De ingevoerde achternaam bevat niet-toegestane tekens, is te lang of is leeg.";
    public const string ErrorInfix = "De ingevoerde voornaam bevat niet-toegestane tekens of is te lang.";
    public const string ErrorEmployeeNumber = "Alleen numerieke waarden zijn toegestaan voor het medewerkersnummer.";
    public const string ErrorEmployeeStatus = "Het is niet mogelijk om je eigen account op inactief te zetten.";
    public const string ErrorEmployeeStatusGeneric = "Er is wat misgegaan bij het activieren/deactiveren van het account.";
    public const string ErrorEmailInUse = "Het opgegeven email adres is al in gebruik.";
    public const string ErrorDeleteEmployee = "Het verwijderen van deze medewerker is niet toegestaan omdat ze de eigenaar zijn van één of meer testen.";
    public const string ErrorDeleteEmployeeSelf = "Het is niet toegestaan ​​om je eigen account te verwijderen.";

}
