using Assets.Code;
using Witching.Rituals;

namespace Witching.Traits
{
    public class WitchesPower : Trait
    {
        public Witch witch;

        public int Charges = 10;

        public int ChargesReceivedMultiplier = 1;

        public WitchesPower(Witch witch)
            => this.witch = witch;

        public override string getName()
        {
            return "Witches Power (" + Charges + ")";
        }

        public override string getDesc()
        {
            return "The Witch can obtain power by completing either a Blasmephous Rituals for three charges, Dark Worship for two charges or Holy: Dark Worship for one charge.";
        }

        public override void onAcquire(Person person)
        {
            witch.UpdateRituals(this, witch.location);
        }

        public override void completeChallenge(Challenge challenge)
        {
            base.completeChallenge(challenge);
            if (challenge is Ch_BlasphemousRituals)
            {
                Charges += 3 * ChargesReceivedMultiplier;
            }
            if (challenge is Ch_DarkWorshipAtTemple || challenge is Ch_DarkWorship)
            {
                Charges += 2 * ChargesReceivedMultiplier;
            }
            if (challenge is Ch_H_DarkWorshipAtTemple)
            {
                Charges += 1 * ChargesReceivedMultiplier;
            }
        }

        public override void turnTick(Person person)
        {
            base.turnTick(person);
            witch.UpdateRituals(this, witch.location);
        }

        public override void onMove(Location current, Location destination)
        {
            base.onMove(current, destination);
            witch.UpdateRituals(this, destination);
            foreach (var unit in destination.units)
                if (unit is Witch otherWitch)
                    otherWitch.rituals.Add(new Empower(destination, witch.GetPower(), witch.person));
        }
    }
}