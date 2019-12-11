using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField][Range (0, 1)] float speed;
    [SerializeField] GameObject[] Rails;
    [SerializeField] GameObject[] PlayerInternal;
    private Transform[][] targets = new Transform[3][];
    private LTSpline[] paths;
    private Vector3[][] targetVectors = new Vector3[3][];
    private float trackPosition;
    private int activeRailNum = 1;
    private Transform activeRail;
    void Start ()
    {
        if (Rails != null)
        {
            int i = 0;
            foreach (var Rail in Rails)
            {
                //targetsに各レールのガイドのtransformを入れる
                targets[i] = Rail.GetComponentsInChildrenWithoutSelf<Transform> ();
                i++;
            }

            for (int k = 0; k < targets.Length; k++)
            {
                //初期化処理
                targetVectors[k] = new Vector3[targets[k].Length];
                paths = new LTSpline[targets[k].Length];
            }
            for (int j = 0; j < targets.Length; j++)
            {
                for (int l = 0; l < targets[0].Length; l++)
                {
                    targetVectors[j][l] = targets[j][l].position;
                }
            }
            pathSetUp ();
        }
    }

    // Update is called once per frame
    void Update ()
    {
        paths[0].place (PlayerInternal[0].transform, trackPosition);
        paths[1].place (PlayerInternal[1].transform, trackPosition);
        paths[2].place (PlayerInternal[2].transform, trackPosition);
        trackPosition += Time.deltaTime * speed;
        
        if (Input.GetKeyDown (KeyCode.RightArrow))
        {
            activeRailNum++;
            if (activeRailNum >= PlayerInternal.Length)
            {
                activeRailNum = PlayerInternal.Length - 1;
            }
        }
        if (Input.GetKeyDown (KeyCode.LeftArrow))
        {
            activeRailNum--;
            if (activeRailNum < 0)
            {
                activeRailNum = 0;
            }
        }

        switch (activeRailNum)
        {
            case 0:
                this.transform.parent = PlayerInternal[0].transform;
                this.transform.localPosition = Vector3.zero;
                break;
            case 1:
                this.transform.parent = PlayerInternal[1].transform;
                this.transform.localPosition = Vector3.zero;
                break;
            case 2:
                this.transform.parent = PlayerInternal[2].transform;
                this.transform.localPosition = Vector3.zero;
                break;
                //レーン増やしたら増やすこのやり方なんとかしたい
        }

    }

    void pathSetUp ()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            paths[i] = new LTSpline (targetVectors[i]);
        }
    }
}