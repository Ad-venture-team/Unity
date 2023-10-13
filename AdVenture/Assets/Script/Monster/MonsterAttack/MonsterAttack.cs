using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterAttack
{
    public abstract void DoAttack(Monster _launcher, Transform _target);

    public abstract void DrawPreview(Monster _launcher, Transform _target);

    public virtual void DrawGizmo(Monster _launcher) { }
}
