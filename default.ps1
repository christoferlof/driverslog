properties {
  $basedir = resolve-path .
  $builddir = "$basedir\build"
  $toolsdir = "$basedir\tools"
  $packagedir = "$toolsdir\Package"
  $xapdir = "$toolsdir\Content\ClientBin"
  $slnfile = "$basedir\Driverslog.sln"
  
}

task default -depends Test

task Test -depends Compile, Clean { 
  copy-item $builddir\Driverslog*.dll $packagedir
  copy-item $builddir\Victoria*.dll $packagedir
  copy-item $builddir\System.Xml.Linq.dll $packagedir
  copy-item $builddir\System.Windows.Interactivity.dll $packagedir
  copy-item $builddir\Caliburn.Micro.dll $packagedir
  
  $env:path = "$toolsdir;$env:path"
  exec {Victoria.Test.Console.exe $testpath} "Testrun failed"
}

task Compile -depends Clean { 
  exec {msbuild "$slnfile" /p:Configuration=Debug  "/p:OutDir=$builddir\" /Verbosity:Quiet /nologo}
}

task Clean { 
  get-childitem $builddir -recurse | remove-item -recurse
  get-childitem $packagedir -recurse | remove-item -recurse
  get-childitem $xapdir -recurse | remove-item -recurse
}

task ? -Description "Helper to display task info" {
	Write-Documentation
}