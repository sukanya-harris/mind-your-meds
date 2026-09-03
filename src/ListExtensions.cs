using Godot;
using System;
using System.Collections.Generic;

public static class ListExtensions
{
    public static void Shuffle<T>(this List<T> list)
    {
        var ranShuffle = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = ranShuffle.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}

