using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambioEscena : MonoBehaviour
{
    public MovimientoJugador mj;
    // Start is called before the first frame update
    void Start()
    {
        GlobalVariables.level++;
        GlobalVariables.dificult++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
