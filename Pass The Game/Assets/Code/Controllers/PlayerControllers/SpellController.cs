using System;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    private HeroData heroData;
    private SpellDataProvider spell_data_provider;
    
    public SpellType selectedSpellType = SpellType.None;

    private void Awake()
    {
        spell_data_provider = SpellDataProvider.GetObject();
    }

    void Start()
    {
        PlayerEvents.OnSpellSelect.AddListener(SelectSpell);
    }

    public void SetHero(HeroData heroData)
    {
        this.heroData = heroData;
    }

    public void CastSelectedSpell(Vector3 pos)
    {
        ParticleSystem pr = Instantiate(GetSelectedSpell().pr);
        pr.transform.position = pos;

        ClearSelection();
    }
    
    private void SelectSpell(int ix)
    {
        selectedSpellType = heroData.GetSpellType(ix);
        PlayerEvents.OnSpellSelected.Invoke(GetSelectedSpell());
    }
    
    private void ClearSelection()
    {
        selectedSpellType = SpellType.None;
    }

    public Spell GetSelectedSpell()
    {
        return spell_data_provider.GetSpellFromType(selectedSpellType);
    }

    public float GetCastRange()
    {
        return GetSelectedSpell().cast_range;
    }

    public int GetSpellMana()
    {
        return GetSelectedSpell().mana;
    }
}