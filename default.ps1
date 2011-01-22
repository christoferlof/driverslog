properties {
  $basedir    = resolve-path .
  $builddir   = "$basedir\build"
  $toolsdir   = "$basedir\tools"
  $packagedir = "$toolsdir\Package"
  $xapdir     = "$toolsdir\Content\ClientBin"
  $slnfile    = "$basedir\Driverslog.sln"
  
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

task Clean { 
  @($builddir, $packagedir, $xapdir) | foreach {
    get-childitem $_ -recurse | remove-item -recurse
  }
}

task ? -Description "Helper to display task info" {
	Write-Documentation
}