using UnityEngine;

public class ManualConfigurationSwitch : MonoBehaviour
{
    private PanelChanger _panelChanger;

    private bool _manualChanged;
    private void Start()
    {
        _panelChanger = GetComponent<PanelChanger>();
    }

    private void Update()
    {
        if ( Input.GetButtonDown("Fire1"))
        {
            if (!_manualChanged)
            {
                _panelChanger.ManualChangeToGamePanel();
                _manualChanged = true;
            }
            else
            {
                _panelChanger.ManualChangeBackFromGamePanel();
                _manualChanged = false;
            }
            
        }
    }

    public void TurnOffManualGamePanel()
    {
        if (_manualChanged)

        {
            _panelChanger.ManualChangeBackFromGamePanel();
            _manualChanged = false;
        }
    }
}
