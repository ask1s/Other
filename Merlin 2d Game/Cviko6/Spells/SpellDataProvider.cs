using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace Cviko6.Spells
{
    public class SpellDataProvider : ISpellDataProvider
    {
        private static SpellDataProvider instance = null;

        private Dictionary<string, int> spellEffects;
        private Dictionary<string, SpellInfo> spellInfo;

        private SpellDataProvider()
        {

        }

        public static SpellDataProvider GetInstance()
        {
            if (instance == null)
            {
                instance = new SpellDataProvider();
            }
            return instance;
        }

        public Dictionary<string, int> GetSpellEffects()
        {
            if (spellEffects == null)
            {
                spellEffects = LoadSpellEffects();
            }
            return spellEffects;
        }

        public Dictionary<string, SpellInfo> GetSpellInfo()
        {
            if (spellInfo == null)
            {
                spellInfo = LoadSpellInfo();
            }
            return spellInfo;
        }

        private Dictionary<string, SpellInfo> LoadSpellInfo()
        {
            List<string> lines = File.ReadAllLines("resources/spells.csv").Skip(1).ToList();
            Dictionary<string, SpellInfo> dictionary = new Dictionary<string, SpellInfo>();

            foreach (string line in lines)
            {
                try
                {
                    SpellInfo info = line;
                    dictionary.Add(info.Name, info);

                }
                catch (ArgumentException a)
                {

                }
            }
            return dictionary;
        }

        private Dictionary<string, int> LoadSpellEffects()
        {
            string json = File.ReadAllText("resources/effects.json");
            List<SpellEffect> effects = JsonConvert.DeserializeObject<List<SpellEffect>>(json);
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            foreach (SpellEffect line in effects)
            {
                try
                {
                    SpellEffect se = line;
                    dictionary.Add(se.Name, se.Cost);

                }
                catch (ArgumentException a)
                {

                }
            }
            return dictionary;
        }
        private class SpellEffect
        {
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("cost")]
            public int Cost { get; set; }
        }

    }
}
