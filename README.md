When this branch is pulled it won't have anything inside the 'Database' folder.

Therefore, for it to work the user will need to navigate to the `BreweryAPI.Endpoints` project
and then run `dotnet ef database update` to run the migrations and create the empoty database.