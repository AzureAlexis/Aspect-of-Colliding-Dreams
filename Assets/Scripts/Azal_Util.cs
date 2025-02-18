/*
    Some utility functions to make my life easier
*/
using UnityEngine;

public static class AzalUtil
{
    public static float Range(float num, float max, float min)
    {
        float value = num;

        value = Mathf.Max(value, min);
        value = Mathf.Min(value, max);

        return value;
    }

    public static Vector3 QuadOut(Vector3 a, Vector3 b, float t)
    {
        Vector3 diff = b - a;
        float factor = 1 - Mathf.Pow(1 - t, 2);
        return a + (diff * factor);
    }
}


