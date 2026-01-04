namespace Lighthouse.Core.Scene
{
    public sealed class UnloadScenePhase : ISceneTransitionPhase
    {
        ISceneTransitionStep[] ISceneTransitionPhase.Steps { get; } =
        {
            new UnloadMainSceneStep(), new UnloadCommonSceneStep()
        };

        bool ISceneTransitionPhase.CanTransitionIntercept => false;
    }
}