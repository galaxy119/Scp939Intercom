using System;
using System.Collections.Generic;
using System.Reflection;
using Harmony;
using Smod2.API;
using UnityEngine;

namespace Scp939Intercom
{
    public class Methods
    {
        private readonly Scp939Intercom plugin;
        internal Transform _intercomArea = null;
        public Methods(Scp939Intercom plugin) => this.plugin = plugin;

        public void PatchMethodsUsingHarmony()
        {
            HarmonyInstance instance = HarmonyInstance.Create("com.joker119/.939intercom");
            instance.PatchAll();
        }

        public IEnumerator<float> CheckFor939Intercom()
        {
            for (;;)
            {
                if (Intercom.host.speaker != null || Intercom.host.speaking)
                    continue;

                foreach (Player player in plugin.Server.GetPlayers())
                {
                    try
                    {
                        if ((int) player.TeamRole.Role != 16 && (int) player.TeamRole.Role != 17)
                            continue;
                        GameObject ply = player.GameObject();
                        Intercom intercom = ply.GetComponent<Intercom>();
                        Scp939PlayerScript script = ply.GetComponent<Scp939PlayerScript>();

                        if (Vector3.Distance(ply.transform.position, intercomArea.position) > intercom.triggerDistance)
                            continue;
                        if (!script.NetworkusingHumanChat)
                            continue;
                        Intercom.host.RequestTransmission(ply);
                    }
                    catch (Exception e)
                    {
                        while (e != null)
                        {
                            plugin.Error(e.ToString());
                            e = e.InnerException;
                        }
                    }
                }
            }
        }

        private Transform intercomArea
        {
            get
            {
                if (_intercomArea == null)
                    _intercomArea = typeof(Intercom).GetField("area", BindingFlags.NonPublic | BindingFlags.Instance)
                        ?.GetValue(Intercom.host) as Transform;
                
                if (_intercomArea == null)
                    throw new MissingFieldException("Field for intercom not found.");

                return _intercomArea;
            }
        }
    }
}