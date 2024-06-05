using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameResources", menuName = "GameResources")]
public class GameResources : ScriptableObject
{
    public List<LevelResources> levels;
}
