﻿/*
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
using System.Linq;

namespace InteractiveFiction_CLI
{
    public class Actor
    {
        public EActorGender ActorGender { get; set; }
        public EActorClass ActorClass { get; set; }
        public EActorType ActorType { get; set; }
        public bool IsKnockedOut { get; set; }
        public bool ResistantToKnockout { get; set; }
        public bool IsDead { get; set; }
        public int HitPoints { get; set; }
        public static int PlayerHitPoints { get; set; }
        public static Actor CurrentActor { get; set; }
        public static Actor TargetActor { get; set; }
        public string ActorName { get; set; }
        List<Object> ActorInventory = new();
        public InventorySystem PlayerInventory { get; set; }

        public Actor()
        {
        }
        //Knockout function - should work normally.
        //Queries the current Loc for an actor with a matching actor name, if found it procedes to execute the method of said function
        //if not, it will warn the user.
        public void KnockOutActor(string actorName)
        {
            Actor myActor = new();
            Location CurrentLoc = new();
            CurrentLoc = CurrentLoc.GetIsCurrentLoc();
            try
            {
                myActor = CurrentLoc.LocationActors.Where(x => x.ActorName == actorName).FirstOrDefault();
                if (myActor.ActorName == actorName && myActor.IsKnockedOut == false)
                {
                    if (ResistantToKnockout == false)
                    {
                        myActor.IsKnockedOut = true;
                        Console.WriteLine($"You knock {myActor.ActorName} out.");
                    }
                    else
                    {
                        Console.WriteLine($"{myActor.ActorName} can not be knocked out.");
                    }
                }
                else if (myActor.IsKnockedOut)
                {
                    Console.WriteLine($"{myActor.ActorName} is already unconscious");
                }
                else
                {
                    IsKnockedOut = false;
                    Console.WriteLine($"You fail to knock {myActor.ActorName} out ");
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("No such actor is present here.");
            }
        }
        //Attack function - should work normally. 
        //As with the knockout function it checks for the actor by name in the current Loc, then procedes to execute the method, if no actor by said name is present it will warn the user.
        //ToDo: Add random damage for dealing with the player - no fight is one-sided in life and nor should it be even in IF.
        //Using a do-while loop to handle the processing of the command rather than a while loop, we want to see whether the actor in question is dead and tell the player, 
        //no point in attacking something already dead.
        public void AttackActor(string actorName, Object.PickuppableObject.Weapon myWeapon)
        {
            Location CurrentLoc = new();
            CurrentLoc = CurrentLoc.GetIsCurrentLoc();
            Actor CurrentActor = new();
            try
            {
                Random DiceRoll = new();
                int Damage = DiceRoll.Next(myWeapon.MinDmg, myWeapon.MaxDmg);
                CurrentActor = CurrentLoc.LocationActors.Where(x => x.ActorName == actorName).FirstOrDefault();
                do
                {
                    CurrentActor.HitPoints -= Damage;
                    PlayerHitPoints -= Damage;
                    //need to add this in, possibly logic or as an actor var
                    //Player hp should realistically be a static int since IF is played/read primarly from the first person's POV.
                    Console.WriteLine($"You attack {CurrentActor.ActorName} for {Damage} hitpoints.");
                    if (CurrentActor.HitPoints == 0)
                    {
                        IsDead = true;
                    }
                    else
                    {
                        Console.WriteLine($"{CurrentActor.ActorName} is already dead.");
                        break;
                    }
                    break; //normal break of loop so that we have to reenter the command to attack instead of it running infinitely until actor hp reaches 0.
                } while (IsDead == false);
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("No such actor is present here.");
            }
        }
        public class Guard : Actor
        {
            public Guard()
            {
            }
            public Guard(string actorName, bool isKnockedOut, bool isDead, List<Object> actorInventory)
            {
                ActorClass = EActorClass.Guard;
                ActorName = actorName;
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
                ActorName = actorName;
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
                ActorName = actorName;
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
            public Player(string actorName, int hp, bool isDead, bool isKnockedOut)
            {
                ActorClass = EActorClass.Thief;
                ActorName = actorName;
                HitPoints = hp;
                IsDead = isDead;
                IsKnockedOut = isKnockedOut;
            }

            public Player(string actorName, InventorySystem playerInventory, int hp, bool isDead, bool isKnockedOut)
            {
                ActorClass = EActorClass.Thief;
                ActorName = actorName;
                PlayerInventory = playerInventory;
                HitPoints = hp;
                IsDead = isDead;
                IsKnockedOut = isKnockedOut;
            }
            public Player()
            {

            }
        }
    }
}