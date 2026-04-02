using UnityEngine;
using UnityEngine.UIElements;

public class SanityMeterController : MonoBehaviour
{
    [SerializeField]
    public SanityMeter sanity;

    private SanityMeterUI sanityUI;
    private UIDocument uiDocument;

     void OnEnable()
    {
      VisualElement root = GetComponent<UIDocument>().rootVisualElement;
      root.Q<SanityMeterUI>().dataSource = sanity;



    }

    public void Update()
    {
       
    }
}


