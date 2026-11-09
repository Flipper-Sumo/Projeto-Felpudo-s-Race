using UnityEngine;

public class MovimentacaoRunner : MonoBehaviour
{
    [Header("Configurações de Corrida")]
    [SerializeField] private float forwardSpeed = 7f;     // Velocidade automática para a frente
    [SerializeField] private float laneChangeSpeed = 15f; // Velocidade de transição entre as pistas
    [SerializeField] private float jumpForce = 12f;       // Força do pulo

    [Header("Alturas das Pistas (Ajuste no Inspector)")]
    [SerializeField] private float alturaPistaBaixo = -2.5f; // Posição Y exata da pista inferior
    [SerializeField] private float alturaPistaMeio = -0.5f;  // Posição Y exata da pista superior/meio

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private int targetLane = 0; // 0 = Pista de Baixo, 1 = Pista do Meio
    private bool jumpRequested;
    private string estadoAtual = "";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        TrocarAnimacao("Correndo");
    }

    private void Update()
    {
        // 1. Troca entre apenas 2 pistas (Setas / W e S)
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            targetLane = 1; // Vai para a pista do meio
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            targetLane = 0; // Volta para a pista de baixo
        }

        // 2. Solicitação de Pulo
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.linearVelocity.y) < 0.1f)
        {
            jumpRequested = true;
        }

        // 3. Controle de Animação
        bool noChao = Mathf.Abs(rb.linearVelocity.y) < 0.1f;

        if (!noChao)
            TrocarAnimacao("Pulando");
        else
            TrocarAnimacao("Correndo");
    }

    private void FixedUpdate()
    {
        // Seleciona a posição Y exata com base na pista (0 ou 1)
        float targetY = (targetLane == 0) ? alturaPistaBaixo : alturaPistaMeio;

        // Interpola suavemente a posição vertical
        float newY = Mathf.Lerp(rb.position.y, targetY, laneChangeSpeed * Time.fixedDeltaTime);

        float currentYVelocity = rb.linearVelocity.y;

        if (jumpRequested)
        {
            currentYVelocity = jumpForce;
            jumpRequested = false;
        }

        rb.linearVelocity = new Vector2(forwardSpeed, jumpRequested ? currentYVelocity : rb.linearVelocity.y);

        // Move o personagem no eixo X e ajusta o Y para a pista correta
        rb.MovePosition(new Vector2(rb.position.x + forwardSpeed * Time.fixedDeltaTime, newY));
    }

    private void TrocarAnimacao(string nome)
    {
        if (estadoAtual == nome) return;

        estadoAtual = nome;
        animator.Play(nome);
    }
}