using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Lighthouse.Core.Scene
{
    public class SceneTransitionAnimator : MonoBehaviour
    {
        public UniTask ResetAnimation(TransitionType transitionType)
        {
            return UniTask.CompletedTask;
        }

        public UniTask In(TransitionType transitionType)
        {
            return UniTask.CompletedTask;
        }

        public UniTask Out(TransitionType transitionType)
        {
            return UniTask.CompletedTask;
        }
    }
}