using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private Text textLabel;
    [SerializeField] private TextAsset textFile;
    private int index;
    private bool textEnd = true;
    private bool skipText;

    List<string> textList = new List<string>();

    void Start()
    {
        var excelRow = ExcelReader.ReadExcel("Sheet1");
        for (int i = 1; i <= excelRow.Count - 1; i++)
        {
            Debug.Log(excelRow[i][1]);
        }

        GetTextFormFile(textFile);
        NextText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextText();
        }
    }

    void NextText()
    {
        if (index >= textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }

        if (!textEnd)
        {
            skipText = true;
            return;
        }

        StartCoroutine(SetText());
    }

    void GetTextFormFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var lineDate = file.text.Split("\n");

        foreach (var line in lineDate)
        {
            textList.Add(line);
        }
    }

    IEnumerator SetText()
    {
        textEnd = false;
        textLabel.text = "";

        int letter = 0;
        while (letter < textList[index].Length - 1 && !skipText)
        {
            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(0.1f);
        }

        textLabel.text = textList[index];
        index++;
        skipText = false;
        textEnd = true;
        yield return 0;
    }
}
