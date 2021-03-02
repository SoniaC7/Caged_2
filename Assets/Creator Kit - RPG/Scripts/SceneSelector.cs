using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSelector : MonoBehaviour
{
    private List<ArrayList> unity_scenes; //format to pass the SceneTimer, contains if take time into account, the number of scenes changes an each time for them

    // Start is called before the first frame update
    void Start()
    {
        unity_scenes = new List<ArrayList>();
        var scene = new ArrayList();
        //introduction
        scene.Add(false);
        scene.Add(3);
        unity_scenes.Add(scene);
        //mission 1
        scene.Add(true);
        scene.Add(3);
        scene.Add(910);
        scene.Add(260);
        scene.Add(260);
        unity_scenes.Add(scene);
        //mission 2
        scene.Add(true);
        scene.Add(1);
        unity_scenes.Add(scene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

//por definir, intención hacer que organice para el prototipo las escenas necesarias según la unidad narrativa actual
//e.g. tutorial (3 celda-pasillo-reunionAbogado, sin tiempo) - misión1 (3 celda-patio-economato)