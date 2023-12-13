using UnityEngine;

public static class RandomUtils
{
    public static int GetRandom(int _min, int _max)
    {
        return Random.Range(_min, _max);
    }

    public static float GetRandom(float _min, float _max)
    {
        return Random.Range(_min, _max);
    }
}
