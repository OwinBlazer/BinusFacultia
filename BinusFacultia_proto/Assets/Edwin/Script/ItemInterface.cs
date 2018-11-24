using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class ItemInterface : MonoBehaviour
{
    [SerializeField]
    string name;
    [SerializeField]
    int price;
    [SerializeField]
    int qty;
    public void changeQtyBy(int qtyChange)
    {
        qty += qtyChange;
    }
    public int GetQty()
    {
        return qty;
    }
    public int GetPrice()
    {
        return price;
    }
    public string GetName()
    {
        return name;
    }
    public abstract PlayerAction UseItem(Chara source);
}
