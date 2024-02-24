using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpellDataProvider : MonoBehaviour
{
    public List<Spell> spell_list = new List<Spell>();
    private Dictionary<SpellType, Spell> spell_type_to_spell = new();

    public static SpellDataProvider GetObject()
    {
        return FindObjectOfType<SpellDataProvider>();
    }
    
    private void Awake()
    {
        foreach (Spell spell in spell_list)
        {
            spell_type_to_spell.TryAdd(spell.type, spell);
        }
    }

    public Spell GetSpellFromType(SpellType type)
    {
        if (spell_type_to_spell.TryGetValue(type, out Spell spell))
        {
            return spell;
        }

        return spell_type_to_spell[SpellType.None];
    }
}