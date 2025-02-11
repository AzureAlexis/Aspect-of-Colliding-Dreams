using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class MenuTab : MonoBehaviour
{
    public void Update(){}

    public void UpdateActive()
    {
        UpdateReminders(-1);
    }

    public void UpdateInactive()
    {
        UpdateReminders(1);
    }

    void UpdateReminders(int target)
    {
        Transform tabGuide = transform.GetChild(transform.childCount - 1);
        Color color = tabGuide.GetComponent<Image>().color;
        color.a += Time.deltaTime * 4 * target;
        color.a = Mathf.Min(1, color.a);
        color.a = Mathf.Max(0, color.a);

        tabGuide.GetComponent<Image>().color = color;
        tabGuide.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = color;
        tabGuide.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = color;
        tabGuide.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = color;
        tabGuide.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = color;

    }
}
