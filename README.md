# LLR Clean Up Tool (DDS)

Once a month the AER provides a detailed listing of the deemed well and facility values, along with their outstanding liabilities. This file comes from DDS in a format that is difficult to work with. This tool can be used to transform this text file into a nice CSV file.

When run, the tool will loop through the facilities and wells in the files and turn entries that look like this...

```
W 0005940 ; Issued; 07-10-011-13W4; $30,808.00; $108,781.00; 1 [PVS_Active_Well]; Y
;;Type; Liability; Included in Cost; Deemed Liability Amount
;;Abandonment; GWP; Y; $46,288.00
;;Abandonment; WB Abandonment; Y; $45,993.00
;;Reclamation; Site Reclamation; Y; $16,500.00
```

into a single CSV line that looks like this...

```
Well,W 0005940,Issued,07-10-011-13W4,30808,108781,1 [PVS_Active_Well],Y,45993,0.0,46288,0.0,0.0,16500,0.0,0.0,0.0,0.0
```

During this translation, some information may be thrown away.