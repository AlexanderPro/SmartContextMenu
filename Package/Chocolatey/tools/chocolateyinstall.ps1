$ErrorActionPreference = 'Stop';
$packageName= 'smartcontextmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartContextMenu/releases/download/v1.4.0/SmartContextMenu_v1.4.0.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartContextMenu*'
  checksum      = 'db329554b07b2fed5147e04184b9a6e6b0d35d507665088361402084ef9209c1'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
