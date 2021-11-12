using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveFiction_CLI
{
    public class InventorySystem
    {
        private const int MaxInvSlots = 15;
        public List<InventoryEntry> InventoryEntries = new();
        public void AddItem(Object.PickuppableObject inventoryItem, int AddableAmount)
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
                    if (InventoryEntries.Count < MaxInvSlots)
                    {
                        InventoryEntries.Add(new InventoryEntry(inventoryItem, 0));
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"There is no more inventory space!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
            }
        }
        public void RemoveItem(Object.PickuppableObject item, int RemovableAmount)
        {
            while (RemovableAmount > 0)
            {
                if (InventoryEntries.Exists(x => (x.InventoryObject.ObjectID == item.ObjectID) && (x.Amount > 0)))
                {
                    InventoryEntry inventoryEntry = InventoryEntries.Last(x => (x.InventoryObject.ObjectID == item.ObjectID) && (x.Amount > 0));
                    int AmountRemovable = Math.Min(RemovableAmount, inventoryEntry.Amount);
                    inventoryEntry.Amount -= AmountRemovable;
                    RemovableAmount -= AmountRemovable;
                    if (inventoryEntry.Amount == 0)
                    {
                        RemoveEntry();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"You have no more of {item.Name} in your inventory.");
                    Console.ForegroundColor = ConsoleColor.Gray;
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
        public Object.PickuppableObject InventoryObject { get; set; }
        public int Amount { get; set; }
        public InventoryEntry(Object.PickuppableObject item, int amount)
        {
            InventoryObject = item;
            Amount = amount;
        }
    }
    public partial class Object
    {
        public partial class PickuppableObject : Object
        {
            public Guid ObjectID { get; set; }
        }
    }
}
