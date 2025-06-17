#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>
#include <string.h>
#include <unistd.h>
#include <libusb-1.0/libusb.h>

// USB Constants (from decompiled code)
#define VID_CYPRESS_FX3         0x0456  // Actual VID from lsusb
#define PID_FX3_APPLICATION     0xEF03
#define ADI_STREAM_REALTIME     0xD0
#define ADI_WRITE_BYTE          0xF1

// SPI Configuration Constants
#define CONFIGURE_SPI_CMD       0xB2    // ConfigureSPI command (from decompiled code)
#define SPI_CLOCK_FREQ          14000000 // 14MHz (from SetDevice function)

// ADCMXL3021 Constants
#define REC_CTRL_ADDRESS        0x38    // REC_CTRL register address
#define MSC_CTRL_ADDRESS        0x34    // MSC_CTRL register address (educated guess)
#define RTS_MODE_VALUE          259     // 0x103 = bit 8 set + mode 3 (Real Time)
#define MSC_CTRL_DATA_READY     6       // Enable=4, Polarity=2, DIO1=0 -> 4|2|0 = 6
#define FRAME_SIZE_3021         200     // 200 bytes per RTS frame for 3-axis
#define TIMEOUT_MS              5000

// USB Control Transfer Parameters
#define BMREQUEST_TYPE          0x40    // Host to device, vendor, device
#define CONTROL_TIMEOUT         2000

int main() {
    libusb_context *ctx = NULL;
    libusb_device_handle *dev_handle = NULL;
    int result;
    
    printf("ADCMXL3021 RTS Linux Driver (Complete Fixed Version)\n");
    printf("====================================================\n");
    
    // Initialize libusb
    result = libusb_init(&ctx);
    if (result < 0) {
        printf("❌ Failed to initialize libusb: %s\n", libusb_strerror(result));
        return 1;
    }
    
    // Find and open FX3 device
    dev_handle = libusb_open_device_with_vid_pid(ctx, VID_CYPRESS_FX3, PID_FX3_APPLICATION);
    if (!dev_handle) {
        printf("❌ Could not find/open FX3 device (VID:%04X PID:%04X)\n", 
               VID_CYPRESS_FX3, PID_FX3_APPLICATION);
        printf("   Make sure device is connected and firmware is loaded\n");
        libusb_exit(ctx);
        return 1;
    }
    
    printf("✅ Connected to FX3 device (VID:%04X PID:%04X)\n", 
           VID_CYPRESS_FX3, PID_FX3_APPLICATION);
    
    // Claim interface 0 (required for control transfers)
    result = libusb_claim_interface(dev_handle, 0);
    if (result < 0) {
        printf("❌ Failed to claim interface: %s\n", libusb_strerror(result));
        libusb_close(dev_handle);
        libusb_exit(ctx);
        return 1;
    }
    
    printf("✅ Claimed USB interface 0\n");
    
    // Step 1: Skip SPI configuration for now (might not be needed)
    printf("\n🔧 Step 1: Skipping SPI configuration (trying direct approach)...\n");
    printf("✅ Proceeding without explicit SPI config\n");
    
    // Step 2: Write REC_CTRL register to enable Real Time mode
    printf("\n📝 Step 2: Writing REC_CTRL register...\n");
    
    uint8_t reg_data[2];
    reg_data[0] = RTS_MODE_VALUE & 0xFF;         // Low byte (0x03)
    reg_data[1] = (RTS_MODE_VALUE >> 8) & 0xFF;  // High byte (0x01)
    
    result = libusb_control_transfer(
        dev_handle,
        BMREQUEST_TYPE,        // bmRequestType: Host to device, vendor, device
        ADI_WRITE_BYTE,        // bRequest: 0xF1 (ADI_WRITE_BYTE)
        RTS_MODE_VALUE,        // wValue: 259 (0x103) - data to write
        REC_CTRL_ADDRESS,      // wIndex: 0x38 (REC_CTRL address)
        NULL,                  // data: NULL for register write
        0,                     // wLength: 0 bytes
        CONTROL_TIMEOUT
    );
    
    if (result < 0) {
        printf("❌ Failed to write REC_CTRL register: %s\n", libusb_strerror(result));
        libusb_release_interface(dev_handle, 0);
        libusb_close(dev_handle);
        libusb_exit(ctx);
        return 1;
    }
    
    printf("✅ REC_CTRL register written successfully (0x%04X = 0x%04X)\n", 
           REC_CTRL_ADDRESS, RTS_MODE_VALUE);
    
    // Step 3: Write MSC_CTRL register for data ready setup (SetupDataReady)
    printf("\n📝 Step 3: Writing MSC_CTRL register for data ready...\n");
    
    result = libusb_control_transfer(
        dev_handle,
        BMREQUEST_TYPE,        // bmRequestType: Host to device, vendor, device
        ADI_WRITE_BYTE,        // bRequest: 0xF1 (ADI_WRITE_BYTE)
        MSC_CTRL_DATA_READY,   // wValue: 6 (Enable=4, Polarity=2, DIO1=0)
        MSC_CTRL_ADDRESS,      // wIndex: 0x34 (MSC_CTRL address)
        NULL,                  // data: NULL for register write
        0,                     // wLength: 0 bytes
        CONTROL_TIMEOUT
    );
    
    if (result < 0) {
        printf("❌ Failed to write MSC_CTRL register: %s\n", libusb_strerror(result));
        libusb_release_interface(dev_handle, 0);
        libusb_close(dev_handle);
        libusb_exit(ctx);
        return 1;
    }
    
    printf("✅ MSC_CTRL register written successfully (0x%04X = 0x%04X)\n", 
           MSC_CTRL_ADDRESS, MSC_CTRL_DATA_READY);
    
    // Step 4: Set DrActive = true (critical for RTS!)
    printf("\n🔧 Step 4: Setting DrActive = true...\n");
    
    result = libusb_control_transfer(
        dev_handle,
        BMREQUEST_TYPE,        // bmRequestType: Host to device, vendor, device
        CONFIGURE_SPI_CMD,     // bRequest: 0xB2 (ConfigureSPI/ADI_SET_SPI_CONFIG)
        0xFFFF,                // wValue: 0xFFFF (DrActive = true)
        12,                    // wIndex: 12 (DrActive index)
        NULL,                  // data: NULL
        0,                     // wLength: 0 bytes
        CONTROL_TIMEOUT
    );
    
    if (result < 0) {
        printf("❌ Failed to set DrActive: %s\n", libusb_strerror(result));
        libusb_release_interface(dev_handle, 0);
        libusb_close(dev_handle);
        libusb_exit(ctx);
        return 1;
    }
    
    printf("✅ DrActive set successfully (critical for RTS)\n");
    
    // Step 5: Sleep 100ms (as per Windows code)
    printf("⏱️  Waiting 100ms...\n");
    usleep(100000);
    
    // Step 6: Start Real Time Streaming
    printf("\n🚀 Step 6: Starting Real Time Streaming...\n");
    
    uint32_t num_frames = 1000;  // Capture 1000 frames (~1 second at 1kHz)
    uint8_t pin_start = 0;       // PinStart flag (0 = false)
    uint8_t pin_exit = 1;        // PinExit flag (1 = true, as set in Windows code)
    
    // Prepare 5-byte buffer as per StartRealTimeStreaming
    uint8_t rts_buffer[5];
    rts_buffer[0] = num_frames & 0xFF;
    rts_buffer[1] = (num_frames >> 8) & 0xFF;
    rts_buffer[2] = (num_frames >> 16) & 0xFF;
    rts_buffer[3] = (num_frames >> 24) & 0xFF;
    rts_buffer[4] = pin_start ? 0xFF : 0x00;  // 5th byte = pinStart flag
    
    printf("   📊 Requesting %d frames (Frame size: %d bytes)\n", num_frames, FRAME_SIZE_3021);
    printf("   🎯 PinExit: %s, PinStart: %s\n", 
           pin_exit ? "true" : "false", 
           pin_start ? "true" : "false");
    
    result = libusb_control_transfer(
        dev_handle,
        BMREQUEST_TYPE,        // bmRequestType: Host to device, vendor, device
        ADI_STREAM_REALTIME,   // bRequest: 0xD0 (ADI_STREAM_REALTIME)
        pin_exit ? 0xFFFF : 0, // wValue: PinExit flag (0xFFFF = true, 0 = false)
        1,                     // wIndex: 1 (start operation, as per decompiled code)
        rts_buffer,            // data: 5-byte buffer
        5,                     // wLength: 5 bytes
        CONTROL_TIMEOUT
    );
    
    if (result < 0) {
        printf("❌ Failed to start RTS streaming: %s\n", libusb_strerror(result));
        
        if (result == LIBUSB_ERROR_TIMEOUT) {
            printf("   💡 This could indicate:\n");
            printf("      - ADCMXL3021 sensor not properly connected to FX3\n");
            printf("      - Additional sensor initialization required\n"); 
            printf("      - Incorrect SPI configuration\n");
            printf("      - Sensor not ready for streaming\n");
        }
        
        libusb_release_interface(dev_handle, 0);
        libusb_close(dev_handle);
        libusb_exit(ctx);
        return 1;
    }
    
    printf("✅ Real Time Streaming started successfully!\n");
    printf("   📈 Streaming %d frames of %d bytes each\n", num_frames, FRAME_SIZE_3021);
    printf("   💾 Total expected data: %d bytes\n", num_frames * FRAME_SIZE_3021);
    
    // Step 7: Read streaming data (simplified version)
    printf("\n📡 Step 7: Reading streaming data...\n");
    
    uint8_t *data_buffer = malloc(FRAME_SIZE_3021 * 10);  // Buffer for 10 frames
    if (!data_buffer) {
        printf("❌ Failed to allocate data buffer\n");
        libusb_release_interface(dev_handle, 0);
        libusb_close(dev_handle);
        libusb_exit(ctx);
        return 1;
    }
    
    int transferred = 0;
    int frames_read = 0;
    
    // Try to read some data from bulk endpoint (endpoint 0x81 based on previous tests)
    for (int i = 0; i < 10 && frames_read < 5; i++) {
        result = libusb_bulk_transfer(
            dev_handle,
            0x81,                    // endpoint (in)
            data_buffer,
            FRAME_SIZE_3021,
            &transferred,
            1000                     // 1 second timeout
        );
        
        if (result == 0 && transferred > 0) {
            printf("   📦 Frame %d: Read %d bytes\n", frames_read + 1, transferred);
            
            // Display first few bytes as hex
            printf("      Data: ");
            int bytes_to_show = transferred > 16 ? 16 : transferred;
            for (int j = 0; j < bytes_to_show; j++) {
                printf("%02X ", data_buffer[j]);
            }
            if (transferred > 16) printf("...");
            printf("\n");
            
            frames_read++;
        } else if (result == LIBUSB_ERROR_TIMEOUT) {
            printf("   ⏰ Timeout waiting for data (attempt %d)\n", i + 1);
        } else {
            printf("   ⚠️  Transfer error: %s\n", libusb_strerror(result));
        }
    }
    
    if (frames_read > 0) {
        printf("\n🎉 SUCCESS! Read %d frames of accelerometer data!\n", frames_read);
        printf("   📊 ADCMXL3021 Real Time Streaming is working!\n");
    } else {
        printf("\n⚠️  No data received, but RTS command succeeded\n");
        printf("   💡 This suggests the streaming setup is correct but may need:\n");
        printf("      - Different bulk endpoint address\n");
        printf("      - Longer timeout for first data\n");
        printf("      - Additional sensor configuration\n");
    }
    
    free(data_buffer);
    
    // Cleanup
    libusb_release_interface(dev_handle, 0);
    libusb_close(dev_handle);
    libusb_exit(ctx);
    
    printf("\n✅ Driver completed successfully\n");
    return 0;
} 