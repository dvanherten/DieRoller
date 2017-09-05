function Exec
{
    [CmdletBinding()] param(
        [Parameter(Position=0,Mandatory=1)][scriptblock]$cmd,
        [Parameter(Position=1,Mandatory=0)][string]$errorMessage = ($msgs.error_bad_command -f $cmd)
    )
    & $cmd
    if ($lastexitcode -ne 0) {
        throw ("Exec: " + $errorMessage)
    }
}

if(Test-Path .\artifacts) { Remove-Item .\artifacts -Force -Recurse }

exec { & dotnet restore }

exec { & dotnet build .\src\DieRoller -c Release }

pushd .\test\DieRoller.Tests\
exec { & dotnet test -c Release }
popd

# Will likely need to update the output path to remove the path navigation in 2.1
exec { & dotnet pack .\src\DieRoller -c Release -o ..\..\artifacts  }
