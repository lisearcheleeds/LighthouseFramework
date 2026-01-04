using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Lighthouse.Core.Scene
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(SceneCanvasInitializer))]
    public abstract class CommonCanvasSceneBase : CommonSceneBase, ICanvasSceneBase
    {
        CanvasGroup canvasGroup;
        SceneCanvasInitializer canvasInitializer;
        ISceneCamera[] placeholderCameras;

        public override ISceneCamera[] GetSceneCameraList()
        {
            return placeholderCameras;
        }

        public virtual void InitializeCanvas(Camera canvasCamera)
        {
            canvasInitializer = GetComponent<SceneCanvasInitializer>();
            canvasInitializer.Initialize(canvasCamera);

            canvasGroup = GetComponent<CanvasGroup>();
        }

        protected override async UniTask OnBeginInAnimation(TransitionType transitionType)
        {
            await base.OnBeginInAnimation(transitionType);
            canvasGroup.alpha = 0f;
        }

        protected override UniTask OnCompleteInAnimation(TransitionType transitionType)
        {
            canvasGroup.alpha = 1f;
            return UniTask.CompletedTask;
        }

        protected override UniTask OnBeginOutAnimation(TransitionType transitionType)
        {
            canvasGroup.alpha = 1f;
            return UniTask.CompletedTask;
        }

        protected override async UniTask OnCompleteOutAnimation(TransitionType transitionType)
        {
            canvasGroup.alpha = 0f;
            await base.OnCompleteOutAnimation(transitionType);
        }
    }
}