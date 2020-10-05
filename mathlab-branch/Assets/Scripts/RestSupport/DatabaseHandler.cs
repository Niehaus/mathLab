using System.Collections.Generic;
using FullSerializer;
using Proyecto26;
using RestSupport;
using UnityEngine;

public static class DatabaseHandler {
    
    private const string projectId = "mathlab-dd587"; // You can find this in your Firebase project settings
    private static readonly string databaseURL = $"https://{projectId}.firebaseio.com/";
    
   private static fsSerializer serializer = new fsSerializer();

    public delegate void PostUserCallback();
    public delegate void GetUserCallback(User user);
    public delegate int GetUsersCallback(Dictionary<string, User> users);
    
    
    /// <summary>
    /// Adds a user to the Firebase Database
    /// </summary>
    /// <param name="user"> User object that will be uploaded </param>
    /// <param name="userId"> Id of the user that will be uploaded </param>
    /// <param name="callback"> What to do after the user is uploaded successfully </param>
    public static void PostUser(User user, string userId, PostUserCallback callback)
    {
        RestClient.Put<User>($"{databaseURL}users/{userId}.json", user).Then(response => { callback(); });
    }
    

    /// <summary>
    /// Retrieves a user from the Firebase Database, given their id
    /// </summary>
    /// <param name="userId"> Id of the user that we are looking for </param>
    /// <param name="callback"> What to do after the user is downloaded successfully </param>
    public static void GetUser(string userId, GetUserCallback callback)
    {
        RestClient.Get<User>($"{databaseURL}users/{userId}.json").Then(user => { callback(user); });
    }

    public static void PutUser(User newUser, string userId, PostUserCallback callback) {
        RestClient.Put<User>($"{databaseURL}users" + $"/{userId}.json", newUser).Then(response => { callback(); });     
    }

    public static void AtualizaUser(User newUser, string userName, PostUserCallback callback) {
        string attUserKey = "";
        GetUsers(users => {
            foreach (var user in users) {
               
                if (userName != null && userName.Equals(user.Value.name) ) {
                    attUserKey = user.Key;
                    Debug.Log($"this user key is {user.Key}");    
                }
            }
            PostUser(newUser, attUserKey , () => { Debug.Log("att done!"); });
            return 200;
        });
    }
    /// <summary>
    /// Gets all users from the Firebase Database
    /// </summary>
    /// <param name="callback"> What to do after all users are downloaded successfully </param>
    public static void GetUsers(GetUsersCallback callback)
    {
        RestClient.Get($"{databaseURL}users.json").Then(response =>
        {
            var responseJson = response.Text;

            // Using the FullSerializer library: https://github.com/jacobdufault/fullserializer
            // to serialize more complex types (a Dictionary, in this case)
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, User>), ref deserialized);

            var users = deserialized as Dictionary<string, User>;
            callback(users);
        });
    }
}