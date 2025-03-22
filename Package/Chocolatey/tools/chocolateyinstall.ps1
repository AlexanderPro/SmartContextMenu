$ErrorActionPreference = 'Stop';
$packageName= 'smartcontextmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartContextMenu/releases/download/v1.6.0/SmartContextMenu_v1.6.0.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartContextMenu*'
  checksum      = 'b5eba7e1909557de35f100e0b16089bcb123816c90c8eb6812b579c587a210d6'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
