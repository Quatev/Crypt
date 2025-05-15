using GorillaLocomotion;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Crypt.Injection;

namespace Crypt
{
    [HarmonyPatch(typeof(GTPlayer), "LateUpdate")]
    internal class UpdateLoop : MonoBehaviour
    {
        private static bool DoOnce = false;

        public static void Prefix() 
        {
            if (!DoOnce) 
            {
                _ = Task.Run(() => PipeLineServerThingy.ConnectAndListen());
                DoOnce = true;
            }
        }
    }
}
