/// <summary>
///     Interface for managing pooled bingo balls.
/// </summary>
public interface IBall
{
    BallObjectPooler Pool { get; set; }
}