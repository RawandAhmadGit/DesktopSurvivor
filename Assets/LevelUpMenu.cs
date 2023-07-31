using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton {
    private Image _icon;
    private string _description;
    private string _name;
    private bool _isWeapon;
    private weapontype _weapontype;
    private StatType _statType;
}

public class LevelUpMenu : MonoBehaviour
{
    private PlayerScript _thePlayer;
    private GameObject _levelUpSelection;
    // Start is called before the first frame update
    void Start()
    {
        _thePlayer = GameObject.FindAnyObjectByType<PlayerScript>();
        _thePlayer.LevelUpEvent.AddListener(OnLevelUp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnLevelUp() {
        Time.timeScale = 0;
        _init();
    }

    private void _init() {

        
    }
}
