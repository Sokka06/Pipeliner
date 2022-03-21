using System;
using System.Collections;
using System.Collections.Generic;
using Demos.Common;
using Sokka06.Pipeliner;
using UnityEngine;

[Serializable]
public struct LoadDataStepParameters: IStepParameters
{
    public DataSystem DataSystem;
    public IData Data;
}

public class LoadDataStepFactory : StepFactoryBehaviour
{
    public DataSystem DataSystem;
    
    public override IStep[] Create()
    {
        var parameters = new LoadDataStepParameters
        {
            DataSystem = DataSystem,
            Data = new UserData()
        };
        return new IStep[]{new LoadDataStep<UserData>(parameters)};
    }
}
