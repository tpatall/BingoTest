/// <summary>
///     Interface for managing pooled bingo balls.
/// </summary>
public interface IBallPooled
{
    BallObjectPooler Pool { get; set; }
}