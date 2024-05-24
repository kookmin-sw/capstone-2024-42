using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;

public class Login : LoginBase
{
    [SerializeField]
    private Image imageID;
    [SerializeField]
    private TMP_InputField inputFieldID;
    [SerializeField]
    private Image imagePW;
    [SerializeField]
    private TMP_InputField inputfieldPW;
    [SerializeField]
    private Button btnLogin;

    public void OnClickLogin()
    {
        ResetUI(imageID, imagePW);

        if (IsFieldDataEmpty(imageID, inputFieldID.text, "아이디")) return;
        if (IsFieldDataEmpty(imagePW, inputfieldPW.text, "비밀번호")) return;

        btnLogin.interactable = false;

        StartCoroutine(nameof(LoginProcess));

        ResponceToLogin(inputFieldID.text, inputfieldPW.text);
    }

    private void ResponceToLogin(string ID, string PW)
    {
        Backend.BMember.CustomLogin(ID, PW, callback =>
        {
            StopCoroutine(nameof(LoginProcess));

            if(callback.IsSuccess())
            {
//                string googlehash = Backend.Utils.GetGoogleHash();

                SetMessage($"{Backend.UserNickName}님 환영합니다.");

                BackendChartData.LoadAllChart();

//                Debug.Log("구글 해시 키 : " + googlehash);

                Utils.LoadScene(SceneNames.LoadingScene);
            }
            else
            {
                btnLogin.interactable = true;

                string message = string.Empty;

                switch ( int.Parse(callback.GetStatusCode()) )
                {
                    case 401:
                        message = callback.GetMessage().Contains("customId") ? "존재하지 않는 아이디입니다." : "잘못된 비밀번호입니다";
                        break;
                    case 403:
                        message = callback.GetMessage().Contains("user") ? "차단당한 유저입니다." : "차단당한 디바이스입니다";
                        break;
                    case 410:
                        message = "탈퇴가 진행중인 유저입니다.";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;
                }
                
                if(message.Contains("비밀번호"))
                {
                    GuideForIncorrectlyEnteredData(imagePW, message);
                }
                else
                {
                    GuideForIncorrectlyEnteredData(imageID, message);
                }
            }
        });
    }

    private IEnumerator LoginProcess()
    {
        float time = 0;

        while(true)
        {
            time += Time.deltaTime;

            SetMessage($"로그인 중입니다... {time:F1}");

            yield return null;
        }
    }
}