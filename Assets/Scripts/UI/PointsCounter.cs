using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointsCounter : MonoBehaviour
{
    private PointsManager _manager;

    [SerializeField] private Text _pointText;

    private void Start()
    {
        //Instatiate the points manager
        _manager = PointsManager.Instance;

        //subscribes the UI method to the event
        _manager.OnPointsChanged += UpdateUI;

        //Initialize the points
        UpdateUI(_manager.Points);
    }

    private void UpdateUI(int points)
    {
        _pointText.text = points.ToString();
    }

    private void OnDestroy()
    {
        //remove the inscription to avoid memory leaks 
        _manager.OnPointsChanged -= UpdateUI;
    }
}
