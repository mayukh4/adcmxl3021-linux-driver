#!/bin/bash
export MONO_PATH=$(pwd)
mono --config fx3.config $(which python3) fx3_program.py
