using Unity.Jobs;
using Unity.Collections;
using UnityEngine;

public class Example : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DoExample();
    }

    private void DoExample()
    {
        NativeArray<float> resultArray = new NativeArray<float>(1, Allocator.TempJob);

        //instantiate
        SimpleJob myjob = new SimpleJob()
        {
            //initialize
            a = 5f,
            result = resultArray
        };

        AnotherJob secondJob = new AnotherJob();
        secondJob.result = resultArray;

        //schedule
        JobHandle handle = myjob.Schedule();
        JobHandle secondHandle = secondJob.Schedule(handle);
        //Do other main thread stuffs

        //handle.Complete(); //dependency implies this
        secondHandle.Complete();
        float resultingValue = resultArray[0];
        Debug.Log("result is " + resultingValue);
        Debug.Log("Job.a is " + myjob.a);

        resultArray.Dispose();
    }

    private struct SimpleJob : IJob
    {
        public float a;
        public NativeArray<float> result;
        public void Execute()
        {
            result[0] = a;
            a = 23;
        }
    }

    private struct AnotherJob : IJob
    {
        public NativeArray<float> result;
        public void Execute()
        {
            result[0] = result[0] + 1;
        }
    }
}
