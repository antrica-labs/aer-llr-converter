using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLRAlbertaConverter.Entities
{
    public class FacilityEntity : LLREntity
    {
        public string LiabilityManagementProgram { get; set; }
        public string AssetCalulationMethod { get; set; }

        public double AbandonmentFacilityAmount { get; set; }
        public bool AbandonmentFacilityAmountIncluded { get; set; }

        public double AbandonmentSiteSpecificAmount { get; set; }
        public bool AbandonmentSiteSpecificAmountIncluded { get; set; }

        public double ReclamationSiteReclamationAmount { get; set; }
        public bool ReclamationSiteReclamationAmountIncluded { get; set; }

        public double ReclamationSiteSpecificAmount { get; set; }
        public bool ReclamationSiteSpecificAmountIncluded { get; set; }


        public FacilityEntity()
        {
            Type = LLREntityType.Facility;
            AssetValue = 0;
            LiabilityAmount = 0;            
            AbandonmentFacilityAmount = 0;
            AbandonmentFacilityAmountIncluded = false;
            AbandonmentSiteSpecificAmount = 0;
            AbandonmentSiteSpecificAmountIncluded = false;
            ReclamationSiteReclamationAmount = 0;
            ReclamationSiteReclamationAmountIncluded = false;
            ReclamationSiteSpecificAmount = 0;
            ReclamationSiteSpecificAmountIncluded = false;
        }
    }
}
