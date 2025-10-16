using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FronkonGames.SpiceUp.Stoned
{
  /// <summary> Sequencer. </summary>
  /// <remarks> This code is designed for a simple demo, not for production environments. </remarks>
  public class Sequencer : MonoBehaviour
  {
    private static readonly List<Sequence> sequences = new();

    private static Coroutine coroutine;
    
    public static Sequence Start()
    {
      Sequence sequence = new Sequence();
      sequences.Add(sequence);
      
      return sequence;
    }

    public static Sequence Start(Sequence sequence)
    {
      sequences.Add(sequence);
      
      return sequence;
    }

    private static IEnumerator UpdateSequences()
    {
      while (true)
      {
        for (int i = sequences.Count - 1; i >= 0; i--)
        {
          if (sequences[i].State == Sequence.States.Done)
            sequences.RemoveAt(i);
          else if (sequences[i].State == Sequence.States.Ready)
            yield return sequences[i].Update();
        }

        yield return null;
      }
    }

    private void Awake()
    {
      coroutine = StartCoroutine(UpdateSequences());
    }

    private void OnDestroy()
    {
      StopCoroutine(coroutine);
    }

    [RuntimeInitializeOnLoadMethod]
    private static void InitializeOnLoad()
    {
      GameObject gameObject = new("Sequencer") { hideFlags = HideFlags.HideInHierarchy };
      gameObject.AddComponent<Sequencer>();
      
      DontDestroyOnLoad(gameObject);
    }
  }

  /// <summary> Sequence. </summary>
  /// <remarks> This code is designed for a simple demo, not for production environments. </remarks>
  public class Sequence
  {
    public enum States
    {
      Ready,
      Running,
      Done
    }

    public States State { get; private set; } = States.Ready;

    private class Step
    {
      public float Duration { get; }
      public Action OnStart { get; }
      public Action<float> OnProgress { get; }
      public Action OnEnd { get; }

      public Func<bool> ConditionWhile { get; }
      public Action<float> Waiting { get; }
      
      public Sequence Child { get; }

      public Step(float duration, Action start = null, Action<float> progress = null, Action end = null, Sequence child = null)
      {
        Duration = Mathf.Max(0.0f, duration);
        OnStart = start;
        OnProgress = progress;
        OnEnd = end;
        Child = child;
      }

      public Step(Func<bool> conditionWhile, Action start = null, Action<float> waiting = null, Action end = null, Sequence child = null)
      {
        ConditionWhile = conditionWhile;
        OnStart = start;
        Waiting = waiting;
        OnEnd = end;
        Child = child;
      }
      
      public Step(Func<bool> conditionWhile, Action<float> waiting = null, Sequence child = null)
      {
        ConditionWhile = conditionWhile;
        Waiting = waiting;
        Child = child;
      }
    }

    private readonly List<Step> steps = new();

    private static readonly WaitForEndOfFrame WaitForEndOfFrame = new();
    
    public Sequence Wait(float seconds)
    {
      steps.Add(new Step(seconds));
      return this;
    }
    
    public Sequence While(Func<bool> conditionWhile, Action start = null, Action<float> waiting = null, Action end = null)
    {
      steps.Add(new Step(conditionWhile, start, waiting, end));
      return this;
    }
    
    public Sequence While(Func<bool> conditionWhile, Action<float> waiting = null)
    {
      steps.Add(new Step(conditionWhile, waiting));
      return this;
    }
    
    public Sequence Then(Action action)
    {
      steps.Add(new Step(0.0f, action));
      return this;
    }

    public Sequence Then(float duration, Action start = null, Action<float> progress = null, Action end = null)
    {
      steps.Add(new Step(Mathf.Max(0.0f, duration), start, progress, end));
      return this;
    }

    public Sequence Then(Sequence sequence)
    {
      steps.Add(new Step(0.0f, null, null, null, sequence));
      return this;
    }
    
    public IEnumerator Update()
    {
      State = States.Running;

      for (int i = 0; i < steps.Count; ++i)
      {
        steps[i].OnStart?.Invoke();

        if (steps[i].Child != null)
          Sequencer.Start(steps[i].Child);
        
        float time = 0.0f;
        if (steps[i].ConditionWhile != null)
        {
          while (steps[i].ConditionWhile.Invoke() == true)
          {
            steps[i].Waiting?.Invoke(time);
            time += Time.deltaTime;

            yield return WaitForEndOfFrame;
          }
        }
        else if (steps[i].Duration > 0.0f)
        {
          float wait = steps[i].Duration;
          while (time <= wait)
          {
            steps[i].OnProgress?.Invoke(Mathf.Clamp01(time / wait));
            time += Time.deltaTime;
            
            yield return WaitForEndOfFrame;
          }

          if (time - wait > 0.0f)
            steps[i].OnProgress?.Invoke(1.0f);
        }

        steps[i].OnEnd?.Invoke();
      }

      State = States.Done;
    }
  }
}