$ErrorActionPreference = 'Stop';
$packageName= 'smartcontextmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartContextMenu/releases/download/v1.2.1/SmartContextMenu_v1.2.1.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartContextMenu*'
  checksum      = 'd484b114451bb1cea59aa3271143ef2430cc70870d8cbfd6a6fbe8d2639a8764'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
