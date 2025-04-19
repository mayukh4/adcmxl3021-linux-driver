#!/bin/bash

# Exit immediately if a command exits with a non-zero status.
set -e

WORKSPACE_ROOT=$(pwd) # Assumes script is run from ~/adcmxl3021
LIB_DIR="$WORKSPACE_ROOT/libcyusb"
BUILD_DIR="$LIB_DIR/build"
LOADER_SRC="$BUILD_DIR/loader.cpp"
LOADER_EXE="$BUILD_DIR/loader"
FIRMWARE_IMG="/home/fissellab/adcmxl3021/iSensor-FX3-Eval/resources/FX3_Firmware.img" # Using absolute path
BOOTLOADER_VID="0456"
BOOTLOADER_PID="ef02"

echo "--- Preparing FX3 Board ---"

# --- Check if libcyusb.so exists --- 
echo "[1/4] Checking for libcyusb.so..."
if [ ! -f "$BUILD_DIR/libcyusb.so" ]; then
    echo "  libcyusb.so not found. Building library..."
    mkdir -p "$BUILD_DIR"
    cd "$BUILD_DIR"
    echo "  Running cmake..."
    cmake ..
    echo "  Running make..."
    make
    echo "  Library built successfully."
    cd "$WORKSPACE_ROOT" # Go back to root
else
    echo "  libcyusb.so already exists. Skipping build."
fi

# --- Ensure loader.cpp exists and has correct firmware path --- 
echo "[2/4] Checking loader.cpp..."
if [ ! -f "$LOADER_SRC" ]; then
    echo "  loader.cpp not found. Please ensure it exists in $BUILD_DIR."
    echo "  You might need to re-run the step where I created it."
    exit 1
fi
# Ensure the correct path is uncommented (simple check)
grep -q "^const char \*firmware_path = \"$FIRMWARE_IMG\";" "$LOADER_SRC" 
if [ $? -ne 0 ]; then
    echo "  ERROR: loader.cpp does not seem to have the correct absolute firmware path uncommented:"
    echo "         $FIRMWARE_IMG"
    echo "  Please fix $LOADER_SRC and re-run."
    exit 1
fi
echo "  loader.cpp found with correct firmware path."

# --- Compile loader --- 
echo "[3/4] Compiling loader executable..."
cd "$BUILD_DIR"
if [ ! -f "$LOADER_EXE" ] || [ "$LOADER_SRC" -nt "$LOADER_EXE" ]; then
    echo "  Compiling loader.cpp..."
    g++ loader.cpp -o loader -I../include -L. -lcyusb -lusb-1.0 -Wl,-rpath,.
    echo "  Loader compiled successfully."
else
    echo "  Loader executable is up-to-date. Skipping compilation."
fi

# --- Run loader --- 
echo "[4/4] Running firmware loader..."
# Check if bootloader is present before trying to load
if lsusb -d $BOOTLOADER_VID:$BOOTLOADER_PID | grep -q $BOOTLOADER_PID; then
    echo "  FX3 Bootloader (PID: $BOOTLOADER_PID) found. Attempting firmware load..."
    echo "  Running: sudo ./loader"
    sudo ./loader
    echo "  Loader finished. Waiting 5 seconds for re-enumeration..."
    sleep 5 # Give time for the device to re-enumerate
else
    echo "  FX3 Bootloader (PID: $BOOTLOADER_PID) not found. Checking for App PID..."
    if lsusb -d 0456:ef03 | grep -q ef03; then
        echo "  FX3 Application firmware (PID: ef03) already seems to be loaded."
    else
        echo "  Neither Bootloader nor Application found. Please check USB connection and power."
        exit 1
    fi
fi

cd "$WORKSPACE_ROOT" # Go back to root
echo "--- FX3 Board Preparation Complete ---" 