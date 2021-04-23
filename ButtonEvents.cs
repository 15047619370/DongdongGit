using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEvents : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private bool IsDown;
    private float Delay = 2f;//延迟相当于按下持续时间
    private float LastDownTime;
    private void Update()
    {
        //判断是否属于长按
        if (IsDown)
        {
            if (Time.time - LastDownTime >= Delay)
            {
                Debug.Log("长按");
                LastDownTime = Time.time;
            }
        }
    }
    /// <summary>
    /// 单击按钮一次
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("按钮被按下");
        IsDown = true;
        LastDownTime = Time.time;
    }
    /// <summary>
    /// 按钮抬起
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("按钮抬起");
        IsDown = false;
    }
    /// <summary>
    /// 鼠标离开按钮区域
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("鼠标离开按钮");
        IsDown = false;
    }
}
