using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;

public class TextManager : MonoBehaviour
{
    static Story story;
    static bool active = false;
    public TextAsset masterText;

    static GameObject leftPortrait;
    static GameObject rightPortrait;
    static string currentLine;

    void Start()
    {
        TextManager.story = new Story(masterText.text);
    }
    void Update()
    {
        TextManager.UpdateStatic();
    }

    static void UpdateStatic()
    {
        if(active && Input.GetKeyDown("z"))
            Continue();
    }

    public static void Continue()
    {
        Debug.Log(story.canContinue);
        if(story.canContinue)
        {
            currentLine = story.Continue();
            Debug.LogAssertion(story.currentTags[0]);
            if(leftPortrait.GetComponent<Portrait>().charName == story.currentTags[0])
            {
                leftPortrait.GetComponent<Portrait>().Activate(currentLine, story.currentTags[1]);
                rightPortrait.GetComponent<Portrait>().Deactivate();
            }
            else if(rightPortrait.GetComponent<Portrait>().charName == story.currentTags[0])
            {
                rightPortrait.GetComponent<Portrait>().Activate(currentLine, story.currentTags[1]);
                leftPortrait.GetComponent<Portrait>().Deactivate();
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

    public static void StartConversation(string id)
    {
        active = true;
        story.ChoosePathString(id);
        List<string> sceneTags = story.TagsForContentAtPath("prefight");
        Debug.Log("portraits/" + sceneTags[0] + ".prefab");
        leftPortrait = Instantiate(Resources.Load("portraits/" + sceneTags[0]) as GameObject, GameObject.Find("Canvas").transform);
        rightPortrait = Instantiate(Resources.Load("portraits/" + sceneTags[1]) as GameObject, GameObject.Find("Canvas").transform);
        leftPortrait.GetComponent<Portrait>().FirstActivation(false);
        rightPortrait.GetComponent<Portrait>().FirstActivation(true);
        leftPortrait.GetComponent<Portrait>().SetEmotion(sceneTags[2]);
        rightPortrait.GetComponent<Portrait>().SetEmotion(sceneTags[3]);

        Continue();
    }

    static void EndConversation()
    {
        leftPortrait.GetComponent<Portrait>().Done();
        rightPortrait.GetComponent<Portrait>().Done();
        active = false;
    }
}
