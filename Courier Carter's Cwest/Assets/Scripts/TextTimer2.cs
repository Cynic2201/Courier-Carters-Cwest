using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TextTimer2 : MonoBehaviour
{
	TextMesh text;
	float time;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent("TextMesh") as TextMesh;
    }

    // Update is called once per frame
    void Update()
    {
		time += Time.deltaTime;
        text.text = Math.Round(time, 2) + "\n /60.00";
    }
}