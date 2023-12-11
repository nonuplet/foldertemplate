# FolderTemplate

テンプレートに沿って自動でフォルダを生成するUnityエディタ拡張です。

## 導入

`Window` -> `Package Manager` で Unity Package Managerを開きます。

左上の `+` から `Add package from git URL`, `https://github.com/nonuplet/foldertemplate.git#pkg` を入力してください。

## 使い方

メニューバーの `Tools` -> `FolderTemplate` から起動します。

プロジェクト名を指定して、フォルダ構造のテンプレートを指定してから `Generate` を押すと自動で生成されます。  
`Asset/プロジェクト名` 以下にフォルダが生成されます。

![image](https://github.com/nonuplet/foldertemplate/assets/130939038/02ab4940-801c-4920-8b57-d8423af6d855)

## 初期テンプレート

初期テンプレートはUnityの公式ガイドに基づいています。  
VCSのコミット時にフォルダが消えないように、自動的に `.gitkeep` ファイルが追加されます。

https://unity.com/ja/how-to/organizing-your-project

小規模なプロジェクトならとりあえず `Simple_AssetType` を使っておけば問題ないです。

## 自分で定義する

テンプレートはjsonファイルで定義することができます。

Unityのプロジェクト直下(`Asset` フォルダと同じ階層)に `folder-structure.json` を作ります。

以下に例を示します。
```json
{
  "options": {
    "project_name": "foobar",
    "git": true
  },
  "folders": [
    "Art/Materials",
    "Art/Models",
    "Art/Textures",
    "Audio/Music",
    "Audio/Sound",
    "Code/Scripts",
    "Code/Shaders",
    "Docs",
    "Level/Prefabs",
    "Level/Scenes",
    "Level/UI"
  ]
}
```

- **options**
  - `project_name` : プロジェクト名
  - `git` : .gitkeepの生成
- **folders** : 生成したいフォルダ

初期テンプレートは `<Package Root>/Configs` に入っているので参考に使用してください。

https://github.com/nonuplet/foldertemplate/tree/main/Assets/k5e/FolderTemplate/Configs
