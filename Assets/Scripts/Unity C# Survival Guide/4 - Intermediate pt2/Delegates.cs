using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delegates : MonoBehaviour
{
    public delegate void ChangeColor(Color newColor);
    public ChangeColor onColorChange;

    public delegate void OnComplete();
    public OnComplete onComplete;


    public Action<int, int> onGetSum;
    public Action onGetName;
    public Func<int> onGetNameLength;
    public Func<int, int, string> onStringSum;

    public void Start()
    {
        onColorChange = UpdateColor;
        if (onColorChange != null)
            onColorChange(Color.magenta);

        onComplete = Task;
        onComplete += Task2;
        onComplete += Task3;
        if (onComplete != null)
            onComplete();

        onGetSum = (a, b) =>
        {
            var sum = a + b;
            Debug.Log("Sum of " + a + " & " + b + " is " + sum);
        };
        if (onGetSum != null)
            onGetSum(4, 9);

        onGetName = () => Debug.Log("this GameObject name is "+this.gameObject.name);
        if (onGetName != null)
            onGetName();

        onGetNameLength = () => this.gameObject.name.Length;
        if (onGetNameLength != null)
        {
            var nameLen = onGetNameLength();
            Debug.Log("Name length is " + nameLen);
        }

        onStringSum = (i, j) =>
        {
            int sum = i + j;
            return sum.ToString();
        };
        if (onStringSum != null)
        {
            int i = 5, j = 5;
            string sumString = onStringSum(i, j);
            Debug.Log("Sum of " + i + " & " + j + " is " + sumString);
        }
            
    }

    public void ButtonClick()
    {
        this.gameObject.SetActive(true);
    }

    public void UpdateColor(Color newColor)
    {
        Debug.Log("Changing color to "+newColor.ToString());
    }

    public void Task()
    {
        Debug.Log("Task Finished");
    }

    public void Task2()
    {
        Debug.Log("Task 2 Finished");
    }

    public void Task3()
    {
        Debug.Log("Task 3 Finished");
    }
}
