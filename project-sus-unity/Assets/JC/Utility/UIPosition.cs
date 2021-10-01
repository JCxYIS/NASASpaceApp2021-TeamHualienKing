using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace JC.Utility
{
    public static class UIPosition
    {
        /// <summary>
        /// 世界座標轉 UGUI 座標
        /// </summary>
        /// <param name="worldPos">transform.position</param>
        /// <param name="canvas">canvas 的 RectTransform</param>
        /// <param name="mainCam">Camera.main，看你有沒有快取起來</param>
        /// <returns>項目在此 Canvas 的 Anchored Position</returns>
        public static Vector2 WorldToCanvasPos(Vector3 worldPos, RectTransform canvas, Camera mainCam = null)
        {            
            if(mainCam == null)
                mainCam = Camera.main;

            Vector2 screenPos = mainCam.WorldToViewportPoint(worldPos); // 世界物件在螢幕上的座標，螢幕左下角為(0,0)，右上角為(1,1)
            Vector2 viewPos = (screenPos - canvas.pivot) * 2; // 世界物件在螢幕上轉換為UI的座標，UI的Pivot point預設是(0.5, 0.5)，這邊把座標原點置中，並讓一個單位從0.5改為1
            float width = canvas.rect.width / 2; // UI一半的寬，因為原點在中心
            float height = canvas.rect.height / 2; // UI一半的高
            return new Vector2(viewPos.x * width, viewPos.y * height); // 回傳UI座標        
        }

        /// <summary>
        /// UI 轉世界座標
        /// </summary>
        /// <param name="canvas">canvas 的 RectTransform</param>
        /// <param name="uiPos"></param>
        /// <returns></returns>
        public static Vector3 CanvasToWorldPos(RectTransform canvas, Vector3 uiPos)
        {
            float width = canvas.rect.width / 2; //UI一半的寬
            float height = canvas.rect.height / 2; //UI一半的高
            Vector3 screenPos = new Vector3(
                ((uiPos.x / width) + 1f) / 2, 
                ((uiPos.y / height) + 1f) / 2, 
                uiPos.z); 
            return Camera.main.ViewportToWorldPoint(screenPos);
        }
    }
}