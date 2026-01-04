namespace Lighthouse.Core.Scene
{
    public sealed class LeaveScenePhase : ISceneTransitionPhase
    {
        ISceneTransitionStep[] ISceneTransitionPhase.Steps { get; } =
        {
            new LeaveSceneStep()
        };

        bool ISceneTransitionPhase.CanTransitionIntercept => false;
    }
}