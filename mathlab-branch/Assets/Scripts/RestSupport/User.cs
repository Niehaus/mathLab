using System;

namespace RestSupport {
    /// <summary>
    /// The user class, which gets uploaded to the Firebase Database
    /// </summary>
    
    [Serializable] // This makes the class able to be serialized into a JSON
    public class User
    {
        public string name;
        public string dataJogo;
        public int tempoFase1;
        public int fase2;
        public int fase3;

        public User(string name, string dataJogo, int tempoFase1, int fase2, int fase3)
        {
            this.name = name;
            this.dataJogo = dataJogo;
            this.tempoFase1 = tempoFase1;
            this.fase2 = fase2;
            this.fase3 = fase3;
        }
    }
}