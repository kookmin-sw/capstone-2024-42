using UnityEngine;

public class CharacterSpriteManager : MonoBehaviour
{
    public static CharacterSpriteManager Instance { get; private set; }
    public int character_select_ID { get; set; } 
    public SpriteRenderer characterRenderer;
    public Sprite missSprite1;
    public Sprite goodSprite1;
    public Sprite greatSprite1;
    public Sprite perfectSprite1;
    public Sprite missSprite;
    public Sprite goodSprite;
    public Sprite greatSprite;
    public Sprite perfectSprite;
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 전환시 객체 유지
        }
        else
        {
            Destroy(gameObject);
        } 
    }


    public void ChangeSprite(string judgment)
    {
        CharacterSelect chsp = new CharacterSelect();
        int chsprite = User.user.character;
        if(chsprite == 0)
        {
            switch (judgment)
            {
                case "Miss":
                    characterRenderer.sprite = missSprite;
                    
                    break;
                case "Good":
                    characterRenderer.sprite = goodSprite;
     
                    break;
                case "Great":
                    characterRenderer.sprite = greatSprite;

                    break;
                case "Perfect":
                    characterRenderer.sprite = perfectSprite;

                    break;
            }
        }
        else if(chsprite == 1)
        {
            switch (judgment)
            {

                case "Miss":
                    characterRenderer.sprite = missSprite1;
                  
                    break;
                case "Good":
                    characterRenderer.sprite = goodSprite1;
                  
                    break;
                case "Great":
                    characterRenderer.sprite = greatSprite1;

                    break;
                case "Perfect":
                    characterRenderer.sprite = perfectSprite1;
                 
                    break;
            }
        }
    }
}
