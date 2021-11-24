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
        public LocationPortal NorthDoorway { get; set; }
        public LocationPortal SouthDoorway { get; set; }
        public LocationPortal EastDoorway { get; set; }
        public LocationPortal WestDoorway { get; set; }
        public LocationPortal StairwayUp { get; set; }
        public LocationPortal StairwayDown { get; set; }
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
        public List<string> AdjacentLocs { get; set; }
        public Scene GetCurrentScene()
        {
            //Test:
            //Console.WriteLine("Location.GetCurrentScene entered");
            List<Scene> MyScenes = new();
            Scene myScene = new();
            myScene = myScene.QueryScene(myScene, MyScenes);
            return myScene;
        }
        public Location GetIsCurrentLoc()
        {
            //Test:
            //Console.WriteLine("Location.GetIsCurrentLoc entered");
            Location myLoc = new();
            Scene myScene = GetCurrentScene();
            myLoc = myScene.Locations.Where(x => x.IsCurrentLocation).FirstOrDefault();
            if (myLoc == null)
            {
                Console.WriteLine("Variable myLoc is null or unset");
            }
            return myLoc;
        }
        public Location GetNewLoc()
        {
            Location NewLoc = new();
            string NewLocName = CommandProcessor.Command.Word4;
            NewLoc = GetCurrentScene().Locations.Where(x => x.Name == NewLocName).FirstOrDefault();
            return NewLoc;
        }
        public Location GetNewLoc(string NewLocName)
        {
            Location NewLoc = new();
            NewLoc = GetCurrentScene().Locations.Where(x => x.Name == NewLocName).FirstOrDefault();
            return NewLoc;
        }
        public Location ChangeLoc()
        {
            //Query Loc for new and current -> CurrentLoc: boolean variable IsCurrentLocation and NewLoc: string variable name, value from cmdproc->word2
            //Test: Console write -> function entered:
            //Console.WriteLine("Location.ChangeLoc entered");
            Location CurrentLoc = GetIsCurrentLoc();
            Location NewLoc = GetNewLoc();
            CheckConnection(CurrentLoc, NewLoc);
            if (LocIsConnected == true)
            {
                CurrentLoc.IsCurrentLocation = false;
                CurrentLoc = NewLoc;
                CurrentLoc.IsCurrentLocation = true;
                Console.WriteLine($"I am now in {CurrentLoc.LongName}");
            }
            else
            {
                Console.WriteLine("You can't go that way.");
            }
            return CurrentLoc;
        }
        public Location QueryLocByDir(string Direction)
        {
            Location CurrentLoc = GetIsCurrentLoc();
            int LocIndex = 0;
            Location TargetLoc = new();
            Location NewLoc = new();
            while (!CurrentLoc.CheckConnection(CurrentLoc, NewLoc))
            {
                TargetLoc = GetNewLoc(CurrentLoc.AdjacentLocs[LocIndex]);
                if (CurrentLoc.HasExitN && Direction == "north")
                {
                    if (TargetLoc.HasExitS == true)
                    {
                        NewLoc = TargetLoc;
                        break;
                    }
                    else
                    {
                        LocIndex++;
                    }
                }
                else if (CurrentLoc.HasExitS && Direction == "south")
                {
                    if (TargetLoc.HasExitN == true)
                    {
                        NewLoc = TargetLoc;
                        break;
                    }
                    else
                    {
                        LocIndex++;
                    }
                }
                else if (CurrentLoc.HasExitE && Direction == "east")
                {
                    if (TargetLoc.HasExitW == true)
                    {
                        NewLoc = TargetLoc;
                        break;
                    }
                    else
                    {
                        LocIndex++;
                    }
                }
                else if (CurrentLoc.HasExitW && Direction == "west")
                {
                    if (TargetLoc.HasExitE == true)
                    {
                        NewLoc = TargetLoc;
                        break;
                    }
                    else
                    {
                        LocIndex++;
                    }
                }
                else if (CurrentLoc.HasExitUp && Direction == "up")
                {
                    if (TargetLoc.HasExitDown == true)
                    {
                        NewLoc = TargetLoc;
                        break;
                    }
                    else
                    {
                        LocIndex++;
                    }
                }
                else if (CurrentLoc.HasExitDown && Direction == "down")
                {
                    if (TargetLoc.HasExitUp == true)
                    {
                        NewLoc = TargetLoc;
                        break;
                    }
                    else
                    {
                        LocIndex++;
                    }
                }
            }
            return NewLoc;
        }
        public Location ChangeLoc(string NewLocName)
        {
            //Query Loc for new and current -> CurrentLoc: boolean variable IsCurrentLocation and NewLoc: string variable name, value from cmdproc->word2
            Location CurrentLoc = GetIsCurrentLoc();
            Location NewLoc = GetNewLoc(NewLocName);
            CheckConnection(CurrentLoc, NewLoc);
            if (LocIsConnected == true)
            {
                CurrentLoc.IsCurrentLocation = false;
                CurrentLoc = NewLoc;
                CurrentLoc.IsCurrentLocation = true;
                Console.WriteLine($"I am now in {CurrentLoc.LongName}");
            }
            else
            {
                Console.WriteLine("You can't go that way.");
            }
            return CurrentLoc;
        }
        public bool CheckConnection(Location Loc1, Location Loc2)
        {
            //Test: console write -> Check connection entered:
            //Console.WriteLine("Location.CheckConnection entered");
            if (Loc1.HasExitN && Loc2.HasExitS && Loc1.AdjacentLocs.Contains(Loc2.Name))
                LocIsConnected = true;
            else if (Loc1.HasExitS && Loc2.HasExitN && Loc1.AdjacentLocs.Contains(Loc2.Name))
                LocIsConnected = true;
            else if (Loc1.HasExitE && Loc2.HasExitW && Loc1.AdjacentLocs.Contains(Loc2.Name))
                LocIsConnected = true;
            else if (Loc1.HasExitW && Loc2.HasExitE && Loc1.AdjacentLocs.Contains(Loc2.Name))
                LocIsConnected = true;
            else if (Loc1.HasExitUp && Loc2.HasExitDown && Loc1.AdjacentLocs.Contains(Loc2.Name))
                LocIsConnected = true;
            else if (Loc1.HasExitDown && Loc2.HasExitUp && Loc1.AdjacentLocs.Contains(Loc2.Name))
                LocIsConnected = true;
            else
                LocIsConnected = false;
            return LocIsConnected;
        }
        public void GetAvailableExits()
        {
            Location CurrentLoc = GetIsCurrentLoc();
            Location NewLoc = new();
            for (int LocIndex = 0; LocIndex < CurrentLoc.AdjacentLocs.Count; LocIndex++)
            {
                NewLoc = GetNewLoc(CurrentLoc.AdjacentLocs[LocIndex]);

                if (CurrentLoc.HasExitN)
                {
                    if (NewLoc.HasExitS)
                    {
                        Console.WriteLine($"There is an exit to the North leading to: {NewLoc.LongName}");
                    }
                    else if (NewLoc == null)
                    {
                        Console.WriteLine($"There is an exit to the North, but it is inaccessible.");
                    }
                }
                if (CurrentLoc.HasExitS)
                {
                    if (NewLoc.HasExitN)
                    {
                        Console.WriteLine($"There is an exit to the South leading to: {NewLoc.LongName}");
                    }
                    else if (NewLoc == null)
                    {
                        Console.WriteLine($"There is an exit to the South, but it is inaccessible.");
                    }
                }
                if (CurrentLoc.HasExitE)
                {
                    if (NewLoc.HasExitW)
                    {
                        Console.WriteLine($"There is an exit to the East leading to: {NewLoc.LongName}");
                    }
                    else if (NewLoc == null)
                    {
                        Console.WriteLine($"There is an exit to the East, but it is inaccessible.");
                    }
                }
                if (CurrentLoc.HasExitW)
                {
                    if (NewLoc.HasExitE)
                    {
                        Console.WriteLine($"There is an exit to the West leading to: {NewLoc.LongName}");
                    }
                    else if (NewLoc == null)
                    {
                        Console.WriteLine($"There is an exit to the West, but it is inaccessible.");
                    }
                }
                if (CurrentLoc.HasExitUp)
                {
                    if (NewLoc.HasExitDown)
                    {
                        Console.WriteLine($"There is a stairway leading up to: {NewLoc.LongName}");
                    }
                    else if (NewLoc == null)
                    {
                        Console.WriteLine($"There is a stairway leading up, but it is inaccessible.");
                    }
                }
                if (CurrentLoc.HasExitDown)
                {
                    if (NewLoc.HasExitUp)
                    {
                        Console.WriteLine($"There is a stairway leading down to: {NewLoc.LongName}");
                    }
                    else if (NewLoc == null)
                    {
                        Console.WriteLine($"There is a stairway leading down, but it is inaccessible.");
                    }
                }
            }
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
        public void GetLocationObjects()
        {
            Location myLoc = new();
            myLoc = myLoc.GetIsCurrentLoc();
            if (myLoc != null)
            {
                if (myLoc.LocationInventory != null)
                {
                    Console.WriteLine($"Inside {myLoc.LongName} I can see: ");
                    for (int index = 0; index < myLoc.LocationInventory.Count; index++)
                    {
                        if (myLoc.LocationInventory.Count > 1 && index < myLoc.LocationInventory.Count)
                        {
                            if ((index + 1) < myLoc.LocationInventory.Count)
                            {
                                Console.Write($"{myLoc.LocationInventory[index].LongName}, ");
                            }
                            else if ((index + 1) == myLoc.LocationInventory.Count)
                            {
                                Console.Write($"{myLoc.LocationInventory[index].LongName}\n\n");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("There is nothing here.");
                }
            }
            else
                Console.WriteLine("Variable myLoc is null or unset.");
        }
        public void GetLocationActors()
        {
            Location myLoc = new();
            myLoc = myLoc.GetIsCurrentLoc();
            if (myLoc != null)
            {
                if (myLoc.LocationActors != null)
                {
                    Console.WriteLine($"Actors present in {myLoc.LongName}:");
                    for (int index = 0; index < myLoc.LocationActors.Count; index++)
                    {
                        if (myLoc.LocationActors.Count > 1 && index < myLoc.LocationActors.Count)
                        {
                            if ((index + 1) < myLoc.LocationActors.Count)
                            {
                                Console.Write($"{myLoc.LocationActors[index].ActorName}, a {myLoc.LocationActors[index].ActorGender.ToString()} {myLoc.LocationActors[index].ActorClass.ToString()},\n");
                            }
                            else if ((index + 1) == myLoc.LocationActors.Count)
                            {
                                Console.Write($"{myLoc.LocationActors[index].ActorName}, a {myLoc.LocationActors[index].ActorGender.ToString()} {myLoc.LocationActors[index].ActorClass.ToString()}\n\n");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"There are no actors in {myLoc.LongName}.\n");
                }
            }
            else
                Console.WriteLine("Variable myLoc is null or unset.");
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
        public Location(string locName, string longName, bool exitN, bool exitS, bool exitE, bool exitW, bool exitUp, bool exitDown, List<Object> locInventory)
        {
            Name = locName;
            LongName = longName;
            HasExitN = exitN;
            HasExitS = exitS;
            HasExitE = exitE;
            HasExitW = exitW;
            HasExitUp = exitUp;
            HasExitDown = exitDown;
            LocationInventory = locInventory;
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
        public Location(string locName, string longName, bool exitN, bool exitS, bool exitE, bool exitW, bool exitUp, bool exitDown, List<Object> locInventory, List<Actor> locActors)
        {
            Name = locName;
            LongName = longName;
            HasExitN = exitN;
            HasExitS = exitS;
            HasExitE = exitE;
            HasExitW = exitW;
            HasExitUp = exitUp;
            HasExitDown = exitDown;
            LocationInventory = locInventory;
            LocationActors = locActors;
        }
        public Location(string locName, string longName, bool exitN, bool exitS, bool exitE, bool exitW, bool exitUp, bool exitDown, List<Actor> locActors)
        {
            Name = locName;
            LongName = longName;
            HasExitN = exitN;
            HasExitS = exitS;
            HasExitE = exitE;
            HasExitW = exitW;
            HasExitUp = exitUp;
            HasExitDown = exitDown;
            LocationActors = locActors;
        }

    }

}