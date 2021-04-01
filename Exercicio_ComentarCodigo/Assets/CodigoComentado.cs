using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodigoComentado : MonoBehaviour
{
    //Array e ponto inicial
    public GameObject[] waypoints;
    int currentWP = 0;

    //Velocidade do player
    public float speed = 3f;

    //determina a distância na qual o player deve estar do objeto antes de procurar uma nova direção para seguir
    public float accuracy = 2f;

    //rotação do player
    public float rotSpeed = 2f;

    void Start()
    {
        //detecta os objetos com a tag Waypoint e adiciona a lista
       // waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
    }

    void LateUpdate()
    {
        
        if (waypoints.Length == 0)
            return;

        //mostra para qual Waypoint o player deve seguir
        Vector3 lookAtGoal = new Vector3(waypoints[currentWP].transform.position.x, this.transform.position.y, waypoints[currentWP].transform.position.z);

        
        Vector3 direction = lookAtGoal - transform.position;
        
        //auxilia na transição do player para o proximo waypoint
        transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);

        //magnitude < direção = novo waypoint
        if (direction.magnitude < accuracy)
        {
            //proximo waypoint
            currentWP++;

            //verifica a existência do proximo waypoint, caso não exista volta ao início (0)
            if (currentWP >= waypoints.Length)
            {
                
                currentWP = 0;
            }
        }

        
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
