using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public int buttonIndex; // 버튼에 할당된 인덱스
    public NoteSpawner noteSpawner; // 노트 스포너 참조

    public void OnButtonClick()
    {
        noteSpawner.SpawnNoteAtIndex(buttonIndex);
    }
}
