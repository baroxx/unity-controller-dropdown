using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VideoOptionsManager : MonoBehaviour
{
    [Header("Video")]
    public TMP_Dropdown Resolution;

    private Resolution[] resolutions;
    private int currentResolutionIndex;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(Resolution.gameObject);
        resolutions = Screen.resolutions;
        List<string> resolutionStrings = new();
        foreach (var res in resolutions)
        {
            resolutionStrings.Add(res.ToString());
        }
        Resolution.ClearOptions();
        Resolution.AddOptions(resolutionStrings);
        currentResolutionIndex = System.Array.IndexOf(resolutions, Screen.currentResolution);
        Resolution.value = currentResolutionIndex;
        Scroll();
    }

    public void OnResolutionChange()
    {
        currentResolutionIndex = Resolution.value;
        var res = resolutions[currentResolutionIndex];
        var width = res.width;
        var height = res.height;
        Screen.SetResolution(width, height, true);
        EventSystem.current.SetSelectedGameObject(Resolution.gameObject);
    }

    public void OnHover()
    {
        if (Resolution.interactable)
        {
            EventSystem.current.SetSelectedGameObject(Resolution.gameObject);
        }
    }

    public void Scroll()
    {
        var items = Resolution.GetComponentsInChildren<Toggle>();
        if (items.Length > 0)
        {
            var selectedItem = items[currentResolutionIndex].GetComponent<RectTransform>();
            var scrollRect = Resolution.GetComponentInChildren<ScrollRect>();

            Canvas.ForceUpdateCanvases();
            var contentPos = (Vector2) scrollRect.transform.InverseTransformPoint(scrollRect.content.position);
            var childPos = (Vector2) scrollRect.transform.InverseTransformPoint(selectedItem.position);
            childPos.y += selectedItem.rect.height;
            var endPos = contentPos - childPos;
            endPos.x = scrollRect.content.anchoredPosition.x;
            scrollRect.content.anchoredPosition = endPos;
        }
    }

    public void Scroll(Toggle toggle)
    {
        var selectedItem = toggle.gameObject.transform;
        var scrollRect = Resolution.GetComponentInChildren<ScrollRect>();

        Canvas.ForceUpdateCanvases();
        var contentPos = (Vector2)scrollRect.transform.InverseTransformPoint(scrollRect.content.position);
        var childPos = (Vector2)scrollRect.transform.InverseTransformPoint(selectedItem.position);
        var endPos = contentPos - childPos;
        endPos.x = scrollRect.content.anchoredPosition.x;
        endPos.y -= 40f;
        scrollRect.content.anchoredPosition = endPos;
    }
}
