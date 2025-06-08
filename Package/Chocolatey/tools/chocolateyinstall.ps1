$ErrorActionPreference = 'Stop';
$packageName= 'smartcontextmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartContextMenu/releases/download/v1.7.0/SmartContextMenu_v1.7.0.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartContextMenu*'
  checksum      = '911eef51ba148b6b3b15b2b970aea18fd34d4afc2e6f63447feb9e8b8f13bd17'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
