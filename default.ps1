properties {
  $basedir    = resolve-path .
  $builddir   = "$basedir\build"
  $toolsdir   = "$basedir\tools"
  $packagedir = "$toolsdir\Package"
  $xapdir     = "$toolsdir\Content\ClientBin"
  $slnfile    = "$basedir\Driverslog.sln"
  
  $releasedir = "$basedir\releases"
  $version    = "1.1.0.0"
}

task default -depends Test

task Test -depends Compile, Clean { 
  @(
    "$builddir\Driverslog*.dll",
    "$builddir\Victoria*.dll",	
    "$builddir\System.Windows.Interactivity.dll",
    "$builddir\Caliburn.Micro.dll",
    "$toolsdir\Victoria.Test.Runner.dll",
    "$toolsdir\System.Xml.Linq.dll",
    "$builddir\Microsoft.Phone.Controls.Toolkit.dll"
  ) | foreach { copy-item $_ $packagedir }
  
  $env:path = "$toolsdir;$env:path"
  Victoria.Test.Console.exe $testpath
  
  if($lastexitcode -eq 0){
    write-host "P" -foregroundcolor green
  } else {
    write-host "F" -foregroundcolor red
  }
}

task Compile -depends Clean { 
  exec {msbuild "$slnfile" /p:Configuration=Debug  "/p:OutDir=$builddir\" /Verbosity:Quiet /nologo}
}

task Release -depends Test, Clean, GenerateAssemblyInfo  {
  exec {msbuild "$slnfile" /p:Configuration=Release /p:IsPackaging=true "/p:OutDir=$releasedir\$version\" }
}

task GenerateAssemblyInfo {
  Generate-Assembly-Info `
    -file "$basedir\Driverslog\Properties\AssemblyInfo.cs" `
    -title "Driver's log $version" `
    -description "" `
    -company "" `
    -product "Driver's log $version" `
    -version $version `
    -copyright "Copyright © Christofer Löf 2011"
}

task Clean { 
  @($builddir, $packagedir, $xapdir) | foreach {
    get-childitem $_ -recurse | remove-item -recurse
  }
}

task ? -Description "Helper to display task info" {
	Write-Documentation
}

##functions

function Get-Git-Commit
{
	$gitLog = git log --oneline -1
	return $gitLog.Split(' ')[0]
}

function Generate-Assembly-Info
{
param(
	[string]$clsCompliant = "true",
	[string]$title, 
	[string]$description, 
	[string]$company, 
	[string]$product, 
	[string]$copyright, 
	[string]$version,
	[string]$file = $(throw "file is a required parameter.")
)
  $commit = Get-Git-Commit
  $asmInfo = "using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: CLSCompliantAttribute($clsCompliant )]
[assembly: ComVisibleAttribute(false)]
[assembly: AssemblyTitleAttribute(""$title"")]
[assembly: AssemblyDescriptionAttribute(""$description"")]
[assembly: AssemblyCompanyAttribute(""$company"")]
[assembly: AssemblyProductAttribute(""$product"")]
[assembly: AssemblyCopyrightAttribute(""$copyright"")]
[assembly: AssemblyVersionAttribute(""$version"")]
[assembly: AssemblyInformationalVersionAttribute(""$version / $commit"")]
[assembly: AssemblyFileVersionAttribute(""$version"")]
[assembly: AssemblyDelaySignAttribute(false)]
"

	$dir = [System.IO.Path]::GetDirectoryName($file)
	if ([System.IO.Directory]::Exists($dir) -eq $false)
	{
		Write-Host "Creating directory $dir"
		[System.IO.Directory]::CreateDirectory($dir)
	}
	Write-Host "Generating assembly info file: $file"
	out-file -filePath $file -encoding UTF8 -inputObject $asmInfo
}