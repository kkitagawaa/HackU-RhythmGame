using System;
using System.Collections.Generic;
using UnityEngine;

internal class UnityMainThread : MonoBehaviour
{
    private static UnityMainThread instance = null;
				public static UnityMainThread Instance
    {
        get
        {
            return UnityMainThread.instance;
        }
    }
    private Queue<Action> aJobs = new Queue<Action>();
				public Queue<Action> Jobs
				{
								get
								{
												return this.aJobs;
								}
				}

    public void Awake()
    {
        if (UnityMainThread.instance == null)
        {
            UnityMainThread.instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    public void Update()
    {
        while (aJobs.Count > 0)
            aJobs.Dequeue().Invoke();
    }

    internal void AddJob(Action newJob)
    {
        aJobs.Enqueue(newJob);
    }
}