using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class luzMenu : MonoBehaviour
{
    float minV = 0.45f;
    float maxV = 0.65f;
    float speed = 1.3f;

    // Start is called before the first frame update
    void Start()
    {
        System.Random random = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        Color.RGBToHSV(GetComponent<Image>().color, out float h, out float s, out float v);

        // Ajustar el valor de V
        v = Mathf.Lerp(minV, maxV, Mathf.PingPong(Time.time * speed, 1));

        // Establecer el nuevo color del Image
        GetComponent<Image>().color = Color.HSVToRGB(h, s, v);
    }
}
