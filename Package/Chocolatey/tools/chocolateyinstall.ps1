$ErrorActionPreference = 'Stop';
$packageName= 'smartcontextmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartContextMenu/releases/download/v1.8.0/SmartContextMenu_v1.8.0.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartContextMenu*'
  checksum      = '6db630e7f353002d0d02eacab4611af6a046bef9b0405cf387ac031b3c58e574'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
