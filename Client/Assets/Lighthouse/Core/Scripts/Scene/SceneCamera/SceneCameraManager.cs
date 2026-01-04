using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Lighthouse.Core.Scene
{
    public class SceneCameraManager : MonoBehaviour, ISceneCameraManager
    {
        public ISceneCamera BaseCamera { get; private set; }
        public ISceneCamera UICamera { get; private set; }

        public ISceneCamera[] OverlayCameraList { get; private set; }

        void ISceneCameraManager.UpdateCameraStack(
            MainSceneGroup afterMainSceneGroup,
            CommonSceneManager commonSceneManager,
            CommonSceneKey[] targetCommonSceneIds)
        {
            var sceneCameras = commonSceneManager
                .GetSceneCameraList(targetCommonSceneIds)
                .Concat(afterMainSceneGroup.GetSceneCameraList())
                .Distinct()
                .OrderBy(x => (x.SceneCameraType, x.CameraDefaultDepth)).ToArray()
                .ToArray();

            var baseCamera = sceneCameras.First();
            var overlayCameraList = sceneCameras.Skip(1).ToArray();

            if (ReferenceEquals(BaseCamera, baseCamera) && OverlayCameraList.SequenceEqual(overlayCameraList))
            {
                return;
            }

            BaseCamera.ClearStackCamera();
            foreach (var overlayCamera in OverlayCameraList)
            {
                overlayCamera.ClearStackCamera();
            }

            BaseCamera = baseCamera;
            UICamera = sceneCameras.FirstOrDefault(x => x.SceneCameraType == SceneCameraType.CameraUI);
            OverlayCameraList = overlayCameraList;

            var depth = 0.0f;
            BaseCamera.SetupCamera(CameraRenderType.Base, depth);

            foreach (var overlayCamera in overlayCameraList)
            {
                depth++;

                overlayCamera.SetupCamera(CameraRenderType.Overlay, depth);
                BaseCamera.AddStackCamera(overlayCamera);
            }
        }
    }
}