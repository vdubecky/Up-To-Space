using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    [SerializeField]
    private Scrollbar sB;

    [SerializeField]
    private GameObject strana1;
    [SerializeField]
    private GameObject strana2;
    [SerializeField]
    private GameObject strana3;

    [SerializeField]
    private Text popis;

    void Update()
    {
        if (sB.value > 0.75)
        {
            popis.text = "Daily rewards";
            strana1.SetActive(false);
            strana2.SetActive(false);
            strana3.SetActive(true);
        }
        else if (sB.value > 0.25)
        {
            popis.text = "Collect components from materializer";
            strana1.SetActive(false);
            strana2.SetActive(true);
            strana3.SetActive(false);
        }
        else
        {
            popis.text = "Tap to unlock new ship";
            strana1.SetActive(true);
            strana2.SetActive(false);
            strana3.SetActive(false);
        }
    }
}
