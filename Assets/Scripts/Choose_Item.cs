using UnityEngine;

public class Choose_Item : MonoBehaviour
{
    public GameObject[] Windows;
    public KeyCode createWindowKey = KeyCode.Space;
    public KeyCode[] objectKeys = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3 };
    public float WindowDistance = 1.1f;

    private GameObject[] instantiatedSprites;
    private bool objectsDestroyed = false;

    private bool hasRifle = false;
    private bool hasShotgun = false;
    private bool hasGrenade = false;

    void Update()
    {
        if (Input.GetKeyDown(createWindowKey))
        {
            CreateSprites();
            Time.timeScale = 0f;
            objectsDestroyed = false;
        }

        for (int i = 0; i < objectKeys.Length; i++)
        {
            if (Input.GetKeyDown(objectKeys[i]))
            {
                DestroySprites();
                Time.timeScale = 1f;
                if (objectsDestroyed && instantiatedSprites.Length > i)
                {
                    string name = instantiatedSprites[i].name;
                    switch (name)
                    {
                        case "Rifle_Window(Clone)":
                            if (!hasRifle)
                            {
                                AddScriptToObject("FirstScript");
                                hasRifle = true;
                            }
                            break;
                        case "Shotgun_Window(Clone)":
                            if (!hasShotgun)
                            {
                                AddScriptToObject("SecondScript");
                                hasShotgun = true;
                            }
                            break;
                        case "Grenade_Window(Clone)":
                            if (!hasGrenade)
                            {
                                AddScriptToObject("ThirdScript");
                                hasGrenade = true;
                            }
                            break;
                    }
                }
            }
        }
    }

    void CreateSprites()
    {
        instantiatedSprites = new GameObject[3];

        Shuffle(Windows);

        float spriteWidth = Windows[0].GetComponent<SpriteRenderer>().bounds.size.x;
        float startX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x + spriteWidth / 2;
        float spacing = spriteWidth * WindowDistance;

        for (int i = 0; i < 3; i++)
        {
            float posX = startX + i * spacing;
            Vector3 position = new Vector3(posX, 0, 0);
            instantiatedSprites[i] = Instantiate(Windows[i], position, Quaternion.identity);
        }

        Vector3 averagePosition = Vector3.zero;
        foreach (GameObject sprite in instantiatedSprites)
        {
            averagePosition += sprite.transform.position;
        }
        averagePosition /= instantiatedSprites.Length;

        Vector3 shift = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0)) - averagePosition;
        foreach (GameObject sprite in instantiatedSprites)
        {
            sprite.transform.position += shift;
            sprite.transform.parent = Camera.main.transform;
        }
    }

    void Shuffle(GameObject[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    void DestroySprites()
    {
        objectsDestroyed = false;
        if (instantiatedSprites != null)
        {
            foreach (GameObject sprite in instantiatedSprites)
            {
                Destroy(sprite);
                objectsDestroyed = true;
            }
        }
    }

    void AddScriptToObject(string scriptName)
    {
        GameObject selectedObject = GameObject.Find("Gun");
        if (selectedObject != null)
        {
            switch (scriptName)
            {
                case "FirstScript":
                    if (!hasRifle)
                    {
                        selectedObject.AddComponent<Rifle>();
                    }
                    break;
                case "SecondScript":
                    if (!hasShotgun)
                    {
                        selectedObject.AddComponent<Shotgun>();
                    }
                    break;
                case "ThirdScript":
                    if (!hasGrenade)
                    {
                        selectedObject.AddComponent<Grenade>();
                    }
                    break;
            }
        }
    }
}