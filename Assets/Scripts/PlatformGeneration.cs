using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformGeneration : MonoBehaviour
{
    private bool generated = false;
    static int platformCount;
    static int maxPlatformCount = 5;
    public bool moving;

    private GameObject platformPrefab, movingPlatformPrefab;
    public static List<GameObject> platforms;

    private void ResetGame(Scene scene, LoadSceneMode mode)
    {
        platformCount = 0;
    }

    private void Start()
    {
        platformPrefab = Resources.Load<GameObject>("Platform");
        movingPlatformPrefab = Resources.Load<GameObject>("MovingPlatform");
        SceneManager.sceneLoaded += ResetGame;

        if (platformCount != 0 && !moving)
            transform.parent.position = new Vector2(Random.Range(-1.8f, 1.8f), transform.parent.position.y);
        else if (moving)
            transform.parent.position = new Vector2(0, transform.parent.position.y);

        else
        {
            platforms = new List<GameObject>();
            platforms.Add(gameObject);
        }

        if (platformCount < maxPlatformCount)
        {
            BuildPlatform();
        }
    }

    private void BuildPlatform()
    {
        platformCount++;
        GameObject platPrefab;
        
        if(GameManager.instance.CurrentScore < 6)
        {
            platPrefab = platformPrefab;
        }
        else
        {
            var rng = Random.Range(0, 101);
            if (rng > 70)
            {
                platPrefab = movingPlatformPrefab;
            }
            else
                platPrefab = platformPrefab;
        }

        var platform = Instantiate(platPrefab, (Vector2)transform.parent.position - new Vector2(0, 5), Quaternion.identity);
        platforms.Add(platform);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            CreatePlatform();
    }

    public void CreatePlatform()
    {
        transform.GetChild(0).GetComponent<DeathTrigger>().passable = true;

        if (platforms.IndexOf(transform.parent.gameObject) == 3 && !generated)
        {
            generated = true;
            var rem = platforms[0];
            platforms.Remove(rem);
            Destroy(rem);

            platforms.ShiftLeft(1);
            platforms[4].transform.GetChild(0).GetComponent<PlatformGeneration>().BuildPlatform();
        }
    }
}

//list shifting src: https://stackoverflow.com/a/18181243
public static class ShiftList
{
    public static List<T> ShiftLeft<T>(this List<T> list, int shiftBy)
    {
        if (list.Count <= shiftBy)
        {
            return list;
        }

        var result = list.GetRange(shiftBy, list.Count - shiftBy);
        result.AddRange(list.GetRange(0, shiftBy));
        return result;
    }
}