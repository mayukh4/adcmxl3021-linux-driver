#!/bin/bash

# Exit immediately if a command exits with a non-zero status.
set -e

WORKSPACE_ROOT=$(pwd) # Assumes script is run from ~/adcmxl3021
FIRMWARE_IMG="$WORKSPACE_ROOT/iSensor-FX3-Eval/resources/FX3_Firmware.img"
BOOTLOADER_VID="0456"
BOOTLOADER_PID="ef02"
APPLICATION_PID="ef03"

echo "--- Preparing FX3 Board ---"

# --- Check if libcyusb.so exists --- 
echo "[1/4] Checking for libcyusb.so..."
if [ -f "/usr/local/lib/libcyusb.so" ]; then
    echo "  libcyusb.so found at /usr/local/lib/libcyusb.so"
else
    echo "  libcyusb.so not found. Building library..."
    cd "$WORKSPACE_ROOT/libcyusb"
    make lib || { echo "  ERROR: Make failed"; exit 1; }
    sudo make install || { echo "  ERROR: Install failed"; exit 1; }
    echo "  Library built and installed successfully."
    cd "$WORKSPACE_ROOT"
fi

# --- Check if download_fx3 tool exists ---
echo "[2/4] Checking for download_fx3 tool..."
if which download_fx3 > /dev/null 2>&1; then
    echo "  download_fx3 tool found at $(which download_fx3)"
else
    echo "  download_fx3 tool not found. Installing from libcyusb..."
    cd "$WORKSPACE_ROOT/libcyusb"
    make all || { echo "  ERROR: Make failed"; exit 1; }
    sudo cp src/download_fx3 /usr/local/bin/ || { echo "  ERROR: Install failed"; exit 1; }
    echo "  download_fx3 tool installed successfully."
    cd "$WORKSPACE_ROOT"
fi

# --- Check firmware file exists ---
echo "[3/4] Checking firmware file..."
if [ -f "$FIRMWARE_IMG" ]; then
    echo "  Firmware file found: $FIRMWARE_IMG"
else
    echo "  ERROR: Firmware file not found: $FIRMWARE_IMG"
    echo "  Please ensure iSensor-FX3-Eval repository is properly cloned."
    exit 1
fi

# --- Run firmware loader ---
echo "[4/4] Running firmware loader..."
cd "$WORKSPACE_ROOT" # Ensure we're in the correct directory

# Check if bootloader is present before trying to load
if lsusb -d $BOOTLOADER_VID:$BOOTLOADER_PID | grep -q $BOOTLOADER_PID; then
    echo "  FX3 Bootloader (PID: $BOOTLOADER_PID) found. Attempting firmware load..."
    echo "  Running: sudo download_fx3 -t RAM -i $FIRMWARE_IMG"
    sudo download_fx3 -t RAM -i "$FIRMWARE_IMG"
    echo "  Firmware loading finished. Waiting 5 seconds for re-enumeration..."
    sleep 5 # Give time for the device to re-enumerate
    
    # Check if device re-enumerated successfully
    if lsusb -d $BOOTLOADER_VID:$APPLICATION_PID | grep -q $APPLICATION_PID; then
        echo "  SUCCESS: FX3 Application firmware (PID: $APPLICATION_PID) is now running!"
    else
        echo "  WARNING: Device may not have re-enumerated correctly."
    fi
else
    echo "  FX3 Bootloader (PID: $BOOTLOADER_PID) not found. Checking for App PID..."
    if lsusb -d $BOOTLOADER_VID:$APPLICATION_PID | grep -q $APPLICATION_PID; then
        echo "  FX3 Application firmware (PID: $APPLICATION_PID) already loaded."
    else
        echo "  Neither Bootloader nor Application found. Please check USB connection and power."
        echo "  Expected devices:"
        echo "    - Bootloader: $BOOTLOADER_VID:$BOOTLOADER_PID"
        echo "    - Application: $BOOTLOADER_VID:$APPLICATION_PID"
        echo "  Current USB devices:"
        lsusb | grep $BOOTLOADER_VID || echo "    No ADI devices found"
        exit 1
    fi
fi

echo "--- FX3 Board Preparation Complete ---" 