using System;
using System.Collections.Generic;
using Proyecto26;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace RestSupport {
    public class CadastroUser : MonoBehaviour {
        
        public InputField username;
        private List<int> _listOfIdUsed = new List<int>();
        private Dictionary<string,string> fase1 = new Dictionary<string, string>();
        DateTime _localDate = DateTime.Now;
        
        public void EnviarCadastro() {
            // ReSharper disable once HeapView.ClosureAllocation
            var insertId = " ";
            ManagerGeral.tempoJogador = _localDate.ToString(CultureInfo.InvariantCulture);
            ManagerGeral.jogadorAtual = username.text;
            var usuarioAtual = new User(ManagerGeral.jogadorAtual, ManagerGeral.tempoJogador , 
                ManagerGeral.totalTempoFase1, ManagerGeral.totalPontosFase2,ManagerGeral.totalPontosFase3);
            Debug.Log("entrei aqui");
            
            DatabaseHandler.GetUsers(users =>
            {
                foreach (var user in users) {
                    _listOfIdUsed.Add(int.Parse(user.Key));
                    Debug.Log($"{user.Value.name} {user.Value.dataJogo} {user.Value.tempoFase1} {user.Key}");
                }

                insertId = (_listOfIdUsed[_listOfIdUsed.Count - 1] + 1).ToString();
                Debug.Log(insertId);
                
                DatabaseHandler.PostUser(usuarioAtual, insertId, () => { Debug.Log("Inserted!"); });
                
                return 200;
            });
            
            SceneManager.LoadScene("Jogo Principal");
        }


        public void AttUser(float tempoFase1, int pontosFase2, int pontosFase3) {
            var usuarioAtual = new User(ManagerGeral.jogadorAtual, ManagerGeral.tempoJogador, tempoFase1,pontosFase2,pontosFase3);
            DatabaseHandler.AtualizaUser(usuarioAtual, ManagerGeral.jogadorAtual, () => Debug.Log("all good!"));
        }

    }
}
