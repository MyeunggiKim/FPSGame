using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


//������ ����(Ready, Start, Game Over)�� �����ϰ�, ������ ���۰� ���� TestUI�� ǥ���Ѵ�.
//���ӻ��� ������ ����, TextUI

//Ready ���¿��� 2�� �� Start ���·� ����ȴ�.

//�÷��̾��� hp�� 0���� ������ ���� �ؽ�Ʈ�� Game Over �ٲٰ�,GameState�� Dead�� �����.

//�÷��̾��� hp�� 0���ϰ� �Ǹ� �÷��̾��� �ִϸ��̼��� �����.
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
