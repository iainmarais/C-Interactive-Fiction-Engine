/*
Interactive Fiction Command line interpreter
Location.cs

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

    public class Location
    {
        public List<Location> Locations { get; set; }
        public LocID LocationID { get; set; }
        public bool IsCurrentLocation { get; set; }
        public bool HasExitN { get; set; }
        public bool HasExitS { get; set; }
        public bool HasExitE { get; set; }
        public bool HasExitW { get; set; }
        public bool HasExitUp { get; set; }
        public bool HasExitDown { get; set; }
        public string Name { get; set; }
        public string LongName { get; set; }
        public List<Actor> LocationActors { get; set; }
        public List<Object> LocationInventory { get; set; }
        public static Location CurrentLoc { get; set; }
        public static Location TargetLoc { get; set; }
        public static bool IsConnected { get; set; }
        public bool LocIsConnected { get; set; }
        public Scene GetCurrentScene()
        {
            //Test:
            //Console.WriteLine("Location.GetCurrentScene entered");
            Scene myScene = new();
            myScene = myScene.QueryScene(myScene);
            return myScene;
        }
        public Location GetIsCurrentLoc()
        {
            //Test:
            //Console.WriteLine("Location.GetIsCurrentLoc entered");
            Location myLoc = new();
            Scene myScene = GetCurrentScene();
            myLoc = myScene.Locations.Where(x => x.IsCurrentLocation).FirstOrDefault();
            //Console.WriteLine($"Variable myLoc, value Name is set to {myLoc.Name}");
            return myLoc;
        }

        public Location GetIsCurrentLocation(Location Loc1)
        {
            CurrentLoc = Loc1;
            if (IsCurrentLocation == true)
            {
                CurrentLoc = this;
            }
            else
            {
                CurrentLoc = null;
            }
            return CurrentLoc;
        }
        public static bool GetIsConnected(Location Loc1, Location Loc2)
        {
            try
            {
                if (Loc1.HasExitN && Loc2.HasExitS)
                {
                    IsConnected = true;
                }
                else if (Loc1.HasExitS && Loc2.HasExitN)
                {
                    IsConnected = true;
                }
                else if (Loc1.HasExitE && Loc2.HasExitW)
                {
                    IsConnected = true;
                }
                else if (Loc1.HasExitW && Loc2.HasExitE)
                {
                    IsConnected = true;
                }
                else if (Loc1.HasExitUp && Loc2.HasExitDown)
                {
                    IsConnected = true;
                }
                else if (Loc1.HasExitDown && Loc2.HasExitUp)
                {
                    IsConnected = true;
                }
                else
                {
                    IsConnected = false;
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Could not recognise that, please try again");
            }
            return IsConnected;
        }
        public Location()
        {

        }
        public Location(string locName)
        {
            Name = locName;
        }
        public Location(string locName, LocID locId)
        {
            LocationID = locId;
            Name = locName;
        }
        public Location(string locName, string longName, LocID locId) //Constructor for 3 params
        {
            Name = locName;
            LongName = longName;
            LocationID = locId;
        }
        public Location(string locName, string longName, LocID locId, bool exitN, bool exitS, bool exitE, bool exitW, bool exitUp, bool exitDown, List<Object> locInventory, bool isCurrentLoc)
        {
            Name = locName;
            LongName = longName;
            LocationID = locId;
            HasExitN = exitN;
            HasExitS = exitS;
            HasExitE = exitE;
            HasExitW = exitW;
            HasExitUp = exitUp;
            HasExitDown = exitDown;
            LocationInventory = locInventory;
            IsCurrentLocation = isCurrentLoc;
        }
        public Location(string locName, string longName, LocID locId, bool exitN, bool exitS, bool exitE, bool exitW, bool exitUp, bool exitDown, List<Object> locInventory, bool isCurrentLoc, List<Actor> locActors)
        {
            Name = locName;
            LongName = longName;
            LocationID = locId;
            HasExitN = exitN;
            HasExitS = exitS;
            HasExitE = exitE;
            HasExitW = exitW;
            HasExitUp = exitUp;
            HasExitDown = exitDown;
            LocationInventory = locInventory;
            IsCurrentLocation = isCurrentLoc;
            LocationActors = locActors;
        }

    }

}