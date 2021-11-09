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
        public List<InventoryEntry> InventoryEntries = new();
        public void AddItem(PickuppableObject inventoryItem, int AddableAmount)
        {
            while (AddableAmount > 0)
            {
                if (InventoryEntries.Exists(x => (x.InventoryObject.ObjectID == inventoryItem.ObjectID) && (x.Amount < inventoryItem.MaxStackCount)))
                {
                    InventoryEntry inventoryEntry = InventoryEntries.First(x => (x.InventoryObject.ObjectID == inventoryItem.ObjectID) && (x.Amount < inventoryItem.MaxStackCount));
                    int MaximumAddable = inventoryItem.MaxStackCount - inventoryEntry.Amount;
                    int AddAmount = Math.Min(inventoryItem.MaxStackCount, AddableAmount);
                    inventoryEntry.Amount += AddableAmount;
                    AddableAmount -= AddAmount;
                }
                else
                {
                    if (InventoryEntries.Count < InvMaxSlots)
                    {
                        InventoryEntries.Add(new InventoryEntry(inventoryItem, 0));
                    }
                    else
                    {
                        Console.WriteLine("There is no more inventory space");
                    }
                }
            }
        }
        public void RemoveItem(PickuppableObject item, int RemovableAmount)
        {
            while (RemovableAmount < 0)
            {
                if (InventoryEntries.Exists(x => (x.InventoryObject.ObjectID == item.ObjectID) && (x.Amount > 0)))
                {
                    InventoryEntry inventoryEntry = InventoryEntries.Last(x => (x.InventoryObject.ObjectID == item.ObjectID) && (x.Amount > 0));
                    int AmountRemovable = Math.Max(RemovableAmount, inventoryEntry.Amount);
                    inventoryEntry.Amount -= AmountRemovable;
                    RemovableAmount -= AmountRemovable;
                    if (inventoryEntry.Amount == 0)
                    {
                        RemoveEntry();
                    }
                }
                else
                {
                    Console.WriteLine("There is no more inventory space");
                    break;
                }
            }
        }
        void RemoveEntry()
        {
            int InventoryEntryNum = InventoryEntries.Count - 1;
            if (InventoryEntries.Count > 0)
            {
                InventoryEntries.RemoveAt(InventoryEntryNum);
            }
            else
            {
                Console.WriteLine("Inventory is empty.");
            }
        }
        public InventorySystem()
        {

        }
    }
    public class InventoryEntry
    {
        public PickuppableObject InventoryObject { get; set; }
        public int Amount { get; set; }
        public InventoryEntry(PickuppableObject item, int amount)
        {
            InventoryObject = item;
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
    public class PickuppableObject
    {
        public Guid ObjectID { get; set; }
        public string Name { get; set; }
        public int MaxStackCount { get; set; }

        public PickuppableObject()
        {
            MaxStackCount = 1;
        }
        public class Potion : PickuppableObject
        {
            public Potion()
            {
                MaxStackCount = 10;
            }
        }
        public class Weapon : PickuppableObject
        {

        }
        public class Ammo : PickuppableObject
        {
            public Ammo()
            {
                MaxStackCount = 50;
            }
        }
    }
}
