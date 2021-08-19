# ======================== #
# BeatSaberKeeper Makefile #
# ======================== #

PROJECT = ./BeatKeeper.App/BeatKeeper.App.csproj

restore:
	dotnet restore $(PROJECT)

build: restore
	dotnet build $(PROJECT)

clean:
	dotnet clean $(PROJECT)
	dotnet clean BeatKeeper.sln

setup-env:
	dotnet tool install -g nbgv

start-release:
	nbgv prepare-release
