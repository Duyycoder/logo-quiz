using System.Collections.Generic;
using UnityEngine;
using _Logo_Quiz.Scripts;

[CreateAssetMenu(fileName = "LevelResources", menuName = "LevelResources")]
public class LevelResources : ScriptableObject
{
    public List<StageResources> stages;
}
