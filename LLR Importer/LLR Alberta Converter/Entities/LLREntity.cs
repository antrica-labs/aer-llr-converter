using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLRAlbertaConverter.Entities
{
    public abstract class LLREntity
    {
        public LLREntityType Type { get; set; }
        public string LicenceNumber { get; set; }
        public string LicenceStatus { get; set; }
        public string LocationIdentifier { get; set; }
        public double AssetValue { get; set; }
        public double LiabilityAmount { get; set; }
        public string PVSValueType { get; set; }
        public string ActiveIndicator { get; set; }
        
    }

    public enum LLREntityType
    {
        Well,
        Facility
    }
}
