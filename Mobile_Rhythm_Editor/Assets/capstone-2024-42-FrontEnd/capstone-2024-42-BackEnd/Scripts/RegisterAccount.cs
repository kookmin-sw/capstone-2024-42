using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;
using Unity.VisualScripting;
using System.Collections;

public class RegisterAccount : LoginBase
{
    [SerializeField]
    private Image imageID;
    [SerializeField]
    private TMP_InputField inputFieldID;
    [SerializeField]
    private Image imagePW;
    [SerializeField]
    private TMP_InputField inputFieldPW;
    [SerializeField]
    private Image imageConfirmPW;
    [SerializeField]
    private TMP_InputField inputFieldConfirmPW;
    [SerializeField]
    private Image imageEmail;
    [SerializeField]
    private TMP_InputField inputFieldEmail;
    [SerializeField]
    private Image imageNickname;
    [SerializeField]
    private TMP_InputField inputFieldNickname;

    [SerializeField]
    private Button btnRegisterAccount;

    public void OnClickRegisterAccount()
    {
        ResetUI(imageID, imagePW, imageConfirmPW, imageEmail);

        if (IsFieldDataEmpty(imageID, inputFieldID.text, "아이디")) return;
        if (IsFieldDataEmpty(imagePW, inputFieldPW.text, "비밀번호")) return;
        if (IsFieldDataEmpty(imageConfirmPW, inputFieldConfirmPW.text, "비밀번호 확인")) return;
        if (IsFieldDataEmpty(imageEmail, inputFieldEmail.text, "메일 주소")) return;
        if (IsFieldDataEmpty(imageNickname, inputFieldEmail.text, "닉네임")) return;

        if(!inputFieldPW.text.Equals(inputFieldConfirmPW.text))
        {
            GuideForIncorrectlyEnteredData(imageConfirmPW, "비밀번호가 일치하지 않습니다.");
            return;
        }
        
        if(!inputFieldEmail.text.Contains("@"))
        {
            GuideForIncorrectlyEnteredData(imageEmail, "메일 형식이 잘못되었습니다. (ex) address@xx.xx");
            return;
        }

        btnRegisterAccount.interactable = false;
        SetMessage("계정 생성중입니다..");

        CustomSignUp();
    }

    private void CustomSignUp()
    {
        Backend.BMember.CustomSignUp(inputFieldID.text, inputFieldPW.text, callback =>
        {
            btnRegisterAccount.interactable = true;

            if(callback.IsSuccess())
            {
                Backend.BMember.UpdateCustomEmail(inputFieldEmail.text, callback =>
                {
                    if(callback.IsSuccess())
                    {
                        Backend.BMember.CreateNickname(inputFieldNickname.text, callback =>
                        {
                            if (callback.IsSuccess())
                            {
                                BackendGameData.Instance.UserDataInsert();

                                SetMessage($"계정 생성 성공. {inputFieldNickname.text}님 환영합니다.");

                                BackendGameData.Instance.PlayerSongDataInsert();

                                BackendGameData.Instance.PlayerCharacterDataInsert();
                            }
                        });
                    }
                });
            }
            else
            {
                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 409:
                        message = "이미 존재하는 아이디입니다.";
                        break;
                    case 403:
                    case 401:
                    case 400:
                    default:
                        message = callback.GetMessage();
                        break;
                }

                if(message.Contains("아이디"))
                {
                    GuideForIncorrectlyEnteredData(imageID, message);
                }
                else
                {
                    SetMessage(message);
                }
            }
        });
    }
}