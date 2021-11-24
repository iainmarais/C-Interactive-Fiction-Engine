/*
Interactive Fiction Command line interpreter
Main.cs

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

    class Program
    {
        static void Main(string[] args)
        {   //Main menu

            //Set up new scene object, and a new instance list of scenes,
            //then assign the scene based on the scene index returned by a menu function, to be created still.
            //Test: Console output of current scene name from scenes list
            //needs further testing: 
            //Menu myMenu = new();
            //myMenu.MainMenu();
            Actor.Player myPlayer = new("Garrett", 20, false, false);
            myPlayer.PlayerInventory = new();
            List<Scene> MyScenes = new();
            Scene myScene = new();
            int SceneIndex = 0;
            bool ValidChoice = false;
            while (ValidChoice != true)
            {
                SceneIndex = int.Parse(Console.ReadLine());
                if (SceneIndex != 0)
                {
                    ValidChoice = true;
                    break;
                }
                else
                {
                    ValidChoice = false;
                }
            }
            myScene = myScene.SetUpScene(SceneIndex, MyScenes);
            CommandProcessor.Command myCommand = new();
            do
            {
                myCommand.GetCmd();
            } while (CommandProcessor.Command.ValidCmd == true);

        }
    }
}