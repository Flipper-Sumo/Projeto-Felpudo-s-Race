using UnityEngine;

public class MovimentacaoRunner : MonoBehaviour
{
    [Header("Configurações de Corrida")]
    [SerializeField] private float forwardSpeed = 7f;     // Velocidade automática para a frente
    [SerializeField] private float laneDistance = 2.5f;   // Distância entre as pistas (Y ou X dependendo da visão, aqui usaremos o eixo Y para pistas verticais ou X para horizontais)
    [SerializeField] private float laneChangeSpeed = 15f; // Velocidade de transição entre as pistas
    [SerializeField] private float jumpForce = 12f;       // Força do pulo

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private int targetLane = 1; // 0 = Pista de Baixo, 1 = Pista do Meio, 2 = Pista de Cima
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
        TrocarAnimacao("Andando"); // No runner, ele começa correndo
    }

    private void Update()
    {
        // 1. Troca de pistas usando as setas verticais ou W/S
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            targetLane++;
            if (targetLane > 2) targetLane = 2; // Limita na pista superior
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            targetLane--;
            if (targetLane < 0) targetLane = 0; // Limita na pista inferior
        }

        // 2. Solicitação de Pulo (Barra de Espaço)
        // Nota: Para runners simples, às vezes removemos a checagem rigorosa de chão para permitir pulo duplo ou fluidez, 
        // mas mantemos uma tolerância básica se necessário.
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.linearVelocity.y) < 0.1f)
        {
            jumpRequested = true;
        }

        // 3. Controle de Animação simples
        bool noChao = Mathf.Abs(rb.linearVelocity.y) < 0.1f;

        if (!noChao)
            TrocarAnimacao("Pulando");
        else
            TrocarAnimacao("Andando"); // Sempre corre no infinite runner
    }

    private void FixedUpdate()
    {
        // Calcula a posição Y alvo baseada na pista selecionada
        float targetY = (targetLane - 1) * laneDistance; // Ex: Pista 0 = -2.5, Pista 1 = 0, Pista 2 = 2.5

        // Interpola suavemente a posição vertical para a pista escolhida
        float newY = Mathf.Lerp(rb.position.y, targetY, laneChangeSpeed * Time.fixedDeltaTime);

        // Mantém a velocidade constante para a frente no eixo X e aplica o movimento da pista no eixo Y
        float currentYVelocity = rb.linearVelocity.y;

        if (jumpRequested)
        {
            currentYVelocity = jumpForce;
            jumpRequested = false;
        }

        rb.linearVelocity = new Vector2(forwardSpeed, jumpRequested ? currentYVelocity : rb.linearVelocity.y);

        // Força a posição exata da pista suavemente sem travar a física do pulo
        rb.MovePosition(new Vector2(rb.position.x + forwardSpeed * Time.fixedDeltaTime, newY));
    }

    private void TrocarAnimacao(string nome)
    {
        if (estadoAtual == nome) return;

        estadoAtual = nome;
        animator.Play(nome);
    }
}