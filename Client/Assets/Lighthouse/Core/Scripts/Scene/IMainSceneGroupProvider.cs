namespace Lighthouse.Core.Scene
{
    public interface IMainSceneGroupProvider
    {
        MainSceneGroup AddMainSceneGroup(params MainSceneKey[] mainSceneKeys);
        MainSceneGroup GetMainSceneGroup(MainSceneKey mainSceneKey);
        void ClearSceneGroup();
    }
}