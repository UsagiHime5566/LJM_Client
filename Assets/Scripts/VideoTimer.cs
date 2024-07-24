using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoTimer : MonoBehaviour
{
    VideoPlayer vp;

    [SerializeField] double vp_time;

    void Awake(){
        vp = GetComponent<VideoPlayer>();
    }
    
    void Update()
    {
        vp_time = vp.time;
    }
}
