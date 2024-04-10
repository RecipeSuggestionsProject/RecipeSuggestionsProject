using AutoMapper;
using Microsoft.AspNetCore.Routing;
using NuGet.ContentModel;
using NUnit.Framework;
using RecipeSuggestions.Server.Mappings;
using RecipeSuggestions.Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSuggestions.Server.Mappings.UnitTests
{
    [TestFixture]
    public class AutomapperUnitTest
    {
        [Test]
        public void AutomapperConfigurationTest()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = new Mapper(mapperConfiguration);
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
            Assert.Pass();
        }
    }
}