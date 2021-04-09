using Boerse;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace BoersenGeschaeft
{
    public static class BoersenGeschaeftUtil
    {
        public const string ResourceName = "BoersenGeschaeft.boersen_geschaeft_db.json";

        private const double CoordFactor = 1e7;

        public static bool Exists(this BoerseResponse boerse)
        {
            return boerse != null && (boerse.TradeNummer.Length != 0);
        }

        public static List<BoerseResponse> Load()
        {
            var boersen = new List<BoerseResponse>();
            var jsonBoersen = JsonConvert.DeserializeObject<List<JsonBoerse>>(ReadFromResource());

            foreach(var jsonBoerse in jsonBoersen)
            {
        boersen.Add(new BoerseResponse
        {
            Bezeichnung = jsonBoerse.bezeichnung,
            TradeNummer = jsonBoerse.tradenummer,
            Wert = jsonBoerse.wert,
            Time = jsonBoerse.time
        });
            }
            return boersen;
        }


        private static string ReadFromResource()
        {
            var stream = typeof(BoersenGeschaeftUtil).GetTypeInfo().Assembly.GetManifestResourceStream(ResourceName);
            if (stream == null)
            {
                throw new IOException(string.Format("Error beim Laden der Ressource \"{0}\"", ResourceName));
            }
            using (var streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();
            }
        }

        private class JsonBoerse
        {
            public string bezeichnung = "";
            public string tradenummer = "";
            public int wert = 0 ;
            public string time = "";
    }

    }
}
