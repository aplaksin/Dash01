using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinListStaticData", menuName = "StaticData/SkinList")]
public class SkinListStaticData : ScriptableObject
{
    public List<Sprite> SpritesList = new List<Sprite>();
}
