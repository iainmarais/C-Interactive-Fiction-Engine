using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveFiction_CLI
{
    public class Menu
    {
        public void MainMenu()
        {
            Console.WriteLine("Enter new to start a new scene or quit to exit.");
            string Choice = Console.ReadLine();
            if (Choice != null)
            {
                switch (Choice)
                {
                    case "new":
                        Console.Write("Enter Scene number: ");
                        int SceneIndex = int.Parse(Console.ReadLine());
                        SelectScene(SceneIndex);
                        break;
                    default:
                        break;
                }
            }
        }
        public Scene SelectScene(int SceneIndex)
        {
            List<Scene> myScenes = new();
            Scene myScene = new();
            switch (SceneIndex)
            {
                case 1:
                case 2:
                    myScene = myScene.SetUpScene(SceneIndex, myScenes);
                    break;
                default:
                    break;
            }
            return myScene;
        }
    }
}
