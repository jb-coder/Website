#!/usr/bin/env bash
# Publishes the web app, starts it, exports the static site to dist/ and stops the server.
#
# Usage:
#   BASE_PATH=/my-repo/ PUBLIC_URL=https://user.github.io ./scripts/export-static.sh
set -euo pipefail

BASE_PATH="${BASE_PATH:-/}"
PUBLIC_URL="${PUBLIC_URL:-https://example.com}"
PORT="${PORT:-5199}"
OUTPUT="${OUTPUT:-dist}"
ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
PUBLISH="$ROOT/src/Portfolio.Web/bin/Release/net10.0/publish"
LOG="$(mktemp)"

echo "Publishing web app..."
dotnet publish "$ROOT/src/Portfolio.Web" -c Release --nologo

export ASPNETCORE_URLS="http://localhost:$PORT"
export ASPNETCORE_ENVIRONMENT="Production"
export Portfolio__BasePath="$BASE_PATH"
export Portfolio__PublicBaseUrl="$PUBLIC_URL"

echo "Starting published site..."
dotnet "$PUBLISH/Portfolio.Web.dll" > "$LOG" 2>&1 &
SERVER_PID=$!
trap 'kill "$SERVER_PID" 2>/dev/null || true' EXIT

for _ in $(seq 1 30); do
    if curl -fsS "http://localhost:$PORT$BASE_PATH" > /dev/null; then
        break
    fi
    sleep 1
done

echo "Exporting static site..."
dotnet run --project "$ROOT/tools/Portfolio.StaticExporter" -c Release -- \
    --source "http://localhost:$PORT" \
    --output "$ROOT/$OUTPUT" \
    --static-dir "$PUBLISH/wwwroot" \
    --base-path "$BASE_PATH" \
    --public-url "$PUBLIC_URL"

echo "Done. Static site available at $OUTPUT"
