/*
Interactive Fiction Command line interpreter
Scene.cs

© 2021 Iain Marais (il-Salvatore on Github)
Licence: Apache v2.0 or 3-clause BSD Licence

Please see www.apache.org/licenses/LICENSE-2.0.html || opensource.org/licenses/BSD-3-Clause for more information.

The scope of this project is to build a simple but efficient command line interpreter for a console-based interactive fiction engine,
Think classic Zork, where one entered commands and read the output.

This project will be entirely c# based.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveFiction_CLI
{
    partial class Program
    {
        //Top level class for scenes. Scenes can contain multiple locations. 
        //Scene 1 is the starting scene for this world.
        //Everything inside it is instantiated before the command processor is called.
        public class Scene
        {
            public static Scene CurrentScene { get; set; }
            public static Scene NewScene { get; set; }
            List<Location> Locations { get; set; }
            public string Name { get; set; }
            public string SceneDescription { get; set; }
            public Scene()
            {
                Locations = new List<Location>();
            }
            public Scene(string sceneName, List<Location> locations)
            {
                Name = sceneName;
                Locations = locations;
            }
            public Scene(string sceneName, string sceneDescription, List<Location> locations)
            {
                Name = sceneName;
                SceneDescription = sceneDescription;
                Locations = locations;
            }
            public class Scene1 : Scene
            {
                public new string Name = "My home";
                public new string SceneDescription = "My Old Quarter home\n\n" +
                                                     "A small house in the older part of the city, where the City Watch is not frequently seen.\n" +
                                                     "The area itself is home to some of the seedier nobles and other unsavoury characters, many thieves among them.";
                new public static List<Location> Locations = new();

                //New stuff

                public static List<Location> SceneLocations = new()
                {
                    new Location("bedroom", "My bedroom", LocID.LocBedroom, false, false, true, false, true, false, new List<Object>
                    {
                        new Object("bed", "My bed"),
                        new Object("computerdesk", "My computer workstation"),
                        new Object.Container("fridge", "My bar fridge", new List<Object.Consumable>
                        {
                            new Object.Consumable.Potion("beer", 6),
                            new Object.Consumable.Potion("wine", 4),
                            new Object.Consumable("steak", 2),
                            new Object.Consumable("bottled soda water", 6),
                        }),
                        new Object.Container("cupboard", "My cupboard", new List<Object>
                        { new Object("clothes"),
                            new Object("guitar"),
                            new Object("box of PC parts"),
                            new Object("stack of CDs")
                        }),
                    }, true),
                    new Location("lounge", "My lounge", LocID.LocLounge, false, true, false, true, false, false, new List<Object>
                    {
                        new Object("table", "Lounge table"),
                        new Object("chair", "Lounge chair"),
                        new Object("chair", "Lounge chair"),
                        new Object("chair", "Lounge chair"),
                    }, false),
                    new Location("attic", "Attic", LocID.LocAttic, false, false, false, false, false, true, new List<Object>
                    {
                        new Object.Container("chest", "Wooden chest", new List<Object>
                        { new Object("blacjack", "Blackjack")
                        })
                    }, false),
                    new Location("livingroom", "My living room", LocID.LocLivingroom, true, false, false, false, false, false, new List<Object>
                    {
                    }, false),

                };
                //End new stuff
                public Scene1()
                {

                }
                public Scene1(string sceneName, List<Location> sceneLocations)
                {
                    Name = sceneName;
                    SceneLocations = sceneLocations;
                }
            }
        }
        //For testing - will be left here until it works, then move to logic.cs
        public void CreateScene()
        {
            //Do something
        }
        public void ChangeScene()
        {
            //Do something
        }
    }
}

