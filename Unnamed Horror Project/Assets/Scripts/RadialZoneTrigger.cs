using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialZoneTrigger : MonoBehaviour
{
    public bool insideSinkZone = false;
    public bool insideWindowZone = false;
    public bool insideTVZone = false;  
    public enum TriggerType {Sink, Window, TV}
    public TriggerType triggerType;

    void OnTriggerEnter(Collider other)
    {
        if(triggerType == TriggerType.Sink)
        {
            insideSinkZone = true;
        }
        else if(triggerType == TriggerType.Window)
        {
            insideWindowZone = true;
        }
        else if(triggerType == TriggerType.TV)
        {
            insideTVZone = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(triggerType == TriggerType.Sink)
        {
            insideSinkZone = false;
        }
        else if(triggerType == TriggerType.Window)
        {
            insideWindowZone = false;
        }
        else if(triggerType == TriggerType.TV)
        {
            insideTVZone = false;
        }
    }

}
