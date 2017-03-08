using UnityEngine;
using System.Collections;

public class DoorManager : MonoBehaviour {

    public bool open = false;
    float counter = 0.0f;

    void Update()
    {
        if(open)
        {
            counter += Time.deltaTime;

            if (counter >= 3)
                CloseDoor();
        }
    }
    public void OpenDoor()
    {
        open = true;
        GetComponentInParent<Animation>().Play("Open");
    }

    void CloseDoor()
    {
        open = false;
        counter = 0;
        GetComponentInParent<Animation>().Play("Close");
    }
}
