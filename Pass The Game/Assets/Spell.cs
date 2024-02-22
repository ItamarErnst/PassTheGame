using System;
using UnityEngine;

public enum SpellType
{
    None = 0,
    FireBall = 1,
    
}

[Serializable]
public class Spell
{
    public SpellType type = SpellType.None;
    public int mana;
    public ParticleSystem pr;

    public Spell()
    {
        type = SpellType.None;
        mana = 0;
        pr = null;
    }
}