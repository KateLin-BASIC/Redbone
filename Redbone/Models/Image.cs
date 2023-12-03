namespace Redbone.Models;

public class Image
{
    /// <summary>
    /// ID (INT)
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// LOCATION (TEXT)
    /// </summary>
    public string Location { get; init; }
    
    /// <summary>
    /// DATE (TIMESTAMP)
    /// </summary>
    public DateTime Date { get; init; }
}