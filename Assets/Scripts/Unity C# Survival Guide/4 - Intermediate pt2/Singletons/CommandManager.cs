using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CommandManager : MonoSingleton<CommandManager>
{
    public override void Init()
    {
        base.Init();
    }

    private List<ICommand> _commandBuffer = new List<ICommand>();

    public void AddCommand(ICommand command)
    {
        _commandBuffer.Add(command);
    }

    public void Play()
    {
        StartCoroutine(PlayRoutine());
    }

    IEnumerator PlayRoutine()
    {
        Debug.Log("Playing...");
        foreach (var command in _commandBuffer)
        {
            command.Execute();
            if(command.GetType().Name=="ClickCommand")
                yield return new WaitForSeconds(1);
            else
                yield return new WaitForEndOfFrame();
        }
        Debug.Log("Finished.");
    }

    public void Rewind()
    {
        StartCoroutine(RewindRoutine());
    }

    IEnumerator RewindRoutine()
    {
        Debug.Log("Rewinding...");
        foreach (var command in Enumerable.Reverse(_commandBuffer))
        {
            command.Undo();
            if (command.GetType().Name == "ClickCommand")
                yield return new WaitForSeconds(1);
            else
                yield return new WaitForEndOfFrame();
        }
        Debug.Log("Finished.");
    }

    public void Done()
    {
        var objects = GameObject.FindGameObjectsWithTag("Command Pattern");
        var target = GameObject.Find("Player Target");
        target.transform.position = new Vector3(0, 0, 5);
        foreach (var obj in objects)
        {
            UtilityHelper.ChangeColor(obj, Color.white);
        }
    }

    public void Reset()
    {
        _commandBuffer.Clear();
        Done();
    }
}
