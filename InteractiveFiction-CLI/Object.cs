/*
Interactive Fiction Command line interpreter
Object.cs

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
    partial class Program
    {
        //Top level class for Objects.
        public class Object
        {
            public int StackCount { get; set; }
            public string LongName { get; set; }
            public string Name { get; set; }
            public class Container : Object
            {
                public static Container CurrentContainer { get; set; }
                public static Container TargetContainer { get; set; }
                public List<Object> ContainerInventory { get; set; }
                public List<Object.Consumable> ConsumablesInventory { get; set; }
                public Container()
                {

                }
                public Container(string containerName)
                {
                    Name = containerName;
                }
                public Container(string containerName, string longName)
                {
                    Name = containerName;
                    LongName = longName;
                }
                public Container(string containerName, string longName, List<Object> containerInventory)
                {
                    Name = containerName;
                    LongName = longName;
                    ContainerInventory = containerInventory;
                }
                public Container(string containerName, string longName, List<Object.Consumable> consumablesInventory)
                {
                    Name = containerName;
                    LongName = longName;
                    ConsumablesInventory = consumablesInventory;
                }

                //This function handles querying the container inventory in each Loc. 
                //It gets the Loc from the instantiated objects of said type,
                //and stores them in a static CurrentLoc and uses that to handle the active instance.

                public static void GetContainerInventory()
                {
                    if (Location.CurrentLoc != null)
                    {
                        Location currentLoc = Location.CurrentLoc;
                        if (CurrentContainer == null)
                        {
                            try
                            {
                                Container currentContainer = (Container)currentLoc.LocationInventory.Where(x => x.Name == CommandProcessor.Command.Word2).FirstOrDefault();
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
                        else if (CurrentContainer != null)
                        {
                            Container currentContainer = CurrentContainer;
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
                        if (CurrentContainer == null)
                        {
                            try
                            {
                                Container currentContainer = (Container)currentLoc.LocationInventory.Where(x => x.Name == CommandProcessor.Command.Word2).FirstOrDefault();
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
                        else if (CurrentContainer != null)
                        {
                            Container currentContainer = CurrentContainer;
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

                }

            }
            public Object()
            {

            }
            public Object(string objectName)
            {
                Name = objectName;
            }
            public Object(string objectName, string longName)
            {
                Name = objectName;
                LongName = longName;
            }
            //Root class for consumables, it inherits traits from the Object class
            public class Consumable : Object
            {
                public bool IsConsumable = true;

                //Root class for Potions (aka drinks), inherits traits from the consumable class
                public class Potion : Consumable
                {
                    public bool IsPotion { get; set; }
                    public Potion()
                    {

                    }
                    public Potion(string potionName)
                    {
                        Name = potionName;
                    }
                    public Potion(string potionName, int stackCount)
                    {
                        Name = potionName;
                        StackCount = stackCount;
                    }
                }
                public Consumable()
                {

                }
                public Consumable(string consumableName)
                {
                    Name = consumableName;
                }
                public Consumable(string consumableName, int stackCount)
                {
                    Name = consumableName;
                    StackCount = stackCount;
                }
            }

        }
    }
}

