using System;

public interface IProgressable
{
    float Progress { get; }
    event Action<float> OnProgressChanged;
    void SetProgress(float value, bool notify = true);
}
