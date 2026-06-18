using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Animation Data", menuName = "Scriptable Objects/Animation Data", order = 1)]
public class AnimationData1 : ScriptableObject
{
    public static float targetFrameTime = 1f / 60f;
    public int frameOfGap;
    public Sprite[] sprites;
    public bool loop;
}
