namespace Redbone.Models;

/// <summary>
/// Post 테이블의 SQL 모델입니다.
/// </summary>
public class Post
{
    /// <summary>
    /// id (INTEGER)
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// view (INTEGER)
    /// </summary>
    public int View { get; init; }
    
    /// <summary>
    /// date (INTEGER)
    /// </summary>
    public long Date { get; init; }
    
    /// <summary>
    /// title (TEXT)
    /// </summary>
    public string Title { get; init; }
    
    /// <summary>
    /// content (TEXT)
    /// </summary>
    public string Content { get; init; }
}