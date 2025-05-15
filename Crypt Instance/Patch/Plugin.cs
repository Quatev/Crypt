using BepInEx;

namespace Crypt.Patch
{
    [BepInPlugin(PluginInfo.PluginGUID, PluginInfo.PluginName, PluginInfo.PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        public void OnEnable() 
        {
            AddRemovePatches.AddPatches();
        }
        public void OnDisable() 
        {
            AddRemovePatches.RemovePatches();
        }
    }
}
