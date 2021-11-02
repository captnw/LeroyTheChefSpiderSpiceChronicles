using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    [SerializeField]
    public Transform m_spawnPosition;

    private GameObject m_player; 

    // Player lives stuff
    public int PlayerLives
    {
        get { return m_numLives; }
    }
    private int m_numLives = 3;

    private int m_numSpicesToCollect; // This will be calculated at run time
    private int m_numSpicesCollected = 0;

    private const string PLAYER_TAG = "Player";
    private const string SPICE_TAG = "Spice";

    private void Awake()
    {
        // Ensure that this class a singleton (you can invoke this within any script as long as GameManager exists on a GameObject anywhere in the scene)
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
        m_numSpicesToCollect = GameObject.FindGameObjectsWithTag(SPICE_TAG).Length;

        SpiceCounterDisplay.Instance.UpdateText(m_numSpicesCollected, m_numSpicesToCollect);
    }

    /// <summary>
    /// If lives are above 3, respawn, otherwise gameOver.
    /// </summary>
    public void CheckRespawn()
    {
        if ((m_numLives - 1) > 0)
        {
            LivesDisplay.Instance.PlayerDied();
            m_numLives--;
            m_player.transform.position = m_spawnPosition.position;
            m_player.SetActive(true);
        }
        else
        {
            Debug.Log("game over!!");
        }
    }

    public void CollectedSpice()
    {
        m_numSpicesCollected++;
        SpiceCounterDisplay.Instance.UpdateText(m_numSpicesCollected, m_numSpicesToCollect);
    }

    public bool HasCollectedAllSpices()
    {
        return m_numSpicesCollected == m_numSpicesToCollect;
    }
}