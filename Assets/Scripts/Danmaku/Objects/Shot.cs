/*
    A shot is a list of bullets that fire at the same time. A pattern usually contains
    multiple of these
*/

using UnityEngine;
using System.Collections.Generic;

public class Shot // A list of danmakus that fire at the same time
{
    public string name;
    public List<DanmakuData> danmaku;
}