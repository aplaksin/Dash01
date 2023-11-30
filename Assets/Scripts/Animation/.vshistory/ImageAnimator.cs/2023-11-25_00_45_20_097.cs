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

    // Start is called before the first frame update
    void Start()
    {
        //bg1.CrossFadeAlpha
        bg1.sprite = bgList[0];
        bg2.sprite = bgList[1];

        
    }

    // Update is called once per frame
    void Update()
    {
        Test2();
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
