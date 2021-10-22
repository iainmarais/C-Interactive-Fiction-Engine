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
        public void AddItem(PickuppableObject item, int AddToStack)
        {
            while (AddToStack > 0)
            {
                if (InventoryEntries.Exists(x => (x.InvObject.ID == item.ID) && (x.Amount < item.MaxStackCount)))
                {
                    InventoryEntry inventoryEntry = InventoryEntries.First(x => (x.InvObject.ID == item.ID) && (x.Amount < item.MaxStackCount));
                    int MaxAddable = (item.MaxStackCount - inventoryEntry.Amount);
                    int AddStackCount = Math.Min(item.MaxStackCount, inventoryEntry.Amount);
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
        public InventorySystem()
        {

        }
    }
    public class InventoryEntry
    {
        public PickuppableObject InvObject { get; set; }
        public int Amount { get; set; }
        public InventoryEntry(PickuppableObject item, int amount)
        {
            InvObject = item;
            Amount = amount;
        }
        public void AddToAmount(int amountToAdd)
        {
            Amount += amountToAdd;
        }
    }
    public class PickuppableObject
    {
        public Guid ID { get; set; }
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
