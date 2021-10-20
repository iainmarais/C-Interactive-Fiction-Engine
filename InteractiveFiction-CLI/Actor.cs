/*
Interactive Fiction Command line interpreter
Actor.cs

© 2021 Iain Marais (il-Salvatore on Github)
Licence: Apache v2.0 or 3-clause BSD Licence

Please see www.apache.org/licenses/LICENSE-2.0.html || opensource.org/licenses/BSD-3-Clause for more information.

The scope of this project is to build a simple but efficient command line interpreter for a console-based interactive fiction engine,
Think classic Zork, where one entered commands and read the output.

This project will be entirely c# based.
*/

using System;
using System.Collections.Generic;


namespace InteractiveFiction_CLI
{
    public class Actor
    {
        public EActorGender ActorGender { get; set; }
        public EActorClass ActorClass { get; set; }
        public EActorType ActorType { get; set; }
        public bool IsKnockedOut { get; set; }
        public bool IsDead { get; set; }
        public int HitPoints { get; set; }
        public static bool CurrentActor { get; set; }
        public static bool TargetActor { get; set; }
        public string Name { get; set; }
        List<Object> ActorInventory = new();

        public Actor()
        {

        }
        public class Guard : Actor
        {
            public Guard()
            {

            }
            public Guard(string actorName, bool isKnockedOut, bool isDead, List<Object> actorInventory)
            {
                ActorClass = EActorClass.Guard;
                Name = actorName;
                IsKnockedOut = isKnockedOut;
                IsDead = isDead;
                ActorInventory = actorInventory;
            }
        }
        public class UnarmedCitizen : Actor
        {
            public UnarmedCitizen()
            {

            }
            public UnarmedCitizen(string actorName, bool isKnockedOut, bool isDead, List<Object> actorInventory)
            {
                ActorClass = EActorClass.Citizen;
                Name = actorName;
                IsKnockedOut = isKnockedOut;
                IsDead = isDead;
                ActorInventory = actorInventory;
            }
        }
        public class IronBeast : Actor
        {
            public IronBeast()
            {

            }
            public IronBeast(string actorName, bool isKnockedOut, bool isDead, List<Object> actorInventory)
            {
                ActorClass = EActorClass.IronBeast;
                Name = actorName;
                IsKnockedOut = isKnockedOut;
                IsDead = isDead;
                ActorInventory = actorInventory;
            }
        }
        public class Beast : Actor
        {
            public Beast()
            {

            }
            public Beast(string actorName, bool isKnockedOut, bool isDead, List<Object> actorInventory)
            {
                ActorClass = EActorClass.Beast;
                IsKnockedOut = isKnockedOut;
                IsDead = isDead;
                ActorInventory = actorInventory;
            }
        }
        public class Player : Actor
        {
            public static List<Object> PlayerInventory = new();
            public Player(string actorName, List<Object> playerInventory, int hp, bool isDead, bool isKnockedOut)
            {
                ActorClass = EActorClass.Thief;
                Name = actorName;
                PlayerInventory = playerInventory;
                HitPoints = hp;
                IsDead = isDead;
                IsKnockedOut = isKnockedOut;

            }
        }

    }
}