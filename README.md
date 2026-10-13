<h1 align="center">
<a href="https://github.com/InaamMhm/no-hero-exp-multiplier/releases/latest/download/noheroexpmultiplier.dll">
    <img align="left" alt="Icon" height="90" src="Icon.png">
    <img align="right" alt="Download" height="75" src="https://raw.githubusercontent.com/gurrenm3/BTD-Mod-Helper/master/BloonsTD6%20Mod%20Helper/Resources/DownloadBtn.png">
</a>
noheroexpmultiplier
</h1>

Removes the XP sharing penalty when more than one hero is on the map. Each hero earns shared XP as if they were the only hero. Direct XP grants and purchased levels are unchanged.

This mod does not remove the limit on placing heroes.

## Compatibility

Tested on the Epic Games version of BTD6 57.1 with:

- MelonLoader 0.7.3
- BTD6 Mod Helper 3.6.9
- BTD6 Epic Games Mod Compat 1.2.1 (in `Plugins`)

BTD6 57.1 uses Unity 6000.3. The older Cpp2IL generator bundled with MelonLoader 0.7.2 cannot read its metadata. Use MelonLoader 0.7.3 with Cpp2IL `2022.1.0-pre-release.21`, then launch the game once to generate current assemblies.

The project also builds against the v57 Steam reference assemblies used by GitHub Actions. Gameplay testing was performed on Epic.

## Edit in Rider

1. Install the .NET 8 SDK and the .NET 6 runtime used by MelonLoader.
2. Install the dependencies above and launch BTD6 once.
3. Open `noheroexpmultiplier.sln` in Rider and allow NuGet restore to finish.
4. Edit `noheroexpmultiplier.cs`.
5. Build the **Debug** configuration. It copies the mod DLL to the game's `Mods` folder.
6. Select **BloonsTD6 (Only This Mod)** from the launch profiles and run it. Restart the game after rebuilding to load the new DLL.

The default game path is `C:\Program Files\Epic Games\BloonsTD6`. For another installation, create an untracked `Local.props` beside the project:

```xml
<Project>
    <PropertyGroup>
        <BloonsTD6>D:\Games\BloonsTD6</BloonsTD6>
    </PropertyGroup>
</Project>
```

No external `btd6.targets` file is needed. The project references your installed game and loader without copying their DLLs into the build output.

## Build and test

```powershell
dotnet build -c Debug
dotnet build -c Release
```

Release builds write `bin/Release/noheroexpmultiplier.dll`. To install a release build locally:

```powershell
dotnet build -c Release -p:DeployMod=true
```

Use `-p:DeployMod=false` to prevent a Debug build from copying into the game.

The **BloonsTD6 (Tests)** launch profile runs the Debug-only regression test in an isolated simulation and exits. It checks zero through four heroes, ordinary towers, and direct XP grants. Results are written to `MelonLoader/Latest.log`. The Release DLL excludes test code.

## Releases and the Mod Browser

Keep the repository public with the `btd6-mod` topic. `ModHelperData.cs` contains the mod version, tested game version, and the existing GitHub repository identity.

After testing a change, update `Version` and `LATEST.md`, commit and push the changes, then push a matching version tag (for example `1.1.0`). The GitHub Actions workflow builds the mod and attaches `noheroexpmultiplier.dll` to the tagged release. The tag must match `ModHelperData.Version`; `v1.1.0` is also accepted.

Updating these local files alone does not publish a release or change the Mod Browser listing.

[![Requires BTD6 Mod Helper](https://raw.githubusercontent.com/gurrenm3/BTD-Mod-Helper/master/banner.png)](https://github.com/gurrenm3/BTD-Mod-Helper#readme)
