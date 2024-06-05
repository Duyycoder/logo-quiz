using System;
using _Logo_Quiz.Scripts;
using GameTool;
using UnityEngine;

public class GameData : SingletonMonoBehaviour<GameData>
{
    public GameDataSave Data;
    public AlphabetResources alphabetResources;
    public GameResources gameResources;
    

    //[Header("RESOURCE")] 

    //[Header("SCENE FLOW")]


    protected override void Awake()
    {
        base.Awake();
        Data = new GameDataSave();
        LoadAllData();
    }
    
    #region LOAD DATA
    public void LoadAllData()
    {
        GameDataControl.LoadAllData();
    }
    #endregion LOAD DATA
    
    #region CLEAR DATA
    public void ClearAllData()
    {
        Data = new GameDataSave();
        GameDataControl.SaveAllData();
    }
    #endregion CLEAR DATA

    #region SAVE DATA
    public void SaveAllData()
    {
        GameDataControl.SaveAllData();
    }

    public void SaveData<T>(eData filename, T value)
    {
        var save = new SaveUtility<T>();
        save.SaveData(filename, value);
    }

    public void LoadData<T>(eData filename, ref T variable)
    {
        var save = new SaveUtility<T>();
        save.LoadData(filename, ref variable);
    }
    #endregion SAVE DATA

    #region CURRRENCY

    public int Diamond
    {
        get => Data.Diamond;
        set
        {
            Data.Diamond = value;
            SaveData(eData.Diamond, Data.Diamond);
        }
    }

    public int Gold
    {
        get => Data.Gold;
        set
        {
            Data.Gold = value;
            SaveData(eData.Gold, Data.Gold);
        }
    }

    #endregion

    public bool MuteAll
    {
        get => Data.MuteAll;
        set
        {
            Data.MuteAll = value;
            SaveData(eData.MuteAll, MuteAll);
        }
    }

    public bool PushAlarm
    {
        get => Data.PushAlarm;
        set
        {
            Data.MuteAll = value;
            SaveData(eData.PushAlarm, PushAlarm);
        }
    }

    public bool SoundFX
    {
        get => Data.SoundFX;
        set
        {
            Data.SoundFX = value;
            SaveData(eData.SoundFX, SoundFX);
        }
    }

    public bool MusicFX
    {
        get => Data.MusicFX;
        set
        {
            Data.MusicFX = value;
            SaveData(eData.MusicFX, SoundFX);
        }
    }

    public bool Vibration
    {
        get => Data.Vibration;
        set
        {
            Data.Vibration = value;
            SaveData(eData.Vibration, Vibration);
        }
    }

    public int CurLevel
    {
        get => Data.CurLevel;
        set
        {
            Data.CurLevel = value;
            SaveData(eData.CurLevel, CurLevel);
        }
    }
    
    public int CurStage
    {
        get => Data.CurStage;
        set
        {
            Data.CurStage = value;
            SaveData(eData.CurStage, CurStage);
        }
    }
}

[Serializable]
public class GameDataSave
{
    [Header("USER DATA")]

    public int CurLevel = 1;
    public int CurStage = 1;
    
    [Header("CURRRENCY")] 
    public int Diamond = 0;
    public int Gold = 0;
    
    [Header("SETTING")]
    public bool MuteAll;
    public bool PushAlarm = true;
    public bool SoundFX = true;
    public bool MusicFX = true;
    public bool Vibration = true;
}