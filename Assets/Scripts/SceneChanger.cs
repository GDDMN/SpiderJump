using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public void Scene1()
    {
        SceneManager.LoadScene("ShopScene");
    }
}
