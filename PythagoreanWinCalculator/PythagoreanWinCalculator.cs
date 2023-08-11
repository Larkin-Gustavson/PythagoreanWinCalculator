namespace PythagoreanWinCalculator;

/// <summary>
///     A class to calculate the expected win total of a team.
/// </summary>
///  <remarks>
///     <para>
///         This calculator works for the following sports <strong>Baseball</strong>, <strong>Hockey</strong>, <strong>Basketball</strong>, 
///         and <strong>Football</strong>
///         based on how many runs (goals in Hockey or points in Basketball) 
///         the team generates and how many runs (goals in Hockey or points in Basketball) the team generates against them. 
///     </para>
///     <para>
///         This calculator works by using the Formula used by baseball statistician Bill James 
///         to determine the expected win total of a team.
///         
///         The Formula derived by Bill James and used in this class to predict the expected win total of a team is:
///         <br/>
///         Expected Win% =         Runs scoredᵉˣᵖ
///                                     <br/>
///                 ----------------------------------------------------
///                                     <br/>
///                        ((Runs scored)ᵉˣᵖ + (Runs allowed)ᵉˣᵖ))
///                 
///         <br/>
///         <br/>
///         The <strong>Expected Win%</strong> part of the formula represents the percentage of games 
///         the formula calculates that the team should have won 
///         (in this particular the formula, we will not return the decimal number of this calculation as percent, meaning it will not be return multiplied by a hundred.)
///         This is because this decimal form of the calculation is used to actually translate this number into the number of predicted wins for the team
///         as specified by the <see cref="GetExpectedNumberOfWins()"/> method.
///         
///         <br/>
///         <br/>
///         The <strong>Runs scored</strong> part of this formula represents the number of runs (goals in hockey or points in basketball) that a team generates.
///         
///         <br/>
///         <br/>
///         The <strong>Runs allowed</strong> part of this formula represent the number of runs (goals in hockey or points in basketball) that are generated against them. 
///         
///         <br/>
///         <br/>
///         The <strong>exp</strong> part of this formula is the exponent that the runs scored (goals in hockey or points in basketball) 
///         and the runs allowed (goals in hockey or points in basketball) runs allowed parts of the formula get raised to. 
///         This exponent is determined by the amount of "chance" or "luck" is determined to be present in the sport. 
///         The higher the exponent (meaning larger exponent) means that the sport has less "luck" involved, 
///         while conversely the lower this exponent the more the sport has more "luck" involved. 
///         
///         <br/>
///         <br/>
///         <strong>Note</strong> that this program uses the following exponents to be used in the calculations. 
///         For Baseball the exponent used is around 1.83, this number is calculated via a separate formula.
///         For Hockey the exponent used is around 2.37, this number is calculated via a separate formula.
///         For Basketball the exponent used is 13.91.
///         For Football the exponent used is 2.37.
///                                 
///         <br/>
///         <br/>
///         <strong>Note</strong> that for sports that can end in a tie or have losses that get put into a separate column in the standings (Hockey or Basketball in regular season), 
///         this formula as currently constructed DOES NOT account for games that end in ties,  as such it can only predict wins and not necessarily indicate 
///         how many games are lost in regulation time or are lost during play in overtime (OT).
///         
///         <strong>Note</strong> all of the resources used in this application (formulas, definition for the exponent part of the formula, and the exponent values for the sports)
///         were found on the following websites: 
///         https://goodcalculators.com/pythagorean-expectation-calculator/ 
///         and 
///         https://towardsdatascience.com/pythagorean-expectation-in-sports-analytics-with-examples-from-different-sports-f5e599530a6c.
///     </para>
///  </remarks>
public sealed class PythagoreanWinCalculator
{
    /// <summary>
    ///     A private constant to represent the exponent that will be used in the formula for Basketball.
    /// </summary>
    private const double BasketballPythagoreanExponentConstant = 13.91;

    /// <summary>
    ///     A private constant to represent the football Pythagorean exponent constant.
    /// </summary>
    private const double FootballPythagoreanExponentConstant = 2.37;

    /// <summary>
    ///     A private constant to represent the number of Baseball games in a single season (regular season only).
    /// </summary>
    private const int NumberOfGamesInBaseball = 162;

    /// <summary>
    ///     A public constant to represent the number of Hockey games in a single season (regular season only).
    /// </summary>
    private const int NumberOfGamesInHockey = 82;

    /// <summary>
    ///     A private constant to represent the number of Basketball games in a single season (regular season only).
    /// </summary>
    private const int NumberOfGamesInBasketball = 82;

    /// <summary>
    ///     A private constant to represent the number of games in football in a single season (regular season only).
    /// </summary>
    private const int NumberOfGamesInFootball = 17;

    /*
     * Formula is: 
     * Runs scored ^ ExponentConstant / (Runs scored) ^ ExponentConstant + (Runs allowed) ^ ExponentConstant 
     * 
     * For example when given the following
     * 
     * ExponentConstant could be one of three values, for the sport. 
     * For Baseball it is around 1.83, to be calculated via a separate formula.
     * For Hockey it is around 2.37, to be calculated via a separate formula.
     * For Basketball it is 13.91.
     * For Football it is 2.37.
     */

    /// <summary>
    ///     A public read only field to represent the exponent part of the Pythagorean Win formula. This exponent can change every year and changes based on the sport.
    ///     The larger this number, the less amount of "luck" is determined to be a factor in a particular sport. Conversely, the smaller this number the more likely "luck" is in play for a sport.
    ///     
    ///     <br/>
    ///     <br/>
    ///     For Baseball this exponent is around 1.83, but calculated via a separate formula.
    ///     
    ///     <br/>
    ///     <br/>
    ///     For Hockey this exponent is around 2.37, but calculated via a separate formula.
    ///     
    ///     <br/>
    ///     <br/>
    ///     For Basketball this exponent is 13.91.
    ///     
    ///     <br/>
    ///     <br/>
    ///     For Football this exponent is 2.37.
    /// </summary>
    private readonly double PythagoreanExponentConstant;

    /// <summary>
    ///     A public property that is read only, a private setter to represents the points scored meaning (runs scored in Baseball or goals scored in Hockey) by a team.
    /// </summary>
    public int PointsScored { get; private set; }

    /// <summary>
    ///     A public property that is read only, a private setter to represents the points scored meaning (runs scored in Baseball or goals scored in Hockey) against a team.
    /// </summary>
    public int PointsAllowed { get; private set; }

    /// <summary>
    ///     A public property that is read only, a private setter to represents the total number of games a sport plays during a full regular season.
    /// </summary>
    public int TotalNumberOfGamesPlayed { get; private set; }

    /// <summary>
    ///     A public property that is read only, a private setter to represents the actual number of wins by a team, not expected (or predicted).
    /// </summary>
    public int ActualNumberOfWins { get; private set; }

    /// <summary>
    ///     A parameterized constructor, this constructor will give the formula all important details necessary to give predictive numbers of a team.
    /// </summary>
    /// <param name="pointsScored">The total number of points scored by a team during a complete regular season.</param>
    /// <param name="pointsAllowed">The total number of points scored against a team during a complete regular season.</param>
    /// <param name="sport">The type of sport that the team played for, this is important as it will tell the class how many games were played in the sport, see <see cref="Sport"/>.</param>
    /// <param name="actualNumberOfWins">This is a optional parameter (if not specified the constructor assumes this value to be 0) to refer to the number of wins actually accrued by a team.</param>
    /// <exception cref="InvalidOperationException">If the user passes in a sport that is not a defined enumeration constant with in the Sport enum (<see cref="Sport"/>).</exception>
    public PythagoreanWinCalculator(int pointsScored, int pointsAllowed, Sport sport, int actualNumberOfWins = 0)
    {
        PointsScored = pointsScored;

        PointsAllowed = pointsAllowed;

        TotalNumberOfGamesPlayed = GetTotalNumberOfGamesPlayedBySport(sport);

        PythagoreanExponentConstant = GetPythagoreanExponentConstantBySport(sport);

        ActualNumberOfWins = actualNumberOfWins;
    }

    /// <summary>
    ///     A public method that intends to retrieve the expected win percentage of a team. It will not return the winning percentage as a actual percentage, rather it will return it as a decimal.
    /// </summary>
    /// <returns>The winning percentage as a actual percentage, rather it will return it as a decimal.</returns>
    public double GetExpectedWinPercentage()
    {
        double expectedWinPercentage = Math.Pow(PointsScored, PythagoreanExponentConstant) / (Math.Pow(PointsScored, PythagoreanExponentConstant) + Math.Pow(PointsAllowed, PythagoreanExponentConstant));

        return expectedWinPercentage;
    }

    /// <summary>
    ///     This method will return the actual winning percentage, much like <see cref="GetExpectedWinPercentage()"/>  the winning percentage will not be returned as a actual percentage, rather it will return it as a decimal.
    /// </summary>
    /// <param name="actualNumberOfWins">The actual number of wins that a team accumulates during a season.</param>
    /// <returns>The winning percentage as a actual percentage, rather it will return it as a decimal</returns>
    public double GetActualWinPercentage(int actualNumberOfWins)
    {
        return Convert.ToDouble(actualNumberOfWins) / TotalNumberOfGamesPlayed;
    }

    /// <summary>
    ///     This method will get the number of predicted (or expected) wins that a team is projected to get.
    ///     
    ///     <para>
    ///         <strong>Note</strong> this method rounds the result of this calculation rounded to the nearest whole number.
    ///     </para>
    /// </summary>
    /// <returns>The predicted number of wins rounded down to the nearest whole.</returns>
    public double GetExpectedNumberOfWins()
    {
        double winPercentageAsDecimal = GetExpectedWinPercentage();

        double numberOfGames = Convert.ToDouble(TotalNumberOfGamesPlayed);

        double numberOfWins = winPercentageAsDecimal * numberOfGames;

        return Math.Round(numberOfWins, 0, MidpointRounding.AwayFromZero);
    }

    /// <summary>
    ///     An overridden ToString method from the <see cref="object.ToString()"/> class that will return the object as a string, which in this case includes information about the team.
    /// </summary>    
    /// <remarks>
    ///         <para>
    ///              <strong>Note</strong> that for certain values (Expected Number of Wins, Expected Winning%, Actual Winning%, and Delta) they do appear rounded in this method.
    ///              As for ease to read, these values are rounded.
    ///        
    ///             <br/>
    ///             <br/>
    ///             For the Expected Number of Wins part, it is rounded to the nearest whole number (rounded down, as you can't have half of a win for example).
    ///     
    ///             <br/>
    ///             <br/>
    ///             For the Expected Winning percentage, this number is rounded to the nearest ten-thousandths place (4 decimal places) the percentage form of this is rounded 
    ///             to the nearest hundredths place.
    ///     
    ///             <br/>
    ///             <br/>
    ///             For the Actual Winning% it is rounded to the nearest ten-thousandths place and it's percent form is rounded to the nearest hundredths place.
    ///     
    ///             <br/>
    ///             <br/>
    ///             For the Delta it is rounded to the nearest tenths place.
    ///     
    ///             <br/>
    ///             <br/>
    ///             <strong>Note</strong> if the optional parameter of "actualNumberOfWins" is not modified 
    ///             (remains the default value of 0, or if someone accidentally passes in a negative value) 
    ///             the Delta information that could get put into the string form of the object, will not represented. 
    ///             If the user did add a value to the parameter in the constructor, then the Delta will be represented in the string form of the object.
    ///             
    ///             <br/>
    ///             <br/>
    ///             If the Delta figures show in the ToString method are <strong>negative (have a - sign)</strong> 
    ///             it means that the team underperformed expectations. 
    ///             Conversely, if this Delta is positive it means that the team over performed based on expectations.
    ///         </para>
    ///     </remarks>
    /// <returns>The object as a string, which in this case includes information about the team.</returns>
    public override string ToString()
    {
        if (ActualNumberOfWins <= 0)
        {
            return $"Expected Number of Wins = {GetExpectedNumberOfWins()} and " +
                   $"Expected Winning% = {Math.Round(GetExpectedWinPercentage(), 4)} ({Math.Round(GetExpectedWinPercentage() * 100, 2)}%)";
        }

        return $"Expected Number of Wins = {GetExpectedNumberOfWins()}, Actual Number of Wins = {ActualNumberOfWins}, Delta between Actual and Expected Number of Wins = {ActualNumberOfWins - GetExpectedNumberOfWins()} \n" +
               $"Expected Winning% = {Math.Round(GetExpectedWinPercentage(), 4)} ({Math.Round(GetExpectedWinPercentage() * 100, 2)}%), " +
               $"Actual Winning% = {Math.Round(GetActualWinPercentage(ActualNumberOfWins), 4)} ({Math.Round(GetActualWinPercentage(ActualNumberOfWins) * 100, 2)}%), " +
               $"Delta Between Actual and Expected Win% = {Math.Round(GetActualWinPercentage(ActualNumberOfWins) * 100 - GetExpectedWinPercentage() * 100, 2)}%";
    }

    /// <summary>
    ///     A private method to retrieve the correct Pythagorean Exponent Constant based on the sport passed in by the user.
    /// </summary>
    /// <param name="sport">The sport that the user needs to get the exponent associated with it</param>
    /// <returns>The exponent constant associated with the desired sport passed in.</returns>
    /// <exception cref="InvalidOperationException">If the user passes in a sport that is not a defined enumeration constant within the Sport enum.</exception>
    private double GetPythagoreanExponentConstantBySport(Sport sport)
    {
        return sport switch
        {
            Sport.Baseball => Math.Pow((Convert.ToDouble(PointsAllowed) + Convert.ToDouble(PointsScored)) / TotalNumberOfGamesPlayed, .287),
            Sport.Hockey => Math.Pow(Convert.ToDouble(PointsScored) / Convert.ToDouble(TotalNumberOfGamesPlayed), .458),
            Sport.Basketball => BasketballPythagoreanExponentConstant,
            Sport.Football => FootballPythagoreanExponentConstant,
            _ => throw new InvalidOperationException($"Error: {sport} is of a unknown enumeration constant.")
        };
    }

    /// <summary>
    ///     Gets the total number of games played by sport.
    /// </summary>
    /// <param name="sport">The sport for the desired team that will be used with the algorithm</param>
    /// <returns>The total number of games for any of the sports supported by the algorithm.</returns>
    private static int GetTotalNumberOfGamesPlayedBySport(Sport sport)
    {
        return sport switch
        {
            Sport.Baseball => NumberOfGamesInBaseball,
            Sport.Hockey => NumberOfGamesInHockey,
            Sport.Basketball => NumberOfGamesInBasketball,
            Sport.Football => NumberOfGamesInFootball,
            _ => throw new InvalidOperationException($"Error: {sport} is not a known enumeration constant."),
        };
    }

}