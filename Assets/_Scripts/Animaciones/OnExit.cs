using System.Collections;
using UnityEngine;

public class OnExit : StateMachineBehaviour
{

    [SerializeField] private AnimacionesJugador animacionObjetivo;
    [SerializeField] bool bloquearCapa=false;
    [SerializeField] bool cancelarAnimacion=false;
    [SerializeField] private float crossfade = 0.01f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AnimatorBrain target = animator.GetComponentInParent<AnimatorBrain>();
        target.StartCoroutine(Wait());

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(stateInfo.length);
            target.SetBloquearCapa(false, layerIndex);
            target.ReproducirAnimacion(animacionObjetivo, layerIndex==0?CapasAnimacion.CapaSuperior: CapasAnimacion.CapaInferior, bloquearCapa, cancelarAnimacion, crossfade);
        }
    }


}
