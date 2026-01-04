using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Lighthouse.Core.Scene
{
    public sealed class SceneManager
    {
        readonly SceneGroupController sceneGroupController;

        public MainSceneKey CurrentMainSceneKey => sceneGroupController.CurrentMainSceneKey;
        public ISceneTransitionPhase CurrentTransitionPhase => sceneGroupController.CurrentTransitionPhase;
        public bool IsTransition => CurrentTransitionPhase != null;

        Stack<TransitionDataBase> transitionDataStack = new();

        [Inject]
        public SceneManager(SceneGroupController sceneGroupController)
        {
            this.sceneGroupController = sceneGroupController;
        }

        public void TransitionScene(TransitionDataBase nextTransitionData)
        {
            TransitionScene(nextTransitionData, null);
        }

        public void TransitionScene(TransitionDataBase nextTransitionData, MainSceneKey backMainSceneKey)
        {
            TransitionSceneAsync(nextTransitionData, TransitionType.Default, backMainSceneKey).Forget();
        }

        public void BackScene()
        {
            if (IsTransition)
            {
                return;
            }

            if (!transitionDataStack.Any())
            {
                return;
            }

            BackSceneAsync().Forget();
        }

        async UniTask<bool> BackSceneAsync()
        {
            if (transitionDataStack.Count < 2)
            {
                return false;
            }

            var currentSceneTransitionData = transitionDataStack.Pop();
            var backTargetSceneTransitionData = transitionDataStack.Pop();

            while (true)
            {
                if (!transitionDataStack.Any())
                {
                    break;
                }

                if (!backTargetSceneTransitionData.CanTransition() && currentSceneTransitionData.MainSceneKey != backTargetSceneTransitionData.MainSceneKey)
                {
                    break;
                }

                backTargetSceneTransitionData = transitionDataStack.Pop();
            }

            return await TransitionSceneAsync(backTargetSceneTransitionData, TransitionType.Back, null);
        }

        async UniTask<bool> TransitionSceneAsync(TransitionDataBase nextTransitionData, TransitionType transitionType, MainSceneKey backMainSceneKey)
        {
            var canTransition = nextTransitionData.CanTransition();
            if (!canTransition)
            {
                return false;
            }

            var transitionSuccess = await sceneGroupController.StartCrossTransitionSequence(nextTransitionData, transitionType, CancellationToken.None);
            if (!transitionSuccess)
            {
                return false;
            }

            transitionDataStack.Push(nextTransitionData);

            if (backMainSceneKey != null)
            {
                while (transitionDataStack.Count > 0 && transitionDataStack.Peek().MainSceneKey != backMainSceneKey)
                {
                    transitionDataStack.Pop();
                }
            }

            // await new WaitWhile(() => PopupManager.Instance.IsOpen(CurrentMainSceneKey) || LoadingManager.Instance.IsShowGuard);

            return true;
        }
    }
}