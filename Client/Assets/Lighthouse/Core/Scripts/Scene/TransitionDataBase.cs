using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Lighthouse.Core.Scene
{
    public abstract class TransitionDataBase
    {
        static readonly CommonSceneKey[] MustRequireCommonSceneIds = Array.Empty<CommonSceneKey>();

        public abstract MainSceneKey MainSceneKey { get; }

        public CommonSceneKey[] RequireCommonSceneIds
        {
            get
            {
                if (requireCommonSceneIds == null)
                {
                    requireCommonSceneIds = MustRequireCommonSceneIds.Concat(ExtendCommonSceneIds).ToArray();
                }

                return requireCommonSceneIds;
            }
        }

        protected abstract CommonSceneKey[] ExtendCommonSceneIds { get; }

        CommonSceneKey[] requireCommonSceneIds;

        public virtual bool CanTransition()
        {
            return true;
        }

        public virtual UniTask SaveSceneState(TransitionType transitionType, CancellationToken cancelToken)
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask LoadSceneState(TransitionType transitionType, CancellationToken cancelToken)
        {
            return UniTask.CompletedTask;
        }
    }
}