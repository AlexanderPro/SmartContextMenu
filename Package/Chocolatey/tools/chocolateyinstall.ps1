$ErrorActionPreference = 'Stop';
$packageName= 'smartcontextmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartContextMenu/releases/download/v1.0.0/SmartContextMenu_v1.0.0.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartContextMenu*'
  checksum      = '3dfce381ae8d3eadd574b13d21f73122e3fceb8b835b457ffa4bdc29b5449110'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
