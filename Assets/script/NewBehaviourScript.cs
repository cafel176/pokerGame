using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public string context;

    private string myFun(string text)
    {
        return text + " our game.";
    }

    void Start () {
        context = "Let's start";
	}
	
	void Update () {
        string a = myFun(context);
        System.Console.WriteLine(a);
    }
}
