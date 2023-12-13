using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public int[,] map;
    public int width;
    public int height;

    public List<RoomElement> monsters;

    public Room(int _width, int _height)
    {
        map = new int[_width, _height];
        monsters = new List<RoomElement>();
    }
}
