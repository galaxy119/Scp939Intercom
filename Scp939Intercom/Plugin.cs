using Smod2;
using Smod2.Attributes;
using Smod2.Config;

namespace Scp939Intercom
{
    [PluginDetails(author = "Joker119",
        description = "Lets 939 talk on intercom.",
        id = "joker.scp939intercom",
        configPrefix = "939intercom",
        name = "Scp939Intercom",
        version = "1.0.0",
        SmodMajor = 3,
        SmodMinor = 5,
        SmodRevision = 1)]
    
    public class Scp939Intercom : Plugin
    {
        public Methods Functions { get; private set; }
        public static Scp939Intercom plugin;
        [ConfigOption] public static bool Enabled = true;
        
        public override void Register()
        {
            AddEventHandlers(new EventHandlers(this));
            Functions = new Methods(this);
            plugin = this;
        }

        public override void OnEnable()
        {
            throw new System.NotImplementedException();
        }

        public override void OnDisable()
        {
            throw new System.NotImplementedException();
        }
    }
}