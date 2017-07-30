using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] m_menus;
    [SerializeField]
    private int m_defaultMenu = 0;
    private int m_currentMenu;
    [SerializeField]
    private bool m_showMenus = true;

    [SerializeField]
    private NavigatableOption[] m_currentOptions;
    private int m_currentlySelectedOption = 0;
    [SerializeField]
    private float m_navigationDelay = 0.5f;
    private float m_navigationWaitTime = 0;

    [SerializeField]
    private int[] m_parentMenus;

    private MenuAudioManager m_menuAudio;

    private bool m_accessingDropDown = false;

    private bool m_isActive = true;
    public bool isActive { get { return m_isActive; } set { m_isActive = value; } }

    public GameObject m_ship;

    void Start()
    {
        m_currentMenu = m_defaultMenu;
        if(m_showMenus)
            ShowCurrentMenu();
        m_navigationWaitTime = m_navigationDelay;

        m_menuAudio = GetComponentInChildren<MenuAudioManager>();
    }

    void Update()
    {
        if (m_navigationWaitTime >= m_navigationDelay)
        {
            if (!m_isActive)
                return;

            for (int i = 1; i < 5; i++) {
                if (InputManager.Instance.GetAxis("LH_" + i) < 0) //(InputManager.Instance.GetButton("Left"))
                {
                    // Sliding audio slider to the left
                    m_currentOptions[m_currentlySelectedOption].Slide(-1);
                }
                else if (InputManager.Instance.GetAxis("LH_" + i) > 0) //(InputManager.Instance.GetButton("Right"))
                {
                    // Sliding audio slider to the right
                    m_currentOptions[m_currentlySelectedOption].Slide(1);
                }
                else if (InputManager.Instance.GetAxis("LV_" + i) < 0) //(InputManager.Instance.GetButtonDown("Up"))
                {
                    if (m_accessingDropDown)
                    {
                        // Moving up in the drop down menu
                        m_currentOptions[m_currentlySelectedOption].NavigateDropDown(-1);
                    }
                    else
                    {
                        // Move up in menu
                        m_currentlySelectedOption--;
                        if (m_currentlySelectedOption < 0)
                        {
                            m_currentlySelectedOption = m_currentOptions.Length - 1;
                        }
                        UpdateSelection();
                    }
                    m_navigationWaitTime = 0;
                }
                else if (InputManager.Instance.GetAxis("LV_" + i) > 0) //(InputManager.Instance.GetButtonDown("Down"))
                {
                    if (m_accessingDropDown)
                    {
                        // Move down in the dropdown menu
                        m_currentOptions[m_currentlySelectedOption].NavigateDropDown(1);
                    }
                    else
                    {
                        // Move down in menu
                        m_currentlySelectedOption++;
                        if (m_currentlySelectedOption >= m_currentOptions.Length)
                        {
                            m_currentlySelectedOption = 0;
                        }
                        UpdateSelection();
                    }
                    m_navigationWaitTime = 0;
                }
                else if (InputManager.Instance.GetButtonDown("A_" + i)) //(InputManager.Instance.GetButtonDown("A_1"))
                {
                    if (m_accessingDropDown)
                    {
                        // Select a dropdown option
                        m_currentOptions[m_currentlySelectedOption].Deactivate();
                        m_accessingDropDown = false;
                    }
                    else
                    {
                        // Select menu option
                        m_currentOptions[m_currentlySelectedOption].Activate();

                        if (m_currentOptions[m_currentlySelectedOption].UIType == UIType.DropDown)
                        {
                            m_accessingDropDown = true;
                        }
                    }
                    m_menuAudio.PlayAccept();
                    m_navigationWaitTime = 0;
                }
                else if (InputManager.Instance.GetButtonDown("B_" + i)) //(InputManager.Instance.GetButtonDown("B_1"))
                {
                    if (m_accessingDropDown)
                    {
                        // Cancel dropdown selection
                        m_currentOptions[m_currentlySelectedOption].Deactivate();
                        m_accessingDropDown = false;
                    }
                    else
                    {
                        //if (m_currentMenu == 1 || m_currentMenu == 2 || m_currentMenu == 3)
                        //{
                        //    Button_GoToMenu(m_defaultMenu);
                        //}
                        //else
                        //{
                            // Go back a menu
                            for (int j = m_parentMenus.Length - 1; j > -1; j--)
                            {
                                if (m_currentMenu > m_parentMenus[j])
                                {
                                    Button_GoToMenu(m_parentMenus[j]);
                                    break;
                                }
                            }
                        //}
                    }

                    m_menuAudio.PlayBack();
                    m_navigationWaitTime = 0;
                }
            }
        }
        else
        {
            m_navigationWaitTime += Time.deltaTime;
        }
    }

    public void Button_GoToMenu(int menuIndex)
    {
        m_currentMenu = menuIndex;
        ShowCurrentMenu();
    }

    public void Button_GoToScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void Button_GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Button_QuitGame()
    {
        Application.Quit();
    }

    public void ShowMenus(bool b)
    {
        m_showMenus = b;
        if(b == false)
        {
            m_menus[m_currentMenu].SetActive(false);
            m_currentMenu = m_defaultMenu;
        }
        ShowCurrentMenu();
    }

    private void ShowCurrentMenu()
    {
        for (int i = 0; i < m_menus.Length; i++)
        {
            if (i == m_currentMenu)
            {
                m_menus[i].SetActive(true);
            }
            else
            {
                m_menus[i].SetActive(false);
            }
        }

        m_currentOptions = m_menus[m_currentMenu].GetComponentsInChildren<NavigatableOption>();
        m_currentlySelectedOption = 0;
        UpdateSelection();

        if (m_currentMenu == 0)
            m_ship.SetActive(true);
        else
            m_ship.SetActive(false);
    }

    private void UpdateSelection()
    {
        for (int i = 0; i < m_currentOptions.Length; i++)
        {
            if (i == m_currentlySelectedOption)
            {
                m_currentOptions[i].IsSelected = true;
            }
            else
            {
                m_currentOptions[i].IsSelected = false;
            }
        }
    }
}
