using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GerenciadorJogo : MonoBehaviour
{
    public static GerenciadorJogo instancia;

    [Header("UI")]
    public TextMeshProUGUI textoMoedas;
    public GameObject[] coracoesUI;
    public GameObject painelGameOver;
    public Button botaoReiniciar;

    [Header("Atributos")]
    public int moedas = 0;
    public int vidas = 3;

    private void Awake()
    {
        // Define a referência da instância para o objeto ativo nesta cena
        instancia = this;
    }

    private void Start()
    {
        // Restaura a escala de tempo imediatamente no início da partida
        Time.timeScale = 1f;

        vidas = 3;
        moedas = 0;

        AtualizarUI();

        if (painelGameOver != null)
            painelGameOver.SetActive(false);

        if (botaoReiniciar != null)
        {
            botaoReiniciar.onClick.RemoveAllListeners();
            botaoReiniciar.onClick.AddListener(ReiniciarJogo);
        }
    }

    public void AdicionarMoeda()
    {
        moedas++;
        AtualizarUI();
    }

    public void PerderVida()
    {
        vidas--;
        AtualizarUI();

        if (vidas <= 0)
        {
            GameOver();
        }
    }

    private void AtualizarUI()
    {
        if (textoMoedas != null)
            textoMoedas.text = "Moedas: " + moedas;

        for (int i = 0; i < coracoesUI.Length; i++)
        {
            if (coracoesUI[i] != null)
                coracoesUI[i].SetActive(i < vidas);
        }
    }

    private void GameOver()
    {
        if (painelGameOver != null)
            painelGameOver.SetActive(true);

        // Pausa a física e os cronômetros
        Time.timeScale = 0f;
    }

    public void ReiniciarJogo()
    {
        // Restaura a velocidade ANTES de recarregar a cena
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}