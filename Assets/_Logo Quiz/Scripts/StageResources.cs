using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Logo_Quiz.Scripts
{
    [CreateAssetMenu(fileName = "Stage", menuName = "Stage")]
    public class StageResources : ScriptableObject
    {
        public int blankNumber; 
        public List<AlphabetCharacter> alphabetsChar; 
        public string stageAnswer;
        public Sprite stageImg;
    }
    
    public enum AlphabetCharacter
    {
        A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z
    }
}