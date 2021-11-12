/*
Interactive Fiction Command line interpreter 
Logic.cs - this file contains all the logic for the command processor

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

    class Logic
    {
        public static InventorySystem InvSys = new();
        //moved here from Object.cs

        //Replaced location check in each function with a new simple func that checks, reducing over all code complexity and improving readability
        //This function performs a simple location check against currentLoc, returning the starting loc if null, or where the player is after executing
        //a move.
        public static Location GetCurrentLoc()
        {
            Location currentLoc = new();
            if (Location.CurrentLoc == null)
            {
                currentLoc = Scene.Scene1.SceneLocations.Where(x => x.IsCurrentLocation == true).FirstOrDefault();
            }
            else if (Location.CurrentLoc != null)
            {
                currentLoc = Location.CurrentLoc;
            }
            return currentLoc;
        }
        //This function is possibly not in use anymore after converting the container handler functions to object-based ones.
        public static Object.Container GetCurrentContainer()
        {
            Location currentLoc = GetCurrentLoc();
            Object.Container currentContainer = new();
            if (Object.Container.CurrentContainer != null)
            {
                currentContainer = (Object.Container)Object.Container.CurrentContainer;
            }
            else if (Object.Container.CurrentContainer == null)
            {
                currentContainer = (Object.Container)currentLoc.LocationInventory.Where(x => x.Name == Object.ContainerName).FirstOrDefault();
            }
            return currentContainer;
        }
        //Moved here from Location.cs
        public static void GetLocationInventory()
        {
            Location currentLoc = GetCurrentLoc();
            if (currentLoc == null)
            {
                //Handle the error condition gracefully, thus avoiding exceptions and crashes.
                Console.WriteLine("I see absolutely nothing.");
            }
            else
            {
                if (currentLoc.LocationInventory != null)
                {
                    Console.Write($"In {currentLoc.LongName} I can see: ");
                    foreach (var InventoryObject in currentLoc.LocationInventory)
                    {
                        if (currentLoc.LocationInventory.Count > 0)
                            Console.Write($"{InventoryObject.LongName} ");
                        else
                            Console.WriteLine("There is nothing here.");
                    }
                    Console.Write("\n");
                }
                //Handle the error condition gracefully, thus avoiding exceptions and crashes.
                else if (currentLoc.LocationInventory == null)
                {
                    Console.WriteLine("There is no storage space here.");
                }
            }

        }
        public static Location GetNewLocation()
        {
            string TargetLocName = CommandProcessor.Command.Word2;
            Location targetLoc = new();
            targetLoc = Scene.Scene1.SceneLocations.Where(x => x.Name == TargetLocName).FirstOrDefault();
            Location.TargetLoc = targetLoc;
            return targetLoc;
        }
        public static void GetCurrentLocation()
        {
            Location currentLoc = GetCurrentLoc();
            if (currentLoc == null)
            {
                //Handle the error condition gracefully, thus avoiding exceptions and crashes.
                Console.WriteLine($"I am nowhere yet.");
            }
            else
            {
                Console.WriteLine($"I am in {currentLoc.LongName}");
                GetDirection();
            }

        }
        public static void GetDirection()
        {
            Location currentLoc = GetCurrentLoc();
            Location targetLoc = new();
            Location TargetLocation = new();
            foreach (var Loc in Scene.Scene1.SceneLocations)
            {
                TargetLocation = Scene.Scene1.SceneLocations.Where(x => x.Name == Loc.Name).FirstOrDefault();
                targetLoc = TargetLocation;
                if (currentLoc.HasExitN == true && targetLoc.HasExitS == true)
                {
                    Console.WriteLine($"There is an exit North, leading to {targetLoc.LongName}");
                }
                if (currentLoc.HasExitS == true && targetLoc.HasExitN == true)
                {
                    Console.WriteLine($"There is an exit South, leading to {targetLoc.LongName}");
                }
                if (currentLoc.HasExitE == true && targetLoc.HasExitW == true)
                {
                    Console.WriteLine($"There is an exit East, leading to {targetLoc.LongName}");
                }
                if (currentLoc.HasExitW == true && targetLoc.HasExitE == true)
                {
                    Console.WriteLine($"There is an exit West, leading to {targetLoc.LongName}");
                }
                if (currentLoc.HasExitUp == true && targetLoc.HasExitDown == true)
                {
                    Console.WriteLine($"There is a stairway leading up to {targetLoc.LongName}");
                }
                if (currentLoc.HasExitDown == true && targetLoc.HasExitUp == true)
                {
                    Console.WriteLine($"There is a stairway leading down to {targetLoc.LongName}");
                }

            }
        }
        /*
        public static void GoToNewLocation(Location targetLoc)
        {

        }
        */
        public static void GoToNewLocation()
        {

            Location currentLoc = GetCurrentLoc();
            Location targetLoc = GetNewLocation();
            Location newLoc = targetLoc;
            if (newLoc == targetLoc)
            {
                Location.GetIsConnected(currentLoc, targetLoc);
                //Handle the error condition gracefully, thus avoiding exceptions and crashes.
                if (targetLoc == null || !(Location.GetIsConnected(currentLoc, targetLoc)))
                {
                    Console.WriteLine("You can't go that way");
                }
                else
                {
                    if (Location.IsConnected == false && targetLoc != null)
                    {
                        Console.WriteLine($"I know where the {targetLoc.Name} is, but how do I get there?");
                    }
                    else
                    {
                        currentLoc = targetLoc;
                        currentLoc.IsCurrentLocation = true;
                        Location.CurrentLoc = currentLoc;
                        Console.WriteLine($"I am now in {currentLoc.LongName}");
                    }

                }
            }


        }//End location handler

        //Moved scene handler here from scene.cs
        public static void SetScene()
        {
            Scene.Scene1 myScene1 = new();
            Console.WriteLine(myScene1.SceneDescription);
        }
    }

}
