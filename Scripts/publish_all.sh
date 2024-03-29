#!/bin/bash

set -e

cd ..
cd Barcabot
dotnet restore
dotnet publish -c Release
cd ..

if [ -d Bot ]
then 
    cd Bot
    rm -r *
    cd ..
else
    mkdir Bot
fi

if [ -d Hangfire ]
then 
    cd Hangfire
    rm -r *
    cd ..
else
    mkdir Hangfire
fi

cp -r Barcabot/Barcabot.Bot/bin/Release/netcoreapp2.2/publish/* Bot/
cp -r Barcabot/Barcabot.HangfireService/bin/Release/netcoreapp2.2/publish/* Hangfire/

echo Done.