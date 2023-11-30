using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAnimator : MonoBehaviour
{

    [SerializeField]
    private Image bg1;

    [SerializeField]
    private Image bg2;

    [SerializeField]
    private List<Sprite> bgList;

    [SerializeField]
    private float fadeTime = 1f;

    [SerializeField]
    private float fadeTimeDelta = 0.1f;

    private float currentFadeTimer = 0;

    private int currentBgIndex = 1;

    private Sprite iSprite;

    private List<Sprite> iSprites;

    private float tempTime = 0;

    private bool isFirsTime = true;

    private bool ddd = false;
    // Start is called before the first frame update
    void Start()
    {
        //bg1.CrossFadeAlpha
        bg1.sprite = bgList[0];
        //bg2.sprite = bgList[1];
        tempTime = 0;
        //iSprites.Add(bgList[0]);

        //iSprites.Add(InterpolateSprites(bgList[0], bgList[1], ));

    }

    // Update is called once per frame
    void Update()
    {
        //Test1();
        //T4();
        T5();

    }

    private void T5()
    {
        
        if(tempTime >= fadeTime)
        {
            tempTime = 0;
            currentBgIndex++;

            if (ddd)
            {
                currentBgIndex = 0;
            }
                
        }

        if(currentBgIndex != bgList.Count-1)
        {
            Sprite sp = InterpolateSprites(bgList[currentBgIndex], bgList[currentBgIndex+1], tempTime);
            bg1.sprite = sp;
            
        } else
        {
            Sprite sp = InterpolateSprites(bgList[currentBgIndex], bgList[0], tempTime);
            bg1.sprite = sp;
            ddd = true;
            //currentBgIndex = 0;
        }
        
        tempTime += Time.deltaTime;

    }

    private void T4()
    {
        if(isFirsTime)
        {
            isFirsTime = false;
            Sprite sp = InterpolateSprites(bgList[0], bgList[1], fadeTime);
            bg1.sprite = sp;
            currentBgIndex++;
        }

        tempTime += Time.deltaTime;

        if(tempTime >= fadeTime)
        {
            if(currentBgIndex == bgList.Count-1)
            {
                currentBgIndex = 0;
            }

            Sprite sp = InterpolateSprites(bgList[currentBgIndex], bgList[currentBgIndex+1], fadeTime);
            bg1.sprite = sp;
            currentBgIndex++;
        }
    }

    private void T3()
    {
        /*        Texture2D texture = bg1.sprite.texture;
                Color[] pixels1 = texture.GetPixels();

                Texture2D texture2 = bg2.sprite.texture;
                Color[] pixels2 = texture2.GetPixels();*/

        /*        for (int i = 0; i < pixels1.Length; i++)
                {

                }*/

        //iSprite = InterpolateSprites();


    }

    // Linear interpolation between two sprites
    private Sprite InterpolateSprites(Sprite spriteA, Sprite spriteB, float t)
    {
        Texture2D textureA = spriteA.texture;
        Texture2D textureB = spriteB.texture;

        int width = textureA.width;
        int height = textureA.height;

        Color[] pixelsA = textureA.GetPixels();
        Color[] pixelsB = textureB.GetPixels();

        Color[] interpolatedPixels = new Color[pixelsA.Length];

        // Linear interpolation of pixel colors
        for (int i = 0; i < pixelsA.Length; i++)
        {
            interpolatedPixels[i] = Color.Lerp(pixelsA[i], pixelsB[i], t);
        }

        // Create a new texture with the interpolated pixels
        Texture2D interpolatedTexture = new Texture2D(width, height);
        interpolatedTexture.SetPixels(interpolatedPixels);
        interpolatedTexture.Apply();

        // Create a new sprite from the interpolated texture
        Sprite interpolatedSprite = Sprite.Create(interpolatedTexture, spriteA.rect, Vector2.one * 0.5f);
        //interpolatedTexture = new Texture2D(width, height);
        return interpolatedSprite;
    }

    private void Test1()
    {
        if (currentFadeTimer < fadeTime)
        {
            currentFadeTimer += Time.deltaTime;
        }
        else
        {
            currentFadeTimer = 0;
            /*            if(currentBgIndex + 1 == bgList.Count)
                        {
                            currentBgIndex = 0;
                        }*/




            if (currentBgIndex % 2 == 0)
            {
                bg1.sprite = bgList[currentBgIndex];

                currentBgIndex++;

                if (currentBgIndex >= bgList.Count)
                {
                    currentBgIndex = 0;
                }

                bg2.sprite = bgList[currentBgIndex];

                currentBgIndex++;

                bg1.CrossFadeAlpha(0, fadeTime, false);
                bg2.CrossFadeAlpha(1, fadeTime - fadeTimeDelta, false);
            }
            else
            {

                bg1.sprite = bgList[currentBgIndex];

                currentBgIndex++;

                if (currentBgIndex >= bgList.Count)
                {
                    currentBgIndex = 0;
                }

                bg2.sprite = bgList[currentBgIndex];

                currentBgIndex++;

                bg1.CrossFadeAlpha(1, fadeTime - fadeTimeDelta, false);
                bg2.CrossFadeAlpha(0, fadeTime, false);
            }

            if (currentBgIndex >= bgList.Count)
            {
                currentBgIndex = 0;
            }

        }
    }

    private void Test2()
    {
        if (currentFadeTimer < fadeTime)
        {
            currentFadeTimer += Time.deltaTime;
        }
        else
        {
            currentFadeTimer = 0;

            if(currentBgIndex >= bgList.Count)
            {
                currentBgIndex = 0;
            }

            bg1.sprite = bg2.sprite;

            bg2.CrossFadeAlpha(0, 0.001f, false);
            bg2.sprite = bgList[currentBgIndex];
            bg2.CrossFadeAlpha(1, fadeTime, false);

            currentBgIndex++;
        }
    }
}
