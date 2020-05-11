using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRotS
{
    class SoundLib
    {
        public static Dictionary<string, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>();
        static int occurance = 0;
        private static SoundLib _instance;

        public static SoundLib Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SoundLib();
                }
                return _instance;
            }
        }

        public void Update()
        {
            //this will ensure their is no muddy soundeffects
            if (occurance != 0) occurance--;
        }

        public SoundLib()
        {
            soundEffects.Add("PlaneGun", MouseClass.Instance._content.Load<SoundEffect>("Sounds/Effects/planeGun"));
            soundEffects.Add("Exsplosion", MouseClass.Instance._content.Load<SoundEffect>("Exsplosion"));
            soundEffects.Add("AmmoCollect", MouseClass.Instance._content.Load<SoundEffect>("Sounds/Effects/AmmoCollect"));
            soundEffects.Add("Reflect", MouseClass.Instance._content.Load<SoundEffect>("Sounds/Effects/Reflect"));
            soundEffects.Add("Fart", MouseClass.Instance._content.Load<SoundEffect>("Sounds/Effects/Fart"));
        }

        public Dictionary<string, SoundEffect> GetLib()
        {
            return soundEffects;
        }

        public bool PlaySound(string soundName, float volume = 0.1f)
        {
            if (occurance != 3)
            {
                occurance++;
                 return soundEffects[soundName].Play(volume, 0, 0);
            }
            else return false;
        }
    }
}
