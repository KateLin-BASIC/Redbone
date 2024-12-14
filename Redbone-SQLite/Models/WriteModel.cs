using System.ComponentModel.DataAnnotations;

namespace Redbone.Models;

/// <summary>
/// Write 뷰에서 유효성 검사를 위해 사용되는 모델입니다.
/// </summary>
public class WriteModel
{
    [Required]
    [StringLength(byte.MaxValue)]
    public string Title { get; init; }
    
    [Required]
    public string Content { get; init; }
}