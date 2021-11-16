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
        public bool IsCurrentScene { get; set; }
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
                                                 "The area itself is home to some of the seedier nobles and other unsavoury characters, many thieves among them.\n" +
                                                 "\n";
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
        //Same as Scene1, but not static.
        public Scene SetUpScene(int SceneIndex, List<Scene> MyScenes)
        {
            Scene TestScene = new()
            {
                Name = "My Home",
                SceneDescription = "My Old Quarter home\n\n" +
                                                 "A small house in the older part of the city, where the City Watch is not frequently seen.\n" +
                                                 "The area itself is home to some of the seedier nobles and other unsavoury characters, many thieves among them.\n" +
                                                 "\n",
                Locations = new()
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
                },
                IsCurrentScene = true,
            };
            MyScenes.Add(TestScene);
            ActiveScenes = MyScenes;
            Scene StartScene = MyScenes[SceneIndex - 1];
            Console.WriteLine($"{MyScenes[SceneIndex - 1].Name} instantiated.");
            return StartScene;
        }
        public Scene QueryScene(Scene MyScene)
        {
            //Test:
            //Console.WriteLine("Scene.QueryScene entered");
            MyScene = ActiveScenes.Where(x => x.IsCurrentScene).FirstOrDefault();
            //Test: 
            //Console.WriteLine($"Variable MyScene, value Name is set to: {MyScene.Name}");
            return MyScene;
        }
        public void CreateScene()
        {
            //Do something
        }
        public void ChangeScene()
        {
            //Do something
        }
    }
    //For testing - will be left here until it works, then move to logic.cs


}

