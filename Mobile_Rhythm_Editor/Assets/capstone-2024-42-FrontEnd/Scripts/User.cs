using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User_info
{
    //BackendGameData.Instance.User-.
    public int uid;      // ���� UID
    public string name; // ���� �г���
    public int character;   // ������ ������ ���� ĳ���� id
    public int level;       // ���� ����
    public int exp;        // ���� ����ġ
    public int gold;    // ������ ������ ��ȭ
    public int ranking;    // ���� ��ŷ
    public int score;     // ������ Ŭ������ �� ������ ��
}

 public class User : MonoBehaviour
{
    public static User_info user = new User_info();

}
