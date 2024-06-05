using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameTool;
using UnityEngine;
using UnityEngine.UI;

public class StageComplelePopup : SingletonUI<StageComplelePopup>
{
    [SerializeField] private Button closeBtn;
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button nextStageBtn;
    [SerializeField] private Image popupImage;

    private void Start()
    {
        popupImage.transform.localScale = new Vector3(0, 0, 0);
        popupImage.transform.DOScale(new Vector3(1, 1, 1), 1f);
        closeBtn.onClick.AddListener(() =>
        {
            LoadSceneManager.Instance.LoadScene("Level " + GameData.Instance.CurLevel);
        });
        
        restartBtn.onClick.AddListener(() =>
        {
            LoadSceneManager.Instance.LoadCurrentScene();
        });
        
        nextStageBtn.onClick.AddListener(() =>
        {
            LoadSceneManager.Instance.LoadScene("Stage " + GameData.Instance.CurLevel + " - "
            + (GameData.Instance.CurStage + 1));
        });
    }
}
