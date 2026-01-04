namespace Lighthouse.Core.Scene
{
    public sealed class StartTransitionPhase : ISceneTransitionPhase
    {
        ISceneTransitionStep[] ISceneTransitionPhase.Steps { get; } =
        {
            new SaveCurrentSceneStateStep(),
            new LoadNextSceneStateStep(),
        };

        bool ISceneTransitionPhase.CanTransitionIntercept => true;
    }
}