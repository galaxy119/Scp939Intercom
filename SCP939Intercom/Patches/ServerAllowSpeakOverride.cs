using System;
using Harmony;
using UnityEngine;

namespace SCP939Intercom.Patches
{
	[HarmonyPatch(typeof(Intercom), "ServerAllowToSpeak")]
	[HarmonyPatch(new Type[] {})]
	public class ServerAllowSpeakOverride
	{
		public static void Postfix(Intercom __instance, ref bool __result)
		{
			CharacterClassManager ccm = __instance.GetComponent<CharacterClassManager>();

			if (!ccm.CurClass.Is939())
				return;
			
			__result = Vector3.Distance(__instance.transform.position, __instance.area.position) < __instance.triggerDistance;
		}
	}
}