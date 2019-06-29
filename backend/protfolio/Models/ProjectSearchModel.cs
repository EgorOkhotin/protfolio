using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace protfolio.Models
{
    public class ProjectSearchModel
    {
        public int[] SphereIds { get; set; }
        public string Name { get; set; }
        public string SphereSpecializtionPairs { get; set; }
        public string[] Tags { get; set; }

        public bool IsEmpty()
        {
            return SphereIds == null &&
                Name == null &&
                SphereSpecializtionPairs == null &&
                Tags == null;
        }

        public KeyValuePair<string, string>[] GetSpecialization()
        {
            if (SphereSpecializtionPairs == null) return null;

            var tmp = SphereSpecializtionPairs.Split(',');
            var result = new List<KeyValuePair<string, string>>(tmp.Length);
            foreach(var t in tmp)
            {
                var subArr = t.Split(':');
                result.Add(
                    new KeyValuePair<string, string>(subArr[0], subArr[1])
                    );
            }
            return result.ToArray();
        }
    }
}
