using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "Scriptables/SceneData")]

public class SceneData : ScriptableObject
{
    public Object SceneObject;
    public Sprite SceneSprite;

    public string GetSceneName => SceneObject.name;
    
}
