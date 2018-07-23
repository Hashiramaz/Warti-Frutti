using UnityEngine;


public class PlayerUI : MonoBehaviour {

   /* [SerializeField]
    RectTransform thrusterFuel;

    [SerializeField]
    GameObject pauseMenu;*/

    private PlayerControler controller;

    public void SetController(PlayerControler _controller)
    {
        controller = _controller;
    }

    private void Start()
    {
       // PauseMenu.IsOn = false;
    }

    void Update()
    {
       /*SetFuelAmount(controller.GetThrusterFuelAmount());
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }*/
    }

    void TogglePauseMenu()
    {/*
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        PauseMenu.IsOn = pauseMenu.activeSelf;
    }

    void SetFuelAmount(float _amount)
    {
        thrusterFuel.localScale = new Vector3(1f, _amount, 1f);
        */
    }
	
}
