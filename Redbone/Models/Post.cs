namespace Redbone.Models;

/// <summary>
/// Post 테이블의 SQL 모델입니다.
/// </summary>
public class Post
{
    /// <summary>
    /// ID (INT)
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// TITLE (VARCHAR [256])
    /// </summary>
    public string Title { get; init; }
    
    /// <summary>
    /// CONTENT (LONGTEXT)
    /// </summary>
    public string Content { get; init; }
    
    /// <summary>
    /// DATE (TIMESTAMP)
    /// </summary>
    public DateTime Date { get; init; }
    
    /// <summary>
    /// VIEWS (INT)
    /// </summary>
    public int Views { get; init; }
}