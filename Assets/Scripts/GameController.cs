using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerColor
{
    public Color panelColor;
    public Color textColor;
}

[System.Serializable]
public class Player
{
    public Image panel;
    public Text text;
    public Button button;
}

public class GameController : MonoBehaviour {

    public Text[] buttonList;
    public Text gameOverText;
    public GameObject gameOverPanel;
    public GameObject restartButton;
    public GameObject startInfo;

    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;

    private string playerSide;
    private int moveCount;
    

    private void Awake()
    {
        
        restartButton.SetActive(false);
        moveCount = 0;
        gameOverPanel.SetActive(false);
        SetGameControllerReferenceOnButtons();
    }
    void SetGameControllerReferenceOnButtons ()
    {
        for (int i = 0; i< buttonList.Length;i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    void StartGame()
    {
        SetBoardInteractable(true);
        SetPlayerButtons(false);
        startInfo.SetActive(false);
    }

    public void SetStartingSide(string startingSide)
    {
        playerSide = startingSide;
        if (playerSide =="X")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }
        StartGame();
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void EndTurn ()
    {
        moveCount++;
        for (int i=0;i<=6;i+=3)
        {
            if (buttonList[i].text == playerSide && buttonList[i+1].text == playerSide && buttonList[i+2].text == playerSide)
            {
                GameOver(playerSide);
            }
        }

        for (int i = 0; i <=2; i++)
        {
            if (buttonList[i].text == playerSide && buttonList[i + 3].text == playerSide && buttonList[i + 6].text == playerSide)
            {
                GameOver(playerSide);
            }
        }

        if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }

        if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver(playerSide);
        }
        

        ChangeSides();
        if (moveCount>=9)
        {
            GameOver("draw");
        }
        
    }

    void GameOver(string winningPlayer)
    {
        SetBoardInteractable(false);
        if (winningPlayer == "draw")
        {
            SetGameOverText("REMIS!");
            SetPlayerColorsInactive();
        }
        else
        {
            SetGameOverText(winningPlayer + " Wygrywa!");
        }
        restartButton.SetActive(true);
    }

    void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
        if (playerSide == "X")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }
    }

    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    public void RestartGame()
    {
        
        restartButton.SetActive(false);
        moveCount = 0;
        gameOverPanel.SetActive(false);

        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }
        SetPlayerButtons(true);
        SetPlayerColorsInactive();
        startInfo.SetActive(true);
    }

    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    void SetPlayerColors (Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }

    void SetPlayerButtons (bool toggle)
    {
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;
    }
    
    void SetPlayerColorsInactive()
    {
        playerX.panel.color = inactivePlayerColor.panelColor;
        playerX.text.color = inactivePlayerColor.textColor;
        playerO.panel.color = inactivePlayerColor.panelColor;
        playerO.text.color = inactivePlayerColor.textColor;
    }
}
