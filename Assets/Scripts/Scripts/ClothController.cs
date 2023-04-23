using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClothController : MonoBehaviour
{
    // Start is called before the first frame update

    public string name;
    public int price;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int showPrice(TextMeshProUGUI name_, TextMeshProUGUI price_)
    {
        name_.text = name;
        price_.text = "Cost: " + price.ToString();
        return price;
    }
}
