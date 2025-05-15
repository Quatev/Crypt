using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Crypt.Patch;

namespace Crypt.Injection
{
    public class Inject : MonoBehaviour
    {
        public static GameObject obj = null;

        public static void OnInjection()
        {
            obj = new GameObject("Crypt Internal");
            obj.AddComponent<Plugin>();
            obj.AddComponent<UpdateLoop>();
            obj.AddComponent<PipeLineServerThingy>();
            DontDestroyOnLoad(obj);
        }
        public static void OnDejection()
        {
            PipeLineServerThingy.PipeServer?.Dispose();
            obj.Destroy();
            Destroy(obj);
        }
    }
}
