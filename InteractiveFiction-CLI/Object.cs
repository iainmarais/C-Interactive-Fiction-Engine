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

    public class InventorySystem
    {
        private const int InvMaxSlots = 15;
        private const int InvMinSlots = 1;
        public readonly List<InventoryEntry> InventoryEntries = new();
        public void AddItem(Object.PickuppableObject item, int AddToStack)
        {
            while (AddToStack > 0)
            {
                if (InventoryEntries.Exists(x => (x.InvObject.ID == item.ID) && (x.Amount < item.MaxStackCount)))
                {
                    InventoryEntry inventoryEntry = InventoryEntries.First(x => (x.InvObject.ID == item.ID) && (x.Amount < item.MaxStackCount));
                    int MaxAddable = (item.MaxStackCount - inventoryEntry.Amount);
                    int AddStackCount = Math.Min(AddToStack, item.MaxStackCount);
                    inventoryEntry.AddToAmount(AddStackCount);
                    AddToStack -= AddStackCount;
                }
                else
                {
                    if (InventoryEntries.Count < InvMaxSlots)
                    {
                        InventoryEntries.Add(new InventoryEntry(item, 0));
                    }
                    else
                    {
                        Console.WriteLine("There is no more inventory space");
                    }
                }
            }
        }
        public void RemoveItem(Object.PickuppableObject item, int RemoveFromStack)
        {
            while (RemoveFromStack > 0)
            {
                if (InventoryEntries.Exists(x => (x.InvObject.ID == item.ID) && (x.Amount > 0)))
                {
                    InventoryEntry inventoryEntry = InventoryEntries.First(x => (x.InvObject.ID == item.ID) && (x.Amount >= item.MinStackCount));
                    int NumLeft = (item.MinStackCount + inventoryEntry.Amount);
                    int SubtractStackCount = Math.Min(RemoveFromStack, inventoryEntry.Amount);
                    inventoryEntry.SubtractFromAmount(SubtractStackCount);
                    RemoveFromStack -= SubtractStackCount;
                }
                else
                {
                    if (InventoryEntries.Count > InvMinSlots)
                    {
                        InventoryEntries.Remove(new InventoryEntry(item, item.MinStackCount));
                    }
                    else if (InventoryEntries.Count == InvMinSlots)
                    {
                        Console.WriteLine($"You have no more left of: {item.Name} ");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("There is no more inventory space");
                    }
                }
            }
        }
        public InventorySystem()
        {

        }
    }
    public class InventoryEntry
    {
        public Object.PickuppableObject InvObject { get; private set; }
        public int Amount { get; private set; }
        public InventoryEntry(Object.PickuppableObject item, int amount)
        {
            InvObject = item;
            Amount = amount;
        }
        public void AddToAmount(int amountToAdd)
        {
            Amount += amountToAdd;
        }
        public void SubtractFromAmount(int amountToRemove)
        {
            Amount -= amountToRemove;
        }
    }
    //Top level class for Objects.
    public class Object
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

            //This function handles querying the container inventory in each Loc. 
            //It gets the Loc from the instantiated objects of said type,
            //and stores them in a static CurrentLoc and uses that to handle the active instance.
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

        public class PickuppableObject : Object
        {
            public bool IsAmmo { get; set; }
            public bool IsConsumable { get; set; }
            public int MaxStackCount { get; set; }
            public bool IsWeapon { get; set; }
            public int MinStackCount { get; set; }

            public PickuppableObject()
            {
                MinStackCount = 1;
                MaxStackCount = 1;
            }
            public PickuppableObject(string objectName)
            {
                MinStackCount = 1;
                MaxStackCount = 1;
                Name = objectName;
            }

            public class Weapon : PickuppableObject
            {
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
                    MaxStackCount = 1;
                    MinStackCount = 1;
                }
                public Weapon(string weaponName, string longName)
                {

                    Name = weaponName;
                    LongName = longName;
                    IsWeapon = true;
                    MaxStackCount = 1;
                    MinStackCount = 1;
                }
            }

            public class Ammo : PickuppableObject
            {
                public Ammo()
                {
                    MinStackCount = 1;
                    MaxStackCount = 50;
                    IsAmmo = true;
                }
                public Ammo(string ammoName)
                {
                    Name = ammoName;
                    MinStackCount = 1;
                    MaxStackCount = 50;
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
                        IsConsumable = true;
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
                    public Potion(string potionName, bool isConsumable)
                    {
                        Name = potionName;
                        IsConsumable = isConsumable;
                    }
                }
                public Consumable()
                {
                    MaxStackCount = 10;
                    IsConsumable = true;
                }
                public Consumable(string consumableName)
                {
                    Name = consumableName;
                    IsConsumable = true;
                }
                public Consumable(string consumableName, int stackCount)
                {
                    Name = consumableName;
                    StackCount = stackCount;
                    IsConsumable = true;
                }
                public Consumable(string potionName, bool isConsumable)
                {
                    Name = potionName;
                    IsConsumable = isConsumable;
                    IsConsumable = true;
                }
            }
        }

    }
}


