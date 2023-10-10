using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TouchMobieRight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Player player;
    public void OnPointerDown(PointerEventData eventData)
    {
        player.SetMoveRight();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.moveRight = false;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        player.moveRight = false;

    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        player.SetMoveRight();
    }
}
