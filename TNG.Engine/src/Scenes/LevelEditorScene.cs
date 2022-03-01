using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNG.Engine.Scenes {

    internal class LevelEditorScene : Scene {

        public LevelEditorScene() {
            Console.WriteLine("Inside LevelEditorScene!");
            AppWindow.Instance.CanvasColor.X = 0.4f;
            AppWindow.Instance.CanvasColor.Y = 0.4f;
            AppWindow.Instance.CanvasColor.Z = 0.4f;
        }
    }
}