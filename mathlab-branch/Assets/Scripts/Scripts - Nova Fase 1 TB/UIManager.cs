using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Toggle = UnityEngine.UIElements.Toggle;

public class UiManager : MonoBehaviour  {
    // Start is called before the first frame update
    
    private TextAsset[] _textFiles;
    public ToggleGroup toggleGroup;
    private List<Toggle> _toggles;
    public GameObject togglePrefab;
    public GameObject[] uiPanels;
    private int _myIndex = 0;
    private float baseY = 68f;
    public TabVerdadeManager tabVerdadeManager;
    private Hashtable _hashtableFiles = new Hashtable();
    private List<Tuple<Hashtable, bool>> _tabelasFeitas = new List<Tuple<Hashtable, bool>>(); //TODO: haash ou string??
    
    void Start() {
        _textFiles = Resources.LoadAll("TabVerdade", typeof(TextAsset)).Cast<TextAsset>().ToArray();
        GenerateToggles(_textFiles);
        
        
        //togglePrefab.GetComponentInChildren<Text> ().text  = "o que??";
        
    }

    private void GenerateToggles(IEnumerable<TextAsset> textFiles) {
        foreach (var textFile in textFiles) {
            var newToggle = Instantiate (togglePrefab, new Vector3(transform.position.x  + 100f, 70f, transform.position.z) , Quaternion.identity);
            newToggle.GetComponentInChildren<Text>().text = textFile.name;
            _hashtableFiles.Add( textFile.name, _myIndex);
            //_toggles.Add(newToggle.GetComponent<Toggle>());
            newToggle.transform.SetParent(toggleGroup.transform);
            var position = newToggle.transform.parent.position;
            //  newToggle.transform.position = TogglePosition(_myIndex, position);
            newToggle.transform.position =  new Vector3( position.x - (HorizontalPosition(_myIndex, 82f)),  position.y + 68f - ( 41f * _myIndex - 2),  position.z);
            _myIndex++;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 TogglePosition(int index, Transform parentPosition) {
        if (index % 2 == 0) { //numero par
            return new Vector3 (parentPosition.position.x + 82f, parentPosition.position.y + baseY, parentPosition.position.z);
        }
        if (index > 2) {
            baseY = 68f - (41f * index - 2);
            return new Vector3 (parentPosition.position.x - 82f, parentPosition.position.y + baseY, parentPosition.position.z);   
        }
        return new Vector3 (parentPosition.position.x - 82f, parentPosition.position.y + baseY, parentPosition.position.z);
    }
    private float HorizontalPosition(int index, float position) {
        if (index % 2 == 0) { //numero par
            return position;
        }
        return -position;
    }

    public void TabelPanelFinaliza() {
        uiPanels[3].SetActive(true);
        GenerateToggles(_textFiles);
    }
    public void Jogar() {
        var toggleActive = toggleGroup.ActiveToggles();
        foreach (var toggle in toggleActive) {
            foreach (var textFile in _textFiles) {
                if (toggle.GetComponentInChildren<Text>().text == textFile.name) {
                    tabVerdadeManager.IniciaEtapa(_textFiles[(int) _hashtableFiles[textFile.name]]);
                }
            }
        }
        /*Ativar paineis de jogo*/
        uiPanels[0].SetActive(false);
        uiPanels[1].SetActive(true);
        uiPanels[2].SetActive(true);
    }
}
