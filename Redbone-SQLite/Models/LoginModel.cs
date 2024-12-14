using System.ComponentModel.DataAnnotations;

namespace Redbone.Models;

/// <summary>
/// Login 뷰에서 유효성 검사를 위해 사용되는 모델입니다.
/// </summary>
public class LoginModel
{
    [StringLength(100)]
    [Required(AllowEmptyStrings = false)]
    public string Name { get; init; }

    [StringLength(100)]
    [Required(AllowEmptyStrings = false)]
    public string Password { get; init; }
}