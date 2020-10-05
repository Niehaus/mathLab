using System.Collections.Generic;
using System.Linq;
using RestSupport;
using UnityEngine;

public class Main : MonoBehaviour {
    private static List<int> _listOfIdUsed = new List<int>();
    public static int lastId;
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void OnAppStart()
    {
        
        var user2 = new User("UsuarioInicial2", "Nao foi feita", Random.Range(0,60), Random.Range(20,30), Random.Range(20,30));
        DatabaseHandler.PostUser(user2, "2", () =>
        {
            DatabaseHandler.GetUser("2", user =>
            {
                Debug.Log($"{user.name} {user.surname} {user.age}");
            });
        });
        
      
    }
}