namespace Gameplay.Levels.LevelCompletion
{
    public interface ILevelCompletionChecker
    {
        LevelCompletionType Type { get; }
        bool IsCompleted();
    }
}