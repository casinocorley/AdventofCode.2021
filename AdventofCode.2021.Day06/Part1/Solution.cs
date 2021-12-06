using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AdventofCode._2021.Day06.Part1
{
    [DebuggerDisplay("InternalTimer={InternalTimer}")]
    public class LanternFish
    {
        public int InternalTimer { get; set; }
        public bool HasGivenBirth { get; set; } = false;

        public LanternFish(int internalTimer)
        {
            InternalTimer = internalTimer;
        }

        public void PassingDay()
        {
            InternalTimer--;

            if (InternalTimer < 0)
            {
                InternalTimer = 6;
                HasGivenBirth = true;
            }
        }
    }

    public class LanternFishSchool : List<LanternFish>
    {
        public LanternFishSchool PassingDay()
        {
            foreach (var lanternFish in this)
            {
                lanternFish.PassingDay();
            }
            
            // Account for births
            var count = this
                .Count(x => x.HasGivenBirth);
            AddNewLanternFish(count);
            
            this.ForEach(x => x.HasGivenBirth = false);

            return this;
        }

        public virtual LanternFishSchool PassingDay(int daysPassed)
        {
            for(var i=0; i<daysPassed; i++)
                PassingDay();

            return this;
        }

        public virtual void AddNewLanternFish(int value)
        {
            for (var i = 0; i < value; i++)
            {
                this.Add(new LanternFish(8));
            }
        }
    }
}
