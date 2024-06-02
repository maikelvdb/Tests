# Ask for the action to perform
$action = Read-Host -Prompt 'Enter the action to perform (add, update, or both)'

# Set the startup project
$startupProject = "Test"

# Set the target project
$targetProject = "Test.Database"

# Set the output directory
$outputDir = "../${targetProject}/Migrations/"

if ($action -eq 'add' -or $action -eq 'both') {
    # Ask for the migration name
    $migrationName = Read-Host -Prompt 'Enter the migration name'

    # Run the dotnet ef migrations add command
    dotnet ef migrations add $migrationName --startup-project $startupProject --project $targetProject --output-dir $outputDir

    if ($action -eq 'both') {
        # Ask whether to update the database
        $update = Read-Host -Prompt 'Do you want to update the database? (yes or no)'
        if ($update -eq 'yes') {
            # Run the dotnet ef database update command
            dotnet ef database update --startup-project $startupProject --project $targetProject
        }
    }
} elseif ($action -eq 'update') {
    # Run the dotnet ef database update command
    dotnet ef database update --startup-project $startupProject --project $targetProject
} else {
    Write-Host "Invalid action. Please enter 'add', 'update', or 'both'."
}
