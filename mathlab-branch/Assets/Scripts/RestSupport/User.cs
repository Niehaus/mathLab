using System;

namespace RestSupport {
    /// <summary>
    /// The user class, which gets uploaded to the Firebase Database
    /// </summary>
    
    [Serializable] // This makes the class able to be serialized into a JSON
    public class User
    {
        public string name;
        public string surname;
        public int age;
        public int fase2;
        public int fase3;

        public User(string name, string surname, int age, int fase2, int fase3)
        {
            this.name = name;
            this.surname = surname;
            this.age = age;
            this.fase2 = fase2;
            this.fase3 = fase3;
        }
    }
}