param(
    [Parameter(Mandatory=$true)]
    [int]$year
)

$basePath = "../src/${year}"
$commonProjectPath = "../src/common/AdventOfCode.Common/AdventOfCode.Common.csproj"

$projectName = "AdventOfCode.y${year}"
$projectPath = "$basePath/${projectName}"

$testProjectName = "$projectName.Tests"
$testProjectPath = "$basePath/${testProjectName}"

# Creates the inputs folder and input files, and add them to the csproj
function Generate-Input-Files {
    param(
        [Parameter(Mandatory=$true)]
        [string]$projectName
    )

    $currentProjectPath = "$basePath/${projectName}"
    $inputsFolderPath = "$currentProjectPath/inputs"
    $projectProj = "$currentProjectPath/$projectName.csproj"

    mkdir $inputsFolderPath | out-null

    For ($day = 1; $day -le 25; $day++) {
        $inputPath = "$inputsFolderPath/day${day}.txt"

        if (Test-Path $inputPath) {
            # Write-Host "Input file for day ${day} already exists at '${dayPath}'."
            continue
        }

        New-Item $inputPath -ItemType File | out-null
    }

    $newNode = [xml]@"
    <ItemGroup>
        <None Update="inputs\*.txt">
        <CopyToOutputDirectory>Always</CopyToOutputDirectory>
        </None>
    </ItemGroup>
"@

    $xml = [xml](Get-Content $projectProj)
    $xml.Project.AppendChild($xml.ImportNode($newNode.ItemGroup, $true)) | out-null
    $xml.Save($projectProj)
}

if (Test-Path $projectPath) {
    Write-Host "Project '${projectName}' already exists at '${projectPath}'."
    exit 1
}

### Generate project ###
Write-Host "Generating project '${projectName}' at '${projectPath}'..."

# Generate a console project and add a reference to the common project
dotnet new console -n $projectName -o $projectPath | out-null
dotnet add "$projectPath/$projectName.csproj" reference $commonProjectPath | out-null

# Update Program.cs to use the common project
$programPath = "$projectPath/Program.cs"
Set-Content $programPath "using AdventOfCode.Common;`n`nDayRunner.Run();"

# Generate classes for each day
For ($day = 1; $day -le 25; $day++) {
    $dayPath = "$projectPath/Day${day}.cs"

    if (Test-Path $dayPath) {
        # Write-Host "Day ${day} already exists at '${dayPath}'."
        continue
    }

    # Write-Host "Generating day ${day} at '${dayPath}'..."

    $dayClass = @"
using AdventOfCode.Common;

namespace AdventOfCode.y$year
{
    [DayNumber($day)]
    public class Day$day : Day
    {
        protected override string ExecutePartOne(IEnumerable<string> input)
        {
            return string.Empty;
        }

        protected override string ExecutePartTwo(IEnumerable<string> input)
        {
            return string.Empty;
        }
    }
}
"@

    Set-Content $dayPath $dayClass
}

# Generate inputs folder and files
Generate-Input-Files -projectName $projectName

### Generate test project ###
Write-Host "Generating test project '${testProjectName}' at '${testProjectPath}'..."

# Generate a test project and add a reference to the main and common projects
dotnet new nunit -n $testProjectName -o $testProjectPath | out-null
dotnet add "$testProjectPath/$testProjectName.csproj" reference $commonProjectPath | out-null
dotnet add "$testProjectPath/$testProjectName.csproj" reference "$projectPath/$projectName.csproj" | out-null

# Remove default test file
Remove-Item "$testProjectPath/UnitTest1.cs"

# Generate inputs folder and files
Generate-Input-Files -projectName $testProjectName

# Generate the test class
$testClass = @"
namespace AdventOfCode.y$year.Tests
{
    public static class DayExtensions
    {
        public static void AssertPartOneResult(this Day day, string expectedResult)
        {
            Assert.That(day.PartOne(), Is.EqualTo(expectedResult));
        }

        public static void AssertPartTwoResult(this Day day, string expectedResult)
        {
            Assert.That(day.PartTwo(), Is.EqualTo(expectedResult));
        }
    }

    [TestFixture]
    public class DayTest
    {
"@

For ($day = 1; $day -le 25; $day++) {

    If($day -ne 1){
        $testClass += "`n"
    }

    $testClass += @"

        [Test]
        public void Assert_Day${day}_Results()
        {
            var day = new Day$day();
            day.AssertPartOneResult("");
            day.AssertPartTwoResult("");
        }
"@
}

$testClass += @"

    }
}
"@

Set-Content "$testProjectPath/DayTest.cs" $testClass

Write-Host "Done!"