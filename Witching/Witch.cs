using System.Collections.Generic;
using Assets.Code;
using UnityEngine;
using Witching.Traits;
using Witching.Rituals;

namespace Witching
{
    public class Witch : UAE
    {
        private readonly List<ICanUpdateRituals> updaters =
            new List<ICanUpdateRituals>();

        public Witch(Location location, Society society)
            : base(location, society)
        {
            person.stat_might = 1;
            person.stat_lore = 3;
            person.stat_intrigue = 2;
            person.stat_command = 3;
            person.isMale = false;
            person.age = 42;
            person.hasSoul = true;
            person.receiveTrait(new WitchesPower(this));
            AddWitchesRitual(new Empower.RitualUpdater(this));
            AddWitchesRitual(new TheWitchesStarvation.RitualUpdater());
        }

        public void AddWitchesRitual(ICanUpdateRituals updater)
        {
            updaters.Add(updater);
        }

        public override string getName()
        {
            return "A Witch";
        }

        public override bool isCommandable()
        {
            return true;
        }

        public override bool hasStartingTraits()
        {
            return true;
        }

        public override List<Trait> getStartingTraits()
        {
            List<Trait> list =
                new List<Trait>
                {
                    new HungerWorship(),
                    new WitchesChanneler(),
                    new Prophet()
                };
            return list;
        }

        public override Sprite getPortraitForeground()
        {
            return EventManager.getImg("witching.witch.png");
        }

        public void UpdateRituals(WitchesPower witchesPower, Location newLocation)
        {
            updaters.ForEach(a => a.UpdateRituals(this, witchesPower, newLocation));
        }
    }
}


