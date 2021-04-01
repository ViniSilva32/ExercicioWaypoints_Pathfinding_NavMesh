using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//não entendi muito bem!
public struct Link
{
public enum direction {UNI,BI}
//criação dos objetos node
public GameObject node01;
public GameObject node02;
//criação das variaveis de direção
public direction dir;

}
public class WPMANAGER : MonoBehaviour
{   //criação do objeto waypoint
    public GameObject[] waypoints;
    public Link[] links;
    //Criação da variavel para fazer com que o Graph receba novais informações
    public Graph graph = new Graph();

    // Start is called before the first frame update
    void Start()
    {   //verifica se o tamanho é maior que 0.
        if (waypoints.Length > 0)
        {   //adiciona nodes dentro dos graphs
            foreach (GameObject wp in waypoints)
            {
                graph.AddNode(wp);
            }
            //adiciona as arestas nos links feitos, faz a verificação da Existencia do BI, caso exista faz o BOT fazer o caminho reverso
            foreach (Link l in links)
            {
                graph.AddEdge(l.node01, l.node02);
                if (l.dir == Link.direction.BI);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {   //faz com que o caminho que o BOT irá fazer seja visível!
        graph.debugDraw();
    }
}
