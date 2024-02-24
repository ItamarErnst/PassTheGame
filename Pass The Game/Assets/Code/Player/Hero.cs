using System;

[Serializable]
public class Hero
{
    public float speed = 5;
    public float attack_range = 5f;
    
    public SpellType spell_slot_q;
    public SpellType spell_slot_w;
    public SpellType spell_slot_e;
    public SpellType spell_slot_r;

    public SpellType GetSpellType(int ix)
    {
        if (ix == 1)
        {
            return GetSpellSlotQ();
        }
        else if (ix == 2)
        {
            return GetSpellSlotW();
        }
        else if (ix == 3)
        {
            return GetSpellSlotE();
        }
        else
        {
            return GetSpellSlotR();
        }
    }
    
    public SpellType GetSpellSlotQ()
    {
        return spell_slot_q;
    }
    
    public SpellType GetSpellSlotW()
    {
        return spell_slot_w;
    }
    
    public SpellType GetSpellSlotE()
    {
        return spell_slot_e;
    }
    
    public SpellType GetSpellSlotR()
    {
        return spell_slot_r;
    }
}
