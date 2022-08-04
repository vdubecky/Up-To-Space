using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BG : MonoBehaviour
{
    [SerializeField]
    private RawImage img;
    [SerializeField]
    private RawImage bg;

    private Texture2D backgroundTexture;

    [SerializeField]
    private Color color1;
    [SerializeField]
    private Color color2;

    [SerializeField]
    private GameObject hrac;
    [SerializeField]
    private GameObject canv;

    private Color32 aktualniBarva;
    private Color32 predchoziBarva;

    private int indexBarvy;
    private int predchoziIndex;

    [SerializeField]
    private GameObject next;

    public List<Color32> barvy = new List<Color32>()
    {
        new Color32(129, 28, 28, 255),
        new Color32(42, 103, 116, 255),
        new Color32(141, 79, 0, 255),
        new Color32(30, 113, 48, 255),
        new Color32(117 , 72 , 164, 255),
    };

    private void Awake()
    {
        backgroundTexture = new Texture2D(1, 2);
        backgroundTexture.wrapMode = TextureWrapMode.Clamp;
        backgroundTexture.filterMode = FilterMode.Bilinear;
        indexBarvy = Random.Range(0, barvy.Count);
        GenerujBarvy(false);
    }

    private void Update()
    {
        canv.transform.position = new Vector3(hrac.transform.position.x, canv.transform.position.y, canv.transform.position.z);
    }

    /// <summary>
    /// stav v argumentu vyjadruje poradi generovani barev
    /// </summary>
    public void GenerujBarvy(bool stav)
    {
        predchoziBarva = barvy[indexBarvy];
        predchoziIndex = indexBarvy;

        indexBarvy = Random.Range(0, barvy.Count);

        while (predchoziIndex == indexBarvy)
        {
            indexBarvy = Random.Range(0, barvy.Count);
        }

        aktualniBarva = barvy[indexBarvy];
        SetColor(predchoziBarva, aktualniBarva, stav);
    }

    public void ZmenPoziciGradientu(bool zmena)
    {
        if (!zmena)
        {
            bg.transform.position += new Vector3(0, bg.rectTransform.sizeDelta.y + img.rectTransform.sizeDelta.y, 0);
            next.transform.position = new Vector3(bg.transform.position.x, bg.transform.position.y + 10, 0);
            SetColor(aktualniBarva);
        }
        else
        {
            img.transform.position += new Vector3(0, bg.rectTransform.sizeDelta.y + img.rectTransform.sizeDelta.y, 0);
            next.transform.position = new Vector3(img.transform.position.x, img.transform.position.y + 10, 0);
            GenerujBarvy(true);
        }
    }

    public void SetColor(Color32 color1)
    {
        bg.color = Convertor(color1);
    }

    public void Restart()
    {
        canv.transform.position = new Vector3(0, 0, 0);
        img.transform.position = new Vector3(-9.69f, -10, 0);
        bg.transform.position = new Vector3(-9.69f, 15, 0);
        next.transform.position = new Vector3(0.17f, 26.4f, 0);
        GenerujBarvy(false);
    }

    public void SetColor(Color32 color1, Color32 color2, bool stav)
    {
        backgroundTexture.SetPixels(new Color[] { Convertor(color1), Convertor(color2) });
        backgroundTexture.Apply();
        img.texture = backgroundTexture;

        if (!stav)
        {
            bg.color = Convertor(color2);
        }
        else
        {
            bg.color = Convertor(color1);
        }
    }

    private Color Convertor(Color32 color)
    {
        return new Color(color.r / 255.0f, color.g / 255.0f, color.b / 255.0f, color.a / 255.0f);
    }
}
