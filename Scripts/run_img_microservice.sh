#!/bin/bash

cd ..
cd ImageManipulationMicroservice

while true; do
    python3 wsgi.py
    sleep 1
done