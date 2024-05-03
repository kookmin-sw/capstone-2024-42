using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class User : MonoBehaviour
{
    public TMP_Text Text_user_name;
    public TMP_Text Text_user_level;
    public TMP_Text Text_gold;

    public string user_name = "min";
    public int user_level = 5;
    public int gold = 10000;

    public int[] song = { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0};

    public int character = 1;
    // Start is called before the first frame update
    void Start()
    {
        Text_user_name.text = user_name;
        Text_user_level.text = user_level.ToString();
        Text_gold.text = gold.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
