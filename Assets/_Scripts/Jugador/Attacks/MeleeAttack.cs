using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ControllesSOSCript controles;
    [SerializeField] private Animator animator;
    [SerializeField] private CameraManager cameraManager;



    [Header("Melee Attack Settings")]
    [SerializeField] private Transform AttackController;
    [SerializeField] private float attackRange = 1.0f;
    [SerializeField] private float damage;
    private HashSet<GameObject> EnemigosGolpeados = new HashSet<GameObject>(); // Usar un HashSet para evitar duplicados en los enemigos golpeados
    [SerializeField] private float tiempoDeAtaque = 0.2f; // Tiempo total del ataque
    private float tiempoActual = 0f; // Tiempo actual del ataque, se incrementará en cada frame durante el ataque
    [SerializeField]private bool estaAtacando = false; // Bandera para evitar múltiples ataques simultáneos
    



    //camera shake settings
    public delegate void tipoEventoCamara();
    public event tipoEventoCamara EventoShakeCamaraGolpe;





    private void Awake()
    {
        controles.EventoAtaque += Hit;
    }
    private void OnDisable()
    {
        controles.EventoAtaque -= Hit;
    }


    private void Hit()
    {
        if (!estaAtacando)
        {
            animator.CrossFade("AtaqueLigero", 0.5f);//nombre incorrecto pero al cambiar el nombre de la animacion no se cambia el nombre de la referencia NO SE PORQUE XD
            EnemigosGolpeados.Clear(); // Limpia el conjunto de enemigos golpeados antes de cada ataque

            // Collider[] hitEnemies = Physics.OverlapSphere(AttackController.position, attackRange); 
            StartCoroutine(Golpe()); // Inicia la coroutine para manejar el golpe y esperar el tiempo de la animación

            estaAtacando = true; // Marca que se está atacando para evitar múltiples ataques simultáneos
        }


    }



    IEnumerator Golpe()
    {
        tiempoActual = 0f; // Reinicia el tiempo actual del ataque
        Collider[] results = new Collider[10];



        while (tiempoActual<tiempoDeAtaque)
        {
            tiempoActual += Time.deltaTime; // Incrementa el tiempo actual del ataque
             // Buffer reutilizable
            int count = Physics.OverlapSphereNonAlloc(
                AttackController.position,
                attackRange,
                results
            );
            for (int i = 0; i < count; i++)
            {
                Collider enemy = results[i];
                if (enemy.TryGetComponent<Enemy>(out Enemy vidaEnemigo))
                {
                    if (!EnemigosGolpeados.Contains(enemy.gameObject))
                    {

                        enemy.GetComponent<Enemy>().TakeDamage(damage);
                        EventoShakeCamaraGolpe?.Invoke(); // Invoca el evento de sacudida de cámara al golpear un enemigo
                        EnemigosGolpeados.Add(enemy.gameObject); // Añade el enemigo al conjunto de enemigos golpeados para evitar duplicados


                    }

                }
            }
            

            
            yield return null; // Espera un frame antes de continuar
            

        }
        estaAtacando = false; // Marca que ya no se está atacando
        yield return null;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackController.position, attackRange);
    }
}





