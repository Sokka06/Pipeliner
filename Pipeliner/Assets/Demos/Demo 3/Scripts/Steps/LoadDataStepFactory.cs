using System;
using System.Collections;
using System.Collections.Generic;
using Demos.Common;
using Sokka06.Pipeliner;
using UnityEngine;

namespace Demos.Demo3
{
    [Serializable]
    public struct LoadDataStepParameters: IStepParameters
    {
        public DataSystem DataSystem;
    }

    public class LoadDataStepFactory : StepFactoryBehaviour
    {
        public DataSystem DataSystem;
    
        public override IStep[] Create()
        {
            var parameters = new LoadDataStepParameters
            {
                DataSystem = DataSystem
            };
            return new IStep[]{new LoadDataStep<GameData>(parameters)};
        }
    }
}