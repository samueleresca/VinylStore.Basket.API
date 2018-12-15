#!/bin/bash

set -e
run_cmd="dotnet run --project ./src/VinylStore.Cart.API/VinylStore.Cart.API.csproj"
exec $run_cmd