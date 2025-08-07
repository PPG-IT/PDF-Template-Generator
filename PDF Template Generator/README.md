# PDF Template Generator

A unified template system for generating PDF overlays for Playaway, Launchpad, and WhaZoodle product lines.

## Features

### 🎯 **Multi-Product Support**
- **Playaway** - Full feature set with complex barcode options and spine elements
- **Launchpad** - Standard feature set with product-specific positioning  
- **WhaZoodle** - Specialized positioning for multi-panel insert designs

### 📄 **PDF Overlay Engine**
- Overlay logos, text, and barcodes onto existing PDF templates
- Product-aware positioning with automatic spine placement
- Support for rotation (0°, 90°, 180°, 270°) for spine barcodes
- Background overlay elements for better contrast

### 📊 **Barcode Generation**
- Multiple formats: CODABAR, CODE11, CODE128, CODE39, CODE93
- Configurable positioning, scaling, and rotation
- Optional white background for better readability
- Product-specific placement presets

### 🎨 **Customizable Elements**
- **Logo Overlay** - Positioning, scaling, product-specific presets
- **Address Text** - 3-line address with color, size, and positioning options
- **Custom Text Fields** - 5 independent text elements with full styling
- **Spine Elements** - Product-specific spine graphics and positioning

### 💾 **Template Management**
- Save/load templates in JSON format
- Import legacy templates from existing Playaway/Launchpad tools
- Position presets library for common placements
- Settings management for output directories

## Architecture

### **Core Classes**

#### Models
- `ProductLine` - Enum defining supported product lines
- `TemplateConfig` - Complete template configuration with JSON serialization
- `PositionPreset` - Named positioning presets per product line

#### Services
- `PDFProcessor` - Unified PDF overlay processing engine
- `PositionPresetManager` - Manages positioning presets for each product line

#### UI Components
- `Form1` - Main application interface with tabbed configuration
- `SettingsForm` - Application preferences and output directory management

### **Position Preset System**

Each product line has predefined positioning options:

**Playaway Presets:**
- Upper Right Corner, Lower Right Corner
- Spine (Vertical) with 90° rotation
- Logo positions optimized for Playaway layout

**WhaZoodle Presets:**
- Position 1-4 matching the numbered barcode areas
- Spine positioning for multi-panel layout
- Panel-specific text placement

**Launchpad Presets:**
- Corner and center positioning options
- Spine placement optimized for Launchpad design
- Logo and text presets for standard layout

## Usage

### **Basic Workflow**
1. **Select Product Line** - Choose Playaway, Launchpad, or WhaZoodle
2. **Load Source PDF** - Select the base template PDF
3. **Configure Elements** - Use tabs to set up logo, address, barcode, text
4. **Use Presets** - Select from named position presets or specify custom coordinates
5. **Generate PDF** - Create the final overlay PDF

### **Template Management**
- **Save Template** - Store configuration for reuse
- **Load Template** - Restore saved configurations
- **Import Legacy** - Convert existing Playaway/Launchpad templates

### **Position Presets**
- Select product line to load appropriate presets
- Choose from dropdown or specify custom X/Y coordinates
- Presets include position, scale, and rotation settings

## Technical Details

### **Dependencies**
- .NET 8.0 Windows Forms
- iTextSharp 5.5.13.3 - PDF manipulation
- System.Text.Json - Template serialization
- System.Configuration.ConfigurationManager - Settings management

### **File Structure**
```
PDF Template Generator/
├── Models/
│   ├── ProductLine.cs          # Product line enumeration
│   ├── TemplateConfig.cs       # Template configuration model
│   └── PositionPreset.cs       # Position preset model
├── Services/
│   ├── PDFProcessor.cs         # PDF overlay processing
│   └── PositionPresetManager.cs # Position preset management
├── Assets/                     # Spine graphics and overlay images
├── Properties/
│   └── Settings.settings       # Application settings
└── Forms/
    ├── Form1.cs               # Main application interface
    └── SettingsForm.cs        # Settings management
```

### **Legacy Compatibility**
The system can import templates from existing Playaway and Launchpad PDF Creator tools, automatically converting the semicolon-delimited format to the new JSON structure.

## Configuration

### **Output Directory**
Set via Settings → Preferences to specify where generated PDFs are saved.

### **Asset Directory**
Place spine graphics and overlay images in the `Assets` folder:
- `play-spine.gif` - Playaway spine graphic
- `view-spine.gif` - Launchpad/WhaZoodle spine graphic
- `WhiteBoxBottom.gif` - Background overlay for Playaway
- `WhiteBoxBack.gif` - Background overlay for barcodes

## Migration from Legacy Tools

The unified system consolidates functionality from:
- **Playaway PDF Creator** - Advanced features with multiple barcode libraries
- **Launchpad PDF Creator** - Standard features with WhaZoodle support

Templates from both legacy systems can be imported using the "Import Legacy Template" feature.

## Future Enhancements

- Enhanced barcode library integration (BusinessRefinery.Barcode)
- PDF preview functionality
- Batch processing capabilities
- Additional product line support
- Custom position preset creation and sharing

---

**Built with .NET 8.0 Windows Forms for unified PDF template generation across multiple product lines.** 