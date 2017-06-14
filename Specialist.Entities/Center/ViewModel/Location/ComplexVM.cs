using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Common.Logic;
using Specialist.Entities.Context;
using SimpleUtils.Common.Extensions;
using System.Linq;

namespace Specialist.Entities.Center.ViewModel
{
    public class ComplexVM: IViewModel
    {
        public Complex Complex { get; set; }

    	public TextWithInfoTags Description { get {
    		return new TextWithInfoTags(Complex.Description, "[AdditionalInfo]");
			} }


        public NearestGroupSet NearestGroupSet { get; set; }

    	public List<Complex> OtherComplexes { get; set; } 


        public string Title
        {
            get { return "Учебный комплекс «" + Complex.Name + "»"; }
        }

    	public string GeoLocation { get; set; }

    	public List<Response> Responses { get; set; }
    }
}