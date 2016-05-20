using UnityEngine;
using System.Collections;
using System;

public enum AutoMove
{
    NONE,
    Straight,
    MoveAround,
    Rotate
}

public static class AutoMoves {

    public static MonoBehaviour GetScript(AutoMove am)
    {
        switch(am)
        {
            case AutoMove.Straight: return new Straight();
            case AutoMove.MoveAround: return new MoveAround();
            case AutoMove.Rotate: return new Rotate();
            default: return null;
        }
    }

    public static AutoMove GetEnum(MonoBehaviour script)
    {
        if (script == null) return AutoMove.NONE;

        if (script.GetType() == typeof(Straight))
            return AutoMove.Straight;
        if (script.GetType() == typeof(Rotate))
            return AutoMove.Rotate;
        if (script.GetType() == typeof(MoveAround))
            return AutoMove.MoveAround;

        return AutoMove.NONE;
    }

    public static Type GetScriptType(AutoMove am)
    {
        switch(am)
        {
            default: return null;
            case AutoMove.MoveAround: return typeof(MoveAround);
            case AutoMove.Rotate: return typeof(Rotate);
            case AutoMove.Straight: return typeof(Straight);
        }
    }

}
