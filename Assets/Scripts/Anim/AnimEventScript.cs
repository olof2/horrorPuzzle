using Unity.VisualScripting;
using UnityEngine;

//Script för animerade events som kan triggas.
//Animationen som spelas är baserat på vilken triggerName
//som objektet har. Sätter triggerName i Inspect
public class AnimEventScript : MonoBehaviour
{
    private Animator anim;
    private AudioSource audioSource;


    //public triggerName för vilken isTrigger animation objektet här
    //skriver namnet i Inspect ("isFalling" är default)
    public string triggerName;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        //Debug.Log($"{gameObject.name} Animator found? {anim != null}");
    }

    //När "TriggerEvent" anropas så startar metoden SetTrigger
    //med triggerName stringen som skrivs i Inspect
    public void TriggerEvent()
    {
        //Debug.Log($"TriggerEvent on: {gameObject.name} | ID: {GetInstanceID()}");

        if (anim == null)
        {
            Debug.LogError($"NO ANIMATOR on {gameObject.name}");
            return;
        }

        anim.SetTrigger(triggerName);

        Debug.Log($"Trigger sent to Animator");
    }
}