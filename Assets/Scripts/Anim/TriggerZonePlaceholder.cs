using UnityEngine;
using System.Collections.Generic;

public class TriggerZonePlaceholder : MonoBehaviour     //script för "trigger zones". Event's triggas inuti zones
{
    public bool PlayerInsideZone = false;               //bool för om player är inuti zone eller ej

    public List<AnimEventScript> animsInZone;           //list för animation events inuti zone
    //om vi gör om detta script till event script istället för animation script så kan vi lägga till andra events här som lights och sound effects

    public float sanityMeterThreshold = 25f;            //sanity meter level där events kan börja triggas (25 som default)

    private float eventCooldownTimer = 0f;              //cooldown timer för events + min och max cooldown time (6 - 16 som default)
    public float minCooldownTime = 6f;
    public float maxCooldownTime = 16f;

    private void Update()
    {
        if (SanityMeter.Instance == null) return;                                                   //failsafe, om singleton inte finns

        float sanityLevel = SanityMeter.Instance.sanityLevel;

        if (sanityLevel >= sanityMeterThreshold && PlayerInsideZone && eventCooldownTimer <= 0f)   //random event triggas om sanityLevel är större eller lika med 25, player är
        {                                                                                          //i trigger zone, och event cooldown är mindre eller lika med 0
            triggerRandomEvent();

            eventCooldownTimer = Random.Range(minCooldownTime, maxCooldownTime);                    //random cooldown time för events
        }

        if (eventCooldownTimer > 0f)                                                                //räkna ner event cooldown (8 sec)
        {
            eventCooldownTimer -= Time.deltaTime;
        }
    }

    private void triggerRandomEvent()
    {
        //just nu så finns det bara animsInZone som option men senare kan man kanske lägga till de andra typer av events i triggerRandomEvent() metoden
        if (animsInZone.Count == 0) return;                 //failsafe, om det inte finns några animation events i listan, return

        int index = Random.Range(0, animsInZone.Count);     //random int mellan 0 och mängden av animationer som finns i animsInZone listan

        animsInZone[index].triggerEvent();                  //anropar "triggerEvent" metoden från animEventScriptet. Spelar en random event från animsInZone listan

        animsInZone.RemoveAt(index);                        //efter animationen vid "index" i listan spelas, ta bort den från listan så den inte kan spelas igen
    }

    private void OnTriggerEnter(Collider other)         //använder "isTrigger" med empty GameObject som "trigger zone". Om player är i zone, true, om utanför zone, false
    {
        if (other.CompareTag("Player"))
        {
            PlayerInsideZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInsideZone = false;
        }
    }
}
