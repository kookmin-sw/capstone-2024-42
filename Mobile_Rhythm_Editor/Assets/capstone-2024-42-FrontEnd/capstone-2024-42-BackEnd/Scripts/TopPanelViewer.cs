using UnityEngine;
using TMPro;

public class TopPanelViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textNickname;

    public void UpdateNickname()
    {
        textNickname.text = UserInfo.Data.nickname == null ? 
                            UserInfo.Data.gamerId : UserInfo.Data.nickname;



    }
}
