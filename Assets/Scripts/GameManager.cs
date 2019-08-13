using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Core core;//게임 승리조건인 코어는 게임매니저의 역할이라고 생각되어 따로 빼주었다.
    public Player player;
    public GameObject platformData;
    public int Gold {
        get {

            return gold;
        }
        set {
            gold = value;
            UIManager.Instance.OnGoldChanged();
        }
    }
    private int gold;
    public static GameManager Instance {
        get {
            return instance;
        }
    }

    private static GameManager instance;

    public bool isGamePlaying=false;

    private void Awake()
    {
        if (GameManager.Instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

  
}
