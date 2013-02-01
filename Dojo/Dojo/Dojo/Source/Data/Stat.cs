using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Source.Data
{
    class Stat
    {
        private const int MAX_LEVEL = 10;
        private const int MIN_LEVEL = 1;
        
        public float value { get; private set; }
        public float max { get; private set; }
        private float multiplier;
        public int level;

        public Stat(int initialLevel, float _multiplier)
        {
            multiplier = _multiplier;
            level = initialLevel;

            value = (level * multiplier);
#if DEBUG
            System.Console.WriteLine(value);
#endif
            max = (MAX_LEVEL * multiplier);
        }

        public void SetLevel(int level)
        {
            if (level > MAX_LEVEL)
            {
                level = MAX_LEVEL;
            }

            if (level < MIN_LEVEL)
            {
                level = MIN_LEVEL;
            }

            value = (level * multiplier);
        }

        public void Update(int modifier)
        {
            if ((level + modifier >= MIN_LEVEL) && (level + modifier <= MAX_LEVEL))
            {
                level += modifier;
            }
            else
            {
                if (level + modifier < MIN_LEVEL)
                {
                    level = MIN_LEVEL;
                }
                else if(level + modifier > MAX_LEVEL)
                {
                    level = MAX_LEVEL;
                }
            }

            value = (level * multiplier);
        }
    }
}
