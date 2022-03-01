using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TNG.Engine;

/// <summary>
/// This represents our Entity System, to initialize a game object on any scene, do this:
/// GameObject go = new GameObject("GameObject Name");   <- Create a (new) GameObject
/// go.addComponent(new SpriteRenderer());               <- add 1 (or more) components
/// this.addGameObjectToScene(go);                       <- add the GameObject to the scene
/// -> DONE <-
/// </summary>
internal class GameObject {

    /// <summary>
    /// What's the name of the GamObject
    /// </summary>
    private readonly string name;

    /// <summary>
    /// A list of components in this GameObject
    /// </summary>
    private readonly IList<Component> components;

    /// <summary>
    /// GameObject constructor
    /// </summary>
    /// <param name="name"> Name of the GameObject </param>
    public GameObject(string name) {
        this.name = name;
        this.components = new List<Component>();
    }

    /// <summary>
    /// This (generic) function returns the component once its type has been confirmed
    /// </summary>
    /// <param name="componentClass"> the component class </param>
    /// @param <T>            the type of the component class </param>
    /// <returns> the component cast into the componentClass type </returns>
    public virtual T getComponent<T>(Type componentClass) where T : Component {
        foreach (Component c in components) {
            if (componentClass.IsAssignableFrom(c.GetType())) {
                try {
                    return (T)c;
                } catch (System.InvalidCastException e) {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                    Debug.Assert(false, "ERROR: Casting component '" + componentClass.Name + "'");
                }
            }
        }
        return default(T);
    }

    /// <summary>
    /// This removes a component from the component list
    /// </summary>
    /// <param name="componentClass"> the component class </param>
    /// @param <T>            the type of the component class </param>
    public virtual void removeComponent<T>(Type componentClass) where T : Component {
        for (int i = 0; i < components.Count; i++) {
            Component c = components[i];
            if (componentClass.IsAssignableFrom(c.GetType())) {
                components.RemoveAt(i);
                return;
            }
        }
    }

    /// <summary>
    /// add a component to our list of component
    /// </summary>
    /// <param name="c"> component </param>
    public virtual void addComponent(Component c) {
        this.components.Add(c);
        c.gameObject = this;
    }

    /// <summary>
    /// Update the component according to deltaT
    /// </summary>
    /// <param name="dt"> float value representing deltaT </param>
    public virtual void update(float dt) {
        for (int i = 0; i < components.Count; i++) {
            components[i].update(dt);
        }
    }

    /// <summary>
    /// Start the game object components
    /// </summary>
    public virtual void start() {
        for (int i = 0; i < components.Count; i++) {
            components[i].start();
        }
    }
}