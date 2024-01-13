using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClothController : MonoBehaviour
{

    public string name;
    public int price;
    public int showPrice(TextMeshProUGUI name_, TextMeshProUGUI price_)
    {
        name_.text = name;
        price_.text = "Cost: " + price.ToString();
        return price;
    }
}
