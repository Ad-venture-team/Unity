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

    public static Vector2 RandomVector2(Vector2 _min,Vector2 _max)
    {
        return new Vector2(GetRandom(_min.x, _max.x), GetRandom(_min.y, _max.y));
    }

    public static Vector2Int RandomVector2Int(Vector2Int _min, Vector2Int _max)
    {
        return new Vector2Int(GetRandom(_min.x, _max.x), GetRandom(_min.y, _max.y));
    }
}
