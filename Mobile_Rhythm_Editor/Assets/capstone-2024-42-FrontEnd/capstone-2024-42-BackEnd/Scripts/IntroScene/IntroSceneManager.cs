using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneManager : MonoBehaviour
{
    public FadeController fader;
    public Camera cam;
    public FadeController fader2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Activate());

    }
    IEnumerator Activate()
    {
        fader.gameObject.SetActive(true);
        fader.FadeIn(1.5f);
        yield return new WaitForSeconds(3f);
        fader.FadeOut(1.2f);

        yield return new WaitForSeconds(1.3f);

        fader.gameObject.SetActive(false);

        Color color;
        ColorUtility.TryParseHtmlString("#130D2C", out color);
        cam.backgroundColor= color;
        
        fader2.gameObject.SetActive(true);
        
        fader2.FadeIn(1.5f);

        yield return new WaitForSeconds(3f);

        fader2.FadeOut(1.2f, () =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("LoginScene");
        });
    }
}
