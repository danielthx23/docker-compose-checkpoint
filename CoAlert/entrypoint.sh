#!/bin/bash
set -e

if [ "$RUN_MIGRATIONS" = "true" ]; then
  echo ">> Rodando migrations..."
  dotnet ef database update --project ./src/CoAlert.csproj --verbose
fi

echo ">> Iniciando aplicação..."
exec dotnet ./CoAlert.dll