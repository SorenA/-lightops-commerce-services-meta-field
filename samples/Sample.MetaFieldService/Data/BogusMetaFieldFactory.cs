using System;
using System.Collections.Generic;
using Bogus;
using LightOps.Commerce.Services.MetaField.Api.Models;

namespace Sample.MetaFieldService.Data
{
    public class BogusMetaFieldFactory
    {
        public int? Seed { get; set; }

        public IList<IMetaField> MetaFields { get; internal set; } = new List<IMetaField>();

        public void Generate()
        {
            if (Seed.HasValue)
            {
                Randomizer.Seed = new Random(Seed.Value);
            }

            // TODO: Generate data
        }
    }
}