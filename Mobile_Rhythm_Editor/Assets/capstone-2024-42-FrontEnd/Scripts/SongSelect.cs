using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SongSelect : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform contentPanel;
    public RectTransform sampleListItem;

    public VerticalLayoutGroup VLG;

    public TMP_Text NameLabel;
    public string[] ItemNames;

    bool isSnapped;

    public float snapForce;
    float snapSpeed;
    void Start()
    {
        //isSnapped = false;
    }

    // Update is called once per frame
    void Update()
    {
        int currentItem = Mathf.RoundToInt(contentPanel.localPosition.y / (sampleListItem.rect.height + VLG.spacing));
        Debug.Log(currentItem);

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
            NameLabel.text = ItemNames[currentItem];
        }

        /*if(scrollRect.velocity.magnitude > 200)
        {
            isSnapped = false;
            snapSpeed = 0;
        }*/
    }
}
//0 - (currentItem * (sampleListItem.rect.height + VLG.spacing))