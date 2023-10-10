using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TouchMobieLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler,IPointerEnterHandler
{
    public Player player;
    public void OnPointerDown(PointerEventData eventData)
    {
        player.SetMoveLeft();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        player.SetMoveLeft();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        player.moveLeft = false;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.moveLeft = false;
    }
}
