using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RestSupport {
    public class CadastroUser : MonoBehaviour {
        
        public InputField username;
        private List<int> _listOfIdUsed = new List<int>();
        private Dictionary<string,string> fase1 = new Dictionary<string, string>();
        // Start is called before the first frame update
        void Start()
        {
               
        }

        // Update is called once per frame
        void Update() {
            
        }

        public void EnviarCadastro() {
            // ReSharper disable once HeapView.ClosureAllocation
            int insertId;
            var usuarioAtual = new User(username.text, "teste", Random.Range(0,50));
     
            DatabaseHandler.GetUsers(users =>
            {
                foreach (var user in users) {
                    _listOfIdUsed.Add(int.Parse(user.Key));
                    Debug.Log($"{user.Value.name} {user.Value.surname} {user.Value.age} {user.Key}");
                }

                insertId = _listOfIdUsed[_listOfIdUsed.Count - 1] + 1;
                
                DatabaseHandler.PostUser(usuarioAtual, insertId.ToString(), () => { Debug.Log("Inserted!"); });
                
                return 200;
            });
            
        }


        public void AttUser(string userName) {
            var usuarioAtual = new User("atual22", "teste", Random.Range(0,50));
            /*string attUserKey = "";
            DatabaseHandler.GetUsers(users => {
                foreach (var user in users) {
                    if (userName != null && userName.Equals(user.Value.name) ) {
                        attUserKey = user.Key;
                        Debug.Log($"this user key is {user.Key}");    
                    }
                }
                
                DatabaseHandler.PostUser(usuarioAtual, attUserKey , () => { Debug.Log("att done!"); });

                return 200;
            });*/
            DatabaseHandler.AtualizaUser(usuarioAtual, userName, () => Debug.Log("all good!"));

        }
        
        

    }
}
