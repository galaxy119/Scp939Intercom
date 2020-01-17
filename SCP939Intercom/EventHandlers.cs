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
						//Plugin.Info("Checking class");
						if (!rh.characterClassManager.CurClass.Is939())
							continue;
						//Plugin.Info("Is939");
						GameObject player = rh.gameObject;
						//Plugin.Info("got game object");
						Intercom intercom = player.GetComponent<Intercom>();
						//Plugin.Info("Got intercom");
						Scp939PlayerScript script = player.GetComponent<Scp939PlayerScript>();
						//Plugin.Info("Got script");

						if (Vector3.Distance(player.transform.position, IntercomArea.position) >
						    intercom.triggerDistance)
						{
							//Plugin.Info("distance check too far");
							continue;
						}

						if (!script.NetworkusingHumanChat)
						{
							//Plugin.Info("not using hooman chat");
							continue;
						}

						//Plugin.Info("requesting transmition");
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