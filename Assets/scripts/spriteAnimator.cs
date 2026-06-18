using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spriteAnimator : MonoBehaviour
{
    public SpriteRenderer mySpriteRenderer;
    public AnimationData1 baseAnimation;
    Coroutine previousAnimation;

    private void OnEnable()
    {
        PlayAnimation(baseAnimation);
    }

    private void Start()
    {
        PlayAnimation(baseAnimation);
    }

    public void StopAnimation()
    {
        if (previousAnimation != null)
        {
            StopCoroutine(previousAnimation);
            previousAnimation = null;
        }
    }

    public void PlayAnimation(AnimationData1 data)
    {
        StopAnimation();
        previousAnimation = StartCoroutine(PlayAnimationCoroutine(data));
    }

    public IEnumerator PlayAnimationCoroutine(AnimationData1 data)
    {
        if (data == null)
        {
            data = baseAnimation;
        }


        int spritesAmount = data.sprites.Length, i=0;
        float waitTime = data.frameOfGap*AnimationData1.targetFrameTime;
        while(i<spritesAmount)
        {
            //change
            mySpriteRenderer.sprite = data.sprites[i++];
            //wait
            yield return new WaitForSeconds(waitTime);
            //check condiions
            if (data.loop && i >= spritesAmount)
                i = 0;
        }
        yield return null;
    }
}
