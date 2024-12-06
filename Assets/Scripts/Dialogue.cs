using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // For dialogue text
    public Image portraitImage;          // UI Image for portraits
    public Image textBoxFrame;           // UI Image for text box frames
    public string[] lines;               // Array of dialogue lines
    public Sprite[] portraits;           // Array of portraits matching dialogue lines
    public Sprite[] textBoxFrames;       // Array of text box frames matching dialogue lines
    public string repeatableLine = "Hello again!";
    public Sprite repeatablePortrait;    // Portrait for the repeatable line
    public Sprite repeatableFrame;       // Text box frame for the repeatable line
    public float textSpeed = 0.03f;
    public bool isTalking = false;

    private int index = 0;
    private bool isDialogueActive = false;
    private bool isDialogueExhausted = false;

    public MovementTest player;
    public NPC npc;

    void Start()
    {
        player = FindFirstObjectByType<MovementTest>();
        npc= FindFirstObjectByType<NPC>();
        if (textComponent != null)
        {
            if (player.isIntroMonologueActive == false)
            {
                textComponent.text = string.Empty;
                textComponent.transform.parent.gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            if (textComponent.text == GetCurrentLine())
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = GetCurrentLine();
            }
        }
    }

    public void StartDialogue()
    {
        if (player.sceneNum == 5 && npc.talked == true)
        {
            player.animator.SetBool("Sitting", true);
        }
        isTalking = true;
        if (textComponent == null || portraitImage == null || textBoxFrame == null)
        {
            EndDialogue();
            return;
        }

        if (isDialogueExhausted)
        {
            index = lines.Length;
        }
        else
        {
            index = 0;
        }

        isDialogueActive = true;
        textComponent.transform.parent.gameObject.SetActive(true);
        textComponent.text = string.Empty;

        UpdatePortrait();   // Update portrait for the first line
        UpdateTextBoxFrame(); // Update text box frame for the first line
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        foreach (char c in GetCurrentLine().ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            UpdatePortrait();    // Update portrait for the next line
            UpdateTextBoxFrame(); // Update text box frame for the next line
            StopAllCoroutines();
            StartCoroutine(TypeLine());
        }
        else if (index == lines.Length - 1 && !isDialogueExhausted)
        {
            isDialogueExhausted = true;
            EndDialogue();
        }
        else if (isDialogueExhausted)
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        if (textComponent != null)
        {
            textComponent.transform.parent.gameObject.SetActive(false);
        }
        isDialogueActive = false;
        isTalking = false;
    }

    private string GetCurrentLine()
    {
        if (index < lines.Length)
        {
            return lines[index];
        }
        return repeatableLine;
    }

    private void UpdatePortrait()
    {
        if (index < portraits.Length && portraits[index] != null)
        {
            portraitImage.sprite = portraits[index];
        }
        else
        {
            portraitImage.sprite = repeatablePortrait;
        }
    }

    private void UpdateTextBoxFrame()
    {
        if (index < textBoxFrames.Length && textBoxFrames[index] != null)
        {
            textBoxFrame.sprite = textBoxFrames[index];
        }
        else
        {
            textBoxFrame.sprite = repeatableFrame;
        }
    }

    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }
}
