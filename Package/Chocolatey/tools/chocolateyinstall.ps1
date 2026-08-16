$ErrorActionPreference = 'Stop';
$packageName= 'smartcontextmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartContextMenu/releases/download/v1.9.0/SmartContextMenu_v1.9.0.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartContextMenu*'
  checksum      = '96ab5c3784094187007fa5c8202c07c3bb88cb26a5b4dbc3322fd5b43b9dae9f'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
