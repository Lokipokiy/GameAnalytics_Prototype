using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialTextScript : MonoBehaviour
{
    public TMP_Text tutorialText;
    public GameObject train;
    public GameObject asteroid;
    public GameObject bullet;
    string[] tutorialCaptions = {
        "Hello, and welcome to BIOHOPE!", //0
        "Looks like you need to learn",
        "how to move your ship!",
        "This is simple. Just use W, A, S, and D!",
        "Great! Now for your weapons system.",
        "For this, you just need to press SPACE to shoot.",//5
        "Perfect. Now for a test.",
        "This is one of BioHope's deadly bioweapon carriers.",
        "Shoot it to blow it up.",
        "You are almost ready.",
        "Trains turn into debris when destroyed.", //10
        "Debris and asteroids can be destroyed, too.",
        "There's an extra debris.",
        "Shoot it.",
        "You are ready.",
        "Don't let the train hit you, it is dangerous.",//15
        "Beware of it's erratic movement.", 
        "It will dodge asteroids and debris to reach you qucker.", 
        "Prepare to save countless lives, and countless ecosystems.",
        "(Click the menu button in the bottom left to exit tutorial)",//19
    };
    int captionValue = 1;
    bool wKey = false;
    bool aKey = false;
    bool sKey = false;
    bool dKey = false;
    bool learnedMove = false;
    bool learnedShoot = false;
    bool learnedTest = false;
    bool trainSpawned = false;
    bool learnedDebris = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tutorialText.text = tutorialCaptions[0];
        StartCoroutine(UpdateText(captionValue));
    }

    // Update is called once per frame
    void Update()
    {
        if(captionValue == 4 && !learnedMove)
        {
            StopAllCoroutines();
            TutorialMove();
        }
        if(captionValue == 6 && !learnedShoot)
        {
            StopAllCoroutines();
            TutorialShoot();
        }
        if(captionValue == 9 && !learnedTest)
        {
            StopAllCoroutines();
            TutorialTest();
        }
        if(captionValue == 14 && !learnedDebris)
        {
            StopAllCoroutines();
            TutorialDebris();
        }
        if(captionValue == 20)
        {
            StopAllCoroutines();
        }
    }
    private void TutorialMove()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            wKey = true;
            Debug.Log("W key");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            aKey = true;
            Debug.Log("A key");
        }
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            sKey = true;
            Debug.Log("S key");
        }
        if (Input.GetKeyDown(KeyCode.D)) 
        { 
            dKey = true;
            Debug.Log("D key");
        }
        if(wKey && aKey && sKey && dKey)
        {
            learnedMove = true;
            StartCoroutine(UpdateText(captionValue));
            Debug.Log("Tutorial Move Complete");
        }
    }

    private void TutorialShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            learnedShoot = true;
            Debug.Log("Space key");
            StartCoroutine(UpdateText(captionValue));
        }
        
    }
    private void TutorialTest()
    {
        Vector3 trainPos = new Vector3(1, 1, 0);
        Quaternion trainRot = new Quaternion(0, 0, 0, 0);
        if (!trainSpawned)
        {
            var st = Instantiate(train, trainPos, trainRot);
            st.transform.SetParent(this.gameObject.transform);
            trainSpawned = true;
        }
        if (this.gameObject.transform.childCount <= 0 && trainSpawned)
        {
            learnedTest = true;
            Debug.Log("Killed Train");
            var ia = Instantiate(asteroid, trainPos, trainRot);
            ia.transform.SetParent(this.gameObject.transform); 
            StartCoroutine(UpdateText(captionValue));
        }
        
    }
    private void TutorialDebris()
    {
        if(this.gameObject.transform.childCount <= 0)
        {
            learnedDebris = true;
            Debug.Log("Destroyed Debris");
            StartCoroutine(UpdateText(captionValue));
        }
    }

IEnumerator UpdateText(int value)
    {
        yield return new WaitForSeconds(3);
        tutorialText.text = tutorialCaptions[value];
        captionValue = captionValue + 1;
        StartCoroutine(UpdateText(captionValue));
    }
}
