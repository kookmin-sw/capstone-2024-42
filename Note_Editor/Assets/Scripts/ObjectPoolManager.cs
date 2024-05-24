using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public ObjectPool poolNoteG;
    public ObjectPool poolNoteY;
    public ObjectPool poolNoteR;

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
            default:
                Debug.LogError("노트타입: " + type);
                return null;
        }
    }

    public void ReturnObject(GameObject obj, string type)
    {
        ResetObjectState(obj);  

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
            default:
                Debug.LogError("노트타입: " + type);
                break;
        }
    }

    private void ResetObjectState(GameObject obj)
    {
        obj.SetActive(false); 
        obj.transform.position = Vector3.zero;  
        obj.transform.rotation = Quaternion.identity;  

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

      
       
    }
}
