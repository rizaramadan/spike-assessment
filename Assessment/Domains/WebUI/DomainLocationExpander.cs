using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Domains.WebUI
{
    public class DomainLocationExpander : IViewLocationExpander
    {

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {

            //replace the Views to MyViews..  
            viewLocations = viewLocations.Select(s => s.Replace("Views", "Domains"));

            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            //nothing to do here.  
        }
    }
}
