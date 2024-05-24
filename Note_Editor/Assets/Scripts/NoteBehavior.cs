using UnityEngine;
using UnityEngine.EventSystems; // 이벤트 처리를 위해 필요

public class NoteBehavior : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public string noteType; // 노트의 타입
    public float noteTime; // 현재 노트의 시간
    private NoteManager manager;
    private RectTransform myRectTransform;
    private Canvas parentCanvas;

    void Awake()
    {
        manager = FindObjectOfType<NoteManager>();
        myRectTransform = GetComponent<RectTransform>();
        parentCanvas = GetComponentInParent<Canvas>(); // 캔버스 찾기
    }

    public void Initialize(NoteManager manager, GameObject noteGO, float time, string type)
    {
        this.manager = manager;
        this.noteType = type;
        this.noteTime = time;
        myRectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그 시작 시 필요한 처리
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(myRectTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out localPointerPosition))
        {
            myRectTransform.localPosition = new Vector3(localPointerPosition.x, myRectTransform.localPosition.y, 0);
            float newTime = manager.ConvertPositionXToTime(localPointerPosition.x, myRectTransform.parent as RectTransform);
            if (Mathf.Abs(noteTime - newTime) > Mathf.Epsilon)
            {
                manager.UpdateNoteTime(this.gameObject, noteTime, newTime);
                noteTime = newTime;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 드래그 종료 시 필요한 처리
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right) // 오른쪽 클릭으로 노트 삭제
        {
            manager.DeleteNote(this);
        }
    }
}
