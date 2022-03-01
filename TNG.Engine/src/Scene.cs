using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNG.Engine;

internal abstract class Scene {
    protected Camera camera;
    private bool isRunning = false;
    private List<GameObject> gameObjects = new List<GameObject>();

    public Scene() {
    }

    public void init() {
    }

    public void start() {
        foreach (GameObject go in gameObjects) {
            go.start();
        }
    }

    public void AddGameObjectToScene(GameObject go) {
        if (!isRunning) {
            gameObjects.Add(go);
        } else {
            gameObjects.Add(go);
            go.start();
        }
    }
}