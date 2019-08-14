#!/bin/bash

cd ..
cd Hangfire

while true; do
    dotnet Barcabot.HangfireService.dll
    sleep 1
done