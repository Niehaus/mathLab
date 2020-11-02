using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using RestSupport;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Toggle = UnityEngine.UIElements.Toggle;

public class UiManager : MonoBehaviour  {
    // Start is called before the first frame update
    
    private TextAsset[] _textFiles;
    public ToggleGroup toggleGroupInicio, toggleGroupFinal;
    private List<Toggle> _toggles;
    public GameObject togglePrefab, togglePrefab2, finalizaEtapaBtn;
    public GameObject[] uiPanels, slideArea;
    private int _myIndex = 0;
    public TabVerdadeManager tabVerdadeManager;
    private Hashtable _hashtableFiles = new Hashtable();
    private List<Tuple<Hashtable, bool>> _tabelasFeitas = new List<Tuple<Hashtable, bool>>(); //TODO: haash ou string??
    private List<string> _filesSolved = new List<string>();
    private PlayerController _disablekey; //bloqueador de teclado enquanto NPC fala
    private GameObject _managerGeral;
    public CadastroUser managerHTTP;
    void Start() {
        _textFiles = Resources.LoadAll("TabVerdade", typeof(TextAsset)).Cast<TextAsset>().ToArray();
        GenerateToggles(_textFiles, toggleGroupInicio);
        _disablekey = FindObjectOfType<PlayerController>();
        _disablekey.keyboardAble = false;
        //TabelPanelFinaliza("nada");
        //togglePrefab.GetComponentInChildren<Text> ().text  = "o que??";
    }

    private void GenerateToggles(IEnumerable<TextAsset> textFiles, ToggleGroup toggleGroup) {
        foreach (var textFile in textFiles) {
            var newToggle = Instantiate (togglePrefab, new Vector3(transform.position.x  + 100f, 70f, transform.position.z) , Quaternion.identity);
            newToggle.GetComponentInChildren<Text>().text = textFile.name;
            _hashtableFiles.Add(textFile.name, _myIndex);
            newToggle.transform.SetParent(toggleGroup.transform);
            newToggle.transform.localPosition = new Vector3(-53.7f, 135f - ( 60f * _myIndex) , 0);
            _myIndex++;
        }
    }

    
    private void GenericToggleGenerator(GameObject parent, string labelName, Vector3 togglePosition) {
        var newToggle = Instantiate (togglePrefab2, new Vector3(transform.position.x , transform.position.y, transform.position.z) , Quaternion.identity);
        newToggle.GetComponentInChildren<Text>().text = labelName;
        newToggle.transform.SetParent(parent.transform);
        newToggle.transform.localPosition = togglePosition;
    }
    
    private void GenerateFinalToggles(IEnumerable<TextAsset> textAssets , IEnumerable<string> solvedFilesNames, ToggleGroup finalToggleGroup, GameObject[] slideArea) {
        var toggleindexArea1 = 0;
        var toggleindexArea2 = 0;
        foreach (var textAsset in textAssets) {
            if (solvedFilesNames.Contains(textAsset.name)) {
                //Arquivo ja resolvido
                GenericToggleGenerator(slideArea[0], textAsset.name, new Vector3(-40f, 135f - (60f * toggleindexArea1) , 0));
                toggleindexArea1++;
            }
            else {
                //arquivo ainda não resolvido 
                GenericToggleGenerator(slideArea[1], textAsset.name, new Vector3(-40f, 135f - (60f * toggleindexArea2) , 0));
                toggleindexArea2++;
            }
        }
    }

    private void DestroyAllChild(Transform parentTransform) {
        foreach (Transform child in parentTransform) {
            Destroy(child.gameObject);
        }
    }
    public void TabelPanelFinaliza(string currentFile, bool alreadyEnd) {
        if (!alreadyEnd) return;
        _filesSolved.Add(currentFile);
        if (_filesSolved.Count == _textFiles.Length) {
            Debug.Log("all files solved");
            finalizaEtapaBtn.SetActive(true);
        }
        _disablekey.keyboardAble = false;
        uiPanels[3].SetActive(true);
        GenerateFinalToggles(_textFiles, _filesSolved, toggleGroupFinal, slideArea);
    }

    public void FinalizaEtapa() {
        ManagerGeral.faseFeita[0] = true;
        // ManagerGeral.totalTempoFase1
        managerHTTP.AttUser(ManagerGeral.totalTempoFase1, ManagerGeral.totalPontosFase2, ManagerGeral.totalPontosFase3);
        /*foreach (var fase in ManagerGeral.faseFeita) {
            Debug.Log("fase feita:" + fase);
        }*/
        
    }
    
    public void Jogar(ToggleGroup currentToggleGroup) {
        var toggleActive = currentToggleGroup.ActiveToggles();
        if (!toggleActive.Any()) { //Quando nenhum arquivo de jogo é selecionado
            //TODO: CHAAMR POPUP??
            return;
        }
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
        uiPanels[3].SetActive(false);
        tabVerdadeManager.alreadyEnd = false;
        tabVerdadeManager.logLinhas.text = " ";
        _disablekey.keyboardAble = true; //ativa teclado
        DestroyAllChild(slideArea[0].transform);
        DestroyAllChild(slideArea[1].transform);
    }
}
