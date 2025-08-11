using System.Text.Json;
using System.Collections.Generic;

namespace PDF_Template_Generator.Models
{
    public class TemplateConfig
    {
        #region Basic Info
        public string Name { get; set; } = string.Empty;
        public ProductLine ProductLine { get; set; } = ProductLine.Playaway;
        public string SourcePdfPath { get; set; } = string.Empty;
        #endregion Basic Info

        #region Logo Settings
        public bool AddLogo { get; set; } = false;
        public string LogoPath { get; set; } = string.Empty;
        public int LogoScale { get; set; } = 100;
        public int LogoX { get; set; } = 0;
        public int LogoY { get; set; } = 0;
        #endregion Logo Settings

        #region Address Settings
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string Address3 { get; set; } = string.Empty;
        public int AddressX { get; set; } = 0;
        public int AddressY { get; set; } = 0;
        public string AddressFont { get; set; } = "Helvetica";
        public float AddressSize { get; set; } = 12f;
        public string AddressAlignment { get; set; } = "Left";
        public string AddressColor { get; set; } = "Black";
        #endregion Image Settings

        #region Barcode Settings
        public bool AddBarcode { get; set; } = false;
        public string BarcodePath { get; set; } = string.Empty;
        public string BarcodeNumber { get; set; } = string.Empty;
        public float BarcodeScale { get; set; } = 100f;
        public int BarcodeX { get; set; } = 0;
        public int BarcodeY { get; set; } = 0;
        public string BarcodeType { get; set; } = "CODABAR";
        public int BarcodeRotation { get; set; } = 0;
        #endregion

        #region Text Fields (5 Limit)
        public string Text1 { get; set; } = string.Empty;
        public int Text1X { get; set; } = 0;
        public int Text1Y { get; set; } = 0;
        public string Text1Font { get; set; } = "Helvetica";
        public float Text1Size { get; set; } = 12f;
        public string Text1Color { get; set; } = "Black";

        public string Text2 { get; set; } = string.Empty;
        public int Text2X { get; set; } = 0;
        public int Text2Y { get; set; } = 0;
        public string Text2Font { get; set; } = "Helvetica";
        public float Text2Size { get; set; } = 12f;
        public string Text2Color { get; set; } = "Black";

        public string Text3 { get; set; } = string.Empty;
        public int Text3X { get; set; } = 0;
        public int Text3Y { get; set; } = 0;
        public string Text3Font { get; set; } = "Helvetica";
        public float Text3Size { get; set; } = 12f;
        public string Text3Color { get; set; } = "Black";

        public string Text4 { get; set; } = string.Empty;
        public int Text4X { get; set; } = 0;
        public int Text4Y { get; set; } = 0;
        public string Text4Font { get; set; } = "Helvetica";
        public float Text4Size { get; set; } = 12f;
        public string Text4Color { get; set; } = "Black";

        public string Text5 { get; set; } = string.Empty;
        public int Text5X { get; set; } = 0;
        public int Text5Y { get; set; } = 0;
        public string Text5Font { get; set; } = "Helvetica";
        public float Text5Size { get; set; } = 12f;
        public string Text5Color { get; set; } = "Black";
        #endregion Text Fields (5 Limit)

        #region Special Elements
        public bool AddSpine { get; set; } = false;
        public bool WhiteBackground { get; set; } = false;
        #endregion

        #region Serialization Methods
        public string ToJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        }

        public static TemplateConfig? FromJson(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<TemplateConfig>(json);
            }
            catch
            {
                return null;
            }
        }

        public void SaveToFile(string filePath)
        {
            File.WriteAllText(filePath, ToJson());
        }

        public static TemplateConfig? LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            
            try
            {
                string json = File.ReadAllText(filePath);
                return FromJson(json);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Advanced Feature Detection
        public bool HasAdvancedFeatures()
        {
            return BarcodeRotation != 0 || HasSpineElements();
        }

        public bool HasRotatedBarcodes()
        {
            return AddBarcode && BarcodeRotation != 0;
        }

        public bool HasSpineElements()
        {
            // Spine is now a standard boolean feature, not advanced
            return false;
        }

        public string GetAdvancedFeaturesDescription()
        {
            var features = new List<string>();
            
            if (HasRotatedBarcodes())
            {
                features.Add($"Barcode rotation ({BarcodeRotation}°)");
            }
            
            if (HasSpineElements())
            {
                features.Add("Spine overlays");
            }
            
            return string.Join(", ", features);
        }

        public string GetLegacyCompatibilityWarning()
        {
            if (!HasAdvancedFeatures()) return "";
            
            return $"⚠️ This template uses advanced features that are NOT compatible with legacy Playaway/Launchpad format:\n\n" +
                   $"• {GetAdvancedFeaturesDescription()}\n\n" +
                   $"These features require the new JSON template format (.json). " +
                   $"Legacy systems will not recognize rotated barcodes or spine elements.\n\n" +
                   $"Recommendation: Save as '{Name}_Advanced.json' to distinguish from legacy templates.";
        }
        #endregion

        #region Legacy Import Support (for existing Playaway/Launchpad templates)
        public static TemplateConfig? ImportLegacyTemplate(string filePath)
        {
            if (!File.Exists(filePath)) return null;

            try
            {
                string content = File.ReadAllText(filePath).Trim();
                string[] fields = content.Split(';');
                
                if (fields.Length < 45) return null; // Minimum field count

                var config = new TemplateConfig();
                
                // Parse fields based on original format
                config.SourcePdfPath = fields[0].Trim();
                config.AddLogo = bool.Parse(fields[1].Trim());
                config.LogoPath = fields[2].Trim();
                int.TryParse(fields[3].Trim(), out int logoScale);
                config.LogoScale = logoScale;
                int.TryParse(fields[4].Trim(), out int logoX);
                config.LogoX = logoX;
                int.TryParse(fields[5].Trim(), out int logoY);
                config.LogoY = logoY;
                
                config.Address1 = fields[6];
                config.Address2 = fields[7];
                config.Address3 = fields[8];
                config.AddressX = int.Parse(fields[9]);
                config.AddressY = int.Parse(fields[10]);
                float.TryParse(fields[11].Trim(), out float addressSize);
                config.AddressSize = addressSize;
                config.AddressColor = fields[12];
                
                config.AddBarcode = bool.Parse(fields[13].Trim());
                config.BarcodePath = fields[14];
                config.BarcodeNumber = fields[15];
                float.TryParse(fields[16].Trim(), out float barcodeScale);
                config.BarcodeScale = barcodeScale;
                config.BarcodeX = int.Parse(fields[17]);
                config.BarcodeY = int.Parse(fields[18]);
                config.BarcodeType = fields[19];
                
                config.Text1 = fields[20];
                config.Text1X = int.Parse(fields[21]);
                config.Text1Y = int.Parse(fields[22]);
                float.TryParse(fields[23].Trim(), out float text1Size);
                config.Text1Size = text1Size;
                config.Text1Color = fields[24];
                
                config.Text2 = fields[25];
                config.Text2X = int.Parse(fields[26]);
                config.Text2Y = int.Parse(fields[27]);
                float.TryParse(fields[28].Trim(), out float text2Size);
                config.Text2Size = text2Size;
                config.Text2Color = fields[29];
                
                config.Text3 = fields[30];
                config.Text3X = int.Parse(fields[31]);
                config.Text3Y = int.Parse(fields[32]);
                float.TryParse(fields[33].Trim(), out float text3Size);
                config.Text3Size = text3Size;
                config.Text3Color = fields[34];
                
                config.Text4 = fields[35];
                config.Text4X = int.Parse(fields[36]);
                config.Text4Y = int.Parse(fields[37]);
                float.TryParse(fields[38].Trim(), out float text4Size);
                config.Text4Size = text4Size;
                config.Text4Color = fields[39];
                
                config.Text5 = fields[40];
                config.Text5X = int.Parse(fields[41]);
                config.Text5Y = int.Parse(fields[42]);
                float.TryParse(fields[43].Trim(), out float text5Size);
                config.Text5Size = text5Size;
                config.Text5Color = fields[44];
                
                config.AddSpine = bool.Parse(fields[45].Trim());
                
                if (fields.Length > 46 && !string.IsNullOrWhiteSpace(fields[46]))
                {
                    if (bool.TryParse(fields[46].Trim(), out bool whiteBackground))
                        config.WhiteBackground = whiteBackground;
                }
                
                return config;
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
} 