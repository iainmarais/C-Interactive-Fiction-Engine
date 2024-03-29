﻿/*
Interactive Fiction Command line interpreter 
CommandProcessor.cs

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
    public static class SysCmds //Moved these here from dictionary file
    {
        public static void ActionCmdList()
        {
            foreach (var word in WordList.Actions)
            {
                Console.WriteLine(word);
            }
        }
        public static void ActionQuit()
        {
            //Ask the user to confirm their choice.
            Console.Write("Exit program? ");
            char userChoice = Console.ReadKey().KeyChar;
            if (userChoice == 'Y' || userChoice == 'y')
            {
                Environment.Exit(0);
            }
            else if (userChoice == 'N' || userChoice == 'n')
            {
                Console.Write("\n");
                return;
            }
        }
        public static void ActionClear()
        {
            Console.Clear();
        }
        public static void ActionAbout()
        {
            Console.WriteLine("Interactive Fiction Command line interpreter\n© 2021 Iain Marais\n" +
                "\nThis program is open source under the Apache v2.0 and 3-clause BSD licences.\n" +
                "\nUse at your own risk, while I take every precaution to protect users from damage,\n" +
                "I can not provide guarantees that damage will not occur.");
        }
    }

    class CommandProcessor
    {
        public class Command
        {
            Logic CommandLogic = new();
            public static string Word1 { get; set; }
            public static string Word2 { get; set; }
            public static string Word3 { get; set; }
            public static string Word4 { get; set; }
            public static string Cmd { get; set; }
            public static bool ValidCmd { get; set; }

            public Command()
            {

            }
            public Command(string cmd1, string cmd2, string cmd3, string cmd4)
            {
                Word1 = cmd1;
                Word2 = cmd2;
                Word3 = cmd3;
                Word4 = cmd4;
            }
            public void GetCmd()
            {
                Console.Write("Command: ");
                ProcessCmd();
                ResetCmd();
            }
            public void ProcessCmd()
            //How many words have we got? Check length of array and assign appropriately.
            {
                string UserInput = Console.ReadLine();
                string[] Words = UserInput.Split(' ');
                switch (Words.Length)
                {
                    case 0:
                        break;
                    case 1:
                        Word1 = Words[0].IsNotBlank();
                        break;
                    case 2:
                        Word1 = Words[0].IsNotBlank();
                        Word2 = Words[1].IsNotBlank();
                        break;
                    case 3:
                        Word1 = Words[0].IsNotBlank();
                        Word2 = Words[1].IsNotBlank();
                        Word3 = Words[2].IsNotBlank();
                        break;
                    case 4:
                        Word1 = Words[0].IsNotBlank();
                        Word2 = Words[1].IsNotBlank();
                        Word3 = Words[2].IsNotBlank();
                        Word4 = Words[3].IsNotBlank();
                        break;
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        while (Console.KeyAvailable) //clear input buffer, probably dont need more than 10 cases at this point.
                            Console.ReadKey(true);
                        Console.ReadKey();
                        break;
                    default:
                        //Handle null here
                        Console.Write("Invalid or null command.\n");
                        break;
                }

                if (Words.Length >= 1 && Words.Length <= 4 && Word1 != null && Word1 != " ")
                {
                    string myWord = WordList.Actions.Where(x => x.Contains(Word1)).FirstOrDefault();
                    if (Word1 == myWord && Word1 != "about" && Word1 != "quit" && Word1 != "cmdlist" && Word1 != "clear")
                    {
                        if (myWord == "where")
                        {
                            Location CurrentLoc = new();
                            CurrentLoc = CurrentLoc.GetIsCurrentLoc();
                        }
                        else if (myWord == "knock")
                        {
                            try
                            {
                                string myWord2 = WordList.HelperWords.Where(x => x.Contains(Word2)).FirstOrDefault();

                                if (Word2 != myWord2 || Word2 == null)
                                {
                                    Console.WriteLine("Knock what?");
                                }
                                else if (Word2 == myWord2)
                                {
                                    if (myWord2 == "out")
                                    {
                                        Location MyLoc = new();
                                        MyLoc = MyLoc.GetIsCurrentLoc();
                                        if (Word3 == null)
                                        {
                                            Console.WriteLine("Knock out who?");
                                        }
                                        else if (Word3 != null)
                                        {
                                            Actor TargetActor = new();
                                            TargetActor = MyLoc.LocationActors.Where(x => x.ActorName == Word3).FirstOrDefault();
                                            TargetActor.KnockOutActor(Word3);
                                        }
                                    }
                                }
                            }
                            catch (ArgumentNullException)
                            {
                                Console.WriteLine("Helper word unrecognised, please try again.");
                            }
                        }
                        else if (myWord == "put")
                        {
                            string myWord2 = Word2;
                            try
                            {
                                if (myWord2 == WordList.ObjectNames.Where(x => x.Contains(Word2)).FirstOrDefault())
                                {
                                    myWord2 = WordList.ObjectNames.Where(x => x.Contains(Word2)).FirstOrDefault();
                                    Object myObject = new();
                                    string myWord3 = WordList.HelperWords.Where(x => x.Contains(Word3)).FirstOrDefault();
                                    if (myWord3 == "in")
                                    {
                                        string myWord4 = WordList.ContainerNames.Where(x => x.Contains(Word4)).FirstOrDefault();
                                        if (myWord4 == null)
                                        {
                                            Console.WriteLine($"Get {myWord2} {myWord3} where?");
                                        }
                                        else if (myWord4 == WordList.ContainerNames.Where(x => x.Contains(Word4)).FirstOrDefault())
                                        {
                                            Object.Container myContainer = new();
                                            myContainer.Name = myWord4;
                                            try
                                            {
                                                {
                                                    Object.ContainerName = myWord4;
                                                    // throw new NotImplementedException("Not implemented");
                                                    myObject.PutObject(myWord4, myWord2);
                                                }
                                            }
                                            catch (NotImplementedException)
                                            {
                                                Console.WriteLine("This function has not yet been implemented.");
                                            }
                                        }
                                    }
                                }
                                if (myWord2 == WordList.ConsumableNames.Where(x => x.Contains(Word2)).FirstOrDefault())
                                {
                                    Object myObject = new();
                                    myObject.Name = myWord2;
                                    string myWord3 = WordList.HelperWords.Where(x => x.Contains(Word3)).FirstOrDefault();
                                    if (myWord3 == "in")
                                    {
                                        string myWord4 = WordList.ContainerNames.Where(x => x.Contains(Word4)).FirstOrDefault();
                                        if (myWord4 == null)
                                        {
                                            Console.WriteLine($"Get {myWord2} {myWord3} where?");
                                        }
                                        else if (myWord4 == WordList.ContainerNames.Where(x => x.Contains(Word4)).FirstOrDefault())
                                        {
                                            try
                                            {
                                                {
                                                    Object.Container myContainer = new();
                                                    myContainer.Name = myWord4;
                                                    //  throw new NotImplementedException("Not implemented");
                                                    myObject.PutObject(myWord4, myWord2);
                                                }
                                            }
                                            catch (NotImplementedException)
                                            {
                                                Console.WriteLine("This function has not yet been implemented.");
                                            }
                                        }
                                    }
                                }

                            }
                            catch (ArgumentNullException)
                            {
                                Console.WriteLine("Put what where?");
                            }

                        }
                        else if (myWord == "get")
                        {
                            string myWord2 = Word2;
                            try
                            {
                                if (myWord2 == WordList.ObjectNames.Where(x => x.Contains(Word2)).FirstOrDefault())
                                {
                                    Object myObject = new();
                                    myObject.Name = myWord2;
                                    string myWord3 = WordList.HelperWords.Where(x => x.Contains(Word3)).FirstOrDefault();

                                    if (myWord3 == "from")
                                    {
                                        string myWord4 = WordList.ContainerNames.Where(x => x.Contains(Word4)).FirstOrDefault();
                                        if (myWord4 == null)
                                        {
                                            Console.WriteLine($"Get {myWord2} {myWord3} where?");
                                        }
                                        else if (myWord4 == WordList.ContainerNames.Where(x => x.Contains(Word4)).FirstOrDefault())
                                        {
                                            Object.Container myContainer = new();
                                            myContainer.Name = myWord4;
                                            myObject.GetObject(myWord4, myWord2);
                                        }
                                    }
                                }

                                if (myWord2 == WordList.ConsumableNames.Where(x => x.Contains(Word2)).FirstOrDefault())
                                {
                                    Object myObject = new();
                                    myObject.Name = myWord2;
                                    string myWord3 = WordList.HelperWords.Where(x => x.Contains(Word3)).FirstOrDefault();
                                    if (myWord3 == null)
                                    {
                                        Console.WriteLine($"Get {myWord2} from where?");
                                    }
                                    else if (myWord3 == "from")
                                    {
                                        string myWord4 = WordList.ContainerNames.Where(x => x.Contains(Word4)).FirstOrDefault();
                                        if (myWord4 == null)
                                        {
                                            Console.WriteLine($"Get {myWord2} {myWord3} where?");
                                        }
                                        else if (myWord4 == WordList.ContainerNames.Where(x => x.Contains(Word4)).FirstOrDefault())
                                        {
                                            Object.Container myContainer = new();
                                            myContainer.Name = myWord4;
                                            myObject.GetObject(myWord4, myWord2);
                                        }
                                    }
                                }
                            }
                            catch (ArgumentNullException)
                            {
                                Console.WriteLine("Get what from where?");
                            }
                        }
                        else if (myWord == "look")
                        {
                            try
                            {
                                string myWord2 = WordList.HelperWords.Where(x => x.Contains(Word2)).FirstOrDefault();
                                if (myWord2 == "around")
                                {
                                    Location CurrentLoc = new();
                                    CurrentLoc.GetLocationObjects();
                                    CurrentLoc.GetLocationActors();
                                    CurrentLoc.GetAvailableExits();
                                }
                                else if (myWord2 == "in")
                                {
                                    string myWord3 = WordList.ContainerNames.Where(x => x.Contains(Word3)).FirstOrDefault();
                                    if (myWord3 == null)
                                    {
                                        Console.WriteLine("Look in where?");
                                    }
                                    else if (myWord3 == WordList.ContainerNames.Where(x => x.Contains(Word3)).FirstOrDefault())
                                    {
                                        Object.Container myContainer = new();
                                        myContainer.GetContainerInventory(myWord3);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid container name specified. Please try again.");
                                    }
                                }
                                else if (myWord2 == "on")
                                {
                                    string myWord3 = WordList.SurfaceContainerNames.Where(x => x.Contains(Word3)).FirstOrDefault();
                                    if (myWord3 == null)
                                    {
                                        Console.WriteLine("Look on where?");
                                    }
                                    else if (myWord3 != null) //temporary handler for non-null container of the surface type
                                    {
                                        Object.SurfaceContainer myContainer = new();
                                        myContainer.GetSurfaceContainerObjects(myWord3);
                                    }
                                }
                            }
                            catch (ArgumentNullException)
                            {
                                Console.WriteLine("Bad command or unknown word.");
                            }
                        }
                        else if (myWord != (WordList.Actions.Where(x => x.Contains(Word1)).FirstOrDefault()))
                        {
                            Console.Write($"This command {myWord} has not yet been implemented.\n");
                        }
                        else if (myWord == "go")
                        {
                            Location currentLoc = new();
                            currentLoc = currentLoc.GetIsCurrentLoc();
                            if (Word2 == null)
                            {
                                Console.WriteLine("Go where?");
                            }
                            else if (Word2 == "north")
                            {
                                if (currentLoc.HasExitN)
                                {
                                    Location NewLoc = new();
                                    NewLoc = NewLoc.ChangeLoc(NewLoc.QueryLocByDir(Word2).Name);
                                }
                            }
                            else if (Word2 == "south")
                            {
                                if (currentLoc.HasExitS)
                                {
                                    Location NewLoc = new();
                                    NewLoc = NewLoc.ChangeLoc(NewLoc.QueryLocByDir(Word2).Name);
                                }
                            }
                            else if (Word2 == "east")
                            {
                                if (currentLoc.HasExitE)
                                {
                                    Location NewLoc = new();
                                    NewLoc = NewLoc.ChangeLoc(NewLoc.QueryLocByDir(Word2).Name);
                                }
                            }
                            else if (Word2 == "west")
                            {
                                if (currentLoc.HasExitW)
                                {
                                    Location NewLoc = new();
                                    NewLoc = NewLoc.ChangeLoc(NewLoc.QueryLocByDir(Word2).Name);
                                }
                            }
                            else if (Word2 == "down")
                            {
                                if (currentLoc.HasExitDown)
                                {
                                    Location NewLoc = new();
                                    NewLoc = NewLoc.ChangeLoc(NewLoc.QueryLocByDir(Word2).Name);
                                }
                            }
                            else if (Word2 == "up")
                            {
                                if (currentLoc.HasExitUp)
                                {
                                    Location NewLoc = new();
                                    NewLoc = NewLoc.ChangeLoc(NewLoc.QueryLocByDir(Word2).Name);
                                }
                            }
                            //Direct loc access: 
                            else if (Word2 != null)
                            {
                                Location NewLoc = new();
                                NewLoc = NewLoc.ChangeLoc(Word2);
                                //Logic.GoToNewLocation();
                            }
                        }

                        ValidCmd = true;
                    }
                    else if (Word1 == "about")
                    {
                        SysCmds.ActionAbout();
                        ValidCmd = true;
                    }

                    else if (Word1 == "quit") //Syscmd: quit : exit program
                    {
                        SysCmds.ActionQuit();
                        ValidCmd = true;
                    }
                    else if (Word1 == "clear") //Syscmd: clear : clear console
                    {
                        SysCmds.ActionClear();
                        ValidCmd = true;
                    }
                    else if (Word1 == "cmdlist") //Write out the list of commands to the console. 
                    {
                        SysCmds.ActionCmdList();
                        ValidCmd = true;
                    }
                    else if (Word1 == "boo")  //Easter egg as a syscmd
                    {
                        Random randomLine = new();
                        int LineIndex = randomLine.Next(1, 8);
                        switch (LineIndex)
                        {
                            case 1:
                                Console.WriteLine("Really?");
                                break;
                            case 2:
                                Console.WriteLine("That won't work on me");
                                break;
                            case 3:
                                Console.WriteLine("Nice try.");
                                break;
                            case 4:
                                Console.WriteLine("Wha...?");
                                break;
                            case 5:
                                Console.WriteLine("Ohhh. I'm shaking, I'm shaking.");
                                break;
                            case 6:
                                Console.WriteLine("Behind you!");
                                break;
                            case 7:
                                Console.WriteLine("Dopefish lives.");
                                break;
                            case 8:
                                Console.WriteLine("The force is strong in you");
                                break;
                            default:
                                break;
                        }
                    }
                    //Test commands, useful for directly setting a scene or loc to a specified one
                    else if (Word1 == "test")
                    {
                        if (Word2 == null)
                        {
                            Console.WriteLine("Test what?");
                        }
                        if (Word2 == "query")
                        {
                            if (Word3 == "objects")
                            {
                                Location myLoc = new();
                                myLoc.GetLocationObjects();
                            }
                            if (Word3 == "location")
                            {
                                Location myLoc = new();
                                myLoc = myLoc.GetIsCurrentLoc();
                                Console.WriteLine($"myLoc is: {myLoc.Name}");
                            }
                            else if (Word3 == "scene")
                            {
                                Scene myScene = new();
                                List<Scene> MyScenes = new();
                                myScene = myScene.QueryScene(myScene, MyScenes);
                                Console.WriteLine($"myScene is set to: {myScene.Name}");
                            }
                            if (Word3 == null)
                            {
                                Console.WriteLine("Please specify what to query.");
                            }
                        }
                        if (Word2 == "set")
                        {
                            if (Word3 == "location")
                            {
                                if (Word4 == null)
                                {
                                    Console.WriteLine("Please specify a location name");
                                }
                                else
                                {
                                    Location myLoc = new();
                                    myLoc.ChangeLoc();
                                }
                            }
                            if (Word3 == "scene")
                            {
                                if (Word4 == null)
                                {
                                    Console.WriteLine("Please specify a scene number");
                                }
                                else if (Word4 == "next")
                                {
                                    Scene MyScene = new();
                                    List<Scene> MyScenes = new();
                                    MyScene = MyScene.ChangeSceneNext();
                                }
                                else if (Word4 == "previous")
                                {
                                    Scene MyScene = new();
                                    List<Scene> MyScenes = new();
                                    MyScene = MyScene.ChangeScenePrevious();
                                }
                                else
                                {
                                    int SceneIndex = int.Parse(Word4);
                                    Scene MyScene = new();
                                    List<Scene> MyScenes = new();
                                    MyScene.SetUpScene(SceneIndex, MyScenes);
                                }
                            }
                            if (Word3 == null)
                            {
                                Console.WriteLine("Please specify what to set");
                            }
                        }
                    }
                    //syscmd easter eggs
                    else if (Word1 == "noclip") // Nod to Quake :D
                    {
                        Console.WriteLine("This is not Quake, dude");
                    }
                    else if (Word1 == "god") // Nod to Quake :D
                    {
                        Console.WriteLine("That name starts with a capital G dude, He lives far, far above us and he sees all.");
                    }
                    else if (Word1 == "notarget") // Nod to Quake :D
                    {
                        Console.WriteLine("Only in IdTech, will that work.");
                    }
                    else if (Word1 != WordList.Actions.Where(x => x.Contains(Word1)).FirstOrDefault())
                    {
                        Console.WriteLine($"This word is not on the list: {Word1}");
                    }
                    ValidCmd = true;
                }
                else if (Words.Length > 4)  //Entered too many words? Processor takes only 4 for now.
                {
                    Console.Write("Too many words entered.\n");
                    ValidCmd = true;
                }
                else if (Words.Length >= 1 && Word1 == null || Word1 == " " && Word2 == null || Word2 == " ")
                {
                    Console.Write("Bad command or unknown word.\n");
                    ValidCmd = true;
                }

                //Write current cmd to out:
                Console.Write(Cmd);
            }
            void ResetCmd() //should clear cmd buffer of any words entered.
            {
                Word1 = null;
                Word2 = null;
                Word3 = null;
                Word4 = null;
            }
        }
    }


}
