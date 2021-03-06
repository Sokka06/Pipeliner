![Module Controller](https://repository-images.githubusercontent.com/457441217/fa2ecbbc-d7d5-4a96-93b4-7acb48d20982)

## What is it?
*Pipeliner* helps you execute sequences, such as initializing social services, loading savegames, loading scenes, level generation and more using custom made *Steps*.

## Getting started
1. Clone or download repository.
2. Copy **Pipeliner** folder from **Plugins** folder to your project.
3. Take a look at the included *Demos*.
4. Done!

Developed in **Unity 2020.3.23f1**, but I don't see why it wouldn't work in any Unity version.

## How To Use
1. Create a new script for your custom *Step* (named *FooStep*, for example). *Step* classes are used to execute your logic. Your class should look something like this:
```csharp
public struct FooStepParameters : IStepParameters
{
    public float Value;
}

public class FooStep : AbstractStep
{
    public FooStep(FooStepParameters parameters) : base(parameters)
    {
    }

    public override IEnumerator Run(Action<IStepResult> result)
    {
        yield return null;
        var parameters = (FooStepParameters)Parameters;

        // Your custom logic here

        Progress = 1f;
        result?.Invoke(new IStepResult.Success());
    }
}
```
2. Create new scripts for your *Step Factories* (named *FooStepBehaviour* and *FooStepObject*, for example). Factories are used to create a new instance of your step in **MonoBehaviour** and **Scriptable Object** pipelines. Your factory classes should look something like this:
```csharp
public class FooStepBehaviour : StepFactoryBehaviour
{
    public override IStep[] Create()
    {
        return new IStep[] {new FooStep(new FooStepParameters{Value = 0f})};
    }
}

[CreateAssetMenu(fileName = "Foo Step", menuName = "Foo/Step")]
public class FooStepObject : StepFactoryObject
{
    public override IStep[] Create()
    {
        return new IStep[] {new FooStep(new FooStepParameters{Value = 0f})};
    }
}
```
3. Add a **Pipeline Runner** and **Pipeline** GameObjects to your scene (found in **GameObject/Pipeliner**) and drag **Pipeline** GameObject to the *Pipeline* field in **Runner** GameObject.
4. Add your custom *FooStepBehaviour* script to the **Pipeline** GameObject.
6. Press play!

## Demos
### 1. Basic
A basic example that prints `Debug.Log`s using a MonoBehaviour *Pipeline*.
### 2. Scriptable Objects
A demo that uses Scriptable Object *Pipelines* and *Steps*.
### 3. Custom Steps
A demo that shows how to use *Pipeliner* to initialize services and save data using custom *Steps*.
### 4. Scripting
A demo that shows how to create and run a *Pipeline* and *Steps* in script to load menus and levels.

## Notes

## Assets
[DoTween](http://dotween.demigiant.com/)
[Font Awesome](https://fontawesome.com/)
#### Models
[Car Kit](https://www.kenney.nl/assets/car-kit)
[Platformer Kit](https://kenney.nl/assets/platformer-kit)
[Input Prompts](https://kenney.nl/assets/input-prompts-pixel-16)
#### Effects
[Explosion](https://assetstore.unity.com/packages/essentials/tutorial-projects/unity-particle-pack-127325)
#### Sounds
[Engine](https://freesound.org/people/cr4sht3st/sounds/157144/)
[Skid](https://freesound.org/people/audible-edge/sounds/71739/)
[Impact1](https://freesound.org/people/Halleck/sounds/121622/)
[Impact2](https://freesound.org/people/Halleck/sounds/121657/)
[Impact3](https://freesound.org/people/Halleck/sounds/121656/)
[Explosion](https://freesound.org/people/derplayer/sounds/587198/)
