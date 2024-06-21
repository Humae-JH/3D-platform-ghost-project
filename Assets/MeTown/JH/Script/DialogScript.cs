using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Speaker
{
    public TextMeshProUGUI textDialogue;
    public GameObject ImageDialog;
    public GameObject objectArrow;
}



public class DialogScript : MonoBehaviour
{
    [SerializeField]
    protected Speaker speaker;

    [SerializeField]
    protected string[] dialogues;

    protected int dialogIndex = 0;
    protected bool isActivate = false;
    protected bool isPrinting = false;
    protected int printDialogIndex = 0;

    protected Vector3 arrowPosition = Vector3.zero;
    protected float arrowSpeed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        arrowPosition = speaker.objectArrow.transform.position;
        initiateDialogue();
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void initiateDialogue()
    {
        speaker.textDialogue.gameObject.SetActive(true);
        speaker.ImageDialog.gameObject.SetActive(true);
        speaker.objectArrow.gameObject.SetActive(true);

        dialogIndex = 0;
        isActivate = true;
        StartCoroutine(printDialogue());
    }

    public void deactivateDialogue()
    {
        speaker.textDialogue.gameObject.SetActive(false);
        speaker.ImageDialog.gameObject.SetActive(false);
        speaker.objectArrow.gameObject.SetActive(false);
    }

    public IEnumerator printDialogue()
    {
        if (!isPrinting)
        {  
            this.printDialogIndex = 0;
            isPrinting = true;
            //yield return new WaitForSeconds(0.1f);
        }

        while (printDialogIndex < dialogues[dialogIndex].Length)
        {
            speaker.textDialogue.text = dialogues[dialogIndex].Substring(0, printDialogIndex);
            printDialogIndex++;
            yield return new WaitForSeconds(0.1f);

        }

        isPrinting = false;
        StartCoroutine(startArrowEffect());
        yield break;
    }

    protected float stepFunction(float i)
    {

        if (i < 0)
        {
            return 0.0f;
        }
        else
        {
            return 1.0f;
        }
    }

    protected void changeArrowTransparency(float transparency)
    {
        Color originalColor = speaker.objectArrow.GetComponent<RawImage>().color;

        Color adjustedColor = new Color(originalColor.r, originalColor.g, originalColor.b, transparency);

        speaker.objectArrow.GetComponent<RawImage>().color = adjustedColor;
    }

    public IEnumerator startArrowEffect()
    {
        speaker.objectArrow.SetActive(true);
        float time = 0.0f;

        while (true)
        {
            float tmp = this.stepFunction(Mathf.Sin(time * Mathf.PI * arrowSpeed));
            speaker.objectArrow.transform.position = arrowPosition + new Vector3(0, tmp  * 5, 0);
            if (tmp <= 0.5)
            {
                this.changeArrowTransparency(1.0f);
            }
            else
            {
                this.changeArrowTransparency(0.5f);
            }
            Debug.Log("time : " + time + " sin(time) " + Mathf.Sin(time));
            time += Time.deltaTime;
            yield return null;
        }
        stopArrowEffect();
        yield break;
    }

    public void stopArrowEffect()
    {
        StopCoroutine(startArrowEffect());
        speaker.objectArrow.SetActive(false);
    }

    public void nextDialogue()
    {
        if (printDialogIndex < dialogues.Length)
        {
            printDialogIndex++;
        }
        else
        {
            this.deactivateDialogue();
        }

    }


    
}
