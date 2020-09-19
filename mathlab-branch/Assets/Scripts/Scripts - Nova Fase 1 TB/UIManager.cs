using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
   
    public TabVerdadeManager tabVerdadeManager;
    void Start() {
        _textFiles = Resources.LoadAll("TabVerdade", typeof(TextAsset)).Cast<TextAsset>().ToArray();
        foreach (var textFile in _textFiles) {
            var newToggle = Instantiate (togglePrefab, new Vector3(transform.position.x  + 74.3f,transform.position.y, transform.position.z) , Quaternion.identity);
            newToggle.GetComponentInChildren<Text>().text = textFile.name;
            //_toggles.Add(newToggle.GetComponent<Toggle>());    
            newToggle.transform.SetParent(toggleGroup.transform);
        }
        
        
        //togglePrefab.GetComponentInChildren<Text> ().text  = "o que??";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jogar() {
        var toggleActive = toggleGroup.ActiveToggles();
        foreach (var toggle in toggleActive) {
            Debug.Log(toggle.name);
        }
        //   tabVerdadeManager.IniciaEtapa(_textFiles[0]);
    }
}
