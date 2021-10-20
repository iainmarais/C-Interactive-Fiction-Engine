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
    partial class Program
    {
        class Logic
        {
            //To do: Move logic out of scene,obj and loc files here as partial classes.
            public void GetObject()
            {
                //do something
            }
            //moved here from Object.cs
            public static void GetContainerInventory()
            {
                if (Location.CurrentLoc != null)
                {
                    Location currentLoc = Location.CurrentLoc;
                    if (Object.Container.CurrentContainer == null)
                    {
                        try
                        {
                            Object.Container currentContainer = (Object.Container)currentLoc.LocationInventory.Where(x => x.Name == CommandProcessor.Command.Word2).FirstOrDefault();
                            if (currentContainer == null)
                            {
                                Console.WriteLine("There is no container of that type here");
                            }
                            else
                            {
                                {
                                    foreach (var item in currentContainer.ContainerInventory)
                                    {
                                        if (item.StackCount == 0)
                                        {
                                            Console.WriteLine($"{currentContainer.LongName} contains {item.Name}");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"{currentContainer.LongName} contains {item.StackCount} of item: {item.Name}");
                                        }
                                    }
                                }
                            }
                        }
                        catch (ArgumentNullException)//test catch here and return safely. Crashing != option here -
                                                     //Simply tell the player/reader there is no sodding container of that type here.
                        {
                            Console.WriteLine("There is no container of that type here");
                        }
                    }
                    else if (Object.Container.CurrentContainer != null)
                    {
                        Object.Container currentContainer = Object.Container.CurrentContainer;
                        if (currentContainer == null)
                        {
                            Console.WriteLine("There is no container of that type here");
                        }
                        else
                        {
                            {
                                foreach (var item in currentContainer.ContainerInventory)
                                {
                                    if (item.StackCount == 0)
                                    {
                                        Console.WriteLine($"{currentContainer.LongName} contains {item.Name}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"{currentContainer.LongName} contains {item.StackCount} of item: {item.Name}");
                                    }
                                }
                            }
                        }
                    }

                }
                else if (Location.CurrentLoc == null)
                {
                    Location currentLoc = Scene.Scene1.SceneLocations.Where(x => x.IsCurrentLocation == true).FirstOrDefault();
                    if (Object.Container.CurrentContainer == null)
                    {
                        try
                        {
                            Object.Container.Container currentContainer = (Object.Container)currentLoc.LocationInventory.Where(x => x.Name == CommandProcessor.Command.Word2).FirstOrDefault();
                            if (currentContainer == null)
                            {
                                Console.WriteLine("There is no container of that type here");
                            }
                            else if (currentContainer.ContainerInventory == null)
                            {
                                foreach (var item in currentContainer.ConsumablesInventory)
                                {
                                    if (item.StackCount == 0)
                                    {
                                        Console.WriteLine($"{currentContainer.LongName} contains {item.Name}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"{currentContainer.LongName} contains {item.StackCount} of item: {item.Name}");
                                    }
                                }
                            }
                            else
                            {
                                foreach (var item in currentContainer.ContainerInventory)
                                {
                                    if (item.StackCount == 0)
                                    {
                                        Console.WriteLine($"{currentContainer.LongName} contains {item.Name}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"{currentContainer.LongName} contains {item.StackCount} of item: {item.Name}");
                                    }
                                }
                            }
                        }
                        catch (ArgumentNullException) //test catch here and return safely. Crashing != option here -
                                                      //Simply tell the player/reader there is no sodding container of that type here.
                        {
                            Console.WriteLine("There is no container of that type here");
                        }
                    }
                    else if (Object.Container.CurrentContainer != null)
                    {
                        Object.Container.Container currentContainer = Object.Container.CurrentContainer;
                        if (currentContainer == null)
                        {
                            Console.WriteLine("There is no container of that type here");
                        }
                        else
                        {
                            foreach (var item in currentContainer.ContainerInventory)
                            {
                                if (item.StackCount == 0)
                                {
                                    Console.WriteLine($"{currentContainer.LongName} contains {item.Name}");
                                }
                                else
                                {
                                    Console.WriteLine($"{currentContainer.LongName} contains {item.StackCount} of item: {item.Name}");
                                }
                            }
                        }
                    }
                }

            } //end container handler

            //Moved here from Location.cs
            public static void GetLocationInventory()
            {
                if (Location.CurrentLoc == null)
                {
                    Location currentLoc = Scene.Scene1.SceneLocations.Where(x => x.IsCurrentLocation == true).FirstOrDefault();
                    if (currentLoc == null)
                    {
                        //Handle the error condition gracefully, thus avoiding exceptions and crashes.
                        Console.WriteLine("I see absolutely nothing.");
                    }
                    else
                    {
                        if (currentLoc.LocationInventory != null)
                        {
                            foreach (var InventoryObject in currentLoc.LocationInventory)
                            {
                                if (currentLoc.LocationInventory.Count > 0)
                                    Console.WriteLine($"{currentLoc.Name} contains: {InventoryObject.Name}");
                                else
                                    Console.WriteLine("There is nothing here.");
                            }
                        }
                        //Handle the error condition gracefully, thus avoiding exceptions and crashes.
                        else if (currentLoc.LocationInventory == null)
                        {
                            Console.WriteLine("There is no storage space here.");
                        }
                    }
                }
                else if (Location.CurrentLoc != null)
                {
                    Location currentLoc = Location.CurrentLoc;
                    if (currentLoc == null)
                    {
                        //Handle the error condition gracefully, thus avoiding exceptions and crashes.
                        Console.WriteLine("I see absolutely nothing.");
                    }
                    else
                    {
                        if (currentLoc.LocationInventory != null)
                        {
                            foreach (var InventoryObject in currentLoc.LocationInventory)
                            {
                                if (currentLoc.LocationInventory.Count > 0)
                                    Console.WriteLine($"{currentLoc.Name} contains: {InventoryObject.Name}");
                                else
                                    Console.WriteLine("There is nothing here.");
                            }
                        }
                        //Handle the error condition gracefully, thus avoiding exceptions and crashes.
                        else if (currentLoc.LocationInventory == null)
                        {
                            Console.WriteLine("There is no storage space here.");
                        }
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
                if (Location.CurrentLoc == null)
                {
                    Location currentLoc = Scene.Scene1.SceneLocations.Where(x => x.IsCurrentLocation == true).FirstOrDefault();
                    if (currentLoc == null)
                    {
                        //Handle the error condition gracefully, thus avoiding exceptions and crashes.
                        Console.WriteLine($"I am nowhere yet.");
                    }
                    else
                    {
                        Console.WriteLine($"I am in {currentLoc.Name}");
                    }
                }
                else if (Location.CurrentLoc != null)
                {
                    Location currentLoc = Location.CurrentLoc;
                    if (currentLoc == null)
                    {
                        //Handle the error condition gracefully, thus avoiding exceptions and crashes.
                        Console.WriteLine($"I am nowhere yet.");
                    }
                    else
                    {
                        Console.WriteLine($"I am in {currentLoc.Name}");
                    }
                }
            }
            public static void GoToNewLocation(Location targetLoc)
            {
            }
            public static void GoToNewLocation()
            {
                if (Location.CurrentLoc == null)
                {
                    Location currentLoc = Scene.Scene1.SceneLocations.Where(x => x.IsCurrentLocation == true).FirstOrDefault();
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
                }
                else if (Location.CurrentLoc != null)
                {
                    Location currentLoc = Location.CurrentLoc;
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
                }
            }//End location handler
        }

    }
}