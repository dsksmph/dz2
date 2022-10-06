using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Image joystickBackgroundImage;
    private Image joystickHandleImage;
    public Vector2 inputDir { set; get; }
    public float offset;

    private void Start()
    {
        joystickBackgroundImage = GetComponent<Image>();
        joystickHandleImage = transform.GetChild(0).GetComponent<Image>();
        inputDir = Vector2.zero;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = Vector2.zero;
        float joystickBackgroundImageSizeX = joystickBackgroundImage.rectTransform.sizeDelta.x;
        float joystickBackgroundImageSizeY = joystickBackgroundImage.rectTransform.sizeDelta.y;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackgroundImage.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x /= joystickBackgroundImageSizeX;
            pos.y /= joystickBackgroundImageSizeY;
            inputDir = new Vector2(pos.x, pos.y);
            inputDir = inputDir.magnitude > 1 ? inputDir.normalized : inputDir;
            joystickHandleImage.rectTransform.anchoredPosition = new Vector2(inputDir.x * (joystickBackgroundImageSizeX / offset), inputDir.y * (joystickBackgroundImageSizeY / offset));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputDir = Vector2.zero;
        joystickHandleImage.rectTransform.anchoredPosition = Vector2.zero;
    }
}
