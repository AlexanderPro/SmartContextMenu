$ErrorActionPreference = 'Stop';
$packageName= 'smartcontextmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartContextMenu/releases/download/v1.7.2/SmartContextMenu_v1.7.2.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartContextMenu*'
  checksum      = '957ba30cedd92e90d66fdb6d565d180e4b153379da7d29a6d512c5485c36bf30'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
