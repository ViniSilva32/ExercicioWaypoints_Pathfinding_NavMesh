using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOLLOWPATH : MonoBehaviour
{
    Transform Goal;
    //determina a velocidade de movimento do BOT
    float speed = 5.0f;
    //determina a precisão do movimento do BOT
    float accuracy = 3.0f;
    //determina a velocidade do movimento de rotação do BOT
    float rotSpeed = 2.0f;

    //referencia o WPMANAGER
    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graph g;

    // Start is called before the first frame update
    void Start()
    {   //pegam os componentes waypoints e graph que estão no FOLLOWPATH e atribui ao wps e ao g.
        wps = wpManager.GetComponent<WPMANAGER>().waypoints;
        g = wpManager.GetComponent<WPMANAGER>().graph;
        //lista onde o wps inicia em 0.
        currentNode = wps[0];
    }

    //indica a posição do Heli
    public void GoToVenus()
    {
        g.AStar(currentNode, wps[0]);
        currentWP = 0;
    }

    //indica a posição das Ruins
    public void GoToPlutao()
    {
        g.AStar(currentNode, wps[3]);
        currentWP = 0;
    }
    //indica a posição de Marte
    public void GoToMarte()
    {
        g.AStar(currentNode, wps[4]);
        currentWP = 0;
    }
    // Update is called once per frame
    void LateUpdate()
    {   //retorna as informações recebidas pelo currentWP
        if (g.getPathLength() == 0 || currentWP == g.getPathLength())
            return;

        currentNode = g.getPathPoint(currentWP);
        //mede a distancia do objetivo e muda a sua direção quando atingir a accuracy determinada
        if (Vector3.Distance(g.getPathPoint(currentWP).transform.position, transform.position) < accuracy)
        {
            //contador currentWP
            currentWP++;
        }
            if(currentWP < g.getPathLength())
            {   //altera o objetivo do BOT gerando um novo ponto a ser percorrido
                Goal = g.getPathPoint(currentWP).transform;
                Vector3 lookAtGoal = new Vector3(Goal.position.x, this.transform.position.y, Goal.position.z);
                Vector3 direction = lookAtGoal - this.transform.position;
                //auxilia nas curvas que o bot faz na mudança de um ponto para outro
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(direction),Time.deltaTime * rotSpeed);


            }
            //altera a direção do BOT
            transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
