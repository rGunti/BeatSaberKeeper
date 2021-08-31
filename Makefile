# ======================== #
# BeatSaberKeeper Makefile #
# ======================== #

SLN = ./BeatSaberKeeper.sln
PROJECT = ./BeatSaberKeeper.App/BeatSaberKeeper.App.csproj
TEST_PROJECT = ./BeatSaberKeeper.Tests/BeatSaberKeeper.Tests.csproj
PUBLISH_FOLDER = ./out/

restore:
	dotnet restore $(SLN)

build: restore
	dotnet build --no-restore $(SLN)

test: build
	dotnet test --no-restore --verbosity normal $(TEST_PROJECT)

publish: restore build
	dotnet publish --no-restore -c Release -r win-x64 -o $(PUBLISH_FOLDER) $(PROJECT)

clean:
	dotnet clean $(SLN)

setup-env:
	dotnet tool install -g nbgv

new-release:
	nbgv prepare-release
