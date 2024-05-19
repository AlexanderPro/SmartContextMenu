$ErrorActionPreference = 'Stop';
$packageName= 'smartcontextmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartContextMenu/releases/download/v1.3.4/SmartContextMenu_v1.3.4.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartContextMenu*'
  checksum      = '04e6168af2083b7245430f16411fc852fe2540586e1954842031fec406886875'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
