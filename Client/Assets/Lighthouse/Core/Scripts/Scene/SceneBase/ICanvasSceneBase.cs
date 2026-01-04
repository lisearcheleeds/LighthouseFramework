using UnityEngine;

namespace Lighthouse.Core.Scene
{
    public interface ICanvasSceneBase
    {
        ISceneCamera[] GetSceneCameraList();
        void InitializeCanvas(Camera canvasCamera);
    }
}