/*
Interactive Fiction Command line interpreter
Actor.cs

© 2021 Iain Marais (il-Salvatore on Github)
Licence: Apache v2.0 or 3-clause BSD Licence

Please see www.apache.org/licenses/LICENSE-2.0.html || opensource.org/licenses/BSD-3-Clause for more information.

The scope of this project is to build a simple but efficient command line interpreter for a console-based interactive fiction engine,
Think classic Zork, where one entered commands and read the output.

This project will be entirely c# based.
*/

using System;
using System.Collections.Generic;


namespace InteractiveFiction_CLI
{
    public class Actor
    {
        public string Name { get; set; }
        List<Object> ActorInventory = new();
        public Actor()
        {

        }
        public class Player : Actor
        {
            public List<Object> PlayerInventory = new();
            public Player()
            {

            }
        }

    }
}