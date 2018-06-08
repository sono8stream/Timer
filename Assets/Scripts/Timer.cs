using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float timerValue;//経過時間
    Text timerText;//ゲーム画面に経過時間を表示するテキスト

    bool inTick;//計測中か判定するフラグ| 「true」(onの意味)と「false」(offの意味)が存在

    //Level4で用いる
    float preTimerValue;
    Text laptimeText;
    int lapCounter;

    //時間を表示するメソッド| 使いまわす操作はメソッド化するとよい
    void ShowTimerValue()
    {
        timerText.text = timerValue.ToString("00.00") + "s";
    }

    //時間計測用のメソッド
    void TickTimer()
    {
        if (inTick)
        {
            timerValue += Time.fixedDeltaTime; //前フレームからの経過時間を取得し、加算
            ShowTimerValue();//ゲーム画面に表示するメソッドの呼び出し
        }
    }

    // Use this for initialization| 初期化
    void Start()
    {
        timerValue = 0;
        timerText = GetComponent<Text>();
        ShowTimerValue();

        inTick = false;

        //Level4
        preTimerValue = 0;
        laptimeText = GameObject.Find("LaptimeText").GetComponent<Text>();
        laptimeText.text = "";
        lapCounter = 1;
    }

    // Update is called once per frame| 更新
    void Update()
    {
        TickTimer();
    }

//-----------------------Level1 ここまで

    //計測開始用のメソッド
    public void StartTick()
    {
        inTick = true;
    }

    //計測停止用のメソッド
    public void PauseTick()
    {
        inTick = false;
    }

//-----------------------Level2 ここまで

    //計測値をリセットするメソッド
    public void ResetTick()
    {
        inTick = false;

        timerValue = 0;
        ShowTimerValue();

        preTimerValue = 0;
        laptimeText.text = "";
    }

//------------------------Level3 ここまで ↓Level4

    public void ShowLapTime()
    {
        if (!inTick) return;//この書き方の方がスマート

        float lapTimeValue = timerValue - preTimerValue;
        //ラップタイムを表示| string.Format()は複雑な文字列のときに便利| "\r\n"は改行の意味
        laptimeText.text += string.Format("Lap{0}: {1:0.00}s\r\n", lapCounter, lapTimeValue);
        preTimerValue = timerValue;
        lapCounter++;
    }
}
