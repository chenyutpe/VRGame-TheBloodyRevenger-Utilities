using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingUtil : MonoBehaviour
{
    public Image endingPic;
    public Image bg;
    public static EndingUtil inst;
    void Awake()
    {
        inst = this;
        endingPic.canvasRenderer.SetAlpha(0f);
        endingPic.gameObject.SetActive(false);
    }

    void FadeIn(Image image)
    {
        image.canvasRenderer.SetAlpha(0f);
        image.gameObject.SetActive(true);
        image.CrossFadeAlpha(1, 2, false);
    }

    public void ShowEnding()
    {
        FadeIn(endingPic);
        FadeIn(bg);
    }
}
