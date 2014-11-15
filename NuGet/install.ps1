param($installPath, $toolsPath, $package, $project)

$projectDirectory = (Get-Item $project.FullName).DirectoryName
& "$toolsPath\install.bat" "$installPath\.." "$projectDirectory" | Write-Host