using System;

[Serializable]
public class Hero
{
    public float speed = 5;
    public float attack_range = 5f;
    
    public Spell spell_slot_q;
    public Spell spell_slot_w;
    public Spell spell_slot_e;
    public Spell spell_slot_r;
    
    public Spell GetSpellSlotQ()
    {
        return spell_slot_q;
    }
    
    public Spell GetSpellSlotW()
    {
        return spell_slot_w;
    }
    
    public Spell GetSpellSlotE()
    {
        return spell_slot_e;
    }
    
    public Spell GetSpellSlotR()
    {
        return spell_slot_r;
    }
}
