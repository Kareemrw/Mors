using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent; 
    public string[] lines;               
    public string repeatableLine = "Hello again!"; 
    public float textSpeed = 0.03f;     

    private int index = 0;              
    private bool isDialogueActive = false;
    private bool isDialogueExhausted = false; 

    void Start()
    {
        if (textComponent != null)
        {
            textComponent.text = string.Empty;
            textComponent.transform.parent.gameObject.SetActive(false); 
        }
    }

    void Update()
    {
        if (isDialogueActive && Input.GetMouseButtonDown(0)) 
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
        if (textComponent == null)
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
    }

    private string GetCurrentLine()
    {
        if (index < lines.Length)
        {
            return lines[index]; 
        }
        return repeatableLine; 
    }

    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }
}
