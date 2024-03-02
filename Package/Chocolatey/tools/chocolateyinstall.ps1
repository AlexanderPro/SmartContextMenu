$ErrorActionPreference = 'Stop';
$packageName= 'smartcontextmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartContextMenu/releases/download/v1.3.0/SmartContextMenu_v1.3.0.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartContextMenu*'
  checksum      = '2dea006a19bccd5e1888212184d13c433415a91dade9a85e807d56c5a9be97ed'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
