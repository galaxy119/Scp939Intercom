using System;
using System.Collections.Generic;
using System.Reflection;
using MEC;
using UnityEngine;

namespace SCP939Intercom
{
	public class EventHandlers
	{
		private Plugin plugin;
		public EventHandlers(Plugin plugin) => this.plugin = plugin;

		private Transform intercomeArea = null;

		private Transform IntercomArea
		{
			get
			{
				if (intercomeArea == null)
					intercomeArea = typeof(Intercom).GetField("area", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(Intercom.host) as Transform;
				
				if (intercomeArea == null)
					throw new MissingFieldException("Field for intercom not found.");
				return intercomeArea;
			}
		}

		public void OnRoundStart()
		{
			intercomeArea = null;
			plugin.Coroutines.Add(Timing.RunCoroutine(CheckFor939Intercom()));
		}

		public IEnumerator<float> CheckFor939Intercom()
		{
			for (;;)
			{
				yield return Timing.WaitForSeconds(0.1f);

				if (Intercom.host.speaker != null || Intercom.host.speaking)
					continue;
				
				foreach (ReferenceHub rh in Plugin.GetHubs())
				{
					try
					{
						if (!rh.characterClassManager.CurClass.Is939())
							continue;
						GameObject player = rh.gameObject;
						Intercom intercom = player.GetComponent<Intercom>();
						Scp939PlayerScript script = player.GetComponent<Scp939PlayerScript>();

						if (Vector3.Distance(player.transform.position, intercomeArea.position) >
						    intercom.triggerDistance)
							continue;
						if (!script.NetworkusingHumanChat)
							continue;
						Intercom.host.RequestTransmission(player);
					}
					catch (Exception e)
					{
						while (e != null)
						{
							Plugin.Error(e.ToString());
							e = e.InnerException;
						}
					}
				}
			}
		}
	}
}