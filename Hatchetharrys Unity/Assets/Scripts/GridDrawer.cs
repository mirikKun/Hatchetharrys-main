using UnityEngine;
using UnityEngine.UI;

public class GridDrawer : MonoBehaviour
{
    [SerializeField] private Image grid;
    [SerializeField] private int width=600;
    [SerializeField] private int height=600;
    [SerializeField] private int rate = 599;
    void Awake()
    {
        Texture2D targetTexture= new Texture2D(width, height);
 
        for (int y = 0; y < width; y++) {
            for (int x = 0; x < height; x++) {
 
                if((y+3)%rate<7|| (x+3)%rate<7)
                //if(y%rate==0|| x%rate==0)
                {
                    targetTexture.SetPixel(x, y, Color.white);
                }
                else
                {
                    targetTexture.SetPixel(x, y, new Color(0,0,0,0));

                }
            }
        }
 
        targetTexture.Apply();
        grid.sprite=Sprite.Create(targetTexture,new Rect(0,0,width,height),new Vector2(0.5f,0.5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
