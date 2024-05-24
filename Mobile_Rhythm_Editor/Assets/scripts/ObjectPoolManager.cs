using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public ObjectPool poolNoteG;
    public ObjectPool poolNoteY;
    public ObjectPool poolNoteR;
    public ObjectPool poolMarkerUp; 
    public ObjectPool poolMarkerDown;
    public GameObject GetObject(string type)
    {
        switch (type)
        {
            case "Note_G":
                return poolNoteG.GetObject();
            case "Note_Y":
                return poolNoteY.GetObject();
            case "Note_R":
                return poolNoteR.GetObject();
            case "Marker":
                return Random.value > 0.5f ? poolMarkerUp.GetObject() : poolMarkerDown.GetObject();
            default:
                Debug.LogError("Invalid type requested: " + type);
                return null;
        }
    }

    public void ReturnObject(GameObject obj, string type)
    {
        switch (type)
        {
            case "Note_G":
                poolNoteG.ReturnObject(obj);
                break;
            case "Note_Y":
                poolNoteY.ReturnObject(obj);
                break;
            case "Note_R":
                poolNoteR.ReturnObject(obj);
                break;
            case "Marker":
                if (obj.GetComponent<MarkerType>().IsUpMarker)
                {
                    poolMarkerUp.ReturnObject(obj);
                }
                else
                {
                    poolMarkerDown.ReturnObject(obj);
                }
                break;
            default:
                Debug.LogError("Invalid type returned: " + type);
                break;
        }
    }
}
