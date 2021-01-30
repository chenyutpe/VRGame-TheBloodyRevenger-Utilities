using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningUtil : MonoBehaviour
{
    //public GameObject player;
    public Image[] images;
    public GameObject leftHand;
    public GameObject rightHand;
    
    void Awake()
    {
        images[0].canvasRenderer.SetAlpha(1.0f);
        for (int i = 1; i < images.Length-1; ++i)
        {
            images[i].canvasRenderer.SetAlpha(0f);
        }
        leftHand.SetActive(false);
        rightHand.SetActive(false);
    }



    void Start()
    {
        StartCoroutine(ImageFadeTimer());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            StopCoroutine(ImageFadeTimer());
            for (int i = 0; i < images.Length; ++i)
            {
                images[i].canvasRenderer.SetAlpha(0f);
            }
            SwitchToGame();
        }
    }

    void FadeIn(Image image, float duration)
    {
        image.canvasRenderer.SetAlpha(0f);
        image.CrossFadeAlpha(1.0f, duration, false);
    }

    void FadeOut(Image image, float duration)
    {
        image.canvasRenderer.SetAlpha(1.0f);
        image.CrossFadeAlpha(0, duration, false);
    }

    IEnumerator ImageFadeTimer()
    {
        yield return new WaitForSeconds(5);
        FadeOut(images[0], 1);
        FadeIn(images[1], 1);
        yield return new WaitForSeconds(5);
        FadeOut(images[1], 1);
        FadeIn(images[2], 1);
        yield return new WaitForSeconds(5);
        FadeOut(images[2], 1);
        FadeIn(images[3], 0);
        yield return new WaitForSeconds(5);
        FadeOut(images[3], 1);
        FadeIn(images[4], 1);
        yield return new WaitForSeconds(5);
        FadeOut(images[4], 1);
        FadeIn(images[5], 0);
        yield return new WaitForSeconds(5);
        FadeOut(images[5], 1);
        FadeIn(images[6], 0);
        yield return new WaitForSeconds(5);
        FadeOut(images[6], 1);
        FadeIn(images[7], 1);
        yield return new WaitForSeconds(5);
        FadeOut(images[7], 1);
        FadeIn(images[8], 1);
        yield return new WaitForSeconds(5);
        FadeOut(images[8], 1);
        yield return new WaitForSeconds(5);
        FadeOut(images[9], 1);
        SwitchToGame();
    }

    void SwitchToGame()
    {
        leftHand.SetActive(true);
        rightHand.SetActive(true);
    }
}
