using UnityEngine;

public class MovimentacaoRunner : MonoBehaviour
{
<<<<<<< HEAD
    [Header("ConfiguraÃ§Ãµes de Corrida")]
    [SerializeField] private float forwardSpeed = 7f;     // Velocidade automÃ¡tica para a frente
    [SerializeField] private float laneChangeSpeed = 15f; // Velocidade de transiÃ§Ã£o entre as pistas
    [SerializeField] private float jumpForce = 12f;       // ForÃ§a do pulo

    [Header("Alturas das Pistas (Ajuste no Inspector)")]
    [SerializeField] private float alturaPistaBaixo = -2.5f; // PosiÃ§Ã£o Y exata da pista inferior
    [SerializeField] private float alturaPistaMeio = -0.5f;  // PosiÃ§Ã£o Y exata da pista superior/meio
=======
    [Header("Configurações de Corrida")]
    [SerializeField] private float forwardSpeed = 7f;     // Velocidade automática para a frente
    [SerializeField] private float laneDistance = 2.5f;   // Distância entre as pistas (Y ou X dependendo da visão, aqui usaremos o eixo Y para pistas verticais ou X para horizontais)
    [SerializeField] private float laneChangeSpeed = 15f; // Velocidade de transição entre as pistas
    [SerializeField] private float jumpForce = 12f;       // Força do pulo
>>>>>>> c097bbd91c46d8965fa10d939de6d5986c5d3e6b

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

<<<<<<< HEAD
    private int targetLane = 0; // 0 = Pista de Baixo, 1 = Pista do Meio
=======
    private int targetLane = 1; // 0 = Pista de Baixo, 1 = Pista do Meio, 2 = Pista de Cima
>>>>>>> c097bbd91c46d8965fa10d939de6d5986c5d3e6b
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
<<<<<<< HEAD
        TrocarAnimacao("Correndo");
=======
        TrocarAnimacao("Andando"); // No runner, ele começa correndo
>>>>>>> c097bbd91c46d8965fa10d939de6d5986c5d3e6b
    }

    private void Update()
    {
<<<<<<< HEAD
        // 1. Troca entre apenas 2 pistas (Setas / W e S)
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            targetLane = 1; // Vai para a pista do meio
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            targetLane = 0; // Volta para a pista de baixo
        }

        // 2. SolicitaÃ§Ã£o de Pulo
=======
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
>>>>>>> c097bbd91c46d8965fa10d939de6d5986c5d3e6b
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.linearVelocity.y) < 0.1f)
        {
            jumpRequested = true;
        }

<<<<<<< HEAD
        // 3. Controle de AnimaÃ§Ã£o
=======
        // 3. Controle de Animação simples
>>>>>>> c097bbd91c46d8965fa10d939de6d5986c5d3e6b
        bool noChao = Mathf.Abs(rb.linearVelocity.y) < 0.1f;

        if (!noChao)
            TrocarAnimacao("Pulando");
        else
<<<<<<< HEAD
            TrocarAnimacao("Correndo");
=======
            TrocarAnimacao("Andando"); // Sempre corre no infinite runner
>>>>>>> c097bbd91c46d8965fa10d939de6d5986c5d3e6b
    }

    private void FixedUpdate()
    {
<<<<<<< HEAD
        // Seleciona a posiÃ§Ã£o Y exata com base na pista (0 ou 1)
        float targetY = (targetLane == 0) ? alturaPistaBaixo : alturaPistaMeio;

        // Interpola suavemente a posiÃ§Ã£o vertical
        float newY = Mathf.Lerp(rb.position.y, targetY, laneChangeSpeed * Time.fixedDeltaTime);

=======
        // Calcula a posição Y alvo baseada na pista selecionada
        float targetY = (targetLane - 1) * laneDistance; // Ex: Pista 0 = -2.5, Pista 1 = 0, Pista 2 = 2.5

        // Interpola suavemente a posição vertical para a pista escolhida
        float newY = Mathf.Lerp(rb.position.y, targetY, laneChangeSpeed * Time.fixedDeltaTime);

        // Mantém a velocidade constante para a frente no eixo X e aplica o movimento da pista no eixo Y
>>>>>>> c097bbd91c46d8965fa10d939de6d5986c5d3e6b
        float currentYVelocity = rb.linearVelocity.y;

        if (jumpRequested)
        {
            currentYVelocity = jumpForce;
            jumpRequested = false;
        }

        rb.linearVelocity = new Vector2(forwardSpeed, jumpRequested ? currentYVelocity : rb.linearVelocity.y);

<<<<<<< HEAD
        // Move o personagem no eixo X e ajusta o Y para a pista correta
=======
        // Força a posição exata da pista suavemente sem travar a física do pulo
>>>>>>> c097bbd91c46d8965fa10d939de6d5986c5d3e6b
        rb.MovePosition(new Vector2(rb.position.x + forwardSpeed * Time.fixedDeltaTime, newY));
    }

    private void TrocarAnimacao(string nome)
    {
        if (estadoAtual == nome) return;

        estadoAtual = nome;
        animator.Play(nome);
    }
}