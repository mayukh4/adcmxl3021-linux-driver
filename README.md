# ADCMXL3021 Linux Driver

A Python driver for the Analog Devices ADCMXL3021 accelerometer that interfaces through the FX3 evaluation board, providing Linux support for functionality similar to the Windows ADCMXL_Evaluation software.

## Overview

This project enables Linux users to interface with the ADCMXL3021 accelerometer using the EVAL-ADIS-FX3 evaluation board. It includes:

- Python driver for ADCMXL3021 communication
- FX3 firmware loading and USB configuration scripts
- Complete libcyusb library and tools
- ADI iSensor-FX3-Eval resources and firmware

## Prerequisites

### System Requirements
- Linux (tested on Ubuntu/Debian-based systems)
- USB access permissions (udev rules or running as root)
- Python 3.x
- Build tools: `gcc`, `make`

### Install Build Dependencies
```bash
sudo apt update
sudo apt install build-essential git python3 python3-pip libusb-1.0-0-dev pkg-config
```

## Quick Start Guide

### 1. Clone the Repository
```bash
git clone https://github.com/your-username/adcmxl3021-linux-driver.git
cd adcmxl3021-linux-driver
```

**Note:** All dependencies (libcyusb and iSensor-FX3-Eval) are included in this repository, so no additional cloning is required.

### 2. Build libcyusb
```bash
cd libcyusb
make lib
sudo make install
sudo ldconfig  # Update library cache
cd ..
```

### 3. Configure USB Permissions

Add ADI device IDs to the cyusb configuration:
```bash
# Create cyusb.conf if it doesn't exist
sudo touch /etc/cyusb.conf

# Add ADI device IDs
echo "0456 ef02 ADI FX3 Bootloader" | sudo tee -a /etc/cyusb.conf
echo "0456 ef03 ADI FX3 Application" | sudo tee -a /etc/cyusb.conf
```

Alternatively, you can manually edit the file:
```bash
sudo nano /etc/cyusb.conf
# Add these lines:
# 0456 ef02 ADI FX3 Bootloader
# 0456 ef03 ADI FX3 Application
```

### 4. Prepare FX3 Board

Connect your EVAL-ADIS-FX3 board via USB and run:
```bash
sudo ./prepare_fx3.sh
```

This script will:
- Verify libcyusb installation
- Check for required tools (download_fx3)
- Load FX3 firmware from `iSensor-FX3-Eval/resources/FX3_Firmware.img`
- Verify successful firmware loading

### 5. Run the Python Driver
```bash
python3 adcmxl3021_python_driver.py
```

## Device Information

**Supported Device IDs:**
- **Bootloader Mode**: `0456:ef02` (ADI FX3 Bootloader)
- **Application Mode**: `0456:ef03` (ADI FX3 Application)

**Expected Workflow:**
1. Device starts in bootloader mode (`0456:ef02`)
2. `prepare_fx3.sh` loads firmware
3. Device re-enumerates as application (`0456:ef03`)
4. Python driver can now communicate with ADCMXL3021

## Project Structure

```
adcmxl3021-linux-driver/
├── README.md                           # This file
├── LICENSE                             # License information
├── prepare_fx3.sh                      # FX3 board setup script
├── setup_adcmxl3021.sh                # Main setup script
├── adcmxl3021_python_driver.py        # Python driver implementation
├── libcyusb/                          # Cypress USB library (included)
├── iSensor-FX3-Eval/                  # ADI evaluation tools (included)
│   └── resources/FX3_Firmware.img     # Required firmware
├── adcmxl_evaluation_rev_2192/        # Reference Windows software
└── ADCMXL_Evaluation_decompiled/      # Decompiled source for reference
```

## Troubleshooting

### FX3 Device Not Found
1. Check USB connection and power
2. Verify device appears in `lsusb`:
   ```bash
   lsusb | grep 0456
   ```
3. Ensure proper permissions:
   ```bash
   sudo chmod 666 /dev/bus/usb/*/*
   ```
   Or for a more permanent solution, add udev rules:
   ```bash
   echo 'SUBSYSTEM=="usb", ATTR{idVendor}=="0456", ATTR{idProduct}=="ef02", MODE="0666"' | sudo tee /etc/udev/rules.d/99-fx3.rules
   echo 'SUBSYSTEM=="usb", ATTR{idVendor}=="0456", ATTR{idProduct}=="ef03", MODE="0666"' | sudo tee -a /etc/udev/rules.d/99-fx3.rules
   sudo udevadm control --reload-rules
   ```

### Build Errors
1. Verify all build dependencies are installed
2. Make sure you're running the commands from the correct directory

### Permission Issues
- Run scripts with `sudo` if needed
- Consider adding user to appropriate groups:
  ```bash
  sudo usermod -a -G plugdev $USER
  ```

## Python Dependencies

Install with:
```bash
pip3 install numpy matplotlib pyusb
```

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test with actual hardware
5. Submit a pull request

## License

See [LICENSE](LICENSE) file for details.

## Related Resources

- [EVAL-ADIS-FX3 User Guide](https://wiki.analog.com/resources/eval/user-guides/inertial-mems/evaluation-systems/eval-adis-fx3)
- [ADCMXL3021 Product Page](https://www.analog.com/en/products/adcmxl3021.html)
- [iSensor-FX3-API Documentation](https://analogdevicesinc.github.io/iSensor-FX3-Eval/)

## Support

For hardware-related questions, refer to the official ADI documentation. For software issues, please open an issue in this repository. 