using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static int numberOfCoins;
    public TextMeshProUGUI numberOfCoinsText;
    // Start is called before the first frame update
    void Start()
    {
        numberOfCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        numberOfCoinsText.text = "coins: " + numberOfCoins;
    }
}
