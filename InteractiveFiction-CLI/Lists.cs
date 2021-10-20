/*
 Interactive Fiction Command line interpreter
 Lists.cs

© 2021 Iain Marais (il-Salvatore on Github)
Licence: Apache v2.0 or 3-clause BSD Licence

Please see www.apache.org/licenses/LICENSE-2.0.html || opensource.org/licenses/BSD-3-Clause for more information.

The scope of this project is to build a simple but efficient command line interpreter for a console-based interactive fiction engine,
Think classic Zork, where one entered commands and read the output.

This project will be entirely c# based.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveFiction_CLI
{


    public enum EWeaponType { Blackjack, Sword, Bow, Hammer, Mace, Wand, Fists }
    public enum EWeaponClass { Melee, Ranged, Magic, Unarmed }
    public enum EWeaponProperty { Lethal, Nonlethal }
    public enum EActorClass
    {
        Guard, Thief, ArcherGuard, Noble, Servant, Citizen, Officer, OfficerCaptain, OfficerArcher, HammeriteGuard,
        HammeriteArcher, HammeritePriest, HammeriteNovice, PaganCommoner, PaganGuard, PaganArcher, PaganMage, Keeper,
        KeeperElder, KeeperNovice, KeeperGuard, KeeperArcher, Zombie, Ghost, HammerHaunt, TreeBeast
    }
    public enum EActorType { Creature, Human, IronBeast, Zombie, Ghost, WoodenBeast }
    public enum LocID { LocBedroom, LocLounge, LocLivingroom, LocEntrancehall, LocDiningarea, LocAttic, LocBasement, LocBedroom2 }

    public class WordList
    {   //List of action words for first stage of Command processor
        public static List<string> Actions = new() { "where", "look", "get", "put", "open", "unlock", "steal", "attack", "use", "jump", "climb", "move", "take", "lock", "pickpocket", "lockpick", "go" };
        public static List<string> StaticObjectNames = new() { "door", "chair", "torch", "desk", };
        public static List<string> ContainerNames = new() { "chest", "drawer", "table", "fridge", "cupboard", "drawer" };
        public static List<string> ConsumableNames = new() { "beer", "wine", "water", "steak", "fruit", "potion", };
        public static List<string> ObjectNames = new() { "blackjack", "sword", "bow", "arrow", "hammer", "mace", "knife", "flashbomb", "landmine", "dagger", "coin", "coins", "purse", "wand", "lockpicks", "lamp" };
    }

}
