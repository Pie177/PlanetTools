using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlanetBase))]
public class PlanetScriptEditor : Editor {

    bool hasMoons = false;
    int numElements;


    public override void OnInspectorGUI()
    {
        PlanetBase myTarget = (PlanetBase)target;

        //Planet Shape
        EditorGUILayout.LabelField("Planet Shape", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical("box");
        myTarget.mainColor = EditorGUILayout.ColorField("Planet Color", myTarget.mainColor);
        myTarget.orbitTime = EditorGUILayout.FloatField(new GUIContent("Orbit Time","The Earth takes 365 days to orbit"), myTarget.orbitTime);
        myTarget.planetSize = EditorGUILayout.IntField(new GUIContent("Planet Size in miles", "The Earth has a radius of 3,959 miles"),myTarget.planetSize);
        myTarget.revolutionTime = EditorGUILayout.FloatField(new GUIContent("Revolution time","The Earth takes 24 hours to revolve"), myTarget.revolutionTime);
        EditorGUILayout.Space();
        myTarget.lowElevation = EditorGUILayout.FloatField(new GUIContent("Lowest Elevation", "Measured in Miles"), myTarget.lowElevation);
        myTarget.highElevation = EditorGUILayout.FloatField(new GUIContent("Highest Elevation", "Measured in Miles"), myTarget.highElevation);
        EditorGUILayout.MinMaxSlider(ref myTarget.lowElevation, ref myTarget.highElevation, 0f, myTarget.planetSize);
        hasMoons = EditorGUILayout.BeginToggleGroup("Has Moons", hasMoons);
        myTarget.moonAmount = EditorGUILayout.IntField("Number of Moons", myTarget.moonAmount);
        if (hasMoons == false)
            myTarget.moonAmount = 0;
        EditorGUILayout.EndToggleGroup();
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();
        //Planet Surface
        EditorGUILayout.LabelField("Planet Surface", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical("box");
        myTarget.radiationAmount = EditorGUILayout.FloatField(new GUIContent("Radiation", "The Earth emits 3-5 mSv per year"), myTarget.radiationAmount);
        myTarget.hasWater = EditorGUILayout.Toggle("Water" ,myTarget.hasWater);
        myTarget.lowTemp = EditorGUILayout.FloatField(new GUIContent("Lowest Temperature", "Measured in Celsius"), myTarget.lowTemp);
        myTarget.highTemp = EditorGUILayout.FloatField(new GUIContent("Highest Temperature", "Measured in Celsius"), myTarget.highTemp);
        EditorGUILayout.MinMaxSlider(ref myTarget.lowTemp, ref myTarget.highTemp, 0f, 1000f);
        serializedObject.Update();
        SerializedProperty myElem = serializedObject.FindProperty("mainElements");
        EditorGUILayout.PropertyField(myElem, true);
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();
        //Planet Life
        EditorGUILayout.LabelField("Planet Life", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical("box");
        myTarget.isHabitable = EditorGUILayout.Toggle("Habitable", myTarget.isHabitable);
        GUI.enabled = myTarget.isHabitable;
        myTarget.flora = EditorGUILayout.Toggle(new GUIContent("Flora", "Plants"), myTarget.flora);
        GUI.enabled = myTarget.flora;
        myTarget.fauna = EditorGUILayout.Toggle(new GUIContent("Fauna", "Animals"), myTarget.fauna);
        GUI.enabled = true;

        if (!myTarget.isHabitable)
            myTarget.flora = false;

        if (!myTarget.flora)
            myTarget.fauna = false;
        EditorGUILayout.Space();
        myTarget.intelligentCreatures = EditorGUILayout.BeginToggleGroup("Intelligent Creatures", myTarget.intelligentCreatures);
        myTarget.icPopulation = EditorGUILayout.IntField("Number of Creatures", myTarget.icPopulation);
        EditorGUILayout.EndToggleGroup();
        if (myTarget.intelligentCreatures == false)
            myTarget.icPopulation = 0;
        EditorGUILayout.EndVertical();
        //base.OnInspectorGUI();
    }
}
