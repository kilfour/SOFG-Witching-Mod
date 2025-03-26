using System.Linq;
using Assets.Code;
using Witching.Traits;
using Witching.Rituals.Bolts;

namespace Witching.Rituals
{
    public class TheWitchesStarvation : WitchesMobileRitual
    {
        public class RitualUpdater : RitualUpdater<TheWitchesStarvation>
        {
            protected override bool CanBeCastOnHero(Person person)
            {
                return person.traits.Any(a => a is T_TheHunger);
            }

            protected override bool CanBeCastOnRuler(SettlementHuman humanSettlement)
            {
                return humanSettlement.ruler.traits.Any(a => a is T_TheHunger);
            }

            protected override Ritual GetRitual(Location location, WitchesPower witchesPower, Person prey)
            {
                return new TheWitchesStarvation(location, witchesPower, prey);
            }
        }

        protected override int RequiredCharges => 5;

        public TheWitchesStarvation(Location location, WitchesPower witchesPower, Person prey)
            : base(location, witchesPower, prey) { }

        public override string getName()
        {
            return "Starve " + prey.getName();
        }

        public override string getDesc()
        {
            return "Spend a Witches Power in order to increase 'The Hunger' on " + prey.getName() + ", which encourages them to feed.";
        }

        public override string getRestriction()
        {
            return "Requires one Witches Power.";
        }

        public override string getCastFlavour()
        {
            return "The Hunger slowly changes its victim, over the course of months, as their humanity is sapped. What they previously valued, the people they previously cherished, are lost to them. Their previous desires slip away, to be replaced by a near animalistic desire to feed, a constant yearning for the sensation of blood flowing past their ever-sharpening teeth.";
        }

        public override double getComplexity()
        {
            return map.param.mg_theHungerComplexity / 5;
        }

        public override int getCompletionMenace()
        {
            return map.param.mg_theHungerMenaceCaster / 5;
        }

        public override int getCompletionProfile()
        {
            return map.param.mg_theHungerProfile / 5;
        }

        public override void complete(UA u)
        {
            if (prey.traits.FirstOrDefault(a => a is T_TheHunger) is T_TheHunger hunger)
                hunger.strength = 1000;
            RitualComplete();
        }
    }
}