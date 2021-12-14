namespace AdventOfCode2021
{
    public class DaySix : BaseDay
    {
        public override string ExecutePartOne(string file)
        {
            IEnumerable<string> input = ReadLines(nameof(DaySix), file);

            List<Fish> fishes = input
                .First()
                .Split(",")
                .Select(i => new Fish(int.Parse(i)))
                .ToList();

            for(int i = 0; i < 80; i++)
            {
                List<Fish> newFishes = new List<Fish>();

                foreach(var fish in fishes)
                {
                    if (fish.CanSpawnNewFish)
                    {
                        newFishes.Add(new Fish(8));
                    }

                    fish.PassDay();
                }

                fishes.AddRange(newFishes);
            }

            return fishes.Count.ToString();
        }

        public override string ExecutePartTwo(string file)
        {
            IEnumerable<string> input = ReadLines(nameof(DaySix), file);

            Dictionary<int, FishBag> fishesByTimer = input
               .First()
               .Split(",")
               .Select(i => int.Parse(i))
               .GroupBy(v => v)
               .Select(g => new KeyValuePair<int, FishBag>(g.Key, new FishBag(g.Key, g.Count())))
               .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            for (int i = 0; i < 256; i++)
            {
                List<FishBag> newFishes = new List<FishBag>();

                foreach (var fishBag in fishesByTimer)
                {
                    if (fishBag.Value.CanSpawnNewFish)
                    {
                        newFishes.Add(fishBag.Value.NewSpawnedFishes);
                    }

                    fishBag.Value.PassDay();
                }

                // Reorder bags
                fishesByTimer = fishesByTimer
                    .Values
                    .GroupBy(fb => fb.Timer)
                    .ToDictionary(f => f.Key, f => new FishBag(f.Key, f.ToList()));

                // Add new fishes
                foreach(var newFishBag in newFishes)
                {
                    if (!fishesByTimer.ContainsKey(newFishBag.Timer))
                    {
                        fishesByTimer.Add(newFishBag.Timer, newFishBag);
                    }
                    else
                    {
                        fishesByTimer[newFishBag.Timer].AddFishes(newFishBag);
                    }
                }
            }

            return fishesByTimer.Values.Sum(f => f.NumberOfFishes).ToString();
        }
    }

    public class Fish
    {
        private int timer;

        public Fish(int timer)
        {
            this.timer = timer;
        }

        public void PassDay()
        {
            this.timer--;

            if(timer < 0)
            {
                timer = 6;
            }
        }

        public bool CanSpawnNewFish => timer == 0;
    }

    public class FishBag
    {
        private int timer;
        private long numberOfFish;

        public FishBag(int timer, long numberOfFish)
        {
            this.timer = timer;
            this.numberOfFish = numberOfFish;
        }

        public FishBag(int timer, List<FishBag> bags)
        {
            this.timer = timer;
            this.numberOfFish = bags.Select(b => b.numberOfFish).Sum();
        }

        public void PassDay()
        {
            this.timer--;

            if (timer < 0)
            {
                timer = 6;
            }
        }

        public void AddFishes(FishBag newFishes)
        {
            this.numberOfFish += newFishes.numberOfFish;
        }

        public bool CanSpawnNewFish => timer == 0;
        public FishBag NewSpawnedFishes => new FishBag(8, numberOfFish);
        public int Timer => timer;
        public long NumberOfFishes => numberOfFish;
    }
}
