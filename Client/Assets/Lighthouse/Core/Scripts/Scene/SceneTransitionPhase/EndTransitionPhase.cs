namespace Lighthouse.Core.Scene
{
    public sealed class EndTransitionPhase : ISceneTransitionPhase
    {
        ISceneTransitionStep[] ISceneTransitionPhase.Steps { get; } =
        {
            new CleanupStep(),
        };

        bool ISceneTransitionPhase.CanTransitionIntercept => false;
    }
}