using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


//게임의 상태(Ready, Start, Game Over)를 구별하고, 게임의 시작과 끝을 TestUI로 표현한다.
//게임상태 열거형 변수, TextUI

//Ready 상태에서 2초 후 Start 상태로 변경된다.

//플레이어의 hp가 0보다 적으면 상태 텍스트를 Game Over 바꾸고,GameState를 Dead로 만든다.

//플레이어의 hp가 0이하가 되면 플레이어의 애니메이션을 멈춘다.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState
    {
        Ready,
        Start,
        GameOver
    }

    public GameState state = GameState.Ready;
    public TMP_Text stateText;

    PlayerMove player;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        stateText.text = "Ready";
        stateText.color = new Color32(255, 185, 0, 255);

        StartCoroutine(GameStart());

        player = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(2);

        stateText.text = "GameStart";
        stateText.color = new Color32(0, 225, 0, 225);

        yield return new WaitForSeconds(0.5f);

        stateText.gameObject.SetActive(false);

        state = GameState.Start;
    }

    void GameOver()
    {
        if (player.hp<0)
        {
            animator.SetFloat(mea)


            stateText.gameObject.SetActive(true);

            stateText.text = "GameOver";

            stateText.color = new Color32(255, 0, 0, 255);

            state = GameState.GameOver;
        }
    }


    // Update is called once per frame
    void Update()
    {
        GameOver();
    }
}
