cd .\src\Persistence\Nabs.TechTrek.PersistenceCli\

dotnet run -- Reset -i SharedShared -t 00000000-0000-0000-0000-000000000000
dotnet run -- Load -i SharedShared -t 731724a1-9b57-46ce-baaf-7325bc8711c0
dotnet run -- Load -i SharedShared -t 931d3b9a-4931-4577-bbe0-dc913db3d3c9

dotnet run -- Reset -i DedicatedDedicated -t 731724a1-9b57-46ce-baaf-7325bc8711c0
dotnet run -- Load -i DedicatedDedicated -t 731724a1-9b57-46ce-baaf-7325bc8711c0

cd ..\..\..\
