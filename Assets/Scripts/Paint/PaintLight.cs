using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HimeLib;


//Only Single color Draw version
public class PaintLight : MonoBehaviour
{
    [Header("Setting")]
    public List<float> sizeValues;

    [Header("Paint Canvas")]
    public Color DefaultColor;
	public Painter painterCanvas;
    public Material drawOtherRtMat;
    public Material matForCopy;

    [Header("Canvas Camera")]
    public Camera canvasCamera;

    public System.Action<Vector2> OnDrawStart;
    public System.Action<Vector2> OnDrawDrag;
    public System.Action OnDrawEnd;

    public bool canDrawing;
    bool _isMouseDown = false;

    void Update()
    {
        // // 遍历所有触控点
        // for (int i = 0; i < Input.touchCount; i++)
        // {
        //     Touch touch = Input.GetTouch(i);

        //     // 获取触控点的唯一标识符
        //     int touchId = touch.fingerId;

        //     if (touch.phase == TouchPhase.Began)
        //     {
        //         // 记录起点
        //         if (!touchDataDict.ContainsKey(touchId))
        //         {
        //             touchDataDict[touchId] = new TouchData();
        //         }
        //         touchDataDict[touchId].startPoint = touch.position;
        //         Debug.Log("Touch " + touchId + " began at " + touch.position);
        //     }
        //     else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        //     {
        //         // 记录拖曳点
        //         if (touchDataDict.ContainsKey(touchId))
        //         {
        //             touchDataDict[touchId].dragPoints.Add(touch.position);
        //             Debug.Log("Touch " + touchId + " moved to " + touch.position);
        //         }
        //     }
        //     else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        //     {
        //         // 记录终点
        //         if (touchDataDict.ContainsKey(touchId))
        //         {
        //             touchDataDict[touchId].endPoint = touch.position;
        //             Debug.Log("Touch " + touchId + " ended at " + touch.position);

        //             // 在这里处理完成的触控数据，例如打印或存储
        //             PrintTouchData(touchId);

        //             // 移除记录的数据
        //             touchDataDict.Remove(touchId);
        //         }
        //     }
        // }


        //#if !UNITY_ANDROID
        if (Input.GetMouseButtonUp(0) && _isMouseDown)
        {
            painterCanvas.EndDraw();
            OnDrawEnd?.Invoke();
            _isMouseDown = false;
        }

        if(!canDrawing) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            _isMouseDown = true;
            //Draw once when mouse down.

            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(painterCanvas.transform as RectTransform, Input.mousePosition, canvasCamera, out pos))
            {
                painterCanvas.ClickDraw(pos, canvasCamera, painterCanvas.penMat.mainTexture, painterCanvas.brushScale, painterCanvas.penMat, painterCanvas.renderTexture, true);
                OnDrawStart?.Invoke(pos);
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (_isMouseDown)
            {
                //draw on mouse drag.
                Vector2 pos;
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(painterCanvas.transform as RectTransform, Input.mousePosition, canvasCamera, out pos))
                {
                    painterCanvas.Drawing(pos, canvasCamera, painterCanvas.renderTexture, false, true);
                    OnDrawDrag?.Invoke(pos);
                }
            }
        }
        //#endif
    }

    public void DrawStartLight(Vector2 pos){
        painterCanvas.ClickDraw(pos, canvasCamera, painterCanvas.penMat.mainTexture, painterCanvas.brushScale, painterCanvas.penMat, painterCanvas.renderTexture, true);
    }

    public void DrawDragLight(Vector2 pos){
        painterCanvas.Drawing(pos, canvasCamera, painterCanvas.renderTexture, false, true);
    }

    public void DrawEndLight(){
        painterCanvas.EndDraw();
    }

    public void DefaultUserSettingLight(){
        painterCanvas.brushScale = sizeValues[0];
        painterCanvas.canvasMat.SetFloat("_Alpha", 1);
        painterCanvas.canvasMat.SetTexture("_MaskTex", null);
        painterCanvas.penColor = DefaultColor;
        drawOtherRtMat.SetFloat("_Alpha", 1);
        drawOtherRtMat.SetVector("_Color", Color.white);
    }

    public void ClearDraw(){
        painterCanvas.ClearCanvas();
    }

    public void CopyTextureToRender(Texture rt, RenderTexture target = null){
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, rt.width, rt.height, 0);
        if(target == null) target = painterCanvas.renderTexture;
        RenderTexture.active = target;
        Graphics.DrawTexture(new Rect(0, 0, rt.width, rt.height), rt, matForCopy);
        RenderTexture.active = null;
        GL.PopMatrix();
    }

    public RenderTexture CombineTextures(Texture tex){
        RenderTexture m_rt = new RenderTexture(painterCanvas.renderTexWidth, painterCanvas.renderTexHeight, 0, RenderTextureFormat.ARGB32);
        m_rt.filterMode = FilterMode.Bilinear;
        m_rt.useMipMap = false;

        Graphics.Blit(tex, m_rt);
        CopyTextureToRender(painterCanvas.renderTexture, m_rt);

        return m_rt;
    }

    void OnDisable() {
        painterCanvas.EndDraw();
    }
}
