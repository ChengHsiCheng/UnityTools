using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeNode : DecoratorNode
{
    private enum JudgeType
    {
        more, equal, less
    }

    public string variableName;
    public VariableType variableType;
    [SerializeField] JudgeType judgeType;
    public string compareVariable;

    public bool canBreak;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (child.state == State.Running && !canBreak)
            return child.Update();

        if (variableType == VariableType.Int)
        {
            return JudgeInt();
        }
        if (variableType == VariableType.Float)
        {
            return JudgeFloat();
        }
        if (variableType == VariableType.Bool)
        {
            return JudgeBool();
        }

        return State.Failure;
    }

    private State JudgeInt()
    {
        int a = variables.GetInt(variableName);
        int b = int.Parse(compareVariable);

        if (judgeType == JudgeType.more)
        {
            if (a > b)
            {
                return child.Update();
            }
            return State.Failure;
        }

        if (judgeType == JudgeType.equal)
        {
            if (a == b)
            {
                return child.Update();
            }
            else
                return State.Failure;
        }

        if (judgeType == JudgeType.less)
        {
            if (a < b)
            {
                return child.Update();
            }
            else
                return State.Failure;
        }

        return State.Failure;
    }

    private State JudgeFloat()
    {
        float a = variables.GetFloat(variableName);
        float b = float.Parse(compareVariable);

        if (judgeType == JudgeType.more)
        {
            if (a > b)
            {
                return child.Update();
            }
            else
                return State.Failure;
        }

        if (judgeType == JudgeType.equal)
        {
            if (a == b)
            {
                return child.Update();
            }
            else
                return State.Failure;
        }

        if (judgeType == JudgeType.less)
        {
            if (a < b)
            {
                return child.Update();
            }
            else
                return State.Failure;
        }

        return State.Failure;
    }

    private State JudgeBool()
    {
        bool a = variables.GetBool(variableName);
        bool b = compareVariable == "true";

        if (judgeType == JudgeType.equal)
        {
            if (a == b)
            {
                return child.Update();
            }
            else
                return State.Failure;
        }

        return State.Failure;
    }
}
