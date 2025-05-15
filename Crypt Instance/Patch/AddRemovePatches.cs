using HarmonyLib;
using System.Reflection;

namespace Crypt.Patch
{
    public class AddRemovePatches
    {
        private static Harmony Instance = null;

        public static void AddPatches() 
        {
            if (Instance == null) 
            {
                Instance = new Harmony(PluginInfo.PluginGUID);
                Instance.PatchAll(Assembly.GetExecutingAssembly());
            }
        }

        public static void RemovePatches() 
        {
            if (Instance != null) 
            {
                Instance.UnpatchSelf();
                Instance = null;
            }
        }
    }
}