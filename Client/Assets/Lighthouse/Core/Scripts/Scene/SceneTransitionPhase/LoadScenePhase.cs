namespace Lighthouse.Core.Scene
{
    public sealed class LoadScenePhase : ISceneTransitionPhase
    {
        ISceneTransitionStep[] ISceneTransitionPhase.Steps { get; } =
        {
            new LoadMainSceneStep(), new LoadCommonSceneStep(),
        };

        bool ISceneTransitionPhase.CanTransitionIntercept => false;
    }
}