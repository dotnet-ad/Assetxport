![Logo](Documentation/Logo.png)

Resize UWP, Xamarin.Android and Xamarin.iOS assets automatically.

## Install

Available on NuGet

[![NuGet](https://img.shields.io/nuget/v/Assetxport.svg?label=NuGet)](https://www.nuget.org/packages/Assetxport/)

## Usage

The easiest way is to use **Assetxport** from its build task. Simply add a [NuGet](https://www.nuget.org/packages/Assetxport/) package reference to your Xamarin.iOS, Xamarin.Android or Universal Windows Platform project and add a `Config.assets.json` configuration file at the root of your project.

`Config.assets.json`

```json
{
  "Platform": "<iOS|Android|UWP>",
  "Input": [ "<input folder path relative to this file, containing all the source hd assets>" ],
  "Output": "<output folder path relative to this file, where all the density assets are generated to>"
}
```

```json
{
  "Platform": "iOS",
  "Input": [ "../Assetxport.Sample.Cli/Images/" ],
  "Output": "Assets.xcassets"
}
```

In your high definition asset source folder, all `.png`|`.jpg` images should have a suffix qualifier indicating their original density. Here is the list of available qualifiers :

* `<name>.<ext>` : **1.00**
* `<name>@<value>x.<ext>` : **value**
* `<name>@ldpi.<ext>` : **0.75**
* `<name>@mdpi.<ext>` : **1.00**
* `<name>@hdpi.<ext>` : **1.50**
* `<name>@xhdpi.<ext>` : **2.00**
* `<name>@xxhdpi.<ext>` : **3.00**
* `<name>@xxxhdp.<ext>i` : **4.00**

Then simply build the project to generate your various assets (*with densities lower or equal to the original density*) to the output folder!

## Platforms

### iOS

Assets are generated as asset catalog entries (`.imageset`).

### Android

Assets are generated inside qualified drawable sub folders (`drawable-hdpi`, `drawable-xhdpi`, ...).

### UWP

Assets are generated with a qualified suffix indicating the scale (`scale-100`, `scale-240`, ...).

## Cli

The standalone `.exe` CLI used by the build task is available from the `/tools` folder of the NuGet package.

## Under the hood

The tool uses [SkiaSharp](https://github.com/mono/SkiaSharp) the resize all images.

## Contributions

Contributions are welcome! If you find a bug please report it and if you want a feature please report it.

If you want to contribute code please file an issue and create a branch off of the current dev branch and file a pull request.

## License

MIT © [Aloïs Deniel](http://aloisdeniel.github.io)
