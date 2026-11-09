using UnityEngine;

public class InimigoObstaculo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D colisao)
    {
        if (colisao.CompareTag("Player"))
        {
            if (GerenciadorJogo.instancia != null)
            {
                GerenciadorJogo.instancia.PerderVida();
            }
            Destroy(gameObject);
        }
    }
}