using System.Linq;
using Assets.Code;
using UnityEngine;
using Witching.Bolts;


namespace Witching
{
    public class WitchAbstract : UAE_Abstraction
    {
        public WitchAbstract(Map map)
            : base(map, -1) { }

        public override bool validTarget(Location location)
        {
            if (AgentCapReached()) return false;
            if (location.settlement == null) return false;
            return location.settlement.subs.Any(sub => sub.getName() == "Coven");
        }

        private bool AgentCapReached()
        {
            return map.world.map.overmind.nEnthralled >= map.world.map.overmind.getAgentCap();
        }

        public override void createAgent(Location target)
        {
            var society =
                target.settlement.subs
                    .Select(a => a as Sub_Temple)
                    .First(a => a != null).order;
            var allreadyUsed = target.map.units.Where(a => a is Witch).Select(a => (a as Witch).ImageIndex);
            var all = Enumerable.Range(1, 5);
            var available = all.Where(a => !allreadyUsed.Contains(a)).ToList();
            if (available.Count == 0)
                available = all.ToList();
            var index = Eleven.random.Next(available.Count);
            var uA = new Witch(target, society, available[index]);
            uA.maxHp = 3;
            uA.hp = 3;
            uA.person.stat_might = getStatMight();
            uA.person.stat_lore = getStatLore();
            uA.person.stat_intrigue = getStatIntrigue();
            uA.person.stat_command = getStatCommand();
            target.units.Add(uA);
            target.map.overmind.agents.Add(uA);
            target.map.units.Add(uA);
            uA.person.shadow = 1.0;
            uA.person.extremeHates.Clear();
            uA.person.extremeHates.Clear();
            uA.person.hates.Clear();
            uA.person.likes.Clear();
            GraphicalMap.selectedUnit = uA;
            target.map.overmind.availableEnthrallments--;
        }

        // public override Sprite getBackground()
        // {
        //     return map.world.iconStore.standardBack;
        // }

        public override Sprite getForeground()
        {
            return EventManager.getImg("witching.witch-1.png");
        }

        public override string getName()
        {
            return "A Witch";
        }

        public override string getDesc()
        {
            return "Gathering at their Coven the witches seek to bring chaos into this world. They have developed a fascination for, and ways to, manipulate the hunger. They perform blaspmephous rituals and worship at their covens in order to obtain their power, with which they can encourage people infected by the hunger to feed. Their High Priestesses can even inflict the hunger on unsuspectiong bystanders. The Truth Speaking ability allows them to extract funds from nearby rulers. Their prophets, the Soothsayers focus on preaching and temple building, while the Channelers can generate even more Witches Power.";
        }

        public override string getFlavour()
        {
            return "Witches have decent lore, but not much else. Don't take them into combat. When in the same location, one witch can gift all her power to another.";
        }

        public override string getRestrictions()
        {
            return "Requires a Coven.";
        }

        public override int getStatMight()
        {
            return Constants.Might;
        }

        public override int getStatLore()
        {
            return Constants.Lore;
        }

        public override int getStatIntrigue()
        {
            return Constants.Intrigue;
        }

        public override int getStatCommand()
        {
            return Constants.Command;
        }
    }

}

