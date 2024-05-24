using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Song_Select_Scene_Manager : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform contentPanel;
    public RectTransform sampleListItem;
    public VerticalLayoutGroup VLG;
    bool isSnapped;
    public float snapForce;
    float snapSpeed;

    public Sprite[] album;
    public Image main_image;
    public TMP_Text score_text;

    public static int play_song_id = 0;

    int currentItem = 0;
    void Start()
    {
        //isSnapped = false;
    }

    void Update() // �� �ľ� ��, ��ũ�� �� �̵� �ӵ��� �ٸ� �ڵ����� ����� ������� ����
    {
        currentItem = Mathf.RoundToInt(contentPanel.localPosition.y / (sampleListItem.rect.height + VLG.spacing));

        if (scrollRect.velocity.magnitude < 100/* && !isSnapped*/)
        {
            /*scrollRect.velocity = Vector2.zero;
            snapSpeed += snapForce * Time.deltaTime;
            contentPanel.localPosition = new Vector3(
                contentPanel.localPosition.x,
                Mathf.MoveTowards(contentPanel.localPosition.y, 0 - (currentItem * (sampleListItem.rect.height + VLG.spacing)), snapSpeed),
                contentPanel.localPosition.z);
            NameLabel.text = ItemNames[currentItem];
            if (contentPanel.localPosition.y == 0 - (currentItem * (sampleListItem.rect.height + VLG.spacing)))
            {
                isSnapped = true;
            }*/
            contentPanel.localPosition = new Vector3(contentPanel.localPosition.x, (currentItem * (sampleListItem.rect.height + VLG.spacing)), contentPanel.localPosition.z);
            main_image.sprite = album[currentItem];
            score_text.text = Song.score[currentItem].ToString();
        }
        touch_select();

        /*if(scrollRect.velocity.magnitude > 200)
        {
            isSnapped = false;
            snapSpeed = 0;
        }*/
    }

    public void touch_select() // �� ���� ��ư Ŭ�� �� �÷��� �� �� id ����
    {
        play_song_id = currentItem;
        SongSelectionManager.Instance.ssongid = currentItem;
        string selectedSongID = play_song_id.ToString();
        Debug.LogError("곡" + selectedSongID);
        if (SongSelectionManager.Instance != null)
        {
            SongSelectionManager.Instance.SelectSong(selectedSongID);
        }
        else
        {
            Debug.LogError("곡 선택 안됨");
        }
    }
}
//0 - (currentItem * (sampleListItem.rect.height + VLG.spacing))