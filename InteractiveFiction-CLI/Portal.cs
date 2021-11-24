using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveFiction_CLI
{
    public class LocationPortal
    {
        public string Direction { get; set; }
        public string PortalName { get; set; }
        public bool IsAccessible { get; set; }
        public bool HasDoor { get; set; }
        public bool PortalState { get; set; }
        public Door door { get; set; }
        public LocationPortal GetPortal()
        {
            return this;
        }
        public LocationPortal()
        {
        }
        public class Door
        {
            public string DoorName { get; set; }
            public bool IsLocked { get; set; }
            public bool IsClosed { get; set; }
            public Door()
            {
            }
            public Door GetDoor()
            {
                return this;
            }
            public void GetLockState()
            {
                if (IsLocked)
                {
                    Console.WriteLine("This door is locked.");
                }
                if (!IsLocked)
                {
                    Console.WriteLine("This door is unlocked and can be opened.");
                }
            }
            public void GetDoorState()
            {
                if (IsClosed)
                {
                    Console.WriteLine("This door is closed.");
                }
                if (!IsClosed)
                {
                    Console.WriteLine("This door is open.");
                }
            }
        }
    }
}
