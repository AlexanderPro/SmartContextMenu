$ErrorActionPreference = 'Stop';
$packageName= 'smartcontextmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartContextMenu/releases/download/v1.5.0/SmartContextMenu_v1.5.0.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartContextMenu*'
  checksum      = '931f09c53a0d3c9f5016624c14d9cec2b220cc70a4b10ffcd1a3ecb1f9ff079a'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
