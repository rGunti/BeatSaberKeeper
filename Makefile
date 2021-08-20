# ======================== #
# BeatSaberKeeper Makefile #
# ======================== #

SLN = ./BeatSaberKeeper.sln
PROJECT = ./BeatSaberKeeper.App/BeatSaberKeeper.App.csproj
TEST_PROJECT = ./BeatSaberKeeper.Tests/BeatSaberKeeper.Tests.csproj

restore:
	dotnet restore $(SLN)

build: restore
	dotnet build --no-restore $(SLN)

test: build
	dotnet test --no-restore $(TEST_PROJECT)

clean:
	dotnet clean $(SLN)

setup-env:
	dotnet tool install -g nbgv

start-release:
	nbgv prepare-release
