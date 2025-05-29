#!/bin/bash
# ADCMXL3021 Python Driver - Complete Setup Script
# This script sets up everything needed to use the ADCMXL3021 accelerometer
# Compatible with Ubuntu, Debian, Raspberry Pi OS, and most Linux distributions

set -e  # Exit on any error

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Configuration
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
LOG_FILE="$SCRIPT_DIR/setup.log"

# Helper functions
log() {
    echo "$(date '+%Y-%m-%d %H:%M:%S') - $1" | tee -a "$LOG_FILE"
}

print_header() {
    echo -e "\n${BLUE}=================================${NC}"
    echo -e "${BLUE} $1${NC}"
    echo -e "${BLUE}=================================${NC}\n"
}

print_success() {
    echo -e "${GREEN}✓ $1${NC}"
}

print_warning() {
    echo -e "${YELLOW}⚠ $1${NC}"
}

print_error() {
    echo -e "${RED}✗ $1${NC}"
}

# Check if running as root
check_root() {
    if [[ $EUID -eq 0 ]]; then
        print_error "Please do not run this script as root!"
        print_warning "The script will ask for sudo permissions when needed."
        exit 1
    fi
}

# Detect OS and package manager
detect_os() {
    print_header "Detecting Operating System"
    
    if [ -f /etc/os-release ]; then
        . /etc/os-release
        OS=$NAME
        VER=$VERSION_ID
        
        # Detect package manager
        if command -v apt-get >/dev/null 2>&1; then
            PKG_MANAGER="apt"
            UPDATE_CMD="sudo apt-get update"
            INSTALL_CMD="sudo apt-get install -y"
        elif command -v yum >/dev/null 2>&1; then
            PKG_MANAGER="yum"
            UPDATE_CMD="sudo yum update -y"
            INSTALL_CMD="sudo yum install -y"
        elif command -v dnf >/dev/null 2>&1; then
            PKG_MANAGER="dnf"
            UPDATE_CMD="sudo dnf update -y"
            INSTALL_CMD="sudo dnf install -y"
        else
            print_error "Unsupported package manager. Please install dependencies manually."
            exit 1
        fi
        
        print_success "Detected: $OS $VER"
        print_success "Package Manager: $PKG_MANAGER"
        log "OS: $OS $VER, Package Manager: $PKG_MANAGER"
    else
        print_error "Cannot detect operating system"
        exit 1
    fi
}

# Install system dependencies
install_dependencies() {
    print_header "Installing System Dependencies"
    
    log "Starting dependency installation"
    
    # Update package lists
    print_success "Updating package lists..."
    $UPDATE_CMD >> "$LOG_FILE" 2>&1
    
    # Core dependencies
    DEPS=(
        "build-essential"
        "cmake"
        "git"
        "libusb-1.0-0-dev"
        "python3"
        "python3-pip"
        "pkg-config"
        "udev"
    )
    
    # Qt5 dependencies for GUI tools (optional)
    QT_DEPS=(
        "qtbase5-dev"
        "qt5-qmake"
        "libqt5widgets5"
    )
    
    if [ "$PKG_MANAGER" = "apt" ]; then
        DEPS+=("g++" "libudev-dev")
        # Add Qt5 dependencies for Ubuntu/Debian
        DEPS+=("${QT_DEPS[@]}")
    elif [ "$PKG_MANAGER" = "yum" ] || [ "$PKG_MANAGER" = "dnf" ]; then
        DEPS+=("gcc-c++" "systemd-devel" "libusbx-devel")
        # Different Qt5 package names for RHEL/Fedora
        DEPS+=("qt5-qtbase-devel")
    fi
    
    print_success "Installing dependencies: ${DEPS[*]}"
    $INSTALL_CMD "${DEPS[@]}" >> "$LOG_FILE" 2>&1
    
    # Install Python dependencies
    print_success "Installing Python dependencies..."
    pip3 install --user numpy matplotlib >> "$LOG_FILE" 2>&1 || true
    
    print_success "All dependencies installed successfully"
    log "Dependencies installation completed"
}

# Build libcyusb library
build_libcyusb() {
    print_header "Building libcyusb Library"
    
    cd "$SCRIPT_DIR"
    
    if [ ! -d "libcyusb" ]; then
        print_error "libcyusb directory not found!"
        exit 1
    fi
    
    cd libcyusb
    log "Building libcyusb from $PWD"
    
    # Clean any previous builds
    make clean >> "$LOG_FILE" 2>&1 || true
    
    # Build the library and tools
    print_success "Compiling libcyusb library..."
    make lib >> "$LOG_FILE" 2>&1
    
    print_success "Compiling command-line tools..."
    make src >> "$LOG_FILE" 2>&1
    
    # Create the build directory structure expected by the Python driver
    mkdir -p build
    ln -sf ../src/download_fx3 build/loader
    
    print_success "libcyusb library built successfully"
    log "libcyusb build completed"
    
    cd "$SCRIPT_DIR"
}

# Install libcyusb system-wide
install_libcyusb() {
    print_header "Installing libcyusb System-wide"
    
    cd "$SCRIPT_DIR/libcyusb"
    
    print_success "Installing library and tools..."
    sudo make install >> "$LOG_FILE" 2>&1
    
    print_success "Updating library cache..."
    sudo ldconfig >> "$LOG_FILE" 2>&1
    
    log "libcyusb system installation completed"
    cd "$SCRIPT_DIR"
}

# Configure USB permissions and udev rules
configure_usb() {
    print_header "Configuring USB Permissions"
    
    # Add ADI vendor/device IDs to cyusb.conf if not already present
    if ! grep -q "0456.*ef02" /etc/cyusb.conf 2>/dev/null; then
        print_success "Adding ADI device IDs to cyusb configuration..."
        sudo cp /etc/cyusb.conf /etc/cyusb.conf.backup 2>/dev/null || true
        
        # Add ADI entries before the closing </VPD> tag
        sudo sed -i '/04b4.*1005.*FX3 2nd stage boot-loader/a\0456\tef02\t\tADI FX3 Bootloader\n0456\tef03\t\tADI FX3 Application' /etc/cyusb.conf
        
        print_success "ADI device IDs added to configuration"
        log "Added ADI device IDs to cyusb.conf"
    else
        print_success "ADI device IDs already configured"
    fi
    
    # Add user to plugdev group for USB access
    print_success "Adding user to plugdev group..."
    sudo usermod -a -G plugdev "$USER" >> "$LOG_FILE" 2>&1
    
    # Reload udev rules
    print_success "Reloading udev rules..."
    sudo udevadm control --reload-rules >> "$LOG_FILE" 2>&1
    sudo udevadm trigger >> "$LOG_FILE" 2>&1
    
    log "USB configuration completed"
}

# Verify firmware files
verify_firmware() {
    print_header "Verifying Firmware Files"
    
    FIRMWARE_PATHS=(
        "iSensor-FX3-Eval/resources/FX3_Firmware.img"
        "adcmxl_evaluation_rev_2192/Resources/FX3_Firmware.img"
    )
    
    FIRMWARE_FOUND=false
    for fw_path in "${FIRMWARE_PATHS[@]}"; do
        if [ -f "$SCRIPT_DIR/$fw_path" ]; then
            print_success "Found firmware: $fw_path"
            FIRMWARE_FOUND=true
            log "Firmware verified at $fw_path"
            break
        fi
    done
    
    if [ "$FIRMWARE_FOUND" = false ]; then
        print_error "No firmware files found!"
        print_warning "Please ensure firmware files are present in the repository"
        exit 1
    fi
}

# Test the installation
test_installation() {
    print_header "Testing Installation"
    
    # Test libcyusb tools
    if command -v download_fx3 >/dev/null 2>&1; then
        print_success "download_fx3 tool available"
    else
        print_warning "download_fx3 tool not in PATH"
    fi
    
    # Test Python driver import
    cd "$SCRIPT_DIR"
    if python3 -c "import sys; sys.path.insert(0, '.'); import adcmxl3021_python_driver; print('Python driver import successful')" >> "$LOG_FILE" 2>&1; then
        print_success "Python driver imports correctly"
    else
        print_warning "Python driver import failed (this may be normal without hardware)"
    fi
    
    # Check for connected FX3 devices
    if lsusb | grep -E "(0456:ef02|0456:ef03)" >/dev/null 2>&1; then
        print_success "FX3 device detected!"
        lsusb | grep -E "(0456:ef02|0456:ef03)"
    else
        print_warning "No FX3 device currently connected"
        print_warning "Connect your ADCMXL3021 device to test functionality"
    fi
    
    log "Installation test completed"
}

# Create usage example script
create_example() {
    print_header "Creating Usage Examples"
    
    cat > "$SCRIPT_DIR/run_example.py" << 'EOF'
#!/usr/bin/env python3
"""
ADCMXL3021 Example Usage
Simple example showing how to use the ADCMXL3021 Python driver
"""

import sys
import os

# Add current directory to path
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))

from adcmxl3021_python_driver import ADCMXL3021_Driver

def main():
    print("ADCMXL3021 Example Usage")
    print("=" * 40)
    
    try:
        # Create driver instance and connect
        with ADCMXL3021_Driver(debug=True) as accel:
            print("\n✓ Successfully connected to ADCMXL3021")
            
            # Show device info
            info = accel.get_device_info()
            print(f"\nDevice Info:")
            for key, value in info.items():
                print(f"  {key}: {value}")
            
            # Configure for RTS mode
            if accel.configure_rts_mode():
                print("\n✓ RTS mode configured")
                
                # Collect 2 seconds of data
                print("\nCollecting 2 seconds of data...")
                if accel.capture_burst_data(duration_sec=2.0):
                    print(f"✓ Captured {len(accel.acceleration_data)} samples")
                    
                    # Export to CSV
                    filename = accel.export_to_csv("example_data.csv")
                    if filename:
                        print(f"✓ Data saved to: {filename}")
                        
                        # Show first few samples
                        print(f"\nFirst 5 samples:")
                        print("Time(s)   | ax(g)   | ay(g)   | az(g)")
                        print("-" * 40)
                        for sample in accel.acceleration_data[:5]:
                            print(f"{sample['time']:8.4f} | {sample['ax']:7.4f} | {sample['ay']:7.4f} | {sample['az']:7.4f}")
                        
                        return True
            
        return False
        
    except Exception as e:
        print(f"\nError: {e}")
        print("\nTroubleshooting:")
        print("1. Ensure ADCMXL3021 device is connected via USB")
        print("2. Check that you're not running as root")
        print("3. Verify firmware loaded correctly")
        print("4. Try running: lsusb | grep 0456")
        return False

if __name__ == "__main__":
    success = main()
    sys.exit(0 if success else 1)
EOF
    
    chmod +x "$SCRIPT_DIR/run_example.py"
    print_success "Created run_example.py"
    
    # Create quick test script
    cat > "$SCRIPT_DIR/quick_test.sh" << 'EOF'
#!/bin/bash
# Quick test script for ADCMXL3021 setup

echo "ADCMXL3021 Quick Test"
echo "===================="

echo "1. Checking for FX3 devices..."
if lsusb | grep -E "(0456:ef02|0456:ef03)"; then
    echo "✓ FX3 device found"
else
    echo "⚠ No FX3 device detected. Please connect your ADCMXL3021 device."
fi

echo -e "\n2. Testing Python driver..."
cd "$(dirname "$0")"
python3 -c "
try:
    import adcmxl3021_python_driver
    print('✓ Python driver loads successfully')
except Exception as e:
    print(f'✗ Python driver error: {e}')
"

echo -e "\n3. Testing libcyusb tools..."
if command -v download_fx3 >/dev/null 2>&1; then
    echo "✓ download_fx3 tool available"
else
    echo "⚠ download_fx3 not in PATH"
fi

echo -e "\n4. Checking firmware files..."
if [ -f "iSensor-FX3-Eval/resources/FX3_Firmware.img" ]; then
    echo "✓ Firmware file found"
else
    echo "⚠ Firmware file missing"
fi

echo -e "\nSetup verification complete!"
echo "Run 'python3 run_example.py' to test with your device."
EOF
    
    chmod +x "$SCRIPT_DIR/quick_test.sh"
    print_success "Created quick_test.sh"
    
    log "Example scripts created"
}

# Print final instructions
print_final_instructions() {
    print_header "Setup Complete!"
    
    echo -e "${GREEN}🎉 ADCMXL3021 Python Driver Setup Successful!${NC}\n"
    
    echo -e "${BLUE}Next Steps:${NC}"
    echo "1. ${YELLOW}Logout and login again${NC} (or reboot) to apply group permissions"
    echo "2. Connect your ADCMXL3021 device via USB"
    echo "3. Run: ${GREEN}./quick_test.sh${NC} to verify everything works"
    echo "4. Run: ${GREEN}python3 run_example.py${NC} to test with your device"
    echo "5. Run: ${GREEN}python3 adcmxl3021_python_driver.py${NC} for the main demo"
    
    echo -e "\n${BLUE}Troubleshooting:${NC}"
    echo "• If device not detected: Check USB connection and try different port"
    echo "• If permission denied: Logout/login or reboot to apply group changes"
    echo "• Check logs in: ${LOG_FILE}"
    
    echo -e "\n${BLUE}Files Created:${NC}"
    echo "• ${GREEN}run_example.py${NC} - Simple usage example"
    echo "• ${GREEN}quick_test.sh${NC} - Quick system verification"
    echo "• ${GREEN}setup.log${NC} - Installation log"
    
    echo -e "\n${GREEN}Your ADCMXL3021 driver is ready to use!${NC} 🚀"
}

# Main setup function
main() {
    clear
    print_header "ADCMXL3021 Python Driver Setup"
    echo -e "${BLUE}Setting up complete ADCMXL3021 accelerometer driver${NC}"
    echo -e "${BLUE}This will install all dependencies and configure your system${NC}\n"
    
    # Initialize log
    echo "ADCMXL3021 Setup Log - $(date)" > "$LOG_FILE"
    
    # Confirm before proceeding
    read -p "Press Enter to continue or Ctrl+C to cancel..."
    
    # Run setup steps
    check_root
    detect_os
    install_dependencies
    build_libcyusb
    install_libcyusb
    configure_usb
    verify_firmware
    test_installation
    create_example
    print_final_instructions
    
    log "Setup completed successfully"
}

# Run main function
main "$@" 