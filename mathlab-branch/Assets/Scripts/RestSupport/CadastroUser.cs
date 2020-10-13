using System;
using System.Collections.Generic;
using Proyecto26;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace RestSupport {
    public class CadastroUser : MonoBehaviour {
        
        public InputField username;
        private List<int> _listOfIdUsed = new List<int>();
        private Dictionary<string,string> fase1 = new Dictionary<string, string>();
        
        public void EnviarCadastro() {
            // ReSharper disable once HeapView.ClosureAllocation
            var insertId = " ";
            var usuarioAtual = new User(username.text, "teste", Random.Range(0,50), Random.Range(20,30), Random.Range(20,30));
     
            
            
            DatabaseHandler.GetUsers(users =>
            {
                foreach (var user in users) {
                    _listOfIdUsed.Add(int.Parse(user.Key));
                    Debug.Log($"{user.Value.name} {user.Value.surname} {user.Value.age} {user.Key}");
                }

                insertId = (_listOfIdUsed[_listOfIdUsed.Count - 1] + 1).ToString();
                Debug.Log(insertId);
                
                DatabaseHandler.PostUser(usuarioAtual, insertId, () => { Debug.Log("Inserted!"); });
                
                return 200;
            });
        }


        public void AttUser(string userName) {
            var usuarioAtual = new User("atual22", "teste", Random.Range(0,50), Random.Range(20,30), Random.Range(20,30));
            DatabaseHandler.AtualizaUser(usuarioAtual, userName, () => Debug.Log("all good!"));
        }
        
        

    }
}
