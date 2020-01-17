using System.Collections.Generic;
using EXILED;
using Harmony;
using MEC;

namespace SCP939Intercom
{
	public class Plugin : EXILED.Plugin
	{
		public EventHandlers EventHandlers;
		public List<CoroutineHandle> Coroutines = new List<CoroutineHandle>();
		private HarmonyInstance instance;
		public static int InstanceNumber = 0;
		
		public override void OnEnable()
		{
			EventHandlers = new EventHandlers(this);
			Events.RoundStartEvent += EventHandlers.OnRoundStart;
			InstanceNumber++;
			instance = HarmonyInstance.Create($"com.joker.scp939-{InstanceNumber}");
			instance.PatchAll();
		}

		public override void OnDisable()
		{
			Events.RoundStartEvent -= EventHandlers.OnRoundStart;
			EventHandlers = null;
			instance.UnpatchAll();
			instance = null;
		}

		public override void OnReload()
		{
			
		}

		public override string getName { get; } = "SCP 939 Intercom";
	}
}