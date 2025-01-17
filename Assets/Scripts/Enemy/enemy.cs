using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Public variables
    public int[] patternIDs;
    float hp;

    // Refrences
    GameObject player;

    // Stuff for waypoints
    Vector3 waypointOld = new Vector3(0, 0, 0);
    Vector3 waypointPosition = new Vector3(0, 0, 0);
    float waypointDuration = 0;
    float waypointTime = 0;

    // Stuff for patterns
    private int currentPatternIndex = 0;
    private int currentPatternId = 0;
    private Pattern pattern;
    float loopTime;
    float patternTime;

    // Misc vars
    float mhp;
    bool active = true;

    void Start()
    {
        player = PlayerStats.player;
        mhp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        player = PlayerStats.player;
        if(IsActive())
        {
            UpdatePosition();
            UpdatePattern();
            UpdateShots();
        }
    }

    void UpdatePattern()
    {
        if(pattern == null)
        {
            pattern = PatternManager.GetPattern(currentPatternId);
        }
        
        switch (pattern.endCondition)
        {
            case "hp":
                if(hp < pattern.endValue)
                {
                    hp = pattern.endValue;
                    NextPattern();
                }
                break;
            case "time":
                if(patternTime > pattern.endValue)
                {
                    hp = pattern.endValue;
                    NextPattern();
                }
                break;
        }
    }

    void NextPattern()
    {
        if(pattern.endEvent != "none")
            TextManager.StartConversation(pattern.endEvent);

        currentPatternIndex += 1;
        currentPatternId = patternIDs[currentPatternIndex];
        loopTime = 0;
        patternTime = 0;
        pattern = PatternManager.GetPattern(currentPatternId);
    }

    void UpdateShots()
    {
        if(IsActive())
        {
            patternTime += Time.deltaTime;
            loopTime += Time.deltaTime;
            FireShotsReady();
            if(loopTime >= pattern.loopTime)
            {
                loopTime = 0;
            }
        }
    }

    void UpdatePosition()
    {
        if(waypointPosition != transform.position)
        {
            float factor;
            factor = waypointTime / waypointDuration;
            factor = Mathf.SmoothStep(0, 1, factor);

            transform.position = Vector3.Lerp(waypointOld, waypointPosition, factor);
            waypointTime += Time.deltaTime;
        }
    }

    void MakeWaypoint(string template)
    {
        float wayX = transform.position.x;;
        float wayY = transform.position.y;
        float wayD = 0;

        switch (template)
        {
            case "toPlayer" :
                wayX = player.transform.position.x;
                if(wayX < 0)
                {
                    wayX = Mathf.Max(wayX, transform.position.x - 2);
                }
                else
                {
                    wayX = Mathf.Min(wayX, transform.position.x + 2);
                }

                wayY = player.transform.position.y;
                if(wayY < 0)
                {
                    wayY = Mathf.Max(wayY, transform.position.y - 2);
                }
                else
                {
                    wayY = Mathf.Min(wayY, transform.position.y + 2);
                }

                wayD = 1;
                break;
        }

        waypointOld = transform.position;
        waypointDuration = wayD;
        waypointPosition = new Vector3(wayX, wayY, 0);
        waypointTime = 0;
    }

    public bool IsActive()
    {
        return BattleManager.active && !TextManager.active;
    }

    void FireShotsReady()
    {
        for(int i = 0; i < pattern.shots.Count; i++)
        {
            float shotTime = pattern.shotTimes[i];
            if(loopTime - Time.deltaTime < shotTime && loopTime >= shotTime)
            {
                DanmakuManager.Fire(pattern.shots[i].danmaku, gameObject);
            }
        }
    }
}
