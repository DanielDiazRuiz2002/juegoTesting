using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables
{
    public static int level
    {
        get { return PlayerPrefs.GetInt("Level", 0); }
        set { PlayerPrefs.SetInt("Level", value); }
    }

    public static int dificult
    {
        get { return PlayerPrefs.GetInt("Dificult", 0); }
        set { PlayerPrefs.SetInt("Dificult", value); }
    }
}

