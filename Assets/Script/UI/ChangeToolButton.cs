using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeToolButton : MonoBehaviour
{
    public ButtonLayout changeTool;
    public RectTransform backBar;
    public Transform contents;
    

    private IEnumerator backBarResize(int scale){

        float lerp = 0f;

        while (lerp < 1f){

            backBar.sizeDelta = new Vector2(Mathf.Lerp(backBar.sizeDelta.x, scale, lerp), 100);
            lerp += Time.deltaTime * 6;
            yield return new WaitForEndOfFrame();
        }
    }

    void Start(){

        ChangeToTool("inspect");
    }

    public void ChooseTool(){

        foreach (Transform child in contents){
            foreach (Transform child2 in child){
                Destroy(child2.gameObject);
            }
            Destroy(child.gameObject);
        }

        StartCoroutine(backBarResize(150 + (205 * (FixedVariables.tools.Length - 1))));

        int currentDistance = -225;

        foreach (string tool in FixedVariables.tools){

            if (tool != GameManager.Instance.tool){
                ButtonLayout button = Instantiate(
                (GameObject)GameManager.Instance.getResource("general:ui:basicbutton"), contents).GetComponent<ButtonLayout>();
                button.icon.sprite = GameManager.Instance.getSprite(string.Format("sprites:buttonIcons:{0}", tool));
                button.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                button.button.onClick.AddListener(delegate {ChangeToTool(tool);});
                StartCoroutine(UIUtils.movePanel(button.transform, new Vector2(-50f, -25f), new Vector2(currentDistance, -25f)));
                currentDistance += -205;
            }
        }

        changeTool.button.onClick.RemoveAllListeners();
        changeTool.button.onClick.AddListener(delegate {ChangeToTool(GameManager.Instance.tool);});
        changeTool.icon.sprite = GameManager.Instance.getSprite(string.Format("sprites:buttonIcons:{0}", GameManager.Instance.tool));
    }

    public void ChangeToTool(string tool){

        GameManager.Instance.tool = tool;

        foreach (Transform child in contents){
            foreach (Transform child2 in child){
                Destroy(child2.gameObject);
            }
            Destroy(child.gameObject);
        }

        StartCoroutine(backBarResize(150 + (205 * FixedVariables.toolOptions[tool].Length)));

        int currentDistance = -225;

        foreach (string option in FixedVariables.toolOptions[tool]){

            ButtonLayout button = Instantiate(
            (GameObject)GameManager.Instance.getResource("general:ui:basicbutton"), contents).GetComponent<ButtonLayout>();
            button.icon.sprite = GameManager.Instance.getSprite(string.Format("sprites:buttonIcons:{0}", option));
            button.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            SetSpecialListener(button, option);
            StartCoroutine(UIUtils.movePanel(button.transform, new Vector2(-50f, -25f), new Vector2(currentDistance, -25f)));
            currentDistance += -205;
        }

        changeTool.button.onClick.RemoveAllListeners();
        changeTool.button.onClick.AddListener(delegate {ChooseTool();});
        changeTool.icon.sprite = GameManager.Instance.getSprite(string.Format("sprites:buttonIcons:{0}", GameManager.Instance.tool));
    }

    public void SetSpecialListener(ButtonLayout button, string set){

        switch (set){
            
            case ("build"):

                button.button.onClick.AddListener(delegate {UIController.Instance.OpenBuildMenu();});
                break;

            case ("destroy"):

                button.button.onClick.AddListener(delegate {UIController.Instance.OpenDestroyMenu();});
                break;

        }
    }
}
