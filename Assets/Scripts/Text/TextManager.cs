using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;

public class TextManager : MonoBehaviour
{
    static Story story;
    public static bool active = false;
    public TextAsset masterText;

    static GameObject leftPortrait;
    static GameObject rightPortrait;
    static string currentLine;

    public AudioSource tutorialMusic;
    public AudioSource fightMusic;
    public AudioSource confirmSound;

    static AudioSource fight;
    static AudioSource tutorial;
    static AudioSource confirm;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        TextManager.fight = fightMusic;
        TextManager.tutorial = tutorialMusic;
        TextManager.confirm = confirmSound;

        TextManager.story = new Story(masterText.text);
        TextManager.CreateInkFunctions();
    }
    void Update()
    {
        TextManager.UpdateStatic();
    }

    static void UpdateStatic()
    {
        if(active && Input.GetKeyDown("z"))
        {
            Continue();
        }
    }

    public static void Continue()
    {
        if(story.canContinue)
        {
            confirm.Play();
            currentLine = story.Continue();
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
        leftPortrait = Instantiate(Resources.Load("portraits/" + sceneTags[0]) as GameObject, GameObject.Find("Canvas").transform);
        rightPortrait = Instantiate(Resources.Load("portraits/" + sceneTags[1]) as GameObject, GameObject.Find("Canvas").transform);
        leftPortrait.GetComponent<Portrait>().FirstActivation(false, rightPortrait.GetComponent<Portrait>());
        rightPortrait.GetComponent<Portrait>().FirstActivation(true, leftPortrait.GetComponent<Portrait>());
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

    static void CreateInkFunctions()
    {
        story.BindExternalFunction ("PlayMusic", (int id) => {
            if(id == 0)
            {
                fight.Stop();
                tutorial.Play(0);
            }
            else if(id == 1)
            {
                tutorial.Stop();
            }
            else if(id == 2)
            {
                tutorial.Stop();
                fight.Play(0);
            }
            else if(id == 3)
            {
                tutorial.Stop();
                fight.Stop();
            }
        });
    }
}
