using UnityEngine;

public class SpellController : MonoBehaviour
{
    public Spell spell;
    public Spell fire_ball;
    
    public int selectedSpell = -1;

    void Start()
    {
        PlayerEvents.OnSpellSelect.AddListener(SelectSpell);
    }

    public void CastSelectedSpell(Vector3 pos)
    {
        ParticleSystem pr = Instantiate(GetSelectedSpell().pr);
        pr.transform.position = pos;

        ClearSelection();
    }
    
    private void SelectSpell(int spellIndex)
    {
        selectedSpell = spellIndex;
    }
    
    private void ClearSelection()
    {
        selectedSpell = -1;
    }

    public Spell GetSelectedSpell()
    {
        if (selectedSpell == -1)
        {
            return spell;
        }

        return fire_ball;
    }

    public bool HasSpellSelected()
    {
        return selectedSpell != -1;
    }

    public float GetCastRange()
    {
        return GetSelectedSpell().cast_range;
    }
}