using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHoverScale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter()
    {
        transform.localScale = new Vector2(1.2f, 1.2f);
    }

    public void OnPointerExit()
    {
        transform.localScale = new Vector2(1f, 1f);
    }
}
