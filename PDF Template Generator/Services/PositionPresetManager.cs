using PDF_Template_Generator.Models;

namespace PDF_Template_Generator.Services
{
    public class PositionPresetManager
    {
        private readonly Dictionary<ProductLine, List<PositionPreset>> _presets;

        public PositionPresetManager()
        {
            _presets = new Dictionary<ProductLine, List<PositionPreset>>();
            InitializeDefaultPresets();
        }

        private void InitializeDefaultPresets()
        {
            // Playaway Presets
            _presets[ProductLine.Playaway] = new List<PositionPreset>
            {
                new PositionPreset("Upper Right Corner", 500, 700, ProductLine.Playaway, "Barcode", 100f, 0),
                new PositionPreset("Lower Right Corner", 500, 100, ProductLine.Playaway, "Barcode", 100f, 0),
                new PositionPreset("Spine (Vertical)", 308, 200, ProductLine.Playaway, "Barcode", 80f, 90),
                new PositionPreset("Center", 300, 400, ProductLine.Playaway, "Barcode", 100f, 0),
                new PositionPreset("Upper Left Corner", 50, 700, ProductLine.Playaway, "Barcode", 100f, 0),
                new PositionPreset("Lower Left Corner", 50, 100, ProductLine.Playaway, "Barcode", 100f, 0),
                // Logo presets
                new PositionPreset("Logo Upper Left", 50, 650, ProductLine.Playaway, "Logo", 50f, 0),
                new PositionPreset("Logo Upper Right", 450, 650, ProductLine.Playaway, "Logo", 50f, 0),
                // Text presets
                new PositionPreset("Address Standard", 480, 200, ProductLine.Playaway, "Text", 12f, 0)
            };

            // WhaZoodle Presets  
            _presets[ProductLine.WhaZoodle] = new List<PositionPreset>
            {
                new PositionPreset("Position 1 (Upper Right)", 550, 650, ProductLine.WhaZoodle, "Barcode", 100f, 0),
                new PositionPreset("Position 2 (Spine)", 455, 300, ProductLine.WhaZoodle, "Barcode", 80f, 90),
                new PositionPreset("Position 3 (Upper Left)", 50, 650, ProductLine.WhaZoodle, "Barcode", 100f, 0),
                new PositionPreset("Position 4 (Lower Left)", 50, 150, ProductLine.WhaZoodle, "Barcode", 100f, 0),
                new PositionPreset("Center Panel", 300, 400, ProductLine.WhaZoodle, "Barcode", 100f, 0),
                // Logo presets
                new PositionPreset("Logo Header", 250, 700, ProductLine.WhaZoodle, "Logo", 60f, 0),
                new PositionPreset("Logo Corner", 500, 600, ProductLine.WhaZoodle, "Logo", 40f, 0),
                // Text presets
                new PositionPreset("Address Panel", 400, 250, ProductLine.WhaZoodle, "Text", 12f, 0)
            };

            // Launchpad Presets
            _presets[ProductLine.Launchpad] = new List<PositionPreset>
            {
                new PositionPreset("Upper Right Corner", 520, 680, ProductLine.Launchpad, "Barcode", 100f, 0),
                new PositionPreset("Lower Right Corner", 520, 120, ProductLine.Launchpad, "Barcode", 100f, 0),
                new PositionPreset("Spine (Vertical)", 600, 300, ProductLine.Launchpad, "Barcode", 85f, 90),
                new PositionPreset("Center", 320, 420, ProductLine.Launchpad, "Barcode", 100f, 0),
                new PositionPreset("Upper Left Corner", 60, 680, ProductLine.Launchpad, "Barcode", 100f, 0),
                new PositionPreset("Lower Left Corner", 60, 120, ProductLine.Launchpad, "Barcode", 100f, 0),
                // Logo presets
                new PositionPreset("Logo Top Center", 300, 720, ProductLine.Launchpad, "Logo", 55f, 0),
                new PositionPreset("Logo Side Panel", 480, 580, ProductLine.Launchpad, "Logo", 45f, 0),
                // Text presets
                new PositionPreset("Address Standard", 450, 180, ProductLine.Launchpad, "Text", 12f, 0)
            };
        }

        public List<PositionPreset> GetPresetsForProduct(ProductLine productLine, string elementType = "")
        {
            if (!_presets.ContainsKey(productLine))
                return new List<PositionPreset>();

            var presets = _presets[productLine];
            
            if (!string.IsNullOrEmpty(elementType))
            {
                return presets.Where(p => p.ElementType.Equals(elementType, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return presets.ToList();
        }

        public PositionPreset? GetPresetByName(ProductLine productLine, string presetName)
        {
            if (!_presets.ContainsKey(productLine))
                return null;

            return _presets[productLine].FirstOrDefault(p => p.Name.Equals(presetName, StringComparison.OrdinalIgnoreCase));
        }

        public void AddCustomPreset(PositionPreset preset)
        {
            if (!_presets.ContainsKey(preset.ProductLine))
                _presets[preset.ProductLine] = new List<PositionPreset>();

            // Remove existing preset with same name
            _presets[preset.ProductLine].RemoveAll(p => p.Name.Equals(preset.Name, StringComparison.OrdinalIgnoreCase));
            
            // Add new preset
            _presets[preset.ProductLine].Add(preset);
        }

        public bool RemovePreset(ProductLine productLine, string presetName)
        {
            if (!_presets.ContainsKey(productLine))
                return false;

            int removedCount = _presets[productLine].RemoveAll(p => p.Name.Equals(presetName, StringComparison.OrdinalIgnoreCase));
            return removedCount > 0;
        }

        public void SaveCustomPresets(string filePath)
        {
            try
            {
                // Save only custom presets (those not in default set)
                var customPresets = new Dictionary<ProductLine, List<PositionPreset>>();
                
                foreach (var kvp in _presets)
                {
                    var defaultPresets = GetDefaultPresetsForProduct(kvp.Key);
                    var custom = kvp.Value.Where(p => !defaultPresets.Any(d => d.Name.Equals(p.Name, StringComparison.OrdinalIgnoreCase))).ToList();
                    
                    if (custom.Any())
                        customPresets[kvp.Key] = custom;
                }

                string json = System.Text.Json.JsonSerializer.Serialize(customPresets, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
            catch
            {
                // Handle save error silently
            }
        }

        public void LoadCustomPresets(string filePath)
        {
            if (!File.Exists(filePath)) return;

            try
            {
                string json = File.ReadAllText(filePath);
                var customPresets = System.Text.Json.JsonSerializer.Deserialize<Dictionary<ProductLine, List<PositionPreset>>>(json);
                
                if (customPresets != null)
                {
                    foreach (var kvp in customPresets)
                    {
                        foreach (var preset in kvp.Value)
                        {
                            AddCustomPreset(preset);
                        }
                    }
                }
            }
            catch
            {
                // Handle load error silently
            }
        }

        private List<PositionPreset> GetDefaultPresetsForProduct(ProductLine productLine)
        {
            var manager = new PositionPresetManager();
            manager._presets.Clear();
            manager.InitializeDefaultPresets();
            return manager._presets.GetValueOrDefault(productLine, new List<PositionPreset>());
        }
    }
} 