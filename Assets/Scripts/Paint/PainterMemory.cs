using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using HimeLib;
using System.Threading.Tasks;

public class PainterMemory : MonoBehaviour
{
    public Button BTN_Upload;
    public Button BTN_Clear;
    public RawImage IMG_BackImage;
    public PaintLight paintLight;
    public PaintData paintData;

    PaintStroke currentStroke;

    [EasyButtons.Button]
    public async void Replay(){
        paintLight.ClearDraw();
        if(paintData == null) return;

        for (int i = 0; i < paintData.strokes.Count; i++)
        {
            paintLight.DrawStartLight(paintData.strokes[i].start);
            for (int j = 0; j < paintData.strokes[i].drag.Count; j++)
            {
                paintLight.DrawDragLight(paintData.strokes[i].drag[j]);
                await Task.Delay(1);
            }
            paintLight.DrawEndLight();
        }
    }

    [EasyButtons.Button]
    public async void Clear(){
        await Task.Delay(1);
        paintData = new PaintData();
        paintLight.ClearDraw();
    }

    void Start()
    {
        paintLight.OnDrawStart += DrawStart;
        paintLight.OnDrawDrag += DrawDrag;
        paintLight.OnDrawEnd += DrawEnd;

        paintData = new PaintData();

        BTN_Upload.onClick.AddListener(Upload);
        BTN_Clear.onClick.AddListener(Clear);
    }

    void Upload(){
        //RenderTexture rt = paintLight.CombineTextures(IMG_BackImage.texture);
        //LJMGameManager.instance.SaveAndSend(rt);
    }

    void DrawStart(Vector2 pos){
        currentStroke = new PaintStroke();
        currentStroke.start = TruncateVector2(pos);
    }

    void DrawDrag(Vector2 pos){
        currentStroke.drag.Add(TruncateVector2(pos));
    }

    void DrawEnd(){
        paintData.strokes.Add(currentStroke);
    }

    Vector2 TruncateVector2(Vector2 value)
    {
        // 計算無條件捨去至小數點後四位
        float x = Mathf.Floor(value.x * 1000) / 1000;
        float y = Mathf.Floor(value.y * 1000) / 1000;

        // 將結果轉換回float
        return new Vector2(x, y);
    }
}

[System.Serializable]
public class PaintData
{
    public PaintData() {
        strokes = new List<PaintStroke>();
    }
    public List<PaintStroke> strokes;
}

[System.Serializable]
public class PaintStroke
{
    public PaintStroke() {
        drag = new List<Vector2>();
    }
    public Vector2 start;
    public List<Vector2> drag;
}