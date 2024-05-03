using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelect : MonoBehaviour
{
    public GameObject panel;

    public TMP_Text Cname;

    public Image image;
    public Sprite[] sprites;

    public int characterId;

    public string[] c = { "AAA", "BBB", "CCC" };

    public void cliked_character(int id)
    {
        characterId = id;
        Cname.text = c[id];
        image.sprite = sprites[id];
        panel.gameObject.SetActive(true);
    }
}

