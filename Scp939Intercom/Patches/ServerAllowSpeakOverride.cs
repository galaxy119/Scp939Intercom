using System;
using Harmony;
using UnityEngine;

namespace Scp939Intercom.Patches
{
    [HarmonyPatch(typeof(Intercom), "ServerAllowToSpeak")]
    [HarmonyPatch(new Type[] {})]
    public class ServerAllowSpeakOverride
    {
        public static void Postfix(Intercom __instance, ref Transform __area, ref bool __result)
        {
            if (!Scp939Intercom.Enabled)
                return;

            CharacterClassManager ccm = __instance.GetComponent<CharacterClassManager>();

            if (ccm.curClass != 16 && ccm.curClass != 17)
                return;

            __result = Vector3.Distance(__instance.transform.position, __area.position) < __instance.triggerDistance;
        }
    }
}