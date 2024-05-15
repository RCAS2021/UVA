using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public enum GameState // enum -> limita as possibilidades, muito útil utilizar switch-case
    {
        Gameplay, // Jogando
        Paused, // Pausado
        GameOver, // Fim de jogo
        LevelUp // Subindo de nível
    }

    public GameState currentState; // Estado atual

    public GameState previousState; // Estado anterior

    [Header("Damage Text Settings")] // Configurações dos textos de dano
    public Canvas damageTextCanvas; // Canvas dos textos de dano
    public float textFontSize = 20; // Tamanho da fonte do texto (alterável no inspetor)
    public TMP_FontAsset textFont; // Fonte do texto
    public Camera referenceCamera; // Referência da câmera

    [Header("Screens")] // Telas
    public GameObject pauseScreen; // Tela de pause
    public GameObject resultsScreen; // Tela de resultados
    public GameObject levelUpScreen; // Tela de subida de nível

    [Header("Current Stat Displays")] // Estatísticas atuais
    public TMP_Text currentHealthDisplay; // Vida
    public TMP_Text currentRecoveryDisplay; // Regeneração de vida
    public TMP_Text currentMoveSpeedDisplay; // Velocidade de movimento
    public TMP_Text currentMightDisplay; // Força
    public TMP_Text currentProjectileSpeedDisplay; // Velocidade de projétil
    public TMP_Text currentMagnetDisplay; // Imã de coleta

    [Header("Results Screen Displays")] // Informações da tela de resultado
    public Image chosenCharacterImage; // Imagem do personagem escolhido
    public TMP_Text chosenCharacterName; // Nome do personagem escolhido
    public TMP_Text levelReachedDisplay; // Nível que chegou
    public TMP_Text timeSurvivedDisplay; // Tempo que sobreviveu
    public List<Image> chosenWeaponsUI = new List<Image>(6); // Lista de imagens das armas escolhidas
    public List<Image> chosenPassiveItemsUI = new List<Image>(6); // Lista de imagens dos itens passivos escolhidos

    [Header("Stopwatch")] // Relógio
    public float timeLimit; // Limite de tempo
    float stopwatchTime; // Tempo atual
    public TMP_Text stopwatchDisplay; // Texto com o tempo percorrido

    public bool isGameOver = false; // Booleano para checar se chegou ao fim do jogo
    public bool isChoosingUpgrade; // Booleano para checar se está escolhendo um upgrade
    public GameObject playerObject;

    void Awake() // Inicializa antes do start
    {
        if(instance == null) // Singleton
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("EXTRA" + this + " DELETED");
            Destroy(gameObject); // Se já houver uma instância, destrói
        }

        DisableScreens(); // Desabilita as telas
    }
    void Update()
    {
        // Switch-case para os estados do jogo
        switch(currentState)
        {
            case GameState.Gameplay:
                CheckForPauseAndResume(); // Checa se o jogo está em estado de pause e retorna, senão pausa o jogo
                UpdateStopwatch(); // Atualiza o relógio
                break;

            case GameState.Paused:
                CheckForPauseAndResume(); // Checa se o jogo está em estado de pause e retorna, senão, pausa o jogo
                break;

            case GameState.GameOver:
                if(!isGameOver) // Checa se o booleano de fim de jogo está falso
                {
                    isGameOver = true; // Altera o booleano para verdadeiro
                    Time.timeScale = 0f; // Pausa o jogo
                    DisplayResults(); // Mostra a tela de resultado
                }
                break;
            
            case GameState.LevelUp:
                if(!isChoosingUpgrade) // Checa se o booleano de escolha de upgrade está falso
                {
                    isChoosingUpgrade = true; // Altera o booleano para verdadeiro
                    Time.timeScale = 0f; // Pausa o jogo
                    levelUpScreen.SetActive(true); // Mostra a tela de subida de nível
                }
                break;
            
            default:
                Debug.LogWarning("STATE DOES NOT EXIST");
                break;
        }
    }

    IEnumerator GenerateFloatingTextCoroutine(string text, Transform target, float duration = 1f, float speed = 50f) // Gerar texto de dano
    {
        //Objetos, componentes e dados
        GameObject textObj = new GameObject("Damage Floating Text"); // Cria o objeto de jogo do texto de dano
        RectTransform rect = textObj.AddComponent<RectTransform>(); // Adiciona o component RectTransform ao objeto
        TextMeshProUGUI tmPro = textObj.AddComponent<TextMeshProUGUI>(); // Adiciona o TextMeshPro ao objeto
        tmPro.text = text;                                              // Texto do TMP
        tmPro.horizontalAlignment = HorizontalAlignmentOptions.Center; // Alinhamento horizontal do TMP (Centralizado)
        tmPro.verticalAlignment = VerticalAlignmentOptions.Middle; // Alinhamento vertical do TMP (Meio)
        tmPro.fontSize = textFontSize;                            // Tamanho da fonte do TMP
        rect.position = referenceCamera.WorldToScreenPoint(target.position); // Altera a posição em relação à câmera baseado na posição do alvo (inimigos)

        if(textFont)
        {
            tmPro.font = textFont; // Fonte do TMP
        }

        textObj.transform.SetParent(instance.damageTextCanvas.transform); // Coloca o objeto de jogo do texto de dano como filho do Canvas
        Destroy(textObj, duration); // Destrói o objeto de jogo após a duração

        WaitForEndOfFrame wait = new WaitForEndOfFrame(); // Espera até o fim do frame
        float time = 0;                                   // Inicializa o tempo
        float yOffset = 0;                                // Inicializa o desvio
        while (time < duration) // Verifica se já chegou à duração
        {
            yield return wait; // Pausa e retorna no próximo frame
            time += Time.deltaTime; // Aumenta o tempo

            tmPro.color = new Color(tmPro.color.r, tmPro.color.g, tmPro.color.b, 1 - time / duration); // Cor do TMP

            yOffset += speed * Time.deltaTime; // Aumenta o desvio
            if(rect != null && target != null) // Teste para remover avisos da unity
            {
                rect.position = referenceCamera.WorldToScreenPoint(target.position + new Vector3(0, yOffset)); // // Altera a posição em relação à câmera baseado na posição do alvo (inimigos) + vetor com o desvio
            }
        }
    }

    public static void GenerateFloatingText(string text, Transform target, float duration = 1f, float speed = 1f) // Gera o texto de dano
    {
        if(!instance.damageTextCanvas)
        {
            return;
        }

        if(!instance.referenceCamera)
        {
            instance.referenceCamera = Camera.main;
        }

        instance.StartCoroutine(instance.GenerateFloatingTextCoroutine(text, target, duration, speed)); // Inicializa a corotina GenerateFloatingTextCoroutine
    }

    public void ChangeState(GameState newState) // Altera o estado
    {
        currentState = newState; 
    }

    public void PauseGame() // Pausa o jogo
    {
        if(currentState != GameState.Paused) // Checa se o jogo não está pausado
        {
            previousState = currentState; // Salve o estado atual na variável estado anterior
            ChangeState(GameState.Paused); // Altera o estado para pausado
            Time.timeScale = 0f;          // Pausa o jogo
            pauseScreen.SetActive(true); // Mostra a tela de pause
            Debug.Log("Game is paused");
        }
        
    }

    public void ResumeGame() // Retorna ao jogo
    {
        if(currentState == GameState.Paused) // Checa se o jogo está pausado
        {
                ChangeState(previousState); // Retorna ao estado anterior
                Time.timeScale = 1f;        // Retorna o decorrer do tempo ao normal
                pauseScreen.SetActive(false); // Desativa a tela de pause
                Debug.Log("Game is resumed");
        }
    }

    void CheckForPauseAndResume() // Checa se, ao clicar Esc, o jogo já está pausado, se sim, retorna ao jogo, se não, pausa o jogo
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(currentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void DisableScreens() // Desabilita as telas
    {
        pauseScreen.SetActive(false);
        resultsScreen.SetActive(false);
        levelUpScreen.SetActive(false);
    }

    public void GameOver() // Método de fim de jogo
    {
        timeSurvivedDisplay.text = stopwatchDisplay.text; // Passa o valor do relógio para o texto de tempo sobrevivido.
        ChangeState(GameState.GameOver); // Altera o estado para fim de jogo
    }

    void DisplayResults() // Método para mostrar a tela de resultado
    {
        resultsScreen.SetActive(true);
    }

    public void AssignChosenCharacterUI(CharacterScriptableObject chosenCharacterData) // Dados de UI do personagem escolhido
    {
        chosenCharacterImage.sprite = chosenCharacterData.Icon;
        chosenCharacterName.text = chosenCharacterData.Name;
    }

    public void AssignLevelReachedUI(int levelReachedData) // Dados de UI do nível que o jogador chegou
    {
        levelReachedDisplay.text = levelReachedData.ToString();
    }

    public void AssignChosenWeaponAndPassiveItemsUI(List<Image> chosenWeaponsData, List<Image> chosenPassiveItemsData) // Método para mostrar as armas e itens passivos que foram selecionados
    {
        if(chosenWeaponsData.Count != chosenWeaponsUI.Count || chosenPassiveItemsData.Count != chosenPassiveItemsUI.Count)
        {
            return;
        }
        for (int i = 0; i < chosenWeaponsUI.Count; i++) // Percorre a lista
        {
            if(chosenWeaponsData[i].sprite) // Checa se existe a arma no índice da lista
            {
                chosenWeaponsUI[i].enabled = true;
                chosenWeaponsUI[i].sprite = chosenWeaponsData[i].sprite;
            }
            else
            {
                chosenWeaponsUI[i].enabled = false; 
            }

            if(chosenPassiveItemsData[i].sprite) // Checa se existe o item passivo no índice da lista
            {
                chosenPassiveItemsUI[i].enabled = true;
                chosenPassiveItemsUI[i].sprite = chosenPassiveItemsData[i].sprite;
            }
            else
            {
                chosenPassiveItemsUI[i].enabled = false;
            }
        }
    }

    void UpdateStopwatch() // Atualiza o relógio
    {
        stopwatchTime += Time.deltaTime; // Incrementa o relógio

        UpdateStopwatchDisplay(); // Atualiza os dados do relógio (texto)

        if(stopwatchTime >= timeLimit) // Checa se o relógio chegou ou passou o tempo limite, se sim, chama o método Kill(), finalizando o jogo
        {
            playerObject.SendMessage("Kill");
        }
    }

    void UpdateStopwatchDisplay() // Atualiza os dados do relógio (texto)
    {
        int minutes = Mathf.FloorToInt(stopwatchTime / 60); // Ex: 100/60 = 1 
        int seconds = Mathf.FloorToInt(stopwatchTime % 60); // Ex: 100 % 60 = 40 

        stopwatchDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartLevelUp() // Inicializa o estado de subida de nível
    {
        ChangeState(GameState.LevelUp);
        playerObject.SendMessage("RemoveAndApplyUpgrades"); // Chama o método RemoveAndApplyUpgrades()
    }

    public void EndLevelUp() // Finaliza o estado de subida de nível
    {
        isChoosingUpgrade = false; // Atualiza o booleano
        Time.timeScale = 1f; // Retorna o tempo ao normal
        levelUpScreen.SetActive(false); // Fecha a tela de subida de nível
        ChangeState(GameState.Gameplay); // Altera o estado para jogando
    }
}