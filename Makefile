noformat:
	@dotnet watch --project Tqxr.SolutionWatcher/Tqxr.SolutionWatcher.csproj format

format:
	@dotnet watch msbuild /t:format

test:
	@dotnet watch --project Tqxr.Capture.Lib/*.csproj test -c Release --filter TestType!=Ignore

