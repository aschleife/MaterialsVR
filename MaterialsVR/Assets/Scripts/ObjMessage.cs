using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enough for now; might need refresh sometime later, thus maybe inherent from MonoBehaviour will be helpful
public static class objMessage{

    private static string name;
    private static string tag;

    public static void loadMessage(string cur_name, string cur_tag)
    {
        name = cur_name;
        tag = cur_tag;
    }

    public static string unLoadMessage(){
    	return name;
    }

    public static bool unLoadIsosurface()
    {
        return tag == "b_iso";
    }

    private static bool isRotational = true;

    public static void pause(){
    	isRotational = false;
    }

    public static void revolve(){
    	isRotational = true;
    }

    public static bool loadBoolean(){
    	return isRotational;
    }

    private static bool beingScale = false;

    public static void disable_scale(){
        beingScale = false;
        Debug.Log(beingScale);
    }

    public static void enable_scale(){
        beingScale = true;
        Debug.Log(beingScale);
    }

    public static bool loadScale(){
        return beingScale;
    }
}
