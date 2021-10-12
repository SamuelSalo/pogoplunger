using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformGeneration : MonoBehaviour
{
    private bool generated = false;
    static int platformCount;
    static int maxPlatformCount = 5;

    public GameObject platformPrefab;
    public static List<GameObject> platforms;

    private void Reset(Scene scene, LoadSceneMode mode)
    {
        platformCount = 0;
    }

    private void Start()
    {
        SceneManager.sceneLoaded += Reset;

        if (platformCount != 0)
            transform.position = new Vector2(Random.Range(-1.8f, 1.8f), transform.position.y);
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
        var platform = Instantiate(platformPrefab, (Vector2)transform.position + new Vector2(0, -5), Quaternion.identity);
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

        if (platforms.IndexOf(gameObject) == 3 && !generated)
        {
            generated = true;
            var rem = platforms[0];
            platforms.Remove(rem);
            Destroy(rem);

            platforms.ShiftLeft(1);
            platforms[4].GetComponent<PlatformGeneration>().BuildPlatform();
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