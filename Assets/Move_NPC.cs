using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_NPC : MonoBehaviour
{
    public GameObject player;
    public SceneTimer st;
    public float time;
    Vector3 p;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        st = player.GetComponent<SceneTimer>();
        this.time = st.time;
        p = new Vector3(-2.14f,8.09f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.time > (st.scene_time[0] - 10f) && transform.position.y < p.y)
        {
            transform.Translate(Vector3.up * Time.deltaTime);
        }

        this.time = st.time;
    }
}
