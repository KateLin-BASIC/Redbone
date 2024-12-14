using Octokit;

namespace Redbone.Models;

/// <summary>
/// Home 뷰에 데이터를 전달하기 위해 사용되는 모델입니다.
/// </summary>
public class HomeData
{
    public IEnumerable<Post> Posts { get; init; }
    public IEnumerable<Repository> Repositories { get; init; }
}