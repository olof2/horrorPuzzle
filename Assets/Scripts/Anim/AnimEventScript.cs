using UnityEngine;

public class AnimEventScript : MonoBehaviour        //animation event script. Placera scriptet på ett objekt som ska köra en animation som sin event från sanity metern
{
    private Animator anim;

    public string triggerName = "isFalling";        //sätt triggerName i inspect till namnet på animationens SetTrigger namn. "isFalling" som default, placeholder anim's setTrigger

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void triggerEvent()                      //när "triggerEvent()" metoden anropas, spelar animation attatched till objektet med "triggerName" som sin SetTrigger()
    {
        anim.SetTrigger(triggerName);
    }
}