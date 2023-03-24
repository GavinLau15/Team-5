using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldController : MonoSingletonPersistent<GoldController>
{
    public int gold;
    public TextMeshProUGUI goldText;

    // Start is called before the first frame update
    void Start()
    {
        gold = 100;
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = gold.ToString();
    }

    public void AddGold(int amount)
    {
        gold += amount;
    }

    public void SubtractGold(int amount)
    {
        gold -= amount;
    }
}
