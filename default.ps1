properties {
  $basedir    = resolve-path .
  $builddir   = "$basedir\build"
  $toolsdir   = "$basedir\tools"
  $packagedir = "$toolsdir\Package"
  $xapdir     = "$toolsdir\Content\ClientBin"
  $slnfile    = "$basedir\Driverslog.sln"
  
  $releasedir = "$basedir\releases"
  $version    = "1.6.0.0"
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
    "$builddir\Microsoft.Phone.Controls.Toolkit.dll",
    "$builddir\FlurryWP7SDK.dll"
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

task Release -depends Test, Clean, GenerateAssemblyInfo, GenerateManifest  {
  exec {msbuild "$slnfile" /p:Configuration=Release /p:IsPackaging=true "/p:OutDir=$releasedir\$version\" }
  @(
    "$basedir\gfx\screen*.png",
    "$basedir\gfx\small-mobile-app-icon.png",
    "$basedir\gfx\large-mobile-app-icon.png",
    "$basedir\gfx\large-pc-app-icon.png",
    "$basedir\gfx\background.png"
  ) | foreach { copy-item $_ "$releasedir\$version\" }
}

task GenerateAssemblyInfo {
  Generate-Assembly-Info `
    -file "$basedir\Driverslog\Properties\AssemblyInfo.cs" `
    -title "Driver's log $version" `
    -description "" `
    -company "" `
    -product "Driver's log $version" `
    -version $version `
    -copyright "Copyright © Christofer Löf 2012"
}

task GenerateManifest {
  Generate-Manifest `
    -file "$basedir\Driverslog\Properties\WMAppManifest.xml" `
    -author "Christofer Löf" `
    -version $version `
    -description "Keep track of your car travels and related expenses. Export through e-mail for easy import to Excel and similar applications." `
    -publisher "Christofer Löf"
    
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
	[string]$clsCompliant = "false",
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
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: CLSCompliantAttribute($clsCompliant)]
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
[assembly: NeutralResourcesLanguageAttribute(""en"")]
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

function Generate-Manifest {
  param(
    [string]$author, 
    [string]$description, 
    [string]$publisher,  
    [string]$version,
    [string]$file = $(throw "file is a required parameter.")
  )
  
  $manifestInfo = "<?xml version=""1.0"" encoding=""utf-8""?>
<Deployment xmlns=""http://schemas.microsoft.com/windowsphone/2009/deployment"" AppPlatformVersion=""7.1"">
  <App xmlns="""" ProductID=""{f774542f-60bf-4c7c-88ed-6ff45b6008f5}"" Title=""@AppResLib.dll,-100"" RuntimeType=""Silverlight"" Version=""$version"" Genre=""apps.normal"" Author=""$author"" Description=""$description"" Publisher=""$publisher"">
    <IconPath IsRelative=""true"" IsResource=""false"">appicon.png</IconPath>
    <Capabilities>
      <Capability Name=""ID_CAP_NETWORKING"" />
      <Capability Name=""ID_CAP_IDENTITY_USER""/>
      <Capability Name=""ID_CAP_IDENTITY_DEVICE""/>
    </Capabilities>
    <Tasks>
      <DefaultTask Name=""_default"" NavigationPage=""MainPage.xaml"" />
    </Tasks>
    <Tokens>
      <PrimaryToken TokenID=""DriverslogToken"" TaskName=""_default"">
        <TemplateType5>
          <BackgroundImageURI IsRelative=""true"" IsResource=""false"">tile.png</BackgroundImageURI>
          <Count>0</Count>
          <Title>@AppResLib.dll,-200</Title>
        </TemplateType5>
      </PrimaryToken>
    </Tokens>
  </App>
</Deployment>"
    
  $dir = [System.IO.Path]::GetDirectoryName($file)
	if ([System.IO.Directory]::Exists($dir) -eq $false)
	{
		Write-Host "Creating directory $dir"
		[System.IO.Directory]::CreateDirectory($dir)
	}
	Write-Host "Generating manifest file: $file"
	out-file -filePath $file -encoding UTF8 -inputObject $manifestInfo
}