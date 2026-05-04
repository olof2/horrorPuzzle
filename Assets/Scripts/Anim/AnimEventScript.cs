using UnityEngine;

//Script för animerade events som kan triggas.
//Animationen som spelas är baserat på vilken triggerName
//som objektet har. Sätter triggerName i Inspect
public class AnimEventScript : MonoBehaviour
{
    private Animator anim;

    //public triggerName för vilken isTrigger animation objektet här
    //skriver namnet i Inspect ("isFalling" är default)
    public string triggerName;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    //När "TriggerEvent" anropas så startar metoden SetTrigger
    //med triggerName stringen som skrivs i Inspect
    public void TriggerEvent()
    {
        anim.SetTrigger(triggerName);
    }
}