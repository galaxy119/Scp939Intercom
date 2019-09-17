using MEC;
using Smod2.EventHandlers;
using Smod2.Events;

namespace Scp939Intercom
{
    public class EventHandlers : IEventHandlerRoundStart, IEventHandlerRoundEnd
    {
        private readonly Scp939Intercom plugin;
        public EventHandlers(Scp939Intercom plugin) => this.plugin = plugin;

        public void OnRoundStart(RoundStartEvent ev)
        {
            plugin.Functions._intercomArea = null;
            Timing.RunCoroutine(plugin.Functions.CheckFor939Intercom(), "Scp939IntercomCheck");
        }

        public void OnRoundEnd(RoundEndEvent ev)
        {
            Timing.KillCoroutines("Scp939IntercomCheck");
        }
    }
}