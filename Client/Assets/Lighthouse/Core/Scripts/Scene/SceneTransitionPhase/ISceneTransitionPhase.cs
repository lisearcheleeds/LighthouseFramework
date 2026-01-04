namespace Lighthouse.Core.Scene
{
    public interface ISceneTransitionPhase
    {
        ISceneTransitionStep[] Steps { get; }

        bool CanTransitionIntercept { get; }
    }
}