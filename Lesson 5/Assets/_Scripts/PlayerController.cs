using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region vars
    [Header("Player Params")]
    public float speed = 2.0f;
    public float jumpSpeed = 1f;
    public float gravity = 10f;

    [Header("UI")]
    public Text text;

    private int money = 0;
    private bool _isDamage = false;

    private CharacterController characterController;

    #endregion

    #region UnityMethods

    // Use this for initialization
    void Start()
    {
        text = GameObject.Find("Text").GetComponent<Text>();
        characterController = GetComponent<CharacterController>();
        money = PlayerPrefs.GetInt("coins");
        text.text = "Coins collected: " + money;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 move = Vector3.zero;

        if (Input.GetButton("Horizontal"))
        {
            move.z = -Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        }
        if (Input.GetButton("Jump"))
        {
            move.y = jumpSpeed * Time.deltaTime;
        }
        move.y -= gravity * Time.deltaTime;
        move.x = speed * Time.deltaTime;
        characterController.Move(move);

    }
    #endregion

    #region Trigers And Damage

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (_isDamage) SceneManager.LoadScene(0);
            else
            {
                Damage();
                _isDamage = true;
                collision.gameObject.GetComponent<PoolItem>().ReturnPool();
            }
        }

        else if (collision.gameObject.tag == "coin") { money++; text.text = "Coins collected: " + money; collision.gameObject.GetComponent<PoolItem>().ReturnPool();
        }
    }

    public void Damage()
    {
        speed /= 2;
        GameObject.Find("Directional Light").GetComponent<Light>().color = Color.red;
        StartCoroutine(UnDamager());
    }

    public void UnDamage()
    {
        speed *= 2;
        GameObject.Find("Directional Light").GetComponent<Light>().color = Color.white;
    }

    IEnumerator UnDamager()
    {
        yield return new WaitForSeconds(5f);
        UnDamage();
    }

    #endregion

    #region Exit

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("coins", money);
    }
    #endregion
}
