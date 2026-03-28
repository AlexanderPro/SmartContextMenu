$ErrorActionPreference = 'Stop';
$packageName= 'smartcontextmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartContextMenu/releases/download/v1.8.1/SmartContextMenu_v1.8.1.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartContextMenu*'
  checksum      = 'ae76eb6a15715d6ac77175a4eef779036e51f0e52dfe0105246d06d5b8939fa9'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
