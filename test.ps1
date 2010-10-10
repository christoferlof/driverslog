#$basedir = resolve-path .
#msbuild "/p:OutDir=$basedir\build\"  /nologo /verbosity:quiet
#if($?){
#  Fx\Victoria.Test.Console\bin\debug\Victoria.Test.Console.exe $args[0]
#}
$testpath = ""
if($args[0] -ne $null) {
  $testpath = $args[0]
}
.\psake.ps1 -framework WP7 -task Test -parameters @{"testpath"=$testpath}
