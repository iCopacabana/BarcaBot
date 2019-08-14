#!/bin/bash

nohup ./run_img_microservice.sh >/dev/null 2>&1 & disown
nohup ./run_charts_microservice.sh >/dev/null 2>&1 & disown
nohup ./run_hangfire_microservice.sh >/dev/null 2>&1 & disown
nohup ./run_bot.sh >/dev/null 2>&1 & disown

echo Launched.