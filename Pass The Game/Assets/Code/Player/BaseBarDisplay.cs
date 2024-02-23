using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseBarDisplay : MonoBehaviour
{
    public List<Image> fill_display;
    private int max;
    public int current;

    void Start()
    {
        max = fill_display.Count * 10;
        current = max;
        UpdateBars();
    }

    void UpdateBars()
    {
        int fullBars = current / 10;
        float remainingHealth = current % 10;

        for (int i = 0; i < fill_display.Count; i++)
        {
            if (i < fullBars)
            {
                fill_display[i].fillAmount = 1f;
            }
            else if (i == fullBars)
            {
                fill_display[i].fillAmount = remainingHealth / 10f;
            }
            else
            {
                fill_display[i].fillAmount = 0f;
            }
        }
    }


    public void Remove(int amount)
    {
        current -= amount;
        current = Mathf.Clamp(current, 0, max);

        UpdateBars();

        if (current == 0)
        {
            Debug.Log("Player is defeated!");
        }
    }

    public void Add(int amount)
    {
        current += amount;
        current = Mathf.Clamp(current, 0, max);

        UpdateBars();
    }

    public bool HasEnough(int amount)
    {
        return current >= amount;
    }
}