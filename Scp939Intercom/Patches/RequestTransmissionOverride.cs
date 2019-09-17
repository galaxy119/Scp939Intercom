using Harmony;
using UnityEngine;

namespace Scp939Intercom.Patches
{
    [HarmonyPatch(typeof(Intercom), "RequestTransmission")]
    [HarmonyPatch(new[] { typeof(GameObject)})]
    public class RequestTransmissionOverride
    {
        public static bool Prefix(Intercom __instance, GameObject spk)
        {
            if (spk != null)
                return true;
            if (Intercom.host.speaker == null)
            {
                Scp939Intercom.plugin.Error("Host speaker is null :c");
                return true;
            }

            CharacterClassManager ccm = Intercom.host.speaker.GetComponent<CharacterClassManager>();

            if (ccm.curClass != 16 && ccm.curClass != 17)
                return true;
            Scp939PlayerScript script = Intercom.host.speaker.GetComponent<Scp939PlayerScript>();

            if (!script.NetworkusingHumanChat)
                __instance.Networkspeaker = null;
            return false;
        }
    }
}