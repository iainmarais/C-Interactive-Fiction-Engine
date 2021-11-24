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

    //public class InventorySystem
    //{
    //    private const int InvMaxSlots = 15;
    //    private const int InvMinSlots = 1;
    //    public readonly List<InventoryEntry> InventoryEntries = new();
    //    public void AddItem(Object.PickuppableObject item, int AddToStack)
    //    {
    //        while (AddToStack > 0)
    //        {
    //            if (InventoryEntries.Exists(x => (x.InvObject.ID == item.ID) && (x.Amount < item.MaxStackCount)))
    //            {
    //                InventoryEntry inventoryEntry = InventoryEntries.First(x => (x.InvObject.ID == item.ID) && (x.Amount < item.MaxStackCount));
    //                int MaxAddable = (item.MaxStackCount - inventoryEntry.Amount);
    //                int AddStackCount = Math.Min(AddToStack, item.MaxStackCount);
    //                inventoryEntry.AddToAmount(AddStackCount);
    //                AddToStack -= AddStackCount;
    //            }
    //            else
    //            {
    //                if (InventoryEntries.Count < InvMaxSlots)
    //                {
    //                    InventoryEntries.Add(new InventoryEntry(item, 0));
    //                }
    //                else
    //                {
    //                    Console.WriteLine("There is no more inventory space");
    //                }
    //            }
    //        }
    //    }
    //    public void RemoveItem(Object.PickuppableObject item, int RemoveFromStack)
    //    {
    //        while (RemoveFromStack > 0)
    //        {
    //            if (InventoryEntries.Exists(x => (x.InvObject.ID == item.ID) && (x.Amount > 0)))
    //            {
    //                InventoryEntry inventoryEntry = InventoryEntries.First(x => (x.InvObject.ID == item.ID) && (x.Amount >= item.MinStackCount));
    //                int NumLeft = (item.MinStackCount + inventoryEntry.Amount);
    //                int SubtractStackCount = Math.Min(RemoveFromStack, inventoryEntry.Amount);
    //                inventoryEntry.SubtractFromAmount(SubtractStackCount);
    //                RemoveFromStack -= SubtractStackCount;
    //            }
    //            else
    //            {
    //                if (InventoryEntries.Count > InvMinSlots)
    //                {
    //                    InventoryEntries.Remove(new InventoryEntry(item, item.MinStackCount));
    //                }
    //                else if (InventoryEntries.Count == InvMinSlots)
    //                {
    //                    Console.WriteLine($"You have no more left of: {item.Name} ");
    //                    break;
    //                }
    //                else
    //                {
    //                    Console.WriteLine("There is no more inventory space");
    //                }
    //            }
    //        }
    //    }
    //    public InventorySystem()
    //    {

    //    }
    //}
    //public class InventoryEntry
    //{
    //    public Object.PickuppableObject InvObject { get; private set; }
    //    public int Amount { get; private set; }
    //    public InventoryEntry(Object.PickuppableObject item, int amount)
    //    {
    //        InvObject = item;
    //        Amount = amount;
    //    }
    //    public void AddToAmount(int amountToAdd)
    //    {
    //        Amount += amountToAdd;
    //    }
    //    public void SubtractFromAmount(int amountToRemove)
    //    {
    //        Amount -= amountToRemove;
    //    }
    //}
    //Top level class for Objects.
    public partial class Object
    {
        public bool HasStackCount { get; set; }
        public int StackCount { get; set; }
        public string LongName { get; set; }
        public string Name { get; set; }
        public Guid ID { get; set; }
        public static string ObjectName { get; set; }
        public static string ConsumableName { get; set; }
        public static string ContainerName { get; set; }
        public static Object CurrentObject { get; set; }
        public static Object TargetObject { get; set; }
        public class Container : Object
        {
            public bool IsLocked { get; set; }
            public bool IsOpen { get; set; }
            public static Container CurrentContainer { get; set; }
            public static Container TargetContainer { get; set; }
            public List<Object> ContainerInventory { get; set; }
            public List<PickuppableObject.Consumable> ConsumablesInventory { get; set; }
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
            public Container(string containerName, string longName, List<Object> containerInventory, bool isOpen, bool isLocked)
            {
                Name = containerName;
                LongName = longName;
                ContainerInventory = containerInventory;
                IsOpen = isOpen;
                IsLocked = isLocked;
            }
            public Container(string containerName, string longName, List<PickuppableObject.Consumable> consumablesInventory)
            {
                Name = containerName;
                LongName = longName;
                ConsumablesInventory = consumablesInventory;
            }
            public Container(string containerName, string longName, List<PickuppableObject.Consumable> consumablesInventory, bool isOpen, bool isLocked)
            {
                Name = containerName;
                LongName = longName;
                ConsumablesInventory = consumablesInventory;
                IsOpen = isOpen;
                IsLocked = isLocked;
            }
            public void GetContainerInventory(string containerName)
            {
                Location currentLoc = new();
                currentLoc = currentLoc.GetIsCurrentLoc();
                var myContainer = (Container)currentLoc.LocationInventory.Where(x => x.Name == containerName).FirstOrDefault();
                try
                {
                    if (myContainer == null)
                    {
                        Console.WriteLine("There is no container of that type here");
                    }
                    else if (myContainer.ConsumablesInventory != null)
                    {
                        {
                            foreach (var item in myContainer.ConsumablesInventory)
                            {
                                if (item.StackCount == 0)
                                {
                                    Console.WriteLine($"{myContainer.LongName} contains {item.LongName}");
                                }
                                else
                                {
                                    Console.WriteLine($"{myContainer.LongName} contains {myContainer.ConsumablesInventory.Count} of item: {item.LongName}");
                                }
                            }
                        }
                    }
                    else if (myContainer.ContainerInventory != null)
                    {
                        {
                            foreach (var item in myContainer.ContainerInventory)
                            {
                                if (item.StackCount == 0)
                                {
                                    Console.WriteLine($"{myContainer.LongName} contains {item.LongName}");
                                }
                                else
                                {
                                    Console.WriteLine($"{myContainer.LongName} contains {myContainer.ConsumablesInventory.Count} of item: {item.LongName}");
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
            //This function handles querying the container inventory in each Loc. 
            //It gets the Loc from the instantiated objects of said type,
            //and stores them in a static CurrentLoc and uses that to handle the active instance.
        }
        //Class for surface container objects, such as tables that can have other objects standing on top of them. 
        //Surface containers can not be locked, opened or closed.
        public class SurfaceContainer : Object
        {
            public List<Object> StaticSurfaceObjects { get; set; }
            public List<PickuppableObject> NonStaticSurfaceObjects { get; set; }
            public List<PickuppableObject.Consumable> Consumables { get; set; }
            public SurfaceContainer()
            {

            }
            public SurfaceContainer(string containerName, string longName, List<Object> staticObjs)
            {
                Name = containerName;
                LongName = longName;
                StaticSurfaceObjects = staticObjs;
            }
            public SurfaceContainer(string containerName, string longName, List<Object> staticObjs, List<Object.PickuppableObject> nonStaticObjs, List<Object.PickuppableObject.Consumable> consumables)
            {
                Name = containerName;
                LongName = longName;
                StaticSurfaceObjects = staticObjs;
                NonStaticSurfaceObjects = nonStaticObjs;
                Consumables = consumables;
            }
            public SurfaceContainer(string containerName, string longName, List<Object> staticObjs, List<Object.PickuppableObject> nonStaticObjs)
            {
                Name = containerName;
                LongName = longName;
                StaticSurfaceObjects = staticObjs;
                NonStaticSurfaceObjects = nonStaticObjs;
            }

            public void GetSurfaceContainerObjects(string containerName)
            {
                Location currentLoc = new();
                currentLoc = currentLoc.GetIsCurrentLoc();
                var myContainer = (SurfaceContainer)currentLoc.LocationInventory.Where(x => x.Name == containerName).FirstOrDefault();
                try
                {
                    if (myContainer == null)
                    {
                        Console.WriteLine("There is no container of that type here");
                    }
                    if (myContainer.NonStaticSurfaceObjects != null)
                    {
                        Console.Write("I see: ");
                        foreach (var Object in myContainer.NonStaticSurfaceObjects)
                        {
                            Console.Write($"{Object.LongName} ");
                        }
                        Console.Write($"on the {myContainer.LongName}. \n");
                    }
                    if (myContainer.StaticSurfaceObjects != null)
                    {
                        Console.Write("I see: ");
                        foreach (var Object in myContainer.StaticSurfaceObjects)
                        {
                            Console.Write($"{Object.LongName} ");
                        }
                        Console.Write($"on the {myContainer.LongName}. \n");
                    }
                    if (myContainer.Consumables != null)
                    {
                        Console.Write("I see: ");
                        foreach (var Object in myContainer.Consumables)
                        {
                            Console.Write($"{Object.LongName} ");
                        }
                        Console.Write($"on the {myContainer.LongName}. \n");
                    }
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("There is no container of that type here");
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine($"That container does not exist in {currentLoc.LongName}");
                }
            }

        }
        //Test: Disable static loc handler
        //Convert to objectified version
        public void PutObject(string containerName, string objectName)
        {
            Location currentLoc = new();
            currentLoc = currentLoc.GetIsCurrentLoc();
            var myContainer = (Container)currentLoc.LocationInventory.Where(x => x.Name == containerName).FirstOrDefault();

            try
            {

                if (myContainer.ContainerInventory != null)
                {
                    var myObject = (PickuppableObject)myContainer.ContainerInventory.Where(x => x.Name == objectName).FirstOrDefault();
                    if (Logic.InvSys.InventoryEntries.Exists(x => x.Amount > 0))
                    {
                        Logic.InvSys.RemoveItem(myObject, 1);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"You put the {myObject.Name} in the {myContainer.Name}");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        myContainer.ContainerInventory.Add(myObject);
                    }
                    else
                    {
                        Console.WriteLine($"You don't have the {myObject.Name} in your inventory");
                    }
                }
                else if (myContainer.ConsumablesInventory != null)
                {
                    var myObject = (PickuppableObject.Consumable)myContainer.ConsumablesInventory.Where(x => x.Name == objectName).FirstOrDefault();
                    //explicitly check inventory for said object.
                    //Can help to avoid NRE's since inventory entries are cleared when empty. 
                    //Also avoids duplicated container and/or consumable inventory items
                    if (Logic.InvSys.InventoryEntries.Exists(x => x.Amount > 0))
                    {
                        Logic.InvSys.RemoveItem(myObject, 1);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"You put a {myObject.Name} in the {myContainer.Name}");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        myContainer.ConsumablesInventory.Add(myObject);
                    }
                    else
                    {
                        Console.WriteLine($"You don't have any of {myObject.Name} in your inventory");
                    }
                }
                else
                {
                    Console.WriteLine($"{myContainer.LongName} is empty or is not a container");
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("No object of that type exists yet");
            }
        }
        public void GetObject(string containerName, string objectName)
        {
            Location currentLoc = new();
            currentLoc = currentLoc.GetIsCurrentLoc();
            var myContainer = (Container)currentLoc.LocationInventory.Where(x => x.Name == containerName).FirstOrDefault();
            try
            {

                if (myContainer.ContainerInventory != null)
                {
                    var myObject = (PickuppableObject)myContainer.ContainerInventory.Where(x => x.Name == objectName).FirstOrDefault();
                    Logic.InvSys.AddItem(myObject, 1);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"You get a {myObject.Name} from the {myContainer.Name}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    myContainer.ContainerInventory.Remove(myObject);

                }
                else if (myContainer.ConsumablesInventory != null)
                {
                    var myObject = (PickuppableObject.Consumable)myContainer.ConsumablesInventory.Where(x => x.Name == objectName).FirstOrDefault();
                    Logic.InvSys.AddItem(myObject, 1);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"You get the {myObject.Name} from the {myContainer.Name}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    myContainer.ConsumablesInventory.Remove(myObject);

                }
                else
                {
                    Console.WriteLine("There is no more space to put anything.");
                }

            }
            catch (NullReferenceException)
            {
                Console.WriteLine("No object of that type exists yet");
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

        //Copied over from InvSys file.

        public partial class PickuppableObject : Object
        {
            public bool IsAmmo { get; set; }
            public bool IsConsumable { get; set; }
            public int MaxStackCount { get; set; }
            public bool IsWeapon { get; set; }
            public int MinStackCount { get; set; }

            public PickuppableObject()
            {
                MinStackCount = 1;
            }
            public PickuppableObject(string objectName)
            {
                MinStackCount = 1;
                Name = objectName;
            }
            public PickuppableObject(string objectName, string longName)
            {
                MinStackCount = 1;
                Name = objectName;
                LongName = longName;
            }

            public class Weapon : PickuppableObject
            {
                public int MinDmg { get; set; }
                public int MaxDmg { get; set; }
                public bool IsNonLethal { get; set; }
                public bool IsRanged { get; set; }
                public Weapon()
                {
                    IsWeapon = true;
                    MinStackCount = 1;
                    MaxStackCount = 1;
                }
                public Weapon(string weaponName)
                {
                    Name = weaponName;
                    IsWeapon = true;
                    MinStackCount = 1;
                    MaxStackCount = 1;
                }
                public Weapon(string weaponName, string longName)
                {

                    Name = weaponName;
                    LongName = longName;
                    IsWeapon = true;
                    MinStackCount = 1;
                    MaxStackCount = 1;
                }
                public Weapon(string weaponName, string longName, int minDmg, int maxDmg, bool isNonLethal, bool isRanged)
                {

                    Name = weaponName;
                    LongName = longName;
                    IsWeapon = true;
                    MinStackCount = 1;
                    MaxStackCount = 1;
                    MinDmg = minDmg;
                    MaxDmg = maxDmg;
                    IsNonLethal = isNonLethal;
                    IsRanged = isRanged;
                }
            }

            public class Ammo : PickuppableObject
            {
                public Ammo()
                {
                    MaxStackCount = 50;
                    MinStackCount = 1;
                    IsAmmo = true;
                }
                public Ammo(string ammoName)
                {
                    Name = ammoName;
                    MaxStackCount = 50;
                    MinStackCount = 1;
                    IsAmmo = true;
                }
            }
            public class Consumable : PickuppableObject
            {

                //Root class for Potions (aka drinks), inherits traits from the consumable class
                public class Potion : Consumable
                {
                    public bool IsPotion { get; set; }
                    public Potion()
                    {
                        MinStackCount = 1;
                        MaxStackCount = 10;
                        IsConsumable = true;
                    }
                    public Potion(string potionName)
                    {
                        Name = potionName;
                        MinStackCount = 1;
                        MaxStackCount = 10;
                        IsConsumable = true;
                    }
                    public Potion(string potionName, string longName)
                    {
                        Name = potionName;
                        LongName = longName;
                        MinStackCount = 1;
                        MaxStackCount = 10;
                        IsConsumable = true;
                    }
                }
                public Consumable()
                {
                    IsConsumable = true;
                }
                public Consumable(string consumableName)
                {
                    Name = consumableName;
                    IsConsumable = true;
                }
                public Consumable(string consumableName, string longName)
                {
                    Name = consumableName;
                    LongName = longName;
                    IsConsumable = true;
                }
            }
            public class PlayerTool : PickuppableObject
            {
                public PlayerTool()
                {
                    MinStackCount = 1;
                }
                public PlayerTool(string toolName, string longName)
                {
                    Name = toolName;
                    LongName = longName;
                }
            }
        }

    }
}


