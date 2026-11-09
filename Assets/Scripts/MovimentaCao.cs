using UnityEngine;

public class MovimentacaoRunner : MonoBehaviour
{
<<<<<<< HEAD
    [Header("Configurações de Corrida")]
    [SerializeField] private float forwardSpeed = 7f;     
    [SerializeField] private float laneChangeSpeed = 15f; 
    [SerializeField] private float jumpForce = 12f;       

    [Header("Alturas das Pistas (Ajuste no Inspector)")]
    [SerializeField] private float alturaPistaBaixo = -2.5f; 
    [SerializeField] private float alturaPistaMeio = -0.5f;  
=======
    [Header("Configura��es de Corrida")]
    [SerializeField] private float forwardSpeed = 7f;     
    [SerializeField] private float laneDistance = 2.5f;   
    [SerializeField] private float laneChangeSpeed = 15f; 
    [SerializeField] private float jumpForce = 12f;       
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
        TrocarAnimacao("Andando"); // No runner, ele come�a correndo
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

        // 2. Solicitação de Pulo
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

        // 2. Solicita��o de Pulo (Barra de Espa�o)
        // Nota: Para runners simples, �s vezes removemos a checagem rigorosa de ch�o para permitir pulo duplo ou fluidez, 
        // mas mantemos uma toler�ncia b�sica se necess�rio.
>>>>>>> c097bbd91c46d8965fa10d939de6d5986c5d3e6b
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.linearVelocity.y) < 0.1f)
        {
            jumpRequested = true;
        }

<<<<<<< HEAD
        // 3. Controle de Animação
=======
        // 3. Controle de Anima��o simples
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
        // Seleciona a posição Y exata com base na pista (0 ou 1)
        float targetY = (targetLane == 0) ? alturaPistaBaixo : alturaPistaMeio;

        // Interpola suavemente a posição vertical
        float newY = Mathf.Lerp(rb.position.y, targetY, laneChangeSpeed * Time.fixedDeltaTime);

=======
        // Calcula a posi��o Y alvo baseada na pista selecionada
        float targetY = (targetLane - 1) * laneDistance; // Ex: Pista 0 = -2.5, Pista 1 = 0, Pista 2 = 2.5

        // Interpola suavemente a posi��o vertical para a pista escolhida
        float newY = Mathf.Lerp(rb.position.y, targetY, laneChangeSpeed * Time.fixedDeltaTime);

        // Mant�m a velocidade constante para a frente no eixo X e aplica o movimento da pista no eixo Y
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
        // For�a a posi��o exata da pista suavemente sem travar a f�sica do pulo
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