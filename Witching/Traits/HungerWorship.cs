using Assets.Code;
using Witching.Rituals;

namespace Witching.Traits
{
    public class HungerWorship : Trait
    {
        public override string getName()
        {
            return "The Witches Hunger";
        }

        public override string getDesc()
        {
            return "The Witch gets access to the Witches Hunger, a ritual which allows for expending Power charges in order to inflict the Hunger on Heroes and Rulers.";
        }

        public override void onAcquire(Person person)
        {
            var witch = person.unit as Witch;
            witch.AddWitchesRitual(new TheWitchesHunger.RitualUpdater());
        }
    }
}