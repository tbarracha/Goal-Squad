
namespace StardropTools
{
    public static class LoopManagerInitializer
    {
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initializer() => LoopManager.Instance.Initialize();
    }
}