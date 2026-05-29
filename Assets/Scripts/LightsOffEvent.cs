using System.Collections.Generic;
using UnityEngine;

//script för att stänga av alla ljus objekten i
//en lista av ljus objekt.
//Stänger av alla ljusen vid 10% sanity (1 minut), samma tid som
//minor anim events händer
public class LightsOffEvent : MonoBehaviour
{
    //List av alla Lights som ska stängas av
    public List<Light> lights;

    //vid start, subscribe till "OnReached10" eventet
    //vid "OnReached10", starta "TurnLightsOff" metoden
    private void Start()
    {
        if (SanityMeter.Instance != null)   //failsafe
        {
            SanityMeter.Instance.OnReached10 += TurnLightsOff;
        }
    }

    private void TurnLightsOff()
    {
        foreach (Light light in lights)
        {
            light.enabled = false;
        }

        Debug.Log("All lights turned off");
    }

    private void OnDestroy()
    {
        if (SanityMeter.Instance != null) //failsafe
        {
            SanityMeter.Instance.OnReached10 -= TurnLightsOff;
        }
    }
}
