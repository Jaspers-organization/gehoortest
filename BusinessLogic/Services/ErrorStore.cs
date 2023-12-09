namespace UserInterface.Stores;

public class ErrorStore
{
    public static string IllegalCharacters = "!@#$%^&*()[]{};:'`|<>";
    public static string ErrorTestName => "Het invoerveld is leeg.\n Geef de test een naam a.u.b.";
    public static string ErrorAudience => "De doelgroep is leeg. \n Kies een doelgroep a.u.b.";
    public static string ErrorTestQuestion => "De vraag is leeg.\n Vul alsjeblieft een vraag in.";
    public static string ErrorOptions => "De doelgroep is leeg. \n Kies een doelgroep a.u.b.";    
    public static string ErrorIllegalCharacters => $"Er zijn karakters aanwezig die niet toegestaan zijn. \n Deze karakters zijn: {IllegalCharacters}";
    public static string ErrorFrequencyLimit => "De opgegeven frequentie valt buiten de grenzen die toegestaan is. \n De waarde moet tussen 125 HZ en 8000 HZ vallen.";
    public static string ErrorStartingDecibels => "De opgegeven start volume valt buiten de grenzen die toegestaan is. \n De waarde moet tussen 0 en 120 decibel vallen.";
    public static string ErrorNotValidInteger => "Er mogen alleen hele getallen ingevoerd worden.";
    public static string ErrorEmpty => "Alle velden moeten ingevuld worden.";
    public static string ErrorQuestionAnwserType => "Een van de twee antwoord types moet actief zijn.";
    public static string ErrorMultipleChoiceOptions => "Er moeten minstens 2 opties beschikbaar zijn voor een meerkeuze vraag.";
}
