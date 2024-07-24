using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HimeLib;

public class PaintLight : MonoBehaviour
{
    [Header("Setting")]
	public ToggleGroup brushBar;
    public List<Toggle> brushToggles;
	public ToggleGroup colorBar;
    public List<Toggle> colorToggles;
    public ToggleGroup sizeBar;
    public List<Toggle> sizeToggles;
    public List<float> sizeValues;
    public Button BTN_Clear;

    [Header("Paint Canvas")]
	public Painter painterCanvas;
    public Material drawOtherRtMat;
    public Material matForCopy;

    [Header("Canvas Camera")]
    public Camera canvasCamera;

    bool _isMouseDown =false;

    [EasyButtons.Button]
    void BindBrushToggles(){
        brushToggles = new List<Toggle>(brushBar.GetComponentsInChildren<Toggle>());
        colorToggles = new List<Toggle>(colorBar.GetComponentsInChildren<Toggle>());
        sizeToggles = new List<Toggle>(sizeBar.GetComponentsInChildren<Toggle>());
    }

    void Start()
    {
        foreach (Toggle toggle in brushToggles)
        {
            //toggle.onValueChanged.AddListener(OnBrushToggleEvent);
        }
        foreach (Toggle toggle in colorToggles)
        {
            toggle.onValueChanged.AddListener(OnColorToggleEvent);
        }
        for (int i = 0; i < sizeToggles.Count; i++)
        {
            int v = i;
            sizeToggles[v].onValueChanged.AddListener(x => {
                if(x){
                    painterCanvas.brushScale = sizeValues[v];
                }
                
            });
        }

        BTN_Clear?.onClick.AddListener(() => {
            painterCanvas.ClearCanvas();
        });

        void OnBrushToggleEvent(bool val){
            foreach(Toggle toggle in brushBar.ActiveToggles()){

                //Set pen texture.
                painterCanvas.penMat.mainTexture = toggle.GetComponent<Image>().sprite.texture;
                break;
            }
        }

        void OnColorToggleEvent(bool val){
            foreach(Toggle toggle in colorBar.ActiveToggles()){
                painterCanvas.penColor = toggle.GetComponent<Image>().color;
                break;
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isMouseDown = true;
            //Draw once when mouse down.

            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(painterCanvas.transform as RectTransform, Input.mousePosition, canvasCamera, out pos))
            {
                painterCanvas.ClickDraw(pos, canvasCamera, painterCanvas.penMat.mainTexture, painterCanvas.brushScale, painterCanvas.penMat, painterCanvas.renderTexture, true);
                Debug.Log("mouse down");
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (_isMouseDown)
            {
                //draw on mouse drag.
                Vector2 pos;
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(painterCanvas.transform as RectTransform, Input.mousePosition, canvasCamera, out pos))
                {
                    painterCanvas.Drawing(pos, canvasCamera, painterCanvas.renderTexture, false, true);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && _isMouseDown)
        {
            painterCanvas.EndDraw();
            _isMouseDown = false;
        }
    }

    public void NewDrawFoNewUser(){
        painterCanvas.brushScale = sizeValues[0];
        sizeToggles[0].isOn = true;
        painterCanvas.canvasMat.SetFloat("_Alpha", 1);
        painterCanvas.penColor = Color.white;
        colorToggles[0].isOn = true;
        painterCanvas.canvasMat.SetTexture("_MaskTex", null);
        painterCanvas.ClearCanvas();
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
