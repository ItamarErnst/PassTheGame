using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public HpDisplay hp_display;
    public ManaDisplay mana_display;

    public bool HasManaFor(int amount)
    {
        return mana_display.HasEnough(amount);
    }
    
    public void RemoveMana(int amount)
    { 
        mana_display.Remove(amount);
    }
    
    public void AddMana(int amount)
    { 
        mana_display.Add(amount);
    }
}