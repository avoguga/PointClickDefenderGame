using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject build_button;
    public GameObject return_button;
    public GameObject building_panel;
    public GameObject building_ui;

    public static BuildingManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OpenBuildingPanel() 
    {
        building_panel.SetActive(true);
    }
    
    public void CloseBuildingPanel() 
    {
        building_panel.SetActive(false);
    }
    
    public void SelectTower(GameObject tower_prefab) 
    {
        building_ui.SetActive(false);
        
        GameObject selected_tower = Instantiate(tower_prefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}