namespace PythagoreanWinCalculator;

public class Program
{
    public static void Main(string[] args)
    {
        PythagoreanWinCalculator mets = new PythagoreanWinCalculator(pointsScored: 772, pointsAllowed: 606, sport: Sport.Baseball, actualNumberOfWins: 101);
        PythagoreanWinCalculator padres = new PythagoreanWinCalculator(pointsScored: 705, pointsAllowed: 660, sport: Sport.Baseball, actualNumberOfWins: 89);

        Console.WriteLine($"Mets = {mets}");
        Console.WriteLine();
        Console.WriteLine($"Padres = {padres}");

        PythagoreanWinCalculator yankees = new PythagoreanWinCalculator(1000, 750, Sport.Baseball, 99);
        PythagoreanWinCalculator guardians = new PythagoreanWinCalculator(698, 634, Sport.Baseball, 92);

        Console.WriteLine();
        Console.WriteLine($"Yankees = {yankees}");
        Console.WriteLine();
        Console.WriteLine($"Guardians = {guardians}");

        Console.WriteLine();
        PythagoreanWinCalculator phillies = new PythagoreanWinCalculator(747, 685, Sport.Baseball, 87);
        Console.WriteLine($"Phillies = {phillies}");

        Console.WriteLine();
        PythagoreanWinCalculator islanders = new PythagoreanWinCalculator(229, 231, Sport.Hockey, 37);
        Console.WriteLine($"Islanders = {islanders}");

        Console.WriteLine();
        PythagoreanWinCalculator rangers = new PythagoreanWinCalculator(250, 204, Sport.Hockey, 52);
        Console.WriteLine($"Rangers = {rangers}");

        Console.WriteLine();
        PythagoreanWinCalculator jets = new PythagoreanWinCalculator(296, 316, Sport.Football, 7);
        Console.WriteLine($"Jets = {jets}");

        Console.WriteLine();
        PythagoreanWinCalculator packers = new PythagoreanWinCalculator(370, 371, Sport.Football, 8);
        Console.WriteLine($"Packers = {packers}");

        Console.WriteLine();
        PythagoreanWinCalculator giants = new PythagoreanWinCalculator(365, 371, Sport.Football, 9);
        Console.WriteLine($"Giants = {giants}");

        Console.WriteLine();
        PythagoreanWinCalculator vikings = new PythagoreanWinCalculator(424, 427, Sport.Football, 13);
        Console.WriteLine($"Vikings = {vikings}");

        Console.WriteLine();
        PythagoreanWinCalculator cowboys = new PythagoreanWinCalculator(467, 342, Sport.Football, 13);
        Console.WriteLine($"Cowboys = {cowboys}");

        Console.WriteLine();
        PythagoreanWinCalculator knicks = new PythagoreanWinCalculator(9514, 9274, Sport.Basketball, 47);
        Console.WriteLine($"Knicks = {knicks}");
    }
}