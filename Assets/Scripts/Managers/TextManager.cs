using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;

public class TextManager : MonoBehaviour
{
    static Story story;
    static bool active = false;

    static GameObject leftPortrait;
    static GameObject rightPortrait;
    static string currentLine;

    void Awake()
    {
        TextManager.story = new Story((Resources.Load("MasterTextFile") as TextAsset).text);
    }
    void Update()
    {
        TextManager.UpdateStatic();
    }

    static void UpdateStatic()
    {

    }

    public static void Continue()
    {
        if(story.canContinue)
        {
            currentLine = story.Continue();
            if(leftPortrait.charName() == story.currentTags[0])
            {
                leftPortrait.Activate(story.currentTags[1]);
                rightPortrait.Deactivate();
            }
            else if(rightPortrait.charName() == story.currentTags[0])
            {
                rightPortrait.Activate(story.currentTags[1]);
                leftPortrait.Deactivate();
            }
        }
        else if(story.currentChoices.Count > 0 )
        {

        }
        else
        {
            EndConversation();
        }
    }

    static void StartConversation(string id)
    {
        story.ChoosePathString(id);
        List<string> sceneTags = story.TagsForContentAtPath("your_knot");
        
        leftPortrait = Instantiate(Resources.Load("portraits/" + sceneTags[0]) as GameObject);
        rightPortrait = Instantiate(Resources.Load("portraits/" + sceneTags[1]) as GameObject);
        leftPortrait.FirstActivation(false);
        rightPortrait.FirstActivation(true);
        leftPortrait.SetEmotion(sceneTags[2]);
        rightPortrait.SetEmotion(sceneTags[3]);

        Continue();
    }

    static void EndConversation()
    {
        leftPortrait.Done();
        rightPortrait.Done();
    }
}
