using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace SpaceShooterLogic
{
    public class SoundEffectList : IEnumerable<SoundEffect>
    {
        private readonly List<SoundEffect> _soundEffectList;

        public SoundEffectList()
        {
            _soundEffectList = new List<SoundEffect>();
        }

        public void Add(SoundEffect soundEffect)
        {
            _soundEffectList.Add(soundEffect);
        }

        public IEnumerator<SoundEffect> GetEnumerator()
        {
            foreach (var item in _soundEffectList)
            {
                yield return item;
            }
        }

        public void Clear()
        {
            _soundEffectList.Clear();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}