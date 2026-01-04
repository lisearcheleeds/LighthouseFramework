namespace Lighthouse.Core.Scene
{
    public sealed class EnterScenePhase : ISceneTransitionPhase
    {
        ISceneTransitionStep[] ISceneTransitionPhase.Steps { get; } =
        {
            new EnterSceneStep(), new ResolveCameraStep(),
        };

        bool ISceneTransitionPhase.CanTransitionIntercept => false;
    }
}