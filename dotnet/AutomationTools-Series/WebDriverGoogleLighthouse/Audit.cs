using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebDriverGoogleLighthouse
{
    public class Audit
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public double Score { get; set; }

        public string ScoreDisplayMode { get; set; }

        public double NumericValue { get; set; }

        public string NumericUnit { get; set; }

        public string DisplayValue { get; set; }
    }
}
