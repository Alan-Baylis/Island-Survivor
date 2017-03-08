using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    Text message;
    Image plane;

    float timer = 0;
    float time = 0;

    void Awake ()
    {
        message = GetComponentInChildren<Text>();
        plane = GetComponentInChildren<Image>();
    }

    void Start()
    {
        plane.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {

        if(time > 0)
        {
            timer += Time.deltaTime;

            //add alpha in plane and text
           if(timer <= time)
            {
                if (plane.color.a < 1)
                {
                    plane.color += new Color(0, 0, 0, 0.01f);
                    message.color += new Color(0, 0, 0, 0.01f);
                }
            }

            if (timer >= time)
            {
                if (plane.color.a > -1)
                {
                    plane.color -= new Color(0, 0, 0, 0.01f);
                    message.color -= new Color(0, 0, 0, 0.01f);
                }
                else
                {
                    plane.enabled = false;
                    message.text = "";
                    timer = 0;
                    time = 0;
                }
            }
        }
	}

    /// <summary>
    /// Display a txt in the screen
    /// </summary>
    public void DisplayMessage(string text, Color textColor, Color backGroundColor, float timeToShow)
    {
        plane.enabled = true;
        plane.color = backGroundColor - new Color(0, 0, 0, 1);
        message.color = textColor - new Color(0, 0, 0, 1);
        message.text = text;
        time = timeToShow;
    }
}
