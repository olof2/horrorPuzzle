using UnityEngine;
using System.Collections.Generic;

//Script för TriggerZones, Zones i spelet där events som Animationer
//kan triggas. Zonen är en empty game objects och när playern går in i en zone
//så registerar zonen att en player har entered, sen om requirements för en
//event kan triggas (sanity meter = 25,50,75%) och player är i zonen då
//väljs en random animation från listorna av animerade objekt och startar animationen
public class TriggerZonePlaceholder : MonoBehaviour
{
    public bool PlayerInsideZone = false;

    //bool för om minor, moderate, eller major events kan triggas (unlocked).
    //också bool för overtime som används för overtime event cooldown
    private bool minorEventUnlocked = false;
    private bool moderateEventUnlocked = false;
    private bool majorEventUnlocked = false;
    private bool overtimeUnlocked = false;

    //list för de olika tiered animationer i zonen
    //event tiers är minor, moderate, och major events
    public List<AnimEventScript> minorEvents;
    public List<AnimEventScript> moderateEvents;
    public List<AnimEventScript> majorEvents;

    //cooldown timer för events + min och max cooldown time (6 - 16 som default)
    //efter event triggas (animation spelas) så startas en cooldown
    private float eventCooldownTimer = 0f;
    public float minCooldownTime = 6f;
    public float maxCooldownTime = 16f;

    //vid start av spelet, "subscribe" till de event Actions från SanityMeter
    //scriptet som TriggerZone scriptet ska använda (onReached25/50/75/100)
    private void Start()
    {
        //failsafe
        if (SanityMeter.Instance == null) return;

        SanityMeter.Instance.OnReached25 += UnlockMinorEvents;
        SanityMeter.Instance.OnReached50 += UnlockModerateEvents;
        SanityMeter.Instance.OnReached75 += UnlockMajorEvents;
        SanityMeter.Instance.OnReached100 += UnlockOvertime;
    }

    //När event Action från sanity meter signalera att "Event händer nu", då anropas
    //metoden som är subscribed till evented som signalerades och boolen blir true.
    //när boolen blir true, då kan minor/moderate/major events hända, 100 är för overtime
    private void UnlockMinorEvents()
    {
        minorEventUnlocked = true;
    }
    private void UnlockModerateEvents()
    {
        moderateEventUnlocked = true;
    }
    private void UnlockMajorEvents()
    {
        majorEventUnlocked = true;
    }
    private void UnlockOvertime()
    {
        overtimeUnlocked = true;
    }

    private void Update()
    {
        //failsafe
        if (!PlayerInsideZone) return;

        //så länge eventCooldownTimer är större än 0, sänk det varje sekund 
        if (eventCooldownTimer > 0f)
        {
            eventCooldownTimer -= Time.deltaTime;
            return;
        }

        //när event cooldown är slut, provar att retunera bool från
        //TryTriggerRandomEvent() metoden.
        bool played = TryTriggerRandomEvent();

        //om retunerar true (animation spelades), resetar eventCooldownTimer
        if (played)
        {
            eventCooldownTimer = GetCooldown();
        }
    }

    //Metod som retunerar en Random.Range() för eventCooldownTimern
    //storleken på min och max float i Random.Range är baserat på event nivå
    private float GetCooldown()
    {
        if (overtimeUnlocked) return Random.Range(4f, 6f);
        else if (majorEventUnlocked) return Random.Range(5f, 10f);
        else if (moderateEventUnlocked) return Random.Range(6f, 12f);
        else if (minorEventUnlocked) return Random.Range(6f, 16f);

        return Random.Range(minCooldownTime, maxCooldownTime);
    }

    //Bool metod som randomly väljer ett animerad objekt från en eller flera listor av
    //objekt och spelar det objektets animation
    private bool TryTriggerRandomEvent()
    {
        //skapar en ny lista för AnimEventScripts som heter "available"
        //som innehåller currently alla "available" events. Skapar listan varje anrop
        List<AnimEventScript> available = new List<AnimEventScript>();

        //kollar om events är unlocked. Om de är, lägg till listorna med
        //det nivå av event animationer till available listan
        if (minorEventUnlocked)
        {
            available.AddRange(minorEvents);
        }
        if (moderateEventUnlocked)
        {
            available.AddRange(moderateEvents);
        }
        if (majorEventUnlocked)
        {
            available.AddRange(majorEvents);
        }

        //failsafe
        if (available.Count == 0) return false;

        //"chosen" är en variable av en randomly chosen anim från available listan
        var chosen = available[Random.Range(0, available.Count)];

        //efter en randomly "chosen" objekt från listan av available animerade objekt
        //har valts, anropa det objektets TriggerEvent() metod (spelar dens animation)
        chosen.TriggerEvent();

        //efter animationen har spelats, ta bort den från alla listorna
        //(även om animationen bara finns i en av listorna så händer inga errors, det är safe)
        minorEvents.Remove(chosen);
        moderateEvents.Remove(chosen);
        majorEvents.Remove(chosen);

        //efter animationen har spelats, retunerar true
        return true;
    }

    //använder "isTrigger" med empty GameObject som "trigger zone".
    //Om player är i zone, true, om utanför zone, false
    private void OnTriggerEnter(Collider other)
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

    //vid avslutning av spelet, OnDestroy händer och scriptet "unsubscribe" från
    //de event Actions som den subscribed till i "Start().
    //Utan OnDestroy så blir events dubbel-subscribade
    private void OnDestroy()
    {
        //failsafe
        if (SanityMeter.Instance == null) return;

        SanityMeter.Instance.OnReached25 -= UnlockMinorEvents;
        SanityMeter.Instance.OnReached50 -= UnlockModerateEvents;
        SanityMeter.Instance.OnReached75 -= UnlockMajorEvents;
        SanityMeter.Instance.OnReached100 -= UnlockOvertime;
    }
}
