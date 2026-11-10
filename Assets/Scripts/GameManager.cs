using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    Item[] items;

    public int luckyNumber;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        items = Array.Empty<Item>();
        items = FindObjectsByType<Item>();

        AssignItems();
    }

    void AssignItems()
    {
        if (items != null && items.Length > 0)
        {
            for (int i = items.Length - 1; i > 0; i--)
            {
                int rnd = UnityEngine.Random.Range(0, i);

                Item temp = items[i];

                items[i] = items[rnd];
                items[rnd] = temp;
            }

            int index = 0;
            foreach (var item in items)
            {
                if (index < 3)
                {
                    item.numberNeeded = instance.luckyNumber;
                }
                else
                {
                    int num = instance.luckyNumber;
                    while (num == instance.luckyNumber)
                    {
                        num = UnityEngine.Random.Range(1, 11);
                    }
                    item.numberNeeded = num;
                }

                index++;
            }
        }
    }
}
