using System.Collections.Generic;
using GameTool;
using UnityEditor;
using UnityEngine;

namespace _Logo_Quiz.Scripts
{
    [CreateAssetMenu(fileName = "AlphabetResources", menuName = "_LogoQuiz/AlphabetResources", order = 0)]
    public class AlphabetResources : ScriptableObject
    {
        public SerializedDict<string, Sprite> alphabetDict;

        [ContextMenu("UpdateDict")]
        public void UpdateDict()
        {
            alphabetDict.Clear();
            var images = GetImagesInFolder("Assets/_Logo Quiz/PNG/Blue");
            for (int i = 0; i < images.Count; i++)
            {
                alphabetDict.Add(images[i].name, Sprite.Create(images[i], new Rect(0, 0, 
                    images[i].width, images[i].height), Vector2.zero));
            }
        }
        
        List<Texture2D> GetImagesInFolder(string folderPath)
        {
            List<Texture2D> images = new List<Texture2D>();

            string[] files = AssetDatabase.FindAssets("t:Texture2D", new string[] { folderPath });

            foreach (string fileGUID in files)
            {
                string filePath = AssetDatabase.GUIDToAssetPath(fileGUID);
                Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(filePath);

                if (texture != null)
                {
                    images.Add(texture);
                }
            }

            return images;
        }
    }
}