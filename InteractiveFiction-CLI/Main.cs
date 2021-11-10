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
        {
            Actor.Player myPlayer = new("Garrett", 20, false, false);
            myPlayer.PlayerInventory = new();
            Logic.SetScene();
            CommandProcessor.Command myCommand = new();
            do
            {
                myCommand.GetCmd();
            } while (CommandProcessor.Command.ValidCmd == true);

        }
    }
}