
1. Verify the release # are correct in *.csproj files

2. Build the Release Version
    dotnet build --configuration Release 

3. Create the nuget package
     dotnet pack -o ./nuget-packages

4. need the readme file on updloads
https://raw.githubusercontent.com/Hem/SimpleNet.Standard/master/README.md