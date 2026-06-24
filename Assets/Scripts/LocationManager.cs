using System;
using UnityEngine;

public enum Location
{
    Room,
    Town,
    City,
    School
}

public class LocationManager : MonoBehaviour
{
    [SerializeField] Teleporter[] allTeleporters;

    public Action OnLocationChanged;
    Location currentLocation;

    void Awake()
    {
        foreach(Teleporter teleporter in allTeleporters)
        {
            teleporter.OnTeleport += SetCurrentLocation;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLocation = Location.Room;
    }

    public Location GetCurrentLocation()
    {
        return currentLocation;
    }

    public void SetCurrentLocation(Location location)
    {
        OnLocationChanged?.Invoke();
        currentLocation = location;
    }
}
