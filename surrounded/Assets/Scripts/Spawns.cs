using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    public GameObject grunt;       // Assign Grunt ship prefab in the Inspector
    public GameObject viper;       // Assign Viper ship prefab in the Inspector
    public GameObject juggernaut;  // Assign Juggernaut ship prefab in the Inspector
    public GameObject striker;     // Assign Striker ship prefab in the Inspector
    public GameObject dreadnought;     // Assign Striker ship prefab in the Inspector
    public float spawnInterval = 5f;      // Time between spawns
    public Transform targetPoint;         // Target for ships to move towards (e.g., player ship)
    private Camera mainCamera;            // Reference to the main camera
    private int random, creditCost, availableCredits;
    public GameObject Timer;
    private RoundTimer round;

    void Start()
    {
        round = Timer.GetComponent<RoundTimer>();
        mainCamera = Camera.main;         // Get the main camera
        StartCoroutine(SpawnShips());
    }

    int availableCreditAmount(){
        return (int)(30*(Mathf.Pow(1.1f, (RoundNumber()-1))))/10;
    }

    IEnumerator SpawnShips()
    {
        yield return new WaitForSeconds(spawnInterval/2);
        while(true){
            availableCredits = availableCreditAmount();
            while (availableCredits >= 0)
            {
                GameObject selectedShip = GetShipBasedOnRound();  // Select the ship type
                if(creditCost > availableCredits){
                    selectedShip = null;
                }
                availableCredits -= creditCost;
                if (selectedShip != null)
                {
                    Vector2 spawnPosition = GetOffScreenPosition();  // Get off-screen spawn position
                    GameObject newShip = Instantiate(selectedShip, spawnPosition, Quaternion.identity);

                    // Move the ship towards the target point
                    //MoveShipToTarget(newShip);
                }
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Choose the appropriate ship prefab based on the current round
    GameObject GetShipBasedOnRound()
    {
        if (RoundNumber() >= 15)
            return ChooseRandomShip(viper, juggernaut, striker, grunt, dreadnought);  
        else if (RoundNumber() >= 10 && RoundNumber() < 15)
            return ChooseRandomShip(viper, grunt, juggernaut, striker); 
        else if (RoundNumber() >= 5 && RoundNumber() < 10)
            return ChooseRandomShip(grunt, viper, juggernaut);
        else
            return ChooseRandomShip(grunt, viper); 
    }

    //IMPLEMENTING CREDITS WITHIN THIS FUNCTION    MESSAGE TO ME (ANMOL)
    GameObject ChooseRandomShip(params GameObject[] ships)
    {
        random = Random.Range(0, ships.Length);
        if(ships[random] == grunt || ships[random] == viper){
            creditCost = 1;
        } else if(ships[random] == juggernaut){
            creditCost = 4;
        } else if(ships[random] == striker){
            creditCost = 7;
        } else{
            creditCost = 11;
        }
        return ships[random];
    }

    // Get a random position just outside the camera's view
    Vector2 GetOffScreenPosition()
    {
        Vector2 screenMin = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 screenMax = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

        float x, y;
        bool spawnOnX = Random.value > 0.5f;

        if (spawnOnX)
        {
            x = Random.value < 0.5f ? screenMin.x - 2 : screenMax.x + 2;
            y = Random.Range(screenMin.y, screenMax.y);
        }
        else
        {
            x = Random.Range(screenMin.x, screenMax.x);
            y = Random.value < 0.5f ? screenMin.y - 2 : screenMax.y + 2;
        }

        return new Vector2(x, y);
    }

    // Move the ship towards the target point (e.g., player ship)
    void MoveShipToTarget(GameObject ship)
    {
        Vector2 targetPosition = targetPoint.position;
        float speed = Random.Range(2f, 5f);  // Randomize ship speed
        Rigidbody2D rb = ship.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = (targetPosition - (Vector2)ship.transform.position).normalized * speed;
        }
    }

    // Increment the round number
    public int RoundNumber()
    {
        return round.round;
    }

}
