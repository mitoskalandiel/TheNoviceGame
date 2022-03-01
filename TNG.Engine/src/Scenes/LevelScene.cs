using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNG.Engine.Scenes {

    internal class LevelScene : Scene {

        public LevelScene() {
            Console.WriteLine("Inside LevelScene!");
            AppWindow.Instance.CanvasColor.X = 0.7f;
            AppWindow.Instance.CanvasColor.Y = 0.7f;
            AppWindow.Instance.CanvasColor.Z = 0.7f;
        }
    }
}