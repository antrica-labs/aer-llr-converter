using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLRAlbertaConverter.Entities
{
    public class WellEntity : LLREntity
    {
        public double AbandomnentWellBoreAmount { get; set; }
        public bool AbandomnentWellBoreAmountIncluded { get; set; }

        public double AbandomnentAdditionalEventAmount { get; set; }
        public bool AbandomnentAdditionalEventAmountIncluded { get; set; }

        public double AbandomnentGWPAdmount { get; set; }
        public bool AbandomnentGWPAdmountIncluded { get; set; }

        public double AbandomnentVentFlowAmount { get; set; }
        public bool AbandomnentVentFlowAmountIncluded { get; set; }

        public double AbandomnentGasMigrationAmount { get; set; }
        public bool AbandomnentGasMigrationAmountIncluded { get; set; }

        public double ReclamationSiteAmount { get; set; }
        public bool ReclamationSiteAmountIncluded { get; set; }

        public WellEntity()
        {
            Type = LLREntityType.Well;
            AssetValue = 0;
            LiabilityAmount = 0;            
            AbandomnentWellBoreAmount = 0;
            AbandomnentWellBoreAmountIncluded = false;
            AbandomnentAdditionalEventAmount = 0;
            AbandomnentAdditionalEventAmountIncluded = false;
            AbandomnentGWPAdmount = 0;
            AbandomnentGWPAdmountIncluded = false;
            AbandomnentVentFlowAmount = 0;
            AbandomnentVentFlowAmountIncluded = false;
            AbandomnentGasMigrationAmount = 0;
            AbandomnentGasMigrationAmountIncluded = false;
            ReclamationSiteAmount = 0;
            ReclamationSiteAmountIncluded = false;
        }

    }
}
