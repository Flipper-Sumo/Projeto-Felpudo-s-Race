using UnityEngine;

public class GeradorWorld : MonoBehaviour
{
    [Header("Referências")]
    public Transform playerTransform;

    [Header("Prefabs")]
    public GameObject[] prefabsInimigos;
    public GameObject[] prefabsColetaveis;

    [Header("Configurações de Spawn")]
    public float distanciaFrente = 20f;
    public float tempoSpawnInimigo = 3.5f;
    public float tempoSpawnColetavel = 1.2f;

    [Header("Alturas das Pistas")]
    public float alturaPistaBaixo = -2.5f;
    public float alturaPistaMeio = -0.8f;

    private float timerInimigo;
    private float timerColetavel;

    private void Update()
    {
        // Se a referência ao Player for perdida ao recarregar a cena, reconecta via Tag
        if (playerTransform == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) playerTransform = p.transform;
            else return;
        }

        timerInimigo += Time.deltaTime;
        timerColetavel += Time.deltaTime;

        if (timerInimigo >= tempoSpawnInimigo)
        {
            SpawnObjeto(prefabsInimigos);
            timerInimigo = 0f;
        }

        if (timerColetavel >= tempoSpawnColetavel)
        {
            SpawnObjeto(prefabsColetaveis);
            timerColetavel = 0f;
        }
    }

    private void SpawnObjeto(GameObject[] listaPrefabs)
    {
        if (listaPrefabs.Length == 0) return;

        GameObject prefab = listaPrefabs[Random.Range(0, listaPrefabs.Length)];

        int pista = Random.Range(0, 2);
        float posY = (pista == 0) ? alturaPistaBaixo : alturaPistaMeio;

        Vector3 posSpawn = new Vector3(playerTransform.position.x + distanciaFrente, posY, 0f);

        Instantiate(prefab, posSpawn, Quaternion.identity);
    }
}