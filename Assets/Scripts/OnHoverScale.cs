using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHoverScale : MonoBehaviour
{
    public string chosenKey;

    public void OnPointerEnter()
    {
        transform.localScale = new Vector2(1.2f, 1.2f);
    }

    public void OnPointerExit()
    {
        transform.localScale = new Vector2(1f, 1f);
    }

    public void ChooseKey()
    {
        transform.parent.GetComponent<KeySelection>().KeySelected(chosenKey);
    }
}
