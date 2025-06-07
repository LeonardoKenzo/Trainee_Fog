using System;
using UnityEngine;

public class PointsManager
{
    //Instance a new Points Manager in another Scripts using PointsManager.Instance;
    private static PointsManager _instance;
    public static PointsManager Instance => _instance ??= new PointsManager();


    private int _points = 0;
    private int _maxPoints = 9999;

    public int Points
    {
        get { return _points; }
        set
        {
            int newValue = Mathf.Clamp(value, 0, _maxPoints);
            if(_points != newValue)
            {
                _points = newValue;
                OnPointsChanged?.Invoke(_points);
            }
        }
    }

    public int MaxPoints
    {
        get { return _maxPoints; }
    }

    //event that active if points change value
    public event Action<int> OnPointsChanged;

    public void AddPoints(int points)
    {
        Points += points;
    }

    public void RemovePoints(int points)
    {
        Points -= points;
    }
}
