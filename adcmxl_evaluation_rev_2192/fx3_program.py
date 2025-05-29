#!/usr/bin/env python3

import clr
from time import sleep
import os
import sys

# Get current directory
current_dir = os.path.abspath(os.getcwd())

try:
    # Add DLL references - they're in the root directory
    print("Loading DLLs...")
    clr.AddReference(os.path.join(current_dir, "FX3ApiWrapper.dll"))
    clr.AddReference(os.path.join(current_dir, "CyUSB.dll"))
    clr.AddReference(os.path.join(current_dir, "FX3Api.dll"))
    clr.AddReference(os.path.join(current_dir, "adisApi.dll"))
    clr.AddReference(os.path.join(current_dir, "RegMapClasses.dll"))
    
    # Import after adding references
    from FX3ApiWrapper import *
    from System import Array, String
    
    print("Creating FX3 wrapper...")
    # Create wrapper with current directory as resource path
    Dut = Wrapper(current_dir, "", SensorType.StandardImu)
    
    print("Checking firmware version...")
    print(f"Current firmware version: {Dut.FX3.GetFirmwareVersion}")
    
    print("Setting firmware paths...")
    resources_dir = os.path.join(current_dir, "Resources")
    
    # Set all firmware paths
    boot_fw_path = os.path.join(resources_dir, "boot_fw.img")
    main_fw_path = os.path.join(resources_dir, "FX3_Firmware.img")
    flash_prog_path = os.path.join(resources_dir, "USBFlashProg.img")
    
    if not os.path.exists(boot_fw_path):
        raise FileNotFoundError(f"Boot firmware not found at {boot_fw_path}")
    
    print(f"Using boot firmware: {boot_fw_path}")
    print(f"Using main firmware: {main_fw_path}")
    print(f"Using flash programmer: {flash_prog_path}")
    
    Dut.FX3.BootloaderPath = boot_fw_path
    Dut.FX3.FirmwarePath = main_fw_path
    Dut.FX3.FlashProgrammerPath = flash_prog_path
    
    # Check if board is available
    if not Dut.FX3.FX3BoardAttached:
        print("Waiting for FX3 board...")
        sleep(2)
        if not Dut.FX3.FX3BoardAttached:
            raise Exception("No FX3 board found")
    
    # Program the device
    print("Programming device...")
    Dut.FX3.ProgramFX3()
    
    # Wait for device to reset
    print("Waiting for device reset...")
    sleep(5)
    
    # Check boot status
    print("Checking boot status...")
    status = Dut.FX3.GetBootStatus
    print(f"Boot status: {status}")
    
    # Blink LED to indicate success
    print("Blinking LED...")
    Dut.UserLEDBlink(2.0)
    
except Exception as e:
    print(f"Error: {e}")
    import traceback
    traceback.print_exc()