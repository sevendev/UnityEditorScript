using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AcademyMenuWindow : EditorWindow
{

    public GameObject selection;

    public int xnum = 3;
    public int ynum = 3;
    public int znum = 3;

    public bool manualOffset = false;
    public float xoffset = 0f;
    public float yoffset = 0f;
    public float zoffset = 0f;
    
    [MenuItem("GameObject/Create Academy", false, 15)]
    static void Init()
    {
        AcademyMenuWindow window = (AcademyMenuWindow)EditorWindow.GetWindow(typeof(AcademyMenuWindow));
        window.selection = Selection.activeGameObject;
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label( "Academy GameObject : " + selection.name, EditorStyles.boldLabel);
        xnum = EditorGUILayout.IntSlider("X", xnum , 1, 30 );
        ynum = EditorGUILayout.IntSlider("Y", ynum, 1, 30);
        znum = EditorGUILayout.IntSlider("Z", znum, 1, 30);

        manualOffset = EditorGUILayout.BeginToggleGroup("Offset", manualOffset);
        xoffset = EditorGUILayout.Slider("X Offset", xoffset, 0f, 1000f);
        yoffset = EditorGUILayout.Slider("Y Offset", yoffset, 0f, 1000f);
        zoffset = EditorGUILayout.Slider("Z Offset", zoffset, 0f, 1000f);
        EditorGUILayout.EndToggleGroup();

        if ( GUILayout.Button("Create"))
        {
            CreateAcademy();
        }
    }

    void CreateAcademy()
    {
        Bounds bounds = new Bounds(selection.transform.position, Vector3.one);
        Renderer[] renderers = selection.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
            bounds.Encapsulate(renderer.bounds);

        float sizex = bounds.size.x;
        float sizey = bounds.size.y;
        float sizez = bounds.size.z;
        xoffset = manualOffset ? xoffset : sizex;
        yoffset = manualOffset ? yoffset : sizey;
        zoffset = manualOffset ? zoffset : sizez;
        for (int i = 0; i < xnum; i++)
        {
            for (int j = 0; j < ynum; j++)
            {
                for (int k = 0; k < znum; k++)
                {
                    if (i == 0 && j == 0 && k == 0 ) continue;
                    GameObject obj = GameObject.Instantiate(selection);
                    obj.name = selection.name + "(" + i + "-" + j + "-" + k + ")";
                    obj.transform.position = 
                        new Vector3(selection.transform.position.x + ((sizex + xoffset) * i),
                        selection.transform.position.y + ((sizey + yoffset) * j),
                        selection.transform.position.z + ((sizez + zoffset) * k));
                }
            }
        }

    }


}