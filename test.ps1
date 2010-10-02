msbuild  /nologo /verbosity:quiet
if($?){
  Fx\Victoria.Test.Console\bin\debug\Victoria.Test.Console.exe $args[0]
}