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
    //Top level class for scenes. Scenes can contain multiple locations. 
    //Scene 1 is the starting scene for this world.
    //Everything inside it is instantiated before the command processor is called.
    public class Scene
    {
        public static Scene CurrentScene { get; set; }
        public static Scene LastScene { get; set; }
        public bool IsCurrentScene { get; set; }
        public bool IsPreviousScene { get; set; }
        public bool IsNextScene { get; set; }
        public static Scene NewScene { get; set; }
        public List<Scene> Scenes { get; set; }
        //Using a static list of scenes here as a holder for any active scenes.
        public static List<Scene> ActiveScenes { get; set; }
        public List<Location> Locations { get; set; }
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
        public Scene(string sceneName, string sceneDescription, List<Location> locations, bool isCurrentScene)
        {
            Name = sceneName;
            SceneDescription = sceneDescription;
            Locations = locations;
            IsCurrentScene = isCurrentScene;
        }
        //Duplicate of Scene1 as a nonstatic object instance:

        public class Scene1 : Scene
        {
            public new string Name = "My home";
            public new string SceneDescription = "My Old Quarter home\n\n" +
                                                 "A small house in the older part of the city, where the City Watch is not frequently seen.\n" +
                                                 "The area itself is home to some of the seedier nobles and other unsavoury characters, many thieves among them.\n\n";
            //New stuff
            public static List<Location> SceneLocations = new()
            {
                new Location("bedroom", "my master bedroom", LocID.LocBedroom, false, false, true, false, true, false, new List<Object>
                    {
                        new Object("bed", "My bed"),
                        new Object("computerdesk", "My computer workstation"),
                        new Object.Container("fridge", "My bar fridge", new List<Object.PickuppableObject.Consumable>
                        {
                            new Object.PickuppableObject.Consumable.Potion("beer","Half-litre can of beer"),
                            new Object.PickuppableObject.Consumable.Potion("beer","Half-litre can of beer"),
                            new Object.PickuppableObject.Consumable.Potion("beer","Half-litre can of beer"),
                            new Object.PickuppableObject.Consumable.Potion("beer","Half-litre can of beer"),
                            new Object.PickuppableObject.Consumable.Potion("beer","Half-litre can of beer"),
                            new Object.PickuppableObject.Consumable.Potion("beer","Half-litre can of beer"),
                            new Object.PickuppableObject.Consumable.Potion("beer","Half-litre can of beer"),
                            new Object.PickuppableObject.Consumable.Potion("beer","Half-litre can of beer"),
                            new Object.PickuppableObject.Consumable.Potion("beer","Half-litre can of beer"),
                            new Object.PickuppableObject.Consumable.Potion("beer","Half-litre can of beer"),
                            new Object.PickuppableObject.Consumable("steak","Sirloin steak"),
                            new Object.PickuppableObject.Consumable("steak","Sirloin steak"),
                            new Object.PickuppableObject.Consumable("steak","Sirloin steak"),
                            new Object.PickuppableObject.Consumable("steak","Sirloin steak"),
                            new Object.PickuppableObject.Consumable("sodawater","Bottle of soda water"),
                            new Object.PickuppableObject.Consumable("sodawater","Bottle of soda water"),
                            new Object.PickuppableObject.Consumable("sodawater","Bottle of soda water"),
                            new Object.PickuppableObject.Consumable("sodawater","Bottle of soda water"),
                            new Object.PickuppableObject.Consumable("sodawater","Bottle of soda water"),
                            new Object.PickuppableObject.Consumable("sodawater","Bottle of soda water"),
                        }),
                        new Object.Container("cupboard", "My cupboard", new List<Object>
                        {
                            new Object.PickuppableObject("clothes", "Some of my clothes"),
                            new Object.PickuppableObject("guitar", "My guitar"),
                            new Object.PickuppableObject("pcparts", "A box of old PC hardware"),
                            new Object.PickuppableObject("cdstack", "Stack of CDs")
                        }),
                    }, true),
                new Location("lounge", "the lounge", LocID.LocLounge, false, true, false, true, false, false, new List<Object>
                    {
                        new Object("table", "Lounge table"),
                        new Object("chair", "Lounge chair"),
                        new Object("chair", "Lounge chair"),
                        new Object("chair", "Lounge chair"),
                    }, false),
                new Location("attic", "the attic", LocID.LocAttic, false, false, false, false, false, true, new List<Object>
                    {
                        new Object.Container("chest", "Wooden chest", new List<Object>
                        {
                            new Object.PickuppableObject.Weapon("blackjack", "Blackjack"),
                            new Object.PickuppableObject.Weapon("bow","High-powered spring-lever bow"),
                            new Object.PickuppableObject.Weapon("sword","Long sword"),
                            new Object.PickuppableObject.PlayerTool("squareLockpick", "Square-tooth lockpick"),
                            new Object.PickuppableObject.PlayerTool("triangleLockpick","Triange-tooth lockpick"),
                        })
                    }, false),
                new Location("livingroom", "the living room", LocID.LocLivingroom, true, false, false, false, false, false, new List<Object>
                {
                    new Object.SurfaceContainer("wallunit","Modular wall unit", new List<Object>
                        {
                        new Object("soundsystem", "High fidelity sound system"),
                        new Object("speakerL", "Left speaker"),
                        new Object("speakerR", "Right speaker"),
                        new Object("hdtv", "High-definition Television"),
                        },
                        new List<Object.PickuppableObject>
                        {
                            new Object.PickuppableObject("whiskyGlass", "Whisky glass"),
                            new Object.PickuppableObject("whiskyGlass", "Whisky glass"),
                            new Object.PickuppableObject("beerGlass", "Beer glass"),
                            new Object.PickuppableObject("beerGlass", "Beer glass"),
                            new Object.PickuppableObject("emptyWineBottle", "Empty wine bottle"),
                        },
                        new List<Object.PickuppableObject.Consumable>
                        {
                            new Object.PickuppableObject.Consumable("whisky", "Scotch whisky"),
                        }),

                    new Object.Container("boozecabinet", "Drinks cabinet", new List<Object.PickuppableObject.Consumable>
                    {
                        new Object.PickuppableObject.Consumable("liqueur","Cream liqueur"),
                        new Object.PickuppableObject.Consumable("whisky","Vintage Scotch whisky"),
                        new Object.PickuppableObject.Consumable("goodWine","Collectors' edition vintage red wine"),
                        new Object.PickuppableObject.Consumable("biltong","Beef biltong"),
                    }),
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
        public List<Scene> PopulateSceneList()
        {
            Scene MyHome = new()
            {
                Name = "My Home",
                SceneDescription = "My Old Quarter home\n\n" +
                                                "A small house in the older part of the city, where the City Watch is not frequently seen.\n" +
                                                "The area itself is home to some of the seedier nobles and other unsavoury characters, many thieves among them.\n\n",
                Locations = new()
                {
                    new Location()
                    {
                        Name = "bedroom",
                        LongName = "my master bedroom",
                        HasExitE = true,
                        EastDoorway = new()
                        {
                            Direction = "East",
                            PortalName = "doorway",
                            IsAccessible = true,
                            HasDoor = true,
                            door = new()
                            {
                                DoorName = "East door",
                                IsLocked = false,
                                IsClosed = true,
                            },
                        },
                        HasExitUp = true,
                        StairwayUp = new()
                        {
                            IsAccessible = true,
                            HasDoor = false,
                            Direction = "Up",
                            PortalName = "Stairway",
                        },
                        LocationInventory = new()
                        {
                            new Object("bed", "My bed"),
                            new Object("computerdesk", "My computer workstation"),
                            new Object.Container("fridge", "My bar fridge", new List<Object.PickuppableObject.Consumable>
                                {
                                    new Object.PickuppableObject.Consumable.Potion("beer", "Half-litre can of beer"),
                                    new Object.PickuppableObject.Consumable.Potion("beer", "Half-litre can of beer"),
                                    new Object.PickuppableObject.Consumable.Potion("beer", "Half-litre can of beer"),
                                    new Object.PickuppableObject.Consumable.Potion("beer", "Half-litre can of beer"),
                                    new Object.PickuppableObject.Consumable.Potion("beer", "Half-litre can of beer"),
                                    new Object.PickuppableObject.Consumable.Potion("beer", "Half-litre can of beer"),
                                    new Object.PickuppableObject.Consumable.Potion("beer", "Half-litre can of beer"),
                                    new Object.PickuppableObject.Consumable.Potion("beer", "Half-litre can of beer"),
                                    new Object.PickuppableObject.Consumable.Potion("beer", "Half-litre can of beer"),
                                    new Object.PickuppableObject.Consumable.Potion("beer", "Half-litre can of beer"),
                                    new Object.PickuppableObject.Consumable("steak", "Sirloin steak"),
                                    new Object.PickuppableObject.Consumable("steak", "Sirloin steak"),
                                    new Object.PickuppableObject.Consumable("steak", "Sirloin steak"),
                                    new Object.PickuppableObject.Consumable("steak", "Sirloin steak"),
                                    new Object.PickuppableObject.Consumable("sodawater", "Bottle of soda water"),
                                    new Object.PickuppableObject.Consumable("sodawater", "Bottle of soda water"),
                                    new Object.PickuppableObject.Consumable("sodawater", "Bottle of soda water"),
                                    new Object.PickuppableObject.Consumable("sodawater", "Bottle of soda water"),
                                    new Object.PickuppableObject.Consumable("sodawater", "Bottle of soda water"),
                                    new Object.PickuppableObject.Consumable("sodawater", "Bottle of soda water"),
                                }),
                            new Object.Container("cupboard", "My cupboard", new List<Object>
                                {
                                    new Object.PickuppableObject("clothes", "Some of my clothes"),
                                    new Object.PickuppableObject("guitar", "My guitar"),
                                    new Object.PickuppableObject("pcparts", "A box of old PC hardware"),
                                    new Object.PickuppableObject("cdstack", "Stack of CDs")
                                }),
                        },
                        IsCurrentLocation = true,
                        AdjacentLocs = new() { "lounge", "attic" }
                    },
                    new Location()
                    {
                        Name = "lounge",
                        LongName = "the lounge",
                        HasExitS = true,
                        HasExitW = true,
                        SouthDoorway = new()
                        {
                            PortalName = "doorway",
                            Direction = "South",
                            HasDoor = true,
                            door = new()
                            {
                                DoorName = "door",
                                IsClosed = true,
                                IsLocked = false,
                            }
                        },
                        WestDoorway = new()
                        {
                            PortalName = "doorway",
                            Direction = "West",
                            HasDoor = true,
                            door = new()
                            {
                                DoorName = "door",
                                IsClosed = true,
                                IsLocked = false,
                            }
                        },
                        LocationInventory = new()
                        {
                            new Object("table", "Lounge table"),
                            new Object("chair", "Lounge chair"),
                            new Object("chair", "Lounge chair"),
                            new Object("chair", "Lounge chair"),
                        },
                        AdjacentLocs = new() { "bedroom", "livingroom" }
                    },
                    new Location()
                    {
                        Name = "attic",
                        LongName = "the attic",
                        HasExitDown = true,
                        LocationInventory = new()
                        {
                            new Object.Container("chest", "Wooden chest", new List<Object>
                                {
                                    new Object.PickuppableObject.Weapon("blackjack", "Blackjack"),
                                    new Object.PickuppableObject.Weapon("bow", "High-powered spring-lever bow"),
                                    new Object.PickuppableObject.Weapon("sword", "Long sword"),
                                    new Object.PickuppableObject.PlayerTool("squareLockpick", "Square-tooth lockpick"),
                                    new Object.PickuppableObject.PlayerTool("triangleLockpick", "Triange-tooth lockpick"),
                                })
                        },
                        AdjacentLocs = new() { "bedroom" }
                    },
                    new Location()
                    {
                        Name = "livingroom",
                        LongName = "the living room",
                        HasExitN = true,
                        LocationInventory = new()
                        {
                            new Object.SurfaceContainer("wallunit", "Modular wall unit", new List<Object>
                                {
                                    new Object("soundsystem", "High fidelity sound system"),
                                    new Object("speakerL", "Left speaker"),
                                    new Object("speakerR", "Right speaker"),
                                    new Object("hdtv", "High-definition Television"),
                                },
                       new List<Object.PickuppableObject>
                       {
                            new Object.PickuppableObject("whiskyGlass", "Whisky glass"),
                            new Object.PickuppableObject("whiskyGlass", "Whisky glass"),
                            new Object.PickuppableObject("beerGlass", "Beer glass"),
                            new Object.PickuppableObject("beerGlass", "Beer glass"),
                            new Object.PickuppableObject("emptyWineBottle", "Empty wine bottle"),
                       },
                       new List<Object.PickuppableObject.Consumable>
                       {
                            new Object.PickuppableObject.Consumable("whisky", "Scotch whisky"),
                       }),

                            new Object.Container("boozecabinet", "Drinks cabinet", new List<Object.PickuppableObject.Consumable>
                                {
                                    new Object.PickuppableObject.Consumable("liqueur", "Cream liqueur"),
                                    new Object.PickuppableObject.Consumable("whisky", "Vintage Scotch whisky"),
                                    new Object.PickuppableObject.Consumable("goodWine", "Collectors' edition vintage red wine"),
                                    new Object.PickuppableObject.Consumable("biltong", "Beef biltong"),
                                }),
                        },
                        AdjacentLocs = new() { "lounge" }
                    },
                },
            };
            Scene EasternCityStreets = new()
            {
                Name = "Eastern city streets",
                SceneDescription = "The eastern city streets.\n\n" +
         "The eastern part of the city streets, where nobles' private guards and City Watch are not often seen.\n" +
         "These streets are also home to the criminal underworld.\n\n",
                Locations = new()
                {
                    new Location()
                    {
                        Name = "courtyard",
                        LongName = "Front courtyard",
                        HasExitE = true,
                        HasExitDown = false,
                        LocationInventory = new()
                        {
                            new Object("table", "Garden table"),
                            new Object("bench", "Garden bench"),
                            new Object("bench", "Garden bench"),

                        },
                        AdjacentLocs = new() { "streets" },
                        IsCurrentLocation = true,
                    },
                    new Location()
                    {
                        Name = "streetsN",
                        LongName = "Eastern city streets (North part)",
                        HasExitN = false,
                        HasExitS = true,
                        HasExitE = false,
                        HasExitW = false,
                        HasExitUp = false,
                        HasExitDown = false,
                        LocationActors = new()
                        {
                            new Actor.UnarmedCitizen(),
                            new Actor.UnarmedCitizen(),
                            new Actor.Guard(),
                        },
                        AdjacentLocs = new() { "streets" },
                    },
                    new Location()
                    {
                        Name = "streets",
                        LongName = "Eastern city streets (Central part)",
                        HasExitN = true,
                        HasExitS = true,
                        HasExitW = true,
                        LocationActors = new()
                        {
                            new Actor.Guard() { ActorName = "Bob", ActorClass = EActorClass.Guard, ActorGender = EActorGender.Male },
                            new Actor.Guard() { ActorName = "Benny", ActorClass = EActorClass.Guard, ActorGender = EActorGender.Male },
                            new Actor.Guard() { ActorName = "Alice", ActorClass = EActorClass.Guard, ActorGender = EActorGender.Female },
                            new Actor.UnarmedCitizen() { ActorName = "Richard", ActorClass = EActorClass.Noble, ActorGender = EActorGender.Male },
                            new Actor.UnarmedCitizen() { ActorName = "Angela", ActorClass = EActorClass.Noble, ActorGender = EActorGender.Female },

                        },
                        AdjacentLocs = new() { "streetsN", "streetsS", "courtyard" },
                    },
                    new Location()
                    {
                        Name = "streetsS",
                        LongName = "Eastern city streets (South part)",
                        HasExitN = true,
                        LocationActors = new()
                        {
                            new Actor.UnarmedCitizen(),
                            new Actor.UnarmedCitizen(),
                            new Actor.Guard(),
                        },
                        AdjacentLocs = new() { "streets" },
                    },
                }
            };
            Scene HammeriteChurch = new()
            {
                Name = "Eastern Streets - Hammerite Church\n\n",
                SceneDescription = "The local Hammerite church on the Eastern side of the city\n" +
                "Security in and around the church is extremely high, and the Hammerite guards on patrol have no love of criminals.\n" +
                "They are known to attack first and ask questions later.\n" +
                "It's no surprise though, as Hammerite churches are rumoured to be full of priceless valuables.\n\n",
                Locations = new()
                {
                    new Location()
                    {
                        Name = "courtyard",
                        LongName = "Church Courtyard",
                        AdjacentLocs = new() { "entrancehall" },
                        HasExitN = true,
                        IsCurrentLocation = true
                    },
                    new Location() { Name = "entrancehall", LongName = "Church - Entrance hall", AdjacentLocs = new() { "westwing", "eastwing", "courtyard", "altarwing" }, HasExitN = true, HasExitS = true, HasExitE = true, HasExitW = true, },
                    new Location() { Name = "altarwing", LongName = "Church - Altar wing", AdjacentLocs = new() { "entrancehall" }, HasExitS = true },
                    new Location() { Name = "westwing", LongName = "Church - West wing", AdjacentLocs = new() { "entrancehall" }, HasExitE = true },
                    new Location() { Name = "eastwing", LongName = "Church - East wing", AdjacentLocs = new() { "entrancehall" }, HasExitW = true },
                }
            };
            List<Scene> MyScenes = new();
            MyScenes.Add(MyHome);
            MyScenes.Add(EasternCityStreets);
            MyScenes.Add(HammeriteChurch);
            return MyScenes;
            //ActiveScenes = MyScenes;
            //return ActiveScenes;
        }
        public Scene SetUpScene(int SceneIndex)
        {
            Scene MyScene = new();
            List<Scene> MyScenes = new();
            MyScenes = PopulateSceneList();
            MyScene = MyScenes[SceneIndex - 1];
            MyScene.IsCurrentScene = true;
            return MyScene;
        }
        //This is the Scene instantiator, which calls the populate scene list func to get its information.
        //This function is responsible for setting up the scene, also when changing scenes.
        public Scene SetUpSceneByName(string SceneName)
        {
            List<Scene> MyScenes = ActiveScenes;
            Scene MyScene = ActiveScenes.Where(x => x.Name == SceneName).FirstOrDefault();
            MyScene.IsCurrentScene = true;
            Console.WriteLine($"{MyScene.Name}\n\n{MyScene.SceneDescription}");
            return MyScene;
        }

        public Scene SetUpScene(int SceneIndex, List<Scene> MyScenes)
        {
            try
            {
                MyScenes = PopulateSceneList();
                Console.WriteLine($"{MyScenes[SceneIndex - 1].Name} \n\n{MyScenes[SceneIndex - 1].SceneDescription}");
                //Using a static variable of same datatype to store/forward this instance.
                ActiveScenes = MyScenes;
                ActiveScenes[SceneIndex - 1].IsCurrentScene = true;
                if (ActiveScenes != null && SceneIndex >= 0)
                {
                    return ActiveScenes[SceneIndex - 1];
                }
                else
                {
                    Console.WriteLine("Scene index can not be negative");
                    return null;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine($"Invalid scene or argument is out of range: {SceneIndex} ");
                return null;
            }
        }
        //This is a test command to get the current scene
        public Scene QueryScene(Scene MyScene, List<Scene> MyScenes)
        {
            MyScenes = QuerySceneList();
            try
            {
                MyScene = MyScenes.Where(x => x.IsCurrentScene).FirstOrDefault();
                if (MyScene == null)
                {
                    Console.WriteLine("MyScene is null or unset");
                }
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Scene list is null or unset.");
            }
            return MyScene;
        }
        public Scene GetCurrentScene()
        {
            Scene CurrentScene = ActiveScenes.Where(x => x.IsCurrentScene == true).FirstOrDefault();
            return CurrentScene;
        }
        public void CreateScene()
        {
            //Do something
        }
        //Change the active scene on trigger to the next one in the list
        public Scene ChangeSceneNext()
        {
            Scene MyScene = new();
            int SceneIndex = ActiveScenes.FindIndex(x => x.IsCurrentScene == true);
            SceneIndex++;
            MyScene = SetUpScene(SceneIndex + 1, ActiveScenes);
            return MyScene;

        }
        //Change the active scene on trigger to the previous one in the list
        public Scene ChangeScenePrevious()
        {
            Scene MyScene = new();
            int SceneIndex = ActiveScenes.FindIndex(x => x.IsCurrentScene);
            SceneIndex--;
            MyScene = SetUpScene(SceneIndex + 1, ActiveScenes);
            return MyScene;
        }

        public List<Scene> QuerySceneList()
        {
            return ActiveScenes;
        }
    }
}

