using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetpackScript : MonoBehaviour {

    public float maximumGas;
    private float currentGas;

    public GameObject remainingGasIndicator;
    public GameObject spentGasIndicator;

    public GameObject guiGasIndicator;

    private Text guiGasIndicatorText;
    private RectTransform guiGasIndicatorRect;

    public float timeToStartRefilling = 2.0f;

    public float gasSpentPerSecond = 4f;
    public float gasRefilledPerSecond = 6f;

    private bool refilling = true;

    private Vector3 movingVector = Vector3.zero;

    private CharacterController cc;
    private PlayerController pc;

    public float thrustSpeed = 2f;

    public GameObject[] emitters;

    // Use this for initialization
    void Start () {
        cc = gameObject.GetComponent<CharacterController>();
        pc = gameObject.GetComponent<PlayerController>();
        currentGas = maximumGas;
        guiGasIndicatorText = guiGasIndicator.transform.GetChild(2).GetComponent<Text>();
        guiGasIndicatorRect = guiGasIndicator.transform.GetChild(1).GetComponent<RectTransform>();
    }

    private Coroutine currentCoroutine;

	// Update is called once per frame
	void Update () {
        if(refilling)
        {
            currentGas += Time.deltaTime * gasRefilledPerSecond;
            currentGas = Mathf.Clamp(currentGas, 0, maximumGas);
            UpdateGas();
        }
        if(Input.GetButtonDown("Jump") && !cc.isGrounded)
        {
            foreach(GameObject emitter in emitters)
            {
                emitter.GetComponent<ParticleSystem>().Play();
            }
        }
        if (Input.GetButton("Jump") && !cc.isGrounded)
        {
            foreach (GameObject emitter in emitters)
            {
                ParticleSystem ps = emitter.GetComponent<ParticleSystem>();
                if(!ps.isPlaying) ps.Play();
            }

            refilling = false;
            if(currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            if (currentGas > 0)
            {
                pc.SetUsingJetpack(true);
                movingVector.y = Mathf.Clamp(movingVector.y + 0.1f,0,thrustSpeed);
                
                cc.Move(movingVector * Time.deltaTime);
                currentGas -= Time.deltaTime * gasSpentPerSecond;
                currentGas = Mathf.Clamp(currentGas, 0, maximumGas);
                
            } else
            {
                pc.SetUsingJetpack(false);
                currentCoroutine = StartCoroutine(RefillGas());
                movingVector.y = 0f;

                foreach (GameObject emitter in emitters)
                {
                    emitter.GetComponent<ParticleSystem>().Stop();
                }

            }
            UpdateGas();
        }
        if (Input.GetButtonUp("Jump"))
        {
            pc.SetUsingJetpack(false);
            currentCoroutine = StartCoroutine(RefillGas());
            movingVector.y = 0f;

            foreach (GameObject emitter in emitters)
            {
                emitter.GetComponent<ParticleSystem>().Stop();
            }
        }
    }

    void UpdateGas()
    {
        float gasRatio = currentGas / maximumGas;
        remainingGasIndicator.transform.localScale = new Vector3(1,1,Mathf.Lerp(0, 1, gasRatio));
        spentGasIndicator.transform.localScale = new Vector3(1, 1, Mathf.Lerp(1, 0, gasRatio));

        guiGasIndicatorText.text = Mathf.Round(currentGas) + "/" + Mathf.Round(maximumGas);
        guiGasIndicatorRect.localScale = new Vector3(gasRatio, 1f, 1f);

    }
    IEnumerator RefillGas()
    {
        yield return new WaitForSeconds(timeToStartRefilling);
        refilling = true;
        currentCoroutine = null;
    }

}
