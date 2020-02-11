using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    private static Tooltip instance;

    [SerializeField] private Camera uiCamera;
    [SerializeField] private TextMeshProUGUI tooltipText;
    [SerializeField] private RectTransform backgroundRectTransfom;

    [SerializeField] private RectTransform backgroundColorRectTransfom, frameRectTransform, fadeRectTransform;

    [SerializeField] private float textPaddingSize = 4f;
    private Vector2 localPoint, backgroundSize;

    [SerializeField] private RectTransform canvasRectTransform;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        localPoint = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform,
            Input.mousePosition, uiCamera,
            out localPoint);
        
            transform.localPosition = localPoint;
    }    

    public void ShowTooltip(string text)
    {
        gameObject.SetActive(true);

        tooltipText.text = text;
        
        backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2f,
            tooltipText.preferredHeight + textPaddingSize * 2f);
        
        backgroundRectTransfom.sizeDelta = backgroundSize;
        backgroundColorRectTransfom.sizeDelta = backgroundSize;
        frameRectTransform.sizeDelta = backgroundSize;
        fadeRectTransform.sizeDelta = backgroundSize;
    }

    public void HideTooltip() { gameObject.SetActive(false); }

    public static void ShowTooltip_Static(string textArea) { instance.ShowTooltip(textArea); }

    public static void HideTooltip_static() { instance.HideTooltip(); }
}