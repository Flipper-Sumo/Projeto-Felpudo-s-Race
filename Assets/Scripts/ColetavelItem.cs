using UnityEngine;

public class ColetavelItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D colisao)
    {
        if (colisao.CompareTag("Player"))
        {
            if (GerenciadorJogo.instancia != null)
            {
                GerenciadorJogo.instancia.AdicionarMoeda();
            }
            Destroy(gameObject);
        }
    }
}