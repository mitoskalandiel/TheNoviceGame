using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNG.Engine;

internal abstract class Component {
    public GameObject gameObject = null;

    public void start() {
    }

    public abstract void update(float dt);
}