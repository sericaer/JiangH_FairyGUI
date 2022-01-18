using JiangH.API;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReactiveMarbles.PropertyChanged;

public class DaysTimer : MonoBehaviour
{

    public int speed { get; set; }
    public bool isSysPause { get; set; }

    private bool isUserPause => speed == -1;

    public bool isPause => isSysPause || isUserPause;

    public void OnSpeedChanged(int value)
    {
        speed = value;
    }

    void Start()
    {
        isSysPause = false;

        Facade.gmEnv.WhenChanged(x => x.DayIncSpeed).Subscribe(x => speed = x);

        StartCoroutine(OnTimer());
    }

    private IEnumerator OnTimer()
    {
        yield return new WaitForSeconds(1.0f / speed);
        yield return new WaitUntil(() => !isPause);

        Facade.gmSession.OnDaysInc();

        StartCoroutine(OnTimer());
    }
}
