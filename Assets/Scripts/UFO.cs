using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UFO : MonoBehaviour
{
    public Rigidbody leftLegRB;
    public Rigidbody rightLegRB;
    public float forse = 2f;
    public float rotationMultiplier = 0.8f;
    public SceneLoader sceneLoader;
    public Slider leftSlider;
    public Slider rightSlider;
    public Slider heightSlider;
    public Text leftText;
    public Text rightText;
    public Text HeightText;

    private AudioSource[] _sounds; // Звуки на сцене
    private GameObject[] _objsUFO; // массив частей UFO

    private void Awake()
    {
        _objsUFO = GameObject.FindGameObjectsWithTag("objs");
        foreach (var i in _objsUFO)
        {
            i.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    void Start()
    {
        _sounds = GetComponents<AudioSource>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        leftSlider.maxValue = forse;
        rightSlider.maxValue = forse;
        heightSlider.maxValue = 40;
    }

    void Update()
    {
        Vector3 minForse = Vector3.up * forse * rotationMultiplier;
        Vector3 maxForse = Vector3.up * forse;

        Vector3 leftForse = Vector3.zero;
        Vector3 rightForse = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            leftForse = maxForse;
            rightForse = maxForse;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            leftForse = minForse;
            rightForse = maxForse;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            leftForse = maxForse;
            rightForse = minForse;
        }

        if (gameObject.transform.position.y >= 26)
        {
            leftForse = Vector3.zero;
            rightForse = Vector3.zero;
        }

        leftLegRB.AddRelativeForce(leftForse);
        rightLegRB.AddRelativeForce(rightForse);

        // Отображение силы двигателя на UI
        leftSlider.value = leftForse.y;
        rightSlider.value = rightForse.y;

        leftText.text = leftSlider.value + " Wt";
        rightText.text = rightSlider.value + " Wt";

        // Отображение высоты на UI
        heightSlider.value = gameObject.transform.position.y;


        // Звук двигаетля
        if (leftForse.y + rightForse.y > 0 && !_sounds[1].isPlaying)
        {
            _sounds[1].Play();
        }
        else if (leftForse.y + rightForse.y == 0)
        {
            _sounds[1].Pause();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            foreach (var i in _objsUFO)
            {
                Destroy(i.gameObject.GetComponent<FixedJoint>().connectedBody);
                i.gameObject.GetComponent<BoxCollider>().enabled = true;
            }
            StartCoroutine(Restart());
        }
        if (collision.gameObject.tag == "Finish")
        {
            sceneLoader.NextScene();
        }
        if (collision.gameObject.tag == "Obstacle") _sounds[0].Play();
        if (collision.gameObject.tag == "Finish") _sounds[2].Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            sceneLoader.RestartScene();
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(1.5f);
        sceneLoader.RestartScene();
    }
}
