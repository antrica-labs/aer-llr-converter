using LLRAlbertaConverter.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLRAlbertaConverter
{
    public class AERConverter
    {
        private static int COL_COUNT = 18;

        private static int RECORD_TYPE = 0;
        private static int LICENCE = 1;
        private static int LICENCE_STATUS = 2;
        private static int LOCATION = 3;
        private static int ASSET_VALUE = 4;
        private static int LIABILITY_AMOUNT = 5;
        private static int PVS_VALUE_TYPE = 6;
        private static int ACTIVE_INDICATOR = 7;
        private static int W_ABAND_WELL_BORE_AMOUNT = 8;
        private static int W_ABAND_ADDITIONAL_EVENT_AMOUNT = 9;
        private static int W_ABAND_GWP_AMOUNT = 10;
        private static int W_ABAND_VENT_FLOW_AMOUNT = 11;
        private static int W_ABAND_GAS_MIGRATION_AMOUNT = 12;
        private static int W_REC_SITE_AMOUNT = 13;
        private static int F_ABAND_FACILITY_AMOUNT = 14;
        private static int F_ABAND_SITE_SPECIFIC_AMOUNT = 15;
        private static int F_REC_SITE_AMOUNT = 16;
        private static int F_REC_SITE_SPECIFIC_AMOUNT = 17;
        

        public string ConvertDocumentToCSV(Stream document)
        {
            return DoCSVConvert(document);
        }        

        private string DoCSVConvert(Stream document)
        {
            List<string[]> records = new List<string[]>();
            var reader = new StreamReader(document);

            string line;
            WellEntity[] wells = null;
            FacilityEntity[] facilities = null;

            // get all of the wells and facilities
            while ((line = reader.ReadLine()) != null)
            {
                if (line == "Well Licence Data")
                    wells = ParseWellSection(reader);
                else if (line == "Facility Licence Data")
                    facilities = ParseFacilitySection(reader);
            }

            records.Add(GetOutputHeader());
            
            if (wells != null)
            {
                foreach (var well in wells)
                {
                    var record = new string[COL_COUNT];

                    record[RECORD_TYPE] = Enum.GetName(typeof(LLREntityType), well.Type);
                    record[LICENCE] = well.LicenceNumber;
                    record[LICENCE_STATUS] = well.LicenceStatus;
                    record[LOCATION] = well.LocationIdentifier;
                    record[ASSET_VALUE] = well.AssetValue.ToString();
                    record[LIABILITY_AMOUNT] = well.LiabilityAmount.ToString();
                    record[PVS_VALUE_TYPE] = well.PVSValueType;
                    record[ACTIVE_INDICATOR] = well.ActiveIndicator;
                    record[W_ABAND_WELL_BORE_AMOUNT] = well.AbandomnentWellBoreAmountIncluded ? well.AbandomnentWellBoreAmount.ToString() : "0.0";
                    record[W_ABAND_ADDITIONAL_EVENT_AMOUNT] = well.AbandomnentAdditionalEventAmountIncluded ? well.AbandomnentAdditionalEventAmount.ToString() : "0.0";
                    record[W_ABAND_GWP_AMOUNT] = well.AbandomnentGWPAdmountIncluded ? well.AbandomnentGWPAdmount.ToString() : "0.0";
                    record[W_ABAND_VENT_FLOW_AMOUNT] = well.AbandomnentVentFlowAmountIncluded ? well.AbandomnentVentFlowAmount.ToString() : "0.0";
                    record[W_ABAND_GAS_MIGRATION_AMOUNT] = well.AbandomnentGasMigrationAmountIncluded ? well.AbandomnentGasMigrationAmount.ToString() : "0.0";
                    record[W_REC_SITE_AMOUNT] = well.ReclamationSiteAmountIncluded ? well.ReclamationSiteAmount.ToString() : "0.0";
                    record[F_ABAND_FACILITY_AMOUNT] = "0.0";
                    record[F_ABAND_SITE_SPECIFIC_AMOUNT] = "0.0";
                    record[F_REC_SITE_AMOUNT] = "0.0";
                    record[F_REC_SITE_SPECIFIC_AMOUNT] = "0.0";

                    records.Add(record);
                }
            }


            if (facilities != null)
            {
                foreach (var facility in facilities)
                {
                    var record = new string[COL_COUNT];

                    record[RECORD_TYPE] = Enum.GetName(typeof(LLREntityType), facility.Type);
                    record[LICENCE] = facility.LicenceNumber;
                    record[LICENCE_STATUS] = facility.LicenceStatus;
                    record[LOCATION] = facility.LocationIdentifier;
                    record[ASSET_VALUE] = facility.AssetValue.ToString();
                    record[LIABILITY_AMOUNT] = facility.LiabilityAmount.ToString();
                    record[PVS_VALUE_TYPE] = facility.PVSValueType;
                    record[ACTIVE_INDICATOR] = facility.ActiveIndicator;
                    record[W_ABAND_WELL_BORE_AMOUNT] = "0.0";
                    record[W_ABAND_ADDITIONAL_EVENT_AMOUNT] = "0.0";
                    record[W_ABAND_GWP_AMOUNT] = "0.0";
                    record[W_ABAND_VENT_FLOW_AMOUNT] = "0.0";
                    record[W_ABAND_GAS_MIGRATION_AMOUNT] = "0.0";
                    record[W_REC_SITE_AMOUNT] = "0.0";
                    record[F_ABAND_FACILITY_AMOUNT] = facility.AbandonmentFacilityAmountIncluded ? facility.AbandonmentFacilityAmount.ToString() : "0.0";
                    record[F_ABAND_SITE_SPECIFIC_AMOUNT] = facility.AbandonmentSiteSpecificAmountIncluded ? facility.AbandonmentSiteSpecificAmount.ToString() : "0.0";
                    record[F_REC_SITE_AMOUNT] = facility.ReclamationSiteReclamationAmountIncluded ? facility.ReclamationSiteReclamationAmount.ToString() : "0.0";
                    record[F_REC_SITE_SPECIFIC_AMOUNT] = facility.ReclamationSiteSpecificAmountIncluded ? facility.ReclamationSiteSpecificAmount.ToString() : "0.0";

                    records.Add(record);
                }
            }

            return ConvertRecordsToCSV(records);
        }   
     
        private string[] GetOutputHeader()
        {
            var record = new string[COL_COUNT];

            record[RECORD_TYPE] = "Type";
            record[LICENCE] = "Licence";
            record[LICENCE_STATUS] = "Licence Status";
            record[LOCATION] = "Location";
            record[ASSET_VALUE] = "Asset Value";
            record[LIABILITY_AMOUNT] = "Liability Amount";
            record[PVS_VALUE_TYPE] = "PVS Value Applied Type";
            record[ACTIVE_INDICATOR] = "Active Indicator";
            record[W_ABAND_WELL_BORE_AMOUNT] = "WB Abandonment";
            record[W_ABAND_ADDITIONAL_EVENT_AMOUNT] = "Additional Event";
            record[W_ABAND_GWP_AMOUNT] = "GWP";
            record[W_ABAND_VENT_FLOW_AMOUNT] = "Vent Flow";
            record[W_ABAND_GAS_MIGRATION_AMOUNT] = "Gas Migration";
            record[W_REC_SITE_AMOUNT] = "Site Reclamation";
            record[F_ABAND_FACILITY_AMOUNT] = "Abandonment";
            record[F_ABAND_SITE_SPECIFIC_AMOUNT] = "Site Specific Abandoment";
            record[F_REC_SITE_AMOUNT] = "Reclamation";
            record[F_REC_SITE_SPECIFIC_AMOUNT] = "Site Specific Reclamation";
            
            return record;
        }

        private string ConvertRecordsToCSV(List<string[]> records)
        {
            StringBuilder output = new StringBuilder();

            foreach (var record in records)
            {
                output.Append(string.Join(",", record));
                output.Append(System.Environment.NewLine);
            }

            return output.ToString();
        }

        private WellEntity[] ParseWellSection(StreamReader reader)
        {
            var wells = new List<WellEntity>();
            bool started = false;
            bool completed = false;
            string line;

            do
            {
                line = reader.ReadLine();

                if (line == null)
                    break;

                
                if (line == "********************************************************************************" && started == false)
                {
                    started = true;

                    // Skip the next to lines since they aren't useful
                    if (reader.ReadLine() == null) break;
                    if (reader.ReadLine() == null) break;

                } 
                else if (line == "********************************************************************************")
                {
                    // We've reached the end of the section
                    completed = true;
                }
                else if (started && line != string.Empty)
                {
                    // This is where the parsing of a single well happens
                    var well = new WellEntity();
                    var header = line.Split(';');

                    well.LicenceNumber = header[0].Trim();
                    well.LicenceStatus = header[1].Trim();
                    well.LocationIdentifier = header[2].Trim();
                    well.AssetValue = double.Parse(header[3], NumberStyles.Currency);
                    well.LiabilityAmount = double.Parse(header[4], NumberStyles.Currency);
                    well.PVSValueType = header[5].Trim();
                    well.ActiveIndicator = header[6].Trim();

                    do
                    {
                        line = reader.ReadLine();

                        if (line == null || line == string.Empty)
                            break;

                        var details = line.Split(';');

                        if (details[2].Trim() == "Abandonment" && details[3].Trim() == "WB Abandonment")
                        {
                            well.AbandomnentWellBoreAmountIncluded = (details[4].Trim() == "Y");
                            well.AbandomnentWellBoreAmount = double.Parse(details[5], NumberStyles.Currency);
                        }
                        else if (details[2].Trim() == "Abandonment" && details[3].Trim() == "Additional Event")
                        {
                            well.AbandomnentAdditionalEventAmountIncluded = (details[4].Trim() == "Y");
                            well.AbandomnentAdditionalEventAmount = double.Parse(details[5], NumberStyles.Currency);
                        }
                        else if (details[2].Trim() == "Abandonment" && details[3].Trim() == "GWP")
                        {
                            well.AbandomnentGWPAdmountIncluded = (details[4].Trim() == "Y");
                            well.AbandomnentGWPAdmount = double.Parse(details[5], NumberStyles.Currency);
                        }
                        else if (details[2].Trim() == "Abandonment" && details[3].Trim() == "Vent Flow")
                        {
                            well.AbandomnentVentFlowAmountIncluded = (details[4].Trim() == "Y");
                            well.AbandomnentVentFlowAmount = double.Parse(details[5], NumberStyles.Currency);
                        }
                        else if (details[2].Trim() == "Abandonment" && details[3].Trim() == "Gas Migration")
                        {
                            well.AbandomnentGasMigrationAmountIncluded = (details[4].Trim() == "Y");
                            well.AbandomnentGasMigrationAmount = double.Parse(details[5], NumberStyles.Currency);
                        }
                        else if (details[2].Trim() == "Reclamation" && details[3].Trim() == "Site Reclamation")
                        {
                            well.ReclamationSiteAmountIncluded = (details[4].Trim() == "Y");
                            well.ReclamationSiteAmount = double.Parse(details[5], NumberStyles.Currency);
                        }

                    } while (true);

                    wells.Add(well);
                }

            } while (!completed);


            return wells.ToArray();
        }

        private FacilityEntity[] ParseFacilitySection(StreamReader reader)
        {
            var facilities = new List<FacilityEntity>();
            bool started = false;
            bool completed = false;
            string line;

            do
            {
                line = reader.ReadLine();

                if (line == null)
                    break;


                if (line == "********************************************************************************" && started == false)
                {
                    started = true;

                    // Skip the next to lines since they aren't useful
                    if (reader.ReadLine() == null) break;
                    if (reader.ReadLine() == null) break;

                }
                else if (line == "********************************************************************************")
                {
                    // We've reached the end of the section
                    completed = true;
                }
                else if (started && line != string.Empty)
                {
                    // This is where the parsing of a single well happens
                    var facility = new FacilityEntity();
                    var header = line.Split(';');

                    facility.LicenceNumber = header[0].Trim();
                    facility.LicenceStatus = header[1].Trim();
                    facility.LocationIdentifier = header[2].Trim();
                    facility.LiabilityManagementProgram = header[3].Trim();
                    facility.AssetCalulationMethod = header[4].Trim();                    
                    facility.AssetValue = double.Parse(header[5], NumberStyles.Currency);
                    facility.LiabilityAmount = double.Parse(header[6], NumberStyles.Currency);
                    facility.PVSValueType = header[7].Trim();
                    facility.ActiveIndicator = header[8].Trim();

                    do
                    {
                        line = reader.ReadLine();

                        if (line == null || line == string.Empty)
                            break;

                        var details = line.Split(';');

                        if (details[2].Trim() == "Abandonment" && details[3].Trim() == "Fac Abandonment")
                        {
                            facility.AbandonmentFacilityAmountIncluded = (details[4].Trim() == "Y");
                            facility.AbandonmentFacilityAmount = double.Parse(details[5], NumberStyles.Currency);
                        }
                        else if (details[2].Trim() == "Abandonment" && details[3].Trim() == "Site Specific")
                        {
                            facility.AbandonmentSiteSpecificAmountIncluded = (details[4].Trim() == "Y");
                            facility.AbandonmentSiteSpecificAmount = double.Parse(details[5], NumberStyles.Currency);                        
                        }
                        else if (details[2].Trim() == "Reclamation" && details[3].Trim() == "Site Reclamation")
                        {
                            facility.ReclamationSiteReclamationAmountIncluded = (details[4].Trim() == "Y");
                            facility.ReclamationSiteReclamationAmount = double.Parse(details[5], NumberStyles.Currency);
                        }
                        else if (details[2].Trim() == "Reclamation" && details[3].Trim() == "Site Specific")
                        {
                            facility.ReclamationSiteSpecificAmountIncluded = (details[4].Trim() == "Y");
                            facility.ReclamationSiteSpecificAmount = double.Parse(details[5], NumberStyles.Currency);
                        }

                    } while (true);

                    facilities.Add(facility);
                }

            } while (!completed);

            return facilities.ToArray();
        }
    }
}
