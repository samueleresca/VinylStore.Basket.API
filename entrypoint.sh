#!/bin/bash

set -e
run_cmd="dotnet run --project ./src/VinylStore.Basket.API/VinylStore.Basket.API.csproj"
exec $run_cmd