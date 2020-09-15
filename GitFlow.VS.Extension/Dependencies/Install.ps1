#Copy necessary files from the util-linux package into the bin directory of the msysgit installation
#Runs gitflow\contrib\msysgit-install.cmd
#
# NOTE:This script must be executed with elevated priviledges, in case Git is installed below program files
#
Param
(
	[Parameter(Mandatory=$True)] [string] $gitInstallPath
)
if (Get-Command 'gh' -CommandType Application -ErrorAction SilentlyContinue)
{
     Write-Host "GitHub CLI exists"
}
else
{
     try
     {
          $site = Invoke-WebRequest -Uri https://cli.github.com
          $fileName = 'GitHubCLI.msi'
          $filePath = (".\" + $fileName)

          #Write-Host $site.Links
          $site.Links | ForEach-Object {
               if ($_.href -match '(gh[_\d\w\.]+\.msi)$')
               {
                    Write-Host "Downloading " $Matches.1
                    Invoke-WebRequest -Uri $_.href -OutFile $filePath
                    break
               }
          }
          
          If (Test-Path -Path $filePath)
          {
               Write-Host "Installing GitHub CLI"
               Start-Process msiexec.exe -Wait -ArgumentList ("/I {0} /quiet" -f $fileName)
               Remove-Item $filePath
          }
          else
          {
               throw "missing file"
          }
     }
     catch
     {          
          Write-Host "Automatic download or installation of GitHub CLI failed, visit https://cli.github.com and install manually."          
     }
}

$installationPath = Split-Path $MyInvocation.MyCommand.Path
$binaries = Join-Path $installationPath "binaries"
$gitLocation = Join-Path $gitInstallPath "bin"

try
{
     Write-Host "Copy binaries to Git installation directory " + $gitLocation
     Copy-Item -Path "$binaries\*.*" -Destination "$gitLocation" -Force
}
catch
{          
     Write-Host "Copying failed, elevated user rights might be required"
}

try
{
     Write-Host "Execute msysgit installation"
     #Run gitflow install script
     $installScript = Join-Path $installationPath "gitflow\contrib\msysgit-install.cmd"
     $pinfo = New-Object System.Diagnostics.ProcessStartInfo
     $pinfo.FileName = $installScript
     $pinfo.UseShellExecute = $true
     $pinfo.Arguments = """$gitInstallPath"""

     $p = New-Object System.Diagnostics.Process
     $p.StartInfo = $pinfo
     $p.Start() | Out-Null
     $p.WaitForExit()

     Write-Host "Installation done!"
}
catch
{          
     Write-Host "Installation of msysgit failed"
}