# Projeto-Felpudo-s-Race

4º Projeto Parcial da matéria Programação para Jogos, para o curso de Jogos Digitais da Universidade do Estado da Bahia, desenvolvidos por Jonatas Argolo e Isabela Coelho.

PLAY UNITY :
[https://play.unity.com/en/games/085913b3-bfcb-4ec7-95ee-5d89916e5334/felpudorace](https://play.unity.com/api/v1/games/game/085913b3-bfcb-4ec7-95ee-5d89916e5334/build/latest/frame)

### RESUMO:
  Nosso projeto foi desenvolvido na Unity 6 utilizando C#. O Felpudo's Race consiste em um infinite run 2D Plataforma com coletáveis e inimigos para desviar no caminho, o objetivo é coletar o máximo de frutinha possíveis enquanto desvia dos Lesmos que vão aparecendo pelo mapa, além disso, o jogador possui 3 vidas que o possibilitam de continuar jogando enquanto seu contador não chega a 0.

# Principais Scripts
## paralaxScript
  Para criar o efeito de paralaxe (parallax) — onde o fundo do cenário se move em uma velocidade diferente da câmera para dar uma sensação realista de profundidade e movimento em jogos 2D —, o script utiliza cálculos baseados na posição da câmera e no tamanho do sprite.

O script conta com 4 variáveis principais para gerenciar o efeito e a posição:
```csharp
private float length, startpos;
public GameObject cam; 
public float parallaxEffect;
```
No método Start(), a posição inicial (startpos) é capturada com base no eixo X do objeto, e o tamanho do sprite (length) é obtido dinamicamente usando o componente SpriteRenderer (GetComponent<SpriteRenderer>().bounds.size.x). Isso garante que o script saiba exatamente o tamanho do tile de fundo, independentemente da escala da imagem. No método Update(), a lógica de movimento e repetição é executada a cada quadro. A variável dist calcula o deslocamento do fundo proporcional ao movimento da câmera multiplicado pelo fator parallaxEffect. Em seguida, a posição do objeto é atualizada mantendo o eixo Y e Z originais (transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);).

Por fim, a estrutura condicional (if / else if) monitora a variável temp em relação aos limites do sprite. Se a câmera se desloca além do comprimento do fundo (startpos + length ou startpos - length), a posição inicial (startpos) é deslocada em um comprimento inteiro. Isso cria um loop infinito perfeito e fluido, ideal para jogos de rolagem lateral (side-scrollers).

```csharp
void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float dist = cam.transform.position.x * parallaxEffect;
        float temp = cam.transform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + length)
            startpos += length;
        else if (temp < startpos - length)
            startpos -= length;
    }
```

## MovimentaCao
A movimentação fluida de um personagem em um jogo estilo Infinite Runner (onde o jogador corre automaticamente para a frente, pode trocar de pistas verticais e saltar) é recriada quando o script utiliza uma combinação de física com Rigidbody2D, interpolação suave (Mathf.Lerp) e detecção de entrada por teclado.

O script conta com variáveis essenciais organizadas em blocos configuráveis pelo Inspector:
- Velocidades e Forças: forwardSpeed (velocidade constante de avanço), laneChangeSpeed (velocidade de transição entre as pistas) e jumpForce (intensidade do impulso vertical).
- Gerenciamento de Pistas: targetLane (armazena a pista atual em que o personagem está) e opções de posicionamento (laneDistance ou alturas customizadas).
- Componentes e Estados: Referências ao Rigidbody2D, Animator, SpriteRenderer e variáveis de controle como jumpRequested e estadoAtual.

No método Update(), a cada quadro são capturados os comandos do jogador:
- Troca de Pistas: As teclas de Seta para Cima/Baixo ou W/S alteram a variável targetLane, permitindo que o personagem suba ou desça entre as faixas do cenário.
- Solicitação de Pulo: Quando a tecla Espaço é pressionada (e o personagem está no chão, validado pela velocidade linear em y), a bandeira jumpRequested é ativada.
- Controle de Animação: O método auxiliar TrocarAnimacao() verifica se o personagem está no chão ("Correndo" ou "Andando") ou no ar ("Pulando"), atualizando o Animator sem reiniciar o estado atual desnecessariamente.
```csharp
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
```

Já no método FixedUpdate(), a física do jogo é processada de forma estável:
- A posição vertical alvo (targetY) é calculada com base na pista selecionada.
- A função Mathf.Lerp é utilizada para fazer uma transição suave (smooth damping) da posição atual do eixo Y até a altura exata da pista, evitando teletransportes bruscos.
- Caso um pulo tenha sido solicitado, a velocidade vertical recebe o impulso de jumpForce.
```csharp
private void FixedUpdate()
    {
        // Calcula a posição Y alvo baseada na pista selecionada
        float targetY = (targetLane - 1) * laneDistance; 

        // Interpola suavemente a posição vertical para a pista escolhida
        float newY = Mathf.Lerp(rb.position.y, targetY, laneChangeSpeed * Time.fixedDeltaTime);

        float currentYVelocity = rb.linearVelocity.y;

        if (jumpRequested)
        {
            currentYVelocity = jumpForce;
            jumpRequested = false;
        }

        rb.linearVelocity = new Vector2(forwardSpeed, jumpRequested ? currentYVelocity : rb.linearVelocity.y);

        // Move o personagem no eixo X mantendo a física e ajusta o Y suavemente
        rb.MovePosition(new Vector2(rb.position.x + forwardSpeed * Time.fixedDeltaTime, newY));
    }
```
