using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoldController : MonoSingletonPersistent<GoldController>
{
    public int gold;
    public GameObject goldText;

    // Start is called before the first frame update
    void Start()
    {
        gold = 20;
    }

    public int getGold() {
        return this.gold;
    }

    // Update is called once per frame
    void Update()
    {
        goldText.GetComponent<Text>().text = gold.ToString();
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
