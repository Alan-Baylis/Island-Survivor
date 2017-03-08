/*  
created by: Gus Passos


    This framework will enable a text and a panel to display subtitles in the screen.
    By calling the function UpdateSubtitle, the string passed as argument will be shown in the screen.
    It's possible to choose when the string will be shown, how long it will be showed and how many "transitions" in the same string.
        If, for example, you want to render a big string without calling the same function twice, will may use " | " between the string, then it'll be divided.

*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SubtitleManager : MonoBehaviour
{

    // panel behind text
    Image background;
    // text
    Text text;

    // if the narrator is talking
    bool talking = false;

    // current time
    float timer = 0;

    // array with every line in this string
    string[] lines;

    // current line in the screen
    int currentLine;

    // timers to show the text
    [Tooltip("Time in seconds to show the first line")]
    public float timeToStart;
    [Tooltip("Time in seconds to maintain the line in the screen")]
    public float timeToShow;

    // used vars from time
    float timeBeforeShow;
    float timeShowing;

    string[] linesQueue;
    float[] showQueue;
    float[] startQueue;

    // max alpha for text and panel
    [Range(0,1)]
    public float maxAlphaText;
    [Range(0,1)]
    public float maxAlphaPanel;

    // proportional sizes for the panel relative to font size
    float panelHeight;
    float panelWidth;

    [Tooltip("Recommended to leave 9 as scale")]
    public float maxPanelWidth;

    // text and visual vars
    public Font textFont;
    public int textFontSize;

    public Color textColor;
    public Color panelColor;

    [Tooltip("Should be related to font size : Recommended = 75")]
    public int maxCharPerLine;

    // Finding background and text
    void Awake()
    {
        background = GetComponentInChildren<Image>();
        text = GetComponentInChildren<Text>();
    }

    // Starting vars
    void Start()
    {

        linesQueue = new string[3];
        startQueue = new float[3];
        showQueue = new float[3];

        // setting timers
        timeBeforeShow = timeToStart;
        timeShowing = timeToShow;

        // setting font and font size
        text.font = textFont;
        text.fontSize = textFontSize;

        // setting text and background colours   
        text.color = textColor - new Color(0, 0, 0, 1);
        background.color = panelColor - new Color(0, 0, 0, 1);

    }

    // reset vars changed in each function
    void ResetVar()
    {
        timer = 0;
        lines = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (talking)
        {
            // add seconds to timer
            timer += Time.deltaTime;

            // starting to show the text after some time
            if (timer > timeBeforeShow && timer < timeShowing + timeBeforeShow)
            {
                // increasing alpha to show the text
                if (text.color.a < maxAlphaText)
                    text.color += new Color(0, 0, 0, 0.05f);

                // increasing alpha to show the text background
                if (background.color.a < maxAlphaPanel)
                    background.color += new Color(0, 0, 0, 0.01f);
            }
            // stopping to show te text after some time
            if (timer > timeShowing + timeBeforeShow)
            {
                // decreasing text alpha
                if (text.color.a > 0)
                    text.color -= new Color(0, 0, 0, 0.05f);
                // only change text after the last one has disappeared
                else
                {
                    // looking for the next line to be shown
                    if (currentLine == lines.Length - 1)
                    {
                        // if there isn't another line, reset every var
                        ResetVar();

                        background.color -= new Color(0, 0, 0, 0.5f);

                        // when the background dissapear, the text stops to be shown
                        if (background.color.a <= 0.1 && queueEmpty())
                        {
                            talking = false;
                        }
                        else
                        {
                            talking = false;
                            for(int i = 0; i < linesQueue.Length; i++)
                            {
                                if(linesQueue[i] != null)
                                {
                                    UpdateSubtitle(linesQueue[i], startQueue[i], showQueue[i]);
                                    linesQueue[i] = null;
                                    startQueue[i] = timeToStart;
                                    showQueue[i] = timeToShow;
                                }
                            }
                        }
                    }
                    else
                    {
                        // if there is another line to be shown: 
                        timer = timeBeforeShow;
                        currentLine++;
                        text.text = lines[currentLine];
                        // updating background size
                        UpdateBackgroundSize(lines[currentLine]);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Basic function to display a string in the screen
    /// </summary>
    /// <param name="line">The string will be split for each separator (|, as standard)  and shown separately</param>
    /// <param name="timeToStart">NULL to use standard time</param>
    /// <param name="timeToWait">NULL to use standard time</param>
    public void UpdateSubtitle(string line, float? timeToStart, float? timeToWait)
    {
        if(!talking)
        {
            // start talking
            talking = true;

            // the first line
            currentLine = 0;

            //dividing string here
            lines = line.Split('|');

            // showing the first line
            text.text = lines[currentLine];

            // set timers
            if (timeToStart != null)
                timeBeforeShow = (float)timeToStart;
            if (timeToWait != null)
                timeShowing = (float)timeToShow;

            // updating pane size
            UpdateBackgroundSize(lines[currentLine]);
        }
        else
        {
            if (timeToStart == null) timeToStart = timeBeforeShow;
            if (timeToWait == null) timeToWait = timeShowing;
            addQueue(line, (float)timeToStart, (float)timeToWait);
        }

    }

    // the panel size will be related to the line size
    void UpdateBackgroundSize(string line)
    {
        float x;
        float y;

        // setting proportional size for panel
        panelHeight = text.fontSize * 0.04f;
        panelWidth = text.fontSize * 0.01f;

        y = line.Length / (float)maxCharPerLine;

        // y can't be 0
        if (y < 0.9f)
            y = 1;

        x = line.Length * panelWidth;

        // max panel size
        if (x > maxPanelWidth)
            x = maxPanelWidth;


        // updating the value
        background.transform.localScale = new Vector3(x, (float)System.Math.Ceiling(y) * panelHeight, 0);
    }

    // Functions that change vars

    /// <summary>
    /// Set a new text size
    /// </summary>
    /// <param name="textFontSize"></param>
    public void setTextFontSize(int textFontSize)
    {
        text.fontSize = textFontSize;
    }

    /// <summary>
    /// Set a new text font
    /// </summary>
    /// <param name="textFont"></param>
    public void setTextFont(Font textFont, FontStyle? style)
    {
        text.font = textFont;
        text.fontStyle = (FontStyle)style;
    }

    /// <summary>
    /// Set a new text color
    /// </summary>
    /// <param name="textColor"></param>
    public void setTextColor(Color textColor)
    {
        text.color = textColor - new Color(0, 0, 0, 1);
    }

    /// <summary>
    /// Set a new background color
    /// </summary>
    /// <param name="backgroundColor"></param>
    public void setBackgroundColor(Color backgroundColor)
    {
        background.color = backgroundColor;
    }

    /// <summary>
    /// Enable or disable background
    /// </summary>
    /// <param name="enable"></param>
    public void backgroundVisible(bool enable)
    {
        background.enabled = enable;
    }

    private void addQueue(string line, float start, float show)
    {
        for(int i = 0; i < linesQueue.Length; i++)
        {
            if(linesQueue[i] == null)
            {
                linesQueue[i] = line;
                startQueue[i] = timeToStart;
                showQueue[i] = show;
                return;
            }
        }
    }

    private bool queueEmpty()
    {

        for (int i = 0; i < linesQueue.Length; i++)
            if (linesQueue[i] != null)  return false;
 
        return true;
    }




}
